using System.Web;

namespace Sids.Prodesp.Infrastructure.Helpers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;

    internal static class DataHelperSiafem<T> where T : class
    {
        static readonly string BaseYear = AppConfig.AnoExercicio;
        static readonly string Year = !string.IsNullOrEmpty(BaseYear) ? BaseYear : DateTime.Now.Year.ToString();
        static readonly string Url = AppConfig.WsUrl;
        static readonly string AccessType = AppConfig.Acesso;
        private const string Urlhom = "siafemhom.intra.fazenda.sp.gov.br";

        public static string Send(string login, string password, string unidadeGestora, T obj, bool tagId = false)
        {
            string outerXml = string.Empty;

            //todo: hack para nao haver self-closed tags.
            Regex emptyElementRegex = new Regex(@"<(\w+)\s*/>");
            outerXml = ToXml(obj, tagId).OuterXml;
            outerXml = emptyElementRegex.Replace(outerXml, @"<$1></$1>");
            
            if (AppConfig.WsUrl == "siafemProd" && !unidadeGestora.StartsWith("#"))
            {
                unidadeGestora = string.Empty;
            }

            return CallService(login, password, unidadeGestora, outerXml);
        }
        public static string Send(string login, string password, string unidadeGestora, T obj, params string[] tagNames)
        {
            return CallService(login, password, unidadeGestora, ToXml(obj, tagNames).OuterXml);
        }


        private static string CallService(string login, string password, string unidadeGestora, string xmlMessage)
        {

            if (AccessType != "WebService")
            {
                return new Services.Moq.RecebeMSG().Message(login, password, Year, unidadeGestora, xmlMessage);
            }

            string result;

            switch (Url)
            {
                case "siafemProd":
                    result = new siafemProd.RecebeMSG().Mensagem(login, password, Year, unidadeGestora, xmlMessage);
                    break;
                default:
                    PularCertificadoSefaz.SetCertificatePolicy(Urlhom);
                    result = new Siafem.RecebeMSG().Mensagem(login, password, Year, unidadeGestora, xmlMessage);
                    break;
            }
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session["xmlSiafem"] = result.Contains("SIAFTrocaSenha") || result.Contains("SIAFLOGIN001") ? "" : result;
            }

            return result;

        }
        private static XmlDocument ToXml(T document, bool tagId = false)
        {
            using (var stream = new StringWriter())
            {
                return XmlDocumentExtensions.LoadXmlDocumentFromXmlStream(document, stream)
                    .GenerateIdForTagName(tagId, "NumeroNF")
                    .GenerateIdForTagName(tagId, "NF")
                    .GenerateIdForTagName(tagId, "desc")
                    .GenerateIdForTagName(tagId, "obs")
                    .GenerateIdForTagName(tagId, "des")
                    .RepairSelfClosingTag("PlanoInterno")
                    .RepairRepeatedTag("repeticao")
                    .RepairRepeatedTag("desc")
                    .RepairRepeatedTag("linha")
                    .RemoveTag("string");
            }
        }
        private static XmlDocument ToXml(T document, params string[] tagNames)
        {
            using (var stream = new StringWriter())
            {
                return XmlDocumentExtensions.LoadXmlDocumentFromXmlStream(document, stream)
                    .RepairRepeatedTag("NF")
                    .RepairRepeatedTag("desc")
                    .RepairRepeatedTag("obs");
            }
        }
    }
}
