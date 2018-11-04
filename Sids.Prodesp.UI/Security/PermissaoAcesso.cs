using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Web;
using System.Web.Mvc;

namespace Sids.Prodesp.UI.Security
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PermissaoAcessoAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public PermissaoAcessoAttribute() { }

        /// <summary>
        /// Operação de acesso:
        ///     - Consultar
        ///     - Incluir
        ///     - Alterar
        ///     - Excluir
        /// </summary>
        public string Operacao { get; set; }

        /// <summary>
        /// Controller para acesso
        /// </summary>
        public Type Controller { get; set; }

        /// <summary>
        /// Redirecionamento de página quando não há acesso
        /// </summary>
        /// <param name="filterContext">AuthorizationContext</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Redireciona para a página de acesso negado quando o usuário logado não for autorizado.
            var user = App.AutenticacaoService.GetUsuarioLogado();
            if(user == null)
                filterContext.Result = new RedirectResult("/Home/Index");
            else if(user.SenhaExpirada)
                filterContext.Result = new RedirectResult("/Home/Index");

            if (!App.AutenticacaoService.EstaAutenticado() || App.AutenticacaoService.GetUsuarioLogado()==null )
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }

        /// <summary>
        /// Validação de acesso do usuário por perfil
        /// </summary>
        /// <param name="httpContext">HttpContextBase</param>
        /// <returns>True quando permite acesso, false quando não permite</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            // Verifica se o cookie existe (usuário logado)
            if (App.AutenticacaoService.EstaAutenticado())
            {
                // Recupera o usuário logado
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                string url = Controller != null ? Controller.Name.Replace("Controller", string.Empty) : string.Empty;
                return App.AutenticacaoService.PermiteAcesso(usuario, url, Operacao);
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}