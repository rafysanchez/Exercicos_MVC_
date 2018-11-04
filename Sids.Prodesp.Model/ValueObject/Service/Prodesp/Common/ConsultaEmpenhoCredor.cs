

using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common
{
    public class ConsultaEmpenhoCredor
    {
        /// <remarks/>
        public string outCGC { get; set; }
        /// <remarks/>
        public string outContrato { get; set; }

        public string outNotaFiscal { get; set; }

        public string outCredorReduzido { get; set; }
        /// <remarks/>
        public string outDisponivelSubEmpenhar { get; set; }
        /// <remarks/>
        public string outErro { get; set; }
        /// <remarks/>
        public string outLiqEmpenhado { get; set; }
        /// <remarks/>
        public string outLiqSubEmpenhado { get; set; }
        /// <remarks/>
        public string outNrEmpenho { get; set; }
        /// <remarks/>
        public string outOrganiz { get; set; }
        /// <remarks/>
        public string outSucesso { get; set; }
        /// <remarks/>
        public List<ListConsultaEmpenhoCredor> ListConsultarEmpenhoCredor { get; set; }

    }
}
