using Sids.Prodesp.Model.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.ValueObject
{
    public static class NaturezaValueObject
    {
        public static string Formatar(string natureza, string fonte)
        {
            return String.Format("{0} - {1}", natureza.Formatar("0.0.00.00"), fonte);
        }
    }
}
