using Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica
{
    public class SIAFDOC
    {

        public SIAFDOC() { }

        public string cdMsg { get; set; }
        public SiafemDocNL SiafemDocNL { get; set; }
        public SiafemDocNLPGObras SiafemDocNLPGObras { get; set; }
        public SIAFNLIssReten SIAFNLIssReten { get; set; }
        public SIAFNLRetInss SIAFNLRetInss { get; set; }
        public SiafemListaBoletos SiafemListaBoletos { get; set; }
        public SiafemDocAltListaBoletos SiafemDocAltListaBoletos { get; set; }
        public SiafemDocPD SiafemDocPD { get; set; }
        public SiafemDocCanPD SiafemDocCanPD { get; set; }
        public SiafemDocConsultaPD SiafemDocConsultaPD { get; set; }
        public SiafemDocExecutarPD SiafemDocExepd2 { get; set; }
        public SiafemDocListaPd SiafemDocListaPd { get; set; }
        public SiafemDocConsultaOB SiafemDocConsultaOB { get; set; }
        public SIAFCanOBCTU SiafemDocCanOBCTU { get; set; }
        public SiafemDocAutorizaOBVI SiafemDocAutorizaOBVI { get; set; }
        public SiafemDocRE SiafemDocRE { get; set; }
        public SiafemDocRT SiafemDocRT { get; set; }
        public SiafemDocCanRel SiafemDocCanRel { get; set; }

        [XmlElement("SiafemDocAutobSef")]
        public SiafemDocAutobSef SiafemDocAutorizaOB { get; set; }
    }

    public class descricao
    {
        public Repeticao Repeticao { get; set; }

        public Repeticao repeticao { get; set; }
    }
}
