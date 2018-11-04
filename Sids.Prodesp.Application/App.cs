using Sids.Prodesp.Core.Services.PagamentoContaUnica;
using Sids.Prodesp.Core.Services.WebService.LiquidacaoDespesa;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Services.LiquidacaoDespesa;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaDer;

namespace Sids.Prodesp.Application
{

    using Core.Base;
    using Core.Services.Configuracao;
    using Core.Services.Empenho;
    using Core.Services.LiquidacaoDespesa;
    using Core.Services.Movimentacao;
    using Core.Services.PagamentoDer;
    using Core.Services.Reserva;
    using Core.Services.Security;
    using Core.Services.Seguranca;
    using Core.Services.WebService;
    using Core.Services.WebService.Empenho;
    using Core.Services.WebService.PagamentoContaUnica;
    using Core.Services.WebService.Reserva;
    using Core.Services.WebService.Seguranca;
    using Infrastructure.DataBase.Configuracao;
    using Infrastructure.DataBase.Empenho;
    using Infrastructure.DataBase.LiquidacaoDespesa;
    using Infrastructure.DataBase.PagamentoDer;
    using Infrastructure.DataBase.Reserva;
    using Infrastructure.DataBase.Seguranca;
    using Infrastructure.Log;
    using Infrastructure.Services;
    using Infrastructure.Services.Empenho;
    using Infrastructure.Services.Movimentacao;
    using Infrastructure.Services.PagamentoContaUnica;
    using Infrastructure.Services.Reserva;
    using Infrastructure.Services.Seguranca;
    using Model.Interface.Service.PagamentoContaUnica;

    public static class App
    {
        #region Siafem
        private static SiafemReservaService _siafemReservaService;
        public static SiafemReservaService SiafemReservaService
        {
            get
            {
                return _siafemReservaService ?? (_siafemReservaService = new SiafemReservaService(new LogErrorDal(), new SiafemReservaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal()));
            }
        }

        private static SiafemEmpenhoService _siafemEmpenhoService;
        public static SiafemEmpenhoService SiafemEmpenhoService
        {
            get
            {
                return _siafemEmpenhoService ?? (_siafemEmpenhoService = new SiafemEmpenhoService(new LogErrorDal(), new SiafemEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal()));
            }
        }


        private static SiafemLiquidacaoDespesaService _siafemLiquidacaoDespesaService;
        public static SiafemLiquidacaoDespesaService SiafemLiquidacaoDespesaService
        {
            get
            {
                return _siafemLiquidacaoDespesaService ?? (_siafemLiquidacaoDespesaService = new SiafemLiquidacaoDespesaService(new LogErrorDal(), new SiafemLiquidacaoDespesaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal()));
            }
        }


        private static SiafemSegurancaService _siafemSegurancaService;
        public static SiafemSegurancaService SiafemSegurancaService
        {
            get
            {
                return _siafemSegurancaService ?? (_siafemSegurancaService = new SiafemSegurancaService(new LogErrorDal(), new SiafemSegurancaWs()));
            }
        }
        #endregion


        #region Prodesp
        private static ProdespReservaService _prodespReservaService;
        public static ProdespReservaService ProdespReservaService
        {
            get
            {
                return _prodespReservaService ?? (_prodespReservaService = new ProdespReservaService(new LogErrorDal(), new ProdespReservaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new RegionalDal()));
            }
        }


        private static ProdespEmpenhoService _prodespEmpenhoService;
        public static ProdespEmpenhoService ProdespEmpenhoService
        {
            get
            {
                return _prodespEmpenhoService ?? (_prodespEmpenhoService = new ProdespEmpenhoService(new LogErrorDal(), new ProdespEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new RegionalDal()));
            }
        }


        private static ProdespLiquidacaoDespesaService _prodespLiquidacaoDespesaService;
        public static ProdespLiquidacaoDespesaService ProdespLiquidacaoDespesaService
        {
            get
            {
                return _prodespLiquidacaoDespesaService ?? (_prodespLiquidacaoDespesaService = new ProdespLiquidacaoDespesaService(new LogErrorDal(), new ProdespLiquidacaoDespesaWs(), new EstruturaDal()));
            }
        }

        private static ProdespPagamentoContaUnicaService _prodespPagamentoContaUnicaService;
        public static ProdespPagamentoContaUnicaService ProdespPagamentoContaUnicaService
        {
            get
            {
                return _prodespPagamentoContaUnicaService ?? (_prodespPagamentoContaUnicaService = new ProdespPagamentoContaUnicaService(new LogErrorDal(), new ProdespPagamentoContaUnicaWs()));
            }
        }



        private static ProdespPagamentoContaDerService _prodespPagamentoContaDerService;
        public static ProdespPagamentoContaDerService ProdespPagamentoContaDerService
        {
            get
            {
                return _prodespPagamentoContaDerService ?? (_prodespPagamentoContaDerService = new ProdespPagamentoContaDerService(new LogErrorDal(), new ProdespPagamentoContaDerWs()));
            }
        }
        #endregion


        #region Base
        private static BaseService _baseService;
        public static BaseService BaseService
        {
            get
            {
                return _baseService ?? (_baseService = new BaseService(new LogErrorDal()));
            }
        }
        #endregion


