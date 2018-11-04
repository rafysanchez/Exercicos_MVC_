using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common
{
    public class ConsultaEmpenhoEstrutura
    {
        public string OutTerminal { get; set; }
        public string OutCED { get; set; }
        public string OutSucesso { get; set; }
        public string OutCodAplicacao { get; set; }
        public string OutDispSubEmpenhar { get; set; }
        public string OutNrEmpenho { get; set; }
        public string OutTipoEmpenho { get; set; }
        public string OutValorAtual { get; set; }
        public string OutValorSubEmpenhado { get; set; }
        public string OutErro { get; set; }

        public List<ListConsultaEstrutura> ListConsultaEstrutura { get; set; }
    }
}
