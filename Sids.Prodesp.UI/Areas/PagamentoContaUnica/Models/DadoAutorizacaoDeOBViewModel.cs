using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoAutorizacaoDeOBViewModel
    {
        
        public DadoAutorizacaoDeOBViewModel()
        {
            Items = new List<DadoAutorizacaoDeOBItemViewModel>();
            ItemsConfirmacaoPagamento = new List<DadoAutorizacaoDeOBItemViewModel>();
            filtroListaPd = new FiltroListaPd();
            filtroListaOB = new FiltroListaOB();
        }

        public DadoAutorizacaoDeOBViewModel(OBAutorizacao entity)
        {
            Items = new List<DadoAutorizacaoDeOBItemViewModel>();
            ItemsConfirmacaoPagamento = new List<DadoAutorizacaoDeOBItemViewModel>();
            filtroListaOB = new FiltroListaOB();

            Codigo = entity.IdAutorizacaoOB;
            IdAutorizacaoOB = entity.IdAutorizacaoOB;
            IdExecucaoPD = entity.IdExecucaoPD;

            filtroListaOB.IdAutorizacaoOB = entity.IdAutorizacaoOB;
            AgrupamentoOB = entity.IdAutorizacaoOB;
            TipoExecucao = entity.TipoExecucao;
            GestaoPagadora = entity.Gestao;
            UGPagadora = entity.UnidadeGestora;

            AnoOB = entity.AnoOB;

            Valor = entity.Valor.HasValue ? entity.Valor.ToString(): "0";
            TransmissaoTransmitidoSiafem = entity.TransmissaoTransmitidoSiafem;
            TransmissaoMensagemSiafem = entity.TransmissaoMensagemSiafem;
            TransmisaoStatusSiafem = entity.TransmissaoStatusSiafem;
            TransmissaoDataSiafem = entity.TransmissaoDataSiafem;
            NumeroContrato = entity.NumeroContrato;
            CodigoAplicacaoObra = entity.CodigoAplicacaoObra;
            
            UGPagadora = entity.UgPagadora;
            UGLiquidante = entity.UgLiquidante;
            GestaoPagadora = entity.Gestao;
            GestaoLiquidante = entity.GestaoLiquidante;

            filtroListaOB.NumOB = entity.NumOB;
            filtroListaOB.Agrupamento = Convert.ToInt32(entity.NumOB);
            filtroListaOB.GestaoPagadora = entity.Gestao;
            filtroListaOB.UGPagadora = entity.UgPagadora;

            filtroListaOB.TipoPagamento = entity.TipoPagamento;

            foreach (var item in entity.Items)
            {
                this.Items.Add(new DadoAutorizacaoDeOBItemViewModel(item));
            }

            //foreach (var itemConfirmacao in entity.Items)
            //{
            //    this.ItemsConfirmacaoPagamento.Add(new DadoAutorizacaoDeOBItemViewModel(itemConfirmacao));
            //}

            IdExecucaoPD = entity.IdExecucaoPD;
            IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;
            confirmacaoPagamento = entity.Confirmacao;


            this.confirmacaoPagamento = entity.Confirmacao;
            this.TipoPagamento = entity.TipoPagamento;
            this.EhConfirmacaoPagamento = entity.EhConfirmacaoPagamento ? SimNao.Sim : SimNao.Nao;
            this.DataConfirmacao = entity.DataConfirmacao;
        }

        public OBAutorizacao ToEntity()
        {
            OBAutorizacao retorno = new OBAutorizacao();

            //retorno.IdAutorizacaoOB = Codigo;
            retorno.IdAutorizacaoOB = IdAutorizacaoOB;
            retorno.IdConfirmacaoPagamento = IdConfirmacaoPagamento;
            //retorno.IdAutorizacaoOB = filtroListaOB.IdAutorizacaoOB;
            retorno.TipoPagamento = TipoPagamento;
            retorno.NumeroAgrupamento = Codigo; //AgrupamentoOB;
            retorno.TipoExecucao = TipoExecucao;

            retorno.NumOB = filtroListaOB.NumOB;
            retorno.NumeroAgrupamento = filtroListaOB.Agrupamento;
            //retorno.UnidadeGestora = this.uni.UGPagadora;
            //retorno.Gestao = this.Gestao;
            retorno.UgPagadora = this.UGPagadora;
            retorno.GestaoPagadora = this.GestaoPagadora;

            decimal outvalue = 0;
            Decimal.TryParse(Valor, out outvalue);
            retorno.Valor = outvalue;

            retorno.AnoOB = AnoOB;
            retorno.QuantidadeAutorizacao = Items.Count;

            retorno.TransmissaoStatusSiafem = TransmisaoStatusSiafem;
            retorno.TransmissaoMensagemSiafem = TransmissaoMensagemSiafem;
            retorno.TransmissaoTransmitidoSiafem = TransmissaoTransmitidoSiafem;
            retorno.TransmissaoDataSiafem = TransmissaoDataSiafem;

            retorno.NumeroContrato = NumeroContrato;
            retorno.CodigoAplicacaoObra = CodigoAplicacaoObra;

            var lista = new List<OBAutorizacaoItem>();
            foreach (var item in Items)
            {
                var itemCorreto = item.ToEntity();
                itemCorreto.GestaoPagadora = this.GestaoPagadora;
                itemCorreto.UGPagadora = this.UGPagadora;
                lista.Add(itemCorreto);
            }
            retorno.Items = lista.AsEnumerable();

            var listaConfirmacaoPagamentoItens = new List<OBAutorizacaoItem>();
            foreach (var itemConfirmacaoPagamento in Items)
            {
                var itemConfirmacaoPagamentoCorreto = itemConfirmacaoPagamento.ToEntity();
                itemConfirmacaoPagamentoCorreto.GestaoPagadora = this.GestaoPagadora;
                itemConfirmacaoPagamentoCorreto.UGPagadora = this.UGPagadora;

                listaConfirmacaoPagamentoItens.Add(itemConfirmacaoPagamentoCorreto);
            }
            retorno.ItemsConfirmacaoPagamento = listaConfirmacaoPagamentoItens.AsEnumerable();

            //retorno.IdExecucaoPD = Items.Select(x => x.IdExecucaoPD).FirstOrDefault();


            #region Confirmacao
            retorno.EhConfirmacaoPagamento = this.EhConfirmacaoPagamento == SimNao.Sim;
            retorno.TipoPagamento = this.TipoPagamento;
            retorno.DataConfirmacao = Convert.ToDateTime(this.DataConfirmacao);
            #endregion

            return retorno;
        }

        [Display(Name = "Codigo")]
        public int? Codigo { get; set; }

        [Display(Name = "AnoOB")]
        public string AnoOB { get; set; }

        public int QuantidadeAutorizacao { get; set; }

        public string NumeroContrato { get; set; }

        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Prodesp")]
        public bool? TransmitirProdesp { get; set; }

        [Display(Name = "Siafem")]
        public bool TransmissaoTransmitidoSiafem { get; set; }

        public string TransmisaoStatusSiafem { get; set; }

        public string TransmissaoMensagemSiafem { get; set; }
        
        [Display(Name = "TransmissaoDataSiafem")]
        public DateTime TransmissaoDataSiafem { get; set; }

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

        private string valor;
        [Display(Name = "Valor")]
        public string Valor
        {
            get
            {
                var valorSomado = Items.Count == 0 ? valor : Items.Sum(x => Convert.ToDecimal(x.ValorDecimal)).ToString();
                valor = valorSomado;
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        private string _idAutorizacaoOB;

        public int? IdAutorizacaoOB
        {
            get
            {
                var _idExecucaoOB_temp = Items.Count > 0 && this.Codigo != null ? Items[0].IdAutorizacaoOB.ToString() : default(string);
                _idAutorizacaoOB = _idExecucaoOB_temp;
                return Convert.ToInt32(_idAutorizacaoOB);
            }

            set
            {
                _idAutorizacaoOB = value.ToString();
            }
        }

        [Display(Name = "Agrupamento")]
        //public int? AgrupamentoOB { get; set; }

        //private int _agrupamentoOB;

        //public int? AgrupamentoOB
        //{
        //    get
        //    {
        //        var _agrupamento_temp = Items.Count > 0 && this.Codigo != null ? Items[0].AgrupamentoItemOB.ToString() : default(string);
        //        _agrupamentoOB = Convert.ToInt32(_agrupamento_temp);
        //        return _agrupamentoOB;
        //    }

        //    set
        //    {
        //        _agrupamentoOB = value.Value;
        //    }
        //}

        public int? AgrupamentoOB { get; set; }

        //[Display(Name = "IdAutorizacaoOB")]
        //public int? IdAutorizacaoOB { get; set; }

        public int IdExecucaoPD { get; set; }

        public FiltroListaPd filtroListaPd { get; set; }
        public FiltroAdicionaPD filtroAdicionarPd { get; set; }
        public FiltroMudapah filtroMudapah { get; set; }

        public FiltroListaOB filtroListaOB { get; set; }

        public List<DadoAutorizacaoDeOBItemViewModel> Items { get; set; }

        #region ConfirmacaoPagamento
        public int IdConfirmacaoPagamento { get; set; }

        public List<DadoAutorizacaoDeOBItemViewModel> ItemsConfirmacaoPagamento { get; set; }

        [Display(Name = "Confirmação de pagamento?")]
        public SimNao EhConfirmacaoPagamento { get; set; }

        public ConfirmacaoPagamento confirmacaoPagamento { get; set; }
        public DateTime? DataConfirmacao { get; set; }

        public int? TipoPagamento { get; set; }

        #endregion ConfirmacaoPagamento


    }

    public class FiltroListaOB
    {

        public int? IdAutorizacaoOB { get; set; }

        [Display(Name = "UG Pagadora")]
        public string UGPagadora { get; set; }

        [Display(Name = "Gestão Pagadora")]
        public string GestaoPagadora { get; set; }

        [Display(Name = "NumOB")]
        public string NumOB { get; set; }

        [Display(Name = "Agrupamento")]
        public int Agrupamento { get; set; }

        [Display(Name = "Tipo de Pagamento")]
        public int? TipoPagamento { get; set; }
        
    }

    public class DadoAutorizacaoDeOBItemViewModel
    {

        public DadoAutorizacaoDeOBItemViewModel() { }

        public DadoAutorizacaoDeOBItemViewModel(ConsultaOB entity, DadoAutorizacaoDeOBViewModel parent, OrigemConsultaOB origem)
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

            //IdAutorizacaoOB = parent.IdAutorizacaoOB;
            //parent.IdAutorizacaoOB = entity.IdAutorizacaoOB;
            IdAutorizacaoOB = entity.IdAutorizacaoOB;
            IdAutorizacaoOBItem = entity.IdAutorizacaoOBItem;
            AgrupamentoItemOB = entity.AgrupamentoItemOB;
            NumPD = entity.NumPD ?? entity.NPD ?? entity.PD;
            NumOB = entity.OB ?? entity.NOB ?? entity.NoOB;
            NumOB = NumOB.Length < 11 ? DateTime.Now.Year + "OB" + NumOB: NumOB;
            NoOB = entity.NoOB;
            NumOP = entity.NumOP;
            MensagemRetornoConsultaOP = entity.MensagemRetornoConsultaOP;
            IdExecucaoPD = entity.IdExecucaoPD;
            IdExecucaoPDItem = entity.IdExecucaoPDItem;
            
            NumDoctoGerador = entity.NumeroDocumentoGerador;
            NumeroContrato = entity.NumeroContrato;
            IdTipoDocumento = entity.IdTipoDocumento;
            NumeroDocumento = entity.NumeroDocumento;
            
            Despesa = entity.Despesa;
            OBCancelada = false;
            //AnoAserpaga = (entity.NumPD ?? entity.NPD ?? entity.PD).Substring(0, 4);
            UGPagadora = ugpagadora ?? parent.UGPagadora;
            UGLiquidante = ugliquidante ?? parent.UGLiquidante;
            GestaoPagadora = parent.GestaoPagadora ?? parent.GestaoPagadora;
            GestaoLiquidante = gestao ?? parent.GestaoLiquidante;
            NouP = "N";
            Valor = entity.Valor;
            ValorItem = entity.Valor;
            Favorecido = entity.Favorecido;
            FavorecidoDesc = entity.Favorecido;//entity.FavorecidoDesc;
            //Vencimento = entity.Vencimento ?? entity.DataVencimento;
            Emissao = entity.DataEmissao;
            Ordem = entity.Ordem;
            Banco = entity.Banco;
            Ano = DateTime.Now.Year.ToString();

            SiafemStatus = entity.TransmissaoItemStatusSiafem;
            SiafemItemTransmitido = entity.TransmissaoItemTransmitidoSiafem;
            SiafemDescricao = entity.TransmissaoItemMensagemSiafem;
            SiafemTransmitidoEm = entity.TransmissaoItemDataSiafem;

            IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;
            IdConfirmacaoPagamentoItem = entity.IdConfirmacaoPagamentoItem;

            DataConfirmacaoItem = "";
            if ((entity.DataConfirmacaoItem != default(DateTime) && entity.DataConfirmacaoItem != null) && entity.TransmissaoItemStatusProdesp == "S")
            {
                DataConfirmacaoItem = entity.DataConfirmacaoItem.ToShortDateString();
            }

            ProdespStatus = entity.TransmissaoItemStatusProdesp;
            ProdespTransmitido = entity.TransmissaoItemTransmitidoProdesp;
            ProdespDescricao = entity.TransmissaoItemMensagemProdesp;
            ProdespTransmitidoEm = entity.TransmissaoItemDataProdesp;
            CodigoAplicacaoObra = entity.CodigoAplicacaoObra;

        }

        public DadoAutorizacaoDeOBItemViewModel(OBAutorizacaoItem entity)
        {
            Codigo = entity.Codigo;
            IdAutorizacaoOB = entity.IdAutorizacaoOB;
            IdAutorizacaoOBItem = entity.IdAutorizacaoOBItem;
            AgrupamentoItemOB = entity.AgrupamentoItemOB.GetValueOrDefault();
            NumPD = entity.NumPD;
            NumOB = entity.NumOB;
            IdExecucaoPD = entity.IdExecucaoPD;
            IdExecucaoPDItem = entity.IdExecucaoPDItem;
            NumOP = entity.NumOP;
            MensagemRetornoConsultaOP = entity.MensagemRetornoConsultaOP;
            
            OBCancelada = entity.OBCancelada;
            NumDoctoGerador = entity.NumeroDocumentoGerador;
            IdTipoDocumento = entity.IdTipoDocumento;
            NumeroDocumento = entity.NumeroDocumento;
            NumeroContrato = entity.NumeroContrato;

            Favorecido = entity.Favorecido;
            FavorecidoDesc = entity.FavorecidoDesc;

            Ordem = entity.Ordem;
            Despesa = entity.CodigoDespesa;
            Banco = entity.NumeroBanco;
            ValorItem = entity.ValorItem;
            Valor = entity.ValorItem;
                        
            SiafemDescricao = entity.TransmissaoItemMensagemSiafem;
            SiafemStatus = entity.TransmissaoItemStatusSiafem;
            SiafemTransmitidoEm = entity.TransmissaoItemDataSiafem;
            SiafemItemTransmitido = entity.TransmissaoItemTransmitidoSiafem;

            IdConfirmacaoPagamento = entity.id_confirmacao_pagamento;
            IdConfirmacaoPagamentoItem = entity.id_confirmacao_pagamento_item;
            ProdespTransmitido = entity.TransmissaoItemTransmitidoProdesp;
            ProdespStatus = entity.TransmissaoItemStatusProdesp;
            ProdespTransmitidoEm = entity.TransmissaoItemDataProdesp;
            ProdespDescricao = entity.TransmissaoItemMensagemProdesp;

            CodigoAplicacaoObra = entity.CodigoAplicacaoObra;

            DataConfirmacaoItem = "";
            if (entity.DataConfirmacaoItem != default(DateTime) && entity.DataConfirmacaoItem != null)
            {
                if (entity.TransmissaoItemStatusProdesp == "S")
                {
                    DataConfirmacaoItem = entity.DataConfirmacaoItem.ToString().Substring(0, 10);
                }
            }
        }

        public OBAutorizacaoItem ToEntity()
        {
            var retorno = new OBAutorizacaoItem();
            retorno.Codigo = Codigo;
            retorno.IdAutorizacaoOB = IdAutorizacaoOB;
            retorno.IdAutorizacaoOBItem = IdAutorizacaoOBItem;
            retorno.AgrupamentoItemOB = AgrupamentoItemOB;
            retorno.NumPD = NumPD;
            retorno.NumOB = NumOB;
            retorno.NumOP = NumOP;
            retorno.IdExecucaoPD = IdExecucaoPD;
            retorno.IdExecucaoPDItem = IdExecucaoPDItem;
            retorno.NumDoctoGerador = NumDoctoGerador;
            retorno.IdTipoDocumento = IdTipoDocumento;
            retorno.NumeroDocumento = NumeroDocumento;
            retorno.NumeroContrato = NumeroContrato;
                                    
            retorno.Favorecido = Favorecido;
            retorno.FavorecidoDesc = FavorecidoDesc;

            retorno.ValorItem = Valor;
            
            retorno.TransmissaoItemMensagemSiafem = SiafemDescricao;
            retorno.TransmissaoItemStatusSiafem = SiafemStatus;
            retorno.TransmissaoItemDataSiafem = SiafemTransmitidoEm;
            retorno.TransmissaoItemTransmitidoSiafem = SiafemItemTransmitido;

            retorno.CodigoDespesa = Despesa;
            retorno.NumeroBanco = Banco;

            retorno.id_confirmacao_pagamento = IdConfirmacaoPagamento;
            retorno.id_confirmacao_pagamento_item = IdConfirmacaoPagamentoItem;
            
            retorno.DataConfirmacaoItem = Convert.ToDateTime(DataConfirmacaoItem);
            retorno.TransmissaoItemStatusProdesp = ProdespStatus;
            retorno.TransmissaoItemMensagemProdesp = ProdespDescricao;
            retorno.TransmissaoItemDataProdesp = ProdespTransmitidoEm;
            retorno.TransmissaoItemTransmitidoProdesp = ProdespTransmitido;
            retorno.CodigoAplicacaoObra = CodigoAplicacaoObra;

            retorno.Executar = this.TransmitirCheckBox;

            return retorno;
        }

        [Display(Name = "Codigo")]
        public int? Codigo { get; set; }

        [Display(Name = "IdAutorizacaoOB")]
        public int IdAutorizacaoOB { get; set; }

        public int IdAutorizacaoOBItem { get; set; }

        public int AgrupamentoItemOB { get; set; }

        [Display(Name = "Nº PD")]
        public string NumPD { get; set; }

        [Display(Name = "N° OB")]
        public string NumOB { get; set; }

        [Display(Name = "N° OB")]
        public string NoOB { get; set; }

        [Display(Name = "N° OP")]
        public string NumOP { get; set; }

        public string MensagemRetornoConsultaOP { get; set; }

        public int IdExecucaoPD { get; set; }

        public int IdExecucaoPDItem { get; set; }

        [Display(Name = "Despesa")]
        public string Despesa { get; set; }

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

        [Display(Name = "Ano")]
        public string AnoAserpaga { get; set; }

        [Display(Name = "Prioridade")]
        public string NouP { get; set; }

        [Display(Name = "Vencimento")]
        public string Vencimento { get; set; }

        [Display(Name = "Emissao")]
        public string Emissao { get; set; }

        [Display(Name = "Ordem")]
        public string Ordem { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }

        public string ValorItem { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        [Display(Name = "Agrupamento")]
        public int Agrupamento { get; set; }

        public string NumDoctoGerador { get; set; }

        public int IdTipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string NumeroContrato { get; set; }

        public string Ano { get; set; }

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
        public bool TransmitirCheckBox { get; set; }

        [Display(Name = "Transmitido Siafem")]
        public bool SiafemItemTransmitido { get; set; }

        [Display(Name = "Status Siafem")]
        public string SiafemStatus { get; set; }

        [Display(Name = "Transmitido em Siafem")]
        public DateTime? SiafemTransmitidoEm { get; set; }

        [Display(Name = "Status Siafem")]
        public string SiafemDescricao { get; set; }

        public bool? ProdespTransmitido { get; set; }

        public string ProdespStatus { get; set; }

        public DateTime? ProdespTransmitidoEm { get; set; }

        public string ProdespDescricao { get; set; }

        public int IdConfirmacaoPagamento { get; private set; }

        public int IdConfirmacaoPagamentoItem { get; set; }

        [Display(Name = "Data Confirmação")]
        public string DataConfirmacao { get; set; }

        [Display(Name = "Data Confirmação do Item")]
        public string DataConfirmacaoItem { get; set; }

        [Display(Name = "Código da Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

    }

    public enum OrigemConsultaOB
    {
        ConsultaOB = 1,
        ListaOB = 2
    }
}