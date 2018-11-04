using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class ListaDeBoletosController : PagamentoContaUnicaController
    {

        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ListaBoletosInscricao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new ListaBoletos()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }


        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ListaBoletosInscricao");
                var _filterItems = Display(new ListaBoletos(), form);

                if (!_filterItems.Any())
                    ExibirMensagemErro("Registro não encontrado.");

                return View("Index", _filterItems);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }

        }


        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.ListaBoletosService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));
        }

        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new ListaBoletos(), true));
        }


        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.ListaBoletosService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, true));
        }


        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.ListaBoletosService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafem)
                    throw new Exception("A lista Boleto transmitida não pode ser excluída");

                App.ListaBoletosService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                return Json("Sucesso", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Transmitir(ListaBoletos desdobramento)
        {
            ListaBoletos objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                desdobramento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                ModelId = SalvarService(desdobramento, 0);
                App.ListaBoletosService.Transmitir(ModelId, usuario, (int)_funcId);
                objModel = App.ListaBoletosService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.ListaBoletosService.Selecionar(ModelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ListaBoletosService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(ListaDeBoletosController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("ListaBoletosInscricao");
                return filtro != null
                    ? Index(filtro)
                    : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", null);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(ListaBoletos listaBoletos)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(listaBoletos, Convert.ToInt32(_funcId)) });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(ListaBoletos desdobramento, int funcionalidade)
        {
            var acao = desdobramento.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.ListaBoletosService.SalvarOuAlterar(
                desdobramento,
                funcionalidade,
                Convert.ToInt16(acao));
        }
    }
}