        #region Autenticação
        private static AutenticacaoService _autenticacaoService;
        public static AutenticacaoService AutenticacaoService
        {
            get
            {
                return _autenticacaoService ?? (_autenticacaoService = new AutenticacaoService(
                    new LogErrorDal(), new UsuarioDal(), new FuncionalidadeDal(),
                    new PerfilFuncionalidadeDal(), new PerfilUsuarioDal(), new PerfilAcaoDal(),
                    new PerfilDal(), new AcaoDal(), new SiafemSegurancaWs(), new RegionalDal()));
            }
        }
        #endregion


        #region Menu
        private static MenuService _menuService;
        public static MenuService MenuService
        {
            get
            {
                return _menuService ?? (_menuService = new MenuService(
              new LogErrorDal(), new MenuDal(), new MenuItemDal()));
            }
        }

        private static MenuItemService _menuItemService;
        public static MenuItemService MenuItemService
        {
            get
            {
                return _menuItemService ?? (_menuItemService = new MenuItemService(
              new LogErrorDal(), new MenuItemDal()));
            }
        }
        #endregion


        #region Funcionalidades
        private static FuncionalidadeService _funcionalidadeService;
        public static FuncionalidadeService FuncionalidadeService
        {
            get
            {
                return _funcionalidadeService ?? (_funcionalidadeService = new FuncionalidadeService(
              new LogErrorDal(), new FuncionalidadeDal(), new FuncionalidadeAcaoDal(), new PerfilAcaoDal(), new AcaoDal()));
            }
        }

        private static FuncionalidadeAcaoService _funcionalidadeAcaoService;
        public static FuncionalidadeAcaoService FuncionalidadeAcaoService
        {
            get { return _funcionalidadeAcaoService ?? (_funcionalidadeAcaoService = new FuncionalidadeAcaoService(new LogErrorDal(), new FuncionalidadeAcaoDal())); }
        }
        #endregion


        #region Perfil
        private static PerfilService _perfilService;
        public static PerfilService PerfilService
        {
            get
            {
                return _perfilService ?? (_perfilService = new PerfilService(
              new LogErrorDal(), new PerfilDal(), new PerfilAcaoDal()));
            }
        }

        private static PerfilFuncionalidadeService _perfilFuncionalidadeService;
        public static PerfilFuncionalidadeService PerfilFuncionalidadeService
        {
            get
            {
                return _perfilFuncionalidadeService ?? (_perfilFuncionalidadeService = new PerfilFuncionalidadeService(
              new LogErrorDal(), new PerfilFuncionalidadeDal()));
            }
        }

        private static PerfilAcaoService _perfilAcaoService;
        public static PerfilAcaoService PerfilAcaoService
        {
            get
            {
                return _perfilAcaoService ?? (_perfilAcaoService = new PerfilAcaoService(
              new LogErrorDal(), new PerfilAcaoDal()));
            }
        }

        private static PerfilUsuarioService _perfilUsuarioService;
        public static PerfilUsuarioService PerfilUsuarioService
        {
            get { return _perfilUsuarioService ?? (_perfilUsuarioService = new PerfilUsuarioService(new LogErrorDal(), new PerfilUsuarioDal())); }
        }
        #endregion


        #region Usuario
        private static UsuarioService _usuaioService;
        public static UsuarioService UsuarioService
        {
            get { return _usuaioService ?? (_usuaioService = new UsuarioService(new LogErrorDal(), new UsuarioDal(), new PerfilUsuarioDal(), new PerfilDal(), new SiafemSegurancaWs(), new RegionalDal())); }
        }
        #endregion


        #region Sistema
        private static SistemaService _sistemaService;
        public static SistemaService SistemaService
        {
            get
            {
                return _sistemaService ?? (_sistemaService = new SistemaService(new LogErrorDal(), new SistemaDal()));
            }
        }
        #endregion

        
        #region Área
        private static AreaService _areaService;
        public static AreaService AreaService
        {
            get
            {
                return _areaService ?? (_areaService = new AreaService(new LogErrorDal(), new AreaDal()));
            }
        }
        #endregion

        
        #region Ação
        private static AcaoService _acaoService;
        public static AcaoService AcaoService
        {
            get
            {
                return _acaoService ?? (_acaoService = new AcaoService(new LogErrorDal(), new AcaoDal()));
            }
        }
        #endregion

        
        #region Regional
        private static RegionalService _regionalService;
        public static RegionalService RegionalService
        {
            get
            {
                return _regionalService ?? (_regionalService = new RegionalService(new LogErrorDal(), new RegionalDal()));
            }
        }
        #endregion


        #region Programa
        private static ProgramaService _programaService;
        public static ProgramaService ProgramaService
        {
            get
            {
                return _programaService ?? (_programaService = new ProgramaService(new LogErrorDal(), new ProgramaDal(), new EstruturaDal()));
            }
        }
        #endregion


        #region Estrutura
        private static EstruturaService _estruturaService;
        public static EstruturaService EstruturaService
        {
            get
            {
                return _estruturaService ?? (_estruturaService = new EstruturaService(new LogErrorDal(), new EstruturaDal(), new ReservaDal(), new ProgramaDal()));
            }
        }
        #endregion    


        #region Fonte
        private static FonteService _fonteService;
        public static FonteService FonteService
        {
            get { return _fonteService ?? (_fonteService = new FonteService(new LogErrorDal(), new FonteDal())); }
        }
        #endregion


