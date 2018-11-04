using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class ConsultaPd
    {
        //public string UG { get; set; }
        public string NumPD { get; set; }
        public int IdExecucaoPD { get; set; }
        public int NumeroAgrupamentoProgramacaoDesembolso { get; set; }
        public int AgrupamentoItemPD { get; set; }
        public string PD { get; set; }
        public string NOB { get; set; }
        public string NumOP { get; set; }
        public string CGC_CPF_UG_Favorecida { get; set; }
        public string ContaCorrenteFavorecido { get; set; }
        public string BancoFavorecido { get; set; }
        public string AgenciaFavorecido { get; set; }
        public string AgenciaPagadora { get; set; }
        public string BancoPagador { get; set; }
        public string Classificacao1 { get; set; }
        public string Classificacao2 { get; set; }
        public string Classificacao3 { get; set; }
        public string HoraLanc { get; set; }
        public string DataLanc { get; set; }
        public string Consultaas { get; set; }
        public string ConsultaEm { get; set; }
        public string ContaCorrentePagadora { get; set; }
        public string DataEmissao { get; set; }
        public string DataVencimento { get; set; }
        public string Evento1 { get; set; }
        public string Evento2 { get; set; }
        public string Evento3 { get; set; }
        public string Finalidade { get; set; }
        public string Fonte1 { get; set; }
        public string Fonte2 { get; set; }
        public string Fonte3 { get; set; }
        public string Gestao { get; set; }
        public string GestaoFavorecido { get; set; }
        public string GestaoPagadora { get; set; }
        public string InscricaoEvento1 { get; set; }
        public string InscricaoEvento2 { get; set; }
        public string InscricaoEvento3 { get; set; }
        public string RecDesp1 { get; set; }
        public string RecDesp2 { get; set; }
        public string RecDesp3 { get; set; }
        public string Lista { get; set; }
        public string NLRef { get; set; }
        public string Numero { get; set; }
        public string OB { get; set; }
        public string Status { get; set; }
        public string UG { get; set; }
        public string UGPagadora { get; set; }
        public string Lancadopor { get; set; }
        public string Usuario { get; set; }
        public string Valor { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Valor3 { get; set; }
        public string Processo { get; set; }
        public string AnoMedicaoObra { get; set; }
        public string AnoObra { get; set; }
        public string MesMedicaoObra { get; set; }
        public string MesObra { get; set; }
        public string NumObra { get; set; }
        public string TipoObra { get; set; }
        public string UGObra { get; set; }
        public string OfertaCompra { get; set; }

        public string ordem { get; set; }
        public string Favorecido { get; set; }
        public string FavorecidoDesc { get; set; }
        public string NPD { get; set; }
        public string Emissao { get; set; }
        public string Vencimento { get; set; }
        public string NumOB { get; set; }
        public int IdConfirmacaoPagamento { get; set; }
        public DateTime DataConfirmacaoItem { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroDocumentoGerador { get; set; }
        public string MensagemRetornoConsultaOP { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string TransmissaoItemStatusSiafem { get; set; }
        public bool TransmissaoItemTransmitidoSiafem { get; set; }
        public DateTime? TransmissaoItemDataSiafem { get; set; }
        public string TransmissaoItemMensagemSiafem { get; set; }
        public string TransmissaoItemStatusProdesp { get; set; }
        public bool TransmissaoItemTransmitidoProdesp { get; set; }
        public DateTime? TransmissaoItemDataProdesp { get; set; }
        public string TransmissaoItemMensagemProdesp { get; set; }
        public string NumeroCnpjCpfCredor { get; set; }
        public string NumeroCnpjCpfPgto { get; set; }
    }
}

