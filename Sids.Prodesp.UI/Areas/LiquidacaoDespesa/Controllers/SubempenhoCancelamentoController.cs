using Sids.Prodesp.UI.Report;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Controllers
{
    using Application;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Enum;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class SubempenhoCancelamentoController : LiquidacaoDespesaController
    {
        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "SubempenhoCancelamento");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new SubempenhoCancelamento()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "SubempenhoCancelamento");
                _filterItems = Display(new SubempenhoCancelamento(),
                    form);

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

        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new SubempenhoCancelamento(), true));
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.SubempenhoCancelamentoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            try
            {
                var objModel = App.SubempenhoCancelamentoService.Selecionar(Convert.ToInt32(id));
                return View("CreateEdit", Display(objModel, false));
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                
                return View("Index", Display(new SubempenhoCancelamento()));
            }
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoCancelamentoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.SubempenhoCancelamentoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafisico || objModel.TransmitidoProdesp || objModel.TransmitidoSiafem)
                    throw new Exception("O Cancelamento do Empenho transmitido não pode ser excluído");

                var acao = App.SubempenhoCancelamentoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
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
                var subEmpenho = App.SubempenhoCancelamentoService.Selecionar(Convert.ToInt32(id));

                var consultaNL = App.SubempenhoService.ConsultaNL(usuario, subEmpenho.CodigoUnidadeGestora, subEmpenho.CodigoGestao, subEmpenho.NumeroSiafemSiafisico);
                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfLiquidacaoDespesa(consultaNL, "Anulação de Subempenho", subEmpenho);

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Transmitir(SubempenhoCancelamento entity)
        {
            var objModel = new SubempenhoCancelamento();
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                if (entity.ValorAnular != 0)
                {
                    entity.Valor = entity.ValorAnular;
                }
                
                _modelId = SalvarService(entity, 0);
                
                App.SubempenhoCancelamentoService.Transmitir(_modelId, usuario, (int)_funcId);

                objModel = App.SubempenhoCancelamentoService.Selecionar(_modelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.SubempenhoCancelamentoService.Selecionar(_modelId);
                var status = "Falha";

                if(objModel.StatusProdesp == "E" && objModel.TransmitirProdesp)
                    status = "Falha Prodesp";

                if (objModel.StatusProdesp == "S" && !objModel.StatusDocumento &&
                   (objModel.StatusSiafemSiafisico == "S"))
                    status = "Falha Doc";

                return Json(new { Status = status, Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.SubempenhoCancelamentoService.Transmitir(ids, usuario, (int)_funcId);

                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("SubempenhoCancelamento");
                return filtro != null
                    ? Index(filtro)
                    : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _filterItems);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(SubempenhoCancelamento entity)
        {
            try
            {
                var id = SalvarService(entity, Convert.ToInt32(_funcId));

                return Json(new { Status = "Sucesso", Id = id });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(SubempenhoCancelamento entity, int funcionalidade)
        {
            var acao = entity.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;

            return App.SubempenhoCancelamentoService.SalvarOuAlterar(entity, funcionalidade, Convert.ToInt16(acao));
        }
    }
}