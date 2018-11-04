using Sids.Prodesp.Model.Base.LiquidacaoDespesa;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Controllers
{
    using Application;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Enum;
    using Report;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;


    public class SubempenhoController : LiquidacaoDespesaController
    {


        [PermissaoAcesso(Controller = typeof(SubempenhoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "Subempenho");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new Subempenho()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }


        [PermissaoAcesso(Controller = typeof(SubempenhoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "Subempenho");
                _filterItems = Display(new Subempenho(), form);

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

        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
            
            return View("CreateEdit", Display(new Subempenho(), true));
        }

        public ActionResult CreateThis(string id)
        {
            var objModel = App.SubempenhoService.Selecionar(Convert.ToInt32(id));
            objModel.CodigoNotaFiscalProdesp = "";

            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(Subempenho), Operacao = "Alterar")]
        public ActionResult Edit(string id, string tipo)
        {
            try
            {
                var objModel = App.SubempenhoService.Selecionar(Convert.ToInt32(id));
                var model = Display(objModel, false, tipo.Equals("c"));
                return View("CreateEdit", model);
            }

            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);

                switch (tipo)
                {
                    case "a":
                        return View("Index", Display(new Subempenho()));
                    default:
                        return RedirectToAction("Edit", new { id = id, tipo = tipo });
                }
            }
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.SubempenhoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafisico || objModel.TransmitidoProdesp || objModel.TransmitidoSiafem)
                    throw new Exception("O Subempenho transmitido não pode ser excluído");

                var acao = App.SubempenhoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
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
                var subEmpenho = App.SubempenhoService.Selecionar(Convert.ToInt32(id));

                //XX
                //var consultaNL = App.SubempenhoService.ConsultaNL(usuario, subEmpenho.CodigoUnidadeGestora, subEmpenho.CodigoGestao, subEmpenho.NumeroOriginalSiafemSiafisico);
                var consultaNL = App.SubempenhoService.ConsultaNL(usuario, subEmpenho.CodigoUnidadeGestora, subEmpenho.CodigoGestao, subEmpenho);
                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfLiquidacaoDespesa(consultaNL, "Lançamento de Apropriação", subEmpenho);

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Transmitir(Subempenho entity)
        {
            var objModel = new Subempenho();
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                _modelId = SalvarService(entity, 0);

                App.SubempenhoService.Transmitir(_modelId, usuario, (int)_funcId);

                objModel = App.SubempenhoService.Selecionar(_modelId);
                return Json(new { Status = "Sucesso", Codigo = _modelId, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.SubempenhoService.Selecionar(_modelId);
                var status = "Falha";

                if (objModel.StatusProdesp == "E" && objModel.TransmitirProdesp)
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
                var msg = App.SubempenhoService.Transmitir(ids, usuario, (int)_funcId);

                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(SubempenhoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("Subempenho");
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
        public ActionResult Save(Subempenho entity)
        {
            try
            {
                entity.Itens = entity.Itens ?? new List<LiquidacaoDespesaItem>();

                var id = SalvarService(entity, Convert.ToInt32(_funcId));

                return Json(new { Status = "Sucesso", Id = id });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(Subempenho entity, int funcionalidade)
        {
            var acao = entity.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.SubempenhoService.SalvarOuAlterar(
                entity,
                funcionalidade,
                Convert.ToInt16(acao));
        }
    }

}