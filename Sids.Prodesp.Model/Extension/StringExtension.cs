namespace Sids.Prodesp.Model.Extension
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        public static string Formatar(this string str, string formato)
        {
            var cont = default(int);
            var result = default(string);

            if (string.IsNullOrWhiteSpace(str)) return result;

            foreach (var caracters in formato)
            {
                if (str.Length < cont + 1) continue;

                if (caracters == '0')
                {
                    result += str[cont].ToString();
                    cont += 1;
                }
                else
                {
                    result += caracters.ToString();
                }
            }

            return result;
        }

        public static string RemoveSpecialChar(this string input)
        {
            return Regex.Replace(input, "[^0-9a-zA-Z]+", string.Empty);
        }

        public static string FormatarCodigoItem(this string codigo)
        {
            var limpo = codigo.Replace("-", string.Empty);
            var parseado = Int64.Parse((limpo ?? " "));
            var mascarado = parseado.ToString("00000000-0");

            return mascarado;
        }
        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }
    }
}
