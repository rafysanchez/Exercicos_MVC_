using System.Web;

namespace Sids.Prodesp.Core.Services.WebService.Seguranca
{
    using Base;
    using Model.Interface.Log;
    using Model.Interface.Service.Seguranca;
    using System;
    using System.Xml;
    using login = Model.ValueObject.Service.Login;

    public class SiafemSegurancaService : BaseService
    {
        private readonly ISiafemSeguranca _siafemService;

        public SiafemSegurancaService(
            ILogError logError, ISiafemSeguranca siafemService) : base(logError)
        {
            _siafemService = siafemService;
        }

        public string Login(string login, string senha, string unidadeGestora)
        {
            try
            {
                var siafdoc = new login.SIAFDOC
                {
                    cdMsg = "SIAFLOGIN001",
                    SiafemLogin = new login.SiafemLogin
                    {
                        login = new login.login
                        {
                            Codigo = login,
                            Senha = senha,
                            Ano = DateTime.Now.Year.ToString(),
                            CICSS = "false"
                        }
                    }
                };

                var result = _siafemService.Login(login, (senha), unidadeGestora, siafdoc);
                var xm = ConverterXml(result);

                var mensagem = xm.GetElementsByTagName("StatusOperacao")[0].FirstChild.Value;

                if (!bool.Parse(mensagem))
                    mensagem = xm.GetElementsByTagName("MsgErro")[0].FirstChild.Value;

                return mensagem;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public bool AlterarSenha(string login, string senha, string novaSenha, string unidadeGestora)
        {
            try
            {
                var caract = novaSenha.Substring(novaSenha.Length - 1, 1).ToUpper() == "X" ? "y" : "x";

                var siafdoc = new login.SIAFDOC
                {
                    cdMsg = "SIAFTrocaSenha",
                    SiafemLogin = new login.SiafemLogin
                    {
                        login = new login.login
                        {
                            Codigo = login,
                            Senha = senha,
                            NovaSenha = senha == novaSenha ? novaSenha.Substring(0, novaSenha.Length - 1) + caract : novaSenha,
                            ManterSenha = senha == novaSenha ? "s" : " ",
                            Ano = DateTime.Now.Year.ToString(),
                            CICSS = "false"
                        }
                    }
                };

                var result = _siafemService.AlterarSenha(login, senha, unidadeGestora, siafdoc);
                var xm = ConverterXml(result);

                var root = xm.GetElementsByTagName("StatusOperacao")[0].FirstChild.Value;

                if (!bool.Parse(root))
                    throw new Exception("Retorno SIAFEM: " + xm.GetElementsByTagName("MsgErro")[0].FirstChild.Value);

                return bool.Parse(root);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }
        

        private XmlDocument ConverterXml(string xml)
        {
            try
            {
                var xm = new XmlDocument();
                xm.LoadXml(xml.Replace("001 <", "001 "));

                return xm;
            }
            catch
            {
                throw new Exception("SIAFEM - " + xml);
            }
        }
        
    }
    
}
