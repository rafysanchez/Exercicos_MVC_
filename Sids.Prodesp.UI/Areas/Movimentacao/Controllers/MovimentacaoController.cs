using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sids.Prodesp.UI.Areas.Movimentacao.Models;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.UI.Report;
using System.Globalization;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Controllers
{
    public class MovimentacaoController : MovimentacaoBaseController
    {
        public MovimentacaoController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(null, "MovimentacaoCreate");
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                return View("Index", Display(new MovimentacaoOrcamentaria()));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(form, "Movimentacao");
                var _filterItems = Display(new MovimentacaoOrcamentaria(), form);

                // Laço que salva o Id da funcionalidade atual ao carregar a página
                //foreach (var fa in _filterItems)
                //{
                //    fa.FuncionalidadeAtual = _funcId;
                //}

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


        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var filtro = App.MovimentacaoService.GetCurrentFilter("Movimentacao");
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


        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));

            var model = Display(new MovimentacaoOrcamentaria(), true);

            return View("CreateEdit", model);
        }


        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Alterar")]
        public ActionResult Edit(string id)
        {
            var objModel = App.MovimentacaoService.Selecionar(Convert.ToInt32(id));

            var model = Display(objModel, false);

            return View("CreateEdit", model);
        }
        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Alterar")]
        public ActionResult Estornar(string id, string idMovimentacao, string opcao, string descDocumento)
        {
            var objModel = App.MovimentacaoService.Selecionar(Convert.ToInt32(idMovimentacao));          

            var model = DisplayEstorno(objModel);

            return View("CreateEdit", model);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(ModelSalvar  movimentacao)
        {
            try
            {
                movimentacao.Model.Cancelamento = movimentacao.Model.Cancelamento ?? new List<MovimentacaoCancelamento>();
                movimentacao.Model.NotasCreditos = movimentacao.Model.NotasCreditos ?? new List<MovimentacaoNotaDeCredito>();
                movimentacao.Model.Distribuicao = movimentacao.Model.Distribuicao ?? new List<MovimentacaoDistribuicao>();

                movimentacao.Model.Meses = movimentacao.Model.Meses ?? new List<MovimentacaoMes>();

                var id = Salvar(movimentacao.Model, Convert.ToInt32(_funcId));

                return Json(new { Status = "Sucesso", Id = id });

            }
            catch (Exception ex)
            {
                return Json(new { Status = "Falha", Msg = ex.Message });
            }
        }

        private int Salvar(MovimentacaoOrcamentaria movimentacao, int funcionalidade)
        {
            var acao = movimentacao.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.MovimentacaoService.SalvarOuAlterar(movimentacao, funcionalidade, Convert.ToInt16(acao));
        }

        public JsonResult Transmitir(ModelSalvar mov)
        {
            MovimentacaoOrcamentaria objModel;
            try
            {  
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                _modelId = Salvar(mov.Model, 0);

                App.MovimentacaoService.Transmitir(_modelId, usuario, (int)_funcId);

                objModel = App.MovimentacaoService.Selecionar(_modelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.MovimentacaoService.Selecionar(_modelId);

                var result = new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel };

                return Json(result);
            }
        }


        [PermissaoAcesso(Controller = typeof(MovimentacaoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                AcaoEfetuada acao;
                var entity = App.MovimentacaoService.Selecionar(Convert.ToInt32(id));

                if (validaExclusao(entity))
                {
                    acao = App.MovimentacaoService.ExcluirMovimentacao(entity, Convert.ToInt32(_funcId), Convert.ToInt16(EnumAcao.Excluir));
                }
                else
                {
                    throw new SidsException("Não é permitido excluir Movimentação, existem documentos já transmitidos");
                }

                return Json(acao.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public bool validaExclusao(MovimentacaoOrcamentaria mov)
        {
            bool ok = true;

            foreach (var movimentacaoCancelamento in mov.Cancelamento.OrderBy(x => x.NrSequencia))
            {
                if (!string.IsNullOrEmpty(movimentacaoCancelamento.NumeroSiafem))
                {
                    ok = false;
                }
            }

            foreach (var movimentacaodistribuicao in mov.Distribuicao.OrderBy(x => x.NrSequencia))
            {
                if (!string.IsNullOrEmpty(movimentacaodistribuicao.NumeroSiafem))
                {
                    ok = false;
                }
            }

            foreach (var movimentacaonotas in mov.NotasCreditos.OrderBy(x => x.NrSequencia))
            {
                if (!string.IsNullOrEmpty(movimentacaonotas.NumeroSiafem))
                {
                    ok = false;
                }
            }
    
            return ok;
        }
        
        public class ModelSalvar
        {
            public MovimentacaoOrcamentaria Model { get; set; }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImprimirMov(int id, int idMovimentacao, string tipoDocumento, int numSequencia, int numAgrupamento, 
            string numDocumento, int opcaoImpressao, int opcaoAgrupamento)
        {
            try
            {
                var listaNL = new List<RespostaConsultaNL>();
                var listaNC = new List<RespostaConsultaNC>();
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var movimentacao = App.MovimentacaoService.Selecionar(idMovimentacao);
                string nrReducao, nrSuplementacao, unidadeGestoraEmitenteNotaCredito = "", gestaoEmitenteNotaCredito = "";
                int numSeqReducao = numSequencia, numSeqSuplementacao = numSequencia;

                // Na NC o valor da variável numSequencia deverá ser sempre 1, quando variar o tipo de movimentação orçamentária (Transferência ou Estorno)
                // Tratamento para UG Emitente quando for estorno
                if (tipoDocumento == "Nota de Crédito")
                {
                    var unidadeGestoraEmitenteNC = movimentacao.Cancelamento.Where(a => a.NrSequencia == numSequencia).Select(b => b.UnidadeGestoraFavorecida)?.FirstOrDefault();
                    var gestaoEmitenteNC = movimentacao.Cancelamento.Where(a => a.NrSequencia == numSequencia).Select(b => b.GestaoFavorecida)?.FirstOrDefault();

                    numSeqReducao = movimentacao.IdTipoMovimentacao == 1 ? 1 : numSequencia;
                    numSeqSuplementacao = movimentacao.IdTipoMovimentacao == 2 ? 1 : numSequencia;
                    unidadeGestoraEmitenteNotaCredito = movimentacao.IdTipoMovimentacao == 2 ? unidadeGestoraEmitenteNC : movimentacao.UnidadeGestoraEmitente;
                    gestaoEmitenteNotaCredito = movimentacao.IdTipoMovimentacao == 2 ? gestaoEmitenteNC : movimentacao.GestaoEmitente;
                }

                nrReducao = movimentacao.ReducaoSuplementacao
                        .Where(a => a.RedSup == "R" && a.NrSequencia == numSeqReducao).Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                nrSuplementacao = movimentacao.ReducaoSuplementacao
                        .Where(a => a.RedSup == "S" && a.NrSequencia == numSeqSuplementacao).Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                // Lista Nº de Notas por agrupamento
                var listaNotaCancelamentoAgrup = movimentacao.Cancelamento.Where(a => a.NrAgrupamento == numAgrupamento).Select(b => new { b.NumeroSiafem, b.Id, b.NrSequencia }).ToList();
                var listaNotaDistribucaoAgrup = movimentacao.Distribuicao.Where(a => a.NrAgrupamento == numAgrupamento).Select(b => new { b.NumeroSiafem, b.Id, b.NrSequencia }).ToList();
                var listaNotaCreditoAgrup = movimentacao.NotasCreditos.Where(a => a.NrAgrupamento == numAgrupamento).Select(b => new { b.NumeroSiafem, b.Id, b.NrSequencia }).ToList();

                if (numDocumento == "" || movimentacao.UnidadeGestoraEmitente == "" || movimentacao.GestaoEmitente == "")
                {
                    throw new Exception(" SIAFEM:<br> - Nº Documento inválido<br> ou Unidade Gestora Emitente inválido<br> ou Gestão Emitente inválido");
                }

                // NL
                if ((tipoDocumento == "Cancelamento" || tipoDocumento == "Distribuição") && opcaoImpressao == 1)
                {
                    var nrReducaoSuplementacao = tipoDocumento == "Cancelamento" ? nrReducao : nrSuplementacao;
                    var consultaNL = new RespostaConsultaNL();

                    consultaNL = App.MovimentacaoService.ConsultaNL(usuario, movimentacao.UnidadeGestoraEmitente, movimentacao.GestaoEmitente, numDocumento, tipoDocumento, nrReducao, nrSuplementacao);
                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfMovimentacaoOrcamentariaNL(consultaNL, nrReducaoSuplementacao, tipoDocumento);
                }
                // NC
                else if (tipoDocumento == "Nota de Crédito" && opcaoImpressao == 1)
                {
                    var consultaNC = App.MovimentacaoService.ConsultaNC(usuario, unidadeGestoraEmitenteNotaCredito, gestaoEmitenteNotaCredito, numDocumento, tipoDocumento, nrReducao, nrSuplementacao);

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfMovimentacaoOrcamentariaNC(consultaNC, nrReducao, nrSuplementacao, tipoDocumento, movimentacao.GestaoEmitente);
                }
                else // Agrupamento
                {
                    if (opcaoAgrupamento == 1 || opcaoAgrupamento == 3) // NL Cancelamento / Distribuição / TODOS
                    {
                        // Cancelamento
                        foreach (var item in listaNotaCancelamentoAgrup)
                        {
                            nrReducao = movimentacao.ReducaoSuplementacao
                                                    .Where(a => a.RedSup == "R" && a.NrSequencia == item.NrSequencia)
                                                    .Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                            if (string.IsNullOrWhiteSpace(item.NumeroSiafem))
                            {
                                throw new Exception(" SIAFEM:<br> - Nº Documento inválido<br> ou Unidade Gestora Emitente inválido<br> ou Gestão Emitente inválido");
                            }

                            var consultaCancNL = App.MovimentacaoService.ConsultaNL(usuario, movimentacao.UnidadeGestoraEmitente, movimentacao.GestaoEmitente, item.NumeroSiafem, "Cancelamento", nrReducao, "");

                            listaNL.Add(consultaCancNL);
                        }

                        // Distribuição
                        foreach (var item in listaNotaDistribucaoAgrup)
                        {
                            nrSuplementacao = movimentacao.ReducaoSuplementacao
                                                    .Where(a => a.RedSup == "S" && a.NrSequencia == item.NrSequencia)
                                                    .Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                            if (string.IsNullOrWhiteSpace(item.NumeroSiafem))
                            {
                                throw new Exception(" SIAFEM:<br> - Nº Documento inválido<br> ou Unidade Gestora Emitente inválido<br> ou Gestão Emitente inválido");
                            }

                            var consultaDistribuicaoNL = App.MovimentacaoService.ConsultaNL(usuario, movimentacao.UnidadeGestoraEmitente, movimentacao.GestaoEmitente, item.NumeroSiafem, "Distribuição", "", nrSuplementacao);

                            listaNL.Add(consultaDistribuicaoNL);
                        }
                    }

                    if (opcaoAgrupamento == 2 || opcaoAgrupamento == 3) // NC  / TODOS
                    {
                        // Nota de Crédito
                        foreach (var item in listaNotaCreditoAgrup)
                        {
                            // Na NC o valor da variável numSequencia deverá ser sempre 1, quando variar o tipo de movimentação orçamentária (Transferência ou Estorno)
                            numSeqReducao = movimentacao.IdTipoMovimentacao == 1 ? 1 : item.NrSequencia;
                            numSeqSuplementacao = movimentacao.IdTipoMovimentacao == 2 ? 1 : item.NrSequencia;

                            nrReducao = movimentacao.ReducaoSuplementacao
                                                    .Where(a => a.RedSup == "R" && a.NrSequencia == numSeqReducao)
                                                    .Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                            nrSuplementacao = movimentacao.ReducaoSuplementacao
                                                    .Where(a => a.RedSup == "S" && a.NrSequencia == numSeqSuplementacao)
                                                    .Select(x => x.NrSuplementacaoReducao)?.FirstOrDefault();

                            var unidadeGestoraEmitenteNC = movimentacao.Cancelamento.Where(a => a.NrSequencia == item.NrSequencia).Select(b => b.UnidadeGestoraFavorecida)?.FirstOrDefault();
                            var gestaoEmitenteNC = movimentacao.Cancelamento.Where(a => a.NrSequencia == item.NrSequencia).Select(b => b.GestaoFavorecida)?.FirstOrDefault();

                            unidadeGestoraEmitenteNotaCredito = movimentacao.IdTipoMovimentacao == 2 ? unidadeGestoraEmitenteNC : movimentacao.UnidadeGestoraEmitente;
                            gestaoEmitenteNotaCredito = movimentacao.IdTipoMovimentacao == 2 ? gestaoEmitenteNC : movimentacao.GestaoEmitente;

                            if (string.IsNullOrWhiteSpace(item.NumeroSiafem))
                            {
                                throw new Exception(" SIAFEM:<br> - Nº Documento inválido<br> ou Unidade Gestora Emitente inválido<br> ou Gestão Emitente inválido");
                            }

                            var consultaCreditoNC = App.MovimentacaoService.ConsultaNC(usuario, unidadeGestoraEmitenteNotaCredito, gestaoEmitenteNotaCredito, item.NumeroSiafem, "Nota de Crédito", nrReducao, nrSuplementacao);

                            listaNC.Add(consultaCreditoNC);
                        }
                    }

                    Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfMovimentacaoAgrupamento(listaNL, listaNC, movimentacao.GestaoEmitente, numAgrupamento);
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