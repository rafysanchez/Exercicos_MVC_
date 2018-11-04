using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class RespostaConsultaNL
    {
        public RespostaConsultaNL() { }

        [XmlElement(ElementName = "UG")]
        public string UG { get; set; }

        [XmlElement(ElementName = "CgcCpf")]
        public string CgcCpf { get; set; }

        [XmlElement(ElementName = "GestaoFavorecido")]
        public string GestaoFavorecido { get; set; }

        [XmlElement(ElementName = "Gestao")]
        public string Gestao { get; set; }

        [XmlElement(ElementName = "DataLancamento")]
        public string DataLancamento { get; set; }

        [XmlElement(ElementName = "DataEmissao")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "NumeroNL")]
        public string NumeroNL { get; set; }

        [XmlElement(ElementName = "Observacao1")]
        public string Observacao1 { get; set; }

        [XmlElement(ElementName = "Observacao2")]
        public string Observacao2 { get; set; }

        [XmlElement(ElementName = "Observacao3")]
        public string Observacao3 { get; set; }

        [XmlElement(ElementName = "Lancadopor")]
        public string Lancadopor { get; set; }

        [XmlElement(ElementName = "LancadoHora")]
        public string LancadoHora { get; set; }

        [XmlElement(ElementName = "LancadoData")]
        public string LancadoData { get; set; }

        [XmlElement(ElementName = "MsgErro")]
        public string MsgErro { get; set; }

        [XmlElement(ElementName = "MsgRetorno")]
        public string MsgRetorno { get; set; }

        [XmlElement(ElementName = "StatusOperacao")]
        public bool StatusOperacao { get; set; }

        public List<ListaEventosNL> ListaEventosNL { get; set; }

        public string TipoDocumento { get; set; }

        public string NumeroReducao { get; set; }

        public string NumeroSuplementacao { get; set; }
    }
}
