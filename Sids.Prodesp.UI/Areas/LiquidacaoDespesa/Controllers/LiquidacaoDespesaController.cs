
namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Controllers
{
    using Application;
    using Empenho.Models;
    using Model.Base.Empenho;
    using Model.Base.LiquidacaoDespesa;
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Extension;
    using Model.Interface.LiquidacaoDespesa;
    using Model.ValueObject;
    using Model.ValueObject.Service.Siafem.Empenho;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using UI.Controllers.Base;


    public abstract class LiquidacaoDespesaController : ConsutasBaseController
    {
        protected readonly IEnumerable<Estrutura> _estruturaList = App.EstruturaService.Buscar(new Estrutura()) ?? new List<Estrutura>();
        protected static readonly IEnumerable<Regional> _regionalList = App.RegionalService.Buscar(new Regional()) ?? new List<Regional>();
        protected readonly IEnumerable<Programa> _programaList = App.ProgramaService.Buscar(new Programa()) ?? new List<Programa>();
        protected readonly IEnumerable<Fonte> _fonteList = App.FonteService.Buscar(new Fonte()) ?? new List<Fonte>();
        protected static readonly IEnumerable<NaturezaTipo> _naturezaTipoList = App.NaturezaTipoService.Buscar(new NaturezaTipo()) ?? new List<NaturezaTipo>();

        protected static readonly IEnumerable<CenarioTipo> _cenarioList = App.CenarioTipoService.Buscar(new CenarioTipo()) ?? new List<CenarioTipo>();
        protected static readonly IEnumerable<ServicoTipo> _servicoTipoList = App.ServicoTipoService.Buscar(new ServicoTipo()) ?? new List<ServicoTipo>();
        protected static readonly IEnumerable<EventoTipo> _eventoTipoList = App.EventoTipoService.Buscar(new EventoTipo()) ?? new List<EventoTipo>();
        protected static readonly IEnumerable<CodigoEvento> _codigoEventoList = App.CodigoEventoService.Buscar(new CodigoEvento()) ?? new List<CodigoEvento>();
        protected static readonly IEnumerable<ILiquidacaoDespesaEvento> _subeventoList = App.SubempenhoEventoService.Buscar(new SubempenhoEvento()) ?? new List<SubempenhoEvento>();

        protected static readonly IEnumerable<ObraTipo> _obraTipoList = App.ObraTipoService.Buscar(new ObraTipo()) ?? new List<ObraTipo>();

        protected IEnumerable<Models.FiltroGridViewModel> _filterItems;
        private Usuario _userLoggedIn;
        protected int _modelId;
        protected static IEnumerable<int> _anoServiceList;


        protected LiquidacaoDespesaController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _filterItems = new List<Models.FiltroGridViewModel>();
            _userLoggedIn = App.AutenticacaoService.GetUsuarioLogado();
            _anoServiceList = _anoServiceList != null ? _anoServiceList : (_anoServiceList = GetProgramAvailableYears().ToList());
        }


        protected IEnumerable<Models.FiltroGridViewModel> Display<T>(T entity) where T : ILiquidacaoDespesa
        {
            var filterModel = InitializeEntityModel(entity);
            InitializeCommonBags(entity);

            if (entity is Subempenho)
            {
                var inclusao = (Subempenho)Convert.ChangeType(filterModel, typeof(Subempenho));
                ViewBag.Filtro = InitializeFiltroViewModel(inclusao);
            }
            else if (entity is IRap)
            {
                ViewBag.Filtro = InitializeFiltroViewModel((IRap)filterModel);
            }
            else
            {
                ViewBag.Filtro = InitializeFiltroViewModel(filterModel);
            }

            return new List<Models.FiltroGridViewModel>();
        }

        protected IEnumerable<Models.FiltroGridViewModel> Display<T>(T entity, FormCollection form) where T : ILiquidacaoDespesa
        {
            IEnumerable<ILiquidacaoDespesa> entities = new List<ILiquidacaoDespesa>();

            var model = GenerateFilterViewModel(form, entity);

            if (entity is Subempenho)
            {
                var inclusao = (Subempenho)Convert.ChangeType(model, typeof(Subempenho));
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                inclusao.RegionalId = (short)usuario.RegionalId;
                entities = App.SubempenhoService.BuscarGrid(inclusao, Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte)).Cast<ILiquidacaoDespesa>();
            }

            if (entity is SubempenhoCancelamento)
            {
                var anulacao = (SubempenhoCancelamento)Convert.ChangeType(model, typeof(SubempenhoCancelamento));
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                anulacao.RegionalId = (short)usuario.RegionalId;
                entities = App.SubempenhoCancelamentoService.BuscarGrid(anulacao, Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte)).Cast<ILiquidacaoDespesa>();
            }

            if (entity is RapInscricao)
            {
                var inscricao = (RapInscricao)Convert.ChangeType(model, typeof(RapInscricao));
                entities = App.RapInscricaoService.BuscarGrid(inscricao, Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte)).Cast<IRap>();
            }

            if (entity is RapRequisicao)
            {
                var inclusao = (RapRequisicao)Convert.ChangeType(model, typeof(RapRequisicao));
                entities = App.RapRequisicaoService.BuscarGrid(inclusao, Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte)).Cast<IRap>();
            }

            if (entity is RapAnulacao)
            {
                var anulacao = (RapAnulacao)Convert.ChangeType(model, typeof(RapAnulacao));
                entities = App.RapAnulacaoService.BuscarGrid(anulacao, Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoDe), Convert.ToDateTime(((Models.FiltroViewModel)ViewBag.Filtro).DataCadastramentoAte)).Cast<IRap>();
            }

            return InitializeFiltroGridViewModel(entities);
        }

        protected T Display<T>(T entity, bool isNewRecord) where T : ILiquidacaoDespesa
        {
            return Display(entity, isNewRecord, false);
        }
        protected T Display<T>(T entity, bool isNewRecord, bool visualizar) where T : ILiquidacaoDespesa
        {
            EnumTipoOperacaoEmpenho tipo = EnumTipoOperacaoEmpenho.Empenho;

            if (isNewRecord)
            {
                entity = InitializeEntityModel(entity);
            }
            entity.CodigoUnidadeGestora = _regionalList.FirstOrDefault(x => x.Id == _userLoggedIn.RegionalId)?.Uge;
            entity.CodigoGestao = "16055";
            entity.NumeroContrato = entity.NumeroContrato == "0" ? null : entity.NumeroContrato;

            var msg = new List<string>
            {
                entity.MensagemProdesp,
                entity.MensagemSiafemSiafisico
            };

            if (!string.IsNullOrEmpty(entity.MensagemProdesp) || !string.IsNullOrEmpty(entity.MensagemSiafemSiafisico))
            { ViewBag.MsgRetorno = string.Join("\n", msg.Where(x => x != null)); }
            else
            { ViewBag.MsgRetorno = null; }


            ViewBag.PesquisaReservaContrato = InitializePesquisaReservaContratoViewModel(entity);

            if (entity is Subempenho)
            {
                tipo = EnumTipoOperacaoEmpenho.Subempenho;
                ViewBag.PesquisaTipoApropriacao = InitializePesquisaTipoApropriacaoViewModel(entity as Subempenho);
                var inclusao = (Subempenho)Convert.ChangeType(entity, typeof(Subempenho));
                ViewBag.DadoApropriacao = InitializeDadoApropriacaoViewModel(inclusao);
                ViewBag.DadoApropriacaoEstrutura = InitializeDadoApropriacaoEstruturaViewModel(inclusao);
                ViewBag.DadoCaucao = new DadoCaucaoViewModel().CreateInstance(inclusao.QuotaGeralAutorizadaPor, inclusao.NumeroGuia, inclusao.ValorCaucionado);
                ViewBag.PesquisaEmpenhoCredor = InitializePesquisaEmpenhoCredorViewModel(inclusao);

            }

            if (entity is SubempenhoCancelamento)
            {
                tipo = EnumTipoOperacaoEmpenho.SubempenhoAnulacao;
                ViewBag.PesquisaTipoApropriacao = InitializePesquisaTipoApropriacaoViewModel(entity);
                var anulacao = (SubempenhoCancelamento)Convert.ChangeType(entity, typeof(SubempenhoCancelamento));
                ViewBag.DadoApropriacao = InitializeDadoApropriacaoViewModel(anulacao);
            }

            if (entity is RapInscricao)
            {
                var inscricao = (RapInscricao)Convert.ChangeType(entity, typeof(RapInscricao));
                ViewBag.DadoInscricao = InitializeDadoInscricaoViewModel(inscricao);
                ViewBag.DadoCaucao = new DadoCaucaoViewModel().CreateInstance(inscricao.QuotaGeralAutorizadaPor, inscricao.NumeroGuia, inscricao.ValorCaucionado, inscricao.ValorRealizado);
                ViewBag.PesquisaSaldoRap = new PesquisaSaldoRapViewModel().CreateInstance(inscricao, GetProgramAvailableYears(), _regionalList);
            }

            if (entity is RapRequisicao)
            {
                var requisicao = (RapRequisicao)Convert.ChangeType(entity, typeof(RapRequisicao));
                ViewBag.PesquisaEmpenhoRap = new PesqEmpenhoRapViewModel().CreateInstance(requisicao);
                ViewBag.PesquisaInscritoRap = new PesqSubempInscritoRapViewModel().CreateInstance(requisicao);
                ViewBag.DadoRequisicaoRap = InitializeDadoRequisicaoRapViewModel(requisicao);
                ViewBag.DadoApropriacaoEstrutura = InitializeDadoApropriacaoEstruturaViewModel(requisicao);
                ViewBag.DadoCaucao = new DadoCaucaoViewModel().CreateInstance(requisicao.QuotaGeralAutorizadaPor, requisicao.NumeroGuia, requisicao.ValorCaucionado, requisicao.ValorRealizado);
            }

            if (entity is RapAnulacao)
            {
                var anulacao = (RapAnulacao)Convert.ChangeType(entity, typeof(RapAnulacao));
                ViewBag.PesquisaRequisicaoRap = new PesquisaRequisicaoRapViewModel().CreateInstance(anulacao);
                ViewBag.DadoRequisicaoRap = InitializeDadoRequisicaoRapViewModel(anulacao);
                ViewBag.DadoApropriacaoEstrutura = InitializeDadoApropriacaoEstruturaViewModel(anulacao);
                ViewBag.DadoCaucao = new DadoCaucaoViewModel().CreateInstance(anulacao.QuotaGeralAutorizadaPor, anulacao.NumeroGuia, anulacao.ValorCaucionado, anulacao.ValorRealizado);
                ViewBag.DadoSaldoValor = new DadoSaldoValorAnulacao().CreateInstance(anulacao.ValorSaldoAnteriorSubempenho, anulacao.ValorAnulado, anulacao.ValorSaldoAposAnulacao, isNewRecord);
            }

            ViewBag.DadoLiquidacaoNota = InitializeDadoLiquidacaoNotaViewModel(entity);

            if (entity is Subempenho || entity is SubempenhoCancelamento)
            {
                ViewBag.DadoLiquidacaoEvento = InitializeDadoLiquidacaoEventoViewModel(entity);
                ViewBag.DadoLiquidacaoEventoGrid = InitializeDadoLiquidacaoEventoGridViewModel(entity);
                ViewBag.DadoLiquidacaoItem = InitializeDadoLiquidacaoItemViewModel(entity);
                //ViewBag.DadoLiquidacaoItemGrid = InitializeDadoLiquidacaoItemGridViewModel(entity);
                ViewBag.CenarioAtual = entity.CenarioSiafemSiafisico;


                if (!visualizar && !isNewRecord)
                {
                    string ct = string.IsNullOrWhiteSpace(entity.NumeroCT) ? string.Empty : entity.NumeroCT;

                    if (string.IsNullOrWhiteSpace(ct) && !string.IsNullOrWhiteSpace(entity.NumeroOriginalSiafemSiafisico) && entity.Itens.Count() > 0 )
                    {
                        ConsultaNe consultaNe = App.CommonService.ConsultaNe(entity.NumeroOriginalSiafemSiafisico, _userLoggedIn);
                        if (consultaNe != null)
                        {
                            ct = consultaNe.NumeroContrato;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ct))
                    {
                        ConsultaCt consultaCt = App.CommonService.ConsultaCt(_userLoggedIn, ct, "");

                        List<BaseEmpenhoItem> itemsComSaldo = CalcularSaldo(_userLoggedIn, consultaCt, tipo,"");
                        var itensDaEntidade = entity.Itens.ToList();
                        entity.Itens = PreencherSaldo(itensDaEntidade, itemsComSaldo);
                    }
                }

                var itens = InitializeDadoLiquidacaoItemGridViewModel(entity.Itens, (EnumCenarioSiafemSiafisico)entity.CenarioSiafemSiafisico);

                ViewBag.DadoLiquidacaoItemGrid = itens;
            }

            ViewBag.DadoObservacao = InitializeDadoObservacaoViewModel(entity);
            ViewBag.DadoAssinatura = InitializeDadoAssinaturaViewModel(entity, isNewRecord);
            ViewBag.DadoReferencia = InitializeDadoReferenciaViewModel(entity);
            ViewBag.DadoDespesa = InitializeDadoDespesaViewModel(entity);

            ViewBag.PesquisaCT = InitializePesquisaCTViewModel(entity, tipo);

            ViewBag.TipoEmpenho = tipo;

            InitializeCommonBags(entity);

            return entity;
        }

        private IEnumerable<LiquidacaoDespesaItem> PreencherSaldo(List<LiquidacaoDespesaItem> itensDaEntidade, List<BaseEmpenhoItem> itemsComSaldo)
        {
            foreach (var item in itensDaEntidade)
            {
                var itemEquivalente = itemsComSaldo.FirstOrDefault(x => x.CodigoItemServico.RemoveSpecialChar().Equals(item.CodigoItemServico.RemoveSpecialChar()));
                if (itemEquivalente != null)
                {
                    item.QuantidadeLiquidar = item.QuantidadeMaterialServico;
                    item.QuantidadeMaterialServico = itemEquivalente.QuantidadeMaterialServico;
                    item.Valor = itemEquivalente.ValorTotal;
                }
            }

            return itensDaEntidade;
        }

        protected T GenerateFilterViewModel<T>(FormCollection form, T entity) where T : ILiquidacaoDespesa
        {
            Models.FiltroViewModel filter;

            if (entity is IRap)
            {
                filter = InitializeFiltroViewModel((IRap)entity);
            }
            else
            {
                filter = InitializeFiltroViewModel(entity);
            }

            entity = MapViewModelToEntityModel(form, entity, ref filter);

            ViewBag.Filtro = filter;

            return entity;
        }

        protected Models.FiltroViewModel InitializeFiltroViewModel(Subempenho entity)
        {
            return new Models.FiltroViewModel().CreateInstance(entity, _cenarioList, new DateTime(), new DateTime());
        }

        protected Models.FiltroViewModel InitializeFiltroViewModel(ILiquidacaoDespesa entity)
        {
            return new Models.FiltroViewModel().CreateInstance(entity, _cenarioList, new DateTime(), new DateTime());
        }

        protected Models.FiltroViewModel InitializeFiltroViewModel(IRap entity)
        {
            return new Models.FiltroViewModel().CreateInstance(entity, _servicoTipoList, new DateTime(), new DateTime());
        }

        protected IEnumerable<Models.FiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<ILiquidacaoDespesa> entities)
        {
            List<Models.FiltroGridViewModel> items = new List<Models.FiltroGridViewModel>();

            foreach (var entity in entities)
            {
                if (entity is IRap)
                {
                    items.Add(new Models.FiltroGridViewModel().CreateInstance((IRap)entity, _servicoTipoList));
                }
                else
                {
                    items.Add(new Models.FiltroGridViewModel().CreateInstance(entity, _cenarioList));
                }
            }

            return items;
        }

        protected IEnumerable<Models.FiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<IRap> entities)
        {
            return entities.Select(entity => new Models.FiltroGridViewModel().CreateInstance(entity, _cenarioList))
                ?? new List<Models.FiltroGridViewModel>();
        }

        protected T MapViewModelToEntityModel<T>(FormCollection form, T entity, ref Models.FiltroViewModel viewModel) where T : ILiquidacaoDespesa
        {
            if (!string.IsNullOrEmpty(form["NumeroSubempenhoProdesp"]))
            {
                entity.NumeroProdesp = form["NumeroSubempenhoProdesp"];
                viewModel.NumeroSubempenhoProdesp = form["NumeroSubempenhoProdesp"];
            }

            if (!string.IsNullOrEmpty(form["NumeroSubempenhoSiafemSiafisico"]))
            {
                entity.NumeroSiafemSiafisico = form["NumeroSubempenhoSiafemSiafisico"];
                viewModel.NumeroSubempenhoSiafemSiafisico = form["NumeroSubempenhoSiafemSiafisico"];
            }

            if (!string.IsNullOrEmpty(form["NumeroEmpenhoProdesp"]))
            {
                entity.NumeroOriginalProdesp = form["NumeroEmpenhoProdesp"];
                viewModel.NumeroEmpenhoProdesp = form["NumeroEmpenhoProdesp"];
            }


            if (!string.IsNullOrEmpty(form["NumeroProcesso"]))
            {
                entity.NumeroProcesso = form["NumeroProcesso"];
                viewModel.NumeroProcesso = form["NumeroProcesso"];
            }


            if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
            {
                entity.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
                viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
            }


            if (!string.IsNullOrEmpty(form["CenarioSiafemSiafisico"]))
            {
                entity.CenarioSiafemSiafisico = Convert.ToInt32(form["CenarioSiafemSiafisico"]);
                viewModel.CenarioSiafemSiafisico = form["CenarioSiafemSiafisico"];
            }



            if (!string.IsNullOrEmpty(form["StatusSiafemSiafisico"]))
            {
                entity.StatusSiafemSiafisico = form["StatusSiafemSiafisico"];
                viewModel.StatusSiafemSiafisico = form["StatusSiafemSiafisico"];
            }



            if (!string.IsNullOrEmpty(form["StatusProdesp"]))
            {
                entity.StatusProdesp = form["StatusProdesp"];
                viewModel.StatusProdesp = form["StatusProdesp"];
            }


            if (!string.IsNullOrEmpty(form["NumeroContrato"]))
            {
                entity.NumeroContrato = form["NumeroContrato"].Replace(".", "").Replace("-", "");
                viewModel.NumeroContrato = form["NumeroContrato"];
            }


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

            if (!string.IsNullOrEmpty(form["NumeroCNPJCPFCredor"]))
            {
                entity.NumeroCNPJCPFCredor = form["NumeroCNPJCPFCredor"];
                viewModel.NumeroCNPJCPFCredor = form["NumeroCNPJCPFCredor"];
            }

            if (!string.IsNullOrEmpty(form["CodigoGestaoCredor"]))
            {
                entity.CodigoGestaoCredor = form["CodigoGestaoCredor"];
                viewModel.CodigoGestaoCredor = form["CodigoGestaoCredor"];
            }

            if (entity is Subempenho)
            {
                if (!string.IsNullOrEmpty(form["CodigoCredorOrganizacao"]))
                {
                    ((Subempenho)Convert.ChangeType(entity, typeof(Subempenho))).CodigoCredorOrganizacao = Convert.ToInt32(form["CodigoCredorOrganizacao"]);
                    viewModel.CodigoCredorOrganizacao = form["CodigoCredorOrganizacao"];
                }

                if (!string.IsNullOrEmpty(form["NumeroCNPJCPFFornecedor"]))
                {
                    ((Subempenho)Convert.ChangeType(entity, typeof(Subempenho))).NumeroCNPJCPFFornecedor = form["NumeroCNPJCPFFornecedor"];
                    viewModel.NumeroCNPJCPFFornecedor = form["NumeroCNPJCPFFornecedor"];
                }
            }

            if (entity is RapInscricao)
            {
                if (!string.IsNullOrEmpty(form["ServicoTipoId"]))
                {
                    ((RapInscricao)Convert.ChangeType(entity, typeof(RapInscricao))).TipoServicoId = Convert.ToInt32(form["ServicoTipoId"]);
                    viewModel.ServicoTipoId = form["ServicoTipoId"];
                }

            }


            if (entity is RapRequisicao)
            {
                if (!string.IsNullOrEmpty(form["ServicoTipoId"]))
                {
                    ((RapRequisicao)Convert.ChangeType(entity, typeof(RapRequisicao))).TipoServicoId = Convert.ToInt32(form["ServicoTipoId"]);
                    viewModel.ServicoTipoId = form["ServicoTipoId"];
                }
            }


            if (entity is RapAnulacao)
            {
                if (!string.IsNullOrEmpty(form["ServicoTipoId"]))
                {
                    ((RapAnulacao)Convert.ChangeType(entity, typeof(RapAnulacao))).TipoServicoId = Convert.ToInt32(form["ServicoTipoId"]);
                    viewModel.ServicoTipoId = form["ServicoTipoId"];
                }

                if (!string.IsNullOrEmpty(form["NumeroEmpenhoSiafem"]))
                {
                    entity.NumeroOriginalSiafemSiafisico = form["NumeroEmpenhoSiafem"];
                    viewModel.NumeroEmpenhoSiafem = form["NumeroEmpenhoSiafem"];
                }
            }

            return entity;
        }

        protected void InitializeCommonBags<T>(T entity) where T : ILiquidacaoDespesa
        {
            ViewBag.Cenario = _cenarioList;
            ViewBag.Usuario = _userLoggedIn;
            ViewBag.EventoTipoList = _eventoTipoList;
            //ViewBag.Regionais = _regionalList.Where(x => x.Id > 1);
            ViewBag.Fontes = _fonteList;
            ViewBag.Regionais = _regionalList;
            ViewBag.Programas = _programaList;
            ViewBag.Programa = _programaList.FirstOrDefault(w => w.Codigo == entity.ProgramaId);
            ViewBag.Anos = _anoServiceList;
            ViewBag.Estruturas = _estruturaList;
        }

        protected T InitializeEntityModel<T>(T entity) where T : ILiquidacaoDespesa
        {
            entity.Id = default(int);
            entity.NumeroSiafemSiafisico = null;
            entity.NumeroProdesp = null;
            entity.DataCadastro = default(DateTime);
            entity.DataTransmitidoProdesp = default(DateTime);
            entity.DataTransmitidoSiafemSiafisico = default(DateTime);
            entity.MensagemProdesp = null;
            entity.MensagemSiafemSiafisico = null;
            entity.TransmitidoProdesp = false;
            entity.TransmitidoSiafem = false;
            entity.TransmitidoSiafisico = false;
            entity.DataEmissao = DateTime.Now;

            entity.StatusProdesp = "N";
            entity.StatusSiafemSiafisico = "N";

            entity.RegionalId = _userLoggedIn.RegionalId == 1
               ? Convert.ToInt16(16)
               : Convert.ToInt16(_userLoggedIn.RegionalId);

            entity.Eventos = new List<LiquidacaoDespesaEvento>();
            entity.Notas = new List<LiquidacaoDespesaNota>();
            entity.Itens = new List<LiquidacaoDespesaItem>();

            return entity;
        }

        protected PesquisaReservaContratoViewModel InitializePesquisaReservaContratoViewModel(ILiquidacaoDespesa entity)
        {
            return new PesquisaReservaContratoViewModel().CreateInstance(entity.NumeroContrato);
        }

        protected PesquisaEmpenhoCredorViewModel InitializePesquisaEmpenhoCredorViewModel(Subempenho entity)
        {
            return new PesquisaEmpenhoCredorViewModel().CreateInstance(entity);
        }

        protected PesquisaTipoApropriacaoViewModel InitializePesquisaTipoApropriacaoViewModel(ILiquidacaoDespesa entity)
        {
            return new PesquisaTipoApropriacaoViewModel().CreateInstance(entity, _cenarioList);
        }

        protected DadoAssinaturaViewModel InitializeDadoAssinaturaViewModel(ILiquidacaoDespesa entity, bool isNewRecord)
        {
            return GetSignaturesFromDomainModel(entity, isNewRecord);
        }

        protected DadoApropriacaoViewModel InitializeDadoApropriacaoViewModel(Subempenho entity)
        {
            //1
            return new DadoApropriacaoViewModel().CreateInstance(entity, _codigoEventoList, _eventoTipoList, _estruturaList, _regionalList, _programaList, _fonteList, _naturezaTipoList, _obraTipoList);
        }
        protected DadoRequisicaoRapViewModel InitializeDadoRequisicaoRapViewModel(RapRequisicao entity)
        {
            return new DadoRequisicaoRapViewModel().CreateInstance(entity, _estruturaList, _naturezaTipoList, _programaList, _servicoTipoList);
        }

        protected DadoRequisicaoRapViewModel InitializeDadoRequisicaoRapViewModel(RapAnulacao entity)
        {
            return new DadoRequisicaoRapViewModel().CreateInstance(entity, _eventoTipoList, _estruturaList, _naturezaTipoList, _obraTipoList);
        }

        protected DadoApropriacaoEstruturaViewModel InitializeDadoApropriacaoEstruturaViewModel(Subempenho entity)
        {
            return new DadoApropriacaoEstruturaViewModel().CreateInstance(entity, _estruturaList, _regionalList, _programaList, _fonteList, _naturezaTipoList);
        }

        protected DadoApropriacaoEstruturaViewModel InitializeDadoApropriacaoEstruturaViewModel(RapRequisicao entity)
        {
            return new DadoApropriacaoEstruturaViewModel().CreateInstance(entity, _estruturaList, _regionalList, _programaList, GetProgramAvailableYears(), _servicoTipoList, _naturezaTipoList);
        }

        protected DadoApropriacaoEstruturaViewModel InitializeDadoApropriacaoEstruturaViewModel(RapAnulacao entity)
        {
            return new DadoApropriacaoEstruturaViewModel().CreateInstance(entity, _estruturaList, _regionalList, _programaList, GetProgramAvailableYears(), _servicoTipoList, _naturezaTipoList);
        }


        protected DadoObservacaoViewModel InitializeDadoObservacaoViewModel(ILiquidacaoDespesa entity)
        {
            if (entity.DescricaoObservacao1 == " " || entity.DescricaoObservacao1 == "")
            {
                if (entity.DescricaoObservacao2 != " " || entity.DescricaoObservacao2 != "")
                {
                    entity.DescricaoObservacao1 = entity.DescricaoObservacao2;
                    entity.DescricaoObservacao2 = " ";
                }
                if (entity.DescricaoObservacao3 != " " || entity.DescricaoObservacao3 != "")
                {
                    if (entity.DescricaoObservacao1 == " " || entity.DescricaoObservacao1 == "")
                    {
                        entity.DescricaoObservacao1 = entity.DescricaoObservacao3;
                    }
                }
            }

            return new DadoObservacaoViewModel().CreateInstance(entity);
        }

        protected DadoInscricaoViewModel InitializeDadoInscricaoViewModel(IRap entity)
        {
            return new DadoInscricaoViewModel().CreateInstance(entity, _estruturaList, _programaList, _servicoTipoList);
        }

        protected DadoApropriacaoViewModel InitializeDadoApropriacaoViewModel(ILiquidacaoDespesa entity)
        {
            return new DadoApropriacaoViewModel().CreateInstance(entity, _codigoEventoList, _eventoTipoList, _obraTipoList);
        }

        protected DadoLiquidacaoNotaViewModel InitializeDadoLiquidacaoNotaViewModel<T>(T entity) where T : ILiquidacaoDespesa
        {
            return new DadoLiquidacaoNotaViewModel().CreateInstance(entity)
                ?? new DadoLiquidacaoNotaViewModel();
        }

        protected DadoLiquidacaoItemViewModel InitializeDadoLiquidacaoItemViewModel<T>(T entity) where T : ILiquidacaoDespesa
        {

            //return (entity.TransmitidoSiafem || entity.TransmitidoSiafisico)
            //    ? entity.Itens.Select(model => new DadoLiquidacaoItemViewModel().CreateInstance(model, entity.TransmitidoSiafem || entity.TransmitidoSiafisico)).FirstOrDefault()
            //    ?? new DadoLiquidacaoItemViewModel()
            //    : new DadoLiquidacaoItemViewModel();

            return entity.Itens.Select(model => new DadoLiquidacaoItemViewModel().CreateInstance(model)).FirstOrDefault()
                ?? new DadoLiquidacaoItemViewModel();

        }

        protected IEnumerable<DadoLiquidacaoItemViewModel> InitializeDadoLiquidacaoItemGridViewModel(IEnumerable<LiquidacaoDespesaItem> itens, EnumCenarioSiafemSiafisico cenario)
        {
            return itens.Select(model => new DadoLiquidacaoItemViewModel().CriarInstancia(model, cenario)).ToList() ?? new List<DadoLiquidacaoItemViewModel>();
        }

        protected IEnumerable<DadoLiquidacaoItemViewModel> InitializeDadoLiquidacaoItemGridViewModel<T>(T entity) where T : ILiquidacaoDespesa
        {
            //return (entity.TransmitidoSiafem || entity.TransmitidoSiafisico)
            //    ? entity.Itens.Select(model => new DadoLiquidacaoItemViewModel().CreateInstance(model, entity.TransmitidoSiafem || entity.TransmitidoSiafisico)).ToList()
            //    ?? new List<DadoLiquidacaoItemViewModel>()
            //    : new List<DadoLiquidacaoItemViewModel>();
            return entity.Itens.Select(model => new DadoLiquidacaoItemViewModel().CreateInstance(model)).ToList()
                ?? new List<DadoLiquidacaoItemViewModel>();
        }

        protected DadoLiquidacaoEventoViewModel InitializeDadoLiquidacaoEventoViewModel<T>(T entity) where T : ILiquidacaoDespesa
        {
            return new DadoLiquidacaoEventoViewModel();
        }

        protected IEnumerable<DadoLiquidacaoEventoViewModel> InitializeDadoLiquidacaoEventoGridViewModel<T>(T entity) where T : ILiquidacaoDespesa
        {
            return entity.Eventos.Select(model => new DadoLiquidacaoEventoViewModel().CreateInstance(model, _codigoEventoList)).ToList();
        }

        protected DadoAssinaturaViewModel GetSignaturesFromDomainModel(ILiquidacaoDespesa entity, bool isNewRecord)
        {
            if (isNewRecord) ObterAssinaturasDorepositorio(ref entity);

            var autorizado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoAutorizadoAssinatura,
                CodigoGrupo = entity.CodigoAutorizadoGrupo,
                CodigoOrgao = entity.CodigoAutorizadoOrgao,
                NomeAssinatura = entity.NomeAutorizadoAssinatura,
                DescricaoCargo = entity.DescricaoAutorizadoCargo
            };

            var examinado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoExaminadoAssinatura,
                CodigoGrupo = entity.CodigoExaminadoGrupo,
                CodigoOrgao = entity.CodigoExaminadoOrgao,
                NomeAssinatura = entity.NomeExaminadoAssinatura,
                DescricaoCargo = entity.DescricaoExaminadoCargo
            };

            var responsavel = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoResponsavelAssinatura,
                CodigoGrupo = entity.CodigoResponsavelGrupo,
                CodigoOrgao = entity.CodigoResponsavelOrgao,
                NomeAssinatura = entity.NomeResponsavelAssinatura,
                DescricaoCargo = entity.DescricaoResponsavelCargo
            };

            return new Empenho.Models.DadoAssinaturaViewModel().CreateInstance(autorizado, examinado, responsavel);

        }

        protected void ObterAssinaturasDorepositorio(ref ILiquidacaoDespesa entity)
        {
            var signatures = GetEntityByRegionalId(entity);
            if (signatures != null)
            {
                entity.CodigoAutorizadoAssinatura = signatures.CodigoAutorizadoAssinatura;
                entity.CodigoAutorizadoGrupo = signatures.CodigoAutorizadoGrupo;
                entity.CodigoAutorizadoOrgao = signatures.CodigoAutorizadoOrgao;
                entity.NomeAutorizadoAssinatura = signatures.NomeAutorizadoAssinatura;
                entity.DescricaoAutorizadoCargo = signatures.DescricaoAutorizadoCargo;

                entity.CodigoExaminadoAssinatura = signatures.CodigoExaminadoAssinatura;
                entity.CodigoExaminadoGrupo = signatures.CodigoExaminadoGrupo;
                entity.CodigoExaminadoOrgao = signatures.CodigoExaminadoOrgao;
                entity.NomeExaminadoAssinatura = signatures.NomeExaminadoAssinatura;
                entity.DescricaoExaminadoCargo = signatures.DescricaoExaminadoCargo;

                entity.CodigoResponsavelAssinatura = signatures.CodigoResponsavelAssinatura;
                entity.CodigoResponsavelGrupo = signatures.CodigoResponsavelGrupo;
                entity.CodigoResponsavelOrgao = signatures.CodigoResponsavelOrgao;
                entity.NomeResponsavelAssinatura = signatures.NomeResponsavelAssinatura;
                entity.DescricaoResponsavelCargo = signatures.DescricaoResponsavelCargo;
            }
        }

        protected Models.DadoDespesaViewModel InitializeDadoDespesaViewModel(ILiquidacaoDespesa entity)
        {
            return new Models.DadoDespesaViewModel().CreateInstance(entity);
        }

        protected Models.DadoReferenciaViewModel InitializeDadoReferenciaViewModel(ILiquidacaoDespesa entity)
        {
            return new Models.DadoReferenciaViewModel().CreateInstance(entity.Referencia, entity.DataVencimento, entity.ReferenciaDigitada);
        }

        protected PesquisaCTViewModel InitializePesquisaCTViewModel(ILiquidacaoDespesa objModel, EnumTipoOperacaoEmpenho tipo)
        {
            return new PesquisaCTViewModel().CreateInstance(objModel, tipo);
        }

        protected static ILiquidacaoDespesa GetEntityByRegionalId<T>(T entity) where T : ILiquidacaoDespesa
        {
            ILiquidacaoDespesa assinaturas = default(T);

            if (entity is Subempenho)
            {
                var inclusao = (Subempenho)Convert.ChangeType(entity, typeof(Subempenho));
                assinaturas = App.SubempenhoService.Assinaturas(inclusao);
            }
            else if (entity is SubempenhoCancelamento)
            {
                var anulacao = (SubempenhoCancelamento)Convert.ChangeType(entity, typeof(SubempenhoCancelamento));
                assinaturas = App.SubempenhoCancelamentoService.Assinaturas(anulacao);
            }

            else if (entity is RapRequisicao)
            {
                var requisicao = (RapRequisicao)Convert.ChangeType(entity, typeof(RapRequisicao));
                assinaturas = App.RapRequisicaoService.Assinaturas(requisicao);
            }

            else if (entity is RapInscricao)
            {
                var inscricao = (RapInscricao)Convert.ChangeType(entity, typeof(RapInscricao));
                assinaturas = App.RapInscricaoService.Assinaturas(inscricao);
            }

            else if (entity is RapAnulacao)
            {
                var anulacaoRap = (RapAnulacao)Convert.ChangeType(entity, typeof(RapAnulacao));
                assinaturas = App.RapAnulacaoService.Assinaturas(anulacaoRap);
            }

            return assinaturas;
        }

        protected static IEnumerable<int> GetProgramAvailableYears(int year = default(int))
        {
            if (year == default(int)) { year = DateTime.Now.Year; }

            var range = DateTime.Now.Month < 3
                ? new int[] { year - 1, year }
                : new int[] { year };

            var yearList = App.ProgramaService.GetAnosPrograma()
                .Where(w => range.Contains(w))
                .ToList();

            if (yearList.All(a => a != DateTime.Now.Year))
                yearList.Add(DateTime.Now.Year);

            return yearList;
        }
    }
}