        #region Destino
        private static DestinoService _destinoService;
        public static DestinoService DestinoService
        {
            get { return _destinoService ?? (_destinoService = new DestinoService(new LogErrorDal(), new DestinoDal())); }
        }
        #endregion        

        #region Minicipio
        private static MunicipioService _municipioService;
        public static MunicipioService MunicipioService
        {
            get { return _municipioService ?? (_municipioService = new MunicipioService(new LogErrorDal(), new MunicipioDal())); }
        }
        #endregion        

        #region Modalidade
        private static ModalidadeService _modalidadeService;
        public static ModalidadeService ModalidadeService
        {
            get
            {
                return _modalidadeService ?? (_modalidadeService = new ModalidadeService(
                    new LogErrorDal(), new ModalidadeDal()));
            }
        }
        #endregion


        #region Licitação
        private static LicitacaoService _licitacaoService;
        public static LicitacaoService LicitacaoService
        {
            get
            {
                return _licitacaoService ?? (_licitacaoService = new LicitacaoService(
                    new LogErrorDal(), new LicitacaoDal()));
            }
        }
        #endregion


        #region Tipo de Aquisição
        private static AquisicaoTipoService _aquisicaoTipoService;
        public static AquisicaoTipoService AquisicaoTipoService
        {
            get
            {
                return _aquisicaoTipoService ?? (_aquisicaoTipoService = new AquisicaoTipoService(
                    new LogErrorDal(), new AquisicaoTipoDal()));
            }
        }
        #endregion


        #region Origem do Material
        private static OrigemMaterialService _origemMaterialService;
        public static OrigemMaterialService OrigemMaterialService
        {
            get
            {
                return _origemMaterialService ?? (_origemMaterialService = new OrigemMaterialService(
                    new LogErrorDal(), new OrigemMaterialDal()));
            }
        }
        #endregion


        #region Tipo de Reserva
        private static TipoReservaService _tipoReservaService;
        public static TipoReservaService TipoReservaService
        {
            get
            {
                return _tipoReservaService ?? (_tipoReservaService = new TipoReservaService(new LogErrorDal(), new TipoReservaDal()));
            }
        }
        #endregion


        #region Tipo de Empenho
        private static EmpenhoTipoService _empenhoTipoService;
        public static EmpenhoTipoService EmpenhoTipoService
        {
            get
            {
                return _empenhoTipoService ?? (_empenhoTipoService = new EmpenhoTipoService(
                    new LogErrorDal(), new EmpenhoTipoDal()));
            }
        }
        #endregion


        #region Tipo de Cancelamento de Empenho
        private static EmpenhoCancelamentoTipoService _empenhoCancelamentoTipoService;
        public static EmpenhoCancelamentoTipoService EmpenhoCancelamentoTipoService
        {
            get
            {
                return _empenhoCancelamentoTipoService ?? (_empenhoCancelamentoTipoService = new EmpenhoCancelamentoTipoService(
                    new LogErrorDal(), new EmpenhoCancelamentoTipoDal()));
            }
        }
        #endregion


        #region Tipo de Natureza
        private static NaturezaTipoService _naturezaTipoService;
        public static NaturezaTipoService NaturezaTipoService
        {
            get
            {
                return _naturezaTipoService ?? (_naturezaTipoService = new NaturezaTipoService(
                    new NaturezaTipoDal()));
            }
        }
        #endregion


        #region Tipo de Cenário
        private static CenarioTipoService _cenarioTipoService;
        public static CenarioTipoService CenarioTipoService
        {
            get
            {
                return _cenarioTipoService ?? (_cenarioTipoService = new CenarioTipoService(
                    new CenarioTipoDal()));
            }
        }
        #endregion


        #region Tipo de Evento
        private static EventoTipoService _eventoTipoService;
        public static EventoTipoService EventoTipoService
        {
            get
            {
                return _eventoTipoService ?? (_eventoTipoService = new EventoTipoService(
                    new EventoTipoDal()));
            }
        }


        private static CodigoEventoService _codigoTipoService;
        public static CodigoEventoService CodigoEventoService
        {
            get
            {
                return _codigoTipoService ?? (_codigoTipoService = new CodigoEventoService(new CodigoEventoDal()));
            }
        }
        #endregion


        #region Tipo de Obra
        private static ObraTipoService _obraTipoService;
        public static ObraTipoService ObraTipoService
        {
            get
            {
                return _obraTipoService ?? (_obraTipoService = new ObraTipoService(
                    new ObraTipoDal()));
            }
        }
        #endregion


        #region Tipo de Serviço
        private static ServicoTipoService _servicoTipoService;
        public static ServicoTipoService ServicoTipoService
        {
            get
            {
                return _servicoTipoService ?? (_servicoTipoService = new ServicoTipoService(
                    new ServicoTipoDal()));
            }
        }
        #endregion


        #region Comum
        private static CommonService _commonService;
        public static CommonService CommonService
        {
            get
            {
                return _commonService ?? (_commonService = new CommonService(new LogErrorDal(), new CommonWs(), new ProdespReservaWs(), new SiafemReservaWs(), new SiafemEmpenhoWs(), new ChaveCicsmoDal()));
            }
        }
        #endregion


