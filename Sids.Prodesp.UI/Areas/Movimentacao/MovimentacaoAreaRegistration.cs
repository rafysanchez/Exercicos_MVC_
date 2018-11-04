using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.Movimentacao
{
    public class MovimentacaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Movimentacao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Movimentacao_default",
                "Movimentacao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}