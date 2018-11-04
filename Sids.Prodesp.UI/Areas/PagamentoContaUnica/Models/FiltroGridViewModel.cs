using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{

    public class FiltroGridViewModel
    {

        #region gridDesdobramento properties

        public string Id { get; set; }

        [Display(Name = "Tipo Desdobramento")]
        public string DesdobramentoTipo { get; set; }

        [Display(Name = "Tipo Documento")]
        public string DocumentoTipo { get; set; }

        [Display(Name = "Total ISSQN")]
        public decimal ValorIssqn { get; set; }

        [Display(Name = "Total IR")]
        public decimal ValorIr { get; set; }

        [Display(Name = "Total INSS")]
        public decimal ValorInss { get; set; }

        [Display(Name = "Valor Documento")]
        public decimal ValorDocumento { get; set; }


        #endregion

        #region gridReclassificacão properties



        [Display(Name = "Nº Reclas. / Ret. SIAFEM")]
        public string NumeroSiafem { get; set; }

        [Display(Name = "Tipo de Reclassificação / Retenção")]
        public string ReclassificacaoRetencaoTipo { get; set; }

        [Display(Name = "Normal ou Estorno?")]
        public string NormalEstorno { get; set; }

        #region Confirmação de Pagamento
        [Display(Name = "Origem da Reclassificação/Retenção")]
        public string OrigemReclassificacaoRetencao { get; set; }

        [Display(Name = "Agrupamento da Confirmação")]
        public string AgrupamentoConfirmacao { get; set; }
        #endregion


        #endregion

        #region gridListaDeBoletos proprieties

        [Display(Name = "Nº Lista de Boletos")]
        public string NumeroSiafemListaBoleto { get; set; }

        [Display(Name = "CNPJ do Favorecido")]
        public string NumeroCnpjcpfFavorecido { get; set; }

        [Display(Name = "Total de Credores")]
        public int TotalCredores { get; set; }

        [Display(Name = "Total da Lista/Anexo")]
        public decimal ValorTotalLista { get; set; }


        #endregion

        #region grid Preparação de pagamento proprieties

        [Display(Name = "Nº da Ordem de Pagamento Inicial")]
        public string NumeroOpInicial { get; set; }

        [Display(Name = "Nº da Ordem de Pagamento Final")]
        public string NumeroOpFinal { get; set; }

        [Display(Name = "Tipo de Preparação de Pagamento")]
        public string PreparacaoPagamentoTipo { get; set; }


        #endregion

        #region Grid Programação Desembolso

        [Display(Name = "Nº Agrupamento")]
        public string NumeroAgrupamento { get; set; }

        [Display(Name = "Nº da Programação de Desembolso")]
        public string NumSiafemProgDesembolso { get; set; }

        [Display(Name = "Tipo Despesa")]
        public string TipoDespesa { get; set; }

        [Display(Name = "Data de Vencimento")]
        public string DataVencimento { get; set; }

        public string DataConfirmacao { get; set; }

        [Display(Name = "Bloqueado / Desbloqueado")]
        public string StatusOp { get; set; }

        public bool Bloqueio { get; set; }
        public bool CanceladoOp { get; set; }

        public int ProgramacaoDesembolsoTipoId { get; set; }

        public string NumeroDocumentoGerador { get; set; }

        public int NumeroContrato { get; set; }

        #endregion

        #region comuns properties

        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }

        [Display(Name = "Data Cadastro")]
        public string Data { get; set; }

        [Display(Name = "Status Trans. Siafem")]
        public string StatusSiafem { get; set; }

        [Display(Name = "Status Transmissão Prodesp")]
        public string StatusProdesp { get; set; }
        public bool TransmitidoSiafem { get; set; }
        public bool TransmitirSiafem { get; set; }
        public string MensagemSiafem { get; set; }  //toolTip

        public bool TransmitirProdesp { get; set; }
        public string MensagemProdesp { get; set; }

        public bool CadastroCompleto { get; set; }

        public bool TransmitidoProdesp { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "N° Documento")]
        public string NumeroDocumento { get; set; }

        #endregion

        #region ExecucaoPD
        [Display(Name = "Id")]
        public int? Codigo { get; set; }

        [Display(Name = "N° PD")]
        public string NumeroPD { get; set; }

        [Display(Name = "N° OB")]
        public string NumeroOB { get; set; }

        public string NumeroOP { get; set; }

        [Display(Name = "UG Liquidante")]
        public string UGLiquidante { get; set; }

        [Display(Name = "Gestão Liquidante")]
        public string GestaoLiquidante { get; set; }

        [Display(Name = "UG Pagadora")]
        public string UGPagadora { get; set; }

        [Display(Name = "UG")]
        public string UnidadeGestora { get; set; }

        [Display(Name = "Gestão Pagadora")]
        public string GestaoPagadora { get; set; }

        [Display(Name = "Gestão")]
        public string Gestao { get; set; }

        [Display(Name = "Favorecido")]
        public string Favorecido { get; set; }

        [Display(Name = "Favorecido")]
        public string FavorecidoDesc { get; set; }

        public string FavorecidoDescSimplificado
        {
            get
            {
                try
                {
                    return this.FavorecidoDesc.Substring(0, 14) + "...";
                }
                catch
                {
                    return default(string);
                }
            }

            set
            {
                this.FavorecidoDesc = FavorecidoDesc.Substring(0, 14) + "...";
            }
        }

        [Display(Name = "Valor")]
        public string Valor { get; set; }

        [Display(Name = "Agrupamento")]
        public string Agrupamento { get; set; }

        public string NumeroAgrupamentoPD { get; set; }

        [Display(Name = "Prioridade")]
        public string Prioridade { get; set; }

        public string StatusSiafemLabel { get; set; }

        #endregion ExecucaoPD

        #region AutorizacaoDeOB

        [Display(Name = "AgrupamentoOB")]
        public string AgrupamentoOB { get; set; }

        public string NumeroAgrupamentoOB { get; set; }

        [Display(Name = "IdAutorizacaoOB")]
        public string IdAutorizacaoOB { get; set; }

        public string IdAutorizacaoOBItem { get; set; }

        [Display(Name = "Despesa")]
        public string Despesa { get; set; }

        public decimal? ValorTotal { get; set; }

        public string ValorItem { get; set; }

        #endregion

        #region Impressão Relação RE RT

        [Display(Name = "Nº RE")]
        public string NumeroRE { get; set; }

        [Display(Name = "Nº RT")]
        public string NumeroRT { get; set; }

        [Display(Name = "Banco")]
        public string NumeroBanco { get; set; }

        [Display(Name = "Data da Solicitação")]
        public DateTime DataSolicitacao { get; set; }

        public string FlagTransmitidoSiafem { get; set; }

        #endregion Impressão Relação RE RT

        public FiltroGridViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento entity, IEnumerable<DesdobramentoTipo> desdobramentoTipos, IEnumerable<DocumentoTipo> documentoTipos)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                DesdobramentoTipo = desdobramentoTipos.FirstOrDefault(w => w.Id == entity.DesdobramentoTipoId)?.Descricao,
                DocumentoTipo = documentoTipos.FirstOrDefault(w => w.Id == entity.DocumentoTipoId)?.Descricao,
                Data = entity.DataCadastro.ToShortDateString(),
                ValorInss = Convert.ToDecimal(entity.ValorInss),
                ValorIssqn = Convert.ToDecimal(entity.ValorIssqn),
                ValorIr = Convert.ToDecimal(entity.ValorIr),
                ValorDocumento = Convert.ToDecimal(entity.ValorDistribuido),
                StatusProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso",
                MensagemProdesp = entity.MensagemServicoProdesp,
                TransmitidoProdesp = entity.TransmitidoProdesp,
                CadastroCompleto = entity.CadastroCompleto,
                Cancelado = entity.SituacaoDesdobramento == "S" ? "Sim" : "Não",
                NumeroDocumento = entity.NumeroDocumento
            };
        }

        public FiltroGridViewModel CreateInstance(ReclassificacaoRetencao entity, IEnumerable<ReclassificacaoRetencaoTipo> reclassificacaoRetencaoTps, IEnumerable<ReclassificacaoRetencaoEvento> recRetEventos)
        {
            var obj = new FiltroGridViewModel();

            obj.Id = Convert.ToString(entity.Id);
            obj.NumeroSiafem = entity.NumeroSiafem;
            obj.ReclassificacaoRetencaoTipo = reclassificacaoRetencaoTps.FirstOrDefault(w => w.Id == entity.ReclassificacaoRetencaoTipoId)?.Descricao;
            obj.NormalEstorno = string.IsNullOrWhiteSpace(entity.NormalEstorno) ? string.Empty : (entity.NormalEstorno.Equals("1") ? "Normal" : "Estorno");
            obj.Total = entity.ReclassificacaoRetencaoTipoId == 2 ? Convert.ToDecimal(entity.Eventos.Sum(x => x.ValorUnitario)) / 100 : Convert.ToDecimal(entity.Valor) / 100;
            obj.Data = entity.DataCadastro.ToShortDateString();
            obj.StatusSiafem = string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";
            obj.TransmitidoSiafem = entity.TransmitidoSiafem;
            obj.TransmitirSiafem = entity.TransmitirSiafem;
            obj.MensagemSiafem = entity.MensagemServicoSiafem;
            obj.CadastroCompleto = entity.CadastroCompleto;
            obj.OrigemReclassificacaoRetencao = entity.Origem.GetEnumDescription();
            obj.AgrupamentoConfirmacao = entity.AgrupamentoConfirmacao?.ToString();

            return obj;
        }

        public FiltroGridViewModel CreateInstance(ListaBoletos entity)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                NumeroSiafemListaBoleto = entity.NumeroSiafem,
                NumeroCnpjcpfFavorecido = entity.NumeroCnpjcpfFavorecido,
                TotalCredores = entity.TotalCredores,
                ValorTotalLista = entity.ValorTotalLista,
                Data = entity.DataCadastro.ToShortDateString(),
                StatusSiafem = string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso",
                MensagemSiafem = entity.MensagemServicoSiafem,
                CadastroCompleto = entity.CadastroCompleto,
                TransmitirSiafem = entity.TransmitirSiafem,
                TransmitidoSiafem = entity.TransmitidoSiafem,
            };
        }

        public FiltroGridViewModel CreateInstance(PreparacaoPagamento entity)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                NumeroOpInicial = entity.NumeroOpInicial,
                NumeroOpFinal = entity.NumeroOpFinal,
                PreparacaoPagamentoTipo = entity.PreparacaoPagamentoTipo != null ? entity.PreparacaoPagamentoTipo.Descricao : null,
                Total = entity.ValorTotal,
                Data = entity.DataCadastro.ToShortDateString(),
                StatusProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : (entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso"),
                MensagemProdesp = entity.MensagemServicoProdesp,
                CadastroCompleto = entity.CadastroCompleto,
                TransmitirProdesp = entity.TransmitirProdesp,
                TransmitidoProdesp = entity.TransmitidoProdesp,
            };
        }

        public FiltroGridViewModel CreateInstance(PDExecucaoItem entity)
        {

            FiltroGridViewModel filtro = new Models.FiltroGridViewModel();
            filtro.Id = entity.Codigo.ToString();
            filtro.Agrupamento = entity.id_execucao_pd.ToString();
            filtro.NumeroAgrupamentoPD = entity.AgrupamentoItemPD.ToString();
            filtro.NumeroPD = entity.NumPD;
            //filtro.NumeroOB = entity.NumOB;
            filtro.NumeroOB = entity.NumOBItem;
            //filtro.NumeroOP = string.IsNullOrEmpty(entity.NumOP) ? string.Empty :  (entity.NumOP.Length > 6) ? entity.NumOP.Substring(5,6) : entity.NumOP;
            filtro.NumeroOP = string.IsNullOrEmpty(entity.NumOP) ? string.Empty : entity.NumOP;
            filtro.NumeroDocumentoGerador = entity.NumeroDocumentoGerador;
            filtro.Favorecido = entity.Favorecido;
            filtro.FavorecidoDesc = entity.FavorecidoDesc;
            filtro.Valor = entity.Valor;
            filtro.DataVencimento = entity.DataVencimento == default(DateTime) ? default(string) : Convert.ToString(entity.DataVencimento.ToShortDateString());
            filtro.DataConfirmacao = entity.Dt_confirmacao == default(DateTime) ? default(string) : Convert.ToString(entity.Dt_confirmacao?.ToShortDateString());
            filtro.Cancelado = entity.OBCancelada == true ? "Sim" : "Não";

            filtro.StatusSiafem = entity.cd_transmissao_status_siafem;
            filtro.MensagemSiafem = entity.ds_transmissao_mensagem_siafem;

            filtro.StatusProdesp = entity.cd_transmissao_status_prodesp;
            filtro.MensagemProdesp = entity.ds_transmissao_mensagem_prodesp;

            filtro.UGLiquidante = entity.UGLiquidante;
            filtro.GestaoLiquidante = entity.GestaoLiquidante;
            filtro.UGPagadora = entity.UGPagadora;
            filtro.GestaoPagadora = entity.GestaoPagadora;
            filtro.Prioridade = entity.NouP;
            return filtro;
        }

        public FiltroGridViewModel CreateInstance(OBAutorizacao entity)
        {

            FiltroGridViewModel filtro = new Models.FiltroGridViewModel();
            filtro.Id = entity.IdAutorizacaoOB.ToString();
            filtro.ValorTotal = entity.Valor;

            filtro.StatusSiafem = entity.TransmissaoStatusSiafem;
            filtro.MensagemSiafem = entity.TransmissaoMensagemSiafem;
            filtro.StatusProdesp = entity.StatusProdesp;
            filtro.MensagemProdesp = entity.MensagemServicoProdesp;

            filtro.GestaoLiquidante = entity.GestaoLiquidante;
            filtro.UnidadeGestora = entity.UnidadeGestora;
            filtro.Gestao = entity.Gestao;
            filtro.UGPagadora = entity.UgPagadora;
            filtro.GestaoPagadora = entity.GestaoPagadora;
            filtro.Data = entity.DataCadastro.ToShortDateString();
            return filtro;
        }

        public FiltroGridViewModel CreateInstance(OBAutorizacaoItem entity)
        {

            FiltroGridViewModel filtro = new Models.FiltroGridViewModel();
            filtro.Id = entity.Codigo.ToString();
            filtro.IdAutorizacaoOBItem = entity.IdAutorizacaoOBItem.ToString();
            filtro.IdAutorizacaoOB = entity.IdAutorizacaoOB.ToString();

            filtro.AgrupamentoOB = entity.IdAutorizacaoOB.ToString();
            filtro.NumeroAgrupamentoOB = entity.AgrupamentoItemOB.ToString();

            filtro.NumeroPD = entity.NumPD;
            filtro.NumeroOB = entity.NumOB;
            filtro.UGLiquidante = entity.UGLiquidante;
            filtro.GestaoLiquidante = entity.GestaoLiquidante;
            filtro.UGPagadora = entity.UGPagadora;
            filtro.GestaoPagadora = entity.GestaoPagadora;
            filtro.Favorecido = entity.Favorecido;
            filtro.FavorecidoDesc = entity.FavorecidoDesc;
            filtro.ValorTotal = entity.ValorTotal;
            filtro.ValorItem = entity.ValorItem;
            filtro.Data = entity.DataCadastro == default(DateTime) ? default(string) : Convert.ToString(entity.DataCadastro.ToShortDateString());

            //filtro.DataConfirmacao = entity.Dt_confirmacao == default(DateTime) ? default(string) : Convert.ToString(entity.Dt_confirmacao?.ToShortDateString());

            filtro.Despesa = entity.CodigoDespesa;
            filtro.Prioridade = entity.NouP;

            filtro.StatusSiafem = entity.TransmissaoItemStatusSiafem;
            filtro.MensagemSiafem = entity.TransmissaoItemMensagemSiafem;

            filtro.StatusProdesp = entity.TransmissaoItemStatusProdesp;
            filtro.MensagemProdesp = entity.TransmissaoItemMensagemProdesp;

            return filtro;
        }

        public IEnumerable<FiltroGridViewModel> CreateInstance(ProgramacaoDesembolso entity, IEnumerable<DocumentoTipo> tpdocumentos)
        {
            IEnumerable<FiltroGridViewModel> result;

            //result = entity.ProgramacaoDesembolsoTipoId == 2 ? GetFiltroGridViewModelAgrupamento(entity, tpdocumentos) : GetFiltroGridViewModel(entity, tpdocumentos);

            if (entity.ProgramacaoDesembolsoTipoId == 2)
            {
                result = entity.Agrupamentos.Any() ? GetFiltroGridViewModelAgrupamento(entity, tpdocumentos) : GetFiltroGridViewModel(entity, tpdocumentos);
            }
            else
            {
                result = GetFiltroGridViewModel(entity, tpdocumentos);
            }




            return result;
        }


        private static List<FiltroGridViewModel> GetFiltroGridViewModel(ProgramacaoDesembolso entity, IEnumerable<DocumentoTipo> tpdocumentos)
        {
            var lista = new List<FiltroGridViewModel>();

            var obj = new FiltroGridViewModel();

            obj.Id = entity.Id.ToString();
            obj.NumeroAgrupamento = entity.NumeroAgrupamento == default(int) ? string.Empty : entity.NumeroAgrupamento.ToString("D5");
            obj.NumSiafemProgDesembolso = entity.NumeroSiafem;
            obj.AgrupamentoId = entity.Id;
            obj.TipoDespesa = entity.CodigoDespesa;
            obj.DocumentoTipo = tpdocumentos.FirstOrDefault(w => w.Id == entity.DocumentoTipoId)?.Descricao;
            obj.NumeroDocumento = entity.NumeroDocumento;
            obj.Total = entity.ProgramacaoDesembolsoTipoId == 2 ? entity.Valor : entity.Eventos.Sum(x => x.ValorUnitario);
            obj.DataVencimento = (entity.DataVencimento == DateTime.MinValue) ? string.Empty : entity.DataVencimento.ToShortDateString();
            obj.Data = entity.DataCadastro.ToShortDateString();
            obj.StatusSiafem = string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : (entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso");
            obj.Bloqueio = entity.Bloqueio;
            obj.MensagemSiafem = entity.MensagemServicoSiafem;
            obj.CadastroCompleto = entity.CadastroCompleto;
            obj.TransmitirSiafem = entity.TransmitirSiafem;
            obj.TransmitidoSiafem = entity.TransmitidoSiafem;
            obj.StatusOp = string.Format("{0}{1}{2}", (entity.Cancelado ? "Cancel" : ""), (entity.Cancelado && entity.Bloqueio ? "/" : ""), (entity.Bloqueio ? "Bloq" : ""));
            obj.CanceladoOp = entity.Cancelado;
            obj.ProgramacaoDesembolsoTipoId = entity.ProgramacaoDesembolsoTipoId;
            obj.DocumentoTipoId = entity.DocumentoTipoId;
            obj.NumeroDocumentoGerador = entity.NumeroDocumentoGerador;
            var nrContrato = entity.NumeroContrato?.Replace(".", "").Replace("-", "");
            obj.NumeroContrato = Convert.ToInt32(nrContrato);

            lista.Add(obj);

            return lista;
        }

        private static IEnumerable<FiltroGridViewModel> GetFiltroGridViewModelAgrupamento(ProgramacaoDesembolso entity, IEnumerable<DocumentoTipo> tpdocumentos)
        {
            var lista = new List<FiltroGridViewModel>();

            foreach (var x in entity.Agrupamentos)
            {
                var obj = new FiltroGridViewModel();

                obj.Id = Convert.ToString(x.Id);
                obj.NumeroAgrupamento = x.NumeroAgrupamento.ToString("D5");
                obj.NumSiafemProgDesembolso = x.NumeroSiafem;
                obj.AgrupamentoId = entity.Id;
                obj.TipoDespesa = x.CodigoDespesa;
                obj.DocumentoTipo = tpdocumentos.FirstOrDefault(w => w.Id == x.DocumentoTipoId)?.Descricao;
                obj.NumeroDocumento = x.NumeroDocumento;
                obj.Total = x.Valor;
                obj.DataVencimento = x.DataVencimento.ToShortDateString();
                obj.Data = entity.DataCadastro.ToShortDateString();
                obj.StatusSiafem = string.IsNullOrEmpty(x.StatusSiafem) || x.StatusSiafem.Equals("N") ? "Não Transmitido" : (x.StatusSiafem.Equals("E") ? "Erro" : "Sucesso");
                obj.Bloqueio = x.Bloqueio;
                obj.CanceladoOp = x.Cancelado;
                obj.StatusOp = string.Format("{0}{1}{2}" , (x.Cancelado ? "Cancel" : ""), (x.Cancelado && x.Bloqueio ? "/" : ""), (x.Bloqueio ? "Bloq" : ""));
                obj.MensagemSiafem = x.MensagemServicoSiafem;
                obj.CadastroCompleto = entity.CadastroCompleto;
                obj.TransmitirSiafem = entity.TransmitirSiafem;
                obj.TransmitidoSiafem = x.TransmitidoSiafem;
                obj.ProgramacaoDesembolsoTipoId = entity.ProgramacaoDesembolsoTipoId;
                obj.DocumentoTipoId = x.DocumentoTipoId;

                lista.Add(obj);
            }


            return lista;
        }

        public FiltroGridViewModel CreateInstance(ImpressaoRelacaoRERT entity)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                NumeroAgrupamento = Convert.ToString(entity.NumeroAgrupamento) == "0" ? null : Convert.ToString(entity.NumeroAgrupamento),
                NumeroRE = entity.CodigoRelacaoRERT?.Substring(4, 2) == "RE" ? entity.CodigoRelacaoRERT : null,
                NumeroRT = entity.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? entity.CodigoRelacaoRERT : null,
                UnidadeGestora = entity.CodigoUnidadeGestora,
                Gestao = entity.CodigoGestao,
                NumeroBanco = entity.CodigoBanco,
                DataSolicitacao = entity.DataSolicitacao == default(DateTime) ? default(DateTime) : entity.DataSolicitacao,
                Cancelado = entity.FlagCancelamentoRERT == true ? "Sim" : "Não",
                StatusSiafem = string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso",
                FlagTransmitidoSiafem = entity.FlagTransmitidoSiafem == true ? "Sim" : "Não",
                MensagemSiafem = entity.MsgRetornoTransmissaoSiafem
            };
        }

        public int AgrupamentoId { get; set; }
        public int DocumentoTipoId { get; set; }
    }
}