        #region Reserva
        private static ReservaService _reservaService;
        public static ReservaService ReservaService
        {
            get
            {
                return _reservaService ?? (_reservaService = new ReservaService(new LogErrorDal(), new ReservaDal(), new ProdespReservaWs(), new SiafemReservaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new ReservaMesDal(), new RegionalDal(), new ChaveCicsmoDal(), new CommonWs()));
            }
        }

        private static ReservaMesService _reservaMesService;
        public static ReservaMesService ReservaMesService
        {
            get
            {
                return _reservaMesService ?? (_reservaMesService = new ReservaMesService(new LogErrorDal(), new ReservaMesDal()));
            }
        }
        #endregion


        #region Reforço de Reserva
        private static ReservaReforcoService _reforcoReservaService;
        public static ReservaReforcoService ReservaReforcoService
        {
            get
            {
                return _reforcoReservaService ?? (_reforcoReservaService = new ReservaReforcoService(new LogErrorDal(), new ReservaReforcoDal(), new ProdespReservaWs(), new SiafemReservaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new ReservaReforcoMesDal(), new RegionalDal(), new ChaveCicsmoDal()));
            }
        }

        private static ReservaReforcoMesService _reforcoMesService;
        public static ReservaReforcoMesService ReforcoMesService
        {
            get
            {
                return _reforcoMesService ?? (_reforcoMesService = new ReservaReforcoMesService(new LogErrorDal(), new ReservaReforcoMesDal()));
            }
        }
        #endregion


        #region Cancelamento de Reserva
        private static ReservaCancelamentoService _cancelamentoReservaService;
        public static ReservaCancelamentoService ReservaCancelamentoService
        {
            get
            {
                return _cancelamentoReservaService ?? (_cancelamentoReservaService = new ReservaCancelamentoService(new LogErrorDal(), new ReservaCancelamentoDal(), new ProdespReservaWs(), new SiafemReservaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new ReservaCancelamentoMesDal(), new RegionalDal(), new ChaveCicsmoDal()));
            }
        }

        private static ReservaCancelamentoMesService _cancelamentoMesService;
        public static ReservaCancelamentoMesService CancelamentoMesService
        {
            get
            {
                return _cancelamentoMesService ?? (_cancelamentoMesService = new ReservaCancelamentoMesService(new LogErrorDal(), new ReservaCancelamentoMesDal()));
            }
        }
        #endregion


        #region Empenho
        private static EmpenhoService _empenhoService;
        public static EmpenhoService EmpenhoService
        {
            get
            {
                return _empenhoService ?? (_empenhoService = new EmpenhoService(
                    new LogErrorDal(), new EmpenhoDal(), new EmpenhoMesDal(), new EmpenhoItemDal(), new ProdespEmpenhoWs(),
                    new SiafemEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new RegionalDal(), new ChaveCicsmoDal(), new CommonWs()));
            }
        }

        private static EmpenhoMesService _empenhoMesService;
        public static EmpenhoMesService EmpenhoMesService
        {
            get
            {
                return _empenhoMesService ?? (_empenhoMesService = new EmpenhoMesService(
                    new LogErrorDal(), new EmpenhoMesDal()));
            }
        }

        private static EmpenhoItemService _empenhoItemService;
        public static EmpenhoItemService EmpenhoItemService
        {
            get
            {
                return _empenhoItemService ?? (_empenhoItemService = new EmpenhoItemService(
                    new LogErrorDal(), new EmpenhoItemDal()));
            }
        }
        #endregion


        #region Reforço do Empenho
        private static EmpenhoReforcoService _empenhoReforcoService;
        public static EmpenhoReforcoService EmpenhoReforcoService
        {
            get
            {
                return _empenhoReforcoService ?? (_empenhoReforcoService = new EmpenhoReforcoService(
                    new LogErrorDal(), new EmpenhoReforcoDal(), new EmpenhoReforcoMesDal(), new EmpenhoReforcoItemDal(), new ProdespEmpenhoWs(),
                    new SiafemEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new RegionalDal(), new ChaveCicsmoDal(), new CommonWs()));
            }
        }

        private static EmpenhoReforcoMesService _empenhoReforcoMesService;
        public static EmpenhoReforcoMesService EmpenhoReforcoMesService
        {
            get
            {
                return _empenhoReforcoMesService ?? (_empenhoReforcoMesService = new EmpenhoReforcoMesService(
                    new LogErrorDal(), new EmpenhoReforcoMesDal()));
            }
        }

        private static EmpenhoReforcoItemService _empenhoReforcoItemService;
        public static EmpenhoReforcoItemService EmpenhoReforcoItemService
        {
            get
            {
                return _empenhoReforcoItemService ?? (_empenhoReforcoItemService = new EmpenhoReforcoItemService(
                    new LogErrorDal(), new EmpenhoReforcoItemDal()));
            }
        }
        #endregion


        #region Cancelamento do Empenho 
        private static EmpenhoCancelamentoService _empenhoCancelamentoService;
        public static EmpenhoCancelamentoService EmpenhoCancelamentoService
        {
            get
            {
                return _empenhoCancelamentoService ?? (_empenhoCancelamentoService = new EmpenhoCancelamentoService(
                    new LogErrorDal(), new EmpenhoCancelamentoDal(), new EmpenhoCancelamentoMesDal(), new EmpenhoCancelamentoItemDal(), new ProdespEmpenhoWs(),
                    new SiafemEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new RegionalDal(), new ChaveCicsmoDal(), new CommonWs()));
            }
        }

