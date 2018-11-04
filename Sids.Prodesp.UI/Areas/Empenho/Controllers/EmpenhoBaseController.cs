namespace Sids.Prodesp.UI.Areas.Empenho.Controllers
{
    using Application;
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface;
    using Model.Interface.Base;
    using Model.Interface.Empenho;
    using Model.ValueObject;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using UI.Controllers.Base;

    public abstract class EmpenhoBaseController : ConsutasBaseController
    {
        protected IEnumerable<Regional> _regionalServiceList;

        protected IEnumerable<Programa> _programaServiceList;
        protected IEnumerable<Estrutura> _estruturaServiceList;
        protected IEnumerable<Fonte> _fonteServiceList;
        protected IEnumerable<Modalidade> _modalidadeServiceList;
        protected IEnumerable<OrigemMaterial> _origemMaterialServiceList;
        protected IEnumerable<Licitacao> _licitacaoServiceList;
        protected IEnumerable<AquisicaoTipo> _aquisicaoTipoServiceList;
        protected IEnumerable<Destino> _destinoServiceList;
        protected IEnumerable<EmpenhoCancelamentoTipo> _empenhoCancelamentoTipoServiceList;
        protected IEnumerable<EmpenhoTipo> _empenhoTipoServiceList;
        protected IEnumerable<int> _anoServiceList;
        protected IEnumerable<Municipio> _municipioList;

        protected IEnumerable<FiltroGridViewModel> _filterItems;
        protected readonly Usuario _userLoggedIn;
        protected int _modelId;



        protected EmpenhoBaseController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _filterItems = new List<FiltroGridViewModel>();
            _userLoggedIn = App.AutenticacaoService.GetUsuarioLogado();
            _modelId = 0;
        }



        protected IEnumerable<Regional> RegionalServiceList
        {
            get
            {
                return _regionalServiceList != null ? _regionalServiceList : (_regionalServiceList = App.RegionalService.Buscar(new Regional()).ToList());
            }
        }
        protected IEnumerable<Programa> ProgramaServiceList
        {
            get
            {
                return _programaServiceList != null ? _programaServiceList : (_programaServiceList = App.ProgramaService.Buscar(new Programa()).ToList());
            }
        }
        protected IEnumerable<Estrutura> EstruturaServiceList
        {
            get
            {
                return _estruturaServiceList != null ? _estruturaServiceList : (_estruturaServiceList = App.EstruturaService.Buscar(new Estrutura()).ToList());
            }
        }
        protected IEnumerable<Fonte> FonteServiceList
        {
            get
            {
                return _fonteServiceList != null ? _fonteServiceList : (_fonteServiceList = App.FonteService.Buscar(new Fonte()).ToList());
            }
        }
        protected IEnumerable<Modalidade> ModalidadeServiceList
        {
            get
            {
                return _modalidadeServiceList != null ? _modalidadeServiceList : (_modalidadeServiceList = App.ModalidadeService.Buscar(new Modalidade()).ToList());
            }
        }
        protected IEnumerable<OrigemMaterial> OrigemMaterialServiceList
        {
            get
            {
                return _origemMaterialServiceList != null ? _origemMaterialServiceList : (_origemMaterialServiceList = App.OrigemMaterialService.Buscar(new OrigemMaterial()).ToList());
            }
        }
        protected IEnumerable<Licitacao> LicitacaoServiceList
        {
            get
            {
                return _licitacaoServiceList != null ? _licitacaoServiceList : (_licitacaoServiceList = App.LicitacaoService.Buscar(new Licitacao()).ToList());
            }
        }
        protected IEnumerable<AquisicaoTipo> AquisicaoTipoServiceList
        {
            get
            {
                return _aquisicaoTipoServiceList != null ? _aquisicaoTipoServiceList : (_aquisicaoTipoServiceList = App.AquisicaoTipoService.Buscar(new AquisicaoTipo()).ToList());
            }
        }
        protected IEnumerable<Destino> DestinoServiceList
        {
            get
            {
                return _destinoServiceList != null ? _destinoServiceList : (_destinoServiceList = App.DestinoService.Buscar(new Destino()).ToList());
            }
        }
        protected IEnumerable<EmpenhoCancelamentoTipo> EmpenhoCancelamentoTipoServiceList
        {
            get
            {
                return _empenhoCancelamentoTipoServiceList != null ? _empenhoCancelamentoTipoServiceList : (_empenhoCancelamentoTipoServiceList = App.EmpenhoCancelamentoTipoService.Buscar(new EmpenhoCancelamentoTipo()).ToList());
            }
        }
        protected IEnumerable<EmpenhoTipo> EmpenhoTipoServiceList
        {
            get
            {
                return _empenhoTipoServiceList != null ? _empenhoTipoServiceList : (_empenhoTipoServiceList = App.EmpenhoTipoService.Buscar(new EmpenhoTipo()).ToList());
            }
        }
        protected IEnumerable<int> AnoServiceList
        {
            get
            {
                return _anoServiceList != null ? _anoServiceList : (_anoServiceList = GetProgramAvailableYears().ToList());
            }
        }
        protected IEnumerable<Municipio> MunicipioList
        {
            get
            {
                return _municipioList ?? (_municipioList = App.MunicipioService.Buscar(new Municipio()).ToList());
            }
        }



        protected T Display<T>(T objModel, bool isNewRecord) where T : IEmpenho
        {
            EnumTipoOperacaoEmpenho tipo = EnumTipoOperacaoEmpenho.Empenho;

            IEnumerable<IMes> monthList = new List<IMes>();
            IEnumerable<IEmpenhoItem> itemList = new List<IEmpenhoItem>();


            if (isNewRecord)
            {
                objModel = InitializeEntityModel(objModel);
            }


            objModel.NumeroContrato = objModel.NumeroContrato == "0" ? null : objModel.NumeroContrato;


            var msg = new List<string>
            {
                objModel.MensagemServicoProdesp,
                objModel.MensagemSiafemSiafisico
            };


            if (!string.IsNullOrEmpty(objModel.MensagemServicoProdesp) ||
                !string.IsNullOrEmpty(objModel.MensagemSiafemSiafisico))
            {
                ViewBag.MsgRetorno = string.Join("\n", msg.Where(x => x != null));
            }
            else
                ViewBag.MsgRetorno = null;


            if (objModel is Empenho)
            {
                tipo = EnumTipoOperacaoEmpenho.Empenho;
                var empenho = (Empenho)Convert.ChangeType(objModel, typeof(Empenho));
                ViewBag.PesquisaReserva = InitializePesquisaReservaViewModel(empenho.CodigoReserva);
                ViewBag.DadoObra = InitializeDadoObraViewModel(empenho);
                ViewBag.DadoEmpenho = InitializeDadoEmpenhoViewModel(empenho);
                ViewBag.DadoEntrega = InitializeDadoEntregaViewModel(empenho);

                monthList = App.EmpenhoMesService.Buscar(new EmpenhoMes() { Id = empenho.Id }).Cast<IMes>();
                itemList = App.EmpenhoItemService.Buscar(new EmpenhoItem() { EmpenhoId = empenho.Id }).Cast<IEmpenhoItem>();
            }


            if (objModel is EmpenhoReforco)
            {
                tipo = EnumTipoOperacaoEmpenho.Reforco;
                var reforco = (EmpenhoReforco)Convert.ChangeType(objModel, typeof(EmpenhoReforco));
                ViewBag.PesquisaReserva = InitializePesquisaReservaViewModel(reforco.CodigoReserva);
                ViewBag.PesquisaEmpenho = InitializePesquisaEmpenhoReforcoViewModel(reforco.CodigoEmpenho);
                ViewBag.PesquisaNotaEmpenho = InitializePesquisaNotaEmpenhoViewModel(reforco);

                monthList = App.EmpenhoReforcoMesService.Buscar(new EmpenhoReforcoMes() { Id = reforco.Id }).Cast<IMes>();
                itemList = App.EmpenhoReforcoItemService.Buscar(new EmpenhoReforcoItem() { EmpenhoId = reforco.Id }).Cast<IEmpenhoItem>();
            }

            if (objModel is EmpenhoCancelamento)
            {
                tipo = EnumTipoOperacaoEmpenho.Cancelamento;
                var cancelamento = (EmpenhoCancelamento)Convert.ChangeType(objModel, typeof(EmpenhoCancelamento));
                ViewBag.PesquisaReserva = InitializePesquisaReservaViewModel(cancelamento.CodigoReserva);
                ViewBag.PesquisaEmpenho = InitializePesquisaEmpenhoReforcoViewModel(cancelamento.CodigoEmpenho);
                ViewBag.PesquisaNotaEmpenho = InitializePesquisaNotaEmpenhoViewModel(cancelamento);
                ViewBag.DadoCancelamento = InitializeDadoCancelamentoViewModel(cancelamento);

                monthList = App.EmpenhoCancelamentoMesService.Buscar(new EmpenhoCancelamentoMes() { Id = cancelamento.Id }).Cast<IMes>();
                itemList = App.EmpenhoCancelamentoItemService.Buscar(new EmpenhoCancelamentoItem() { EmpenhoId = cancelamento.Id }).Cast<IEmpenhoItem>();
            }



            List<string> municipios = new List<string>();
            List<object> listaMunicipios = new List<object>();

            municipios.AddRange(MunicipioList.Select(x => x.Descricao).ToList());
            listaMunicipios.AddRange(MunicipioList.Select(x => new { Nome = x.Descricao, x.Codigo }).ToList());

            ViewBag.Municipios = municipios;
            ViewBag.ListaMunicipios = listaMunicipios;

            ViewBag.PesquisaCT = InitializePesquisaCTViewModel(objModel, tipo);
            ViewBag.PesquisaReservaContrato = InitializePesquisaReservaContratoViewModel(objModel.NumeroContrato);
            ViewBag.PesquisaReservaEstrutura = InitializePesquisaReservaEstruturaViewModel(objModel);
            ViewBag.DadoReforco = InitializeDadoReforcoViewModel(objModel);
            ViewBag.DadoDespesa = InitializeDadoDespesaViewModel(objModel);
            ViewBag.DadoAssinatura = InitializeDadoAssinaturaViewModel(objModel, isNewRecord);

            ViewBag.ValorEmpenho = new ValorEmpenhoViewModel().CreateInstance(objModel.Id, monthList);
            ViewBag.DadoEmpenhoItem = InitializeDadoEmpenhoItemViewModel(itemList, objModel.TransmitirSiafem);
            ViewBag.DadoEmpenhoItemGrid = InitializeDadoEmpenhoItemGridViewModel(itemList, objModel.TransmitirSiafem);

            InitializeCommonBags(objModel.ProgramaId);

            return objModel;
        }

        protected IEnumerable<FiltroGridViewModel> Display<T>(T objModel, FormCollection form) where T : IEmpenho
        {
            IEnumerable<IMes> monthList = new List<IMes>();
            IEnumerable<IEmpenho> objModelList = new List<IEmpenho>();

            var model = GenerateFilterViewModel(form, objModel);

            if (objModel is Empenho)
            {
                var empenho = (Empenho)Convert.ChangeType(model, typeof(Empenho));
                monthList = App.EmpenhoMesService.Buscar(new EmpenhoMes() { Id = empenho.Id }).Cast<IMes>();
                objModelList = App.EmpenhoService.BuscarGrid(empenho).Cast<IEmpenho>();
            }

            if (objModel is EmpenhoReforco)
            {
                var reforco = (EmpenhoReforco)Convert.ChangeType(model, typeof(EmpenhoReforco));
                monthList = App.EmpenhoReforcoMesService.Buscar(new EmpenhoReforcoMes() { Id = reforco.Id }).Cast<IMes>();
                objModelList = App.EmpenhoReforcoService.BuscarGrid(reforco).Cast<IEmpenho>();
            }

            if (objModel is EmpenhoCancelamento)
            {
                var cancelamento = (EmpenhoCancelamento)Convert.ChangeType(model, typeof(EmpenhoCancelamento));
                monthList = App.EmpenhoCancelamentoMesService.Buscar(new EmpenhoCancelamentoMes() { Id = cancelamento.Id }).Cast<IMes>();
                objModelList = App.EmpenhoCancelamentoService.BuscarGrid(cancelamento).Cast<IEmpenho>();
            }

            InitializeCommonBags(model.ProgramaId);

            return InitializeFiltroGridViewModel(objModelList, monthList);
        }

        protected IEnumerable<FiltroGridViewModel> Display<T>(T objModel) where T : IEmpenho
        {
            var model = InitializeEntityModel(objModel);
            InitializeCommonBags(model.ProgramaId);

            if (objModel is Empenho)
            {
                var empenho = (Empenho)Convert.ChangeType(model, typeof(Empenho));
                ViewBag.Filtro = InitializeFiltroViewModel(empenho);
            }
            else
            {
                ViewBag.Filtro = InitializeFiltroViewModel(model);
            }

            return new List<FiltroGridViewModel>();
        }

        protected IEmpenho GenerateFilterViewModel(FormCollection form, IEmpenho objModel)
        {
            var filter = InitializeFiltroViewModel(objModel);
            objModel = MapViewModelToEntityModel(form, objModel, ref filter);

            ViewBag.Filtro = filter;
            return objModel;
        }

        protected T GenerateFilterViewModel<T>(FormCollection form, T objModel) where T : IEmpenho
        {
            //FiltroViewModel filter = new FiltroViewModel();
            //   objModel = MapViewModelToEntityModel(form, objModel, ref filter);
            //filter = InitializeFiltroViewModel(objModel);

            var filter = InitializeFiltroViewModel(objModel);
            objModel = MapViewModelToEntityModel(form, objModel, ref filter);
            ViewBag.Filtro = filter;
            return objModel;
        }

        protected FiltroViewModel InitializeFiltroViewModel(IEmpenho objModel)
        {
            return new FiltroViewModel().CreateInstance(
                objModel, RegionalServiceList, ProgramaServiceList, EstruturaServiceList, FonteServiceList, LicitacaoServiceList, AnoServiceList);
        }

        protected FiltroViewModel InitializeFiltroViewModel(Empenho objModel)
        {
            return new FiltroViewModel().CreateInstance(
                objModel, RegionalServiceList, ProgramaServiceList, EstruturaServiceList, FonteServiceList, LicitacaoServiceList, AnoServiceList);
        }



        protected IEnumerable<FiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<IEmpenho> objModelList, IEnumerable<IMes> monthsList)
        {
            return objModelList.Select(model => new FiltroGridViewModel().CreateInstance(
                model, monthsList, ProgramaServiceList, EstruturaServiceList, FonteServiceList, LicitacaoServiceList, DestinoServiceList))
                ?? new List<FiltroGridViewModel>();
        }

        protected ValorEmpenhoViewModel InitializeValorEmpenhoViewModel(int modelId, IEnumerable<IMes> monthList)
        {
            return new ValorEmpenhoViewModel().CreateInstance(modelId, monthList);
        }

        protected PesquisaReservaViewModel InitializePesquisaReservaViewModel(string codigoReserva)
        {
            return new PesquisaReservaViewModel().CreateInstance(codigoReserva);
        }

        protected PesquisaNotaEmpenhoViewModel InitializePesquisaNotaEmpenhoViewModel(IEmpenho objModel)
        {
            var empenhoOriginal = default(string);
            var naturezaNe = default(string);
            var fonteSiafisico = default(string);

            if (objModel is EmpenhoReforco)
            {
                var eReforco = (EmpenhoReforco)Convert.ChangeType(objModel, typeof(EmpenhoReforco));
                empenhoOriginal = eReforco.CodigoEmpenhoOriginal;
                naturezaNe = eReforco.CodigoNaturezaNe;
                fonteSiafisico = eReforco.CodigoFonteSiafisico;
            }

            if (objModel is EmpenhoCancelamento)
            {
                var eCancelamento = (EmpenhoCancelamento)Convert.ChangeType(objModel, typeof(EmpenhoCancelamento));
                empenhoOriginal = eCancelamento.CodigoEmpenhoOriginal;
                naturezaNe = eCancelamento.CodigoNaturezaNe;
                fonteSiafisico = eCancelamento.CodigoFonteSiafisico;
            }

            return InitializePesquisaNotaEmpenhoViewModel(objModel, empenhoOriginal, naturezaNe, fonteSiafisico);
        }

        protected PesquisaNotaEmpenhoViewModel InitializePesquisaNotaEmpenhoViewModel(IEmpenho objModel, string empenhoOriginal, string naturezaNe, string fonteSiafisico)
        {
            return new PesquisaNotaEmpenhoViewModel().CreateInstance(
                objModel.CodigoUnidadeGestora, objModel.CodigoGestao, empenhoOriginal,
                objModel.DataEmissao, objModel.NumeroCNPJCPFUGCredor, objModel.CodigoGestaoCredor,
                objModel.ModalidadeId, objModel.LicitacaoId, naturezaNe,
                fonteSiafisico, objModel.NumeroProcessoNE,
                EstruturaServiceList, FonteServiceList, ModalidadeServiceList, LicitacaoServiceList);
        }

        protected DadoObraViewModel InitializeDadoObraViewModel(Empenho objModel)
        {
            return new DadoObraViewModel().CreateInstance(
                objModel.CodigoUGObra, objModel.TipoObraId, objModel.NumeroAnoContrato,
                objModel.NumeroMesContrato, objModel.NumeroObra);
        }

        protected DadoEmpenhoViewModel InitializeDadoEmpenhoViewModel(Empenho objModel)
        {
            return new DadoEmpenhoViewModel().CreateInstance(objModel,
                EmpenhoTipoServiceList, ModalidadeServiceList, OrigemMaterialServiceList, LicitacaoServiceList, DestinoServiceList, AquisicaoTipoServiceList);
        }

        protected DadoEntregaViewModel InitializeDadoEntregaViewModel(Empenho objModel)
        {
            return new DadoEntregaViewModel().CreateInstance(
                objModel.DataEntregaMaterial, objModel.DescricaoLogradouroEntrega,
                objModel.DescricaoBairroEntrega, objModel.DescricaoCidadeEntrega,
                objModel.NumeroCEPEntrega, objModel.DescricaoInformacoesAdicionaisEntrega);
        }

        protected DadoEmpenhoItemViewModel InitializeDadoEmpenhoItemViewModel(IEnumerable<IEmpenhoItem> itemList, bool isSiafem)
        {
            return isSiafem
                ? itemList.Select(model => new DadoEmpenhoItemViewModel().CreateInstance(
                    model, isSiafem)).FirstOrDefault() ?? new DadoEmpenhoItemViewModel()
                : new DadoEmpenhoItemViewModel();
        }

        protected IEnumerable<DadoEmpenhoItemViewModel> InitializeDadoEmpenhoItemGridViewModel(IEnumerable<IEmpenhoItem> itemList, bool isSiafem)
        {
            return !isSiafem
                ? itemList.Select(model => new DadoEmpenhoItemViewModel().CreateInstance(
                    model, isSiafem)).ToList()
                : new List<DadoEmpenhoItemViewModel>();
        }

        protected DadoReforcoViewModel InitializeDadoReforcoViewModel(IEmpenho objModel)
        {
            return new DadoReforcoViewModel().CreateInstance(objModel, DestinoServiceList);
        }

        protected DadoCancelamentoViewModel InitializeDadoCancelamentoViewModel(EmpenhoCancelamento objModel)
        {
            return new DadoCancelamentoViewModel().CreateInstance(objModel, EmpenhoCancelamentoTipoServiceList);
        }

        protected DadoDespesaViewModel InitializeDadoDespesaViewModel(IEmpenho objModel)
        {
            return new DadoDespesaViewModel().CreateInstance(objModel);
        }

        protected DadoAssinaturaViewModel InitializeDadoAssinaturaViewModel<T>(T objModel, bool isNewRecord) where T : IEmpenho
        {
            return GetSignaturesFromDomainModel(objModel, isNewRecord);
        }

        protected PesquisaEmpenhoReforcoViewModel InitializePesquisaEmpenhoReforcoViewModel(string codigoReforco)
        {
            return new PesquisaEmpenhoReforcoViewModel().CreateInstance(codigoReforco);
        }

        protected PesquisaReservaEstruturaViewModel InitializePesquisaReservaEstruturaViewModel(IEmpenho objModel)
        {
            return new PesquisaReservaEstruturaViewModel().CreateInstance(
                objModel, RegionalServiceList, ProgramaServiceList, EstruturaServiceList, FonteServiceList, AnoServiceList);
        }

        protected PesquisaReservaContratoViewModel InitializePesquisaReservaContratoViewModel(string numeroContrato)
        {
            return new PesquisaReservaContratoViewModel().CreateInstance(numeroContrato);
        }

        protected PesquisaCTViewModel InitializePesquisaCTViewModel(IEmpenho objModel, EnumTipoOperacaoEmpenho tipo)
        {
            return new PesquisaCTViewModel().CreateInstance(objModel, tipo);
        }

        protected T MapViewModelToEntityModel<T>(FormCollection form, T objModel, ref FiltroViewModel viewModel) where T : IEmpenho
        {
            if (!string.IsNullOrEmpty(form["NumeroEmpenhoProdesp"]))
            {
                objModel.NumeroEmpenhoProdesp = form["NumeroEmpenhoProdesp"];
                viewModel.NumeroEmpenhoProdesp = form["NumeroEmpenhoProdesp"];
            }

            if (!string.IsNullOrEmpty(form["NumeroEmpenhoSiafem"]))
            {
                objModel.NumeroEmpenhoSiafem = form["NumeroEmpenhoSiafem"];
                viewModel.NumeroEmpenhoSiafem = form["NumeroEmpenhoSiafem"];
            }

            if (!string.IsNullOrEmpty(form["NumeroEmpenhoSiafisico"]))
            {
                objModel.NumeroEmpenhoSiafisico = form["NumeroEmpenhoSiafisico"];
                viewModel.NumeroEmpenhoSiafisico = form["NumeroEmpenhoSiafisico"];
            }

            if (!string.IsNullOrEmpty(form["NumeroProcesso"]))
            {
                objModel.NumeroProcesso = form["NumeroProcesso"];
                viewModel.NumeroProcesso = form["NumeroProcesso"];
            }

            if (!string.IsNullOrEmpty(form["CodigoAplicacaoObra"]))
            {
                objModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"].Replace("-", "");
                viewModel.CodigoAplicacaoObra = form["CodigoAplicacaoObra"];
            }

            if (!string.IsNullOrEmpty(form["NumeroAnoExercicio"]))
            {
                objModel.NumeroAnoExercicio = Convert.ToInt32(form["NumeroAnoExercicio"]);
                viewModel.NumeroAnoExercicio = form["NumeroAnoExercicio"];
            }

            if (!string.IsNullOrEmpty(form["RegionalId"]))
            {
                objModel.RegionalId = Convert.ToInt16(form["RegionalId"]);
                viewModel.RegionalId = form["RegionalId"];
            }

            if (!string.IsNullOrEmpty(form["CodigoPtres"]))
            {
                viewModel.CodigoPtres = form["CodigoPtres"];
            }

            if (!string.IsNullOrEmpty(form["ProgramaId"]))
            {
                objModel.ProgramaId = Convert.ToInt32(form["ProgramaId"]);
                viewModel.ProgramaId = form["ProgramaId"];
            }

            if (!string.IsNullOrEmpty(form["NaturezaId"]))
            {
                objModel.NaturezaId = Convert.ToInt32(form["NaturezaId"]);
                viewModel.NaturezaId = form["NaturezaId"];
            }

            if (!string.IsNullOrEmpty(form["CodigoNaturezaItem"]))
            {
                objModel.CodigoNaturezaItem = form["CodigoNaturezaItem"];
                viewModel.CodigoNaturezaItem = form["CodigoNaturezaItem"];
            }

            if (!string.IsNullOrEmpty(form["FonteId"]))
            {
                objModel.FonteId = Convert.ToInt32(form["FonteId"]);
                viewModel.FonteId = form["FonteId"];
            }

            if (!string.IsNullOrEmpty(form["StatusProdesp"]))
            {
                objModel.StatusProdesp = form["StatusProdesp"];
                viewModel.StatusProdesp = form["StatusProdesp"];
            }

            if (!string.IsNullOrEmpty(form["StatusSiafemSiafisico"]))
            {
                objModel.StatusSiafemSiafisico = form["StatusSiafemSiafisico"];
                viewModel.StatusSiafemSiafisico = form["StatusSiafemSiafisico"];
            }

            if (!string.IsNullOrEmpty(form["NumeroContrato"]))
            {
                objModel.NumeroContrato = form["NumeroContrato"].Replace(".", "").Replace("-", "");
                viewModel.NumeroContrato = form["NumeroContrato"];
            }

            if (!string.IsNullOrEmpty(form["LicitacaoId"]))
            {
                objModel.LicitacaoId = form["LicitacaoId"];
                viewModel.LicitacaoId = form["LicitacaoId"];
            }

            if (!string.IsNullOrEmpty(form["NumeroCNPJCPFUGCredor"]))
            {
                objModel.NumeroCNPJCPFUGCredor = form["NumeroCNPJCPFUGCredor"];
                viewModel.NumeroCNPJCPFUGCredor = form["NumeroCNPJCPFUGCredor"];
            }

            if (!string.IsNullOrEmpty(form["CodigoGestaoCredor"]))
            {
                objModel.CodigoGestaoCredor = form["CodigoGestaoCredor"];
                viewModel.CodigoGestaoCredor = form["CodigoGestaoCredor"];
            }

            if (!string.IsNullOrEmpty(form["CodigoCredorOrganizacao"]))
            {
                objModel.CodigoCredorOrganizacao = Convert.ToInt32(form["CodigoCredorOrganizacao"]);
                viewModel.CodigoCredorOrganizacao = form["CodigoCredorOrganizacao"];
            }

            if (!string.IsNullOrEmpty(form["NumeroCNPJCPFFornecedor"]))
            {
                objModel.NumeroCNPJCPFFornecedor = form["NumeroCNPJCPFFornecedor"];
                viewModel.NumeroCNPJCPFFornecedor = form["NumeroCNPJCPFFornecedor"];
            }

            if (!string.IsNullOrEmpty(form["DataCadastramentoDe"]))
            {
                objModel.DataCadastramentoDe = Convert.ToDateTime(form["DataCadastramentoDe"]);
                viewModel.DataCadastramentoDe = form["DataCadastramentoDe"];
            }

            if (!string.IsNullOrEmpty(form["DataCadastramentoAte"]))
            {
                objModel.DataCadastramentoAte = Convert.ToDateTime(form["DataCadastramentoAte"]);
                viewModel.DataCadastramentoAte = form["DataCadastramentoAte"];
            }

            return objModel;
        }

        protected void InitializeCommonBags(int programId)
        {
            ViewBag.Usuario = _userLoggedIn;
            ViewBag.Estrutura = EstruturaServiceList;
            ViewBag.Regional = RegionalServiceList;
            ViewBag.Fontes = FonteServiceList;
            ViewBag.Programas = ProgramaServiceList;
            ViewBag.Programa = ProgramaServiceList.FirstOrDefault(f => f.Codigo == programId);
        }

        protected T InitializeEntityModel<T>(T objModel) where T : IEmpenho
        {
            objModel.Id = default(int);

            objModel.DataTransmitidoProdesp = default(DateTime);
            objModel.DataTransmitidoSiafem = default(DateTime);
            objModel.DataTransmitidoSiafisico = default(DateTime);
            objModel.NumeroCT = null;

            objModel.MensagemServicoProdesp = null;
            objModel.MensagemServicoSiafem = null;
            objModel.MensagemServicoSiafisico = null;

            objModel.DataCadastramentoDe = DateTime.Now;
            objModel.NumeroAnoExercicio = DateTime.Now.Year;
            objModel.CodigoGestao = "16055";
            objModel.CodigoUO = 16055;

            objModel.RegionalId = _userLoggedIn.RegionalId == 1
                ? Convert.ToInt16(16)
                : Convert.ToInt16(_userLoggedIn.RegionalId);

            objModel.DataEmissao = DateTime.Now;

            objModel.TransmitidoSiafem = false;
            objModel.TransmitidoSiafisico = false;
            objModel.TransmitidoProdesp = false;

            objModel.StatusProdesp = "N";
            objModel.StatusSiafemSiafisico = "N";
            objModel.StatusSiafisicoCT = "N";
            objModel.StatusSiafisicoNE = "N";

            objModel.NumeroEmpenhoProdesp = null;
            objModel.NumeroEmpenhoSiafem = null;
            objModel.NumeroEmpenhoSiafisico = null;

            GetSignaturesFromRepository(ref objModel);

            return objModel;
        }

        protected void GetSignaturesFromRepository<T>(ref T objModel) where T : IEmpenho
        {
            var entity = GetEntityByRegionalId(objModel, Convert.ToInt16(objModel.RegionalId));

            if (entity != null)
            {
                objModel.CodigoAutorizadoAssinatura = entity.CodigoAutorizadoAssinatura;
                objModel.CodigoAutorizadoGrupo = entity.CodigoAutorizadoGrupo;
                objModel.CodigoAutorizadoOrgao = entity.CodigoAutorizadoOrgao;
                objModel.NomeAutorizadoAssinatura = entity.NomeAutorizadoAssinatura;
                objModel.DescricaoAutorizadoCargo = entity.DescricaoAutorizadoCargo;

                objModel.CodigoExaminadoAssinatura = entity.CodigoExaminadoAssinatura;
                objModel.CodigoExaminadoGrupo = entity.CodigoExaminadoGrupo;
                objModel.CodigoExaminadoOrgao = entity.CodigoExaminadoOrgao;
                objModel.NomeExaminadoAssinatura = entity.NomeExaminadoAssinatura;
                objModel.DescricaoExaminadoCargo = entity.DescricaoExaminadoCargo;

                objModel.CodigoResponsavelAssinatura = entity.CodigoResponsavelAssinatura;
                objModel.CodigoResponsavelGrupo = entity.CodigoResponsavelGrupo;
                objModel.CodigoResponsavelOrgao = entity.CodigoResponsavelOrgao;
                objModel.NomeResponsavelAssinatura = entity.NomeResponsavelAssinatura;
                objModel.DescricaoResponsavelCargo = entity.DescricaoResponsavelCargo;
            }
        }

        protected DadoAssinaturaViewModel GetSignaturesFromDomainModel<T>(T objModel, bool isNewRecord = false) where T : IEmpenho
        {
            if (isNewRecord) GetSignaturesFromRepository(ref objModel);

            var autorizado = new Assinatura()
            {
                CodigoAssinatura = objModel.CodigoAutorizadoAssinatura,
                CodigoGrupo = objModel.CodigoAutorizadoGrupo,
                CodigoOrgao = objModel.CodigoAutorizadoOrgao,
                NomeAssinatura = objModel.NomeAutorizadoAssinatura,
                DescricaoCargo = objModel.DescricaoAutorizadoCargo
            };

            var examinado = new Assinatura()
            {
                CodigoAssinatura = objModel.CodigoExaminadoAssinatura,
                CodigoGrupo = objModel.CodigoExaminadoGrupo,
                CodigoOrgao = objModel.CodigoExaminadoOrgao,
                NomeAssinatura = objModel.NomeExaminadoAssinatura,
                DescricaoCargo = objModel.DescricaoExaminadoCargo
            };

            var responsavel = new Assinatura()
            {
                CodigoAssinatura = objModel.CodigoResponsavelAssinatura,
                CodigoGrupo = objModel.CodigoResponsavelGrupo,
                CodigoOrgao = objModel.CodigoResponsavelOrgao,
                NomeAssinatura = objModel.NomeResponsavelAssinatura,
                DescricaoCargo = objModel.DescricaoResponsavelCargo
            };

            return new DadoAssinaturaViewModel().CreateInstance(autorizado, examinado, responsavel);
        }

        protected static IEmpenho GetEntityByRegionalId<T>(T objModel, short regionalId) where T : IEmpenho
        {
            IEmpenho list = null;

            if (objModel is Empenho)
            {
                list = App.EmpenhoService.BuscarAssinaturas(new Empenho { RegionalId = regionalId });
            }
            else if (objModel is EmpenhoReforco)
            {
                list = App.EmpenhoReforcoService.BuscarAssinaturas(new EmpenhoReforco { RegionalId = regionalId });
            }
            else if (objModel is EmpenhoCancelamento)
            {
                list = App.EmpenhoCancelamentoService.BuscarAssinaturas(new EmpenhoCancelamento { RegionalId = regionalId });
            }

            return list;
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