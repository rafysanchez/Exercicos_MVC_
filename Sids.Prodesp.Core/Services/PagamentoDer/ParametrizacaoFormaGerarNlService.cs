using Sids.Prodesp.Core.Services.PagamentoContaUnica;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.Empenho;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.FormaGerarNl;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Core.Services.PagamentoDer
{
    public class ParametrizacaoFormaGerarNlService : Base.BaseService
    {
        private readonly ConfirmacaoPagamentoItemDal _paraConfirmacaoPagamentoItemDal;
        private readonly NlParametrizacaoFormaGerarNlDal _paraNlParametrizacaoFormaGerarNlDal;
        private readonly NlParametrizacaoService _parametrizacaoService;
        private readonly ParaRestoAPagarService _paraRestoAPagarService;
        private readonly ReclassificacaoRetencaoService _reclassificacaoRetencaoService;
        private readonly ConfirmacaoPagamentoDal _paraConfirmacaoPagamentoIDal;
        private readonly CommonService _commomService;
        private readonly NlParametrizacaoEventoDal _nlParametrizacaoEventoDal;
        private readonly ReclassificacaoRetencaoNotaService _reclassificacaoRetencaoNotaService;
        private const int TAMANHOOBS = 228;


        public ParametrizacaoFormaGerarNlService(ILogError log, ConfirmacaoPagamentoItemDal paraConfirmacaoPagamentoItemDal, NlParametrizacaoFormaGerarNlDal paraNlParametrizacaoFormaGerarNlDal,
                NlParametrizacaoService parametrizacaoService, ParaRestoAPagarService paraRestoAPagarService, ReclassificacaoRetencaoService reclassificacaoRetencaoService, ConfirmacaoPagamentoDal paraConfirmacaoPagamentoIDal,
                 CommonService commomService, NlParametrizacaoEventoDal nlParametrizacaoEventoDal, ReclassificacaoRetencaoNotaService reclassificacaoRetencaoNotaService) : base(log)
        {
            _paraConfirmacaoPagamentoItemDal = paraConfirmacaoPagamentoItemDal;
            _paraNlParametrizacaoFormaGerarNlDal = paraNlParametrizacaoFormaGerarNlDal;
            _parametrizacaoService = parametrizacaoService;
            _paraRestoAPagarService = paraRestoAPagarService;
            _reclassificacaoRetencaoService = reclassificacaoRetencaoService;
            _paraConfirmacaoPagamentoIDal = paraConfirmacaoPagamentoIDal;
            _commomService = commomService;
            _nlParametrizacaoEventoDal = nlParametrizacaoEventoDal;
            _reclassificacaoRetencaoNotaService = reclassificacaoRetencaoNotaService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">IdConfirmacaoPagamento</param>
        /// <param name="confirmacaoPagamento"></param>
        /// <param name="usuario"></param>
        /// <param name="regionais"></param>
        /// <returns></returns>
        public string GerarNl(int id, ConfirmacaoPagamento confirmacaoPagamento, Usuario usuario, List<Regional> regionais)
        {
            try
            {

                // Listar todos Itens Aprovados
                //Comentar quando POC estiver ok -- apenas para testar - após retirar item1
                var itens = _paraConfirmacaoPagamentoItemDal.Fetch(new ConfirmacaoPagamentoItem { IdConfirmacaoPagamento = id }).ToList();

                // int? idConformacao = id;

                // item1- descomentar quando terminar
                // var itens = _paraConfirmacaoPagamentoItemDal.Fetch(a).Where(x => x.StatusProdesp == "S").ToList();


                var itensAposValidacao = new ConfirmacaoPagamentoItem().ValidacaoItens(itens);


                var itensPorFormasNl = ObterTipoDespesaPorFormaGerarNl(itensAposValidacao);
                confirmacaoPagamento.Id = id;
                confirmacaoPagamento.Items = itensPorFormasNl;
                confirmacaoPagamento.CodigoAgrupamentoConfirmacaoPagamento = _paraConfirmacaoPagamentoIDal.Fetch(new ConfirmacaoPagamento { Id = id }).FirstOrDefault().CodigoAgrupamentoConfirmacaoPagamento;
                return GerarNlPorForma(confirmacaoPagamento, usuario, regionais);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        private List<ConfirmacaoPagamentoItem> ObterTipoDespesaPorFormaGerarNl(List<ConfirmacaoPagamentoItem> itens)
        {


            List<int?> listIdTipoDespesa = new List<int?>();
            //Todos os Tipos de Despesa
            foreach (var item in itens)
                listIdTipoDespesa.Add(item.IdTipoDespesa);

            List<FormaGerarNl> formasGerarNlPorTipoDespesa = new List<FormaGerarNl>();

            var filtro = new FormaGerarNl();

            //Cada tipo de Despesa possui uma forma de Gerar NL
            foreach (var tipoDespesa in listIdTipoDespesa.Distinct())
            {
                filtro.IdDespesaTipo = tipoDespesa ?? 0;
                formasGerarNlPorTipoDespesa.Add(GetFormaGerarNlPorTipoDespesa(filtro));
            }


            //Setando cada item da forma que ira gerar a NL (OP, Credor/Empenho ou Empenho)
            foreach (var item in itens)
            {
                var result = formasGerarNlPorTipoDespesa.Where(x => x.IdDespesaTipo == item.IdTipoDespesa).FirstOrDefault();
                item.FormaGerar = result.FormaGerar;
                item.CodigoTipoDespesa = result.CodigoTipoDespesa;
                item.IdNlParametrizacao = result.IdNlParametrizacao;
                item.numeroTipoNl = result.numeroTipoNl;

            }


            return itens;

        }


        private FormaGerarNl GetFormaGerarNlPorTipoDespesa(FormaGerarNl entity)
        {
            return _paraNlParametrizacaoFormaGerarNlDal.GetFormaGerarNlPorTipoDespesa(entity);
        }



        private IEnumerable<IGrouping<dynamic, ConfirmacaoPagamentoItem>> ObterItensPorCredorEmpenhoDespesa(List<ConfirmacaoPagamentoItem> entidades)
        {

            var agrupadosPorCredorEmpenho = entidades.GroupBy(x => new { x.NumeroBancoFavorecido, x.NumeroAgenciaFavorecido, x.NumeroContaFavorecido, x.NumeroEmpenhoSiafem, x.IdTipoDespesa });

            return agrupadosPorCredorEmpenho;
        }

        private IEnumerable<IGrouping<dynamic, ConfirmacaoPagamentoItem>> ObterItensPorEmpenhoDespesa(List<ConfirmacaoPagamentoItem> entidades)
        {

            var agrupadosPorEmpenhoDespesa = entidades.GroupBy(x => new { x.NumeroEmpenhoSiafem, x.IdTipoDespesa });

            return agrupadosPorEmpenhoDespesa;
        }

        private IEnumerable<IGrouping<dynamic, ConfirmacaoPagamentoItem>> ObterItensPorOpDespesa(List<ConfirmacaoPagamentoItem> entidades)
        {

            var agrupadosPorOpDespesa = entidades.GroupBy(x => new { x.NumeroOp, x.IdTipoDespesa });

            return agrupadosPorOpDespesa;
        }

        private string GerarNlPorForma(ConfirmacaoPagamento confirmacao, Usuario usuario, List<Regional> regionais, int recursoId = 0)
        {
            try
            {
                List<ReclassificacaoRetencao> nlsGeradas = new List<ReclassificacaoRetencao>();

                List<ParaRestoAPagar> tiposRap = _paraRestoAPagarService.Listar(new ParaRestoAPagar()).ToList();
                List<NlParametrizacao> parametrizacoesCadastradas = _parametrizacaoService.ObterTodas();

                // inicio da area de Nl de repasse


                var nlRepasse = FormacaoRepasse(confirmacao, tiposRap, parametrizacoesCadastradas, usuario, regionais);

                var idnlRepasseLinha = _reclassificacaoRetencaoService.SalvarOuAlterar(nlRepasse, 25, (short)EnumAcao.Inserir);

                // fim do repasse
                if (confirmacao.Items.Any(x => x.FormaGerar == EnumFormaGerarNl.CredorEmpenho))
                {
                    var nlsPorCredorEmpenhoDespesa = ObterItensPorCredorEmpenhoDespesa(confirmacao.Items.Where(x => x.FormaGerar == EnumFormaGerarNl.CredorEmpenho).ToList());

                    foreach (var nlPorCredorEmpenhoDespesa in nlsPorCredorEmpenhoDespesa)
                    {
                        var baseNl = FormacaoBaseNl(confirmacao, tiposRap, nlPorCredorEmpenhoDespesa, parametrizacoesCadastradas, usuario, regionais);

                        nlsGeradas.Add(NlPorTipoDespesa(nlPorCredorEmpenhoDespesa.ToList(), baseNl));
                    }
                }

                if (confirmacao.Items.Any(x => x.FormaGerar == EnumFormaGerarNl.Empenho))
                {
                    var nlsPorEmpenhoDespesa = ObterItensPorEmpenhoDespesa(confirmacao.Items.Where(x => x.FormaGerar == EnumFormaGerarNl.Empenho).ToList());
                    foreach (var nlPorEmpenhoDespesa in nlsPorEmpenhoDespesa)
                    {
                        var baseNl = FormacaoBaseNl(confirmacao, tiposRap, nlPorEmpenhoDespesa, parametrizacoesCadastradas, usuario, regionais);

                        nlsGeradas.Add(NlPorTipoDespesa(nlPorEmpenhoDespesa.ToList(), baseNl));
                    }
                }

                if (confirmacao.Items.Any(x => x.FormaGerar == EnumFormaGerarNl.Op))
                {
                    var nlsPorOpDespesa = ObterItensPorOpDespesa(confirmacao.Items.Where(x => x.FormaGerar == EnumFormaGerarNl.Op).ToList());
                    foreach (var nlPorOpDespesa in nlsPorOpDespesa)
                    {
                        var baseNl = FormacaoBaseNl(confirmacao, tiposRap, nlPorOpDespesa, parametrizacoesCadastradas, usuario, regionais);

                        nlsGeradas.Add(NlPorTipoDespesa(nlPorOpDespesa.ToList(), baseNl));
                    }
                }



                // com os dados organizados  - processa para amrmazenar - rafael magna 2018
                foreach (var nlGerada in nlsGeradas)
                {
                    // 25 - Reclassificação Retenção
                    var idReclassificacaoRentencao = _reclassificacaoRetencaoService.SalvarOuAlterar(nlGerada, 25, (short)EnumAcao.Inserir);

                    // ADICIONA OS DADOS nl-> NOTA
                    ReclassificacaoRetencaoNota nota = new ReclassificacaoRetencaoNota();
                    nota.IdReclassificacaoRetencao = idReclassificacaoRentencao;
                    nota.Ordem = 1;
                    nota.CodigoNotaFiscal = "NADA CONSTA";
                    _reclassificacaoRetencaoNotaService.SalvarOuAlterar(nota, recursoId, (short)EnumAcao.Inserir);
                    // FIM DE ADICAO NOTA

                    foreach (var item in nlGerada.itensPertenceNl)
                    {
                        _paraConfirmacaoPagamentoItemDal.EditReclassificacaoRetencao(new ConfirmacaoPagamentoItem { Id = item.Id, IdReclassificacaoRetencao = idReclassificacaoRentencao });
                    }

                }
                // gerar os totais

                // fim 
                return "sucesso";
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }



        private ReclassificacaoRetencao NlPorTipoDespesa(List<ConfirmacaoPagamentoItem> grupoItensPorTipoDespesa, ReclassificacaoRetencao nlParametrizada)
        {

            //Gravar as Nls por TipoDespesa
            switch ((EnumTipoNl)grupoItensPorTipoDespesa.FirstOrDefault().numeroTipoNl)
            {
                case EnumTipoNl.RepasseFinanceiro:
                    return NlPorRepasseFinanceiro(grupoItensPorTipoDespesa, nlParametrizada);
                    break;

            }

            return nlParametrizada;
        }

        private ReclassificacaoRetencao FormacaoRepasse(ConfirmacaoPagamento confirmacaoPagamento, List<ParaRestoAPagar> tiposRap, List<NlParametrizacao> parametrizacoesCadastradas, Usuario usuario, List<Regional> regionais)
        {
            //Os itens ja se encontram separados por Tipo de Despesa
            var infoBaseItem = confirmacaoPagamento.Items.FirstOrDefault();

            //Informações do item na parametrização
            var infoBaseParametrizacao = parametrizacoesCadastradas.Where(x => x.Id == 74).FirstOrDefault();

            infoBaseParametrizacao.Eventos = _nlParametrizacaoEventoDal.Fetch(new NlParametrizacaoEvento { IdNlParametrizacao = infoBaseParametrizacao.Id });

            ReclassificacaoRetencao entidadeNl = new ReclassificacaoRetencao();

            if (regionais.Any(x => x.Descricao.Contains(infoBaseItem.IdRegional.ToString())))
                entidadeNl.RegionalId = regionais.Where(x => x.Descricao.Contains(infoBaseItem.IdRegional.ToString())).FirstOrDefault().Id;

            //Atributos em comum
            entidadeNl.Id = 0; // geral
            entidadeNl.DataEmissao = entidadeNl.DataCadastro = DateTime.Now; // geral
            entidadeNl.CodigoUnidadeGestora = infoBaseParametrizacao.UnidadeGestora.HasValue ? infoBaseParametrizacao.UnidadeGestora.Value.ToString() : string.Empty;
            entidadeNl.NumeroProcesso = infoBaseItem.NumeroProcesso;
            entidadeNl.NumeroDocumento = infoBaseItem.NrDocumento;
            entidadeNl.DataCadastro = DateTime.Now.Date;

            entidadeNl.CodigoAplicacaoObra = infoBaseItem.CodigoObra;
            entidadeNl.NumeroCNPJCPFFornecedorId = infoBaseItem.NumeroCnpjCpfUgCredor;
            entidadeNl.NormalEstorno = "1";
            entidadeNl.NotaLancamenoMedicao = infoBaseItem.NumeroNlDocumento;
            entidadeNl.NumeroContrato = infoBaseItem.NumeroContrato;
            entidadeNl.CadastroCompleto = true;
            entidadeNl.Origem = OrigemReclassificacaoRetencao.ConfirmacaoDePagamento;
            // entidadeNl.NumeroCNPJCPFCredor = infoBaseParametrizacao.FavorecidaCnpjCpfUg; comentado  - verificar
            entidadeNl.NumeroCNPJCPFCredor = infoBaseItem.NumeroCnpjCpfUgCredor;
            entidadeNl.CodigoGestaoCredor = infoBaseItem.CodigoGestaoCredor;
            entidadeNl.CodigoGestao = "16055";
            entidadeNl.DescricaoObservacao1 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 1);
            entidadeNl.DescricaoObservacao2 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 2);
            entidadeNl.DescricaoObservacao3 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 3);
            entidadeNl.Valor = confirmacaoPagamento.Items.Sum(x => x.ValorDocumento * 100) ?? 0;
            entidadeNl.AgrupamentoConfirmacao = confirmacaoPagamento.CodigoAgrupamentoConfirmacaoPagamento;
            entidadeNl.id_confirmacao_pagamento = confirmacaoPagamento.Id;
            AtribuiValorCampoAnoExercicio(infoBaseItem, entidadeNl);

            var NLPGOBRAS = AtribuiValoresQuandoNLPGOBRAS(usuario, infoBaseItem, infoBaseParametrizacao, entidadeNl);

            List<ReclassificacaoRetencaoEvento> eventos = new List<ReclassificacaoRetencaoEvento>();

            var parametrosGeracaoNl = new ParametrosGeracaoNl(confirmacaoPagamento, infoBaseItem, null, tiposRap);
            var tipoDocumento = infoBaseItem.NumeroDocumentoGerador.Substring(0, 2);
            parametrosGeracaoNl.IsNlPgObras = NLPGOBRAS;
            parametrosGeracaoNl.IsSubempenho = tipoDocumento.Equals(EnumTipoDocumento.Subempenho.GetHashCode().ToString("d2"));
            parametrosGeracaoNl.IsRap = tipoDocumento.Equals(EnumTipoDocumento.Rap.GetHashCode().ToString("d2"));
            parametrosGeracaoNl.valorAcumuladoNl = entidadeNl.Valor;
            entidadeNl.NumeroOriginalSiafemSiafisico = infoBaseItem.NumeroEmpenhoSiafem;
            entidadeNl.CodigoCredorOrganizacaoId = infoBaseItem.CodigoOrganizacaoCredor ?? 0;

            //***********novos campos - rafael magna see ************
            entidadeNl.DocumentoTipoId = infoBaseItem.IdTipoDocumento;
            //********** fim ****************************************

            GerarEventosSaida(parametrosGeracaoNl, eventos, infoBaseParametrizacao);
            GerarEventoEntrada(parametrosGeracaoNl, entidadeNl, eventos, infoBaseParametrizacao);
            entidadeNl.NormalEstorno = ((int)EnumNormalEstorno.Normal).ToString();


            entidadeNl.itensPertenceNl = confirmacaoPagamento.Items.ToList();

            return entidadeNl;


        }

        private ReclassificacaoRetencao FormacaoBaseNl(ConfirmacaoPagamento confirmacaoPagamento, List<ParaRestoAPagar> tiposRap, IGrouping<dynamic, ConfirmacaoPagamentoItem> itensNl, List<NlParametrizacao> parametrizacoesCadastradas, Usuario usuario, List<Regional> regionais)
        {
            //Os itens ja se encontram separados por Tipo de Despesa
            var infoBaseItem = itensNl.FirstOrDefault();

            //Informações do item na parametrização
            var infoBaseParametrizacao = parametrizacoesCadastradas.Where(x => x.Id == infoBaseItem.IdNlParametrizacao).FirstOrDefault();

            infoBaseParametrizacao.Eventos = _nlParametrizacaoEventoDal.Fetch(new NlParametrizacaoEvento { IdNlParametrizacao = infoBaseParametrizacao.Id });



            ReclassificacaoRetencao entidadeNl = new ReclassificacaoRetencao();



            if (regionais.Any(x => x.Descricao.Contains(infoBaseItem.IdRegional.ToString())))
                entidadeNl.RegionalId = regionais.Where(x => x.Descricao.Contains(infoBaseItem.IdRegional.ToString())).FirstOrDefault().Id;

            //Atributos em comum
            entidadeNl.Id = 0; // geral
            entidadeNl.DataEmissao = entidadeNl.DataCadastro = DateTime.Now; // geral
            entidadeNl.CodigoUnidadeGestora = infoBaseParametrizacao.UnidadeGestora.HasValue ? infoBaseParametrizacao.UnidadeGestora.Value.ToString() : string.Empty;
            entidadeNl.NumeroProcesso = infoBaseItem.NumeroProcesso;
            entidadeNl.NumeroDocumento = infoBaseItem.NrDocumento;
            entidadeNl.DataCadastro = DateTime.Now.Date;

            entidadeNl.CodigoAplicacaoObra = infoBaseItem.CodigoObra;
            entidadeNl.NumeroCNPJCPFFornecedorId = infoBaseItem.NumeroCnpjCpfUgCredor;
            entidadeNl.NormalEstorno = "1";
            entidadeNl.NotaLancamenoMedicao = infoBaseItem.NumeroNlDocumento;
            entidadeNl.NumeroContrato = infoBaseItem.NumeroContrato;
            entidadeNl.CadastroCompleto = true;
            entidadeNl.Origem = OrigemReclassificacaoRetencao.ConfirmacaoDePagamento;
            // entidadeNl.NumeroCNPJCPFCredor = infoBaseParametrizacao.FavorecidaCnpjCpfUg; comentado  - verificar
            entidadeNl.NumeroCNPJCPFCredor = infoBaseItem.NumeroCnpjCpfUgCredor;
            entidadeNl.CodigoGestaoCredor = infoBaseItem.CodigoGestaoCredor;
            entidadeNl.CodigoGestao = "16055";
            entidadeNl.DescricaoObservacao1 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 1);
            entidadeNl.DescricaoObservacao2 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 2);
            entidadeNl.DescricaoObservacao3 = SepararObservacao(infoBaseParametrizacao.Observacao, TAMANHOOBS, 3);
            entidadeNl.Valor = itensNl.Sum(x => x.ValorDocumento * 100) ?? 0;
            entidadeNl.AgrupamentoConfirmacao = confirmacaoPagamento.CodigoAgrupamentoConfirmacaoPagamento;
            entidadeNl.id_confirmacao_pagamento = confirmacaoPagamento.Id;
            AtribuiValorCampoAnoExercicio(infoBaseItem, entidadeNl);

            var NLPGOBRAS = AtribuiValoresQuandoNLPGOBRAS(usuario, infoBaseItem, infoBaseParametrizacao, entidadeNl);

            List<ReclassificacaoRetencaoEvento> eventos = new List<ReclassificacaoRetencaoEvento>();

            var parametrosGeracaoNl = new ParametrosGeracaoNl(confirmacaoPagamento, infoBaseItem, null, tiposRap);
            var tipoDocumento = infoBaseItem.NumeroDocumentoGerador.Substring(0, 2);
            parametrosGeracaoNl.IsNlPgObras = NLPGOBRAS;
            parametrosGeracaoNl.IsSubempenho = tipoDocumento.Equals(EnumTipoDocumento.Subempenho.GetHashCode().ToString("d2"));
            parametrosGeracaoNl.IsRap = tipoDocumento.Equals(EnumTipoDocumento.Rap.GetHashCode().ToString("d2"));
            parametrosGeracaoNl.valorAcumuladoNl = entidadeNl.Valor;
            entidadeNl.NumeroOriginalSiafemSiafisico = infoBaseItem.NumeroEmpenhoSiafem;
            entidadeNl.CodigoCredorOrganizacaoId = infoBaseItem.CodigoOrganizacaoCredor ?? 0;

            //***********novos campos - rafael magna see ************
            entidadeNl.DocumentoTipoId = infoBaseItem.IdTipoDocumento;
            //********** fim ****************************************

            GerarEventosSaida(parametrosGeracaoNl, eventos, infoBaseParametrizacao);
            GerarEventoEntrada(parametrosGeracaoNl, entidadeNl, eventos, infoBaseParametrizacao);
            entidadeNl.NormalEstorno = ((int)EnumNormalEstorno.Normal).ToString();


            entidadeNl.itensPertenceNl = itensNl.ToList();

            //entidade.AgrupamentoConfirmacao = parametrosGeracao.ConfirmacaoPagamento.CodigoAgrupamentoConfirmacaoPagamento; // geral



            return entidadeNl;
        }

        private bool AtribuiValoresQuandoNLPGOBRAS(Usuario usuario, ConfirmacaoPagamentoItem infoBaseItem, NlParametrizacao infoBaseParametrizacao, ReclassificacaoRetencao entidadeNl)
        {
            var NLPGOBRAS = false;
            if (VerrificarNlPgObras(infoBaseItem.NaturezaDespesa))
            {
                var infoNe = _commomService.ConsultaNe(infoBaseItem.NumeroEmpenhoSiafem, usuario);

                // Se sim quer dizer que temos uma NLPGOBRAS
                if (!string.IsNullOrEmpty(infoNe.IdentificadorObra))
                {
                    entidadeNl.ReclassificacaoRetencaoTipoId = EnumTipoReclassificacaoRetencao.PagamentoObrasSemOB.GetHashCode();
                    entidadeNl.CodigoEvento = infoBaseParametrizacao.Eventos.FirstOrDefault().NumeroEvento;
                    entidadeNl.CodigoFonte = infoBaseParametrizacao.Eventos.FirstOrDefault().NumeroFonte;
                    entidadeNl.CodigoClassificacao = infoBaseParametrizacao.Eventos.FirstOrDefault().NumeroClassificacao;

                    entidadeNl.MesMedicao = FormatarMesMedicao(infoBaseItem.Referencia);
                    entidadeNl.AnoMedicao = FormatarAnoMedicao(infoBaseItem.Referencia);
                    NLPGOBRAS = true;
                }
                else
                {
                    entidadeNl.ReclassificacaoRetencaoTipoId = EnumTipoReclassificacaoRetencao.NotaLancamento.GetHashCode();
                }
            }
            else
            {
                entidadeNl.ReclassificacaoRetencaoTipoId = EnumTipoReclassificacaoRetencao.NotaLancamento.GetHashCode();
            }

            return NLPGOBRAS;
        }

        private static void AtribuiValorCampoAnoExercicio(ConfirmacaoPagamentoItem infoBaseItem, ReclassificacaoRetencao entidadeNl)
        {
            var tipoDocumento = infoBaseItem.NumeroDocumentoGerador.Substring(0, 2);
            var isSubempenho = tipoDocumento.Equals(EnumTipoDocumento.Subempenho.GetHashCode().ToString("d2"));
            var isRap = tipoDocumento.Equals(EnumTipoDocumento.Rap.GetHashCode().ToString("d2"));


            string ano = "20";

            if (isSubempenho)
            {
                ano += infoBaseItem.NumeroDocumentoGerador.Substring(5, 2);
            }
            else if (isRap)
            {
                ano = infoBaseItem.NumeroDocumentoGerador.Substring(2, 2);
            }

            entidadeNl.AnoExercicio = int.Parse(ano);
        }

        private ReclassificacaoRetencao NlPorRepasseFinanceiro(List<ConfirmacaoPagamentoItem> itens, ReclassificacaoRetencao rr)
        {


            //Até aqui chegamos  com os itens separados por Tipo de Despesa e temos setados as propiedades comuns
            //fazer as somas para nota de lançamento com a propiedade (Itens) e salvar parametrosGeracao


            //if (parametrizacao != null)
            //{

            //    rr.Valor = itens.Sum(x => x.ValorDocumento) ?? 0;

            //    // TODO valor
            //    // TODO UG Favorecido já definido no começo?
            //    // TODO Gestão Favorecido já definido no começo?
            //    rr.DescricaoObservacao1 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 1);
            //    rr.DescricaoObservacao2 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 2);
            //    rr.DescricaoObservacao3 = SepararObservacao(parametrizacao.Observacao, TAMANHOOBS, 3);

            //}

            return rr;

        }

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
            public decimal valorAcumuladoNl { get; set; }

            public ParametrosGeracaoNl(ConfirmacaoPagamento confirmacaoPagamento, ConfirmacaoPagamentoItem itemConfirmacao, List<NlParametrizacao> parametrizacoes, List<ParaRestoAPagar> TiposRap)
            {
                this.ConfirmacaoPagamento = confirmacaoPagamento;
                this.ParametrizacoesNl = parametrizacoes;
                this.ItemConfirmacao = itemConfirmacao;
                this.TiposRap = TiposRap;
            }
        }

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

                if (primeiroDigito.Equals("3") || primeiroDigito.Equals("4") && (quintoSextoDigitos.Equals("39"))) // TODO remover hardcode
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

            string obs = "";
            if (inicio < tamanho)
            {
                var selectedText = observacaoInteira.Substring(inicio, tamanho);
                obs = string.IsNullOrWhiteSpace(selectedText) ? null : selectedText;
            }


            return obs;
        }

        private static void GerarEventoEntrada(ParametrosGeracaoNl parametrosGeracao, ReclassificacaoRetencao rr, List<ReclassificacaoRetencaoEvento> eventos, NlParametrizacao parametrizacao)
        {
            var eventosEntradaParametrizado = parametrizacao.Eventos.Where(x => x.EntradaSaidaDescricao.Trim().Contains(EnumDirecaoEvento.Entrada.ToString())).OrderBy(x => x.Id);
            if (eventosEntradaParametrizado != null)
            {
                var eventosFiltrados = RetornaEventosFiltrandoPeloTipoDocumento(parametrosGeracao.IsRap, eventosEntradaParametrizado);

                foreach (var eventoEntradaParametrizado in eventosFiltrados)
                {
                    rr.CodigoEvento = eventoEntradaParametrizado.NumeroEvento.ToString();
                    rr.CodigoClassificacao = eventoEntradaParametrizado.NumeroClassificacao.ToString();

                    var evento = new ReclassificacaoRetencaoEvento();
                    evento.NumeroEvento = eventoEntradaParametrizado.NumeroEvento.ToString();
                    evento.Classificacao = eventoEntradaParametrizado.NumeroClassificacao.ToString();
                    evento.Fonte = eventoEntradaParametrizado.NumeroFonte.ToString();
                    evento.ValorUnitario = Convert.ToInt32(parametrosGeracao.valorAcumuladoNl);
                    eventos.Add(evento);
                }

                rr.Eventos = eventos;
            }
        }

        private static void GerarEventosSaida(ParametrosGeracaoNl parametrosGeracao, List<ReclassificacaoRetencaoEvento> eventos, NlParametrizacao parametrizacao)
        {
            var eventosSaidaParametrizado = parametrizacao.Eventos.Where(x => x.EntradaSaidaDescricao.Trim().Contains(EnumDirecaoEvento.Saida.ToString())).OrderBy(x => x.Id);
            if (eventosSaidaParametrizado != null)
            {
                var eventosFiltrados = RetornaEventosFiltrandoPeloTipoDocumento(parametrosGeracao.IsRap, eventosSaidaParametrizado);

                foreach (var eventoSaidaParametrizado in eventosFiltrados)
                {
                    var evento = new ReclassificacaoRetencaoEvento();
                    evento.InscricaoEvento = String.Format("{0}{1}{2}", parametrosGeracao.ItemConfirmacao.NumeroBancoPagador, parametrosGeracao.ItemConfirmacao.NumeroAgenciaPagador, parametrosGeracao.ItemConfirmacao.NumeroContaPagador);
                    evento.Classificacao = eventoSaidaParametrizado.NumeroClassificacao.ToString();
                    evento.Fonte = eventoSaidaParametrizado.NumeroFonte.ToString();
                    evento.NumeroEvento = eventoSaidaParametrizado.NumeroEvento.ToString();
                    evento.ValorUnitario = Convert.ToInt32(parametrosGeracao.valorAcumuladoNl);

                    eventos.Add(evento);
                }



            }
        }
        private static IEnumerable<NlParametrizacaoEvento> RetornaEventosFiltrandoPeloTipoDocumento(bool IsRap, IOrderedEnumerable<NlParametrizacaoEvento> eventosSaidaParametrizado)
        {
            var eventos = eventosSaidaParametrizado.Where(x => x.IdDocumentoTipo == EnumTipoDocumento.Subempenho.GetHashCode());

            if (IsRap)
            {
                eventos = eventosSaidaParametrizado.Where(x => x.IdDocumentoTipo == EnumTipoDocumento.Rap.GetHashCode());
            }

            return eventos;
        }
    }


}
