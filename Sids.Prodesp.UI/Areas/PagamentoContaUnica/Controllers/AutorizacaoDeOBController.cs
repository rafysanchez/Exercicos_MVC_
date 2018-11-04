using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using Sids.Prodesp.UI.Security;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Microsoft.Reporting.WebForms;
using System.IO;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System.Globalization;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class AutorizacaoDeOBController : PagamentoContaUnicaController
    {
        public AutorizacaoDeOBController()
        {
            ViewBag.TiposPagamento = App.ProgramacaoDesembolsoExecucaoService.TiposPagamento();
        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "AutorizacaoDeOB");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new OBAutorizacao()));
                //return View("Index", Display(new OBAutorizacaoItem()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<FiltroGridViewModel> _filterOB = new List<FiltroGridViewModel>();
            try
            {
                App.PerfilService.SetCurrentFilter(form, "AutorizacaoDeOB");

                //_filterOB = Display(new OBAutorizacao(), form).ToList();
                _filterOB = Display(new OBAutorizacaoItem(), form).ToList();

                if (!_filterOB.Any())
                    ExibirMensagemErro("Registro não encontrado.");

                return View("Index", _filterOB);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _filterOB);
            }

        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            var model = new DadoAutorizacaoDeOBViewModel();

            //model.UGPagadora = App.RegionalService.Buscar(new Model.Entity.Seguranca.Regional() { Id = (int)_userLoggedIn.RegionalId }).FirstOrDefault().Uge;
            model.UGPagadora = "162184";
            model.GestaoPagadora = "16055";

            return View("CreateEdit", model);
        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Alterar")]
        public ActionResult Edit(string id, string numOB)
        {
            //var objModel = App.AutorizacaoDeOBService.Selecionar(Int32.Parse(id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
            var objModel = App.AutorizacaoDeOBService.Selecionar(Int32.Parse(id), numOB, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
            objModel.Confirmacao = App.ConfirmacaoPagamentoService.Selecionar(null, objModel.IdAutorizacaoOB);

            var vm = new DadoAutorizacaoDeOBViewModel(objModel);

            return View("CreateEdit", vm);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ConsultarListaOB(DadoAutorizacaoDeOBViewModel model)
        {
            try
            {

                var consultaOB = App.AutorizacaoDeOBService.ConsultarOB(_userLoggedIn, model.UGPagadora, model.GestaoPagadora, model.filtroListaOB.NumOB, model.filtroListaOB.Agrupamento);

                model.Items.Clear();

                foreach (var item in consultaOB)
                {
                    model.Items.Add(new DadoAutorizacaoDeOBItemViewModel(item, model, OrigemConsultaOB.ConsultaOB));
                }

                model.AnoOB = DateTime.Now.Year.ToString();
                model.IdAutorizacaoOB = model.Codigo == null ? 0 : model.IdAutorizacaoOB;
                model.AgrupamentoOB = model.Codigo == null ? 0 : model.AgrupamentoOB;

                return View("_ItemsAutorizacaoOB", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LimparListaItems(DadoAutorizacaoDeOBViewModel model)
        {
            if (model.Codigo == null)
            {
                model.Items = new List<DadoAutorizacaoDeOBItemViewModel>();
            }
            var view = PartialView("_ItemsAutorizacaoOB", model);
            var viewString = ConvertPartialViewToString(view);
            return Json(new { Status = "Sucesso", grid = viewString });
        }

        public ActionResult Retransmitir(int[] ListaDeOB, string filtroMudapah)
        {
            try
            {

                var entity = new OBAutorizacao();
                var entityItensDaOB = new OBAutorizacaoItem();

                foreach (var itemDaLista in ListaDeOB)
                {

                    entityItensDaOB.ItensSelecionados = App.AutorizacaoDeOBService.ConsultarItensDaOB(itemDaLista);

                    foreach (var item in entityItensDaOB.ItensSelecionados)
                    {
                        App.AutorizacaoDeOBService.AutorizarItemOB(_userLoggedIn, item, filtroMudapah);
                    }

                }
                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.AutorizacaoDeOBService.GetCurrentFilter("AutorizacaoDeOB");
                return filtro != null ? Index(filtro) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(DadoAutorizacaoDeOBViewModel model)
        {
            try
            {
                var entity = model.ToEntity();

                var checados = model.Items.Where(y => y.TransmitirCheckBox).Select(y => y.NumPD).ToList();

                Salvar(ref entity, checados);

                return Json(new { Status = "Sucesso", IdAutorizacaoOB = entity.IdAutorizacaoOB });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Erro ao salvar o registro.");
            }
        }

        private void Salvar(ref OBAutorizacao model, List<string> checados)
        {
            var acao = model.IdAutorizacaoOB > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            App.AutorizacaoDeOBService.Salvar(ref model, checados, Convert.ToInt32(_funcId), Convert.ToInt16(acao));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Transmitir(DadoAutorizacaoDeOBViewModel model)
        {
            model.Items = model.Items.OrderByDescending(x => x.TransmitirCheckBox).ToList();
            var entity = model.ToEntity();
            var viewString = "";
            var primeiraVez = model.IdExecucaoPD == 0;

            try
            {
                DeletarNaoAgrupados(entity);

                var marcados = model.Items.Where(y => y.TransmitirCheckBox).Select(y => y.NumOB).ToList();

                this.Salvar(ref entity, marcados);

                App.AutorizacaoDeOBService.AutorizarOB(entity, marcados, model, entity.UgPagadora, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Transmitir), _userLoggedIn);

                if (model.EhConfirmacaoPagamento == SimNao.Sim && entity.Items != null && entity.Items.Any())
                {
                    var dataConfirmacao = model.DataConfirmacao;
                    var tipoPagamento = model.TipoPagamento;

                    App.ConfirmacaoPagamentoService.TransmitirProdesp(entity, marcados, dataConfirmacao, tipoPagamento, primeiraVez, (int)_funcId);
                }
                else
                {
                    this.RelacionarAutorizacaoComPagamentoDesdobrado(entity, entity.Items, marcados);
                }

                entity = App.AutorizacaoDeOBService.Selecionar(entity.IdAutorizacaoOB.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
                viewString = ConvertPartialViewToString(PartialView("_ItemsAutorizacaoOB", new DadoAutorizacaoDeOBViewModel(entity)));

                return Json(new { Status = "Sucesso", grid = viewString });

            }
            catch (Exception ex)
            {
                entity = App.AutorizacaoDeOBService.Selecionar(entity.IdAutorizacaoOB.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
                viewString = ConvertPartialViewToString(PartialView("_ItemsAutorizacaoOB", new DadoAutorizacaoDeOBViewModel(entity)));
                var status = "Falha - " + ex.Message;

                return Json(new { Status = status, Id = entity.Id, grid = viewString });
            }
        }

        public void RelacionarAutorizacaoComPagamentoDesdobrado(OBAutorizacao entity, IEnumerable<OBAutorizacaoItem> entityItem, List<string> checados)
        {
            try
            {
                App.ConfirmacaoPagamentoService.RelacionarAutorizacaoComPagamentoDesdobrado(entity, entity.Items, checados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarNaoAgrupados(OBAutorizacao entity)
        {
            try
            {
                foreach (var item in entity.Items)
                {
                    DeletarNaoAgrupados(item.IdAutorizacaoOB.ToString());
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [PermissaoAcesso(Controller = typeof(AutorizacaoDeOBController), Operacao = "Excluir")]
        public ActionResult Delete(int Id)
        {
            try
            {
                //var autorizacaoOB = App.AutorizacaoDeOBService.Selecionar(int.Parse(Id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));
                App.AutorizacaoDeOBService.Deletar(Id, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                return Json("Exclusão efetuada com sucesso!.", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
        private ActionResult DeletarNaoAgrupados(string Id)
        {
            try
            {
                App.AutorizacaoDeOBService.DeletarNaoAgrupados(Convert.ToInt32(Id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                return Json("Execução excluída com sucesso!.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        private List<ConsultaOB> lista;

        private void PreencherListaOB(int Id, string numOb, string mudapah, string tipo)
        {

            var busca = App.AutorizacaoDeOBService.SelecionarItem(Id, numOb, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));

            lista = new List<ConsultaOB>();

            //Item unico
            if (tipo == "1")
            {
                var item = App.ProgramacaoDesembolsoExecucaoService.ConsultarOB(_userLoggedIn, mudapah, busca.GestaoPagadora, busca.NumOB, 0);
                var valorCulturaBr = Decimal.Parse(item.Valor).ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
                item.Valor = valorCulturaBr;
                item.Prioridade = item.Situacao2;
                lista.Add(item);
            }
            if (tipo == "2")
            {
                var autorizacao = App.AutorizacaoDeOBService.Selecionar(busca.IdAutorizacaoOB, _funcId.Value, (int)EnumAcao.Listar);
                var items = autorizacao.Items;

                foreach (var item in items)
                {
                    if (!string.IsNullOrWhiteSpace(item.NumOB))
                    {
                        var itemOB = App.AutorizacaoDeOBService.ConsultarOBForReport(_userLoggedIn, mudapah, busca.UG, item.NumOB, 0);
                        lista.Add(itemOB);
                    }
                }
            }
        }

        public ActionResult ImprimirOB(int Id, string numOb, string filtromudapah, string tipo = "1", string filetype = "PDF", string contenttype = "application/pdf")
        {
            try
            {
                this.PreencherListaOB(Id, numOb, filtromudapah, tipo);

                string RptPath = Server.MapPath("~/bin/Relatorios/PagamentoContaUnica/ExecucaoPD/rdl_exec_pd.rdlc");
                LocalReport rpt = new LocalReport();

                rpt.DataSources.Clear();
                rpt.DataSources.Add(new ReportDataSource("DataSet1", lista.AsEnumerable()));
                rpt.SubreportProcessing += new SubreportProcessingEventHandler(delegate (object o, SubreportProcessingEventArgs e)
                {
                    if (e.ReportPath == "rdl_exec_pd_obras")
                    {
                        var OB = e.Parameters["pOB"].Values[0].ToString();
                        var items = lista.Where(x => x.NumeroOB == OB).FirstOrDefault().AllObras().AsEnumerable();
                        e.DataSources.Add(new ReportDataSource("DataSet2", items));
                    }

                    if (e.ReportPath == "rdl_exec_pd_itens_liquidados")
                    {
                        var OB = e.Parameters["pOB"].Values[0].ToString();
                        var items = lista.FirstOrDefault(x => x.NumeroOB == OB).AllItensLiquidados().AsEnumerable();
                        e.DataSources.Add(new ReportDataSource("DataSet2", items));
                    }
                });

                rpt.ReportPath = RptPath;
                string filePath = Path.GetTempFileName();

                return HelperReport.ExportReport(rpt, filetype, contenttype);

            }
            catch (Exception ex)
            {
                base.ExibirMensagemErro(ex.ToString());
                return RedirectToAction("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OBPorNumero(DadoAutorizacaoDeOBViewModel model)
        {
            try
            {
                var tipo_consulta = Core.Services.PagamentoContaUnica.AutorizacaoDeOBService.TIPO_CONSULTA_PD.CONSULTAR;
                var consultapd = App.AutorizacaoDeOBService.ComplementarDadosOb(_userLoggedIn, model.UGLiquidante, model.GestaoLiquidante, model.UGPagadora, model.filtroAdicionarPd.NumeroPD, model.Codigo.GetValueOrDefault(), tipo_consulta);

                if (!model.Items.Select(x => x.NumPD).Contains(consultapd.NumOP ?? consultapd.NPD ?? consultapd.OB))
                {
                    model.Items.Add(new DadoAutorizacaoDeOBItemViewModel(consultapd, model, OrigemConsultaOB.ConsultaOB));
                }

                return Json(new { data = consultapd });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
    }
}
