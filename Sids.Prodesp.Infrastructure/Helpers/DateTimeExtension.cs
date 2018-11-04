namespace Sids.Prodesp.Infrastructure.Helpers
{
    using System;
    using System.Runtime.Serialization;

    internal static class DateTimeExtension
    {
        public static object ValidateDBNull(this DateTime value)
        {
            return ValidadeDBNull(value);
        }
        public static object ValidateDBNull(this DateTime? value)
        {
            return ValidadeDBNull(value ?? default(DateTime));
        }



        static object ValidadeDBNull<T>(this T value) where T : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
        {
            return value.Equals(DateTime.MinValue) ? Convert.DBNull : value;
        }
    }
}
