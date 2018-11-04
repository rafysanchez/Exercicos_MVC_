using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sids.Prodesp.UI.Areas.PagamentoContaDer.Models;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Exceptions;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Controllers
{
    public class ArquivoRemessaController : PagamentoContaDerBaseController
    {
        private new readonly Usuario _userLoggedIn; // TODO remover // HACK
        public ArquivoRemessaController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();

            _filterItems = new ArquivoRemessaFiltroGridViewModel();
        }

        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ArquivoRemessa");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new ArquivoRemessa()));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }


        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ArquivoRemessa");
               var  _filterItems = Display(new ArquivoRemessa(), form);

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



        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("ArquivoRemessa");
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

        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new ArquivoRemessa(), true));
        }



        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));

        }


        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Alterar")]
        public ActionResult EditPrep(string id)
        {
            var objModel = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(id));


          var preparado =   App.ArquivoRemessaService.SelecionarPreparado(objModel, Convert.ToInt32(_funcId));


            return View("Visualizar", Display(preparado, false));

        }




        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(id));

            return View("CreateEdit", Display(objModel, true));
        }


        [PermissaoAcesso(Controller = typeof(ArquivoRemessaController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var objModel = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(id));
                if (objModel.StatusProdesp == "S")
                    throw new Exception("Arquivo Remessa transmitido não pode ser excluído");
                var acao = App.ArquivoRemessaService.Excluir(objModel, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                return Json(acao.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(ArquivoRemessa arquivoRemessa)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(arquivoRemessa, Convert.ToInt32(_funcId)) });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }


        private int SalvarService(ArquivoRemessa arquivoremessa, int funcionalidade)
        {
            var acao = arquivoremessa.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.ArquivoRemessaService.SalvarOuAlterar(
               arquivoremessa,
               funcionalidade,
                Convert.ToInt16(acao));

        }


        public JsonResult Transmitir(ArquivoRemessa arquivoRemessa)
        {
            ArquivoRemessa objModel;
            try
            {

                //ValidarData(arquivoRemessa.DataPreparacao,arquivoRemessa.DataPagamento);



                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                //preparacaoPagamento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                _modelId = SalvarService(arquivoRemessa, 0);
                App.ArquivoRemessaService.Transmitir(_modelId, usuario, (int)_funcId);
                objModel = App.ArquivoRemessaService.Selecionar(_modelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.ArquivoRemessaService.Selecionar(_modelId);

                var result = new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel };

                return Json(result);
            }
        }


        public JsonResult TransmitirCancelamentoOp(ArquivoRemessa arquivoRemessa)
        {
            ArquivoRemessa objModel;
            try
            {
                _modelId = arquivoRemessa.Id;

                App.ArquivoRemessaService.TransmitirCancelamentoOp(arquivoRemessa, (int)_funcId);


                    objModel = App.ArquivoRemessaService.Selecionar(_modelId);


                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
               
                    objModel = App.ArquivoRemessaService.Selecionar(_modelId);

             

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ArquivoRemessaService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });

            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }




        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Imprimir(ArquivoRemessa arquivoRemessa)
        //{
        //    try
        //    {

        //        var objModel = App.ArquivoRemessaService.ImprimirProdesp(arquivoRemessa);

        //        return Json(new { Status = "Sucesso", objModel });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Status = "Falha", Msg = ex.Message });
        //    }
        //}
        public JsonResult ConsultaImpressaoOpApoio(int id)
        {
            ArquivoRemessa objModel;
            try
            {
                objModel = App.ArquivoRemessaService.Selecionar(id);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.ArquivoRemessaService.Selecionar(_modelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImprimirOP(ArquivoRemessa arquivoRemessa)
        {
            try
            {

                var objModel = App.ArquivoRemessaService.ImprimirProdespOP(arquivoRemessa);

                return Json(new { Status = "Sucesso", objModel });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Imprimir(string id)
        //{
        //    try
        //    {
        //        var usuario = App.AutenticacaoService.GetUsuarioLogado();

        //        var arquivoRemessa = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(id));
        //        var objModel = App.ArquivoRemessaService.ImprimirProdesp(arquivoRemessa);

        //        return Json(new { Status = "Sucesso", objModel });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Status = "Falha", Msg = ex.Message });
        //    }
        //}



    }
}