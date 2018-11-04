using Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa
{
    using System.Collections.Generic;

    public class Repeticao
    {
        public List<obs> obs { get; set; }
        public List<desc> desc { get; set; }
        public List<NF> NF { get; set; }
        public List<string> NumeroNF { get; set; }
        public List<des> des { get; set; }
    }
}
