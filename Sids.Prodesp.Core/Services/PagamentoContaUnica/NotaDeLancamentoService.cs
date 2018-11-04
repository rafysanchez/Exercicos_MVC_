using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Core.Services.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class NotaDeLancamentoService : CommonService
    {
        private readonly ReclassificacaoRetencaoService _service;
        private readonly NlParametrizacaoService _parametrizacaoService;
        private readonly ParaRestoAPagarService _paraRestoAPagarService;

        private const string FORMATOPE = "PE0{0}";
        private const int TAMANHOOBS = 76;



        public NotaDeLancamentoService(ILogError log, ICommon common, IChaveCicsmo chave, ReclassificacaoRetencaoService service
            , NlParametrizacaoService parametrizacaoService, ParaRestoAPagarService paraRestoAPagarService)
                : base(log, common, chave)
        {

            _service = service;
            _parametrizacaoService = parametrizacaoService;
            _paraRestoAPagarService = paraRestoAPagarService;
        }

        public AcaoEfetuada GerarNotasLancamento(ConfirmacaoPagamento confirmacaoPagamento, Usuario user, bool transmitir, int recursoId, EnumAcao action)
        {
            List<ReclassificacaoRetencao> nlsGeradas = new List<ReclassificacaoRetencao>();

            var regional = _regional.Buscar(new Regional { Id = Convert.ToInt32(user.RegionalId) }).FirstOrDefault();

            var ug = regional?.Uge;

            foreach (ConfirmacaoPagamentoItem itemConfirmacao in confirmacaoPagamento.Items)
            {
                var gerarNls = confirmacaoPagamento.IdTipoDocumento != 40 && itemConfirmacao.IdTipoDespesa.Value == 78;

                if (gerarNls)
                {
                    // usar caso existam RAPs
                    List<ParaRestoAPagar> tiposRap = _paraRestoAPagarService.Listar(new ParaRestoAPagar()).ToList();
                    List<NlParametrizacao> parametrizacoes = _parametrizacaoService.ObterTodas();


                    var tipoDocumento = confirmacaoPagamento.NumeroDocumento.Substring(0, 2);
                    var isSubempenho = tipoDocumento.Equals(EnumTipoDocumento.Subempenho.ToString("d2"));
                    var isRap = tipoDocumento.Equals(EnumTipoDocumento.Rap.ToString("d2"));
                    var isNlPgObras = VerrificarNlPgObras(itemConfirmacao.NaturezaDespesa);

                    int anoAtual = int.Parse(DateTime.Today.ToString("YY"));
                    int ano = anoAtual;

                    if (isSubempenho)
                    {
                        ano = int.Parse(confirmacaoPagamento.NumeroDocumento.Substring(5, 2));
                    }
                    else if (isRap)
                    {
                        ano = int.Parse(confirmacaoPagamento.NumeroDocumento.Substring(2, 2));
                    }


                    var parametrosGeracaoNl = new ParametrosGeracaoNl(confirmacaoPagamento, itemConfirmacao, parametrizacoes, tiposRap);
                    parametrosGeracaoNl.Ug = ug;
                    parametrosGeracaoNl.IsNlPgObras = isNlPgObras;
                    parametrosGeracaoNl.IsSubempenho = isSubempenho;
                    parametrosGeracaoNl.IsRap = isRap;
                    parametrosGeracaoNl.AnoAtual = anoAtual;
                    parametrosGeracaoNl.Ano = ano;



                    // TODO codificar NLs de repasse
                    #region NLs de repasse
                    // NL de repasse financeiro
                    ReclassificacaoRetencao nlRepasseFinanceiro = GerarNlRepasseFinanceiro(parametrosGeracaoNl);
                    nlRepasseFinanceiro.CodigoInscricao = string.Format(FORMATOPE, ug);
                    nlsGeradas.Add(nlRepasseFinanceiro);

                    // NL de repasse de IR                    
                    if (true) // TODO Cadastrar NL de “Repasse de Imposto de Renda” se para a fonte o valor estiver diferente de 0. 
                    {
                        ReclassificacaoRetencao nlRepasseDeIr = GerarNlRepasseDeIr(parametrosGeracaoNl);
                        nlsGeradas.Add(nlRepasseDeIr);
                    }


                    // NL de repasse para regionais 
                    // TODO Cadastrar uma NL para cada OP (Ordem de Pagamento).

                    var ops = confirmacaoPagamento.Items.GroupBy(x => x.NumeroOp).Select(x => new KeyValuePair<string, List<ConfirmacaoPagamentoItem>>(x.Key, x.ToList()));
                    foreach (var op in ops)
                    {
                        ReclassificacaoRetencao nlDeRepasseParaRegionais = GerarNlDeRepasseParaRegionais(parametrosGeracaoNl, op);
                        nlsGeradas.Add(nlDeRepasseParaRegionais);
                    }
                    #endregion

                    // TODO codificar NLs de baixa
                    #region NLs de baixa
                    // NL de baixa jeton jari (por NE)
                    // NL de baixa de fornecedor com contrato (por credor + NE)
                    // NL para baixa de pagamento de licença prêmio (por OP)
                    // NL de baixa pagamento outros (por credor + NE)
                    // NL para baixa de devolução de caução (por OP)
                    // NL para baixa de pagamento de adiantamento (por credor + NE)
                    // NL para baixa de pagamento de restituição de multa (por NE)
                    // NL para baixa de pagamento de perito do quadro e judicial (por credor + NE)
                    // NL para baixa de pagamento de bonificação por resultados (por credor + NE) 
                    #endregion


                    var idNlRepasseFinanceiro = _service.SalvarOuAlterar(nlRepasseFinanceiro, recursoId, (short)action); // TODO verificar

                    if (transmitir)
                    {
                        _service.Transmitir(idNlRepasseFinanceiro, user, recursoId); // TODO transmitir separadas?
                    }
                }
            }

            return AcaoEfetuada.Sucesso;
        }

        private ReclassificacaoRetencao GerarNlRepasseFinanceiro(ParametrosGeracaoNl parametrosGeracao)
        {
            var rr = new ReclassificacaoRetencao();
            var eventos = new List<ReclassificacaoRetencaoEvento>();
            var notas = new List<ReclassificacaoRetencaoNota>();

            // TODO verificar propriedades
            DefinirPropriedadesComuns(parametrosGeracao, rr);

            //obj.CodigoEvento =  //adicionar?
            //obj.CodigoInscricao =  //adicionar?
            //obj.CodigoClassificacao = 
            //obj.CodigoFonte = 
            //obj.Valor = itemConfirmacao.ValorDocumento;
            //obj.DescricaoObservacao1 = 
            //obj.Notas = //

            var parametrizacao = parametrosGeracao.ParametrizacoesNl.FirstOrDefault(x => x.Despesas.Any(y => y.IdTipo == parametrosGeracao.ItemConfirmacao.IdTipoDespesa));
            if (parametrizacao != null)
            {
                rr.CodigoGestao = parametrizacao.FavorecidaNumeroGestao == 0 ? "16055" : parametrizacao.FavorecidaNumeroGestao.ToString(); // geral
                rr.CodigoInscricao = String.Format("PE0{0}", parametrosGeracao.Ug);
                rr.CodigoUnidadeGestora = parametrizacao.UnidadeGestora.HasValue ? parametrizacao.UnidadeGestora.Value.ToString() : string.Empty;
                // TODO valor
                // TODO UG Favorecido já definido no começo?
                // TODO Gestão Favorecido já definido no começo?
                rr.DescricaoObservacao1 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 1);
                rr.DescricaoObservacao2 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 2);
                rr.DescricaoObservacao3 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 3);

                GerarEventoEntrada(rr, eventos, parametrizacao);
                GerarEventosSaida(parametrosGeracao, rr, eventos, parametrizacao);
            }

            // TODO gerar notas

            return rr;
        }

        private ReclassificacaoRetencao GerarNlRepasseDeIr(ParametrosGeracaoNl parametrosGeracao)
        {
            var rr = new ReclassificacaoRetencao();
            var eventos = new List<ReclassificacaoRetencaoEvento>();
            var notas = new List<ReclassificacaoRetencaoNota>();

            // TODO verificar propriedades
            DefinirPropriedadesComuns(parametrosGeracao, rr);

            var parametrizacao = parametrosGeracao.ParametrizacoesNl.FirstOrDefault(x => x.Despesas.Any(y => y.IdTipo == parametrosGeracao.ItemConfirmacao.IdTipoDespesa));
            if (parametrizacao != null)
            {
                rr.CodigoGestao = parametrizacao.FavorecidaNumeroGestao.ToString(); // geral
                rr.CodigoInscricao = string.Empty;
                // rr.CodigoUnidadeGestora = parametrizacao.UnidadeGestora.HasValue ? parametrizacao.UnidadeGestora.Value.ToString() : string.Empty;
                // TODO valor
                // TODO UG Favorecido já definido no começo?
                // TODO Gestão Favorecido já definido no começo?
                rr.DescricaoObservacao1 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 1);
                rr.DescricaoObservacao2 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 2);
                rr.DescricaoObservacao3 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 3);


                GerarEventoEntrada(rr, eventos, parametrizacao);
                GerarEventosSaida(parametrosGeracao, rr, eventos, parametrizacao);
                // TODO Na coluna “Inscrição do Evento” de saída, é formatado com o número da “Unidade Gestora” seguido do número da Gestão, devera ser desconsiderada, UG e Gestão do usuário e obter UG e Gestão parametrizada para a NL.
            }

            // TODO gerar notas

            return rr;
        }

        private ReclassificacaoRetencao GerarNlDeRepasseParaRegionais(ParametrosGeracaoNl parametrosGeracao, KeyValuePair<string, List<ConfirmacaoPagamentoItem>> ops)
        {
            var rr = new ReclassificacaoRetencao();
            var eventos = new List<ReclassificacaoRetencaoEvento>();
            var notas = new List<ReclassificacaoRetencaoNota>();

            // TODO verificar propriedades
            DefinirPropriedadesComuns(parametrosGeracao, rr);

            var parametrizacao = parametrosGeracao.ParametrizacoesNl.FirstOrDefault(x => x.Despesas.Any(y => y.IdTipo == parametrosGeracao.ItemConfirmacao.IdTipoDespesa));
            if (parametrizacao != null)
            {
                rr.CodigoGestao = parametrizacao.FavorecidaNumeroGestao.ToString(); // geral
                rr.CodigoInscricao = string.Empty;
                // rr.CodigoUnidadeGestora = parametrizacao.UnidadeGestora.HasValue ? parametrizacao.UnidadeGestora.Value.ToString() : string.Empty;
                // TODO valor
                // TODO UG Favorecido já definido no começo?
                // TODO Gestão Favorecido já definido no começo?
                rr.DescricaoObservacao1 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 1);
                rr.DescricaoObservacao2 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 2);
                rr.DescricaoObservacao3 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 3);


                GerarEventoEntrada(rr, eventos, parametrizacao);
                GerarEventosSaida(parametrosGeracao, rr, eventos, parametrizacao);
                // TODO Na coluna “Inscrição do Evento” de saída, é formatado com o número da “Unidade Gestora” seguido do número da Gestão, devera ser desconsiderada, UG e Gestão do usuário e obter UG e Gestão parametrizada para a NL.
            }

            // TODO gerar notas

            return rr;
        }

        private void DefinirPropriedadesComuns(ParametrosGeracaoNl parametrosGeracao, ReclassificacaoRetencao rr)
        {
            rr.Id = 0; // geral
            rr.DataEmissao = rr.DataCadastro = DateTime.Now; // geral
            rr.CodigoUnidadeGestora = parametrosGeracao.Ug; // geral

            rr.NumeroOriginalSiafemSiafisico = parametrosGeracao.IsNlPgObras ? parametrosGeracao.ItemConfirmacao.NumeroEmpenho : string.Empty; // geral
            rr.NormalEstorno = parametrosGeracao.IsNlPgObras ? ((int)EnumNormalEstorno.Normal).ToString() : string.Empty; // geral
            rr.AnoMedicao = parametrosGeracao.IsNlPgObras ? FormatarAnoMedicao(parametrosGeracao.ItemConfirmacao.Referencia) : null; // geral
            rr.MesMedicao = parametrosGeracao.IsNlPgObras ? FormatarMesMedicao(parametrosGeracao.ItemConfirmacao.Referencia) : null; // geral
            rr.NumeroCNPJCPFCredor = parametrosGeracao.ItemConfirmacao.NumeroCnpjCpfUgCredor; // geral
            rr.CodigoGestaoCredor = parametrosGeracao.ItemConfirmacao.CodigoGestaoCredor; // geral TODO como verificar se o favorecido é igual à UG?

            rr.DocumentoTipoId = parametrosGeracao.ConfirmacaoPagamento.IdTipoDocumento!=null? Convert.ToInt32(parametrosGeracao.ConfirmacaoPagamento.IdTipoDocumento):0; // geral
            rr.NumeroDocumento = parametrosGeracao.ConfirmacaoPagamento.NumeroDocumento; // geral

            rr.AgrupamentoConfirmacao = parametrosGeracao.ConfirmacaoPagamento.CodigoAgrupamentoConfirmacaoPagamento; // geral

            rr.Origem = OrigemReclassificacaoRetencao.ConfirmacaoDePagamento; // geral
        }

        private static void GerarEventoEntrada(ReclassificacaoRetencao rr, List<ReclassificacaoRetencaoEvento> eventos, NlParametrizacao parametrizacao)
        {
            var eventoEntradaParametrizado = parametrizacao.Eventos.FirstOrDefault(x => x.EntradaSaidaDescricao.Equals(EnumDirecaoEvento.Entrada.ToString()));
            if (eventoEntradaParametrizado != null)
            {
                rr.CodigoEvento = eventoEntradaParametrizado.NumeroEvento.ToString();
                rr.CodigoClassificacao = eventoEntradaParametrizado.NumeroClassificacao.ToString();

                var evento = new ReclassificacaoRetencaoEvento();
                evento.NumeroEvento = eventoEntradaParametrizado.NumeroEvento.ToString();
                evento.Classificacao = eventoEntradaParametrizado.NumeroClassificacao.ToString();
                evento.Fonte = eventoEntradaParametrizado.NumeroFonte.ToString();

                eventos.Add(evento);
            }
        }

        private static void GerarEventosSaida(ParametrosGeracaoNl parametrosGeracao, ReclassificacaoRetencao rr, List<ReclassificacaoRetencaoEvento> eventos, NlParametrizacao parametrizacao)
        {
            var eventoSaidaParametrizado = parametrizacao.Eventos.FirstOrDefault(x => x.EntradaSaidaDescricao.Equals(EnumDirecaoEvento.Saida.ToString()));
            if (eventoSaidaParametrizado != null)
            {
                var evento = new ReclassificacaoRetencaoEvento();
                evento.InscricaoEvento = String.Format("{0}{1}{2}", parametrosGeracao.ItemConfirmacao.NumeroBancoPagador, parametrosGeracao.ItemConfirmacao.NumeroAgenciaPagador, parametrosGeracao.ItemConfirmacao.NumeroContaPagador);
                evento.Classificacao = eventoSaidaParametrizado.NumeroClassificacao.ToString();
                evento.Fonte = eventoSaidaParametrizado.NumeroFonte.ToString();

                if (parametrosGeracao.IsSubempenho)
                {
                    evento.NumeroEvento = eventoSaidaParametrizado.NumeroEvento.ToString();
                }
                else if (parametrosGeracao.IsRap)
                {
                    // Para RAP de exercício anterior de despesa prevista (Data da Realização igual a 0) e Ano da NL igual ao ano do exercício, evento = Parametrizado para o tipo de rap “N - Não Proces.Transf.P / Proces.De Exerc.Anterior”;
                    // Para RAP de exercício anterior de despesa prevista (Data da Realização igual a 0) e Ano da NL igual ao ano anterior ao exercício, evento igual a V - Não Proces.Transf.P / proces.De Exerc.Anteriores;
                    // Para RAP com Data da Realização diferente de 0 e Ano da NL igual ao ano anterior ao exercício, evento = Parametrizado para o tipo de rap “P - Processado do Exercício anterior”;
                    // Para RAP de outros exercícios anterior, evento R-Revigorados de Exercícios Anteriores

                    var isAnoAnterior = parametrosGeracao.Ano < parametrosGeracao.AnoAtual;
                    if (isAnoAnterior)
                    {
                        // TODO evento.NumeroEvento =  ???
                    }

                }
                eventos.Add(evento);
            }

            rr.Eventos = eventos;
        }

        #region Métodos Auxiliares
        private string FormatarAnoMedicao(string referencia)
        {
            // Ano da Medição = Apenas para NLPGObras, os dígitos da posição 27 e 28 do campo Referência 
            // retornado da consulta de pagamentos a confirmar, completar o ano para formatar 4 dígitos(2017).
            if (!String.IsNullOrEmpty(referencia))
            {
                var anoReferencia = referencia.Substring(26, 2);

                return string.Format("20{0}", anoReferencia);
            }

            return string.Empty;
        }

        private string FormatarMesMedicao(string referencia)
        {
            // Mês da medição = Apenas para NLPGObras, os dígitos da posição 24 e 25  do campo Referência 
            // retornado da consulta de pagamentos a confirmar, 
            if (!String.IsNullOrEmpty(referencia))
            {
                var mesReferencia = referencia.Substring(23, 2);

                return mesReferencia;
            }

            return string.Empty;
        }

        private bool VerrificarNlPgObras(int? naturezaDespesa)
        {
            // TODO implementar regra NLPGObras
            // FA3 – Notas de Lançamento de baixa para pagamento de obras - NLPGObras[RDN1][RDN4]
            // 1 – Para o cadastro das NL’s de Obra, se o nº do CED retornado da consulta de pagamentos a confirmar 
            // o primeiro digito for = 4 ou o primeiro digito = 3 e o quinto e sexto digito = “3” e “9”, 
            // o sistema deverá acionar o WebService do SIAFEM(CONNE) enviando a Unidade Gestora, a Gestão e o Número do Empenho 
            // para verificar se o empenho possui identificador da obra e se retorno positivo, com os dados retornados da consulta 
            // pagamentos a confirmar o sistema deverá cadastrar uma NLPGObras;

            if (naturezaDespesa.HasValue)
            {
                var ced = naturezaDespesa.Value.ToString().RemoveSpecialChar();
                var primeiroDigito = ced.Substring(0, 1);
                var quintoSextoDigitos = ced.Substring(4, 2);

                if (primeiroDigito.Equals("3") || primeiroDigito.Equals("4") && quintoSextoDigitos.Equals("39")) // TODO remover hardcode
                {
                    return true;
                }
            }

            return false;
        }

        private static string SepararObservacao(string observacaoInteira, int tamanho, int posicao)
        {
            int inicio = (posicao - 1) * tamanho;
            int maximo = observacaoInteira.Length;
            var restante = maximo - inicio;
            tamanho = restante >= tamanho ? tamanho : restante;

            var selectedText = observacaoInteira.Substring(inicio, tamanho);

            var obs = string.IsNullOrWhiteSpace(selectedText) ? null : selectedText;

            return obs;
        }
        #endregion

        internal class ParametrosGeracaoNl
        {
            public List<ParaRestoAPagar> TiposRap { get; set; }
            public ConfirmacaoPagamento ConfirmacaoPagamento { get; set; }
            public List<NlParametrizacao> ParametrizacoesNl { get; set; }
            public ConfirmacaoPagamentoItem ItemConfirmacao { get; set; }
            public string Ug { get; set; }
            public bool IsSubempenho { get; set; }
            public bool IsRap { get; set; }
            public int AnoAtual { get; set; }
            public int Ano { get; set; }
            public bool IsNlPgObras { get; internal set; }

            public ParametrosGeracaoNl(ConfirmacaoPagamento confirmacaoPagamento, ConfirmacaoPagamentoItem itemConfirmacao, List<NlParametrizacao> parametrizacoes, List<ParaRestoAPagar> TiposRap)
            {
                this.ConfirmacaoPagamento = confirmacaoPagamento;
                this.ParametrizacoesNl = parametrizacoes;
                this.ItemConfirmacao = itemConfirmacao;
            }
        }
    }
}
