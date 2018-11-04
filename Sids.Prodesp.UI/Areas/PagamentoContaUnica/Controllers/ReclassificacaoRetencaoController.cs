using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class ReclassificacaoRetencaoController : PagamentoContaUnicaController
    {

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ReclassificacaoRetencaoInscricao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new ReclassificacaoRetencao()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ReclassificacaoRetencaoInscricao");
                var _filterItems = Display(new ReclassificacaoRetencao(), form);

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

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new ReclassificacaoRetencao(), true));
        }

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.ReclassificacaoRetencaoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.ReclassificacaoRetencaoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));
        }

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.ReclassificacaoRetencaoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafem)
                    throw new Exception("O ReclassificacaoRetencao transmitido não pode ser excluído");

                  var acao = App.ReclassificacaoRetencaoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                return Json(acao.ToString(), JsonRequestBehavior.AllowGet);
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
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var reclassificacaoRetencao = App.ReclassificacaoRetencaoService.Selecionar(Convert.ToInt32(id));

                var consultaNL = App.SubempenhoService.ConsultaNL(usuario, reclassificacaoRetencao.CodigoUnidadeGestora, reclassificacaoRetencao.CodigoGestao, reclassificacaoRetencao.NumeroSiafem);
                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfReclassificacaoRetencao(consultaNL, "Reclassificação / Retenção", reclassificacaoRetencao);

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        public JsonResult Transmitir(ReclassificacaoRetencao desdobramento)
        {
            ReclassificacaoRetencao objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                desdobramento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                ModelId = SalvarService(desdobramento, 0);
                App.ReclassificacaoRetencaoService.Transmitir(ModelId, usuario, (int)_funcId);
                objModel = App.ReclassificacaoRetencaoService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.ReclassificacaoRetencaoService.Selecionar(ModelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ReclassificacaoRetencaoService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(ReclassificacaoRetencaoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("ReclassificacaoRetencaoInscricao");
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
        public ActionResult Save(ReclassificacaoRetencao desdobramento)
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

        private int SalvarService(ReclassificacaoRetencao desdobramento, int funcionalidade)
        {
            var acao = desdobramento.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.ReclassificacaoRetencaoService.SalvarOuAlterar(
                desdobramento,
                funcionalidade,
                Convert.ToInt16(acao));
        }
    }
}