namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa
{
    public class SFCODOC
    {
        public string cdMsg { get; set; }
        public SiafisicoDocConItemDetalha SiafisicoDocConItemDetalha { get; set; }
        public SFCOLiqNLBec SFCOLiqNLBec { get; set; }
        public SiafisicoDocNLEmLiq SiafisicoDocNLEmLiq { get; set; }
        public SiafisicoDocNLContrato SiafisicoDocNLContrato { get; set; }
        public SFCONLEstorno SFCONLEstorno { get; set; }
        public SFCOCanNLPregao SFCOCanNLPregao { get; set; }
        public SFCOCanNLBec SFCOCanNLBec { get; set; }
        public SFCONLPregao SFCONLPregao { get; set; }
        public SiafisicoDocPDBEC SiafisicoDocPDBEC { get; set; }
        public SiafisicoDocLiquidaNl SFCOLiquidaNL { get; set; }
    }
}

