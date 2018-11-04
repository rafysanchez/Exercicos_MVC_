namespace Sids.Prodesp.Model.Extension
{
    using System;

    public static class NumericoExtension
    {
        public static string ZeroParaNulo(this int value)
        {
            return value.ZeroParaNulo<int>();
        }

        public static string ZeroParaNulo(this decimal value)
        {
            return value.ZeroParaNulo<decimal>() ?? string.Empty;
        }

        public static string ZeroParaNulo(this double value)
        {
            return value.ZeroParaNulo<double>();
        }


        /// <summary>
        /// Converte um valor numérico para string, caso seja um valor padrão (0) então converte para uam string nula.
        /// </summary>
        /// <typeparam name="T">Valor numérico a ser convertido.</typeparam>
        /// <param name="value">Valor numérico convertido.</param>
        /// <returns></returns>
        private static string ZeroParaNulo<T>(this T value) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            return value.Equals(default(T)) ? default(string) : Convert.ToString(value);
        }
    }
}