        private static EmpenhoCancelamentoMesService _empenhoCancelamentoMesService;
        public static EmpenhoCancelamentoMesService EmpenhoCancelamentoMesService
        {
            get
            {
                return _empenhoCancelamentoMesService ?? (_empenhoCancelamentoMesService = new EmpenhoCancelamentoMesService(
                    new LogErrorDal(), new EmpenhoCancelamentoMesDal()));
            }
        }

        private static EmpenhoCancelamentoItemService _empenhoCancelamentoItemService;
        public static EmpenhoCancelamentoItemService EmpenhoCancelamentoItemService
        {
            get
            {
                return _empenhoCancelamentoItemService ?? (_empenhoCancelamentoItemService = new EmpenhoCancelamentoItemService(
                    new LogErrorDal(), new EmpenhoCancelamentoItemDal()));
            }
        }
        #endregion


        #region Liquidação de Despesas

        #region Apropriação / Subempenho

        private static SubempenhoService _subempenhoService;
        public static SubempenhoService SubempenhoService
        {
            get
            {
                return _subempenhoService ?? (_subempenhoService = new SubempenhoService(
                    new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(),
                    new SubempenhoDal(), new SubempenhoNotaDal(), new SubempenhoItemDal(), new SubempenhoEventoDal(),
                    new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new ProdespLiquidacaoDespesaWs(), new SiafemLiquidacaoDespesaWs()));
            }
        }

        private static SubempenhoNotaService _subempenhoNotaService;
        public static SubempenhoNotaService SubempenhoNotaService
        {
            get
            {
                return _subempenhoNotaService ?? (_subempenhoNotaService = new SubempenhoNotaService(
                    new LogErrorDal(), new SubempenhoNotaDal()));
            }
        }

        private static SubempenhoEventoService _subempenhoEventoService;
        public static SubempenhoEventoService SubempenhoEventoService
        {
            get
            {
                return _subempenhoEventoService ?? (_subempenhoEventoService = new SubempenhoEventoService(
                    new LogErrorDal(), new SubempenhoEventoDal()));
            }
        }

        private static SubempenhoItemService _subempenhoItemService;
        public static SubempenhoItemService SubempenhoItemService
        {
            get
            {
                return _subempenhoItemService ?? (_subempenhoItemService = new SubempenhoItemService(
                    new LogErrorDal(), new SubempenhoItemDal()));
            }
        }

        #endregion

        #region Anulação de Apropriação / Subempenho

        private static SubempenhoCancelamentoService _subempenhoCancelamentoService;
        public static SubempenhoCancelamentoService SubempenhoCancelamentoService
        {
            get
            {
                return _subempenhoCancelamentoService ?? (_subempenhoCancelamentoService = new SubempenhoCancelamentoService(
                    new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(),
                    new SubempenhoCancelamentoDal(), new SubempenhoCancelamentoNotaDal(), new SubempenhoCancelamentoItemDal(), new SubempenhoCancelamentoEventoDal(),
                    new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new ProdespLiquidacaoDespesaWs(), new SiafemLiquidacaoDespesaWs()));
            }
        }

        private static SubempenhoCancelamentoNotaService _subempenhoCancelamentoNotaService;
        public static SubempenhoCancelamentoNotaService SubempenhoCancelamentoNotaService
        {
            get
            {
                return _subempenhoCancelamentoNotaService ?? (_subempenhoCancelamentoNotaService = new SubempenhoCancelamentoNotaService(
                    new LogErrorDal(), new SubempenhoCancelamentoNotaDal()));
            }
        }

        private static SubempenhoCancelamentoEventoService _subempenhoCancelamentoEventoService;
        public static SubempenhoCancelamentoEventoService SubempenhoCancelamentoEventoService
        {
            get
            {
                return _subempenhoCancelamentoEventoService ?? (_subempenhoCancelamentoEventoService = new SubempenhoCancelamentoEventoService(
                    new LogErrorDal(), new SubempenhoCancelamentoEventoDal()));
            }
        }

        private static SubempenhoCancelamentoItemService _subempenhoCancelamentoItemService;
        public static SubempenhoCancelamentoItemService SubempenhoCancelamentoItemService
        {
            get
            {
                return _subempenhoCancelamentoItemService ?? (_subempenhoCancelamentoItemService = new SubempenhoCancelamentoItemService(
                    new LogErrorDal(), new SubempenhoCancelamentoItemDal()));
            }
        }

        #endregion

        #region Restos a Pagar

        #region Inscrição
        private static RapInscricaoService _rapInscricao;
        public static RapInscricaoService RapInscricaoService
        {
            get
            {
                return _rapInscricao ?? (_rapInscricao = new RapInscricaoService(
                    new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(),
                    new RapInscricaoDal(), new RapInscricaoNotaDal(),
                    new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new ProdespLiquidacaoDespesaWs(), new SiafemLiquidacaoDespesaWs()));
            }
        }

        private static RapInscricaoNotaService _rapInscricaoNotaService;
        public static RapInscricaoNotaService RapInscricaoNotaService
        {
            get
            {
                return _rapInscricaoNotaService ?? (_rapInscricaoNotaService = new RapInscricaoNotaService(
                    new LogErrorDal(), new RapInscricaoNotaDal()));
            }
        }
        #endregion

