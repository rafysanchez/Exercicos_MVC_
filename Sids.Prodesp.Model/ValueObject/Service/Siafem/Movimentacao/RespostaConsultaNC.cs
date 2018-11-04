using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class RespostaConsultaNC
    {
        public RespostaConsultaNC() { }

        [XmlElement(ElementName = "ConsultaAs")]
        public string ConsultaAs { get; set; }

        [XmlElement(ElementName = "ConsultaEm")]
        public string ConsultaEm { get; set; }

        [XmlElement(ElementName = "DataEmissao")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "EventoDesc")]
        public string EventoDesc { get; set; }

        [XmlElement(ElementName = "GestaoDesc")]
        public string GestaoDesc { get; set; }

        [XmlElement(ElementName = "GestaoFavorecido")]
        public string GestaoFavorecido { get; set; }

        [XmlElement(ElementName = "GestaoFavorecidoDesc")]
        public string GestaoFavorecidoDesc { get; set; }

        [XmlElement(ElementName = "LancadoPor")]
        public string LancadoPor { get; set; }

        [XmlElement(ElementName = "LancadoPorAs")]
        public string LancadoPorAs { get; set; }

        [XmlElement(ElementName = "LancadoPorEm")]
        public string LancadoPorEm { get; set; }

        [XmlElement(ElementName = "Numero")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "Obs1")]
        public string Obs1 { get; set; }

        [XmlElement(ElementName = "Obs2")]
        public string Obs2 { get; set; }

        [XmlElement(ElementName = "UgFavorecidoDesc")]
        public string UgFavorecidoDesc { get; set; }

        [XmlElement(ElementName = "UGFavorecido")]
        public string UGFavorecido { get; set; }

        [XmlElement(ElementName = "UGEmitente")]
        public string UGEmitente { get; set; }

        [XmlElement(ElementName = "UgEmitenteDesc")]
        public string UgEmitenteDesc { get; set; }

        [XmlElement(ElementName = "UgPor")]
        public string UgPor { get; set; }

        [XmlElement(ElementName = "Evento")]
        public string Evento { get; set; }      

        [XmlElement(ElementName = "MsgErro")]
        public string MsgErro { get; set; }

        [XmlElement(ElementName = "MsgRetorno")]
        public string MsgRetorno { get; set; }

        [XmlElement(ElementName = "StatusOperacao")]
        public bool StatusOperacao { get; set; }

        public List<ListaEventosNC> ListaEventosNC { get; set; }

        public string TipoDocumento { get; set; }

        public string NumeroReducao { get; set; }

        public string NumeroSuplementacao { get; set; }
    }
}
