using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa
{
    public class LiquidacaoDespesaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LiquidacaoDespesa";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LiquidacaoDespesa_default",
                "LiquidacaoDespesa/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}