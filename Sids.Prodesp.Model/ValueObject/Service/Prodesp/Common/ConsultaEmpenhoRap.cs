

using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common
{
    public class ConsultaEmpenhoRap
    {


        public string outNrEmpenho { get; set; }
        /// <remarks/>
        public string outCredor { get; set; }

        public string outNrContrato { get; set; }

        /// <remarks/>
        public string outValorIniRef_EMP { get; set; }
        /// <remarks/>
        public string outValorAnul_EMP { get; set; }

        public string outValorLiqEmp_EMP { get; set; }

        public string outValorpagoExerc_SUB { get; set; }
        /// <remarks/>
        public string outValorInscRAP_SUB { get; set; }
        /// <remarks/>
        public string outValorReqRAP_SUB { get; set; }
        /// <remarks/>
        public string outValorAnulRAP_SUB { get; set; }
        /// <remarks/>
        public string outSaldoReq_SUB { get; set; }
        /// <remarks/>
        public string outTotalUtilizado_SUB { get; set; }
        /// <remarks/>
        public string outValorSubEmp_PAG { get; set; }
        /// <remarks/>
        public string outValorReq_PAG { get; set; }

        public string outValorPagar_PAG { get; set; }

        /// <remarks/>
        public List<ListConsultaEmpenhoRap> ListConsultarEmpenhoRap { get; set; }

    }
}
