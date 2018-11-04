using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.Parametrizacao
{
    public class ParametrizacaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Parametrizacao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Parametrizacao_default",
                "Parametrizacao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}