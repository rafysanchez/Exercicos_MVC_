namespace Sids.Prodesp.Infrastructure.Services
{
    using Helpers;
    using Model.Interface.Service;
    using System;
    using System.IO;
    using System.Net;

    public class CommonWs : ICommon
    {
        public string GetAddressByZipCode(string zipCode)
        {
            return ExecuteRequest(string.Concat("http://viacep.com.br/ws/", zipCode, "/json/"));
        }

        public string GetCaptcha()
        {
            PularCertificadoSefaz.SetCertificatePolicy("http://10.200.45.234");
            return ExecuteRequest("http://10.200.45.234/captcha.api/api/captcha/v1.0/gera/300/100");
        }



        private static string ExecuteRequest(string url)
        {
            var json = default(string);

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/json; charset=utf-8";
                req.ProtocolVersion = HttpVersion.Version11;
                req.KeepAlive = false;

                var response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return json;
        }
    }
}
