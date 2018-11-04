using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace Sids.Prodesp.UI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected int? _funcId;

        public BaseController()
        {
            App.BaseService.SetCurrentVersao(typeof(BaseController).Assembly.GetName().Version.ToString());
        }


        /// <summary>
        /// Adiciona mensagem ao TempData
        /// </summary>
        /// <param name="_sucesso">True - Sucesso, False - Erro</param>
        /// <param name="_msg">Mensagem a ser exibida</param>
        private void AdicionarMensagem(bool _sucesso, string _msg, bool _novo = false)
        {
            if (TempData.Keys.Contains("Sucesso"))
                TempData.Remove("Sucesso");

            if (TempData.Keys.Contains("Mensagem"))
                TempData.Remove("Mensagem");

            if (TempData.Keys.Contains("NovoCadastro"))
                TempData.Remove("NovoCadastro");

            TempData.Add("Sucesso", _sucesso);
            TempData.Add("Mensagem", _msg);
            TempData.Add("NovoCadastro", _novo);
        }

        /// <summary>
        /// Configura uma mensagem de erro a ser exibida
        /// </summary>
        /// <param name="_msg">Mensagem a ser exibida</param>
        protected void ExibirMensagemErro(string _msg)
        {
            if (_msg.Contains("conexão com o SQL Server"))
                _msg = "Erro de Conexão";

            AdicionarMensagem(false, _msg);
        }

        /// <summary>
        /// Configura uma mensagem de sucesso
        /// </summary>
        /// <param name="_msg">Mensagem a ser exibida</param>
        protected void ExibirMensagemSucesso(string _msg)
        {
            AdicionarMensagem(true, _msg);
        }

        /// <summary>
        /// Configura uma mensagem de sucesso
        /// </summary>
        /// <param name="_msg">Mensagem a ser exibida</param>
        protected void ExibirMensagemSucesso(AcaoEfetuada acao)
        {
            switch (acao)
            {
                case AcaoEfetuada.Sucesso:
                    AdicionarMensagem(true, MensagemGeral.MGRegistroIncluido, true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Retorna acões permitidas ao usuario logado na funcionalidade acessada
        /// </summary>
        [HttpPost]
        public JsonResult PermissoesAcao()
        {
            try
            {
                List<Acao> result = App.AcaoService.ObterAcaoPorFuncionalidadeEUsuario((int)_funcId);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Configura uma mensagem de sucesso
        /// </summary>
        protected void ExibirMensagemSucesso()
        {
            ExibirMensagemSucesso(string.Empty);
        }

        public ActionResult DownloadFile()
        {
            var stream = HttpContext.Session[App.BaseService.GetCurrentIp()] as FileStreamResult;

            if (stream == null)
                return new EmptyResult();

            HttpContext.Session[App.BaseService.GetCurrentIp()] = null;

            return stream;
        }

        public string ConvertPartialViewToString(PartialViewResult partialView)
        {
            using (var sw = new StringWriter())
            {
                partialView.View = ViewEngines.Engines.FindPartialView(ControllerContext, partialView.ViewName).View;

                var vc = new ViewContext(ControllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return partialViewString;
            }
        }

        public ActionResult Info()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[0];
            ViewBag.ConnectionString = connectionString?.ToString();

            var settings = new List<KeyValuePair<string, string>>();

            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                settings.Add(new KeyValuePair<string, string>(key, ConfigurationManager.AppSettings[key]));
            }

            ViewBag.AppSettings = settings;

            var regexValidaSelect = @"^select .+ from (\w+?\.)?\b(\w+)\b ?(\w+)?\b ?(join)? ?(\w+?\.)?\b(\w+)?\b ?(\w+)?\b ? ?(on)?\b ?(\w)?\.?(\w+)?\b ?\=? ?(\w+.\w+)?\b ?(where)?\b ?(\w+.\w+)?\b ?(=|<>|<|>)? ?(\w+)?\b ?(and)? ?(\w+)? ?(=|<>|<|>)? ?(\w)?\b";

            var assembliesPrincipais = new List<Tuple<string, string, string>>();
            var assemblies = new List<Tuple<string, string, string>>();

            var dir = Assembly.GetExecutingAssembly().GetDirectory();

            foreach (string dll in Directory.GetFiles(dir, "*.dll"))
            {
                var currentfile = new FileInfo(dll);

                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(dll);

                var version = myFileVersionInfo.FileVersion;
                var nome = currentfile.Name.Substring(0, currentfile.Name.Length - 4);
                var data = currentfile.CreationTime.ToString();

                if (nome.ToLower().StartsWith("sids."))
                {
                    assembliesPrincipais.Add(new Tuple<string, string, string>(nome, version, data));
                }
                else
                {
                    assemblies.Add(new Tuple<string, string, string>(nome, version, data));
                }
            }

            assemblies.InsertRange(0, assembliesPrincipais);

            ViewBag.Assemblies = assemblies;

            return View();
        }
    }
}