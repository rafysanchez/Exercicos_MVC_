using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica
{
    public class PagamentoContaUnicaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PagamentoContaUnica";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PagamentoContaUnica_default",
                "PagamentoContaUnica/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        } 
    }
}