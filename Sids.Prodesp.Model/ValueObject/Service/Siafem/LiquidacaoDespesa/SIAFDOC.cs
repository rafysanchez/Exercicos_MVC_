namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa
{
    public class SIAFDOC
    {
        public string cdMsg { get; set; }
        public SiafemDocNL SiafemDocNL { get; set; }
        public SiafemDocNL SFCOLiquidaNL { get; set; }
        public SiafemDocNLObras SiafemDocNLObras { get; set; }
        public SiafemDocNLCTObras SiafemDocNLCTObras { get; set; }
        public SiafemDocConsultaNL SiafemDocConsultaNL { get; set; }
        public SIAFDocInscRNP SIAFDocInscRNP { get; set; }
        public SIAFDocIncTraRPNP SIAFDocIncTraRPNP { get; set; }
    }
}