        #region Requisição
        private static RapRequisicaoService _rapRequisicaoService;
        public static RapRequisicaoService RapRequisicaoService
        {
            get
            {
                return _rapRequisicaoService ?? (_rapRequisicaoService = new RapRequisicaoService(
                    new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(),
                    new RapRequisicaoDal(), new RapRequisicaoNotaDal(),
                    new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new ProdespLiquidacaoDespesaWs(), new SiafemLiquidacaoDespesaWs()));
            }
        }

        private static RapRequisicaoNotaService _rapRequisicaoNotaService;
        public static RapRequisicaoNotaService RapRequisicaoNotaService
        {
            get
            {
                return _rapRequisicaoNotaService ?? (_rapRequisicaoNotaService = new RapRequisicaoNotaService(
                    new LogErrorDal(), new RapRequisicaoNotaDal()));
            }
        }
        #endregion

        #region Anulação
        private static RapAnulacaoService _rapAnulacaoService;
        public static RapAnulacaoService RapAnulacaoService
        {
            get
            {
                return _rapAnulacaoService ?? (_rapAnulacaoService = new RapAnulacaoService(
                    new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(),
                    new RapAnulacaoDal(), new RapAnulacaoNotaDal(),
                    new ProgramaDal(), new FonteDal(), new EstruturaDal(),
                    new ProdespLiquidacaoDespesaWs(), new SiafemLiquidacaoDespesaWs()));

            }
        }

        private static RapAnulacaoNotaService _rapAnulacaoNotaService;
        public static RapAnulacaoNotaService RapAnulacaoNotaService
        {
            get
            {
                return _rapAnulacaoNotaService ?? (_rapAnulacaoNotaService = new RapAnulacaoNotaService(
                    new LogErrorDal(), new RapAnulacaoNotaDal()));
            }
        }
        #endregion

        #endregion

        #endregion


        #region PagamentoContaUnica

        private static DesdobramentoService _desdobramentoService;
        public static DesdobramentoService DesdobramentoService
        {
            get
            {
                return _desdobramentoService ?? (_desdobramentoService = new DesdobramentoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new DesdobramentoDal(), new ProdespPagamentoContaUnicaWs()));
            }
        }


        private static IdentificacaoDesdobramentoService _identificacaoDesdobramentoServiceService;
        public static IdentificacaoDesdobramentoService IdentificacaoDesdobramentoService
        {
            get
            {
                return _identificacaoDesdobramentoServiceService ?? (_identificacaoDesdobramentoServiceService = new IdentificacaoDesdobramentoService(new IdentificacaoDesdobramentoDal()));
            }
        }


        private static CredorService _credorService;
        public static CredorService CredorService
        {
            get
            {
                return _credorService ?? (_credorService = new CredorService(new LogErrorDal(), new CredorDal()));
            }
        }


        private static DesdobramentoTipoService _desdobramentoTipoService;
        public static DesdobramentoTipoService DesdobramentoTipoService
        {
            get
            {
                return _desdobramentoTipoService ?? (_desdobramentoTipoService = new DesdobramentoTipoService(new DesdobramentoTipoDal()));
            }
        }


        private static DocumentoTipoService _documentoTipoService;
        public static DocumentoTipoService DocumentoTipoService
        {
            get
            {
                return _documentoTipoService ?? (_documentoTipoService = new DocumentoTipoService(new DocumentoTipoDal()));
            }
        }


        private static ReterService _reterService;
        public static ReterService ReterService
        {
            get
            {
                return _reterService ?? (_reterService = new ReterService(new ReterDal()));
            }
        }


