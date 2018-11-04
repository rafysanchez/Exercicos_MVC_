using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class CancelarImpressaoRelacaoReRt
    {
        public CancelarImpressaoRelacaoReRt() { }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string UnidadeGestora { get; set; }

        public string Gestao { get; set; }

        public string PrefixoREouRT { get; set; }

        public string NumREouRT { get; set; }

        public string MsgErro { get; set; }
    }
}
