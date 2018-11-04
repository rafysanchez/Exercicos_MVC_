namespace Sids.Prodesp.Core.Extensions
{
    internal static class StringExtension
    {
        public static string NormalizeForService(this string text)
        {
            if (text == null)
                return " ";

            const string source = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç<>";
            const string target = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc  ";

            for (int i = 0; i < source.Length; i++)
                text = text.Replace(source[i].ToString(), target[i].ToString());

            return text;
        }
    }
}
