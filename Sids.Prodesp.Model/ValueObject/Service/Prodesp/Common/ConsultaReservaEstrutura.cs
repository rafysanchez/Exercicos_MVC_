using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common
{
    public class ConsultaReservaEstrutura
    {
        /// <remarks/>
       // public string outTerminal { get; set; }
        /// <remarks/>
        public string OutCodAplicacao { get; set; }
        /// <remarks/>
        public string OutDataInic { get; set; }
        /// <remarks/>
        public string OutDispEmpenhar { get; set; }
        /// <remarks/>
        public string OutNrReserva { get; set; }
        /// <remarks/>
        public string OutValorAtual { get; set;}
        /// <remarks/>
        public string OutValorEmpenhado { get; set;}
        /// <remarks/>
        public string OutErro { get; set;}
        /// <remarks/>
        public string OutCED { get; set; }

        public List<InfoConsultaReservaEstrutura> ListConsultaEstrutura { get; set; }

    }
}



