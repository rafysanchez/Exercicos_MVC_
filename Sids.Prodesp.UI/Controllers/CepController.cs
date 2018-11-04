using Sids.Prodesp.Application;
using System.Web.Mvc;

namespace Sids.Prodesp.UI.Controllers
{
    public class CepController : Controller
    {
        //
        // GET: /Cep/
        
        public JsonResult GetEnderecoByCep(string cep)
        {
            try
            {
                return Json(App.CommonService.GetEnderecoByCep(cep), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(string.Empty);
            }
        }

    }
}
