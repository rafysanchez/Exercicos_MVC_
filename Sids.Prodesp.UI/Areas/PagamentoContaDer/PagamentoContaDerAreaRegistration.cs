using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer
{
    public class PagamentoContaDerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PagamentoContaDer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PagamentoContaDer_default",
                "PagamentoContaDer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}