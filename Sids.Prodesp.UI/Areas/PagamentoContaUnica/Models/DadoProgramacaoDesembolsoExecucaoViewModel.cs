using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.UI.Areas.PagamentoContaDer.Models;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoProgramacaoDesembolsoExecucaoViewModel
    {
        public DadoProgramacaoDesembolsoExecucaoViewModel()
        {
            Items = new List<DadoProgramacaoDesembolsoExecucaoItemViewModel>();
            ItemsConfirmacaoPagamento = new List<DadoProgramacaoDesembolsoExecucaoItemViewModel>();
            filtroAdicionarPd = new FiltroAdicionaPD();
            filtroListaPd = new FiltroListaPd();
            confirmacaoPagamento = new ConfirmacaoPagamento();
        }

        //L
        public DadoProgramacaoDesembolsoExecucaoViewModel(PDExecucao entity)
        {
            Items = new List<DadoProgramacaoDesembolsoExecucaoItemViewModel>();
            ItemsConfirmacaoPagamento = new List<DadoProgramacaoDesembolsoExecucaoItemViewModel>();
            this.IdExecucaoPD = entity.IdExecucaoPD;
            this.Codigo = entity.IdExecucaoPD;

            this.IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;
            this.NumeroDocumento = entity.NumeroDocumento;
            this.NumeroContrato = entity.NumeroContrato != null ? entity.NumeroContrato : string.Empty;
            this.CodigoAplicacaoObra = entity.CodigoAplicacaoObra;
            this.NumeroDocumentoGerador = entity.NumeroDocumentoGerador;
            this.DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString());

            this.DataVencimento = entity.DataVencimento == default(DateTime) ? default(string) : Convert.ToString(entity.DataVencimento.ToShortDateString());
            this.TransmitirProdesp = entity.TransmitirProdesp;
            this.TransmitirSiafem = entity.TransmitirSiafem;
            this.UGPagadora = entity.UgPagadora;
            this.UGLiquidante = entity.UgLiquidante;
            this.GestaoPagadora = entity.GestaoPagadora;
            this.GestaoLiquidante = entity.GestaoLiquidante;
            this.TipoExecucao = entity.TipoExecucao;
            this.Valor = entity.Valor.ToString();
            this.Total = entity.Valor.ToString();

            foreach (var item in entity.Items)
            {
                item.ds_transmissao_mensagem_prodesp = entity.MensagemServicoProdesp ?? item.ds_transmissao_mensagem_prodesp;
                this.Items.Add(new DadoProgramacaoDesembolsoExecucaoItemViewModel(item));
            }

            this.confirmacaoPagamento = entity.Confirmacao;
            this.TipoPagamento = entity.TipoPagamento;
            this.EhConfirmacaoPagamento = entity.EhConfirmacaoPagamento ? SimNao.Sim : SimNao.Nao;
            this.DataConfirmacao = entity.DataConfirmacao;
        }

        public PDExecucao ToEntity()
        {
            PDExecucao retorno = new PDExecucao();

            retorno.IdExecucaoPD = this.IdExecucaoPD;
            retorno.IdConfirmacaoPagamento = this.IdConfirmacaoPagamento;
            retorno.TipoExecucao = this.TipoExecucao;
            retorno.TransmitirProdesp = this.TransmitirProdesp;
            retorno.TransmitirSiafem = this.TransmitirSiafem;
            retorno.UgPagadora = this.UGPagadora;
            retorno.UgLiquidante = this.UGLiquidante;
            retorno.GestaoPagadora = this.GestaoPagadora;
            retorno.GestaoLiquidante = this.GestaoLiquidante;
            retorno.NumeroContrato = this.NumeroContrato;
            retorno.CodigoAplicacaoObra = this.CodigoAplicacaoObra;
            retorno.NumeroDocumento = this.NumeroDocumento;
            retorno.NumeroDocumentoGerador = this.NumeroDocumentoGerador;
            retorno.DataEmissao = Convert.ToDateTime(this.DataEmissao);
            retorno.DataVencimento = Convert.ToDateTime(this.DataVencimento);
            
            #region Confirmacao
            retorno.EhConfirmacaoPagamento = this.EhConfirmacaoPagamento == SimNao.Sim;
            retorno.TipoPagamento = this.TipoPagamento;
            retorno.DataConfirmacao = Convert.ToDateTime(this.DataConfirmacao); 
            #endregion

            decimal outvalue = 0;
            Decimal.TryParse(this.Valor, out outvalue);
            retorno.Valor = outvalue;

            if (Items != null && Items.Any())
            {
                var lista = new List<PDExecucaoItem>();
                foreach (var item in Items)
                {
                    lista.Add(item.ToEntity());
                }
                retorno.Items = lista.AsEnumerable();
            }

            //var listaConfirmacaoPagamentoItens = new List<PDExecucaoItem>();
            //foreach (var itemConfirmacaoPagamento in Items)
            //{
            //    listaConfirmacaoPagamentoItens.Add(itemConfirmacaoPagamento.ToEntity());
            //}
            //retorno.ItemsConfirmacaoPagamento = listaConfirmacaoPagamentoItens.AsEnumerable();

            return retorno;
        }

        [Display(Name = "Codigo")]
        public int? Codigo { get; set; }

        //public int? IdExecucaoPD { get; set; }

        private string _idExecucaoPD;

        public int? IdExecucaoPD
        {
            get
            {
                var _idExecucaoPD_temp = Items == null || Items.Count == 0 ? default(string) : Items[0].IdExecucaoPD.ToString();
                _idExecucaoPD = _idExecucaoPD_temp;
                return Convert.ToInt32(_idExecucaoPD);
            }

            set
            {
                valor = value.ToString();
            }
        }

        public int? Agrupamento
        {
            get
            {
                var _agrupamento_temp = Items.Count == 0 ? default(string) : Items[0].AgrupamentoItemPD.ToString();
                return Convert.ToInt32(_agrupamento_temp);
            }

            set
            {
                valor = value.ToString();
            }
        }

        public bool ComConfirmacaoPagamento { get; set; }

        public int IdConfirmacaoPagamento { get; set; }

        public int IdConfirmacaoPagamentoItem { get; set; }

        public string NumeroDocumento { get; set; }

        public string NumeroContrato { get; set; }

        public string NumeroDocumentoGerador { get; set; }

        public string DataEmissao { get; set; }

        public string DataVencimento { get; set; }

        [Display(Name = "Prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Display(Name = "Siafem")]
        public bool? TransmitirSiafem { get; set; }

        [Display(Name = "UG Pagadora")]
        public string UGPagadora { get; set; }

        [Display(Name = "Gestão Pagadora")]
        public string GestaoPagadora { get; set; }

        [Display(Name = "UG Liquidante")]
        public string UGLiquidante { get; set; }

        [Display(Name = "Gestão Liquidante")]
        public string GestaoLiquidante { get; set; }

        [Display(Name = "Tipo de Execução")]
        public int? TipoExecucao { get; set; }

        public DateTime? _data { get; set; }

        private string valor;

        [Display(Name = "Valor")]
        public string Valor
        {
            get
            {
                var valorSomado = Items == null || Items.Count == 0 ? valor : Items.Sum(x => Convert.ToDecimal(x.Valor)).ToString();
                valor = valorSomado;
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        [Display(Name = "Valor(Total)")]
        public string Total
        {
            get
            {
                var valorSomado = Items.Count == 0 ? valor : Items.Sum(x => Convert.ToDecimal(x.Valor)).ToString();
                valor = valorSomado;
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        public FiltroListaPd filtroListaPd { get; set; }
        public FiltroAdicionaPD filtroAdicionarPd { get; set; }
        public FiltroMudapah filtroMudapah { get; set; }
        public List<DadoProgramacaoDesembolsoExecucaoItemViewModel> Items { get; set; }

        public List<DadoProgramacaoDesembolsoExecucaoItemViewModel> ItemsConfirmacaoPagamento { get; set; }

        public ConfirmacaoPagamento confirmacaoPagamento { get; set; }

        [Display(Name = "Confirmação de pagamento?")]
        public SimNao EhConfirmacaoPagamento { get; set; }

        //public DateTime? DataConfirmacao { get; set; }

        public DateTime? DataConfirmacao { get; set; }
        public int? TipoPagamento{ get; set; }
        public string CodigoAplicacaoObra { get; private set; }
    }

    public class FiltroMudapah
    {

        [Display(Name = "UG Mudapah")]
        public string UnidadeGestora { get; set; }
    }

    public class FiltroListaPd
    {
        [Display(Name = "Opção")]
        public string Opcao { get; set; }

        [Display(Name = "Inicio")]
        public string DataInicial { get; set; }

        [Display(Name = "Final")]
        public string DataFinal { get; set; }
    }

    public class FiltroAdicionaPD
    {
        [Display(Name = "Nº PD")]
        public string NumeroPD { get; set; }
    }

    public class DadoProgramacaoDesembolsoExecucaoItemViewModel
    {

        public DadoProgramacaoDesembolsoExecucaoItemViewModel() { }

        //ConsultaPD no C
        public DadoProgramacaoDesembolsoExecucaoItemViewModel(ConsultaPd entity, DadoProgramacaoDesembolsoExecucaoViewModel parent, OrigemConsultaPd origem)
        {

            string ugpagadora = null;
            if (!String.IsNullOrWhiteSpace(entity.UGPagadora))
            {
                ugpagadora = entity.UGPagadora.Split(new string[] { " " }, StringSplitOptions.None)[0];
            }

            string ugliquidante = null;
            if (!String.IsNullOrWhiteSpace(entity.UG))
            {
                ugliquidante = entity.UG.Split(new string[] { " " }, StringSplitOptions.None)[0];
            }

            string gestao = null;
            if (!String.IsNullOrWhiteSpace(entity.Gestao))
            {
                gestao = entity.Gestao.Split(new string[] { " " }, StringSplitOptions.None)[0];
            }

            DataConfirmacaoItem = "";
            if ((entity.DataConfirmacaoItem != default(DateTime) && entity.DataConfirmacaoItem != null) && entity.TransmissaoItemStatusProdesp == "S")
            {
                DataConfirmacaoItem = entity.DataConfirmacaoItem.ToShortDateString();
            }

            IdExecucaoPD = entity.IdExecucaoPD;

            NumeroAgrupamentoProgramacaoDesembolso = entity.NumeroAgrupamentoProgramacaoDesembolso; // Agrupamento da Programação de Desembolso

            AgrupamentoItemPD = entity.AgrupamentoItemPD;

            IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;
            NumPD = entity.NumPD ?? entity.NPD ?? entity.PD;
            NumOB = entity.OB ?? entity.NOB ?? entity.NumOB;
            //NumOB = entity.NumOB;
            NumOBItem = entity.OB ?? entity.NOB ?? entity.NumOB;
            NumOP = string.IsNullOrEmpty(entity?.NumOP) ? string.Empty : entity.NumOP;

            NumeroContrato = entity.NumeroContrato;
            NumeroDocumentoGerador = entity.NumeroDocumentoGerador;
            MensagemRetornoConsultaOP = entity.MensagemRetornoConsultaOP;

            IdTipoDocumento = entity.IdTipoDocumento; //(ConsultaPD)
            NumeroDocumento = entity.NumeroDocumento;

            SiafemStatus = entity.TransmissaoItemStatusSiafem;
            SiafemDescricao = entity.TransmissaoItemMensagemSiafem;

            ProdespStatus = entity.TransmissaoItemStatusProdesp;
            ProdespDescricao = entity.TransmissaoItemMensagemProdesp;

            OBCancelada = false;
            AnoAserpaga = (entity.NumPD ?? entity.NPD ?? entity.PD).Substring(0, 4);
            UGPagadora = ugpagadora ?? parent.UGPagadora;
            UGLiquidante = ugliquidante ?? parent.UGLiquidante;
            GestaoPagadora = parent.GestaoPagadora ?? parent.GestaoPagadora;
            GestaoLiquidante = gestao ?? parent.GestaoLiquidante;
            NouP = "N";
            Valor = entity.Valor;
            Favorecido = entity.Favorecido;
            FavorecidoDesc = entity.FavorecidoDesc ?? entity.CGC_CPF_UG_Favorecida;
            Vencimento = entity.Vencimento ?? entity.DataVencimento;

            Emissao = entity.DataEmissao;
            DataVencimentoItem = entity.Vencimento ?? entity.DataVencimento;
            DataEmissaoItem = entity.Emissao ?? entity.DataEmissao;
            Ordem = entity.ordem;

            IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;

            NumeroCnpjCpfCredor = entity.NumeroCnpjCpfCredor;
            NumeroCnpjcpfPagto = entity.NumeroCnpjCpfPgto;

        }

        //L
        public DadoProgramacaoDesembolsoExecucaoItemViewModel(PDExecucaoItem entity)
        {
            Codigo = entity.Codigo;

            NumeroAgrupamentoProgramacaoDesembolso = entity.NumeroAgrupamentoProgramacaoDesembolso; //Número agrupamento Programação de Desembolso

            NumPD = entity.NumPD;
            IdExecucaoPD = entity.id_execucao_pd;
            //Agrupamento = entity.Agrupamento.GetValueOrDefault();
            AgrupamentoItemPD = entity.AgrupamentoItemPD.GetValueOrDefault();

            //NumOB = entity.NumOB;
            NumOBItem = entity.NumOBItem;
            IdExecucaoPD = entity.id_execucao_pd;
            OBCancelada = entity.OBCancelada;

            NumOP = string.IsNullOrEmpty(entity.NumOP) ? string.Empty : entity.NumOP;

            IdTipoDocumento = entity.IdTipoDocumento;
            NumeroDocumento = entity.NumeroDocumento;

            NumeroContrato = entity.NumeroContrato;
            NumeroDocumentoGerador = entity.NumeroDocumentoGerador;

            DataEmissaoItem = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString());
            DataVencimentoItem = entity.DataVencimento == default(DateTime) ? default(string) : Convert.ToString(entity.DataVencimento.ToShortDateString());
            //DataConfirmacaoItem = entity.DataConfirmacao == default(DateTime) ? default(string): Convert.ToString(entity.DataConfirmacao.ToShortDateString());

            MensagemRetornoConsultaOP = entity.ds_consulta_op_prodesp;

            Favorecido = entity.Favorecido;
            FavorecidoDesc = entity.FavorecidoDesc;

            NumeroCnpjCpfCredor = entity.NumeroCnpjCpfCredor;
            NumeroCnpjcpfPagto = entity.NumeroCnpjcpfPagto;

            UGPagadora = entity.UGPagadora;
            UGLiquidante = entity.UGLiquidante;
            GestaoPagadora = entity.GestaoPagadora;
            GestaoLiquidante = entity.GestaoLiquidante;
            AnoAserpaga = entity.AnoAserpaga;
            Valor = entity.Valor;
            //Emissao = entity.
            //Vencimento = entity.
            Ordem = entity.Ordem;
            NouP = entity.NouP;
            NormalOuPrioritario = entity.NouP;
            Prioritario = entity.Prioritario || entity.NouP.Equals("P");
            SiafemDescricao = entity.ds_transmissao_mensagem_siafem;
            SiafemStatus = entity.cd_transmissao_status_siafem;
            SiafemTransmitidoEm = entity.dt_transmissao_transmitido_siafem;
            SiafemTransmitido = entity.fl_transmissao_transmitido_siafem;

            DataConfirmacaoItem = "";
            if (entity.Dt_confirmacao != default(DateTime) && entity.Dt_confirmacao != null)
            {
                if (entity.cd_transmissao_status_prodesp == "S")
                {
                    DataConfirmacaoItem = entity.Dt_confirmacao.ToString().Substring(0, 10);
                }
            }

            ProdespDescricao = entity.ds_transmissao_mensagem_prodesp;
            ProdespStatus = entity.cd_transmissao_status_prodesp;
            ProdespTransmitidoEm = entity.dt_transmissao_transmitido_prodesp;
            ProdespTransmitido = entity.fl_transmissao_transmitido_prodesp;
        }

        //T no C
        public PDExecucaoItem ToEntity()
        {
            var retorno = new PDExecucaoItem();
            retorno.Codigo = this.Codigo;

            retorno.NumeroAgrupamentoProgramacaoDesembolso = this.NumeroAgrupamentoProgramacaoDesembolso;

            retorno.NumPD = this.NumPD?.ToUpper();
            retorno.id_execucao_pd = this.IdExecucaoPD;
            retorno.AgrupamentoItemPD = this.AgrupamentoItemPD;
            retorno.Dt_confirmacao = Convert.ToDateTime(this.DataConfirmacaoItem);
            retorno.NumOBItem = this.NumOBItem;
            retorno.NumOP = this.NumOP?.ToUpper();
            retorno.NumeroDocumentoGerador = this.NumeroDocumentoGerador;

            retorno.IdTipoDocumento = this.IdTipoDocumento;
            retorno.NumeroDocumento = this.NumeroDocumento;

            retorno.NumeroContrato = this.NumeroContrato;
            retorno.ds_consulta_op_prodesp = this.MensagemRetornoConsultaOP;

            retorno.Favorecido = this.Favorecido;
            retorno.FavorecidoDesc = this.FavorecidoDesc;

            retorno.NumeroCnpjCpfCredor = NumeroCnpjCpfCredor;
            retorno.NumeroCnpjcpfPagto = NumeroCnpjcpfPagto;

            retorno.DataEmissao = Convert.ToDateTime(this.Emissao);
            retorno.DataVencimento = Convert.ToDateTime(this.Vencimento);

            retorno.UGPagadora = this.UGPagadora;
            retorno.UGLiquidante = this.UGLiquidante;
            retorno.GestaoPagadora = this.GestaoPagadora;
            retorno.GestaoLiquidante = this.GestaoLiquidante;
            retorno.NouP = this.NouP;
            retorno.NormalOuPrioritario = this.NormalOuPrioritario;
            retorno.Prioritario = this.Prioritario;

            retorno.AnoAserpaga = this.NumPD.Substring(0, 4);
            retorno.Valor = this.Valor;

            retorno.ds_transmissao_mensagem_siafem = this.SiafemDescricao;
            retorno.cd_transmissao_status_siafem = this.SiafemStatus;
            retorno.dt_transmissao_transmitido_siafem = this.SiafemTransmitidoEm;
            retorno.fl_transmissao_transmitido_siafem = this.SiafemTransmitido;

            retorno.id_confirmacao_pagamento = this.IdConfirmacaoPagamento;
            retorno.IdConfirmacaoPagamentoItem = this.IdConfirmacaoPagamentoItem;
            retorno.Dt_confirmacao = Convert.ToDateTime(this.DataConfirmacaoItem);
            retorno.ds_transmissao_mensagem_prodesp = this.ProdespDescricao;
            retorno.cd_transmissao_status_prodesp = this.ProdespStatus;
            retorno.dt_transmissao_transmitido_prodesp = this.ProdespTransmitidoEm;
            retorno.fl_transmissao_transmitido_prodesp = this.ProdespTransmitido;

            retorno.Executar = this.TransmitirCheckBox;

            return retorno;
        }

        [Display(Name = "Codigo")]
        public int? Codigo { get; set; }

        public int NumeroAgrupamentoProgramacaoDesembolso { get; set; }

        [Display(Name = "Nº PD")]
        public string NumPD { get; set; }

        public int IdExecucaoPD { get; set; }

        public int Agrupamento { get; set; }

        public int AgrupamentoItemPD { get; set; }

        [Display(Name = "N° OB")]
        public string NumOB { get; internal set; }

        public string NumOBItem { get; set; }

        public string NumOP { get; set; }

        public int IdTipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string NumeroContrato { get; set; }

        public string NumeroDocumentoGerador { get; set; }

        public string MensagemRetornoConsultaOP { get; set; }

        public string DataEmissaoItem { get; set; }

        public string DataVencimentoItem { get; set; }

        [Display(Name = "OB Cancelada")]
        public bool? OBCancelada { get; set; }

        [Display(Name = "UG Pagadora")]
        public string UGPagadora { get; set; }

        [Display(Name = "Gestão Pagadora")]
        public string GestaoPagadora { get; set; }

        [Display(Name = "UG Liquidante")]
        public string UGLiquidante { get; set; }

        [Display(Name = "Gst Pagadora")]
        public string GestaoLiquidante { get; set; }

        [Display(Name = "Favorecido")]
        public string Favorecido { get; set; }

        [Display(Name = "FavorecidoDesc")]
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

        [Display(Name = "Ano")]
        public string AnoAserpaga { get; set; }

        [Display(Name = "Prioridade")]
        public string NouP { get; set; }

        public string NormalOuPrioritario { get; set; }

        public bool Prioritario { get; set; }

        [Display(Name = "Vencimento")]
        public string Vencimento { get; set; }

        [Display(Name = "Emissao")]
        public string Emissao { get; set; }

        [Display(Name = "Ordem")]
        public string Ordem { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }

        public decimal ValorDecimal
        {
            get
            {
                try
                {
                    return Decimal.Parse(this.Valor);
                }
                catch
                {
                    return 0;
                }
            }

            set
            {
                this.Valor = value.ToString("c", new CultureInfo("pt-BR"));
            }
        }

        [Display(Name = "Transmitir")]
        //public bool Transmitir { get; set; }

        public bool TransmitirCheckBox { get; set; }

        [Display(Name = "Transmitido Siafem")]
        public bool? SiafemTransmitido { get; set; }

        [Display(Name = "Status Siafem")]
        public string SiafemStatus { get; set; }

        [Display(Name = "Transmitido em Siafem")]
        public DateTime? SiafemTransmitidoEm { get; set; }

        [Display(Name = "Status Siafem")]
        public string SiafemDescricao { get; set; }

        public int IdConfirmacaoPagamento { get; private set; }

        public int IdConfirmacaoPagamentoItem { get; set; }

        public string DataConfirmacaoItem { get; set; }

        //private string dataFormatada;
        //public string DataConfirmacaoItemFormatada
        //{
        //    get
        //    {
        //        var _dataFormatada = !DataConfirmacaoItem.HasValue ? dataFormatada : DataConfirmacaoItem.Value.ToShortDateString();
        //        dataFormatada = _dataFormatada;
        //        return dataFormatada;
        //    }

        //    set
        //    {
        //        dataFormatada = value;
        //    }

        //}

        public bool? ProdespTransmitido { get; set; }

        public string ProdespStatus { get; set; }

        public DateTime? ProdespTransmitidoEm { get; set; }

        public string ProdespDescricao { get; set; }

        public string NumeroCnpjCpfCredor { get; set; }
        public string NumeroCnpjcpfPagto { get; set; }

    }

    public enum OrigemConsultaPd
    {
        ConsultaPD = 1,
        ListaPd = 2
    }

}