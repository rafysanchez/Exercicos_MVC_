using Sids.Prodesp.Model.ValueObject.Service.Siafem.Base;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Reserva
{
    public class SFCODOC
    {
        public string cdMsg { get; set; }
        public SiafisicoDocNRPregao SiafisicoDocNRPregao { get; set; }

        public SiafisicoConsultaOC SiafisicoConsultaOC { get; set; }

    }
    
    public class SiafisicoDocNRPregao
    {
        public documento documento { get; set; }
    }

    public class SiafisicoConsultaOC
    {
        public documento documento { get; set; }
    }
}
