using System;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Security;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;
using System.Linq;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.Enum;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class ImpressaoRelacaoRERTController : PagamentoContaUnicaController
    {
        private List<RespostaImpressaoRelacaoReRt> respostaImpressaoReRt;

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "ImpressaoRelacaoInscricao");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

                return View("Index", Display(new ImpressaoRelacaoReRtConsultaVo()));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "ImpressaoRelacaoInscricao");
                var _filterItems = Display(new ImpressaoRelacaoReRtConsultaVo(), form);

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

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.ImpressaoRelacaoRERTService.GetCurrentFilter("ImpressaoRelacaoRERT");
                return filtro != null ? Index(filtro) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
        }

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            return View("CreateEdit", Display(new DadoImpressaoRelacaoReRtConsultaViewModel(), true));
        }

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.ImpressaoRelacaoRERTService.Selecionar(Convert.ToInt32(id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));

            var obj = Display(objModel, false);

            return View("CreateEdit", ViewBag.PesquisaImpressaoRelacaoRERTPaiVo);
        }

        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                App.ImpressaoRelacaoRERTService.Excluir(Convert.ToInt32(id), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                return Json("Exclusão efetuada com sucesso!.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Cancelar")]
        public ActionResult CancelarImpressaoRelacaoReRt(int id, string unidadeGestora, string gestao, string prefixoREouRT, string numREouRT, int flagCancelamento)
        {
            try
            {
                var objModel = App.ImpressaoRelacaoRERTService.CancelarImpressaoRelacaoReRt(unidadeGestora, gestao, prefixoREouRT, numREouRT, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                var obj = App.ImpressaoRelacaoRERTService.GetPorId(id, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                obj.FlagCancelamentoRERT = Convert.ToBoolean(flagCancelamento);
                App.ImpressaoRelacaoRERTService.SalvarOuAlterar(obj, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Ativar));

                return Json(new { Status = "Sucesso", objModel });
            }
            catch (Exception ex)
            {
                var obj = App.ImpressaoRelacaoRERTService.GetPorId(id, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                obj.MsgRetornoTransmissaoSiafem = ex.Message;
                App.ImpressaoRelacaoRERTService.SalvarOuAlterar(obj, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Ativar));
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Incluir")]
        public ActionResult Transmitir(string unidadeGestora, string gestao, string banco, string dataSolicitacao, string numeroRelatorio, string mantemAgrupamento)
        {
            try
            {
                string Titulo = "", Mensagem = "";
                var objModel = App.ImpressaoRelacaoRERTService.TransmitirImpressaoRelacaoReRt(unidadeGestora, gestao, banco, dataSolicitacao, numeroRelatorio, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Transmitir), Convert.ToString(Session["ChamarApenasRT"]));

                if (mantemAgrupamento == "")
                {
                    Session["Agrupamento"] = App.ImpressaoRelacaoRERTService.GetNumeroAgrupamento();
                }

                // Verifica se chamou uma vez a RT para chamar apenas o webService do RT
                if (objModel.Relob?.Substring(4, 2) == "RT")
                {
                    Session["ChamarApenasRT"] = "RT";
                }

                // Grava um "OK" para identificar que passou uma vez na RE para validar a condição da mensagem de "Sem Registros RE e RT"
                if (objModel.Relob?.Substring(4, 2) == "RE" && objModel.MsgRetorno == null)
                {
                    Session["GravouUmaRE"] = "OK";
                }
                else if (Session["GravouUmaRE"] == "OK" && objModel.MsgRetorno?.Substring(0, 6) == "(0203)")
                {
                    Session["GravouUmaRE"] = "";
                }

                //if (string.IsNullOrWhiteSpace(objModel.MsgRetorno) && string.IsNullOrWhiteSpace(objModel.MsgErro))
                if ((objModel.MsgRetorno?.Substring(0, 6) != "(0203)" || objModel.MsgRetorno != "HA SOMENTE RT" || objModel.MsgErro != null) && Session["GravouUmaRE"] != "")
                {
                    var obj = new ImpressaoRelacaoRERT();
                    obj.CodigoRelacaoRERT = objModel.Relob;
                    obj.CodigoOB = "";
                    obj.CodigoRelatorio = objModel.CodigoRelatorio;
                    obj.NumeroAgrupamento = Convert.ToInt32(Session["Agrupamento"]);
                    obj.CodigoUnidadeGestora = string.IsNullOrWhiteSpace(objModel.UnidadeGestora) ? unidadeGestora : objModel.UnidadeGestora;
                    obj.NomeUnidadeGestora = objModel.Relob?.Substring(4, 2) == "RE" ? objModel.NomeDaUnidadeGestoraRE : objModel.NomeDaUnidadeGestoraRT;
                    obj.CodigoGestao = string.IsNullOrWhiteSpace(objModel.Gestao) ? gestao : objModel.Gestao;
                    obj.NomeGestao = objModel.Relob?.Substring(4, 2) == "RE" ? objModel.NomeDaGestaoRE : objModel.NomeDaGestaoRT;
                    obj.CodigoBanco = string.IsNullOrWhiteSpace(objModel.Banco) ? banco : objModel.Banco;
                    obj.NomeBanco = objModel.Relob?.Substring(4, 2) == "RE" ? objModel.NomeDoBancoRE : objModel.NomeDoBancoRT;
                    obj.TextoAutorizacao = objModel.TextoAutorizacao;
                    obj.Cidade = objModel.Cidade;
                    obj.NomeGestorFinanceiro = objModel.NomeGestorFinanceiro;
                    obj.NomeOrdenadorAssinatura = objModel.NomeOrdenadorAssinatura;
                    obj.DataSolicitacao = string.IsNullOrWhiteSpace(dataSolicitacao) ? DateTime.Now : DateTime.ParseExact(dataSolicitacao, "ddMMyyyy", CultureInfo.InvariantCulture);
                    obj.DataReferencia = string.IsNullOrWhiteSpace(objModel.DataReferencia) ? default(DateTime) : DateTime.ParseExact(objModel.DataReferencia, "ddMMyyyy", CultureInfo.InvariantCulture);
                    obj.DataCadastramento = DateTime.Now;
                    obj.DataEmissao = string.IsNullOrWhiteSpace(objModel.DataEmissao) ? default(DateTime) : DateTime.ParseExact(objModel.DataEmissao, "ddMMyyyy", CultureInfo.InvariantCulture);
                    obj.ValorTotalDocumento = objModel.ValorTotalDocumento / 100; // Divide o valor que retorna do Mainframe
                    obj.ValorExtenso = objModel.ValorPorExtenso;
                    obj.FlagTransmitidoSiafem = objModel.StatusOperacao ? true : false;
                    obj.FlagTransmitirSiafem = true;
                    obj.DataTransmissaoSiafem = DateTime.Now;
                    obj.StatusSiafem = (string.IsNullOrWhiteSpace(objModel.MsgRetorno) && string.IsNullOrWhiteSpace(objModel.MsgErro)) ? "S" : "E";
                    obj.MsgRetornoTransmissaoSiafem = objModel.MsgErro != null ? objModel.MsgErro : null;
                    obj.FlagCancelamentoRERT = false;
                    obj.Agencia = objModel.Agencia;
                    obj.NomeAgencia = objModel.NomeDaAgencia;
                    obj.NumeroConta = objModel.ContaC;

                    App.ImpressaoRelacaoRERTService.SalvarOuAlterar(obj, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Inserir));
                }

                // Inclusão de OBs
                if (objModel.Relob?.Substring(4, 2) == "RE")
                {
                    var objListaRE = new ListaRE();
                    foreach (var re in objModel.RepeticoesReRt.ListaOB)
                    {
                        objListaRE.NumeroRE = objModel.Relob;
                        objListaRE.NumeroOB = re.Numero;
                        objListaRE.FlagPrioridade = re.IndPrioridade;
                        objListaRE.TipoOB = Convert.ToInt32(re.TipoOb);
                        objListaRE.NomeFavorecido = re.NomeDoFavorecido;
                        objListaRE.BancoFavorecido = re.BancoFavorecido;
                        objListaRE.AgenciaFavorecida = re.AgenciaFavorecido;
                        objListaRE.ContaFavorecida = re.ContaFavorecido;
                        objListaRE.ValorOB = re.ValorOb / 100; // Divide o valor que retorna do Mainframe

                        Mensagem = "Gerado relação: <b>" + objModel.Relob + "</b><br><br>Deseja continuar a geração?";
                        Titulo = "Geração RE/RT";

                        App.ImpressaoRelacaoRERTService.SalvarOuAlterarRe(objListaRE, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Inserir));
                    }
                }
                else if (objModel.Relob?.Substring(4, 2) == "RT")
                {
                    var objListaRT = new ListaRT();
                    foreach (var rt in objModel.RepeticoesReRt.ListaOB)
                    {
                        objListaRT.NumeroRT = objModel.Relob;
                        objListaRT.NumeroOB = rt.Numero;
                        objListaRT.ContaBancariaEmitente = rt.ContaBancariaEmitente;
                        objListaRT.UnidadeGestoraFavorecida = rt.UnidadeGestoraFavorecida;
                        objListaRT.GestaoFavorecida = rt.GestaoFavorecida;
                        objListaRT.MnemonicoUfFavorecida = rt.MnemonicoUgFavorecida;
                        objListaRT.BancoFavorecido = rt.BancoFavorecido;
                        objListaRT.AgenciaFavorecida = rt.AgenciaFavorecido;
                        objListaRT.ContaFavorecida = rt.ContaFavorecido;
                        objListaRT.ValorOB = rt.ValorOb / 100; // Divide o valor que retorna do Mainframe

                        Mensagem = "Gerado relação: <b>" + objModel.Relob + "</b><br><br>Deseja continuar a geração?";
                        Titulo = "Geração RE/RT";

                        App.ImpressaoRelacaoRERTService.SalvarOuAlterarRt(objListaRT, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Inserir));
                    }
                }
                else if (objModel.SemReRt == "Sem Registros RE e RT" && mantemAgrupamento == "")
                {
                    Titulo = "Transmitido";
                    Mensagem = "Não existe documento pendente para emissão de RE e RT";
                }
                else if (objModel.MsgRetorno?.Substring(0, 6) == "(0203)")
                {
                    Titulo = "Transmitido";
                    Mensagem = "Não existe documento pendente para emissão.";
                }
                else
                {
                    Titulo = "Erro";
                    Mensagem = string.IsNullOrWhiteSpace(objModel.MsgRetorno) ? objModel.MsgErro : objModel.MsgRetorno;
                }
                return Json(new
                {
                    Status = "Sucesso",
                    CodigoReRt = string.IsNullOrWhiteSpace(objModel.Relob) ? "" : objModel.Relob,
                    Mensagem = Mensagem,
                    Titulo = Titulo,
                    Agrupamento = Session["Agrupamento"],
                    UnidadeGestora = unidadeGestora,
                    Gestao = gestao,
                    Banco = banco
                });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [PermissaoAcesso(Controller = typeof(ImpressaoRelacaoRERTController), Operacao = "Listar")]
        public ActionResult CreateListar(string agrupamento, string ug, string gestao, string banco)
        {
            try
            {
                var objModel = App.ImpressaoRelacaoRERTService.SelecionarPorAgrupamento(Convert.ToInt32(agrupamento), ug, gestao, banco,
                    Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));

                Display(objModel, false);

                return View("CreateEdit", ViewBag.PesquisaImpressaoRelacaoRERTPaiVo);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
        public ActionResult ListarReRtPorAgrupamento(int codigoAgrupamento)
        {
            try
            {
                var objlistaObAgrupamento = App.ImpressaoRelacaoRERTService.ListarCodigoReRtPorAgrupamento(Convert.ToInt32(codigoAgrupamento), Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar));

                return Json(objlistaObAgrupamento, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        public ActionResult ImprimirImpressaoRelacao(string codigoUnidadeGestora, string codigoGestao, string codigoBanco, string dataSolicitacao, 
                                    string numeroRelatorio, string filetype = "PDF", string contenttype = "application/pdf")
        {
            try
            {
                var objlistaOB = PreencherListaOB(codigoUnidadeGestora, codigoGestao, codigoBanco, dataSolicitacao, numeroRelatorio);
                if (objlistaOB.RepeticoesReRt == null)
                {
                    throw new Exception(numeroRelatorio + ": " + objlistaOB.MsgRetorno);
                }

                    string rptPath;

                if (numeroRelatorio?.Substring(4, 2) == "RE")
                {
                    rptPath = Server.MapPath("~/bin/Relatorios/PagamentoContaUnica/ImpressaoRelacaoRERT/rdl_impressao_relacao_re.rdlc");
                }
                else
                {
                    rptPath = Server.MapPath("~/bin/Relatorios/PagamentoContaUnica/ImpressaoRelacaoRERT/rdl_impressao_relacao_rt.rdlc");
                }

                LocalReport rpt = new LocalReport();

                rpt.DataSources.Clear();
                rpt.DataSources.Add(new ReportDataSource("DataSetImpressaoRelacao", respostaImpressaoReRt));
                rpt.SubreportProcessing += new SubreportProcessingEventHandler(delegate (object o, SubreportProcessingEventArgs e)
                {
                    var lista = new List<ListaOB>();
                    foreach (var itens in objlistaOB.RepeticoesReRt.ListaOB)
                    {
                        itens.ValorOb = itens.ValorOb / 100; // Está vindo do webservice, precisa formatar novamente
                        lista.Add(itens);
                    }

                    if (numeroRelatorio?.Substring(4, 2) == "RE")
                    {
                        e.DataSources.Add(new ReportDataSource("DataSetItensObsRe", lista));
                    }
                    else
                    {
                        e.DataSources.Add(new ReportDataSource("DataSetItensObsRt", lista));
                    }
                });

                rpt.ReportPath = rptPath;
                rpt.DisplayName = numeroRelatorio;

                return HelperReport.ExportReport(rpt, filetype, contenttype);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private RespostaImpressaoRelacaoReRt PreencherListaOB(string codigoUnidadeGestora, string codigoGestao, string codigoBanco, 
                string dataSolicitacao, string numeroRelatorio)
        {
            respostaImpressaoReRt = new List<RespostaImpressaoRelacaoReRt>();
            var item = App.ImpressaoRelacaoRERTService.TransmitirImpressaoRelacaoReRt(codigoUnidadeGestora, codigoGestao, codigoBanco, dataSolicitacao, numeroRelatorio, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Listar), "");

            if (item.MsgErro == null && item.MsgRetorno == null)
            {
                DateTime dataRef = DateTime.ParseExact(item.DataReferencia, "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime dataEmi = DateTime.ParseExact(item.DataEmissao, "ddMMyyyy", CultureInfo.InvariantCulture);
                item.DataReferencia = String.Format("{0:dd/MM/yyyy}", dataRef);
                item.DataEmissao = String.Format("{0:dd/MM/yyyy}", dataEmi);
                item.ValorTotalDocumento = item.ValorTotalDocumento / 100; // Está vindo do webservice, precisa formatar novamente

                respostaImpressaoReRt.Add(item);
            }

            return item;
        }
    }
}