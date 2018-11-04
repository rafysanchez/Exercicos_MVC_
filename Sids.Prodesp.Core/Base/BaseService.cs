using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Core.Base
{
    using Infrastructure;
    using Model.Entity.Log;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml;

    public class BaseService
    {
        protected readonly ILogError _log;
        protected const string _aut = "aut";


        protected string _UserId => Encrypt("UsuCodigoIdentificador");
        protected string _UserKey => Encrypt("UsuChaveDeAcesso");
        protected string _UserName => Encrypt("UsuNomeDeUsuario");
        protected string _TipoAutenticacao => Encrypt("TipoAutenticacao");
        internal string Hash => AppConfig.Hash;


        public BaseService(ILogError log)
        {
            _log = log;
        }


        /// <summary>
        /// Preenche as informações básicas de rastreablidade no model antes do insert
        /// </summary>
        /// <param name="entity">Base Model</param>
        protected void PreInsertModel(BaseModel entity)
        {
            if (entity.DataCriacao == DateTime.MinValue || entity.UsuarioCriacao == 0)
            {
                entity.DataCriacao = DateTime.Now;
                entity.UsuarioCriacao = GetUserIdLogado();
            }
        }


        private static readonly Dictionary<Type, int> TypeErros = new Dictionary<Type, int>
        {
            { typeof (WebException), 0 },
            { typeof (XmlException), 1 },
            { typeof (InvalidCastException), 2 },
            { typeof (Exception), 3 }
        };


        protected AcaoEfetuada LogFalha(short actionId, int functionalityId, string arg)
        {
            SaveLog(actionId, functionalityId, AcaoEfetuada.Falha, arg);
            return AcaoEfetuada.Falha;
        }
        protected AcaoEfetuada LogSucesso(int actionId, int functionalityId, string arg)
        {
            SaveLog((short)actionId, functionalityId, AcaoEfetuada.Sucesso, arg);
            return AcaoEfetuada.Sucesso;
        }
        protected AcaoEfetuada LogSucesso(short actionId, int functionalityId, string arg)
        {
            SaveLog(actionId, functionalityId, AcaoEfetuada.Sucesso, arg);
            return AcaoEfetuada.Sucesso;
        }
        protected void SaveLog(short actionId, int functionalityId, AcaoEfetuada result, string arg)
        {
            try
            {
                _log.Save(FetchApplicationLog(functionalityId, result, actionId, arg));
            }
            catch (Exception ex)
            {
                SaveLog(ex, actionId, functionalityId);
            }
        }
        protected Exception SaveLog(Exception exception, short? actionId = null, int? functionalityId = null, AcaoEfetuada result = AcaoEfetuada.Falha)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            if (!Enum.IsDefined(typeof(AcaoEfetuada), result))
                throw new InvalidEnumArgumentException(nameof(result), (int)result, typeof(AcaoEfetuada));

            _log.Save(ObterLogAplicacao(exception, actionId, functionalityId, result));

            if (!TypeErros.ContainsKey(exception.GetType()))
                return new Exception(exception.Message, exception);

            switch (TypeErros[exception.GetType()])
            {
                case 0:
                case 1:
                case 2:
                case 3:
                default:
                    return new Exception(exception.Message, exception);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Encrypt(string value)
        {
            try
            {
                var clearBytes = Encoding.Unicode.GetBytes(value);

                using (var stream = new MemoryStream())
                using (var writer = new CryptoStream(stream, CreateRijndaelInstance().CreateEncryptor(), CryptoStreamMode.Write))
                {
                    writer.Write(clearBytes, 0, clearBytes.Length);
                    writer.Close();
                    writer.Clear();

                    return Convert.ToBase64String(stream.ToArray());
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Decrypt(string value)
        {
            try
            {
                var cipherBytes = Convert.FromBase64String(value);

                using (var stream = new MemoryStream())
                using (var writer = new CryptoStream(stream, CreateRijndaelInstance().CreateDecryptor(), CryptoStreamMode.Write))
                {
                    writer.Write(cipherBytes, 0, cipherBytes.Length);
                    writer.Close();

                    return Encoding.Unicode.GetString(stream.ToArray());
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Rijndael CreateRijndaelInstance()
        {
            var pdb = new PasswordDeriveBytes(Hash, new byte[]
                { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
            );

            var alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            return alg;
        }

        public static string RandomPassword(int passwordLength)
        {
            const string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789#@$";
            var sb = new StringBuilder();
            var rd = new Random();
            for (var i = 0; i < passwordLength; i++)
            { sb.Append(chars[rd.Next(0, chars.Length)]); }
            return sb.ToString();
        }
        /// <summary>
        /// Retorna o Hash SHA-256 da string informada.
        /// </summary>
        /// <param name="input">String de entrada.</param>
        /// <returns> Hash SHA-256 em formato Hexadecimal.</returns>
        public static string GetSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString().ToUpper();
            }
        }

        private LogAplicacao ObterLogAplicacao(Exception ex, short? acao = null, int? recusoid = null, AcaoEfetuada resultado = AcaoEfetuada.Falha)
        {
            LogAplicacao log = new LogAplicacao();
            log.Data = DateTime.Now;
            log.AcaoId = acao;
            log.UsuarioId = GetUserIdLogado();
            log.Argumento = ex.Message;
            log.Descricao = ex.StackTrace;
            log.RecursoId = recusoid == 0 ? null : recusoid;
            log.ResultadoId = (short?)resultado;
            log.Ip = GetIpUserLogado();
            log.Url = GetIpUrlLogado();
            log.Versao = GetSystemVersion();
            log.Navegador = GetBrowserUserLogado();
            log.Terminal = GetCurrentTerminal();
            log.Xml = GetCurrentXml();

            SetCurrentXml("");

            return log;
        }

        private string GetTerminal()
        {
            return GetCurrentTerminal();
        }

        private LogAplicacao FetchApplicationLog(int functionalityId, AcaoEfetuada result, int actionId, string arg)
        {
            LogAplicacao log = new LogAplicacao();

            log.Data = DateTime.Now;
            log.AcaoId = (short)actionId;
            log.ResultadoId = (short)result;
            log.UsuarioId = GetUserIdLogado();
            log.RecursoId = functionalityId;
            log.Url = GetIpUrlLogado();
            log.Ip = GetIpUserLogado();
            log.Argumento = arg;
            log.Descricao = arg;
            log.Versao = GetSystemVersion();
            log.Navegador = GetBrowserUserLogado();
            log.Terminal = GetTerminal();

            SetCurrentXml("");

            return log;
        }


        #region Log

        public List<LogAplicacao> Search(LogFilter filtro)
        {
            try
            {

                Char delimiter = ';';
                var argumento = filtro.Argumento;
                List<LogAplicacao> dados = new List<LogAplicacao>();
                List<LogAplicacao> logs = new List<LogAplicacao>();

                string[] substrings = new string[] { null };

                substrings = argumento == null ? substrings : argumento.Split(delimiter);

                foreach (var substring in substrings)
                {
                    filtro.Argumento = substring;

                    dados.AddRange(_log.Fetch(filtro).ToList());

                    dados.RemoveAll(x => logs.Contains(x));

                    logs.AddRange(dados.Except(logs).ToList());
                }

                filtro.Argumento = argumento;
                return logs;

            }
            catch (Exception e)
            {
                throw new SidsException(SaveLog(e).Message.ToString());
            }
        }


        public List<LogResultado> GetLogResultados(string obj = null)
        {
            try
            {
                return _log.GetLogResultado(obj).ToList();
            }
            catch (Exception e)
            {
                throw new SidsException(SaveLog(e).Message.ToString());
            }
        }
        public List<LogNavegador> GetLogNavegador(string obj = null)
        {
            try
            {
                return _log.GetLogNavegador(obj).ToList();
            }
            catch (Exception e)
            {
                throw new SidsException(SaveLog(e).Message.ToString());
            }
        }


        #endregion


        public string GetNomeUsuarioLogado()
        {
            try
            {
                //if (HttpContext.Current.Request.Cookies.AllKeys.Contains(_aut) &&
                //HttpContext.Current.Request.Cookies[_aut].Values.Count > 0 &&
                //!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[_aut].Values[_UserKey]))
                return Decrypt(HttpContext.Current.Request.Cookies[_aut].Values[_UserName]);
            }
            catch
            {
                return null;
            }
        }

        public string GetUserKeyLogado()
        {
            try
            {
                return Decrypt(HttpContext.Current.Request.Cookies[_aut].Values[_UserKey]);
            }
            catch
            {
                return null;
            }
        }



        private string GetSystemVersion()
        {
            try
            {
                return GetCurrentVersao();
            }
            catch
            {
                return null;
            }
        }

        public string GetIpUserLogado()
        {
            try
            {
                return GetCurrentIp();
            }
            catch
            {
                return null;
            }
        }
        public string GetIpUrlLogado()
        {
            try
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
            catch
            {
                return null;
            }
        }
        public string GetBrowserUserLogado()
        {
            try
            {
                return GetCurrentBrowser();
            }
            catch
            {
                return null;
            }
        }
        public int GetUserIdLogado()
        {
            try
            {
                return Convert.ToInt32(
                    Decrypt(HttpContext.Current.Request.Cookies[_aut].Values[_UserId])
                );
            }
            catch
            {
                return 0;
            }
        }
        public string GetNomePagina()
        {
            try
            {
                return HttpContext.Current.Request.Url.AbsolutePath;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetCurrentBrowser()
        {
            try
            {
                return GetUserInfoCookie().Values["browser"];
            }
            catch
            {
                return null;
            }
        }

        public void SetCurrentBrowser(string browser)
        {
            var cookie = GetUserInfoCookie();

            if (cookie.Values[nameof(browser)] == null) cookie.Values.Add(nameof(browser), browser);
            else cookie.Values[nameof(browser)] = browser;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public string GetCurrentVersao()
        {
            try
            {
                return Convert.ToString(HttpContext.Current.Session["versao"]);
            }
            catch
            {
                return null;
            }
        }

        public void SetCurrentVersao(string versao)
        {
            HttpContext.Current.Session[nameof(versao)] = versao;
        }

        public string GetCurrentIp()
        {
            try
            {
                return GetUserInfoCookie().Values["ip"];
            }
            catch
            {
                return null;
            }
        }

        public void SetCurrentIp(string ip)
        {
            var cookie = GetUserInfoCookie();

            if (cookie.Values[nameof(ip)] == null) cookie.Values.Add(nameof(ip), ip);
            else cookie.Values[nameof(ip)] = ip;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        public void DeleteCurrentFilter(string tipo)
        {
            HttpContext.Current.Session.Remove(tipo);
        }

        public FormCollection GetCurrentFilter(string tipo)
        {
            try
            {
                return (FormCollection)HttpContext.Current.Session[tipo];
            }
            catch
            {
                return null;
            }
        }

        public void SetCurrentFilter(FormCollection Filter, string tipo)
        {
            HttpContext.Current.Session[tipo] = Filter;
        }

        public string GetCurrentTerminal()
        {
            try
            {
                return Convert.ToString(HttpContext.Current.Session["terminal"]);
            }
            catch
            {
                return null;
            }
        }


        public string GetCurrentXml()
        {
            return (string)HttpContext.Current?.Session["xml"];
        }

        public void SetCurrentXml(string xml)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session["xml"] = xml;
        }

        public void SetCurrentTerminal(string terminal)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session[nameof(terminal)] = terminal;
        }


        public IEnumerator<T> GetCurrentCache<T>(string tipo)
        {
            return (IEnumerator<T>)HttpContext.Current.Cache[tipo];
        }

        public void SetCurrentCache<T>(IEnumerator<T> filter, string tipo)
        {
            HttpContext.Current.Cache.Insert(tipo, filter, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8));
        }

        private static HttpCookie GetUserInfoCookie()
        {
            try
            {
                return HttpContext.Current.Request.Cookies["UserInfo"] ?? new HttpCookie("UserInfo");
            }
            catch
            {
                return new HttpCookie("UserInfo");
            }
        }


        public void SetCurrentUser(Usuario usuarioLogado)
        {
            HttpContext.Current.Session[nameof(usuarioLogado)] = usuarioLogado;
        }

        public Usuario GetCurrentUser()
        {
            try
            {
                return (Usuario)HttpContext.Current.Session["usuarioLogado"];
            }
            catch
            {
                return null;
            }
        }

        public static string RemoverAcentos(string text)
        {
            var comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç<>";
            var semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc  ";

            for (int i = 0; i < comAcentos.Length; i++)
                text = text.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            return text;
        }


        public List<SelectListItem> GerarDrops<TTipo>(string selected, List<TTipo> lista, string value, string text)
        {
            return lista.Select(x => new SelectListItem
            {
                Text = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == text).GetValue(x).ToString(),
                Value = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == value).GetValue(x).ToString(),
                Selected = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == value).GetValue(x).ToString() == selected
            }).ToList();
        }

        internal static List<string> ListaString(int dist, string texto, int qtd)
        {

            var resultado = new List<string>();
            if (texto == null)
            {
                for (var x = 0; x < qtd; x++)
                {
                    resultado.Add(null);
                }
                return resultado;
            }
            texto += texto.Length % 2 > 0 ? " " : "";
            texto = texto.Replace(";", "").Replace(";", "").Replace(";", "");

            for (var x = 0; x < qtd; x++)
            {
                var need = ((x + 1) * dist);
                var fim = texto.Length >= need ? dist : texto.Length - (0 + x * dist);

                if (texto.Length >= (0 + x * dist) && texto.Length > 0)
                    resultado.Add(texto.Substring(0 + x * dist, fim));
                else
                    resultado.Add(" ");
            }

            return resultado;
        }

        public string GetIpAddress()
        {
            string ipAddressString = HttpContext.Current.Request.UserHostAddress;

            if (ipAddressString == null)
                return null;

            IPAddress ipAddress;
            IPAddress.TryParse(ipAddressString, out ipAddress);
            
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                ipAddress = System.Net.Dns.GetHostEntry(ipAddress).AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            }

            return ipAddress.ToString();
        }
        public LoginSiafem ConsultarUsuarioHomologacaoProducao(string usuario, string senha, bool transmitir = false)
        {
            // parâmetro "transmitir" refere-se a chamada SIAFEM ou SIAFISICO
            var user = new LoginSiafem();
            if (AppConfig.WsUrl != "siafemProd")
            {
                user.usuario = transmitir ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                user.senha = Encrypt(AppConfig.WsPassword);
            }
            else
            {
                user.usuario = usuario;
                user.senha = senha;
            }

            return user;
        }

        public class LoginSiafem
        {
            public string usuario { get; set; }
            public string senha { get; set; }
        }
    }
}
