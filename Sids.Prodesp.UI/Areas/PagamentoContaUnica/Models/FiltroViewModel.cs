using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class FiltroViewModel
    {
        #region Commom


        [Display(Name = "Tipo Documento")]
        public string DocumentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }

        [StringLength(20), MaxLength(20)]
        [Display(Name = "Nº Documento")]
        public string NumeroDocumento { get; set; }


        [StringLength(20), MaxLength(20)]
        [Display(Name = "Nº Docto. Gerador")]
        public string NumeroDocumentoGerador { get; set; }

        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        public IEnumerable<SelectListItem> StatusProdespListItems { get; set; }

        [Display(Name = "Data de Cadastro De")]
        public string DataCadastramentoDe { get; set; }

        [Display(Name = "Data de Cadastro Até")]
        public string DataCadastramentoAte { get; set; }

        [Display(Name = "Status Siafem")]
        public string StatusSiafem { get; set; }
        public IEnumerable<SelectListItem> StatusSiafemListItems { get; set; }

        [Display(Name = "Nº Contrato")]
        [StringLength(13), MaxLength(13)]
        public string NumeroContrato { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Cód. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Tipo de Pagamento")]
        public string IdTipoPagamento { get; set; }
        public IEnumerable<SelectListItem> DocumentoTipoPagamentoListItems { get; set; }

        #endregion

        #region Desdobramento Proprieties
        [Display(Name = "Tipo de Desdobramento")]
        public string DesdobramentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DesdobramentoTipoListItems { get; set; }

        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }
        public IEnumerable<SelectListItem> StatusCanceladoListItems { get; set; }
        #endregion

        #region Reclassificação properties

        [Display(Name = "Nº Reclassificação/Ret. SIAFEM")]
        public string NumeroSiafem { get; set; }

        [Display(Name = "Nº Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Nº do Empenho SIAFEM/SIAFISICO")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

        [Display(Name = "Tipo de Reclassificação / Retenção")]
        public string ReclassificacaoRetencaoTipo { get; set; }
        public IEnumerable<SelectListItem> ReclassificacaoRetencaoTipoListItems { get; set; }

        [Display(Name = "Normal ou Estorno?")]
        public string NormalEstorno { get; set; }
        public IEnumerable<SelectListItem> NormalEstornoListItems { get; set; }


        #region Confirmação de Pagamento
        [Display(Name = "Origem da Reclassificação/Retenção")]
        public OrigemReclassificacaoRetencao? OrigemReclassificacaoRetencao { get; set; }

        [Display(Name = "Agrupamento da Confirmação")]
        public int? AgrupamentoConfirmacao { get; set; }
        #endregion

        #endregion

        #region Lista de Boletos Proprieties

        [Display(Name = "Nº Lista de Boletos")]
        public string NumeroSiafemListaBoleto { get; set; }


        [Display(Name = "Data da Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "Nome da Lista")]
        public string NomeLista { get; set; }

        [Display(Name = "CNPJ do Favorecido")]
        public string NumeroCnpjcpfFavorecido { get; set; }

        [Display(Name = "Tipo de Boleto")]
        public string TipoDeBoletoId { get; set; }
        public IEnumerable<SelectListItem> TipoBoletoListItems { get; set; }

        [Display(Name = "Digite o Código de Barras ou utilize o Leitor Óptico")]
        public string NumeroDoCodigoDebarras { get; set; }

        #region Codigo de barras
        public string NumeroBoleto1 { get; set; }
        public string NumeroBoleto2 { get; set; }
        public string NumeroBoleto3 { get; set; }
        public string NumeroBoleto4 { get; set; }
        public string NumeroBoleto5 { get; set; }
        public string NumeroBoleto6 { get; set; }
        public string NumeroBoleto7 { get; set; }
        public string NumeroDigito { get; set; }
        public string NumeroTaxa1 { get; set; }
        public string NumeroTaxa2 { get; set; }
        public string NumeroTaxa3 { get; set; }
        public string NumeroTaxa4 { get; set; }
        #endregion

        #endregion

        #region PreparacaoPagamento

        [Display(Name = "Nº da OP")]
        public string NumeroOp { get; set; }

        [Display(Name = "Tipo de Preparação de Pagamento")]
        public string PreparacaoPagamentoTipoId { get; set; }

        public IEnumerable<SelectListItem> PreparacaoPagamentoTipoListItems { get; set; }

        #endregion

        #region Programação de Desembolso
        [Display(Name = "Nº da Programação de Desembolso")]
        public string NumSiafemProgDesembolso { get; set; }

        [Display(Name = "Órgão")]
        public string RegionalId { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }

        [Display(Name = "Data Vencimento")]
        public string DataVencimento { get; set; }

        //TODO: nao esta na model verificar com Luis
        [Display(Name = "Tipo de Despesa")]
        public string TipoDespesa { get; set; }

        //documentotipo       // common
        //numeroDocumento     // common
        //codigoAplicacaoObra // common
        //NumeroContrato      // common
        //StatusSiafem        // common

        [Display(Name = "Nº do Agrupamento")]
        public string NumeroAgrupamento { get; set; }

        [Display(Name = "Tipo de Programação de Desembolso")]
        public string ProgDesembolsoTipoId { get; set; }
        public IEnumerable<SelectListItem> ProgDesembolsoTipoListItems { get; set; }

        [Display(Name = "Bloqueado/Desbloqueado")]
        public string Bloqueio { get; set; }
        public IEnumerable<SelectListItem> BloqueioListItem { get; set; }


        public bool? Bloquear { get; set; }

        #endregion

        #region Execução de PD
        [Display(Name = "Nº da PD")]
        public string NumeroPD { get; set; }

        [Display(Name = "Nº da OB")]
        public string NumeroOB { get; set; }

        [Display(Name = "OB Cancelada")]
        public bool? OBCancelada { get; set; }

        [Display(Name = "Tipo de Execução")]
        public int? TipoExecucao { get; set; }
        public IEnumerable<SelectListItem> TipoExecucaoItems { get; set; }

        [Display(Name = "Favorecido")]
        [StringLength(14)]
        public string Favorecido { get; set; }

        [Display(Name = "Cadastro de:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CadastradoEmDe { get; set; }

        [Display(Name = "Cadastro até:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CadastradoEmAte { get; set; }

        //Autorizacão de OB
        public string Despesa { get; set; }

        public string Valor { get; set; }
        //Autorizacão de OB

        #endregion Execução de PD

        #region Impressão Relação RE RT

        [Display(Name = "Nº RE")]
        public string NumeroRE { get; set; }

        [Display(Name = "Nº RT")]
        public string NumeroRT { get; set; }

        [Display(Name = "Banco")]
        public string NumeroBanco { get; set; }

        [Display(Name = "Nº do Agrupamento")]
        public int? NumeroAgrupamentoImpressao { get; set; }

        [Display(Name = "Data da Solicitação")]
        public string DataSolicitacao { get; set; }

        #endregion Impressão Relação RE RT

        public FiltroViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento objModel, IEnumerable<DesdobramentoTipo> tipoDesdobramento, IEnumerable<DocumentoTipo> documento, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();

            filtro.DesdobramentoTipoId = Convert.ToString(tipoDesdobramento?.FirstOrDefault(x => x.Id == objModel.DesdobramentoTipoId));

            filtro.DesdobramentoTipoListItems = tipoDesdobramento?.Where(x => x.Id <= 2).
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DesdobramentoTipoId
                });


            filtro.DocumentoTipoId = Convert.ToString(documento?.FirstOrDefault(x => x.Id == objModel.DesdobramentoTipoId));

            filtro.DocumentoTipoListItems = documento?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DocumentoTipoId
                });


            filtro.StatusProdesp = Convert.ToString(objModel.TransmitidoProdesp && objModel.DataTransmitidoProdesp > default(DateTime)).ToLower();
            filtro.StatusProdespListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };


            filtro.Cancelado = Convert.ToString(objModel.SituacaoDesdobramento == "S").ToLower();
            filtro.StatusCanceladoListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Cancelado", Value = "S" },
                    new SelectListItem { Text = "Ativo", Value = "N" }
                };

            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;

            return filtro;
        }

        public FiltroViewModel CreateInstance(ReclassificacaoRetencao objModel, IEnumerable<ReclassificacaoRetencaoTipo> tipoReclassificacao, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();

            filtro.NumeroSiafem = objModel.NumeroSiafem;
            filtro.NumeroProcesso = objModel.NumeroProcesso;
            filtro.CodigoAplicacaoObra = objModel.CodigoAplicacaoObra;
            filtro.NumeroOriginalSiafemSiafisico = objModel.NumeroOriginalSiafemSiafisico;


            filtro.ReclassificacaoRetencaoTipoListItems = tipoReclassificacao
                .Select(x => new SelectListItem
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString(),
                    Selected = x.Id == objModel.ReclassificacaoRetencaoTipoId
                });
            filtro.ReclassificacaoRetencaoTipo = objModel.ReclassificacaoRetencaoTipoId.ToString();


            filtro.NormalEstornoListItems = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Normal", Value = "1", Selected = objModel.NormalEstorno == "1"},
                    new SelectListItem {Text = "Estorno", Value = "2", Selected = objModel.NormalEstorno == "2"}
                };
            filtro.NormalEstorno = objModel.NormalEstorno;


            filtro.StatusSiafem = objModel.StatusSiafem;
            filtro.StatusSiafemListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
            };


            filtro.NumeroContrato = objModel.NumeroContrato;
            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;

            filtro.OrigemReclassificacaoRetencao = objModel.Origem;
            filtro.AgrupamentoConfirmacao = objModel.AgrupamentoConfirmacao;

            return filtro;
        }

        public FiltroViewModel CreateInstance(ListaBoletos objModel, IEnumerable<TipoBoleto> tipoBoleto, IEnumerable<DocumentoTipo> documento, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();
            filtro.NumeroDoCodigoDebarras = objModel.CodigoDeBarras;
            filtro.NumeroSiafemListaBoleto = objModel.NumeroSiafem;
            filtro.CodigoUnidadeGestora = objModel.CodigoUnidadeGestora;
            filtro.CodigoGestao = objModel.CodigoGestao;
            //filtro.DataEmissao = objModel.DataEmissao;
            filtro.NomeLista = objModel.NomeLista;
            filtro.NumeroCnpjcpfFavorecido = objModel.NumeroCnpjcpfFavorecido;
            filtro.NumeroDocumento = objModel.NumeroDocumento;

            filtro.DocumentoTipoId = Convert.ToString(documento?.FirstOrDefault(x => x.Id == objModel.DocumentoTipoId));
            filtro.DocumentoTipoListItems = documento?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DocumentoTipoId
                });

            filtro.StatusSiafem = objModel.StatusSiafem;
            filtro.StatusSiafemListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
            };

            filtro.TipoDeBoletoId = Convert.ToString(tipoBoleto?.FirstOrDefault(x => x.Id == objModel.TipoBoletoId));
            
            filtro.TipoBoletoListItems = tipoBoleto?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.TipoBoletoId
                });

            filtro.NumeroBoleto1 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(0, 5) : "";
            filtro.NumeroBoleto2 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(5, 5) : "";
            filtro.NumeroBoleto3 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(10, 5) : "";
            filtro.NumeroBoleto4 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(15, 6) : "";
            filtro.NumeroBoleto5 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(21, 5) : "";
            filtro.NumeroBoleto6 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(26, 6) : "";
            filtro.NumeroDigito = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(32, 1) : "";
            filtro.NumeroBoleto6 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(33, 14) : "";

            filtro.NumeroTaxa1 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(0, 12) : "";
            filtro.NumeroTaxa2 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(12, 12) : "";
            filtro.NumeroTaxa3 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(24, 12) : "";
            filtro.NumeroTaxa4 = objModel.CodigoDeBarras != null ? objModel.CodigoDeBarras.Substring(36, 12) : "";

            return filtro;
        }

        public FiltroViewModel CreateInstance(PreparacaoPagamento objModel, IEnumerable<PreparacaoPagamentoTipo> preparacaoPagamentoTipos, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();


            filtro.StatusProdesp = Convert.ToString(objModel.TransmitidoProdesp && objModel.DataTransmitidoProdesp > default(DateTime)).ToLower();
            filtro.StatusProdespListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.PreparacaoPagamentoTipoListItems = preparacaoPagamentoTipos?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.PreparacaoPagamentoTipoId
                });

            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;


            return filtro;
        }

        public FiltroViewModel CreateInstance(PDExecucao objModel, IEnumerable<PreparacaoPagamentoTipo> preparacaoPagamentoTipos, DateTime de, DateTime ate) {
            var filtro = new FiltroViewModel();
            return filtro;
        }

        public FiltroViewModel CreateInstance(ProgramacaoDesembolso objModel, IEnumerable<ProgramacaoDesembolsoTipo> programacaoDesembolsoTipos, IEnumerable<DocumentoTipo> documento, IEnumerable<Regional> regional, DateTime de, DateTime ate)
        {
            return new FiltroViewModel()
            {
                StatusSiafem = Convert.ToString(objModel.StatusSiafem == "S").ToLower(),
                StatusSiafemListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },
                Bloqueio = Convert.ToString(objModel.Bloqueio == true).ToLower(),
                BloqueioListItem = new SelectListItem[] {
                    new SelectListItem { Text = "Bloqueado", Value = "1" },
                    new SelectListItem { Text = "Desbloqueado", Value = "0" }
                },

                DataVencimento = null,
                DataCadastramentoDe = null,
                DataCadastramentoAte = null,

                DocumentoTipoId = Convert.ToString(documento?.FirstOrDefault(x => x.Id == objModel.DocumentoTipoId)),

                DocumentoTipoListItems = documento?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DocumentoTipoId
                }),


                ProgDesembolsoTipoId = Convert.ToString(programacaoDesembolsoTipos?.FirstOrDefault(x => x.Id == objModel.ProgramacaoDesembolsoTipoId)),
                ProgDesembolsoTipoListItems = programacaoDesembolsoTipos?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.ProgramacaoDesembolsoTipoId
                }),


                RegionalId = objModel.RegionalId > 0 ? objModel.RegionalId.ToString() : default(string),
                RegionalListItems = regional.Where(x => x.Id > 1).ToList()
              .Select(s => new SelectListItem
              {
                  Text = s.Descricao,
                  Value = s.Id.ToString(),
                  Selected = s.Id == objModel.RegionalId
              }),


                NumeroAgrupamento = objModel.NumeroAgrupamento.ZeroParaNulo(),
                CodigoAplicacaoObra = objModel.CodigoAplicacaoObra,
                NumeroContrato = objModel.NumeroContrato,
                NumSiafemProgDesembolso = objModel.NumeroSiafem,

            };
        }

        public FiltroViewModel CreateInstance(PDExecucaoItem objModel, int? tipoExecucao, IEnumerable<PDExecucaoTipoExecucao> tipos, DateTime de, DateTime ate)
        {
            return new FiltroViewModel()
            {
                StatusSiafem = Convert.ToString(objModel.cd_transmissao_status_siafem == "S").ToLower(),
                StatusSiafemListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },
                TipoExecucao = tipoExecucao,
                OBCancelada = objModel.OBCancelada,
                TipoExecucaoItems = tipos.Select(x=> new SelectListItem() { Value = x.Codigo.ToString(), Text=x.Descricao, Selected=( x.Codigo == TipoExecucao)  }).ToList(),

                StatusProdesp = Convert.ToString(objModel.cd_transmissao_status_prodesp == "S").ToLower(),
                StatusProdespListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                }


                //Bloqueio = Convert.ToString(objModel.Bloqueio == true).ToLower(),
                //BloqueioListItem = new SelectListItem[] {
                //    new SelectListItem { Text = "Bloqueado", Value = "1" },
                //    new SelectListItem { Text = "Desbloqueado", Value = "0" }
                //},

                //  DataVencimento = null,
                //  DataCadastramentoDe = null,
                //  DataCadastramentoAte = null,

                //  DocumentoTipoId = Convert.ToString(documento?.FirstOrDefault(x => x.Id == objModel.DocumentoTipoId)),

                //  DocumentoTipoListItems = documento?.
                //  Select(s => new SelectListItem
                //  {
                //      Text = s.Descricao,
                //      Value = s.Id.ToString(),
                //      Selected = s.Id == objModel.DocumentoTipoId
                //  }),


                //  ProgDesembolsoTipoId = Convert.ToString(programacaoDesembolsoTipos?.FirstOrDefault(x => x.Id == objModel.ProgramacaoDesembolsoTipoId)),
                //  ProgDesembolsoTipoListItems = programacaoDesembolsoTipos?.
                //  Select(s => new SelectListItem
                //  {
                //      Text = s.Descricao,
                //      Value = s.Id.ToString(),
                //      Selected = s.Id == objModel.ProgramacaoDesembolsoTipoId
                //  }),


                //  RegionalId = objModel.RegionalId > 0 ? objModel.RegionalId.ToString() : default(string),
                //  RegionalListItems = regional.Where(x => x.Id > 1).ToList()
                //.Select(s => new SelectListItem
                //{
                //    Text = s.Descricao,
                //    Value = s.Id.ToString(),
                //    Selected = s.Id == objModel.RegionalId
                //}),


                //  NumeroAgrupamento = objModel.NumeroAgrupamento.ZeroParaNulo(),
                //  CodigoAplicacaoObra = objModel.CodigoAplicacaoObra,
                //  NumeroContrato = objModel.NumeroContrato,
                //  NumSiafemProgDesembolso = objModel.NumeroSiafem,

            };
        }

        public FiltroViewModel CreateInstance(OBAutorizacao objModel, DateTime de, DateTime ate)
        {
            return new FiltroViewModel()
            {
                StatusSiafem = Convert.ToString(objModel.TransmissaoStatusSiafem == "S").ToLower(),
                StatusSiafemListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },
            };
        }

        public FiltroViewModel CreateInstance(OBAutorizacaoItem objModel, DateTime de, DateTime ate)
        {
            return new FiltroViewModel()
            {
                StatusSiafem = Convert.ToString(objModel.TransmissaoItemStatusSiafem == "S").ToLower(),
                StatusSiafemListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },

                StatusProdesp = Convert.ToString(objModel.TransmissaoItemStatusProdesp == "S" && objModel.TransmissaoItemDataProdesp > default(DateTime)).ToLower(),
                StatusProdespListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },

                //TipoExecucao = tipoExecucao,
                OBCancelada = objModel.OBCancelada,
                Despesa = objModel.CodigoDespesa,
                //Valor = objModel.ValorItem.ToString()


            };
        }

        public FiltroViewModel CreateInstance(
        Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT.ImpressaoRelacaoRERT objModel,  DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();

            // Bloco 1
            filtro.NumeroRE = objModel.CodigoRelacaoRERT?.Substring(4, 2) == "RE" ? objModel.CodigoRelacaoRERT : null;
            filtro.NumeroRT = objModel.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? objModel.CodigoRelacaoRERT : null;
            filtro.NumeroOB = objModel.CodigoOB;
            filtro.StatusSiafem = !string.IsNullOrEmpty(objModel.StatusSiafem) ? objModel.StatusSiafem : null;
            filtro.StatusSiafemListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;

            // Bloco 2
            filtro.CodigoUnidadeGestora = objModel.CodigoUnidadeGestora;
            filtro.CodigoGestao = objModel.CodigoGestao;
            filtro.NumeroBanco = objModel.CodigoBanco;
            filtro.DataSolicitacao = null;
            filtro.NumeroAgrupamentoImpressao = objModel.NumeroAgrupamento == 0 ? null : objModel.NumeroAgrupamento;
            filtro.Cancelado = Convert.ToString(objModel.FlagCancelamentoRERT) != null ? Convert.ToString(objModel.FlagCancelamentoRERT) : null;
            filtro.StatusCanceladoListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Cancelado", Value = "S" },
                    new SelectListItem { Text = "Ativo", Value = "N" }
                };

            return filtro;
        }
    }
}
