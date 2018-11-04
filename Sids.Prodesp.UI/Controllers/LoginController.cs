using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Web.Mvc;
using Sids.Prodesp.UI.Controllers.Base;

namespace Sids.Prodesp.UI.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {

            if (App.AutenticacaoService.EstaAutenticado())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Versao = App.BaseService.GetCurrentVersao();
            return View();
        }

        [HttpPost]
        public ActionResult Acessar(DTOLogin dtoLogin)
        {
            try
            {

                string browser = System.Web.HttpContext.Current.Request.Browser.Browser;
                string ip = App.BaseService.GetIpAddress();
                
                App.BaseService.SetCurrentBrowser(browser);
                App.BaseService.SetCurrentIp(ip);

                Usuario usu =
                    new Usuario
                    {
                        ChaveDeAcesso = dtoLogin.Login,
                        Senha = dtoLogin.Senha,
                        TipoAutenticacao = Model.Enum.TipoAutenticacao.SSO,
                    };

                var result = new { status = "Ok", message = App.AutenticacaoService.Autenticar(ref usu) };


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { status = "Erro", message = ex.Message.ToString() };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EncerrarSessao()
        {
            App.AutenticacaoService.EncerrarSessao();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
        public class DTOLogin
        {
            public string Login { get; set; }
            public string Senha { get; set; }
        }
    }
}