namespace Sids.Prodesp.Core.Extensions
{
    using System;

    internal static class DateTimeExtension
    {
        public static string ToSiafisicoDateTime(this DateTime date, bool toUpper = true)
        {
            var _date = date.ToString("ddMMMyyyy");
            return toUpper ? _date.ToUpper() : _date;
        }
    }
}