        private static ReclassificacaoRetencaoService _reclassificacaoRetencaoService;
        public static ReclassificacaoRetencaoService ReclassificacaoRetencaoService
        {
            get
            {
                return _reclassificacaoRetencaoService ?? (_reclassificacaoRetencaoService = new ReclassificacaoRetencaoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new ReclassificacaoRetencaoDal(), new ReclassificacaoRetencaoNotaDal(), new ReclassificacaoRetencaoEventoDal(), new SiafemPagamentoContaUnicaWs(), NlParametrizacaoService));
            }
        }


        private static ReclassificacaoRetencaoTipoService _reclassificacaoRetencaoTipoService;
        public static ReclassificacaoRetencaoTipoService ReclassificacaoRetencaoTipoService
        {
            get
            {
                return _reclassificacaoRetencaoTipoService ?? (_reclassificacaoRetencaoTipoService = new ReclassificacaoRetencaoTipoService(new ReclassificacaoRetencaoTipoDal()));
            }
        }
        

        private static ParaRestoAPagarService _paraRestoAPagarService;
        public static ParaRestoAPagarService ParaRestoAPagarService
        {
            get
            {
                return _paraRestoAPagarService ?? (_paraRestoAPagarService = new ParaRestoAPagarService(new ParaRestoPagarDal()));
            }
        }


        private static ReclassificacaoRetencaoEventoService _reclassificacaoRetencaoEventoService;
        public static ReclassificacaoRetencaoEventoService ReclassificacaoRetencaoEventoService
        {
            get
            {
                return _reclassificacaoRetencaoEventoService ?? (_reclassificacaoRetencaoEventoService = new ReclassificacaoRetencaoEventoService(new LogErrorDal(), new ReclassificacaoRetencaoEventoDal()));
            }
        }



        private static ReclassificacaoRetencaoNotaService _reclassificacaoRetencaoNotaService;
        public static ReclassificacaoRetencaoNotaService ReclassificacaoRetencaoNotaService
        {
            get
            {
                return _reclassificacaoRetencaoNotaService ?? (_reclassificacaoRetencaoNotaService = new ReclassificacaoRetencaoNotaService(new LogErrorDal(), new ReclassificacaoRetencaoNotaDal()));
            }
        }




        private static ListaBoletosService _listaBoletosService;
        public static ListaBoletosService ListaBoletosService
        {
            get
            {
                return _listaBoletosService ?? (_listaBoletosService = new ListaBoletosService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new SiafemPagamentoContaUnicaWs(), new ProdespPagamentoContaUnicaWs(), new ListaBoletosDal()));
            }
        }


        private static TipoBoletoService _tipoBoletoService;
        public static TipoBoletoService TipoBoletoService
        {
            get
            {
                return _tipoBoletoService ?? (_tipoBoletoService = new TipoBoletoService(new TipoBoletoDal()));
            }
        }


        private static ListaCodigoBarrasService _listaCodigoBarrasService;
        public static ListaCodigoBarrasService ListaCodigoBarrasService
        {
            get
            {
                return _listaCodigoBarrasService ?? (_listaCodigoBarrasService = new ListaCodigoBarrasService(new ListaCodigoBarrasDal()));
            }
        }


        private static CodigoBarraTaxaService _codigoBarraTaxaService;
        public static CodigoBarraTaxaService CodigoBarraTaxaService
        {
            get
            {
                return _codigoBarraTaxaService ?? (_codigoBarraTaxaService = new CodigoBarraTaxaService(new CodigoBarraTaxaDal()));
            }
        }


        private static CodigoBarraBoletoService _codigoBarraBoletoService;
        public static CodigoBarraBoletoService CodigoBarraBoletoService
        {
            get
            {
                return _codigoBarraBoletoService ?? (_codigoBarraBoletoService = new CodigoBarraBoletoService(new CodigoBarraBoletoDal()));
            }
        }



        private static PreparacaoPagamentoService _preparacaoPagamentoService;
        public static PreparacaoPagamentoService PreparacaoPagamentoService
        {
            get
            {
                return _preparacaoPagamentoService ?? (_preparacaoPagamentoService = new PreparacaoPagamentoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new ProdespPagamentoContaUnicaWs(), new ProdespPagamentoContaDerWs(), new PreparacaoPagamentoDal()));
            }
        }


        private static PreparacaoPagamentoTipoService _preparacaoPagamentoTipoService;
        public static PreparacaoPagamentoTipoService PreparacaoPagamentoTipoService
        {
            get
            {
                return _preparacaoPagamentoTipoService ?? (_preparacaoPagamentoTipoService = new PreparacaoPagamentoTipoService(new PreparacaoPagamentoTipoDal()));
            }
        }


        private static ProgramacaoDesembolsoService _programacaoDesembolsoService;
        public static ProgramacaoDesembolsoService ProgramacaoDesembolsoService
        {
            get
            {
                return _programacaoDesembolsoService ?? (_programacaoDesembolsoService = new ProgramacaoDesembolsoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new ProgramacaoDesembolsoDal(), new ProgramacaoDesembolsoAgrupamentoDal(), new ProgramacaoDesembolsoEventoDal(), new SiafemPagamentoContaUnicaWs(), new ProdespPagamentoContaUnicaWs()));
            }
        }


        private static ProgramacaoDesembolsoTipoService _programacaoDesembolsoTipoService;
        public static ProgramacaoDesembolsoTipoService ProgramacaoDesembolsoTipoService
        {
            get
            {
                return _programacaoDesembolsoTipoService ?? (_programacaoDesembolsoTipoService = new ProgramacaoDesembolsoTipoService(new ProgramacaoDesembolsoTipoDal()));
            }
        }


        private static ProgramacaoDesembolsoEventoService _programacaoDesembolsoEventoService;
        public static ProgramacaoDesembolsoEventoService ProgramacaoDesembolsoEventoService
        {
            get
            {
                return _programacaoDesembolsoEventoService ?? (_programacaoDesembolsoEventoService = new ProgramacaoDesembolsoEventoService(new LogErrorDal(), new ProgramacaoDesembolsoEventoDal()));
            }
        }

        
        private static ProgramacaoDesembolsoAgrupamentoService _programacaoDesembolsoAgrupamentoService;
        public static ProgramacaoDesembolsoAgrupamentoService ProgramacaoDesembolsoAgrupamentoService
        {
            get
            {
                return _programacaoDesembolsoAgrupamentoService ?? (_programacaoDesembolsoAgrupamentoService = new ProgramacaoDesembolsoAgrupamentoService(new LogErrorDal(), new ProgramacaoDesembolsoAgrupamentoDal()));
            }
        }

        private static ProgramacaoDesembolsoExecucaoService _programacaoDesembolsoExecucaoService;
        public static ProgramacaoDesembolsoExecucaoService ProgramacaoDesembolsoExecucaoService
        {
            get
            {
                return _programacaoDesembolsoExecucaoService ?? (_programacaoDesembolsoExecucaoService = 
                    new ProgramacaoDesembolsoExecucaoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new ProgramacaoDesembolsoExecucaoDal(),new ProgramacaoDesembolsoExecucaoItemDal(), new SiafemPagamentoContaUnicaService(new LogErrorDal(), new SiafemPagamentoContaUnicaWs()), new ProdespPagamentoContaDerService(new LogErrorDal(), new ProdespPagamentoContaDerWs())));
            }
        }

        private static AutorizacaoDeOBService _autorizacaoDeOBService;

        public static AutorizacaoDeOBService AutorizacaoDeOBService
        {
            get
            {
                return _autorizacaoDeOBService ?? (_autorizacaoDeOBService =
                    new AutorizacaoDeOBService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new AutorizacaoDeOBDal(), new AutorizacaoDeOBItemDal(), new SiafemPagamentoContaUnicaService(new LogErrorDal(), new SiafemPagamentoContaUnicaWs()), new ProgramacaoDesembolsoExecucaoDal(), new ProgramacaoDesembolsoExecucaoItemDal(), new ProdespPagamentoContaDerService(new LogErrorDal(), new ProdespPagamentoContaDerWs())));
            }
        }



        private static NotaDeLancamentoService _notaDeLancamentoService;
        public static NotaDeLancamentoService NotaDeLancamentoService
        {
            get
            {
                return _notaDeLancamentoService ?? (_notaDeLancamentoService = new NotaDeLancamentoService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), ReclassificacaoRetencaoService, NlParametrizacaoService, ParaRestoAPagarService));
            }
        }

        private static ImpressaoRelacaoRERTService _impressaoRelacaoRERTService;
        public static ImpressaoRelacaoRERTService ImpressaoRelacaoRERTService
        {
            get
            {
                return _impressaoRelacaoRERTService ?? (_impressaoRelacaoRERTService = new ImpressaoRelacaoRERTService(new LogErrorDal(), new CommonWs(), new ChaveCicsmoDal(), new SiafemPagamentoContaUnicaWs(), new ImpressaoRelacaoRERTDal(), new ListaReDal(), new ListaRtDal()));
            }
        }

        #endregion


        #region Pagamento Conta DER
        private static NlParametrizacaoService _nlParametrizacaoService;
        public static NlParametrizacaoService NlParametrizacaoService
        {
            get
            {
                return _nlParametrizacaoService ?? (_nlParametrizacaoService =
                    new NlParametrizacaoService(new LogErrorDal(), new NlParametrizacaoDal(), new NlParametrizacaoDespesaDal(), new NlParametrizacaoDespesaTipoDal(), new NlParametrizacaoEventoDal(), new NlParametrizacaoNlTipoDal(), new ParaRestoPagarDal(), new NlParametrizacaoFormaGerarNlDal(), new DocumentoTipoDal()));
            }
        }

        private static ConfirmacaoPagamentoService _confirmacaoPagamentoService;
        public static ConfirmacaoPagamentoService ConfirmacaoPagamentoService
        {
            get
            {
                return _confirmacaoPagamentoService ?? (_confirmacaoPagamentoService =
                    new ConfirmacaoPagamentoService(new LogErrorDal(), new ChaveCicsmoDal(), new ConfirmacaoPagamentoDal(), new ConfirmacaoPagamentoItemDal(), new ProdespPagamentoContaUnicaWs(), new ProdespPagamentoContaDerWs(), new ProgramacaoDesembolsoExecucaoItemDal(), new AutorizacaoDeOBItemDal()));
            }
        }


        private static ArquivoRemessaService _arquivoRemessaService;
        public static ArquivoRemessaService ArquivoRemessaService
        {
            get
            {
                return _arquivoRemessaService ?? (_arquivoRemessaService = new ArquivoRemessaService(new LogErrorDal(), new ChaveCicsmoDal(), new ArquivoRemessaDal(), new ProdespPagamentoContaDerWs()));
            }
        }


        #endregion


        #region Movimentacao

        private static MovimentacaoService _movimentacaoService;
        public static MovimentacaoService MovimentacaoService
        {
            get
            {
                return _movimentacaoService ?? (_movimentacaoService = new MovimentacaoService(new LogErrorDal(), new ChaveCicsmoDal(), new MovimentacaoDal(),new MovimentacaoTipoDal(),new MovimentacaoDocumentoTipoDal(), new MovimentacaoMesDal(),new ProdespMovimentacaoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new SiafemMovimentacaoWs())); 
            }
        }

        private static MovimentacaoMesService _movimentacaoMesService;
        public static MovimentacaoMesService MovimentacaoMesService
        {
            get
            {
                return _movimentacaoMesService ?? (_movimentacaoMesService = new MovimentacaoMesService(
                    new LogErrorDal(), new MovimentacaoMesDal()));
            }
        }

        private static ParametrizacaoFormaGerarNlService _parametrizacaoFormaGerarNlService;
        public static ParametrizacaoFormaGerarNlService ParametrizacaoFormaGerarNlService
        {
            get
            {
                return _parametrizacaoFormaGerarNlService ?? (_parametrizacaoFormaGerarNlService = new ParametrizacaoFormaGerarNlService(
                    new LogErrorDal(), new ConfirmacaoPagamentoItemDal(), new  NlParametrizacaoFormaGerarNlDal(),
                    NlParametrizacaoService, ParaRestoAPagarService, ReclassificacaoRetencaoService,new ConfirmacaoPagamentoDal(), CommonService, new NlParametrizacaoEventoDal(), ReclassificacaoRetencaoNotaService));
            }
        }



        #endregion





    }
}
