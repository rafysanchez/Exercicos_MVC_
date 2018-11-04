using System.Web.Mvc;
using System.Web.Routing;

namespace Sids.Prodesp.UI
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               "ListarPerfil",
               "ListarPerfil/{funcId}",
                new { controller = "Perfil", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CadastrarPerfil",
                "CadastrarPerfil/{funcId}",
                new { controller = "Perfil", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ListarFuncionalidade",
                "ListarFuncionalidade/{funcId}",
                new { controller = "Funcionalidade", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CadastrarFuncionalidade",
                "CadastrarFuncionalidade/{funcId}",
                new { controller = "Funcionalidade", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ListarLog",
                "ListarLog/{funcId}",
                new { controller = "Log", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ListarUsuario",
                "ListarUsuario/{funcId}",
                new { controller = "Usuario", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CadastrarUsuario",
                "CadastrarUsuario/{funcId}",
                new { controller = "Usuario", action = "Create", id = UrlParameter.Optional }
            );
            


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Service",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Service", action = "ViewService", id = UrlParameter.Optional }
            );
        }
    }


}