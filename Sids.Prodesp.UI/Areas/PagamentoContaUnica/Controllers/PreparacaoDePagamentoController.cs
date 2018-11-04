using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class PreparacaoDePagamentoController : PagamentoContaUnicaController
    {
        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "PreparacaoPagamentoCreate");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new PreparacaoPagamento()));

            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }


        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "PreparacaoPagamentoCreate");
                var _filterItems = Display(new PreparacaoPagamento(), form);

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


        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new PreparacaoPagamento(), true));
        }


        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.PreparacaoPagamentoService.Selecionar(Convert.ToInt32(id));

            return View("CreateEdit", Display(objModel, true));
        }


        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.PreparacaoPagamentoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));

        }

        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.PreparacaoPagamentoService.Selecionar(Convert.ToInt32(id));
                if (objModel.TransmitidoProdesp)
                    throw new Exception("A Preparacao de PagamentoService transmitida não pode ser excluída");
                var acao = App.PreparacaoPagamentoService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                return Json(acao.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Imprimir(PreparacaoPagamento preparacaoPagamento)
        {
            try
            {

                var objModel = App.PreparacaoPagamentoService.ImprimirProdesp(preparacaoPagamento);

                return Json(new { Status = "Sucesso", objModel });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }


        public JsonResult ConsultaImpressaoOpApoio(int id)
        {
            PreparacaoPagamento objModel;
            try
            {
                objModel = App.PreparacaoPagamentoService.Selecionar(id);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.PreparacaoPagamentoService.Selecionar(ModelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }

        public JsonResult Transmitir(PreparacaoPagamento preparacaoPagamento)
        {
            PreparacaoPagamento objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                //preparacaoPagamento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                ModelId = SalvarService(preparacaoPagamento, 0);
                App.PreparacaoPagamentoService.Transmitir(ModelId, usuario, (int)_funcId);
                objModel = App.PreparacaoPagamentoService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.PreparacaoPagamentoService.Selecionar(ModelId);
                
                var result = new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel };

                return Json(result);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.PreparacaoPagamentoService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });

            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }



        [PermissaoAcesso(Controller = typeof(PreparacaoDePagamentoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("PreparacaoPagamentoCreate");
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
        public ActionResult Save(PreparacaoPagamento preparacaoPagamento)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(preparacaoPagamento, Convert.ToInt32(_funcId)) });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }


        private int SalvarService(PreparacaoPagamento preparacaoPagamento, int funcionalidade)
        {
            var acao = preparacaoPagamento.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.PreparacaoPagamentoService.SalvarOuAlterar(
               preparacaoPagamento,
               funcionalidade,
                Convert.ToInt16(acao));

        }


    }
}