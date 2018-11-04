using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.ListaDeBoletos;
using Sids.Prodesp.UI.Controllers.Base;
using System.Reflection;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base
{
    public abstract class PagamentoContaUnicaController : ConsutasBaseController
    {
        public readonly IEnumerable<Credor> CredorList = App.CredorService.Listar(new Credor()) ?? new List<Credor>();
        protected static readonly IEnumerable<DesdobramentoTipo> DesdobramentoTipoList = App.DesdobramentoTipoService.Listar(new DesdobramentoTipo()) ?? new List<DesdobramentoTipo>();
        protected static readonly IEnumerable<DocumentoTipo> DocumentoTipoList = App.DocumentoTipoService.Listar(new DocumentoTipo()) ?? new List<DocumentoTipo>();
        protected static readonly IEnumerable<ParaRestoAPagar> ParaRestoAPagarList = App.ParaRestoAPagarService.Listar(new ParaRestoAPagar()) ?? new List<ParaRestoAPagar>();
        protected static readonly IEnumerable<PreparacaoPagamentoTipo> PreparacaoPagamentoTipoList = App.PreparacaoPagamentoTipoService.Listar(new PreparacaoPagamentoTipo()) ?? new List<PreparacaoPagamentoTipo>();
        protected static readonly IEnumerable<ProgramacaoDesembolsoTipo> ProgramacaoDesembolsoTipoList = App.ProgramacaoDesembolsoTipoService.Listar(new ProgramacaoDesembolsoTipo()) ?? new List<ProgramacaoDesembolsoTipo>();
        protected static readonly IEnumerable<ReclassificacaoRetencaoEvento> ReclassificacaoRetencaoEventoList = App.ReclassificacaoRetencaoEventoService.Buscar(new ReclassificacaoRetencaoEvento()) ?? new List<ReclassificacaoRetencaoEvento>();
        protected static readonly IEnumerable<ReclassificacaoRetencaoTipo> ReclassificacaoRetencaoTipoList = App.ReclassificacaoRetencaoTipoService.Listar(new ReclassificacaoRetencaoTipo()) ?? new List<ReclassificacaoRetencaoTipo>();
        protected static readonly IEnumerable<Regional> RegionalList = App.RegionalService.Buscar(new Regional()) ?? new List<Regional>();
        protected static readonly IEnumerable<Reter> ReterList = App.ReterService.Listar(new Reter()) ?? new List<Reter>();
        protected static readonly IEnumerable<TipoBoleto> TipoBoletoList = App.TipoBoletoService.Listar(new TipoBoleto()) ?? new List<TipoBoleto>();
        protected static readonly IEnumerable<PDExecucaoTipoExecucao> TiposExecucao = App.ProgramacaoDesembolsoExecucaoService.TiposExecucao();
        protected static readonly IEnumerable<PDExecucaoTipoPagamento> TiposPagamento = App.ProgramacaoDesembolsoExecucaoService.TiposPagamento();

        //protected static readonly IEnumerable<ProgramacaoDesembolsoAgrupamento> ProgDesembAgrupamentoList = App.ProgramacaoDesembolsoAgrupamentoService.Buscar(new ProgramacaoDesembolsoAgrupamento()) ?? new List<ProgramacaoDesembolsoAgrupamento>();

        protected readonly Usuario _userLoggedIn;
        protected int ModelId;

        protected PagamentoContaUnicaController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _userLoggedIn = App.AutenticacaoService.GetUsuarioLogado();
        }


        protected T Display<T>(T objModel, bool isNewRecord) where T : IPagamentoContaUnica
        {
            if (isNewRecord)
            {
                objModel = InitializeEntityModel(objModel);
            }

            if (objModel is Desdobramento)
            {
                var desdobramento = (Desdobramento)Convert.ChangeType(objModel, typeof(Desdobramento));
                var msg = new List<string>
                {
                    desdobramento.MensagemServicoProdesp,
                };

                List<string> credores = new List<string>();
                List<object> listaCredores = new List<object>();

                credores.AddRange(CredorList.Select(x => $"{x.NomeReduzidoCredor} - {x.Prefeitura}").ToList());
                //credores.AddRange(CredorList.Select(x => x.Prefeitura).ToList());
                listaCredores.AddRange(CredorList.Select(x => new { Nome = $"{x.NomeReduzidoCredor} - {x.Prefeitura}", Reduzido = x.NomeReduzidoCredor, x.BaseCalculo }).ToList());
                //listaCredores.AddRange(CredorList.Select(x => new { Nome = x.Prefeitura, Reduzido = x.NomeReduzidoCredor, x.BaseCalculo }).ToList());
                ViewBag.MsgRetorno = !string.IsNullOrEmpty(desdobramento.MensagemServicoProdesp) ? string.Join("\n", msg.Where(x => x != null)) : null;
                ViewBag.NomeReduzidoCredores = credores;
                ViewBag.ListaCredores = listaCredores;
                ViewBag.DadoDesdobramentoTipo = InitializeDadoDesdobramentoTipoViewModel(desdobramento, DesdobramentoTipoList);
                ViewBag.PesquisarContrato = InitializePesquisaContratoViewModelViewModel(desdobramento);
                ViewBag.DadoDesdobramento = InitializeDadoDesdobramentoViewModel(desdobramento);
                ViewBag.DadoDesdobramentoTotais = InitializeDadoDesdobramentoTotaisViewModel(desdobramento);
                ViewBag.DadoDesdobramentoIdentificacao = InitializeDadoIdentificacaoDesdobramentoViewModel(desdobramento, new IdentificacaoDesdobramento());
                ViewBag.DadoDesdobramentoIdentificacaoISSQNGrid = desdobramento.DesdobramentoTipoId == 1 ? InitializeDadoIdentificacaoDesdobramentoGridViewModel(desdobramento, desdobramento.IdentificacaoDesdobramentos) : new List<DadoDesdobramentoIdentificacaoViewModel>();
                ViewBag.DadoDesdobramentoIdentificacaoOutrosGrid = desdobramento.DesdobramentoTipoId == 2 ? InitializeDadoIdentificacaoDesdobramentoGridViewModel(desdobramento, desdobramento.IdentificacaoDesdobramentos) : new List<DadoDesdobramentoIdentificacaoViewModel>();
            }


            if (objModel is ReclassificacaoRetencao)
            {
                var reclassificacaoRetencao = (ReclassificacaoRetencao)Convert.ChangeType(objModel, typeof(ReclassificacaoRetencao));
                var msg = new List<string>
                {
                    reclassificacaoRetencao.MensagemServicoSiafem,
                };

                ViewBag.PesquisarContrato = InitializePesquisaContratoViewModelViewModel(reclassificacaoRetencao);
                ViewBag.MsgRetorno = !string.IsNullOrEmpty(reclassificacaoRetencao.MensagemServicoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;
                ViewBag.DadoPagamentoContaUnicaNota = InitializeDadoPagamentoContaUnicaNotaViewModel(reclassificacaoRetencao);
                ViewBag.DadoPagamentoContaUnicaEvento = InitializeDadoPagamentoContaUnicaEventoViewModel(reclassificacaoRetencao);
                ViewBag.DadoPagamentoContaUnicaEventoGrid = InitializeDadoPagamentoContaUnicaEventoGridViewModel(reclassificacaoRetencao);
                ViewBag.DadoObservacao = InitializeDadoObservacaoViewModel(reclassificacaoRetencao);
                ViewBag.PesquisaEmpenhoCredor = InitializePesquisaEmpenhoCredorViewModel(reclassificacaoRetencao);
                ViewBag.DadoReclassificacaoRetencao = InitializeDadoReclassificacaoRetencaoViewModel(reclassificacaoRetencao);
                ViewBag.DadoReclassificacaoRetencaoTipo = InitializeDadoReclassificacaoRetencaoTipoViewModel(reclassificacaoRetencao);
                ViewBag.DadoDesdobramento = InitializeDadoDesdobramentoViewModel(reclassificacaoRetencao);
            }


            if (objModel is ListaBoletos)
            {
                var listaBoleto = (ListaBoletos)Convert.ChangeType(objModel, typeof(ListaBoletos));
                var msg = new List<string>
                {
                    listaBoleto.MensagemServicoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(listaBoleto.MensagemServicoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;

                ViewBag.DadoListaBoletos = InitializeDadoListaBoletosViewModel(listaBoleto);
                ViewBag.DadoCodigoDeBarras = InitializeDadoCodigoDeBarrasViewModel();
                ViewBag.DadoCodigoDeBarrasGrid = InitializeDadoCodigoDeBarrasGridViewModel(listaBoleto);
                ViewBag.DadoListaBoletosValor = InitializeDadoListaBoletosValorViewModel(listaBoleto);
                ViewBag.DadoDesdobramento = InitializeDadoDesdobramentoViewModel(listaBoleto);
            }

            if (objModel is PreparacaoPagamento)
            {
                var preparacaoPagamento = (PreparacaoPagamento)Convert.ChangeType(objModel, typeof(PreparacaoPagamento));
                var msg = new List<string>
                {
                    preparacaoPagamento.MensagemServicoProdesp,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(preparacaoPagamento.MensagemServicoProdesp) ? string.Join("\n", msg.Where(x => x != null)) : null;


                ViewBag.DadoTipoPreparacao = InitializeTipoPreparacaoPagamentoViewModel(preparacaoPagamento, PreparacaoPagamentoTipoList);
                ViewBag.DadoPreparacaoPagamento = InitializeDadoPreparacaoPagamentoViewModel(preparacaoPagamento);
                ViewBag.PesquisaDocumentoGerador = InitializePesquisaDocumentoGeradorViewModel(preparacaoPagamento);
                ViewBag.DadoReferencia = InitializeDadoReferenciaViewModel(preparacaoPagamento.Referencia);
                ViewBag.DadoPreparacaoPagamentoCredor = InitializeDadoPreparacaoPagamentoCredorViewModel(preparacaoPagamento);
                ViewBag.DadoPreparacaoPagamentoContas = InitializeDadoPagamentoContaUnicaContasViewModel(preparacaoPagamento);


                ViewBag.DadoAssinatura = InitializeDadoAssinaturaViewModel(preparacaoPagamento);

            }


            if (objModel is ProgramacaoDesembolso)
            {
                var programacaoDesembolso = (ProgramacaoDesembolso)Convert.ChangeType(objModel, typeof(ProgramacaoDesembolso));
                var msg = new List<string>
                {
                    programacaoDesembolso.MensagemServicoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(programacaoDesembolso.MensagemServicoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;


                ViewBag.DadoTipoProgramacaoDesembolso = InitializeTipoProgramacaoDesembolsoViewModel(programacaoDesembolso);
                ViewBag.DadoProgramacaoDesembolso = InitializeDadoProgramacaoDesembolsoViewModel(programacaoDesembolso);
                ViewBag.DadoProgramacaoDesembolsoPDBEC = InitializeDadoProgramacaoDesembolsoPDBECViewModel(programacaoDesembolso);
                ViewBag.PesquisaDocumentoGerador = InitializePesquisaDocumentoGeradorViewModel(programacaoDesembolso);
                ViewBag.DadoPagamentoContaUnicaContas = InitializeDadoPagamentoContaUnicaContasViewModel(programacaoDesembolso);
                ViewBag.DadoPagamentoContaUnicaEvento = InitializeDadoPagamentoContaUnicaEventoViewModel(programacaoDesembolso);
                ViewBag.DadoPagamentoContaUnicaEventoGrid = InitializeDadoPagamentoContaUnicaEventoGridViewModel(programacaoDesembolso);
                ViewBag.DadoPagamentoContaUnicaAgrupamento = InitializeDadoPagamentoContaUnicaAgrupamentoViewModel(programacaoDesembolso);
            }

            if (objModel is ImpressaoRelacaoReRtConsultaPaiVo)
            {
                var impressaoRelacaoPaiVo = objModel as ImpressaoRelacaoReRtConsultaPaiVo;
                var msg = new List<string>
                {
                    impressaoRelacaoPaiVo.MsgRetornoTransmissaoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(impressaoRelacaoPaiVo.MsgRetornoTransmissaoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;

                ViewBag.PesquisaImpressaoRelacaoRERTPaiVo = InitializePesquisaImpressaoRelacaoRERTVoViewModel(impressaoRelacaoPaiVo);
            }
            else if (objModel is ImpressaoRelacaoReRtConsultaVo)
            {
                var impressaoRelacaoVo = objModel as ImpressaoRelacaoReRtConsultaVo;
                var msg = new List<string>
                {
                    impressaoRelacaoVo.MsgRetornoTransmissaoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(impressaoRelacaoVo.MsgRetornoTransmissaoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;

                ViewBag.PesquisaImpressaoRelacaoRERTVo = InitializePesquisaImpressaoRelacaoRERTVoViewModel(impressaoRelacaoVo);
            }
            else if (objModel is ImpressaoRelacaoRERT)
            {
                var impressaoRelacao = objModel as ImpressaoRelacaoRERT;
                var msg = new List<string>
                {
                    impressaoRelacao.MsgRetornoTransmissaoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(impressaoRelacao.MsgRetornoTransmissaoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;
                
                ViewBag.PesquisaImpressaoRelacaoRERT = InitializePesquisaImpressaoRelacaoRERTViewModel(impressaoRelacao);
            }

            if (objModel is DadoImpressaoRelacaoReRtConsultaViewModel)
            {
                var impressaoRelacaoPaiVo = new ImpressaoRelacaoReRtConsultaPaiVo();
                var msg = new List<string>
                {
                    impressaoRelacaoPaiVo.MsgRetornoTransmissaoSiafem,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(impressaoRelacaoPaiVo.MsgRetornoTransmissaoSiafem) ? string.Join("\n", msg.Where(x => x != null)) : null;

                ViewBag.PesquisaImpressaoRelacaoRERTCadastrar = InitializePesquisaImpressaoRelacaoRERTVoViewModel(impressaoRelacaoPaiVo);
            }

            InitializeCommonBags(objModel);

            return objModel;
        }

        protected T Display<T>(T objModel, bool isNewRecord, string tipoDesdobramento) where T : IPagamentoContaUnica
        {

            var desdobramento = (Desdobramento)Convert.ChangeType(objModel, typeof(Desdobramento));
            var msg = new List<string>
                {
                    desdobramento.MensagemServicoProdesp,
                };
            ViewBag.MsgRetorno = !string.IsNullOrEmpty(desdobramento.MensagemServicoProdesp) ? string.Join("\n", msg.Where(x => x != null)) : null;
            ViewBag.NomeReduzidoCredores = CredorList.Select(x => x.NomeReduzidoCredor).ToList();
            ViewBag.DadoDesdobramento = InitializeDadoDesdobramentoViewModel(desdobramento);
            ViewBag.DadoDesdobramentoCancelarGrid = InitializeDadoDesdobramentoCancelarGridViewModel(desdobramento.IdentificacaoDesdobramentos, desdobramento);

            InitializeCommonBags(objModel);
            return objModel;
        }




        protected IEnumerable<FiltroGridViewModel> Display(IPagamentoContaUnica entity)

        {

            var filterModel = InitializeEntityModel(entity);
            InitializeCommonBags(entity);

            if (entity is Desdobramento)
            {
                var desdobramento = (Desdobramento)Convert.ChangeType(filterModel, typeof(Desdobramento));
                ViewBag.Filtro = InitializeFiltroViewModel(desdobramento);

            }


            if (entity is ListaBoletos)
            {
                var listaBoletos = (ListaBoletos)Convert.ChangeType(filterModel, typeof(ListaBoletos));
                ViewBag.Filtro = InitializeFiltroViewModel(listaBoletos);
            }

            if (entity is PreparacaoPagamento)
            {
                var preparacaoPagamento = (PreparacaoPagamento)Convert.ChangeType(filterModel, typeof(PreparacaoPagamento));
                ViewBag.Filtro = InitializeFiltroViewModel(preparacaoPagamento);
            }


            if (entity is ProgramacaoDesembolso)
            {
                var programacaoDesembolso = (ProgramacaoDesembolso)Convert.ChangeType(filterModel, typeof(ProgramacaoDesembolso));
                ViewBag.Filtro = InitializeFiltroViewModel(programacaoDesembolso);
            }

            if (entity is PDExecucaoItem)
            {
                var programacaoDesembolso = (PDExecucaoItem)Convert.ChangeType(filterModel, typeof(PDExecucaoItem));
                ViewBag.Filtro = InitializeFiltroViewModel(programacaoDesembolso, null);
            }

            if (entity is OBAutorizacao)
            {
                var autorizacaoDeOB = (OBAutorizacao)Convert.ChangeType(filterModel, typeof(OBAutorizacao));
                ViewBag.Filtro = InitializeFiltroViewModel(autorizacaoDeOB);
            }

            if (entity is OBAutorizacaoItem)
            {
                var autorizacaoDeOB = (OBAutorizacaoItem)Convert.ChangeType(filterModel, typeof(OBAutorizacaoItem));
                ViewBag.Filtro = InitializeFiltroViewModel(autorizacaoDeOB);
            }

            if (entity is ImpressaoRelacaoReRtConsultaVo)
            {
                var impressaoRelacaoReRtConsultaVo = filterModel as ImpressaoRelacaoReRtConsultaVo;
                ViewBag.Filtro = InitializeFiltroViewModel(impressaoRelacaoReRtConsultaVo);
            }
            else if (entity is ImpressaoRelacaoRERT)
            {
                var impressaoRelacaoRERT = (ImpressaoRelacaoRERT)Convert.ChangeType(filterModel, typeof(ImpressaoRelacaoRERT));
                ViewBag.Filtro = InitializeFiltroViewModel(impressaoRelacaoRERT);
            }

            return new List<FiltroGridViewModel>();
        }

        protected IEnumerable<FiltroGridViewModel> Display(ReclassificacaoRetencao entity)
        {
            var filterModel = InitializeEntityModel(entity);
            InitializeCommonBags(entity);

            if (entity is ReclassificacaoRetencao)
            {
                var reclassificacao = (ReclassificacaoRetencao)Convert.ChangeType(filterModel, typeof(ReclassificacaoRetencao));
                ViewBag.Filtro = InitializeFiltroViewModel(reclassificacao);
            }
            return new List<FiltroGridViewModel>();
        }

        protected IEnumerable<FiltroGridViewModel> Display<T>(T entity, FormCollection form) where T : IPagamentoContaUnica
        {
            IEnumerable<IPagamentoContaUnica> entities = new List<IPagamentoContaUnica>();

            T model = GenerateFilterViewModel(form, entity);

            var usuario = App.AutenticacaoService.GetUsuarioLogado();

            var entityDesdobramento = entity as Desdobramento;
            if (entityDesdobramento != null)
            {
                var inclusao = (Desdobramento)Convert.ChangeType(model, typeof(Desdobramento));
                if (usuario.RegionalId != null) inclusao.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                entities = App.DesdobramentoService.BuscarGrid(inclusao, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }

            var entityReclassificacaoRetencao = entity as ReclassificacaoRetencao;
            if (entityReclassificacaoRetencao != null)
            {
                var inclusao = (ReclassificacaoRetencao)Convert.ChangeType(model, typeof(ReclassificacaoRetencao));
                if (usuario.RegionalId != null)
                    inclusao.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                entities = App.ReclassificacaoRetencaoService.BuscarGrid(inclusao, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
             }

            var entityListaBoletos = entity as ListaBoletos;
            if (entityListaBoletos != null)
            {
                var inclusao = (ListaBoletos)Convert.ChangeType(model, typeof(ListaBoletos));
                if (usuario.RegionalId != null)
                    inclusao.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                entities = App.ListaBoletosService.BuscarGrid(inclusao, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }

            var entityPreparacaoPagamento = entity as PreparacaoPagamento;
            if (entityPreparacaoPagamento != null)
            {
                var inclusao = (PreparacaoPagamento)Convert.ChangeType(model, typeof(PreparacaoPagamento));
                if (usuario.RegionalId != null)
                    inclusao.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                entities = App.PreparacaoPagamentoService.BuscarGrid(inclusao, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }

            var entityProgramacaoDesembolso = entity as ProgramacaoDesembolso;
            if (entityProgramacaoDesembolso != null)
            {
                var inclusao = (ProgramacaoDesembolso)Convert.ChangeType(model, typeof(ProgramacaoDesembolso));
                entities = App.ProgramacaoDesembolsoService.BuscarGrid(inclusao, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }

            var entityPDExecucaoItem = entity as PDExecucaoItem;
            if (entityPDExecucaoItem != null)
            {
                var inclusao = (PDExecucaoItem)Convert.ChangeType(model, typeof(PDExecucaoItem));

                FiltroViewModel filtro = ((FiltroViewModel)ViewBag.Filtro);

                DateTime? de = null;
                DateTime? ate = null;

                if (filtro.CadastradoEmDe != null)
                    de = filtro.CadastradoEmDe;

                if (filtro.CadastradoEmAte != null)
                    ate = filtro.CadastradoEmAte;

                var tipoExecucao = filtro.TipoExecucao;

                entities = App.ProgramacaoDesembolsoExecucaoService.ConsultarItems(inclusao, tipoExecucao, de, ate).ToList();
            }

            if (entity is OBAutorizacao)
            {
                var inclusao = (OBAutorizacao)Convert.ChangeType(model, typeof(OBAutorizacao));

                FiltroViewModel filtro = ((FiltroViewModel)ViewBag.Filtro);

                DateTime? de = null;
                DateTime? ate = null;

                if (filtro.CadastradoEmDe != null)
                    de = Convert.ToDateTime(filtro.CadastradoEmDe);

                if (filtro.CadastradoEmAte != null)
                    ate = Convert.ToDateTime(filtro.CadastradoEmAte);

                entities = App.AutorizacaoDeOBService.ConsultarItensAgrupadosPorOB(inclusao, de, ate).ToList();
            }

            if (entity is OBAutorizacaoItem)
            {
                var inclusao = (OBAutorizacaoItem)Convert.ChangeType(model, typeof(OBAutorizacaoItem));

                FiltroViewModel filtro = ((FiltroViewModel)ViewBag.Filtro);

                DateTime? de = null;
                DateTime? ate = null;

                if (filtro.CadastradoEmDe != null)
                    de = Convert.ToDateTime(filtro.CadastradoEmDe);

                if (filtro.CadastradoEmAte != null)
                    ate = Convert.ToDateTime(filtro.CadastradoEmAte);

                var tipoExecucao = filtro.TipoExecucao;

                entities = App.AutorizacaoDeOBService.ConsultarItems(inclusao, tipoExecucao, de, ate).ToList();
            }

            if (entity is ImpressaoRelacaoReRtConsultaVo)
            {
                var impressaoRelacaoReRtConsultaVo = entity as ImpressaoRelacaoReRtConsultaVo;
                if (usuario.RegionalId != null)
                {
                    impressaoRelacaoReRtConsultaVo.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                }

                entities = App.ImpressaoRelacaoRERTService.BuscarGrid(impressaoRelacaoReRtConsultaVo, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }
            else if (entity is ImpressaoRelacaoRERT)
            {
                var entityImpressaoRelacaoRERT = entity as ImpressaoRelacaoRERT;
                if (usuario.RegionalId != null)
                {
                    entityImpressaoRelacaoRERT.RegionalId = (short)usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                }
                entities = App.ImpressaoRelacaoRERTService.BuscarGrid(entityImpressaoRelacaoRERT, Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte));
            }

            return InitializeFiltroGridViewModel(entities);
        }



        protected T GenerateFilterViewModel<T>(FormCollection form, T entity) where T : IPagamentoContaUnica
        {
            FiltroViewModel filter = new FiltroViewModel();

            if (entity is Desdobramento)
            {
                filter = InitializeFiltroViewModel(entity as Desdobramento);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
            }
            else
            if (entity is ReclassificacaoRetencao)
            {
                filter = InitializeFiltroViewModel(entity as ReclassificacaoRetencao);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
            }
            else
            if (entity is ListaBoletos)
            {
                filter = InitializeFiltroViewModel(entity as ListaBoletos);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
            }
            else
            if (entity is PreparacaoPagamento)
            {
                filter = InitializeFiltroViewModel(entity as PreparacaoPagamento);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
            }
            else
            if (entity is ProgramacaoDesembolso)
            {
                filter = InitializeFiltroViewModel(entity as ProgramacaoDesembolso);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
                ViewBag.Usuario = _userLoggedIn;
            }
            else
            if (entity is PDExecucaoItem)
            {
                filter = InitializeFiltroViewModel(entity as PDExecucaoItem, null);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
                ViewBag.Usuario = _userLoggedIn;
            }
            else
            if (entity is OBAutorizacao)
            {
                filter = InitializeFiltroViewModel(entity as OBAutorizacao);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
                ViewBag.Usuario = _userLoggedIn;
            }
            else
            if (entity is OBAutorizacaoItem)
            {
                filter = InitializeFiltroViewModel(entity as OBAutorizacaoItem);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
                ViewBag.Usuario = _userLoggedIn;
            }
            else
            if (entity is ImpressaoRelacaoRERT)
            {
                filter = InitializeFiltroViewModel(entity as ImpressaoRelacaoRERT);
                entity = MapViewModelToEntityModel(form, entity, ref filter);
                ViewBag.Filtro = filter;
                ViewBag.Usuario = _userLoggedIn;
            }

            return entity;
        }

        protected Empenho.Models.DadoAssinaturaViewModel GetSignaturesFromDomainModel(PreparacaoPagamento entity)
        {

            var autorizado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoAutorizadoAssinatura,
                CodigoGrupo = Convert.ToInt32(entity.CodigoAutorizadoGrupo),
                CodigoOrgao = entity.CodigoAutorizadoOrgao,
                NomeAssinatura = entity.NomeAutorizadoAssinatura,
                DescricaoCargo = entity.DescricaoAutorizadoCargo
            };

            var examinado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoExaminadoAssinatura,
                CodigoGrupo = Convert.ToInt32(entity.CodigoExaminadoGrupo),
                CodigoOrgao = entity.CodigoExaminadoOrgao,
                NomeAssinatura = entity.NomeExaminadoAssinatura,
                DescricaoCargo = entity.DescricaoExaminadoCargo
            };

            return new Empenho.Models.DadoAssinaturaViewModel().CreateInstance(autorizado, examinado, new Assinatura { });

        }

        protected void InitializeCommonBags<T>(T entity)
        {
            ViewBag.Usuario = _userLoggedIn;
        }

        protected Empenho.Models.DadoAssinaturaViewModel InitializeDadoAssinaturaViewModel(PreparacaoPagamento entity)
        {
            return GetSignaturesFromDomainModel(entity);
        }

        protected IEnumerable<DadoDesdobramentoCancelarGridViewModel> InitializeDadoDesdobramentoCancelarGridViewModel(IEnumerable<IdentificacaoDesdobramento> entities, Desdobramento entity)
        {
            var itens = entities.Select(x => new DadoDesdobramentoCancelarGridViewModel().CreateInstance(x, entity, ReterList, DesdobramentoTipoList)).ToList();
            return itens;
        }

        protected DadoDesdobramentoTipoViewModel InitializeDadoDesdobramentoTipoViewModel(Desdobramento entity, IEnumerable<DesdobramentoTipo> desdobramentoTipoList)
        {
            return new DadoDesdobramentoTipoViewModel().CreateInstance(entity, desdobramentoTipoList);
        }

        protected DadoDesdobramentoTotaisViewModel InitializeDadoDesdobramentoTotaisViewModel(Desdobramento entity)
        {
            return new DadoDesdobramentoTotaisViewModel().CreateInstance(entity);
        }

        protected DadoDesdobramentoViewModel InitializeDadoDesdobramentoViewModel(Desdobramento entity)
        {
            return new DadoDesdobramentoViewModel().CreateInstance(entity, DesdobramentoTipoList, DocumentoTipoList);
        }

        protected DadoDesdobramentoViewModel InitializeDadoDesdobramentoViewModel(IPagamentoContaUnicaSiafem entity)
        {
            return new DadoDesdobramentoViewModel().CreateInstance(entity, DocumentoTipoList);
        }

        protected IEnumerable<DadoDesdobramentoIdentificacaoViewModel> InitializeDadoIdentificacaoDesdobramentoGridViewModel(Desdobramento desdobramento, IEnumerable<IdentificacaoDesdobramento> entities)
        {
            var itens = entities.Select(entity => new DadoDesdobramentoIdentificacaoViewModel().CreateInstance(desdobramento, entity, DesdobramentoTipoList, ReterList)).ToList();
            return itens;
        }

        protected DadoDesdobramentoIdentificacaoViewModel InitializeDadoIdentificacaoDesdobramentoViewModel(Desdobramento desdobramento, IdentificacaoDesdobramento entity)
        {
            return new DadoDesdobramentoIdentificacaoViewModel().CreateInstance(desdobramento, entity, DesdobramentoTipoList, ReterList);
        }

        protected DadoObservacaoViewModel InitializeDadoObservacaoViewModel(ReclassificacaoRetencao entity)
        {
            return new DadoObservacaoViewModel().CreateInstance(entity);
        }

        protected DadoPagamentoContaUnicaContasViewModel InitializeDadoPagamentoContaUnicaContasViewModel(PreparacaoPagamento entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel().CreateInstance(entity);
        }

        protected DadoPagamentoContaUnicaContasViewModel InitializeDadoPagamentoContaUnicaContasViewModel(ProgramacaoDesembolso entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel().CreateInstance(entity);
        }

        protected IEnumerable<DadoPagamentoContaUnicaEventoViewModel> InitializeDadoPagamentoContaUnicaEventoGridViewModel(ReclassificacaoRetencao entity)
        {
            return entity.Eventos.Select(model => new DadoPagamentoContaUnicaEventoViewModel().CreateInstance(model)).ToList();
        }

        protected IEnumerable<DadoPagamentoContaUnicaEventoViewModel> InitializeDadoPagamentoContaUnicaEventoGridViewModel(ProgramacaoDesembolso entity)
        {
            return entity.Eventos.Select(model => new DadoPagamentoContaUnicaEventoViewModel().CreateInstance(model)).ToList();
        }

        protected DadoPagamentoContaUnicaEventoViewModel InitializeDadoPagamentoContaUnicaEventoViewModel<T>(T entity) where T : IPagamentoContaUnica
        {
            return new DadoPagamentoContaUnicaEventoViewModel();
        }

        protected DadoPagamentoContaUnicaNotaViewModel InitializeDadoPagamentoContaUnicaNotaViewModel<T>(T entity) where T : ReclassificacaoRetencao
        {
            return new DadoPagamentoContaUnicaNotaViewModel().CreateInstance(entity)
                ?? new DadoPagamentoContaUnicaNotaViewModel();
        }

        protected DadoPreparacaoPagamentoCredorViewModel InitializeDadoPreparacaoPagamentoCredorViewModel(PreparacaoPagamento entity)
        {
            return new DadoPreparacaoPagamentoCredorViewModel().CreateInstance(entity);
        }

        protected DadoPreparacaoPagamentoViewModel InitializeDadoPreparacaoPagamentoViewModel(PreparacaoPagamento entity)
        {
            return new DadoPreparacaoPagamentoViewModel().CreateInstance(entity, RegionalList, _userLoggedIn);
        }

        protected DadoReclassificacaoRetencaoTipoViewModel InitializeDadoReclassificacaoRetencaoTipoViewModel(ReclassificacaoRetencao entity)
        {
            return new DadoReclassificacaoRetencaoTipoViewModel().CreateInstance(entity, ReclassificacaoRetencaoTipoList);
        }

        protected DadoReclassificacaoRetencaoViewModel InitializeDadoReclassificacaoRetencaoViewModel(ReclassificacaoRetencao entity)
        {
            return new DadoReclassificacaoRetencaoViewModel().CreateInstance(entity, ParaRestoAPagarList);
        }

        protected LiquidacaoDespesa.Models.DadoReferenciaViewModel InitializeDadoReferenciaViewModel(string entity)
        {
            return new LiquidacaoDespesa.Models.DadoReferenciaViewModel().CreateInstance(entity);
        }

        protected T InitializeEntityModel<T>(T objModel) where T : IPagamentoContaUnica
        {
            var model = new object();

            if (objModel is Desdobramento)
            {
                var desdobramento = (Desdobramento)Convert.ChangeType(objModel, typeof(Desdobramento));
                desdobramento.Id = default(int);
                desdobramento.DataTransmitidoProdesp = default(DateTime);
                desdobramento.TransmitidoProdesp = false;
                desdobramento.DataEmissao = DateTime.Now;
                desdobramento.StatusProdesp = "N";
                desdobramento.MensagemServicoProdesp = default(string);
                desdobramento.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                desdobramento.ValorInss = 0;
                desdobramento.ValorIr = 0;
                desdobramento.ValorIssqn = 0;
                desdobramento.DataCadastro = default(DateTime);
                desdobramento.TransmitirProdesp = true;
                desdobramento.SituacaoDesdobramento = "N";
                desdobramento.IdentificacaoDesdobramentos.ForEach(x => x.ValorDesdobrado = x.ValorDesdobradoInicial);
                desdobramento.IdentificacaoDesdobramentos.ForEach(x => x.Id = 0);
                desdobramento.IdentificacaoDesdobramentos.ForEach(x => x.Desdobramento = 0);
                if (desdobramento.DesdobramentoTipoId == 1)
                    desdobramento.IdentificacaoDesdobramentos.Where(x => (CredorList.FirstOrDefault(c => c.NomeReduzidoCredor == x.NomeReduzidoCredor)?.BaseCalculo ?? false)).ForEach(x => x.ValorPercentual = 0);
                if (desdobramento.DesdobramentoTipoId == 2)
                    desdobramento.IdentificacaoDesdobramentos.Where(x => x.ValorDesdobradoInicial > 0).ForEach(x => x.ValorPercentual = 0);
                model = desdobramento;
            }


            if (objModel is ReclassificacaoRetencao)
            {
                var retencao = (ReclassificacaoRetencao)Convert.ChangeType(objModel, typeof(ReclassificacaoRetencao));
                retencao.Id = default(int);
                retencao.DataTransmitidoSiafem = default(DateTime);
                retencao.TransmitidoSiafem = false;
                retencao.DataEmissao = DateTime.Now;
                retencao.MensagemServicoSiafem = default(string);
                retencao.NumeroSiafem = default(string);
                retencao.Eventos.ForEach(x => x.Id = 0);
                retencao.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                retencao.CodigoUnidadeGestora = RegionalList.FirstOrDefault(x => x.Id == _userLoggedIn.RegionalId)?.Uge;
                retencao.CodigoGestao = "16055";
                retencao.DataCadastro = default(DateTime);
                retencao.TransmitirSiafem = true;
                model = retencao;
            }


            if (objModel is ListaBoletos)
            {
                var listaBoletos = (ListaBoletos)Convert.ChangeType(objModel, typeof(ListaBoletos));
                listaBoletos.Id = default(int);
                listaBoletos.DataTransmitidoSiafem = default(DateTime);
                listaBoletos.TransmitidoSiafem = false;
                listaBoletos.DataEmissao = DateTime.Now;
                listaBoletos.MensagemServicoSiafem = default(string);
                listaBoletos.NumeroSiafem = default(string);
                listaBoletos.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                listaBoletos.CodigoUnidadeGestora = RegionalList.FirstOrDefault(x => x.Id == _userLoggedIn.RegionalId)?.Uge;
                listaBoletos.CodigoGestao = "16055";
                listaBoletos.DataCadastro = default(DateTime);
                listaBoletos.TransmitirSiafem = true;

                var listacodigoBarras = listaBoletos.ListaCodigoBarras.ToList();

                foreach (var item in listacodigoBarras)
                {
                    item.Id = 0;
                    if (item.CodigoBarraBoleto != null)
                    {
                        item.CodigoBarraBoleto.Id = 0;
                        item.CodigoBarraBoleto.CodigoBarraId = 0;
                    }

                    if (item.CodigoBarraTaxa != null)
                    {
                        item.CodigoBarraTaxa.Id = 0;
                        item.CodigoBarraTaxa.CodigoBarraId = 0;
                    }
                    item.TransmitidoSiafem = false;
                }

                listaBoletos.ListaCodigoBarras = listacodigoBarras;

                listaBoletos.ValorTotalLista = 0;
                listaBoletos.TotalCredores = 0;
                model = listaBoletos;
            }

            if (objModel is PreparacaoPagamento)
            {
                var preparacaoPagamento = (PreparacaoPagamento)Convert.ChangeType(objModel, typeof(PreparacaoPagamento));
                preparacaoPagamento.Id = default(int);
                preparacaoPagamento.DataEmissao = DateTime.Now;
                preparacaoPagamento.DataTransmitidoProdesp = default(DateTime);
                preparacaoPagamento.TransmitidoProdesp = false;
                preparacaoPagamento.MensagemServicoProdesp = default(string);
                preparacaoPagamento.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                preparacaoPagamento.DataCadastro = default(DateTime);
                preparacaoPagamento.TransmitirProdesp = true;

                preparacaoPagamento.NumeroOpInicial = default(string);
                preparacaoPagamento.NumeroOpFinal = default(string);
                preparacaoPagamento.QuantidadeOpPreparada = default(int);
                preparacaoPagamento.ValorTotal = default(int);

                model = preparacaoPagamento;
            }



            if (objModel is ProgramacaoDesembolso)
            {
                var programacaoDesembolso = (ProgramacaoDesembolso)Convert.ChangeType(objModel, typeof(ProgramacaoDesembolso));
                programacaoDesembolso.Id = default(int);
                programacaoDesembolso.NumeroSiafem = default(string);
                programacaoDesembolso.DataEmissao = DateTime.Now;
                programacaoDesembolso.DataTransmitidoSiafem = default(DateTime);
                programacaoDesembolso.MensagemServicoSiafem = default(string);
                programacaoDesembolso.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                programacaoDesembolso.DataCadastro = default(DateTime);
                programacaoDesembolso.DataVencimento = default(DateTime);
                programacaoDesembolso.CodigoUnidadeGestora = RegionalList.FirstOrDefault(x => x.Id == _userLoggedIn.RegionalId)?.Uge;
                programacaoDesembolso.CodigoGestao = "16055";
                programacaoDesembolso.DataCadastro = default(DateTime);
                programacaoDesembolso.TransmitirSiafem = true;
                programacaoDesembolso.TransmitidoSiafem = false;
                programacaoDesembolso.Eventos.ForEach(x => x.Id = 0);
                programacaoDesembolso.Agrupamentos.ForEach(x => x.Id = 0);
                model = programacaoDesembolso;
            }

            if (objModel is PDExecucaoItem)
            {
                var programacaoDesembolsoExecucaoItem = (PDExecucaoItem)Convert.ChangeType(objModel, typeof(PDExecucaoItem));

                //programacaoDesembolsoExecucaoItem.NumOB = default(string);
                programacaoDesembolsoExecucaoItem.NumOBItem = default(string);
                programacaoDesembolsoExecucaoItem.NumPD = default(string);
                programacaoDesembolsoExecucaoItem.fl_transmissao_transmitido_siafem = false;
                programacaoDesembolsoExecucaoItem.cd_transmissao_status_siafem = "N";
                model = programacaoDesembolsoExecucaoItem;
            }

            if (objModel is OBAutorizacao)
            {
                var autorizacaoOB = (OBAutorizacao)Convert.ChangeType(objModel, typeof(OBAutorizacao));

                autorizacaoOB.NumOB = default(string);
                //autorizacaoOB.NumPD = default(string);
                autorizacaoOB.DataCadastro = default(DateTime);
                autorizacaoOB.TransmissaoTransmitidoSiafem = false;
                //autorizacaoOB.fl_transmissao_transmitido_prodesp = false;
                autorizacaoOB.TransmissaoStatusSiafem = "N";
                //autorizacaoOB.cd_transmissao_status_prodesp = "N";
                model = autorizacaoOB;
            }

            if (objModel is OBAutorizacaoItem)
            {
                var autorizacaoOBItem = (OBAutorizacaoItem)Convert.ChangeType(objModel, typeof(OBAutorizacaoItem));

                autorizacaoOBItem.NumOB = default(string);
                autorizacaoOBItem.NumPD = default(string);
                autorizacaoOBItem.DataCadastro = default(DateTime);
                autorizacaoOBItem.TransmissaoItemTransmitidoSiafem = false;
                autorizacaoOBItem.TransmissaoItemTransmitidoProdesp = false;
                autorizacaoOBItem.TransmissaoItemStatusSiafem = "N";
                autorizacaoOBItem.TransmissaoItemStatusProdesp = "N";
                model = autorizacaoOBItem;
            }

            if (objModel is ConfirmacaoPagamento)
            {
                var confirmacaoPagamento = (ConfirmacaoPagamento)Convert.ChangeType(objModel, typeof(ConfirmacaoPagamento));

                confirmacaoPagamento.AnoReferencia = 2018;
                confirmacaoPagamento.IdTipoDocumento = 5;
                confirmacaoPagamento.NumeroDocumento = "123456789";
                confirmacaoPagamento.RegionalId = 1;
                confirmacaoPagamento.TipoPagamento = 11;

            }

            if (objModel is DadoImpressaoRelacaoReRtConsultaViewModel)
            {
                var impressaoRelacaoReRtConsultaPaiVo = objModel as DadoImpressaoRelacaoReRtConsultaViewModel;
                impressaoRelacaoReRtConsultaPaiVo.Id = default(int);
                impressaoRelacaoReRtConsultaPaiVo.CodigoUnidadeGestora = default(string);
                impressaoRelacaoReRtConsultaPaiVo.CodigoGestao = default(string);
                impressaoRelacaoReRtConsultaPaiVo.CodigoBanco = default(string);
                impressaoRelacaoReRtConsultaPaiVo.DataEmissao = DateTime.Now;
                impressaoRelacaoReRtConsultaPaiVo.FlagTransmitidoSiafem = false;
                impressaoRelacaoReRtConsultaPaiVo.DataTransmissaoSiafem = default(DateTime);
                impressaoRelacaoReRtConsultaPaiVo.MsgRetornoTransmissaoSiafem = default(string);

                model = impressaoRelacaoReRtConsultaPaiVo;
            }

            if (objModel is ImpressaoRelacaoReRtConsultaVo)
            {
                var impressaoRelacaoReRtConsultaVo = objModel as ImpressaoRelacaoReRtConsultaVo;
                impressaoRelacaoReRtConsultaVo.Id = default(int);
                impressaoRelacaoReRtConsultaVo.CodigoRelatorio = default(string);
                impressaoRelacaoReRtConsultaVo.CodigoRelacaoRERT = default(string);
                impressaoRelacaoReRtConsultaVo.CodigoOB = default(string);
                impressaoRelacaoReRtConsultaVo.NumeroAgrupamento = default(int);
                impressaoRelacaoReRtConsultaVo.CodigoUnidadeGestora = default(string);
                impressaoRelacaoReRtConsultaVo.NomeUnidadeGestora = default(string);
                impressaoRelacaoReRtConsultaVo.CodigoGestao = default(string);
                impressaoRelacaoReRtConsultaVo.NomeGestao = default(string);
                impressaoRelacaoReRtConsultaVo.CodigoBanco = default(string);
                impressaoRelacaoReRtConsultaVo.NomeBanco = default(string);
                impressaoRelacaoReRtConsultaVo.TextoAutorizacao = default(string);
                impressaoRelacaoReRtConsultaVo.Cidade = default(string);
                impressaoRelacaoReRtConsultaVo.NomeGestorFinanceiro = default(string);
                impressaoRelacaoReRtConsultaVo.NomeOrdenadorAssinatura = default(string);
                impressaoRelacaoReRtConsultaVo.DataSolicitacao = default(DateTime);
                impressaoRelacaoReRtConsultaVo.DataReferencia = default(DateTime);
                impressaoRelacaoReRtConsultaVo.DataCadastramento = default(DateTime);
                impressaoRelacaoReRtConsultaVo.DataEmissao = DateTime.Now;
                impressaoRelacaoReRtConsultaVo.ValorTotalDocumento = default(decimal);
                impressaoRelacaoReRtConsultaVo.ValorExtenso = default(string);
                impressaoRelacaoReRtConsultaVo.FlagTransmitidoSiafem = false;
                impressaoRelacaoReRtConsultaVo.FlagTransmitirSiafem = true;
                impressaoRelacaoReRtConsultaVo.DataTransmissaoSiafem = default(DateTime);
                impressaoRelacaoReRtConsultaVo.StatusSiafem = null;
                impressaoRelacaoReRtConsultaVo.MsgRetornoTransmissaoSiafem = default(string);
                impressaoRelacaoReRtConsultaVo.FlagCancelamentoRERT = null;
                impressaoRelacaoReRtConsultaVo.Agencia = default(string);
                impressaoRelacaoReRtConsultaVo.NomeAgencia = default(string);
                impressaoRelacaoReRtConsultaVo.NumeroConta = default(string);

                model = impressaoRelacaoReRtConsultaVo;
            }
            else if (objModel is ImpressaoRelacaoRERT)
            {
                var impressaoRelacaoRERT = (ImpressaoRelacaoRERT)Convert.ChangeType(objModel, typeof(ImpressaoRelacaoRERT));
                impressaoRelacaoRERT.Id = default(int);
                impressaoRelacaoRERT.CodigoRelatorio = default(string);
                impressaoRelacaoRERT.CodigoRelacaoRERT = default(string);
                impressaoRelacaoRERT.CodigoOB = default(string);
                impressaoRelacaoRERT.NumeroAgrupamento = default(int);
                impressaoRelacaoRERT.CodigoUnidadeGestora = default(string);
                impressaoRelacaoRERT.NomeUnidadeGestora = default(string);
                impressaoRelacaoRERT.CodigoGestao = default(string);
                impressaoRelacaoRERT.NomeGestao = default(string);
                impressaoRelacaoRERT.CodigoBanco = default(string);
                impressaoRelacaoRERT.NomeBanco = default(string);
                impressaoRelacaoRERT.TextoAutorizacao = default(string);
                impressaoRelacaoRERT.Cidade = default(string);
                impressaoRelacaoRERT.NomeGestorFinanceiro = default(string);
                impressaoRelacaoRERT.NomeOrdenadorAssinatura = default(string);
                impressaoRelacaoRERT.DataSolicitacao = default(DateTime);
                impressaoRelacaoRERT.DataReferencia = default(DateTime);
                impressaoRelacaoRERT.DataCadastramento = default(DateTime);
                impressaoRelacaoRERT.DataEmissao = DateTime.Now;
                impressaoRelacaoRERT.ValorTotalDocumento = default(decimal);
                impressaoRelacaoRERT.ValorExtenso = default(string);
                impressaoRelacaoRERT.FlagTransmitidoSiafem = false;
                impressaoRelacaoRERT.FlagTransmitirSiafem = true;
                impressaoRelacaoRERT.DataTransmissaoSiafem = default(DateTime);
                impressaoRelacaoRERT.StatusSiafem = null;
                impressaoRelacaoRERT.MsgRetornoTransmissaoSiafem = default(string);
                impressaoRelacaoRERT.FlagCancelamentoRERT = null;
                impressaoRelacaoRERT.Agencia = default(string);
                impressaoRelacaoRERT.NomeAgencia = default(string);
                impressaoRelacaoRERT.NumeroConta = default(string);

                model = impressaoRelacaoRERT;
            }

            return (T)model;
        }
        protected IEnumerable<FiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<IPagamentoContaUnica> entities)
        {
            List<FiltroGridViewModel> items = new List<FiltroGridViewModel>();

            foreach (var entity in entities)
            {
                if (entity is Desdobramento)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((Desdobramento)entity, DesdobramentoTipoList, DocumentoTipoList));
                }

                if (entity is ReclassificacaoRetencao)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((ReclassificacaoRetencao)entity, ReclassificacaoRetencaoTipoList, ReclassificacaoRetencaoEventoList));
                }

                if (entity is ListaBoletos)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((ListaBoletos)entity));
                }

                if (entity is PreparacaoPagamento)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((PreparacaoPagamento)entity));
                }

                if (entity is ProgramacaoDesembolso)
                {
                    items.AddRange(new FiltroGridViewModel().CreateInstance((ProgramacaoDesembolso)entity, DocumentoTipoList));
                }

                if (entity is PDExecucaoItem)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((PDExecucaoItem)entity));
                }

                if (entity is OBAutorizacao)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((OBAutorizacao)entity));
                }

                if (entity is OBAutorizacaoItem)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((OBAutorizacaoItem)entity));
                }

                if (entity is ImpressaoRelacaoRERT)
                {
                    items.Add(new FiltroGridViewModel().CreateInstance((ImpressaoRelacaoRERT)entity));
                }
            }

            return items;
        }

        protected FiltroViewModel InitializeFiltroViewModel(Desdobramento entity)
        {
            return new FiltroViewModel().CreateInstance(entity, DesdobramentoTipoList, DocumentoTipoList, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(ListaBoletos entity)
        {
            return new FiltroViewModel().CreateInstance(entity, TipoBoletoList, DocumentoTipoList, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(PreparacaoPagamento entity)
        {
            return new FiltroViewModel().CreateInstance(entity, PreparacaoPagamentoTipoList, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(ProgramacaoDesembolso entity)
        {
            return new FiltroViewModel().CreateInstance(entity, ProgramacaoDesembolsoTipoList, DocumentoTipoList, RegionalList, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(PDExecucaoItem entity, int? tipoExecucao)
        {
            return new FiltroViewModel().CreateInstance(entity, tipoExecucao, TiposExecucao, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(OBAutorizacao entity)
        {
            return new FiltroViewModel().CreateInstance(entity, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(OBAutorizacaoItem entity)
        {
            return new FiltroViewModel().CreateInstance(entity, new DateTime(), new DateTime());
        }

        protected FiltroViewModel InitializeFiltroViewModel(ReclassificacaoRetencao entity)
        {
            return new FiltroViewModel().CreateInstance(entity, ReclassificacaoRetencaoTipoList, new DateTime(), new DateTime());
        }

        protected PesquisaContratoViewModel InitializePesquisaContratoViewModelViewModel<T>(T entity) where T : IPagamentoContaUnica
        {
            return new PesquisaContratoViewModel().CreateInstance(entity);
        }

        protected PesquisaDocumentoGeradorViewModel InitializePesquisaDocumentoGeradorViewModel(PreparacaoPagamento entity)
        {
            return new PesquisaDocumentoGeradorViewModel().CreateInstance(entity, DocumentoTipoList);
        }

        protected PesquisaDocumentoGeradorViewModel InitializePesquisaDocumentoGeradorViewModel(ProgramacaoDesembolso entity)
        {
            return new PesquisaDocumentoGeradorViewModel().CreateInstance(entity, DocumentoTipoList);
        }

        protected PesquisaEmpenhoCredorViewModel InitializePesquisaEmpenhoCredorViewModel(ReclassificacaoRetencao entity)
        {
            return new PesquisaEmpenhoCredorViewModel().CreateInstance(entity);
        }

        protected DadoTipoPreparacaoPagamentoViewModel InitializeTipoPreparacaoPagamentoViewModel(PreparacaoPagamento entity, IEnumerable<PreparacaoPagamentoTipo> PreparacaoPagamentoTipoList)
        {
            return new DadoTipoPreparacaoPagamentoViewModel().CreateInstance(entity, PreparacaoPagamentoTipoList);
        }

        protected DadoImpressaoRelacaoReRtConsultaViewModel InitializePesquisaImpressaoRelacaoRERTVoViewModel(ImpressaoRelacaoReRtConsultaPaiVo entity)
        {
            return new DadoImpressaoRelacaoReRtConsultaViewModel().CreateInstance(entity);
        }

        protected PesquisaImpressaoRelacaoRERTVoViewModel InitializePesquisaImpressaoRelacaoRERTVoViewModel(ImpressaoRelacaoReRtConsultaVo entity)
        {
            return new PesquisaImpressaoRelacaoRERTVoViewModel().CreateInstance(entity);
        }

        protected PesquisaImpressaoRelacaoRERTViewModel InitializePesquisaImpressaoRelacaoRERTViewModel(ImpressaoRelacaoRERT entity)
        {
            return new PesquisaImpressaoRelacaoRERTViewModel().CreateInstance(entity);
        }

        protected DadoImpressaoRelacaoReRtCadastrarViewModel InitializePesquisaImpressaoRelacaoRERTCadastrarVoViewModel(ImpressaoRelacaoReRtConsultaPaiVo entity)
        {
            return new DadoImpressaoRelacaoReRtCadastrarViewModel().CreateInstance(entity);
        }

        protected FiltroViewModel InitializeFiltroViewModel(ImpressaoRelacaoRERT entity)
        {
            return new FiltroViewModel().CreateInstance(entity, new DateTime(), new DateTime());
        }

        protected T MapViewModelToEntityModel<T>(FormCollection form, T entity, ref FiltroViewModel viewModel) where T : IPagamentoContaUnica
        {
            #region Desdobramento
            if (entity is Desdobramento)
            {
                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).NumeroContrato = form["NumeroContrato"].Replace(".", "").Replace("-", "");
                    viewModel.NumeroContrato = form["NumeroContrato"];
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                }

                if (!string.IsNullOrEmpty(form["DesdobramentoTipoId"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).DesdobramentoTipoId = Convert.ToInt32(form["DesdobramentoTipoId"]);
                    viewModel.DesdobramentoTipoId = form["DesdobramentoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["DocumentoTipoId"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).DocumentoTipoId = Convert.ToInt32(form["DocumentoTipoId"]);
                    viewModel.DocumentoTipoId = form["DocumentoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["NumeroDocumento"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).NumeroDocumento = form["NumeroDocumento"];
                    viewModel.NumeroDocumento = form["NumeroDocumento"];
                }

                if (!string.IsNullOrEmpty(form["StatusProdesp"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).StatusProdesp = form["StatusProdesp"];
                    viewModel.StatusProdesp = form["StatusProdesp"];
                }

                if (!string.IsNullOrEmpty(form["Cancelado"]))
                {
                    ((Desdobramento)Convert.ChangeType(entity, typeof(Desdobramento))).SituacaoDesdobramento = form["Cancelado"];
                    viewModel.Cancelado = form["Cancelado"];
                }

            }
            #endregion

            #region Reclassificação Retenção
            if (entity is ReclassificacaoRetencao)
            {
                if (!string.IsNullOrEmpty(form["NumeroSiafem"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).NumeroSiafem = form["NumeroSiafem"];
                    viewModel.NumeroSiafem = form["NumeroSiafem"];
                }

                if (!string.IsNullOrEmpty(form["NumeroProcesso"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).NumeroProcesso = form["NumeroProcesso"];
                    viewModel.NumeroProcesso = form["NumeroProcesso"];
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                }

                if (!string.IsNullOrEmpty(form["NumeroOriginalSiafemSiafisico"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).NumeroOriginalSiafemSiafisico = form["NumeroOriginalSiafemSiafisico"];
                    viewModel.NumeroOriginalSiafemSiafisico = form["NumeroOriginalSiafemSiafisico"];
                }

                if (!string.IsNullOrEmpty(form["ReclassificacaoRetencaoTipo"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).ReclassificacaoRetencaoTipoId = Convert.ToInt32(form["ReclassificacaoRetencaoTipo"]);
                    viewModel.ReclassificacaoRetencaoTipo = form["ReclassificacaoRetencaoTipo"];
                }

                if (!string.IsNullOrEmpty(form["NormalEstorno"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).NormalEstorno = form["NormalEstorno"];
                    viewModel.NormalEstorno = form["NormalEstorno"];
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).StatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).NumeroContrato = form["NumeroContrato"];
                    viewModel.NumeroContrato = form["NumeroContrato"];
                }

                var origem = form["OrigemReclassificacaoRetencao"];
                if (!string.IsNullOrEmpty(origem))
                {
                    viewModel.OrigemReclassificacaoRetencao = (OrigemReclassificacaoRetencao)Enum.Parse(typeof(OrigemReclassificacaoRetencao), origem);
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).Origem = viewModel.OrigemReclassificacaoRetencao;
                }

                var agrupamentoConfirmacao = form["AgrupamentoConfirmacao"];
                if (!string.IsNullOrEmpty(agrupamentoConfirmacao))
                {
                    viewModel.AgrupamentoConfirmacao = Convert.ToInt32(agrupamentoConfirmacao);
                    ((ReclassificacaoRetencao)Convert.ChangeType(entity, typeof(ReclassificacaoRetencao))).AgrupamentoConfirmacao = viewModel.AgrupamentoConfirmacao;
                }

            }
            #endregion

            #region Lista Boletos
            if (entity is ListaBoletos)
            {

                if (!string.IsNullOrEmpty(form["NumeroSiafemListaBoleto"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).NumeroSiafem = form["NumeroSiafemListaBoleto"];
                    viewModel.NumeroSiafemListaBoleto = form["NumeroSiafemListaBoleto"];
                }

                if (!string.IsNullOrEmpty(form["CodigoUnidadeGestora"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                    viewModel.CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                }

                if (!string.IsNullOrEmpty(form["CodigoGestao"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).CodigoGestao = form["CodigoGestao"];
                    viewModel.CodigoGestao = form["CodigoGestao"];
                }

                if (!string.IsNullOrEmpty(form["DataEmissao"]))
                {
                    entity.DataEmissao = Convert.ToDateTime(form["DataEmissao"]);
                    viewModel.DataEmissao = form["DataEmissao"];
                }

                if (!string.IsNullOrEmpty(form["NomeLista"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).NomeLista = form["NomeLista"];
                    viewModel.NomeLista = form["NomeLista"];
                }


                if (!string.IsNullOrEmpty(form["NumeroCnpjcpfFavorecido"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).NumeroCnpjcpfFavorecido = form["NumeroCnpjcpfFavorecido"].Replace("/", "").Replace("-", "");
                    viewModel.NumeroCnpjcpfFavorecido = form["NumeroCnpjcpfFavorecido"];
                }


                if (!string.IsNullOrEmpty(form["DocumentoTipoId"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).DocumentoTipoId = Convert.ToInt32(form["DocumentoTipoId"]);
                    viewModel.DocumentoTipoId = form["DocumentoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["TipoDeBoletoId"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).TipoBoletoId = Convert.ToInt32(form["TipoDeBoletoId"]);
                    viewModel.TipoDeBoletoId = form["TipoDeBoletoId"];
                }

                if (!string.IsNullOrEmpty(form["NumeroBoleto1"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto2"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto3"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto4"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto5"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto6"]) &&
                    !string.IsNullOrEmpty(form["NumeroBoleto7"]) &&
                    !string.IsNullOrEmpty(form["NumeroDigito"]))
                {
                    var codboletos = ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).CodigoDeBarras = (form["NumeroBoleto1"] + form["NumeroBoleto2"] + form["NumeroBoleto3"] + form["NumeroBoleto4"] + form["NumeroBoleto5"] + form["NumeroBoleto6"] + form["NumeroDigito"] + form["NumeroBoleto7"]).Trim();
                    viewModel.NumeroDoCodigoDebarras = codboletos;
                    viewModel.NumeroBoleto1 = codboletos.Substring(0, 5);
                    viewModel.NumeroBoleto2 = codboletos.Substring(5, 5);
                    viewModel.NumeroBoleto3 = codboletos.Substring(10, 5);
                    viewModel.NumeroBoleto4 = codboletos.Substring(15, 6);
                    viewModel.NumeroBoleto5 = codboletos.Substring(21, 5);
                    viewModel.NumeroBoleto6 = codboletos.Substring(26, 6);
                    viewModel.NumeroDigito = codboletos.Substring(32, 1);
                    viewModel.NumeroBoleto7 = codboletos.Substring(33, 14);

                }
                else if (!string.IsNullOrEmpty(form["NumeroTaxa1"]) &&
                        !string.IsNullOrEmpty(form["NumeroTaxa2"]) &&
                        !string.IsNullOrEmpty(form["NumeroTaxa3"]) &&
                        !string.IsNullOrEmpty(form["NumeroTaxa4"]))
                {
                    var codboletos = ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).CodigoDeBarras = (form["NumeroTaxa1"] + form["NumeroTaxa2"] + form["NumeroTaxa3"] + form["NumeroTaxa4"]).Trim();
                    viewModel.NumeroDoCodigoDebarras = codboletos;
                    viewModel.NumeroTaxa1 = codboletos.Substring(0, 12);
                    viewModel.NumeroTaxa2 = codboletos.Substring(12, 12);
                    viewModel.NumeroTaxa3 = codboletos.Substring(24, 12);
                    viewModel.NumeroTaxa4 = codboletos.Substring(36, 12);
                }
                else if (!string.IsNullOrEmpty(form["numeroDoCodigoDebarras"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).CodigoDeBarras = form["numeroDoCodigoDebarras"];
                    viewModel.NumeroDoCodigoDebarras = form["numeroDoCodigoDebarras"];
                }

                if (!string.IsNullOrEmpty(form["NumeroDocumento"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).NumeroDocumento = form["NumeroDocumento"];
                    viewModel.NumeroDocumento = form["NumeroDocumento"];
                }



                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    ((ListaBoletos)Convert.ChangeType(entity, typeof(ListaBoletos))).StatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

            }
            #endregion

            #region Preparação Pagamento
            if (entity is PreparacaoPagamento)
            {

                if (!string.IsNullOrEmpty(form["NumeroOp"]))
                {
                    ((PreparacaoPagamento)Convert.ChangeType(entity, typeof(PreparacaoPagamento))).NumeroOpInicial = form["NumeroOp"].Replace("-", "");
                    viewModel.NumeroOp = form["NumeroOp"].Replace("-", "");
                }

                if (!string.IsNullOrEmpty(form["PreparacaoPagamentoTipoId"]))
                {
                    ((PreparacaoPagamento)Convert.ChangeType(entity, typeof(PreparacaoPagamento))).PreparacaoPagamentoTipoId = Convert.ToInt32(form["PreparacaoPagamentoTipoId"]);
                    viewModel.PreparacaoPagamentoTipoId = form["PreparacaoPagamentoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["StatusProdesp"]))
                {
                    ((PreparacaoPagamento)Convert.ChangeType(entity, typeof(PreparacaoPagamento))).StatusProdesp = form["StatusProdesp"];
                    viewModel.StatusProdesp = form["StatusProdesp"];
                }
            }
            #endregion

            #region Programação Desembolso
            if (entity is ProgramacaoDesembolso)
            {
                var obj = entity as ProgramacaoDesembolso;

                if (!string.IsNullOrEmpty(form["NumSiafemProgDesembolso"]))
                {
                    obj.NumeroSiafem = form["NumSiafemProgDesembolso"];
                    viewModel.NumSiafemProgDesembolso = form["NumSiafemProgDesembolso"];
                }

                if (!string.IsNullOrEmpty(form["RegionalId"]))
                {
                    obj.RegionalId = Convert.ToInt32(form["RegionalId"]);
                    viewModel.RegionalId = form["RegionalId"];
                }

                if (!string.IsNullOrEmpty(form["DataVencimento"]))
                {
                    obj.DataVencimento = Convert.ToDateTime(form["DataVencimento"]);
                    viewModel.DataVencimento = form["DataVencimento"];
                }

                if (!string.IsNullOrEmpty(form["TipoDespesa"]))
                {
                    obj.CodigoDespesa = form["TipoDespesa"];
                    viewModel.TipoDespesa = form["TipoDespesa"];
                }

                if (!string.IsNullOrEmpty(form["DocumentoTipoId"]))
                {

                    obj.DocumentoTipoId = Convert.ToInt32(form["DocumentoTipoId"]);
                    viewModel.DocumentoTipoId = form["DocumentoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["NumeroDocumento"]))
                {
                    obj.NumeroDocumento = form["NumeroDocumento"].Replace("/", "");
                    viewModel.NumeroDocumento = form["NumeroDocumento"];
                }

                if (!string.IsNullOrEmpty(form["NumeroAgrupamento"]))
                {
                    obj.NumeroAgrupamento = Convert.ToInt32(form["NumeroAgrupamento"]);
                    viewModel.NumeroAgrupamento = form["NumeroAgrupamento"];
                }

                if (!string.IsNullOrEmpty(form["ProgDesembolsoTipoId"]))
                {
                    obj.ProgramacaoDesembolsoTipoId = Convert.ToInt32(form["ProgDesembolsoTipoId"]);
                    viewModel.ProgDesembolsoTipoId = form["ProgDesembolsoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    obj.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                }

                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    obj.NumeroContrato = form["NumeroContrato"].Replace(".", "").Replace("-", "");
                    viewModel.NumeroContrato = form["NumeroContrato"];
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    obj.StatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                if (!string.IsNullOrEmpty(form["Bloqueio"]))
                {
                    //obj.Bloqueio = Convert.ToBoolean(form["Bloqueio"]);
                    obj.Bloqueio = form["Bloqueio"] == "1" ? true : false;
                    viewModel.Bloqueio = form["Bloqueio"];
                    viewModel.Bloquear = form["Bloqueio"] == "1" ? true : false;
                    obj.Bloquear = form["Bloqueio"] == "1" ? true : false;
                  
                }


            }

            #endregion

            #region Programacao Desembolso Execucao
            if (entity is PDExecucaoItem)
            {
                var obj = entity as PDExecucaoItem;

                if (!string.IsNullOrEmpty(form["NumeroOB"]))
                {
                    //obj.NumOB = form["NumeroOB"];
                    obj.NumOBItem = form["NumeroOB"];
                    viewModel.NumeroOB = form["NumeroOB"];
                }

                if (!string.IsNullOrEmpty(form["NumeroPD"]))
                {
                    obj.NumPD = form["NumeroPD"];
                    viewModel.NumeroPD = form["NumeroPD"];
                }


                if (!string.IsNullOrEmpty(form["OBCancelada"]))
                {
                    obj.OBCancelada = form["OBCancelada"].ToString() == "true";
                    viewModel.OBCancelada = form["OBCancelada"].ToString() == "true";
                }

                if (!string.IsNullOrEmpty(form["Favorecido"]))
                {
                    obj.FavorecidoDesc = form["Favorecido"].ToString();
                    viewModel.Favorecido = form["Favorecido"].ToString();
                }

                viewModel.TipoExecucao = null;
                if (!string.IsNullOrEmpty(form["TipoExecucao"]))
                {
                    viewModel.TipoExecucao = Int32.Parse(form["TipoExecucao"].ToString());
                }


                if (!string.IsNullOrEmpty(form["CadastradoEmDe"]))
                {
                    viewModel.CadastradoEmDe = Convert.ToDateTime(form["CadastradoEmDe"]);
                }

                if (!string.IsNullOrEmpty(form["CadastradoEmAte"]))
                {
                    viewModel.CadastradoEmAte = Convert.ToDateTime(form["CadastradoEmAte"]);
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    obj.cd_transmissao_status_siafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                if (!string.IsNullOrEmpty(form["StatusProdesp"]))
                {
                    obj.cd_transmissao_status_prodesp = form["StatusProdesp"];
                    viewModel.StatusProdesp = form["StatusProdesp"];
                }

                if (!string.IsNullOrEmpty(form["NumeroDocumento"]))
                {
                    obj.NumeroDocumento = form["NumeroDocumento"];
                    viewModel.NumeroDocumento = form["NumeroDocumento"];
                }

                if (!string.IsNullOrEmpty(form["NumeroDocumentoGerador"]))
                {
                    obj.NumeroDocumentoGerador = form["NumeroDocumentoGerador"];
                    viewModel.NumeroDocumentoGerador = form["NumeroDocumentoGerador"];
                }

                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    obj.NumeroContrato = form["NumeroContrato"];
                    viewModel.NumeroContrato = obj.NumeroContrato;
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    obj.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = obj.CodigoAplicacaoObra;
                }
            }
            #endregion

            #region Confirmação de Pagto
            //if (entity is ConfirmacaoPagamento)
            //{
            //    viewModel.DocumentoTipoPagamentoListItems = null;
            //    if (!string.IsNullOrEmpty(form["opcoesTipoPagamento"]))
            //    {
            //        viewModel.DocumentoTipoPagamentoListItems = Int32.Parse(form["opcoesTipoPagamento"].ToString());
            //    }
            //}
            #endregion

            #region Autorização de OB
            if (entity is OBAutorizacao)
            {
                var obj = entity as OBAutorizacao;

                if (!string.IsNullOrEmpty(form["CodigoUnidadeGestora"]))
                {
                    ((OBAutorizacao)Convert.ChangeType(entity, typeof(OBAutorizacao))).UgPagadora = form["CodigoUnidadeGestora"];
                    viewModel.CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                }

                if (!string.IsNullOrEmpty(form["CodigoGestao"]))
                {
                    ((OBAutorizacao)Convert.ChangeType(entity, typeof(OBAutorizacao))).GestaoPagadora = form["CodigoGestao"];
                    viewModel.CodigoGestao = form["CodigoGestao"];
                }

                if (!string.IsNullOrEmpty(form["NumeroOB"]))
                {
                    obj.NumOB = form["NumeroOB"];
                    viewModel.NumeroOB = form["NumeroOB"];
                }

                if (!string.IsNullOrEmpty(form["Favorecido"]))
                {
                    obj.FavorecidoDesc = form["Favorecido"].ToString();
                    viewModel.Favorecido = form["Favorecido"].ToString();
                }

                if (!string.IsNullOrEmpty(form["Despesa"]))
                {
                    obj.CodigoDespesa = form["Despesa"].ToString();
                    viewModel.Despesa = form["Despesa"].ToString();
                }

                if (!string.IsNullOrEmpty(form["Valor"]))
                {
                    obj.Valor = Convert.ToDecimal(form["Valor"]);
                    viewModel.Valor = form["Valor"].ToString();
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    obj.TransmissaoStatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                //if (!string.IsNullOrEmpty(form["StatusProdesp"]))
                //{
                //    obj.cd_transmissao_status_prodesp = form["StatusProdesp"];
                //    viewModel.StatusProdesp = form["StatusProdesp"];
                //}

                if (!string.IsNullOrEmpty(form["CadastradoEmDe"]))
                {
                    viewModel.CadastradoEmDe = Convert.ToDateTime(form["CadastradoEmDe"]);
                }

                if (!string.IsNullOrEmpty(form["CadastradoEmAte"]))
                {
                    viewModel.CadastradoEmAte = Convert.ToDateTime(form["CadastradoEmAte"]);
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    obj.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                }

                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    obj.NumeroContrato = form["NumeroContrato"];
                    viewModel.NumeroContrato = form["NumeroContrato"];
                }

            }

            if (entity is OBAutorizacaoItem)
            {
                var obj = entity as OBAutorizacaoItem;

                if (!string.IsNullOrEmpty(form["CodigoUnidadeGestora"]))
                {
                    ((OBAutorizacaoItem)Convert.ChangeType(entity, typeof(OBAutorizacaoItem))).UGPagadora = form["CodigoUnidadeGestora"];
                    viewModel.CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                }

                if (!string.IsNullOrEmpty(form["CodigoGestao"]))
                {
                    ((OBAutorizacaoItem)Convert.ChangeType(entity, typeof(OBAutorizacaoItem))).GestaoPagadora = form["CodigoGestao"];
                    viewModel.CodigoGestao = form["CodigoGestao"];
                }

                if (!string.IsNullOrEmpty(form["NumeroOB"]))
                {
                    obj.NumOB = form["NumeroOB"];
                    viewModel.NumeroOB = form["NumeroOB"];
                }

                if (!string.IsNullOrEmpty(form["IdExecucaoPD"]))
                {
                    obj.IdExecucaoPD = Convert.ToInt32(form["IdExecucaoPD"]);
                    //viewModel.IdExecucaoPD = form["IdExecucaoPD"];
                }

                if (!string.IsNullOrEmpty(form["NumOP"]))
                {
                    obj.NumOP = form["NumOP"];
                    viewModel.NumeroOp = form["NumOP"];
                }

                if (!string.IsNullOrEmpty(form["Favorecido"]))
                {
                    obj.FavorecidoDesc = form["Favorecido"].ToString();
                    viewModel.Favorecido = form["Favorecido"].ToString();
                }

                if (!string.IsNullOrEmpty(form["Despesa"]))
                {
                    obj.CodigoDespesa = form["Despesa"].ToString();
                    viewModel.Despesa = form["Despesa"].ToString();
                }

                if (!string.IsNullOrEmpty(form["Valor"]))
                {
                    //obj.ValorItem = form["Valor"].ToString();
                    obj.ValorItem = form["Valor"];
                    viewModel.Valor = form["Valor"].ToString();
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    obj.TransmissaoItemStatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                if (!string.IsNullOrEmpty(form["StatusProdesp"]))
                {
                    obj.TransmissaoItemStatusProdesp = form["StatusProdesp"];
                    viewModel.StatusProdesp = form["StatusProdesp"];
                }

                if (!string.IsNullOrEmpty(form["CadastradoEmDe"]))
                {
                    viewModel.CadastradoEmDe = Convert.ToDateTime(form["CadastradoEmDe"]);
                }

                if (!string.IsNullOrEmpty(form["CadastradoEmAte"]))
                {
                    viewModel.CadastradoEmAte = Convert.ToDateTime(form["CadastradoEmAte"]);
                }

                if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
                {
                    obj.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                    viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                }

                if (!string.IsNullOrEmpty(form["NumeroContrato"]))
                {
                    obj.NumeroContrato = form["NumeroContrato"];
                    viewModel.NumeroContrato = form["NumeroContrato"];
                }

            }
            #endregion

            #region Impressão Relação RE RT
            if (entity is ImpressaoRelacaoRERT)
            {
                var obj = entity as ImpressaoRelacaoRERT;

                if (!string.IsNullOrEmpty(form["NumeroRE"]))
                {
                    obj.CodigoRelacaoRERT = form["NumeroRE"].Substring(4, 2) == "RE" ? form["NumeroRE"] : null;
                    viewModel.NumeroRE = form["NumeroRE"];
                }

                if (!string.IsNullOrEmpty(form["NumeroRT"]))
                {
                    obj.CodigoRelacaoRERT = form["NumeroRT"].Substring(4, 2) == "RT" ? form["NumeroRT"] : null;
                    viewModel.NumeroRT = form["NumeroRT"];
                }

                if (!string.IsNullOrEmpty(form["NumeroOB"]))
                {
                    obj.CodigoOB = form["NumeroOB"];
                    viewModel.NumeroOB = form["NumeroOB"];
                }

                if (!string.IsNullOrEmpty(form["StatusSiafem"]))
                {
                    obj.StatusSiafem = form["StatusSiafem"];
                    viewModel.StatusSiafem = form["StatusSiafem"];
                }

                if (!string.IsNullOrEmpty(form["DataCadastramentoDe"]))
                {
                    viewModel.DataCadastramentoDe = form["DataCadastramentoDe"];
                }

                if (!string.IsNullOrEmpty(form["DataCadastramentoAte"]))
                {
                    viewModel.DataCadastramentoAte = form["DataCadastramentoAte"];
                }

                if (!string.IsNullOrEmpty(form["CodigoUnidadeGestora"]))
                {
                    ((ImpressaoRelacaoRERT)Convert.ChangeType(entity, typeof(ImpressaoRelacaoRERT))).CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                    viewModel.CodigoUnidadeGestora = form["CodigoUnidadeGestora"];
                }

                if (!string.IsNullOrEmpty(form["CodigoGestao"]))
                {
                    ((ImpressaoRelacaoRERT)Convert.ChangeType(entity, typeof(ImpressaoRelacaoRERT))).CodigoGestao = form["CodigoGestao"];
                    viewModel.CodigoGestao = form["CodigoGestao"];
                }

                if (!string.IsNullOrEmpty(form["NumeroBanco"]))
                {
                    obj.CodigoBanco = form["NumeroBanco"];
                    viewModel.NumeroBanco = form["NumeroBanco"];
                }

                if (!string.IsNullOrEmpty(form["DataSolicitacao"]))
                {
                    obj.DataSolicitacao = Convert.ToDateTime(form["DataSolicitacao"]);
                    viewModel.DataSolicitacao = form["DataSolicitacao"];
                }

                if (!string.IsNullOrEmpty(form["NumeroAgrupamento"]))
                {
                    obj.NumeroAgrupamento = Convert.ToInt32(form["NumeroAgrupamento"]);
                    viewModel.NumeroAgrupamento = form["NumeroAgrupamento"];
                }

                if (!string.IsNullOrEmpty(form["Cancelado"]))
                {
                    //((ImpressaoRelacaoRERT)Convert.ChangeType(entity, typeof(ImpressaoRelacaoRERT))).FlagCancelamentoRERT = Convert.ToBoolean(form["Cancelado"]);
                    obj.FlagCancelamentoRERT = form["Cancelado"] == "S" ? true : false;
                    viewModel.Cancelado = form["Cancelado"];
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(form["DataCadastramentoDe"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastramentoDe"]);
                viewModel.DataCadastramentoDe = form["DataCadastramentoDe"];
            }

            if (!string.IsNullOrEmpty(form["DataCadastramentoAte"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastramentoAte"]);
                viewModel.DataCadastramentoAte = form["DataCadastramentoAte"];
            }

            return entity;
        }

        private static DadoPagamentoContaUnicaAgrupamentoViewModel InitializeDadoPagamentoContaUnicaAgrupamentoViewModel(ProgramacaoDesembolso entity)
        {
            return new DadoPagamentoContaUnicaAgrupamentoViewModel().CreateInstance(entity, DocumentoTipoList, RegionalList);
        }

        private static DadoProgramacaoDesembolsoViewModel InitializeDadoProgramacaoDesembolsoViewModel(ProgramacaoDesembolso entity)
        {
            return new DadoProgramacaoDesembolsoViewModel().CreateInstance(entity);
        }

        private static DadoProgramacaoDesembolsoPDBECViewModel InitializeDadoProgramacaoDesembolsoPDBECViewModel(ProgramacaoDesembolso entity)
        {
            return new DadoProgramacaoDesembolsoPDBECViewModel().CreateInstance(entity, RegionalList, null);
        }

        private static DadoTipoProgramacaoDesembolsoViewModel InitializeTipoProgramacaoDesembolsoViewModel(ProgramacaoDesembolso entity)
        {
            return new DadoTipoProgramacaoDesembolsoViewModel().CreateInstance(entity, ProgramacaoDesembolsoTipoList);
        }

        private IEnumerable<DadoCodigoDeBarrasViewModel> InitializeDadoCodigoDeBarrasGridViewModel(ListaBoletos listaBoleto)
        {
            var items = new List<DadoCodigoDeBarrasViewModel>();
            foreach (var listaBoletoListaCodigoBarra in listaBoleto.ListaCodigoBarras)
            {

                var boletoCodbarra = listaBoletoListaCodigoBarra.CodigoBarraBoleto != null ? listaBoletoListaCodigoBarra.CodigoBarraBoleto.Id : 0;

                //if (listaBoletoListaCodigoBarra.TipoBoletoId == 2 && boletoCodbarra > 0)
                if (listaBoletoListaCodigoBarra.TipoBoletoId == 2)
                {
                    items.Add(new DadoCodigoDeBarrasViewModel().CreateInstance(listaBoletoListaCodigoBarra.CodigoBarraBoleto, listaBoletoListaCodigoBarra.Valor));
                }

                //if (listaBoletoListaCodigoBarra.TipoBoletoId == 1 && listaBoletoListaCodigoBarra.CodigoBarraTaxa.Id > 0)
                if (listaBoletoListaCodigoBarra.TipoBoletoId == 1)
                {
                    items.Add(new DadoCodigoDeBarrasViewModel().CreateInstance(listaBoletoListaCodigoBarra.CodigoBarraTaxa, listaBoletoListaCodigoBarra.Valor));
                }
            }


            return items;
        }

        private DadoCodigoDeBarrasViewModel InitializeDadoCodigoDeBarrasViewModel()
        {
            return new DadoCodigoDeBarrasViewModel().CreateInstance(TipoBoletoList);
        }

        private DadoListaBoletosValorViewModel InitializeDadoListaBoletosValorViewModel(ListaBoletos listaBoleto)
        {
            return new DadoListaBoletosValorViewModel().CreateInstance(listaBoleto);
        }
        private DadoListaBoletosViewModel InitializeDadoListaBoletosViewModel(ListaBoletos listaBoleto)
        {
            return new DadoListaBoletosViewModel().CreateInstance(listaBoleto);
        }
    }
}