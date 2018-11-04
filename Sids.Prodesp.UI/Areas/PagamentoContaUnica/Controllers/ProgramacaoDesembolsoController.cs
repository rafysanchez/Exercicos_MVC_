using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using Sids.Prodesp.UI.Report;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class ProgramacaoDesembolsoController : PagamentoContaUnicaController
    {
        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ProgramacaoDesembolso");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new ProgramacaoDesembolso()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ProgramacaoDesembolso");
                var _filterItems = Display(new ProgramacaoDesembolso(), form);

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


        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new ProgramacaoDesembolso(), true));

        }

        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            var objModel = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, true));
        }


        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Alterar")]
        public ActionResult EditByList(string id, int tipoId, string tipo)
        {
            ProgramacaoDesembolso objModel = new ProgramacaoDesembolso();
            objModel = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));

            objModel.TipoBloqueio = (tipo == "c") ? 1 : 0;

            return View("CreateEdit", Display(objModel, false));

        }


        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {

            var objModel = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
            return View("CreateEdit", Display(objModel, false));

        }

        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Excluir")]
        public ActionResult Delete(string id, int tipo,string agrupamentoId,string unica)
        {
            try
            {
               AcaoEfetuada acao;
               IEnumerable<ProgramacaoDesembolsoAgrupamento> programacaoDesembolsoAgrupamento;


                if (tipo == 1 || tipo == 3)
                {
                    var programacaoDesembolso = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
                    acao = App.ProgramacaoDesembolsoService.Excluir(programacaoDesembolso, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                }
                else
                {
                    if (id == agrupamentoId)
                    {
                        var programacaoDesembolso = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
                        acao = App.ProgramacaoDesembolsoService.Excluir(programacaoDesembolso, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                        return Json(acao.ToString(), JsonRequestBehavior.AllowGet);
                    }
                   
 
                    if(unica == "sim")
                    {
                        
                        programacaoDesembolsoAgrupamento = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = Convert.ToInt32(agrupamentoId) });
                        acao = App.ProgramacaoDesembolsoAgrupamentoService.Excluir(programacaoDesembolsoAgrupamento, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                        programacaoDesembolsoAgrupamento = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = Convert.ToInt32(id) });

                        if(!programacaoDesembolsoAgrupamento.Any())
                        {
                            var programacaoDesembolso = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
                            acao = App.ProgramacaoDesembolsoService.Excluir(programacaoDesembolso, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                        }

                    }
                    else
                    {

                        programacaoDesembolsoAgrupamento = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = Convert.ToInt32(id) });
                        acao = App.ProgramacaoDesembolsoAgrupamentoService.Excluir(programacaoDesembolsoAgrupamento, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));

                        if (acao == AcaoEfetuada.Sucesso)
                        {
                            var programacaoDesembolso = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
                            acao = App.ProgramacaoDesembolsoService.Excluir(programacaoDesembolso, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                        }

                    }

                }

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
                ConsultaPd consultaPd = new ConsultaPd();
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var regional = App.RegionalService.Buscar(new Model.Entity.Seguranca.Regional() { Id = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId }).FirstOrDefault();

                var pd = App.ProgramacaoDesembolsoService.Selecionar(Convert.ToInt32(id));
                if (pd.ProgramacaoDesembolsoTipoId == 2)
                {
                    consultaPd = App.SubempenhoService.ConsultaPD(usuario, pd.Agrupamentos.FirstOrDefault()?.CodigoUnidadeGestora, pd.Agrupamentos.FirstOrDefault()?.CodigoGestao, pd.Agrupamentos.FirstOrDefault()?.NumeroSiafem);
                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfProgramacaoDesembolsoAgrupamento(consultaPd, "Programação Desembolso", pd);
                }
                else
                {
                    consultaPd = App.SubempenhoService.ConsultaPD(usuario, regional.Uge, "16055", pd.NumeroSiafem);
                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfProgramacaoDesembolso(consultaPd, "Programação Desembolso", pd);
                }

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImprimirLista(string id, int tipo, bool list)
        {
            try
            {
                IProgramacaoDesembolso pd;
                ConsultaPd consultaPd;

                var usuario     = App.AutenticacaoService.GetUsuarioLogado();
               
                if (list)
                {
                    var agrup = App.ProgramacaoDesembolsoAgrupamentoService.Selecionar(Convert.ToInt32(id));
                    pd = App.ProgramacaoDesembolsoService.Selecionar(agrup.PagamentoContaUnicaId);
                    consultaPd = App.SubempenhoService.ConsultaPD(usuario, agrup.CodigoUnidadeGestora, agrup.CodigoGestao, agrup.NumeroSiafem);
                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfProgramacaoDesembolsoAgrupamento(consultaPd, "Programação Desembolso", pd as ProgramacaoDesembolso);
                }
                else
                {
                    pd = App.ProgramacaoDesembolsoAgrupamentoService.Selecionar(Convert.ToInt32(id));
                    consultaPd = App.SubempenhoService.ConsultaPD(usuario, pd.CodigoUnidadeGestora, pd.CodigoGestao, pd.NumeroSiafem);
                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfProgramacaoDesembolso(consultaPd, "Programação Desembolso", pd);
                }



                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        public JsonResult Transmitir(ProgramacaoDesembolso programacaoDesembolso)
        {
            ProgramacaoDesembolso objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                programacaoDesembolso.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                ModelId = SalvarService(programacaoDesembolso, 0);
                App.ProgramacaoDesembolsoService.Transmitir(ModelId, usuario, (int)_funcId);
                objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }


        public JsonResult TransmitirCancelamentoOp(ProgramacaoDesembolso programacaoDesembolso)
        {
            IProgramacaoDesembolso objModel;
            try
            {
                ModelId = programacaoDesembolso.Id;

                App.ProgramacaoDesembolsoService.TransmitirCancelamentoOp(programacaoDesembolso, (int)_funcId);

                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }

        public JsonResult TransmitirBoqureioOp(ProgramacaoDesembolso programacaoDesembolso)
        {
            IProgramacaoDesembolso objModel;
            try
            {
                ModelId = programacaoDesembolso.Id;

                App.ProgramacaoDesembolsoService.TransmitirBoqueioOp(programacaoDesembolso, (int)_funcId);

                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }
        public JsonResult TransmitirDesbloqueioOp(ProgramacaoDesembolso programacaoDesembolso)
        {
            IProgramacaoDesembolso objModel;
            try
            {
                ModelId = programacaoDesembolso.Id;

                App.ProgramacaoDesembolsoService.TransmitirDesbloqueioOp(programacaoDesembolso, (int)_funcId);

                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 1 || programacaoDesembolso.ProgramacaoDesembolsoTipoId == 3)
                    objModel = App.ProgramacaoDesembolsoService.Selecionar(ModelId);
                else
                    objModel = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = ModelId }).FirstOrDefault();

                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });

            }
        }

        public JsonResult TransmitirCancelamento(ProgramacaoDesembolso programacaoDesembolso)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                App.ProgramacaoDesembolsoService.TransmitirCancelamento(programacaoDesembolso, usuario, (int)_funcId);

                return Json(new { Status = "Sucesso" });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ProgramacaoDesembolsoService.Transmitir(ids, usuario, (int)_funcId);
                return Json(new { Status = string.IsNullOrWhiteSpace(msg) ? "Sucesso" : "Falha", Msg = msg });

            }
            catch (Exception ex) { return Json(new { Status = "Falha", Msg = ex.Message }); }
        }

        [PermissaoAcesso(Controller = typeof(ProgramacaoDesembolsoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramaService.GetCurrentFilter("ProgramacaoDesembolso");
                return filtro != null
                    ? Index(filtro)
                    : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(ProgramacaoDesembolso programacaoDesembolso)
        {
            try
            {
                return Json(new { Status = "Sucesso", Id = SalvarService(programacaoDesembolso, Convert.ToInt32(_funcId)) });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int SalvarService(ProgramacaoDesembolso programacaoDesembolso, int funcionalidade)
        {
            var acao = programacaoDesembolso.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.ProgramacaoDesembolsoService.SalvarOuAlterar(
               programacaoDesembolso,
               funcionalidade,
                Convert.ToInt16(acao));

        }

    }
}