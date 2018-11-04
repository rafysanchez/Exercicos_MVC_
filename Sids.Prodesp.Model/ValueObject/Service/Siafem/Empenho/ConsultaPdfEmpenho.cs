using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class ConsultaPdfEmpenho
    {
        [XmlElement(ElementName = "StatusOperacao")]
        public string StatusOperacao { get; set; }

        [XmlElement(ElementName = "MsgRetorno")]
        public string MsgRetorno { get; set; }

        [XmlElement(ElementName = "NumeroNE")]
        public string NumeroNE { get; set; }

        [XmlElement(ElementName = "PDF_Base64")]
        public string PdfBase64 { get; set; }
    }
}
