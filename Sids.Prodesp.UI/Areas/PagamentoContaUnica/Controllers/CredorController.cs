using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class CredorController : ConsutasBaseController
    {
        public CredorController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(CredorController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                var objModel = App.CredorService.Listar(new Credor()).ToList();
                return View(CredorGridViewModel(objModel));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(CredorController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.CredorService.AtualizarcCredor(Convert.ToInt32(_funcId));
                var objModel = App.CredorService.Listar(new Credor()).ToList();
                ExibirMensagemSucesso("Credores Atualizados com sucesso!");
                return View("Index", CredorGridViewModel(objModel));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        protected List<DadoCredorGridViewModel> CredorGridViewModel(List<Credor> entities)
        {
            return entities.Select(entity => new DadoCredorGridViewModel().CreateInstance(entity)).ToList();
        }

        [HttpPost]
        public ActionResult PesquisarCredor(string nome)
        {
            List<Credor> teste = new List<Credor>();
            int id = 0;
            try
            {

                var objModel = App.CredorService.Listar(new Credor()).OrderBy(c => c.Id);
                var credorSelecao = objModel.Where(c => c.Prefeitura.Contains(nome.ToUpper()));

                if (credorSelecao.Any())
                {
                    foreach (Credor credor in credorSelecao)
                    {
                        credor.Id = id;
                        teste.Add(credor);
                        id++;
                    }
                }
                else
                {
                    var result = new { Status = "Falha", Msg = "Prefeitura não encontrada." };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                foreach (Credor item in objModel)
                {
                    if (!item.Prefeitura.Contains(nome.ToUpper()))
                    {
                        item.Id = id;
                        teste.Add(item);
                        id++;
                    }
                }

                if (teste.Any())
                {
                    return Json(
                        new
                        {
                            Status = "Sucesso",
                            Credores = CredorGridViewModel(teste)

                        },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Status = "Falha", Msg = "Prefeitura não encontrada." };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}