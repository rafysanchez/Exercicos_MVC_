using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class ConsultaOB
    {
        public string Ordem { get; set; }

        public string NOB { get; set; }

        public string NoOB { get; set; }

        public int AgrupamentoItemOB { get; set; }

        public string Despesa { get; set; }

        public string OB { get; set; }

        public int IdAutorizacaoOB { get; set; }

        public int IdAutorizacaoOBItem { get; set; }

        public int IdExecucaoPD { get; set; }

        public int IdExecucaoPDItem { get; set; }

        public string NumOP { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroDocumentoGerador { get; set; }
        public string MensagemRetornoConsultaOP { get; set; }
        public string Valor { get; set; }
        public string TransmissaoItemStatusSiafem { get; set; }
        public bool TransmissaoItemTransmitidoSiafem { get; set; }
        public DateTime? TransmissaoItemDataSiafem { get; set; }
        public string TransmissaoItemMensagemSiafem { get; set; }
        public string TransmissaoItemStatusProdesp { get; set; }
        public bool TransmissaoItemTransmitidoProdesp { get; set; }
        public DateTime? TransmissaoItemDataProdesp { get; set; }
        public string TransmissaoItemMensagemProdesp { get; set; }

        public int IdConfirmacaoPagamento { get; set; }
        public int IdConfirmacaoPagamentoItem { get; set; }

        public DateTime DataConfirmacaoItem { get; set; }

        public ConsultaOB() {
            this.Obra = new List<ObraObj>();
            this.ItensLiquidados = new ItensLiquidadosObj();
        }

        /*
        UG	String	6	UG	13 posicoes
        DescUnidadeGestora	String 	40	DescUnidadeGestora 	40 posicoes
        Gestao	String	5	Gestao	5 posicoes
        Prioridade	String	30	Prioridade	30 posicoes
        Numero	String 	11	Numero	11 posicoes
        PDNLOCLista	String	40	PDNLOCLista	40 posicoes
        Agencia	String	5	Agencia	5 posicoes
        AgenciaFav	String	5	AgenciaFav	5 posicoes
        GestaoFav	String	5	GestaoFav	5 posicoes
        ContaCorrente	String	9	ContaCorrente	9 posicoes
        ContaCorrenteFav	String	9	ContaCorrenteFav	9 posicoes
        CgcCpfUG	String	40	CgcCpfUG 	40 posicoes
        Classificacao1	String	9	Classificacao1	9 posicoes
        Classificacao2	String	9	Classificacao2	9 posicoes
        Classificacao3	String	9	Classificacao3	9 posicoes
        Consulta	String	10	Consulta	10 posicoes
        DataEmissao	String	9	DataEmissao	9 posicoes
        DataLancamento	String	9	DataLancamento	9 posicoes
        Evento1	String	6	Evento1	6 posicoes
        Evento2	String	6	Evento2	6 posicoes
        Evento3	String	6	Evento3	6 posicoes
        Finalidade	String 	40	Finalidade	40 posicoes
        Fonte1	String	9	Fonte1	9 posicao
        Fonte2	String	9	Fonte2	9 posicao
        Fonte3	String	9	Fonte3	9 posicao
        Hora 	String	5	Hora	5 posicoes
        InscricaoEvento1	String	20	InscricaoEvento1	20 posicoes
        InscricaoEvento2	String	20	InscricaoEvento2	20 posicoes
        InscricaoEvento3	String	20	InscricaoEvento3	20 posicoes
        Processo	String 	11	Processo	11 posicoes
        Situacao1	String	55	Situacao1	55 posicoes
        Situacao2	String	55	Situacao2	55 posicoes
        Valor	String	17	Valor	17 posicoes
        Valor1	String	17	Valor1	17 posicoes
        Valor2	String	17	Valor2	17 posicoes
        Valor3	String	17	Valor3	17 posicoes
        Consulta	String	10	Consulta	10 posicoes
        Hora	String	5	Hora	5 posicoes
        HoraLancado	String	5	HoraLancado	5 posicoes
        DataEmissao	String	9	DataEmissao	9 posicoes
        DataLancamento	String	9	DataLancamento	9 posicoes
        Lancadopor	String	25	Lancadopor	25 posicoes
        Usuario	String	15	Usuario	15 posicoes
        TipoOB	String	3	TipoOB	3 posicoes

        */

        [XmlElement(ElementName = "UG")]
        public string UG { get; set; }

        [XmlElement(ElementName = "DescUnidadeGestora")]
        public string DescUnidadeGestora { get; set; }

        [XmlElement(ElementName = "Gestao")]
        public string Gestao { get; set; }

        [XmlElement(ElementName = "Prioridade")]
        public string Prioridade { get; set; }

        [XmlElement(ElementName = "NumeroOB")]
        public string NumeroOB { get; set; }

        [XmlElement(ElementName = "PDNLOCLista")]
        public string PDNLOCLista { get; set; }

        [XmlElement(ElementName = "Banco")]
        public string Banco { get; set; }

        [XmlElement(ElementName = "BancoFav")]
        public string BancoFav { get; set; }

        [XmlElement(ElementName = "Agencia")]
        public string Agencia { get; set; }

        [XmlElement(ElementName = "AgenciaFav")]
        public string AgenciaFav { get; set; }

        [XmlElement(ElementName = "GestaoFav")]
        public string GestaoFav { get; set; }

        [XmlElement(ElementName = "ContaCorrente")]
        public string ContaCorrente { get; set; }

        [XmlElement(ElementName = "ContaCorrenteFav")]
        public string ContaCorrenteFav { get; set; }

        [XmlElement(ElementName = "CgcCpfUG")]
        public string CgcCpfUG { get; set; }

        [XmlElement(ElementName = "outRecDesp1")]
        public string OutRecDesp1 { get; set; }

        [XmlElement(ElementName = "outRecDesp2")]
        public string OutRecDesp2 { get; set; }

        [XmlElement(ElementName = "outRecDesp3")]
        public string OutRecDesp3 { get; set; }

        [XmlElement(ElementName = "Classificacao1")]
        public string Classificacao1 { get; set; }

        [XmlElement(ElementName = "Classificacao2")]
        public string Classificacao2 { get; set; }

        [XmlElement(ElementName = "Classificacao3")]
        public string Classificacao3 { get; set; }

        [XmlElement(ElementName = "Evento1")]
        public string Evento1 { get; set; }

        [XmlElement(ElementName = "Evento2")]
        public string Evento2 { get; set; }

        [XmlElement(ElementName = "Evento3")]
        public string Evento3 { get; set; }

        [XmlElement(ElementName = "Finalidade")]
        public string Finalidade { get; set; }

        [XmlElement(ElementName = "Fonte1")]
        public string Fonte1 { get; set; }

        [XmlElement(ElementName = "Fonte2")]
        public string Fonte2 { get; set; }

        [XmlElement(ElementName = "Fonte3")]
        public string Fonte3 { get; set; }

        [XmlElement(ElementName = "InscricaoEvento1")]
        public string InscricaoEvento1 { get; set; }

        [XmlElement(ElementName = "InscricaoEvento2")]
        public string InscricaoEvento2 { get; set; }

        [XmlElement(ElementName = "InscricaoEvento3")]
        public string InscricaoEvento3 { get; set; }

        [XmlElement(ElementName = "Processo")]
        public string Processo { get; set; }

        [XmlElement(ElementName = "Situacao1")]
        public string Situacao1 { get; set; }

        [XmlElement(ElementName = "Situacao2")]
        public string Situacao2 { get; set; }

        //[XmlElement(ElementName = "Valor")]
        //public string Valor { get; set; }

        [XmlElement(ElementName = "Valor1")]
        public string Valor1 { get; set; }

        [XmlElement(ElementName = "Valor2")]
        public string Valor2 { get; set; }

        [XmlElement(ElementName = "Valor3")]
        public string Valor3 { get; set; }

        [XmlElement(ElementName = "Consulta")]
        public string Consulta { get; set; }

        [XmlElement(ElementName = "Hora")]
        public string Hora { get; set; }

        [XmlElement(ElementName = "HoraLancado")]
        public string HoraLancado { get; set; }

        [XmlElement(ElementName = "DataEmissao")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "DataLancamento")]
        public string DataLancamento { get; set; }

        [XmlElement(ElementName = "Lancadopor")]
        public string Lancadopor { get; set; }

        [XmlElement(ElementName = "TipoOB")]
        public string TipoOB { get; set; }

        [XmlElement(ElementName = "Obra")]
        public List<ObraObj> Obra { get; set; }

        [XmlElement(ElementName = "ItensLiquidados")]
        public ItensLiquidadosObj ItensLiquidados { get; set; }
        public string UGPagadora { get; set; }
        public string NumPD { get; set; }
        public string NPD { get; set; }
        public string Favorecido { get; set; }
        public string FavorecidoDesc { get; set; }
        public string Vencimento { get; set; }
        public string DataVencimento { get; set; }
        public string PD { get; set; }

        [XmlIgnore]
        public string CodigoAplicacaoObra { get; set; }

        public List<ObraObj> AllObras()
        {
            foreach (var item in Obra)
            {
                item.OB = this.NumeroOB;
            }

            return Obra; 
        }
        public List<ItensLiquidadosItemObj> AllItensLiquidados()
        {

            foreach (var item in ItensLiquidados.Item)
            {
                item.OB = this.NumeroOB;
            }

            return ItensLiquidados.Item;
        }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "Obra")]
    public class ObraObj {

        [XmlIgnore]
        public string OB { get; set; }

        [XmlElement(ElementName = "Tipo")]
        public string Tipo { get; set; }

        [XmlElement(ElementName = "UG")]
        public string UG { get; set; }

        [XmlElement(ElementName = "Ano")]
        public string Ano { get; set; }

        [XmlElement(ElementName = "Mes")]
        public string Mes { get; set; }

        [XmlElement(ElementName = "Numero")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "AnoMedicao")]
        public string AnoMedicao { get; set; }

        [XmlElement(ElementName = "MesMedicao")]
        public string MesMedicao { get; set; }

    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "ItensLiquidados")]
    public class ItensLiquidadosObj {

        public ItensLiquidadosObj() {
            this.Item = new List<ItensLiquidadosItemObj>();
        }

        [XmlElement(ElementName = "Item")]
        public List<ItensLiquidadosItemObj> Item { get; set; }

    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "Item")]
    public class ItensLiquidadosItemObj
    {

        [XmlIgnore]
        public string OB { get; set; }

        [XmlElement(ElementName = "Sequencia")]
        public string Sequencia { get; set; }

        [XmlElement(ElementName = "SubSequencia")]
        public string SubSequencia { get; set; }

        [XmlElement(ElementName = "CodItem")]
        public string CodItem { get; set; }

        [XmlElement(ElementName = "Quantidade")]
        public string Quantidade { get; set; }

        [XmlElement(ElementName = "Valor")]
        public string Valor { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class CancelarOB {

        public CancelarOB() { }

        public string StatusOperacao { get; set; }
        public string NumeroNL { get; set; }
        public string MsgErro { get; set; }
        public string RetornoID { get; set; }

    }
}
