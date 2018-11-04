using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.UI.Report;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class DesdobramentoController : PagamentoContaUnicaController
    {


        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "DesdobramentoInscricao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new Desdobramento()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }



        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            IEnumerable <FiltroGridViewModel> _filterItems = new List<FiltroGridViewModel>();
            try
            {
                App.PerfilService.SetCurrentFilter(form, "DesdobramentoInscricao");
                _filterItems = Display(new Desdobramento(), form);

                if (!_filterItems.Any())
                    ExibirMensagemErro("Registro não encontrado.");

                return View("Index", _filterItems);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _filterItems);
            }

        }

        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new Desdobramento(), true));
        }

        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {

            var objModel = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));
        }


        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoProdesp)
                    throw new Exception("O Desdobramento transmitido não pode ser excluído");

                App.DesdobramentoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                return Json("Sucesso", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Imprimir(string id)
        {
            try
            {
                var desdobramento = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
                var consultaDesdobramento = App.DesdobramentoService.ConsultaDesdobramento(desdobramento);

                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfDesdobramento(consultaDesdobramento, $"Desdobramento para {desdobramento.DesdobramentoTipo.Descricao}", desdobramento);

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }


        public JsonResult Transmitir(Desdobramento desdobramento)
        {
            Desdobramento objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                desdobramento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                ModelId = SalvarService(desdobramento, 0);
                App.DesdobramentoService.Transmitir(ModelId, usuario, (int)_funcId);
                objModel = App.DesdobramentoService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.DesdobramentoService.Selecionar(ModelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.DesdobramentoService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }


        [PermissaoAcesso(Controller = typeof(DesdobramentoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("DesdobramentoInscricao");
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
        public ActionResult Save(Desdobramento desdobramento)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(desdobramento, Convert.ToInt32(_funcId)) });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(Desdobramento desdobramento, int funcionalidade)
        {
            var acao = desdobramento.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.DesdobramentoService.SalvarOuAlterar(
                desdobramento,
                funcionalidade,
                Convert.ToInt16(acao));
        }

    }
}