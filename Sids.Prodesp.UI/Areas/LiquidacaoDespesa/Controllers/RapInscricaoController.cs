namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Controllers
{
    using Application;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Enum;
    using Model.Interface.LiquidacaoDespesa;
    using Report;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;


    public class RapInscricaoController : LiquidacaoDespesaController
    {


        [PermissaoAcesso(Controller = typeof(RapInscricaoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "RapInscricao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new RapInscricao()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }


        [PermissaoAcesso(Controller = typeof(RapInscricaoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "RapInscricao");
                _filterItems = Display(new RapInscricao(), form);

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

            return View("CreateEdit", Display(new RapInscricao(), true));
        }

        public ActionResult CreateThis(string id)
        {
            //return View("CreateEdit", Display(
            //    App.RapInscricaoService.Selecionar(Convert.ToInt32(id)), 
            //    true)
            //);
            var objModel = App.RapInscricaoService.Selecionar(Convert.ToInt32(id));

            objModel.TransmitirProdesp = true;
            objModel.TransmitirSiafem = true;

            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(RapInscricao), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.RapInscricaoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));
        }

        [PermissaoAcesso(Controller = typeof(RapInscricaoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.RapInscricaoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafem || objModel.TransmitidoSiafisico || objModel.TransmitidoProdesp)
                    throw new Exception("O RapInscricao transmitido não pode ser excluído");

                var acao = App.RapInscricaoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
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
                var rapInscricao = App.RapInscricaoService.Selecionar(Convert.ToInt32(id));

                var consultaNL = App.RapInscricaoService.ConsultaNL(usuario, rapInscricao.CodigoUnidadeGestora, rapInscricao.CodigoGestao, rapInscricao.NumeroSiafemSiafisico);
                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfLiquidacaoDespesa(consultaNL, "RapInscricao", rapInscricao);


                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Transmitir(RapInscricao entity)
        {
            var objModel = new RapInscricao();
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                _modelId = SalvarService(entity, 0);

                App.RapInscricaoService.Transmitir(_modelId, usuario, (int)_funcId, entity.CEDId);

                objModel = App.RapInscricaoService.Selecionar(_modelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.RapInscricaoService.Selecionar(_modelId);
                var status = "Falha";

                if (objModel.StatusProdesp == "E" && objModel.TransmitirProdesp)
                    status = "Falha Prodesp";

                if (objModel.StatusProdesp == "S" && !objModel.StatusDocumento &&
                   (objModel.StatusSiafemSiafisico == ("S") || objModel.StatusSiafemSiafisico == "S"))
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
                var msg = App.RapInscricaoService.Transmitir(ids, usuario, (int)_funcId);

                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [PermissaoAcesso(Controller = typeof(RapInscricaoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("RapInscricao");
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
        public ActionResult Save(RapInscricao entity)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(entity, Convert.ToInt32(_funcId)) });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(RapInscricao entity, int funcionalidade)
        {
            var acao = entity.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.RapInscricaoService.SalvarOuAlterar(
                entity,
                funcionalidade,
                Convert.ToInt16(acao));
        }
    }

}