using Sids.Prodesp.Model.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.ValueObject
{
    public static class CfpValueObject
    {
        public static string Formatar(string codigo, string descricao)
        {
            return String.Format("{0} {1}", codigo.Formatar("00.000.0000.0000"), descricao);
        }
    }
}
