using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common
{
    public class ConsultaContrato
    {
        /// <remarks/>
        public string OutContrato { get; set; }

        /// <remarks/>
        public string OutCpfcnpj { get; set; }


        /// <remarks/>
        public string OutCodObra { get; set; }

        /// <remarks/>
        public string OutContratada { get; set; }

        /// <remarks/>
        public string OutObjeto { get; set; }

        /// <remarks/>
        public string OutProcesSiafem { get; set; }

        /// <remarks/>
        public string OutPrograma { get; set; }

        /// <remarks/>
        public string OutCED { get; set; }

        public string OutTipo { get; set; }



        public List<InfoConsultaContrato> ListConsultaContrato { get; set; }
    }
}
