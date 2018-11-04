using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica
{
    public class SiafemDocAutobSef
    {
        [XmlElement("documento")]
        public SiafemDocAutobSefDocumento Documento { get; set; }
    }

    public class SiafemDocAutobSefDocumento
    {
        [XmlElement("Gestao")]
        public string Gestao { get; set; }

        [XmlElement("OB")]
        public string Ob { get; set; }

        [XmlElement("UnidadeGestora")]
        public string UnidadeGestora { get; set; }

        [XmlElement("Valor")]
        public string Valor { get; set; }
    }
}
