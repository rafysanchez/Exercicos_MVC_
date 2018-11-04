using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Extension
{
    public static class DateTimeExtension
    {
        public static int GetQuarter(this DateTime date)
        {
            return (date.Month + 2) / 3;
        }
    }
}
