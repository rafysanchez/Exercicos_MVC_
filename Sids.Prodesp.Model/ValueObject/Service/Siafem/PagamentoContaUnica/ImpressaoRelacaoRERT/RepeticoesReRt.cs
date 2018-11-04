using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    public class RepeticoesReRt
    {
        [XmlElement("OB")]
        public List<ListaOB> ListaOB { get; set; }
    }
}
