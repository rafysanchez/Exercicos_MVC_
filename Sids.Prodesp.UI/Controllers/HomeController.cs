using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Web.Mvc;
using Sids.Prodesp.UI.Controllers.Base;

namespace Sids.Prodesp.UI.Controllers
{
    public class HomeController : BaseController
    {
        //[PermissaoAcesso(Controller = typeof(PerfilController))]
        public ActionResult Index()
        {
            if (App.AutenticacaoService.EstaAutenticado())
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                if (usuario == null) return RedirectToAction("EncerrarSessao", "Login");

                ViewBag.AlterarSenha = usuario.SenhaExpirada || usuario.SenhaSiafemExpirada;
                ViewBag.AcessaSiafem = usuario.AcessaSiafem;
                ViewBag.SenhaSiafemExpirada = usuario.SenhaSiafemExpirada;
                ViewBag.AlterarSenhaSiafem = usuario.AlterarSenhaSiafem;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult AtualizarSenha(DTOSalvarSenha dtoSalvarSenha)
        {
            try
            {
                dtoSalvarSenha.Senha = App.UsuarioService.Encrypt(dtoSalvarSenha.Senha);
                dtoSalvarSenha.NovaSenha = App.UsuarioService.Encrypt(dtoSalvarSenha.NovaSenha);
                Usuario obj = App.AutenticacaoService.GetUsuarioLogado();
                var result = App.UsuarioService.AlterarSenha(obj, dtoSalvarSenha.NovaSenha, dtoSalvarSenha.Senha, 0).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public class DTOSalvarSenha
        {
            public string Senha { get; set; }
            public string NovaSenha { get; set; }

        }


    }
}
