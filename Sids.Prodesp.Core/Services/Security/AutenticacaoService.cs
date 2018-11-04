namespace Sids.Prodesp.Core.Services.Security
{
    using Infrastructure.Security;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface.Log;
    using Model.Interface.Security;
    using Model.Interface.Seguranca;
    using Model.Interface.Service.Seguranca;
    using Seguranca;
    using System;
    using System.Linq;
    using System.Web;

    public class AutenticacaoService : Base.BaseService
    {
        IAutenticacao _autenticacao;
        private readonly ICrudUsuario _usuario;
        private readonly ICrudFuncionalidade _recurso;
        private readonly ICrudPerfilAcao _perfilAcao;
        private readonly UsuarioService _uService;
        private readonly IAcao _acao;

        public AutenticacaoService(ILogError l, ICrudUsuario u, ICrudFuncionalidade r, ICrudPerfilFuncionalidade pr, ICrudPerfilUsuario pu, ICrudPerfilAcao pa, ICrudPerfil p, IAcao a, ISiafemSeguranca s, IRegional regional)
            : base(l)
        {
            _usuario = u;
            _recurso = r;
            _uService = new UsuarioService(l, u, pu, p, s, regional);
            _perfilAcao = pa;
            _acao = a;
        }

        public bool EstaAutenticado()
        {
            return HttpContext.Current.Request.Cookies.AllKeys.Contains(_aut)
            && HttpContext.Current.Request.Cookies[_aut].Values.Count > 0
            && HttpContext.Current.Request.Cookies[_aut].Values[_UserKey] != string.Empty;
        }

        public string Autenticar(ref Usuario objModel)
        {
            bool retorno = false;
            var message = "";

            if (string.IsNullOrEmpty(objModel.ChaveDeAcesso) || string.IsNullOrEmpty(objModel.Senha))
                throw new SidsException("Preencha os campos com seu usuário e senha");

            try
            {
                TipoAutenticacao tipo = objModel.TipoAutenticacao;
                switch (tipo)
                {
                    case TipoAutenticacao.Ad:
                        _autenticacao = new Ad();
                        break;
                    case TipoAutenticacao.SSO:
                        _autenticacao = new SSO(_usuario);
                        break;
                    case TipoAutenticacao.Facebook:
                        _autenticacao = new Facebook();
                        break;
                    case TipoAutenticacao.Twitter:
                        _autenticacao = new Twitter();
                        break;
                    default:
                        break;
                }

                retorno = _autenticacao.Authenticate(objModel);
                message = retorno ? _uService.ValidaLoginAcesso(ref objModel) : "";

                if (retorno)
                {

                    var cookie = new HttpCookie(_aut);
                    cookie.Values.Add(_UserId, Encrypt(objModel.Codigo.ToString()));
                    cookie.Values.Add(_UserKey, Encrypt(objModel.ChaveDeAcesso));
                    cookie.Values.Add(_UserName, Encrypt(objModel.Nome));
                    cookie.Values.Add(_TipoAutenticacao, Encrypt(Convert.ToString(tipo)));

                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else
                {
                    throw new SidsException("Usuário ou senha atual não confere.");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }

            return message;
        }

        public void EncerrarSessao()
        {
            if (EstaAutenticado())
            {
                HttpContext.Current.Request.Cookies.Remove("UserInfo");
                var myCookie1 = new HttpCookie("UserInfo");
                myCookie1.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie1);

                HttpContext.Current.Request.Cookies.Remove(_aut);
                var myCookie = new HttpCookie(_aut);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        public Usuario GetUsuarioLogado()
        {

            Usuario usu = null;

            if (EstaAutenticado())
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[_aut];

                usu = _uService.Buscar(new Usuario { ChaveDeAcesso = Decrypt(cookie.Values[_UserKey]), Status = true }).FirstOrDefault();
                if (usu != null)
                    usu.TipoAutenticacao = (TipoAutenticacao)Enum.Parse(typeof(TipoAutenticacao), Decrypt(cookie.Values[_TipoAutenticacao]));
            }

            return usu;
        }

        public bool PermiteAcesso(Usuario usuario, string url, string operacao)
        {

            if (usuario == null)
                return false;

            if (usuario.SenhaExpirada)
                return false;

            if (usuario.AcessaSiafem && usuario.SenhaSiafemExpirada)
                return false;

            if (string.IsNullOrEmpty(url))
                return true;

            var _recursos = this._recurso.Fetch(new Funcionalidade { URL = url, Status = true }).ToList();
            var _recursosUser = this._recurso.FetchByUser(usuario).ToList();
            var ids = _recursosUser.Select(x => x.Codigo).ToList();
            var _recurso = _recursos.FirstOrDefault(x => ids.Contains(x.Codigo));

            if (_recurso != null)
                return GetPermissaoPerfilUsuario(_recurso, usuario, operacao);
            else
                return false;
        }

        /// <summary>
        /// Retorna se possui acesso a operação do recurso
        /// </summary>
        /// <returns>Retorna verdadeiro quando o Usuário possui acesso</returns>
        private bool GetPermissaoPerfilUsuario(Funcionalidade r, Usuario u, string operacao)
        {
            var perfilAcoes = _perfilAcao.FetchByUserAndFunctionality(u, r).ToList();
            var acao = _acao.Fetch(new Acao { Descricao = operacao }).FirstOrDefault();

            return perfilAcoes.Any(a => a.Acao == acao?.Id);
        }
    }
}
