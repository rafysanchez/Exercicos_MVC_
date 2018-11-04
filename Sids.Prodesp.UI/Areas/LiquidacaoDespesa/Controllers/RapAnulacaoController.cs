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


    public class RapAnulacaoController : LiquidacaoDespesaController
    {               


        [PermissaoAcesso(Controller = typeof(RapAnulacaoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "RapAnulacao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new  RapAnulacao()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }
        

        [PermissaoAcesso(Controller = typeof(RapAnulacaoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "RapAnulacao");
                _filterItems = Display(new RapAnulacao(), form);

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

            return View("CreateEdit", Display(new RapAnulacao(), true));
        }

        public ActionResult CreateThis(string id)
        {
            var objModel = App.RapAnulacaoService.Selecionar(Convert.ToInt32(id));

            objModel.TransmitirProdesp = true;
            objModel.TransmitirSiafem = true;
            objModel.Valor = default(int);

            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(RapAnulacao), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.RapAnulacaoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));
        }
        
        [PermissaoAcesso(Controller = typeof(RapAnulacaoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.RapAnulacaoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoSiafisico || objModel.TransmitidoProdesp)
                    throw new Exception("O RapAnulacao transmitido não pode ser excluído");

                var acao = App.RapAnulacaoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
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
                var subEmpenho = App.RapAnulacaoService.Selecionar(Convert.ToInt32(id));
                
                var consultaNL = App.RapAnulacaoService.ConsultaNL(usuario, subEmpenho.CodigoUnidadeGestora, subEmpenho.CodigoGestao, subEmpenho.NumeroSiafemSiafisico);
                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfLiquidacaoDespesa(consultaNL, "RapAnulacao", subEmpenho);
                

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Transmitir(RapAnulacao entity)
        {
            var objModel = new RapAnulacao();
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                entity.RegionalId = (short)usuario.RegionalId == 1
                ? Convert.ToInt16(16) : Convert.ToInt16((short)usuario.RegionalId);
                
                _modelId = SalvarService(entity, 0);

                App.RapAnulacaoService.Transmitir(_modelId, usuario, (int)_funcId);

                objModel = App.RapAnulacaoService.Selecionar(_modelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.RapAnulacaoService.Selecionar(_modelId);
                var status = "Falha";

                if (objModel.StatusProdesp.Equals("E") && objModel.TransmitirProdesp)
                    status = "Falha Prodesp";

                if (objModel.StatusProdesp.Equals("S") && !objModel.StatusDocumento &&
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
                var msg = App.RapAnulacaoService.Transmitir(ids, usuario, (int)_funcId);

                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(RapAnulacaoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("RapAnulacao");
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
        public ActionResult Save(RapAnulacao entity)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                entity.RegionalId = (short)usuario.RegionalId == 1
                 ? Convert.ToInt16(16): Convert.ToInt16((short)usuario.RegionalId);
                
                return Json(new { Status = "Sucesso", Id = SalvarService(entity, Convert.ToInt32(_funcId)) });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(RapAnulacao entity, int funcionalidade)
        {
            var acao = entity.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.RapAnulacaoService.SalvarOuAlterar(
                entity,
                funcionalidade,
                Convert.ToInt16(acao));
        }
    }

 }