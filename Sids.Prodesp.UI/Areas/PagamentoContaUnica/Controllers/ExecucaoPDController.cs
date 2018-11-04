using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.UI.Security;
using System.Web.UI.WebControls;
using Sids.Prodesp.UI.Report;
using System.IO;
using Microsoft.Reporting.WebForms;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System.Globalization;
using System.Threading;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class ExecucaoPDController : PagamentoContaUnicaController
    {

        public ExecucaoPDController()
        {
            ViewBag.TiposPagamento = App.ProgramacaoDesembolsoExecucaoService.TiposPagamento();
            ViewBag.TiposExecucao = App.ProgramacaoDesembolsoExecucaoService.TiposExecucao();

            var a = User;
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ExecucaoPD");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new PDExecucaoItem()));
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<FiltroGridViewModel> _filterItems = new List<FiltroGridViewModel>();
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ExecucaoPD");

                _filterItems = Display(new PDExecucaoItem(), form).ToList();

                if (!_filterItems.Any())
                    ExibirMensagemErro("Registro não encontrado.");

                return View("Index", _filterItems);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _filterItems);
            }

        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            var model = new DadoProgramacaoDesembolsoExecucaoViewModel();

            model.UGPagadora = App.RegionalService.Buscar(new Model.Entity.Seguranca.Regional() { Id = (int)_userLoggedIn.RegionalId }).FirstOrDefault().Uge;
            model.UGPagadora = "162184";
            model.GestaoPagadora = "16055";
            model.UGLiquidante = "162101";
            model.GestaoLiquidante = "16055";
            model.TransmitirSiafem = true;

            return View("CreateEdit", model);
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Incluir")]
        public ActionResult CreateThis(string id)
        {
            //  var objModel = App.ExecucaoPDService.Selecionar(Convert.ToInt32(id));
            // return View("CreateEdit", Display(objModel, true));
            return null;
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.ProgramacaoDesembolsoExecucaoService.Selecionar(Int32.Parse(id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
            objModel.Confirmacao = App.ConfirmacaoPagamentoService.Selecionar(objModel.IdExecucaoPD, null);

            return View("CreateEdit", new DadoProgramacaoDesembolsoExecucaoViewModel(objModel));
        }

        public void DeletarNaoAgrupados(PDExecucao entity)
        {
            try
            {
                foreach (var item in entity.Items)
                {
                    Delete(item.id_execucao_pd.ToString(), 2);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Excluir")]
        public ActionResult Delete(string Id, int tipo)
        {
            try
            {
                if (tipo == 1)
                {

                    App.ProgramacaoDesembolsoExecucaoService.DeletarItem(Convert.ToInt32(Id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                    return Json("Execução excluída com sucesso!.", JsonRequestBehavior.AllowGet);
                }
                else if (tipo == 2)
                {
                    App.ProgramacaoDesembolsoExecucaoService.DeletarNaoAgrupados(Convert.ToInt32(Id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                    return Json("Execução excluída com sucesso!.", JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var execucaoPD = App.ProgramacaoDesembolsoExecucaoService.Selecionar(int.Parse(Id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));
                    App.ProgramacaoDesembolsoExecucaoService.Deletar(execucaoPD.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                    return Json("Execução excluída com sucesso!.", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Imprimir(string id, string tipo)
        {
            try
            {
                //var usuario = App.AutenticacaoService.GetUsuarioLogado();
                //var execucaoPd = App.ExecucaoPdService.Selecionar(Convert.ToInt32(id));
                //var consulta = App.ExecucaoPdService.Consulta(usuario, execucaoPd.Codigo, .CodigoGestao, reclassificacaoRetencao.NumeroSiafem);
                //Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfReclassificacaoRetencao(consulta, "Programação Desembolso Execução", execucaoPd);

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }
        public ActionResult ImprimirOB(int Id, string filtromudapah, string tipo = "1", string filetype = "PDF", string contenttype = "application/pdf")
        {
            try
            {
                PreencherListaOB(Id, filtromudapah, tipo);

                string rptPath = Server.MapPath("~/bin/Relatorios/PagamentoContaUnica/ExecucaoPD/rdl_exec_pd.rdlc");
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

                rpt.ReportPath = rptPath;

                return HelperReport.ExportReport(rpt, filetype, contenttype);

            }
            catch (Exception ex)
            {
                base.ExibirMensagemErro(ex.ToString());
                return RedirectToAction("Index");
            }
        }

        private List<ConsultaOB> lista;

        private void PreencherListaOB(int Id, string mudapah, string tipo)
        {

            var busca = App.ProgramacaoDesembolsoExecucaoService.SelecionarItem(Id, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));

            lista = new List<ConsultaOB>();

            //Item unico
            if (tipo == "1")
            {
                var item = App.ProgramacaoDesembolsoExecucaoService.ConsultarOB(_userLoggedIn, mudapah, busca.Gestao, busca.NumOBItem, 0);
                var valorCulturaBr = Decimal.Parse(item.Valor).ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
                item.Valor = valorCulturaBr;
                item.Prioridade = item.Situacao2;
                lista.Add(item);
            }

            if (tipo == "2")
            {
                var execucao = App.ProgramacaoDesembolsoExecucaoService.Selecionar(busca.id_execucao_pd, 0, 0);
                var items = execucao.Items;

                foreach (var item in items)
                {
                    if (!string.IsNullOrWhiteSpace(item.NumOBItem))
                    {
                        var itemOB = App.ProgramacaoDesembolsoExecucaoService.ConsultarOB(_userLoggedIn, mudapah, busca.Gestao, item.NumOBItem, 0);
                        lista.Add(itemOB);
                    }
                }
            }
        }

        [PermissaoAcesso(Controller = typeof(ExecucaoPDController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ProgramacaoDesembolsoExecucaoService.GetCurrentFilter("ExecucaoPD");
                return filtro != null ? Index(filtro) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            var viewString = "";
            try
            {
                var entity = model.ToEntity();

                var checados = model.Items.Where(y => y.TransmitirCheckBox).Select(y => y.NumPD).ToList();

                Salvar(ref entity, checados);

                //entity = App.ProgramacaoDesembolsoExecucaoService.Selecionar(entity.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
                //viewString = ConvertPartialViewToString(PartialView("_ItemsExecucaoPD", new DadoProgramacaoDesembolsoExecucaoViewModel(entity)));

                return Json(new { Status = "Sucesso", AgrupamentoItemPD = entity.IdExecucaoPD });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Erro ao salvar o registro.");
            }
        }
        private void Salvar(ref PDExecucao model, List<string> checados)
        {
            var acao = model.IdExecucaoPD > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            App.ProgramacaoDesembolsoExecucaoService.Salvar(ref model, checados, Convert.ToInt32(_funcId), Convert.ToInt16(acao));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Transmitir(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            model.Items = model.Items.OrderByDescending(x => x.TransmitirCheckBox).ToList();
            var entity = model.ToEntity();
            var viewString = "";
            var primeiraVez = model.IdExecucaoPD == 0;

            try
            {
                DeletarNaoAgrupados(entity);

                var marcados = model.Items.Where(y => y.TransmitirCheckBox).Select(y => y.NumPD).ToList();

                this.Salvar(ref entity, marcados);



                App.ProgramacaoDesembolsoExecucaoService.ExecutarPD(entity, entity.UgPagadora, marcados, _userLoggedIn, (int)_funcId);

                if (model.EhConfirmacaoPagamento == SimNao.Sim && entity.Items != null && entity.Items.Any())
                {
                    var dataConfirmacao = model.confirmacaoPagamento.DataConfirmacao;
                    var tipoPagamento = model.confirmacaoPagamento.TipoPagamento;

                    App.ConfirmacaoPagamentoService.TransmitirProdesp(entity, marcados, dataConfirmacao, tipoPagamento, primeiraVez, (int)_funcId);
                }
                else
                {
                    this.RelacionarExecucaoComPagamentoDesdobrado(entity, entity.Items, marcados, primeiraVez);
                }

                entity = App.ProgramacaoDesembolsoExecucaoService.Selecionar(entity.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
                viewString = ConvertPartialViewToString(PartialView("_ItemsExecucaoPD", new DadoProgramacaoDesembolsoExecucaoViewModel(entity)));

                return Json(new { Status = "Sucesso", grid = viewString });

            }
            catch (Exception ex)
            {
                entity = App.ProgramacaoDesembolsoExecucaoService.Selecionar(entity.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
                viewString = ConvertPartialViewToString(PartialView("_ItemsExecucaoPD", new DadoProgramacaoDesembolsoExecucaoViewModel(entity)));
                var status = "Falha - " + ex.Message;

                return Json(new { Status = status, grid = viewString });
            }

        }

        public void RelacionarExecucaoComPagamentoDesdobrado(PDExecucao entity, IEnumerable<PDExecucaoItem> entityItem, List<string> checados, bool primeiraVez)
        {
            try
            {
                App.ConfirmacaoPagamentoService.RelacionarExecucaoComPagamentoDesdobrado(entity, entity.Items, checados, primeiraVez);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ActionResult Retrasmitir(int[] Codigos, string filtroMudapah)
        public ActionResult Retrasmitir(int[] Codigos, string filtroMudapah)
        {
            //var entity = model.ToEntity();
            try
            {
                //var checados = model.Items.Where(y => y.TransmitirCheckBox).Select(y => y.NumPD).ToArray();

                //App.ProgramacaoDesembolsoExecucaoService.ExecutarPD(entity, entity.UgPagadora, Codigos, (int)_funcId);

                foreach (var item in Codigos)
                {
                    var execucaoItem = App.ProgramacaoDesembolsoExecucaoService.SelecionarItem(item, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));
                    if (execucaoItem != null)
                    {
                        App.ProgramacaoDesembolsoExecucaoService.ExecutarItemPD(_userLoggedIn, execucaoItem, filtroMudapah);
                    }
                }

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AdicionarPD(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            try
            {
                var consultapd = App.ProgramacaoDesembolsoExecucaoService.ComplementarDadosPd(_userLoggedIn, model.UGLiquidante, model.GestaoLiquidante, model.UGPagadora, model.filtroAdicionarPd.NumeroPD, model.Codigo.GetValueOrDefault());

                if (!model.Items.Select(x => x.NumPD).ToArray().Contains(consultapd.NumPD ?? consultapd.NPD ?? consultapd.PD))
                {
                    model.Items.Add(new DadoProgramacaoDesembolsoExecucaoItemViewModel(consultapd, model, OrigemConsultaPd.ConsultaPD));
                }

                var consultapdDesdobrada = App.ProgramacaoDesembolsoExecucaoService.AdicionarPDDesdobrada(model.filtroAdicionarPd.NumeroPD);

                foreach (var item in consultapdDesdobrada)
                {
                    consultapd = App.ProgramacaoDesembolsoExecucaoService.ComplementarDadosPd(_userLoggedIn, model.UGLiquidante, model.GestaoLiquidante, model.UGPagadora, item.NumPD, model.Codigo.GetValueOrDefault());
                    model.Items.Add(new DadoProgramacaoDesembolsoExecucaoItemViewModel(consultapd, model, OrigemConsultaPd.ConsultaPD));
                }

                foreach (var item in model.Items)
                {
                    item.SiafemStatus = "N";
                    item.SiafemDescricao = null;

                    item.ProdespStatus = "N";
                    item.ProdespDescricao = null;

                }


                return View("_ItemsExecucaoPD", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PDPorNumero(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            try
            {
                var tipo_consulta = Core.Services.PagamentoContaUnica.ProgramacaoDesembolsoExecucaoService.TIPO_CONSULTA_PD.CONSULTAR;
                var consultapd = App.ProgramacaoDesembolsoExecucaoService.ComplementarDadosPd(_userLoggedIn, model.UGLiquidante, model.GestaoLiquidante, model.UGPagadora, model.filtroAdicionarPd.NumeroPD, model.Codigo.GetValueOrDefault(), tipo_consulta);

                if (!model.Items.Select(x => x.NumPD).ToArray().Contains(consultapd.NumPD ?? consultapd.NPD ?? consultapd.PD))
                {
                    model.Items.Add(new DadoProgramacaoDesembolsoExecucaoItemViewModel(consultapd, model, OrigemConsultaPd.ConsultaPD));
                }

                return Json(new { data = consultapd });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ConsultarListaPD(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            try
            {

                var d1 = DateTime.Parse(model.filtroListaPd.DataInicial);
                var d2 = DateTime.Parse(model.filtroListaPd.DataFinal);

                model.filtroListaPd.DataInicial = d1.ToString("dd") + d1.ToString("MMMM").Substring(0, 3);
                model.filtroListaPd.DataFinal = d2.ToString("dd") + d2.ToString("MMMM").Substring(0, 3);

                var consultapd = App.ProgramacaoDesembolsoExecucaoService.ConsultarPD(_userLoggedIn, model.Codigo.GetValueOrDefault(), model.UGLiquidante, model.GestaoLiquidante, model.UGPagadora, model.GestaoPagadora, "", model.filtroListaPd.DataInicial, model.filtroListaPd.DataFinal, d1.Year.ToString(), d2.Year.ToString(), model.filtroListaPd.Opcao, "", "");

                model.Items.Clear();

                foreach (var item in consultapd)
                {
                    model.Items.Add(new DadoProgramacaoDesembolsoExecucaoItemViewModel(item, model, OrigemConsultaPd.ConsultaPD));
                }

                return View("_ItemsExecucaoPD", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LimparListaItems(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {
            if (model.Codigo == null)
            {
                model.Items = new List<DadoProgramacaoDesembolsoExecucaoItemViewModel>();
            }
            var view = PartialView("_ItemsExecucaoPD", model);
            var viewString = ConvertPartialViewToString(view);
            return Json(new { Status = "Sucesso", grid = viewString });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ExcluirItem(DadoProgramacaoDesembolsoExecucaoViewModel model)
        {

            //var entity = new PDExecucao();
            //var viewString = "";

            //var view = PartialView("_ItemsExecucaoPD", model);
            //var viewString = ConvertPartialViewToString(view);
            //return Json(new { Status = "Sucesso", grid = viewString });



            //entity = App.ProgramacaoDesembolsoExecucaoService.Selecionar(entity.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));
            var objModel = App.ProgramacaoDesembolsoExecucaoService.Selecionar(model.IdExecucaoPD.GetValueOrDefault(), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Alterar));

            //model.Items = model.Items.Where(x => x.TransmitirCheckBox).ToList();
            var codigoItemExcluido = model.Items.FirstOrDefault(x => x.TransmitirCheckBox).Codigo;

            model.Items.Clear();
            LimparListaItems(model);

            //if (codigoItemExcluido != null)
            //{
            //    model.Items.Remove(item);
            //}

            objModel.Items = objModel.Items.Where(x => x.Codigo != codigoItemExcluido).ToList();

            //entity = model.ToEntity();

            return View("CreateEdit", new DadoProgramacaoDesembolsoExecucaoViewModel(objModel));

            //viewString = ConvertPartialViewToString(PartialView("_ItemsExecucaoPD", new DadoProgramacaoDesembolsoExecucaoViewModel(entity)));

            //return Json(new { Status = "Sucesso", grid = viewString });

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CancelarOB(string mudapah, string OB, string gestao, string causa)
        {
            try
            {
                App.ProgramacaoDesembolsoExecucaoService.CancelarOB(mudapah, gestao, OB, causa, null, _userLoggedIn);

                //var items = App.ProgramacaoDesembolsoExecucaoService.ConsultarItems(new PDExecucaoItem() { NumOB = OB }, null, null, null);
                var items = App.ProgramacaoDesembolsoExecucaoService.ConsultarItems(new PDExecucaoItem() { NumOBItem = OB }, null, null, null);

                foreach (var item in items)
                {
                    item.OBCancelada = true;
                    item.CausaCancelamento = causa;
                    App.ProgramacaoDesembolsoExecucaoService.SalvarItem(item);
                }

                return Json(new { Status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
    }
}