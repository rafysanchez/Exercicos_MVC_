using System.Collections.Generic;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem.Base;

namespace Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem
{
    public class SIAFDOC
    {
        public string cdMsg { get; set; }
        public SiafemDocNR SiafemDocNR { get; set; }

        public SiafemDocConsultaNR SiafemDocConsultaNR { get; set; }

        public SiafemDocCanNR SiafemDocCanNR { get; set; }
    }

    public class SiafemDocConsultaNR
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNR
    {
        public documento documento { get; set; }
        public cronograma cronograma { get; set; }
        public observacao observacao { get; set; }
    }

    public class SiafemDocNR
    {
        public documento documento { get; set; }
        public cronograma cronograma { get; set; }
        public observacao observacao { get; set; }
    }

    public class observacao
    {
        public Repeticao Repeticao { get; set; }
    }

    public class Repeticao
    {
        public List<obs> obs { get; set; }
        public List<desc> desc { get; set; }
    }

    public class obs
    {
        public string Observacao { get; set; }
    }

    public class cronograma
    {
        public Repeticao Repeticao { get; set; }
    }

    public class desc
    {
        public string Mes { get; set; }
        public string Valor { get; set; }
    }
    
}
