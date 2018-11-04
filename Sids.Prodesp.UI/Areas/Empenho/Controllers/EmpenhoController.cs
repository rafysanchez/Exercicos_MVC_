namespace Sids.Prodesp.UI.Areas.Empenho.Controllers
{
    using Application;
    using Model.Entity.Empenho;
    using Model.Enum;
    using Model.ValueObject.Service.Siafem.Empenho;
    using Report;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class EmpenhoController : EmpenhoBaseController
    {
        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "Empenho");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new Empenho()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "Empenho");
                _filterItems = Display(new Empenho(), form);

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

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new Empenho(), true));
        }

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.EmpenhoService.Buscar(new Empenho { Id = Convert.ToInt32(id) }).SingleOrDefault();
            objModel.DataEntregaMaterial = default(DateTime);
            return View("CreateEdit", Display(objModel, true));
        }

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.EmpenhoService
                .Buscar(new Empenho { Id = Convert.ToInt32(id) }).FirstOrDefault();

            return View("CreateEdit", Display(objModel, false));
        }

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.EmpenhoService.Buscar(new Empenho { Id = Convert.ToInt32(id) }).Single();
                if (objModel.TransmitidoSiafem || objModel.TransmitidoSiafisico || objModel.TransmitidoProdesp)
                    throw new Exception("O Empenho transmitido não pode ser excluído");

                var acao = App.EmpenhoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
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
                var objModel = App.EmpenhoService.Buscar(new Empenho { Id = Convert.ToInt32(id) }).FirstOrDefault();
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHOCONTROLLER - IMPRIMIR - 1: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));

                if (!string.IsNullOrWhiteSpace(objModel.NumeroEmpenhoSiafem))
                {
                    ConsultaPdfEmpenho pdf = App.EmpenhoService.ObterPdfEmpenho(objModel, App.AutenticacaoService.GetUsuarioLogado());

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfEmpenhoDireto(pdf, "Empenho", objModel);
                }
                else if (!string.IsNullOrWhiteSpace(objModel.NumeroEmpenhoSiafisico))
                {
                    ConsultaPdfEmpenho pdf = App.EmpenhoService.ObterPdfEmpenho(objModel, App.AutenticacaoService.GetUsuarioLogado());

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfEmpenhoDireto(pdf, "Empenho", objModel);
                }

                // TODO remover quando chegar o XML do SFCONEPDF001
                if (!string.IsNullOrWhiteSpace(objModel.NumeroEmpenhoSiafem))
                {
                    ConsultaPdfEmpenho pdf = App.EmpenhoService.ObterPdfEmpenho(objModel, App.AutenticacaoService.GetUsuarioLogado());

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfEmpenhoDireto(pdf, "Empenho", objModel);
                }
                else
                if (!string.IsNullOrWhiteSpace(objModel.NumeroEmpenhoSiafisico))
                {
                    //Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfEmpenho(consultaNe, "Empenho", objModel);

                    ConsultaPdfEmpenho pdf = App.EmpenhoService.ObterPdfEmpenho(objModel, App.AutenticacaoService.GetUsuarioLogado());

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfEmpenhoDireto(pdf, "Empenho", objModel);
                }

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Transmitir(ModelSalvar objSave)
        {
            Empenho objModel;
            try
            {
                _modelId = SalvarService(objSave, (int)_funcId);

                App.EmpenhoService.Transmitir(_modelId, App.AutenticacaoService.GetUsuarioLogado(), (int)_funcId);

                objModel = App.EmpenhoService.Buscar(new Empenho { Id = _modelId }).FirstOrDefault();
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHOCONTROLLER - TRANSMITIR - 1: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));
                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });
            }
            catch (Exception ex)
            {
                objModel = App.EmpenhoService.Buscar(new Empenho { Id = _modelId }).FirstOrDefault();

                var status = "Falha";

                if (objModel.StatusProdesp == "E" && objModel.TransmitirProdesp)
                    status = "Falha Prodesp";

                if (objModel.StatusProdesp == "S" && !objModel.StatusDocumento &&
                    objModel.StatusSiafemSiafisico == "S")
                    status = "Falha Doc";

                return Json(new { Status = status, Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var msg = App.EmpenhoService.Transmitir(ids, App.AutenticacaoService.GetUsuarioLogado(), (int)_funcId);

                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });
            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(EmpenhoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("Empenho");
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
        public ActionResult Save(ModelSalvar objSave)
        {
            try
            {

                return Json(new { Status = "Sucesso", Id = SalvarService(objSave, Convert.ToInt32(_funcId)) });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(ModelSalvar objSave, int funcionalidade)
        {
            var acao = objSave.Model.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;

            return App.EmpenhoService.Salvar(
                objSave.Model,
                objSave.Meses ?? new List<EmpenhoMes>(),
                objSave.Itens ?? new List<EmpenhoItem>(),
                funcionalidade,
                (short)acao);
        }

        public class ModelSalvar
        {
            public Empenho Model { get; set; }
            public IEnumerable<EmpenhoItem> Itens { get; set; }
            public IEnumerable<EmpenhoMes> Meses { get; set; }
        }
    }
}