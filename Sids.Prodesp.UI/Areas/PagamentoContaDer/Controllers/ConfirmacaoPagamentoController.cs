using System;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Application;
using Sids.Prodesp.UI.Security;
using System.Collections.Generic;
using Sids.Prodesp.UI.Areas.PagamentoContaDer.Models;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Controllers
{
    public class ConfirmacaoPagamentoController : PagamentoContaDerBaseController
    {
        public ConfirmacaoPagamentoController()
        {
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(33);
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(ConfirmacaoPagamentoController), Operacao = "Listar")]
        [HttpGet]
        public ActionResult Index(string id)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    this.CarregarCombos();
                    App.PerfilService.SetCurrentFilter(null, "ConfirmacaoPagamento");
                    App.FuncionalidadeService.SalvarFuncionalidadeAtual(Convert.ToInt32(id));
                }

                return View("Index", Display(new ConfirmacaoPagamento()));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [PermissaoAcesso(Controller = typeof(ConfirmacaoPagamentoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<ConfirmacaoPagamentoFiltroViewModel> _filterItems = new List<ConfirmacaoPagamentoFiltroViewModel>();
            try
            {
                if (ModelState.IsValid)
                {
                    this.CarregarCombos();
                    App.PerfilService.SetCurrentFilter(form, "ConfirmacaoPagamento");
                    _filterItems = this.Display(new ConfirmacaoPagamento(), form).ToList();

                    if (!_filterItems.Any())
                    {
                        ExibirMensagemErro("Registro não encontrado.");
                    }
                }

                return View("Index", _filterItems);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }

        }

        [PermissaoAcesso(Controller = typeof(ConfirmacaoPagamentoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                FormCollection filtro = App.ProgramaService.GetCurrentFilter("ConfirmacaoPagamento");
                return filtro != null ? Index(filtro) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        protected IEnumerable<ConfirmacaoPagamentoFiltroViewModel> Display(ConfirmacaoPagamento entity)
        {
            ConfirmacaoPagamento filterModel = InitializeEntityModel(entity);

            ConfirmacaoPagamento obj = (ConfirmacaoPagamento)Convert.ChangeType(filterModel, typeof(ConfirmacaoPagamento));
            ViewBag.Filtro = InitializeFiltroViewModel(obj);
            ViewBag.Usuario = _userLoggedIn;

            return new List<ConfirmacaoPagamentoFiltroViewModel>();
        }

        protected IEnumerable<ConfirmacaoPagamentoFiltroViewModel> Display(ConfirmacaoPagamento entity, FormCollection form)
        {
            IEnumerable<ConfirmacaoPagamento> entities = new List<ConfirmacaoPagamento>();

            ConfirmacaoPagamento model = GenerateFilterViewModel(form, entity);

            entities = App.ConfirmacaoPagamentoService.BuscarGrid(model);

            return InitializeConfirmacaoPagamentoFiltroViewModel(entities);

        }

        protected ConfirmacaoPagamento GenerateFilterViewModel(FormCollection form, ConfirmacaoPagamento entity)
        {
            ConfirmacaoPagamentoFiltroViewModel filter;

            filter = InitializeFiltroViewModel(entity, form);

            entity.DataCadastroAte = filter.DataCadastroAte;


            //entity = MapViewModelToEntityModel(form, entity, ref filter);

            ViewBag.Filtro = filter;

            return entity;
        }

        private ConfirmacaoPagamentoFiltroViewModel InitializeFiltroViewModel(ConfirmacaoPagamento entity)
        {
            return new ConfirmacaoPagamentoFiltroViewModel().CreateInstance(entity);
        }

        private ConfirmacaoPagamentoFiltroViewModel InitializeFiltroViewModel(ConfirmacaoPagamento entity, FormCollection form)
        {
            return new ConfirmacaoPagamentoFiltroViewModel().CreateInstance(entity, form);
        }

        private ConfirmacaoPagamento MapViewModelToEntityModel(FormCollection form, ConfirmacaoPagamento entity, ref ConfirmacaoPagamentoFiltroViewModel viewModel)
        {
            string field = form["AnoReferencia"];
            if (!string.IsNullOrEmpty(field))
            {
                int ano = int.Parse(field);
                ((ConfirmacaoPagamento)Convert.ChangeType(entity, typeof(ConfirmacaoPagamento))).AnoReferencia = ano;
                viewModel.AnoReferencia = ano;
            }
            return entity;
        }

        private IEnumerable<ConfirmacaoPagamentoFiltroViewModel> InitializeConfirmacaoPagamentoFiltroViewModel(IEnumerable<ConfirmacaoPagamento> entities)
        {
            List<ConfirmacaoPagamentoFiltroViewModel> items = new List<ConfirmacaoPagamentoFiltroViewModel>();

            foreach (ConfirmacaoPagamento entity in entities)
            {
                items.Add(new ConfirmacaoPagamentoFiltroViewModel().CreateInstance(entity));
            }

            return items;
        }

        [HttpGet]
        public ActionResult Create(FormCollection form)
        {
            if (ModelState.IsValid)
                CarregarCombosCadastro();

            return View("CreateEdit", new List<ConfirmacaoPagamentoFiltroViewModel>());
        }

        //usada para consulta que fica dentro do gravar
        [HttpPost]
        public ActionResult ListarPagamentosConfirmar(ConfirmacaoPagamentoFiltroViewModel view, FormCollection form)
        {
            String viewString = string.Empty;
            try
            {
                // 3.8 RDN8 – Preenchimento dos campos Data
                // Preenchido automaticamente com a data do processamento, podendo ser alterado pelo usuário, 
                // se alterado deverá ser menor ou igual à data de processamento, 
                // emitindo a mensagem ao acionar a opção “Consultar” “A data deve ser menor ou igual a data de processamento”
                ValidarData(view.DataCadastro);
                ValidarData(view.DataConfirmacao);
                ValidarData(view.DataPreparacao);


                ConfirmacaoPagamentoFiltroViewModel ret = this.GenerateFilterFormViewModel(form);
                if (!string.IsNullOrEmpty(ret.TipoConfirmacao))
                {
                    //List<ConfirmacaoPagamentoFiltroViewModel> retorno = GenerateFilterEditViewModel(view);

                    List<ConfirmacaoPagamentoFiltroViewModel> retorno = GenerateFilterEditViewModel(ret);
                    if (retorno.Count > 0)
                    {
                        viewString = ConvertPartialViewToString(PartialView("_ListarPagamentosConfirmar", retorno));

                        return Json(new { Status = "Sucesso", Html = viewString, Dados = retorno });

                        //return View("_ListarPagamentosConfirmar", retorno);
                    }
                    else
                        base.ExibirMensagemErro("Não foi possível cadastrar o registro");
                }

                return View("_ListarPagamentosConfirmar", new List<ConfirmacaoPagamentoFiltroViewModel>());
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }


        //clicando em gravar
        public ActionResult ListarPagamentosConfirmarSalvar(ConfirmacaoPagamentoFiltroViewModel view, FormCollection form)
        {
            try

            {

                // String viewString = string.Empty;
                // 3.8 RDN8 – Preenchimento dos campos Data
                // Preenchido automaticamente com a data do processamento, podendo ser alterado pelo usuário, 
                // se alterado deverá ser menor ou igual à data de processamento, 
                // emitindo a mensagem ao acionar a opção “Consultar” “A data deve ser menor ou igual a data de processamento”
                ValidarData(view.DataCadastro);
                ValidarData(view.DataConfirmacao);
                ValidarData(view.DataPreparacao);

                ConfirmacaoPagamentoFiltroViewModel ret = this.GenerateFilterFormViewModel(form);

                List<ConfirmacaoPagamento> retorno = GenerateFilterEditViewModelSalvar(ret);

                this.Save(retorno);

                // return null; // View("CreateEdit", new List<ConfirmacaoPagamentoFiltroViewModel>());

                string status = "Pagamentos confirmados com sucesso!";

                return Json(new { Status = status });
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }

        }

        public ActionResult ListarPagamentosTransmitir(ConfirmacaoPagamentoFiltroViewModel view, FormCollection form)
        {
            String viewString = string.Empty;
            // 3.8 RDN8 – Preenchimento dos campos Data
            // Preenchido automaticamente com a data do processamento, podendo ser alterado pelo usuário, 
            // se alterado deverá ser menor ou igual à data de processamento, 
            // emitindo a mensagem ao acionar a opção “Consultar” “A data deve ser menor ou igual a data de processamento”
            ValidarData(view.DataCadastro);
            ValidarData(view.DataConfirmacao);
            ValidarData(view.DataPreparacao);
            view.Tela = "Cadastrar";
            ConfirmacaoPagamentoFiltroViewModel ret = this.GenerateFilterFormViewModel(form);

            List<ConfirmacaoPagamento> retorno = GenerateFilterEditViewModelSalvar(ret);

            int retornoSalvar = this.Save(retorno);

            this.Transmitir(retorno, retornoSalvar);

            return null;

        }

        private static void ValidarData(DateTime? data)
        {
            if (data.HasValue && data.Value.Date > DateTime.Today)
            {
                throw new SidsException("A data deve ser menor ou igual a data de processamento.");
            }
        }

        public ActionResult Altera(int? id, int? item)
        {
            this.CarregarCombosCadastro();
            ConfirmacaoPagamentoFiltroViewModel retorno = new ConfirmacaoPagamentoFiltroViewModel();
            ConfirmacaoPagamento consultaAlteracao = App.ConfirmacaoPagamentoService.ConsultarPagamentoPorId(id, item).FirstOrDefault();
            if (consultaAlteracao != null)
            {
                retorno = new ConfirmacaoPagamentoFiltroViewModel().CreateInstance(consultaAlteracao);
                if (retorno != null)
                {
                    @ViewBag.TipoConfirmacao = retorno.TipoConfirmacao.ToString();
                    ExibirMensagemSucesso("Registro atualizado com sucesso!");
                }
                else
                    ExibirMensagemErro("Não foi possível excluir o registro!");
            }
            return View("Altera", retorno);
        }

        public ActionResult Exclui(int? id, int? item)
        {
            this.CarregarCombosCadastro();

            int retornoConsulta = App.ConfirmacaoPagamentoService.ApagarPagamento(id, item);
            if (retornoConsulta > 0)
                base.ExibirMensagemSucesso("Registro excluído com sucesso!.");
            else
                base.ExibirMensagemErro("Não foi possível excluir o registro.");

            return View("Index", new List<ConfirmacaoPagamentoFiltroViewModel>());
        }

        public ActionResult Visualiza(int? id, int? item)
        {
            this.CarregarCombosCadastro();
            ConfirmacaoPagamentoFiltroViewModel retorno = new ConfirmacaoPagamentoFiltroViewModel();
            ConfirmacaoPagamento consultaAlteracao = App.ConfirmacaoPagamentoService.ConsultarPagamentoPorId(id, item).FirstOrDefault();
            if (consultaAlteracao != null)
            {
                retorno = new ConfirmacaoPagamentoFiltroViewModel().CreateInstance(consultaAlteracao);
                if (retorno != null)
                    return View("Visualiza", retorno);
                else
                    base.ExibirMensagemErro("Não foi possível visualizar o registro");
            }
            return View("Visualiza", retorno);
        }


        protected ConfirmacaoPagamentoFiltroViewModel GenerateFilterFormViewModel(FormCollection form)
        {
            return new ConfirmacaoPagamentoFiltroViewModel().GenerateFilterFormViewModel(form);
        }

        //usada para consulta que fica dentro do botão gravar 
        protected List<ConfirmacaoPagamentoFiltroViewModel> GenerateFilterEditViewModel(ConfirmacaoPagamentoFiltroViewModel form)
        {
            List<ConfirmacaoPagamentoFiltroViewModel> ret = new List<ConfirmacaoPagamentoFiltroViewModel>();

            ConfirmacaoPagamento filter = new ConfirmacaoPagamentoFiltroViewModel().InitializeFiltroGridEditViewModel(form);
            List<ConfirmacaoPagamento> retornoConsulta = App.ConfirmacaoPagamentoService.ConsultarPagamento(filter);
            if (retornoConsulta != null)
            {
                ret = new ConfirmacaoPagamentoFiltroViewModel().EditMapViewModelToEntityModel(retornoConsulta);
                ViewBag.Filtro = filter;
            }
            return ret;
        }

        protected List<ConfirmacaoPagamento> GenerateFilterEditViewModelSalvar(ConfirmacaoPagamentoFiltroViewModel form)
        {
            List<ConfirmacaoPagamentoFiltroViewModel> ret = new List<ConfirmacaoPagamentoFiltroViewModel>();

            ConfirmacaoPagamento filter = new ConfirmacaoPagamentoFiltroViewModel().InitializeFiltroGridEditViewModel(form);
            List<ConfirmacaoPagamento> retornoConsulta = App.ConfirmacaoPagamentoService.ConsultarPagamento(filter);
            return retornoConsulta;
        }


        //[HttpPost]
        //public ActionResult SalvarCadastro(FormCollection form, ConfirmacaoPagamentoFiltroViewModel PesquisaRetorno)
        //{
        //    ConfirmacaoPagamento ret = new ConfirmacaoPagamentoFiltroViewModel().CreateNewSaveInstance(form);
        //    int retornoConsulta = App.ConfirmacaoPagamentoService.SalvarPagamento(ret, "Filho");
        //    return View(retornoConsulta);
        //}

        [HttpPost]
        public ActionResult Transmitir(List<ConfirmacaoPagamento> confirmarPagamento, int id)
        {
            // está salvando no método ListarPagamentosTransmitir que chama este


            string verificaNumeroGerador = string.Empty;

            try
            {
                App.ConfirmacaoPagamentoService.Transmitir(id, _userLoggedIn, _funcId ?? 0);

                ////Transmitir confirmação Prodesp
                //Model.Entity.Seguranca.Regional orgao = new Model.Entity.Seguranca.Regional();

                //foreach (var confirmacao in confirmarPagamento.OrderBy(p => p.NumeroDocumentoItem))
                //{

                //    if (confirmacao.FonteLista == string.Empty || confirmacao.FonteLista == null)
                //    {

                //        if (confirmacao.NumeroDocumentoItem.Substring(0, 17) != verificaNumeroGerador)
                //        {

                //            ConfirmacaoPagamento confirmarPagamentoUnico = new ConfirmacaoPagamento();
                //            confirmarPagamentoUnico.Senha = confirmacao.Senha;
                //            confirmarPagamentoUnico.Chave = confirmacao.Chave;
                //            confirmarPagamentoUnico.DataConfirmacao = confirmacao.DataConfirmacao;
                //            confirmarPagamentoUnico.AnoReferencia = confirmacao.AnoReferencia;
                //            confirmarPagamentoUnico.IdTipoDocumento = Convert.ToInt16(confirmacao.NumeroDocumentoItem.Substring(0, 2));
                //            confirmarPagamentoUnico.NumeroOP = confirmacao.NumeroOP;
                //            confirmarPagamentoUnico.TipoPagamento = confirmacao.TipoPagamento;
                //            orgao.Id = Convert.ToInt16(confirmacao.NumeroOP.Substring(2, 2));

                //            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConfirmacaoPagtoOPApoio(confirmacao.Chave, confirmacao.Senha, confirmarPagamentoUnico, orgao);

                //            verificaNumeroGerador = confirmacao.NumeroDocumentoItem.Substring(0, 17);


                //            if (result[0].outErro == "" || result[0].outErro == null)
                //            {
                //                confirmarPagamentoUnico.DataTransmitidoProdesp = DateTime.Now;
                //                confirmarPagamentoUnico.StatusProdesp = "S";
                //                confirmarPagamentoUnico.TransmitidoProdesp = true;
                //                confirmarPagamentoUnico.NumeroDocumento = confirmacao.NumeroDocumento;
                //                confirmarPagamentoUnico.id_confirmacao_pagamento = id;
                //                confirmarPagamentoUnico.NumeroDocumentoItem = confirmacao.NumeroDocumentoItem;
                //            }
                //            else
                //            {
                //                confirmarPagamentoUnico.DataTransmitidoProdesp = DateTime.Now;
                //                confirmarPagamentoUnico.StatusProdesp = "E";
                //                confirmarPagamentoUnico.TransmitidoProdesp = false;
                //                confirmarPagamentoUnico.NumeroDocumento = confirmacao.NumeroDocumento;
                //                confirmarPagamentoUnico.id_confirmacao_pagamento = id;
                //                confirmarPagamentoUnico.MensagemServicoProdesp = result[0].outErro;

                //                confirmarPagamentoUnico.NumeroDocumentoItem = confirmacao.NumeroDocumentoItem;
                //            }

                //            var update = App.ConfirmacaoPagamentoService.AtualizarPagamento(confirmarPagamentoUnico);

                //        }

                //    }
                //}

                // gerar notas somente se confirmação foi transmitida?
                // EnumAcao acao = viewModel.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
                // App.NotaDeLancamentoService.GerarNotasLancamento(confirmacaoSalva, _userLoggedIn, viewModel.TransmitirSiafem, _funcId.Value, acao);
                // id = confirmacaoSalva.Id;
                // Para fazer testes depois precisa arrancar(Rodrigo)
                App.ParametrizacaoFormaGerarNlService.GerarNl(id, new ConfirmacaoPagamento(), _userLoggedIn, RegionalList.ToList());

                return Json(new { Status = "Sucesso", id = id });
            }
            catch (Exception ex)
            {
                string status = "";
                status = "Falha Transmissão Confirmação de Pagamento Prodesp";
                return Json(new { Status = status });
            }
        }

        [HttpPost]
        public ActionResult Retransmitir(FormCollection form, ConfirmacaoPagamentoFiltroViewModel PesquisaRetorno, DadoConfirmacaoPagamentoViewModel entrada)
        {
            int id = 0;
            try
            {
                ConfirmacaoPagamento confirmacao = entrada.ToEntity();
                AcaoEfetuada retornoSalvamentoConfirmacao = App.ConfirmacaoPagamentoService.Salvar(confirmacao, _funcId.Value);

                if (retornoSalvamentoConfirmacao == AcaoEfetuada.Sucesso)
                {
                    ConfirmacaoPagamento confirmacaoSalva = App.ConfirmacaoPagamentoService.Selecionar(confirmacao.Id);

                    EnumAcao acao = entrada.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;

                    App.NotaDeLancamentoService.GerarNotasLancamento(confirmacaoSalva, _userLoggedIn, entrada.TransmitirSiafem, _funcId.Value, acao);

                    id = confirmacaoSalva.Id;
                }
                return Json(new { Status = "Sucesso", id = id });
            }
            catch
            {
                string status = "";
                status = "Falha Transmissão Confirmação de Pagamento Prodesp";
                return Json(new { Status = status });
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]

        [HttpPost]
        public int Save(List<ConfirmacaoPagamento> confimacaoPagamento)
        {
            try
            {
                return SalvarService(confimacaoPagamento);

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        private int SalvarService(List<ConfirmacaoPagamento> confirmacaoPagamento)
        {
            string TipoSalvar = "";

            int contadorFonteLista = 0;
            var totalFonteLista = confirmacaoPagamento.Where(x => x.FonteLista != "").Count();

            //Preenche dados confirmacao_pagamento (tabela pai)
            ConfirmacaoPagamento dadosPagamentoPai = new ConfirmacaoPagamento();
            dadosPagamentoPai.TipoConfirmacao = confirmacaoPagamento[0].TipoConfirmacao;
            dadosPagamentoPai.AnoReferencia = confirmacaoPagamento[0].AnoReferencia;
            dadosPagamentoPai.Conta = confirmacaoPagamento[0].ContaProdesp;
            dadosPagamentoPai.DataPreparacao = confirmacaoPagamento[0].DataPreparacao;
            dadosPagamentoPai.DataConfirmacao = confirmacaoPagamento[0].DataConfirmacao;
            dadosPagamentoPai.TipoDocumento = confirmacaoPagamento[0].TipoDocumento;
            dadosPagamentoPai.NumeroDocumento = confirmacaoPagamento[0].NumeroDocumento;
            dadosPagamentoPai.TipoPagamento = confirmacaoPagamento[0].TipoPagamento;
            TipoSalvar = "Pai";
            int retorno_Id_Confirmacao_Pagamento = App.ConfirmacaoPagamentoService.SalvarPagamento(dadosPagamentoPai, TipoSalvar);

            //Preenche dados confirmacao_pagamento_item (tabela filho)
            ConfirmacaoPagamento dadosPagamentoFilho = new ConfirmacaoPagamento();
            ConfirmacaoPagamento dadosPagamentoFonte = new ConfirmacaoPagamento();
            foreach (var item in confirmacaoPagamento)
            {
                if (item.FonteLista == null || item.FonteLista == "")
                {
                    dadosPagamentoFilho.id_confirmacao_pagamento = retorno_Id_Confirmacao_Pagamento;
                    dadosPagamentoFilho.IdExecucaoPD = item.IdExecucaoPD;
                    dadosPagamentoFilho.IdAutorizacaoOB = item.IdAutorizacaoOB;

                    dadosPagamentoFilho.RegionalId = Convert.ToInt32(item.Orgao);

                    dadosPagamentoFilho.OrigemConfirmacao = "3";
                    dadosPagamentoFilho.TipoDespesa = item.TipoDespesa;
                    dadosPagamentoFilho.DataVencimento = item.DataVencimento;

                    if (!string.IsNullOrEmpty(dadosPagamentoFilho.NumeroContrato))
                    {

                        dadosPagamentoFilho.NumeroContrato = item.NumeroContrato.Substring(0, 2) + "." + item.NumeroContrato.Substring(2, 2) + "." + item.NumeroContrato.Substring(4, 5) + "-" + item.NumeroContrato.Substring(9, 1);

                    }
                    else
                    {
                        dadosPagamentoFilho.NumeroContrato = string.Empty;
                    }



                    dadosPagamentoFilho.CodigoObra = item.CodigoObra;
                    dadosPagamentoFilho.NumeroOP = item.NumeroOP;
                    dadosPagamentoFilho.Banco = item.Banco; //banco pagador
                    dadosPagamentoFilho.Agencia = item.Agencia;// agencia pagador
                    dadosPagamentoFilho.Conta = item.Conta; // conta pagador
                    dadosPagamentoFilho.BancoFavorecido = item.BancoFavorecido;
                    dadosPagamentoFilho.AgenciaFavorecido = item.AgenciaFavorecido;
                    dadosPagamentoFilho.ContaFavorecido = item.ContaFavorecido;

                    dadosPagamentoFilho.FonteSIAFEM = item.FonteSIAFEM;
                    dadosPagamentoFilho.NumeroEmpenho = item.NumeroEmpenho;
                    dadosPagamentoFilho.NumeroProcesso = item.NumeroProcesso;
                    dadosPagamentoFilho.NotaFiscal = item.NotaFiscal;
                    dadosPagamentoFilho.NLDocumento = item.NLDocumento;
                    dadosPagamentoFilho.ValorDocumento = item.ValorDocumento;
                    dadosPagamentoFilho.NaturezaDespesa = item.NaturezaDespesa;

                    dadosPagamentoFilho.Referencia = item.Referencia;
                    dadosPagamentoFilho.Orgao = item.Orgao;

                    dadosPagamentoFilho.numeroEmpenhoSiafem = item.numeroEmpenhoSiafem;

                    //credor
                    dadosPagamentoFilho.NomeReduzidoCredor = item.NomeReduzidoCredor;
                    dadosPagamentoFilho.CredorOrganizacaoCredorDocto = item.CredorOrganizacaoCredorDocto;
                    dadosPagamentoFilho.CPF_CNPJ_Credor = item.CPF_CNPJ_Credor;

                    //credor docto
                    dadosPagamentoFilho.NomeReduzidoCredorDocto = item.NomeReduzidoCredorDocto;
                    dadosPagamentoFilho.CredorOrganizacaoCredorOriginal = item.CredorOrganizacaoCredorOriginal;
                    dadosPagamentoFilho.CPFCNPJCredorOriginal = item.CPFCNPJCredorOriginal;


                    dadosPagamentoFilho.DataRealizacao = item.DataRealizacao;
                    dadosPagamentoFilho.ValorDesdobradoCredor = item.ValorDesdobradoCredor;

                    dadosPagamentoFilho.NumeroDocumentoItem = item.NumeroDocumentoItem;


                    dadosPagamentoFilho.TipoDocumento = item.NumeroDocumentoItem.Substring(0, 2);

                    var numeroDoc = dadosPagamentoFilho.NumeroDocumentoItem;

                    if (dadosPagamentoFilho.TipoDocumento == "05")
                    {
                        dadosPagamentoFilho.NumeroDocumento = numeroDoc.Substring(5, 9) + "/" + numeroDoc.Substring(14, 3);
                    }
                    else if (dadosPagamentoFilho.TipoDocumento == "11")
                    {
                        dadosPagamentoFilho.NumeroDocumento = numeroDoc.Substring(2, 9) + "/" + numeroDoc.Substring(11, 3) + "/" + numeroDoc.Substring(14, 3);
                    }
                    else if (dadosPagamentoFilho.TipoDocumento == "40")
                    {
                        dadosPagamentoFilho.NumeroDocumento = numeroDoc.Substring(8, 9);
                    }

                    TipoSalvar = "Filho";
                    App.ConfirmacaoPagamentoService.SalvarPagamento(dadosPagamentoFilho, TipoSalvar);
                    dadosPagamentoFilho = new ConfirmacaoPagamento();
                }
                else
                {

                    dadosPagamentoFonte.FonteLista = item.FonteLista;
                    dadosPagamentoFonte.ValorTotalConfirmarIR = item.ValorTotalConfirmarIR;
                    dadosPagamentoFonte.ValorTotalConfirmarISSQN = item.ValorTotalConfirmarISSQN;
                    dadosPagamentoFonte.id_confirmacao_pagamento = retorno_Id_Confirmacao_Pagamento;
                    dadosPagamentoFonte.ValorTotalFonte = item.ValorTotalFonte;
                    dadosPagamentoFonte.ValorTotalConfirmado = item.ValorTotalConfirmado;

                    App.ConfirmacaoPagamentoService.SalvarFonte(dadosPagamentoFonte);

                    contadorFonteLista++;

                    if (contadorFonteLista == totalFonteLista)
                    {

                        App.ConfirmacaoPagamentoService.SalvarValorConfirmado(dadosPagamentoFonte);
                    }

                }
            }
            //gerarNL(retorno_Id_Confirmacao_Pagamento);
            return retorno_Id_Confirmacao_Pagamento;
        }

        internal void gerarNL(int retorno_Id_Confirmacao_Pagamento)
        {
            // ajuste gerarNL - rafael magna sistemas out 2018
            App.ParametrizacaoFormaGerarNlService.GerarNl(retorno_Id_Confirmacao_Pagamento, new ConfirmacaoPagamento(), _userLoggedIn, RegionalList.ToList());
        }
        internal void CarregarCombos()
        {
            var dropdownTipoDocumentoCadastro = new ConfirmacaoPagamentoFiltroViewModel().CarregarTipoDocumentoCadastro().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownTipoDocumentoCadastro = new SelectList(dropdownTipoDocumentoCadastro, "id", "name");

            var dropdownTipoPagamento = new ConfirmacaoPagamentoFiltroViewModel().CarregarTipoPagamento().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownListTipoPagamento = new SelectList(dropdownTipoPagamento, "id", "name");

            var dropdownOrigemConfirmacao = new ConfirmacaoPagamentoFiltroViewModel().CarregarOrigemConfirmacao().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownListOrigemConfirmacao = new SelectList(dropdownOrigemConfirmacao, "id", "name");

            var dropdownStatusProdesp = new ConfirmacaoPagamentoFiltroViewModel().CarregarStatusProdesp().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownListStatusProdesp = new SelectList(dropdownStatusProdesp, "id", "name");
        }

        internal void CarregarCombosCadastro()
        {
            var dropdownTipoConfirmacao = new ConfirmacaoPagamentoFiltroViewModel().CarregarTipoConfirmacaoPagamento().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownTipoConfirmacao = new SelectList(dropdownTipoConfirmacao, "id", "name");

            var dropdownTipoDocumentoCadastro = new ConfirmacaoPagamentoFiltroViewModel().CarregarTipoDocumentoCadastro().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownTipoDocumentoCadastro = new SelectList(dropdownTipoDocumentoCadastro, "id", "name");

            var dropdownTipoPagamento = new ConfirmacaoPagamentoFiltroViewModel().CarregarTipoPagamento().Select(x => new { id = x.Value, name = x.Text });
            ViewBag.DropdownListTipoPagamento = new SelectList(dropdownTipoPagamento, "id", "name");
        }
    }
}