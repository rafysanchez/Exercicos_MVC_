using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.ValueObject;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.UI.Areas.Movimentacao.Models;
using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Controllers
{
    public class MovimentacaoBaseController : BaseController
    {
        protected FiltroGridViewModel _filterItems;
        protected readonly Usuario _userLoggedIn;
        protected int _modelId;
        protected static readonly IEnumerable<Regional> RegionalList = App.RegionalService.Buscar(new Regional()) ?? new List<Regional>();
        protected static readonly IEnumerable<MovimentacaoTipo> MovimentacaoList = App.MovimentacaoService.Buscar(new MovimentacaoTipo()) ?? new List<MovimentacaoTipo>();
        protected static readonly IEnumerable<MovimentacaoDocumentoTipo> DocumentoList = App.MovimentacaoService.Buscar(new MovimentacaoDocumentoTipo()) ?? new List<MovimentacaoDocumentoTipo>();
        protected readonly IEnumerable<Estrutura> _estruturaList = App.EstruturaService.Buscar(new Estrutura()) ?? new List<Estrutura>();
        protected readonly IEnumerable<Programa> _programaList = App.ProgramaService.Buscar(new Programa()) ?? new List<Programa>();
        protected static readonly IEnumerable<NaturezaTipo> _naturezaTipoList = App.NaturezaTipoService.Buscar(new NaturezaTipo()) ?? new List<NaturezaTipo>();
        protected IEnumerable<Regional> _regionalServiceList;
        

        public MovimentacaoBaseController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _filterItems = new FiltroGridViewModel();
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
        

        protected IEnumerable<FiltroGridViewModel> Display(MovimentacaoOrcamentaria entity)
        {
            var filterModel = InitializeEntityModel(entity);

            var movimentacao = (MovimentacaoOrcamentaria)Convert.ChangeType(filterModel, typeof(MovimentacaoOrcamentaria));
            ViewBag.Filtro = InitializeFiltroViewModel(movimentacao);

            return new List<FiltroGridViewModel>();
        }


        protected MovimentacaoOrcamentaria InitializeEntityModel(MovimentacaoOrcamentaria objModel)
        {

            var model = new MovimentacaoOrcamentaria();
            model.Id = default(int);
            model.DataCadastro = default(DateTime);
            model.StatusProdesp = default(string);

            return model;
        }


        protected IEnumerable<FiltroGridViewModel> Display(MovimentacaoOrcamentaria entity, FormCollection form)
        {

            IEnumerable<MovimentacaoOrcamentaria> entities = new List<MovimentacaoOrcamentaria>();

            var model = GenerateFilterViewModel(form, entity);
            
            entities = App.MovimentacaoService.BuscarGrid(model, Convert.ToDateTime(((MovimentacaoFiltroViewModel)ViewBag.Filtro).DataCadastroDe), Convert.ToDateTime(((MovimentacaoFiltroViewModel)ViewBag.Filtro).DataCadastroAte));

            var result = InitializeFiltroGridViewModel(entities).OrderBy(x => x.DataCadastro);

            return result;
        }


        protected MovimentacaoOrcamentaria GenerateFilterViewModel(FormCollection form, MovimentacaoOrcamentaria entity)
        {
            MovimentacaoFiltroViewModel filter;

            // Fixar o documento selecionado no combox
            if (!string.IsNullOrEmpty(form["DocumentoId"]))
            {
                entity.IdTipoDocumento = Convert.ToInt32(form["DocumentoId"]);
            }

            filter = InitializeFiltroViewModel(entity);

            entity = MapViewModelToEntityModel(form, entity, ref filter);

            ViewBag.Filtro = filter;

            return entity;
        }


        private MovimentacaoOrcamentaria MapViewModelToEntityModel(FormCollection form, MovimentacaoOrcamentaria entity, ref MovimentacaoFiltroViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(form["UnidadeGestora"]))
            {
                entity.UnidadeGestoraEmitente = form["UnidadeGestora"];
                viewModel.UnidadeGestora = form["UnidadeGestora"];
            }
            if (!string.IsNullOrEmpty(form["idGestao"]))
            {
                entity.GestaoEmitente = form["idGestao"];
                viewModel.idGestao = form["idGestao"];
            }
            if (!string.IsNullOrEmpty(form["numAgrupamento"]))
            {
                entity.NrAgrupamento = Convert.ToInt32(form["numAgrupamento"]);
                viewModel.numAgrupamento = Convert.ToInt32(form["numAgrupamento"]);
            }
            if (!string.IsNullOrEmpty(form["numDocumento"]))
            {
                entity.NumSiafem = form["numDocumento"];
                viewModel.NumSiafem = form["numDocumento"];
            }



            if (!string.IsNullOrEmpty(form["MovimentacaoId"]))
            {
                entity.IdTipoMovimentacao = Convert.ToInt32(form["MovimentacaoId"]);
                viewModel.tipoMovimentacao = Convert.ToInt32(form["MovimentacaoId"]);
            }

            if (!string.IsNullOrEmpty(form["DocumentoId"]))
            {
                entity.IdTipoDocumento = Convert.ToInt32(form["DocumentoId"]);
                viewModel.tipoDocumento = Convert.ToInt32(form["DocumentoId"]);
            }


            if (!string.IsNullOrEmpty(form["ugFavorecido"]))
            {
                entity.UnidadeGestoraFavorecida = form["ugFavorecido"];
                viewModel.ugFavorecido = form["ugFavorecido"];
            }
            if (!string.IsNullOrEmpty(form["idGestaoFavorecida"]))
            {
                entity.IdGestaoFavorecida = form["idGestaoFavorecida"];
                viewModel.idGestaoFavorecida = form["idGestaoFavorecida"];
            }
            if (!string.IsNullOrEmpty(form["idCFP"]))
            {
                entity.IdCFP = Convert.ToInt64(form["idCFP"]);
                viewModel.idCFP = Convert.ToInt64(form["idCFP"]);
            }
            if (!string.IsNullOrEmpty(form["idCED"]))
            {
                entity.IdCED = Convert.ToInt32(form["idCED"]);
                viewModel.idCED = Convert.ToInt32(form["idCED"]);
            }

            if (!string.IsNullOrEmpty(form["StatusProdesp"]))
            {
                entity.StatusProdesp = form["StatusProdesp"];
                viewModel.StatusProdesp = form["StatusProdesp"];
            }

            if (!string.IsNullOrEmpty(form["StatusSiafem"]))
            {
                entity.StatusSiafem = form["StatusSiafem"];
                viewModel.StatusSiafem = form["StatusSiafem"];
            }

            if (!string.IsNullOrEmpty(form["DataCadastroDe"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastroDe"]);
                viewModel.DataCadastroDe = Convert.ToDateTime(form["DataCadastroDe"]);
            }

            if (!string.IsNullOrEmpty(form["DataCadastroAte"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastroAte"]);
                viewModel.DataCadastroAte = Convert.ToDateTime(form["DataCadastroAte"]);
            }
            return entity;
        }


        private IEnumerable<FiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<MovimentacaoOrcamentaria> entities)
        {
            List<FiltroGridViewModel> items = new List<FiltroGridViewModel>();

            foreach (var entity in entities)
            {
                items.Add(new FiltroGridViewModel().CreateInstance(entity));
            }

            return items;
        }

        private MovimentacaoFiltroViewModel InitializeFiltroViewModel(MovimentacaoOrcamentaria entity)
        {

            return new MovimentacaoFiltroViewModel().CreateInstance(entity, MovimentacaoList, DocumentoList, new DateTime(), new DateTime());
        }

        protected void InitializeCommonBags<T>(T entity)
        {
            ViewBag.Usuario = _userLoggedIn;
        }
        protected void DisplayBase(MovimentacaoOrcamentaria movimentacao, bool isNewRecord)
        {
            DefinirAssinaturaPaiMovimentacao(movimentacao);

            ViewBag.DadoAssinatura = InitializeDadoAssinaturaViewModel(movimentacao, isNewRecord);

            ViewBag.Estrutura = _estruturaList;
            ViewBag.Programas = _programaList;

            ViewBag.Usuario = _userLoggedIn;
            ViewBag.Regionais = RegionalServiceList;
        }

        protected MovimentacaoOrcamentaria Display(MovimentacaoOrcamentaria movimentacao, bool isNewRecord)
        {
            this.DisplayBase(movimentacao, isNewRecord);

            if (isNewRecord)
            {
                movimentacao = InitializeEntityModel(movimentacao);
                movimentacao.TransmitirProdesp = movimentacao.TransmitirSiafem = true;
                movimentacao.AnoExercicio = DateTime.Now.Year;
            }

            var movVm = InitializeDadoMovimentacaoViewModel(movimentacao);
            movVm.UGO = "162101";
            movVm.Uo = "16055";
            movVm.DestinoRecurso = "21";
            movVm.FlProc = "0002";

            ViewBag.DadoMovimentacao = movVm;


            ViewBag.DadoCancelamentoReducaoGrid = InitializeDadoCancelamentoReducaoGridViewModel(movimentacao);
            ViewBag.DadoNotaCreditoGrid = InitializeDadoNotaCreditoGridViewModel(movimentacao);
            ViewBag.DadoDistribuicaoSuplementacaoGrid = InitializeDadoDistribuicaoSuplementacaoGridViewModel(movimentacao);

            var programa = _programaList.FirstOrDefault(x => x.Codigo == movimentacao.IdPrograma);
            if (programa != null)
            {
                movimentacao.CfpDesc = programa.Cfp.ToString().Formatar("00.000.0000.0000"); 
            }

            var estrutura = _estruturaList.FirstOrDefault(x => x.Codigo == movimentacao.IdEstrutura);
            if (estrutura != null)
            {
                movimentacao.CedDesc = estrutura.Natureza.Formatar("0.0.00.00") + " - " + estrutura.Fonte; 
            }

                return movimentacao;
        }
        protected MovimentacaoOrcamentaria DisplayEstorno(MovimentacaoOrcamentaria movimentacao)
        {
            movimentacao.IdTipoMovimentacao = (int)EnumTipoMovimentacao.Estorno;

            ZerarIds(movimentacao);

            ReordenarSequencias(movimentacao);

            InverterTab(movimentacao);

            InverterCanDis(movimentacao);

            this.DisplayBase(movimentacao, false);

            var movVm = InitializeDadoMovimentacaoViewModel(movimentacao);
            movVm.UGO = "162101";
            movVm.Uo = "16055";
            movVm.IdTipoMovimentacao = "2";

            ViewBag.DadoMovimentacao = movVm;

            ViewBag.DadoCancelamentoReducaoGrid = InitializeDadoCancelamentoReducaoGridViewModelEstorno(movimentacao);
            ViewBag.DadoNotaCreditoGrid = InitializeDadoNotaCreditoGridViewModelEstorno(movimentacao);
            ViewBag.DadoDistribuicaoSuplementacaoGrid = InitializeDadoDistribuicaoSuplementacaoGridViewModelEstorno(movimentacao);

            InverterRedSup(movimentacao);

            //// Dados origem
            var originalCancelamento = movimentacao.Cancelamento;
            var originalNc = movimentacao.NotasCreditos;
            var originalDistribuicao = movimentacao.Distribuicao;

            PreencherCancelamento(movimentacao, originalDistribuicao);
            InverterEmitenteFavorecidaNc(movimentacao);
            PreencherDistribuicao(movimentacao, originalCancelamento);

            LimparNumerosRecebidos(movimentacao);

            movimentacao.TransmitidoProdesp = movimentacao.TransmitidoSiafem = false;
            movimentacao.StatusProdesp = movimentacao.StatusSiafem = "N";
            
            return movimentacao;
        }

        public void PreencherCancelamento(MovimentacaoOrcamentaria movimentacao, List<MovimentacaoDistribuicao> originalDistribuicao)
        {
            var distribuicaoGrid = InitializeDadoCancelamentoReducaoGridViewModelEstorno(movimentacao);
            movimentacao.Cancelamento = new List<MovimentacaoCancelamento>();
            
            foreach (var od in originalDistribuicao)
            {
                var valor = distribuicaoGrid.Where(b => b.NrSequencia == od.NrSequencia).Select(a => a.Valor)?.FirstOrDefault();
                var unidadeGestoraFavorecida = distribuicaoGrid.Where(b => b.NrSequencia == od.NrSequencia).Select(a => a.UnidadeGestoraFavorecida)?.FirstOrDefault();

                var item = new MovimentacaoCancelamento
                {
                    Id = od.Id,
                    IdMovimentacao = od.IdMovimentacao,
                    IdFonte = Convert.ToInt32(od.IdFonte),
                    NrAgrupamento = od.NrAgrupamento,
                    NrSequencia = od.NrSequencia,
                    Valor = Convert.ToDecimal(valor),
                    UnidadeGestoraFavorecida = unidadeGestoraFavorecida,
                    GestaoFavorecida = od.GestaoFavorecida,
                    Evento = od.Evento,
                    CategoriaGasto = od.CategoriaGasto,
                    EventoNC = od.EventoNC,
                    Observacao = od.Observacao,
                    Observacao2 = od.Observacao2,
                    Observacao3 = od.Observacao3,
                    IdTipoDocumento = od.IdTipoDocumento
                };

                movimentacao.Cancelamento.Add(item);
            }
        }

        private void InverterEmitenteFavorecidaNc(MovimentacaoOrcamentaria movimentacao)
        {
            foreach (var nc in movimentacao.NotasCreditos)
            {
                var emitente = nc.UnidadeGestoraEmitente;

                nc.UnidadeGestoraEmitente = nc.UnidadeGestoraFavorecida;
                nc.UnidadeGestoraFavorecida = emitente;
            }
        }

        public void PreencherDistribuicao(MovimentacaoOrcamentaria movimentacao, List<MovimentacaoCancelamento> originalCancelamento)
        {
            movimentacao.Distribuicao = new List<MovimentacaoDistribuicao>();

            foreach (var oc in originalCancelamento)
            {
                var item = new MovimentacaoDistribuicao
                {
                    Id = oc.Id,
                    IdMovimentacao = oc.IdMovimentacao,
                    IdFonte = Convert.ToString(oc.IdFonte),
                    NrAgrupamento = oc.NrAgrupamento,
                    NrSequencia = oc.NrSequencia,
                    Valor = Convert.ToDecimal(oc.Valor),
                    UnidadeGestoraFavorecida = oc.UnidadeGestoraFavorecida,
                    GestaoFavorecida = oc.GestaoFavorecida,
                    Evento = oc.Evento,
                    CategoriaGasto = oc.CategoriaGasto,
                    EventoNC = oc.EventoNC,
                    Observacao = oc.Observacao,
                    Observacao2 = oc.Observacao2,
                    Observacao3 = oc.Observacao3,
                    IdTipoDocumento = oc.IdTipoDocumento
                };

                movimentacao.Distribuicao.Add(item);
            }
        }

        private static void ZerarIds(MovimentacaoOrcamentaria movimentacao)
        {
            movimentacao.Id = 0;
            foreach (var item in movimentacao.Cancelamento)
            {
                item.Id = 0;
                item.IdMovimentacao = 0;
                item.NrAgrupamento = 0;
                item.NumeroSiafem = null;
            }

            foreach (var item in movimentacao.Distribuicao)
            {
                item.Id = 0;
                item.IdMovimentacao = 0;
                item.NrAgrupamento = 0;
                item.NumeroSiafem = null;
            }

            foreach (var item in movimentacao.NotasCreditos)
            {
                item.Id = 0;
                item.IdMovimentacao = 0;
                item.NrAgrupamento = 0;
                item.NumeroSiafem = null;
            }

            foreach (var item in movimentacao.Meses)
            {
                item.Id = 0;
                item.IdCancelamento = 0;
                item.IdDistribuicao = 0;
                item.IdMovimentacao = 0;
                item.IdReducaoSuplementacao = 0;
                item.NrAgrupamento = 0;
            }

            foreach (var item in movimentacao.ReducaoSuplementacao)
            {
                item.Id = 0;
                item.IdCancelamento = 0;
                item.IdDistribuicao = 0;
                item.IdMovimentacao = 0;
                item.IdNotaCredito = 0;
                item.NrAgrupamento = 0;
            }
        }

        private static void InverterTab(MovimentacaoOrcamentaria movimentacao)
        {
            #region Cancelamento / Distribuição Tabela Meses
            foreach (var item in movimentacao.Meses)
            {
                if (item.tab.Equals("C"))
                {
                    item.tab = "X";
                }

                if (item.tab.Equals("D"))
                {
                    item.tab = "C";
                }
            }

            foreach (var item in movimentacao.Meses)
            {
                if (item.tab.Equals("X"))
                {
                    item.tab = "D";
                }
            }
            #endregion

            #region Redução / Suplementação Tabela Meses
            foreach (var item in movimentacao.Meses)
            {
                if (item.tab.Equals("R"))
                {
                    item.tab = "X";
                }

                if (item.tab.Equals("S"))
                {
                    item.tab = "R";
                }
            }

            foreach (var item in movimentacao.Meses)
            {
                if (item.tab.Equals("X"))
                {
                    item.tab = "S";
                }
            }
            #endregion
        }

        private static void InverterRedSup(MovimentacaoOrcamentaria movimentacao)
        {
            #region Redução / Suplementação Tabela Redução / Suplementação
            foreach (var item in movimentacao.ReducaoSuplementacao)
            {
                if (item.RedSup.Equals("R"))
                {
                    item.RedSup = "X";
                }

                if (item.RedSup.Equals("S"))
                {
                    item.RedSup = "R";
                }
            }

            foreach (var item in movimentacao.ReducaoSuplementacao)
            {
                if (item.RedSup.Equals("X"))
                {
                    item.RedSup = "S";
                }
            }
            #endregion
        }

        private static void InverterCanDis(MovimentacaoOrcamentaria movimentacao)
        {
            #region Cancelamento / Distribuição Tabela Nota de Crédito
            foreach (var item in movimentacao.NotasCreditos)
            {
                if (item.CanDis.Equals("C"))
                {
                    item.CanDis = "X";
                }

                if (item.CanDis.Equals("D"))
                {
                    item.CanDis = "C";
                }
            }

            foreach (var item in movimentacao.NotasCreditos)
            {
                if (item.CanDis.Equals("X"))
                {
                    item.CanDis = "D";
                }
            }
            #endregion
        }

        private static void ReordenarSequencias(MovimentacaoOrcamentaria movimentacao)
        {
            int newSeq = 0;
            foreach (var item in movimentacao.ReducaoSuplementacao.Where(x => x.RedSup.Equals("R")))
            {
                item.NrSequencia = ++newSeq;
            }
            newSeq = 0;
            foreach (var item in movimentacao.ReducaoSuplementacao.Where(x => x.RedSup.Equals("S")))
            {
                item.NrSequencia = ++newSeq;
            }
        }


        private void LimparNumerosRecebidos(MovimentacaoOrcamentaria movimentacao)
        {
            foreach (var x in movimentacao.Cancelamento)
            {
                x.NumeroSiafem = null;
            }

            foreach (var x in movimentacao.NotasCreditos)
            {
                x.NumeroSiafem = null;
            }

            foreach (var x in movimentacao.Distribuicao)
            {
                x.NumeroSiafem = null;
            }

            foreach (var x in movimentacao.ReducaoSuplementacao)
            {
                x.NumeroSiafem = null;
                x.NrSuplementacaoReducao = null;
            }
        }

        protected DadoMovimentacaoViewModel InitializeDadoMovimentacaoViewModel(MovimentacaoOrcamentaria entity)
        {
            return new DadoMovimentacaoViewModel().CreateInstance(entity, _estruturaList, _programaList, _naturezaTipoList, MovimentacaoList, DocumentoList);
        }


        protected IEnumerable<DadoCancelamentoReducaoViewModel> InitializeDadoCancelamentoReducaoGridViewModel(MovimentacaoOrcamentaria entity)
        {
            var lista = new List<DadoCancelamentoReducaoViewModel>();

            if (entity.Cancelamento.Any())
            {
                foreach (var c in entity.Cancelamento)
                {
                    var vm = new DadoCancelamentoReducaoViewModel().CreateInstance(c, entity.UnidadeGestoraEmitente, entity.GestaoEmitente);

                    if (entity.ReducaoSuplementacao.Any())
                    {
                        var r = entity.ReducaoSuplementacao.FirstOrDefault(x => x.NrSequencia == c.NrSequencia && x.RedSup.Equals("R"));

                        if (r != null)
                        {
                            vm.UnidadeGestoraFavorecida = c.UnidadeGestoraFavorecida;
                            vm.NrOrgao = r.NrOrgao;
                            vm.NrSuplementacaoReducao = r.NrSuplementacaoReducao;
                            vm.TransmitidoProdesp = r.StatusProdesp.Equals("S") ? "Sucesso" : (r.StatusProdesp.Equals("E") ? "Erro" : "Não Transmitido");
                            vm.MensagemProdesp = r.MensagemProdesp;
                        }
                    }

                    lista.Add(vm);
                }
            }
            else
            {
                if (entity.ReducaoSuplementacao.Any())
                {
                    foreach (var rs in entity.ReducaoSuplementacao.Where(x => x.RedSup.Equals("R")))
                    {
                        var vm = new DadoCancelamentoReducaoViewModel().CreateInstance(rs, entity.UnidadeGestoraEmitente, entity.GestaoEmitente);
                        vm.Fonte = entity.IdFonte.ToString().PadLeft(3, '0');

                        lista.Add(vm);
                    }
                }
            }

            return lista;
        }

        protected IEnumerable<DadoNotaCreditoViewModel> InitializeDadoNotaCreditoGridViewModel(MovimentacaoOrcamentaria entity)
        {
            var lista = entity.NotasCreditos.Select(model => new DadoNotaCreditoViewModel().CreateInstance(model)).ToList();

            return lista;
        }

        protected IEnumerable<DadoDistribuicaoSuplementacaoViewModel> InitializeDadoDistribuicaoSuplementacaoGridViewModel(MovimentacaoOrcamentaria entity)
        {
            var lista = new List<DadoDistribuicaoSuplementacaoViewModel>();

            if (entity.Distribuicao.Any())
            {
                foreach (var c in entity.Distribuicao)
                {
                    var vm = new DadoDistribuicaoSuplementacaoViewModel().CreateInstance(c, entity.UnidadeGestoraEmitente);

                    if (entity.ReducaoSuplementacao.Any())
                    {
                        var r = entity.ReducaoSuplementacao.FirstOrDefault(x => x.NrSequencia == c.NrSequencia && x.RedSup.Equals("S"));

                        if (r != null)
                        {
                            vm.NrOrgao = r.NrOrgao;
                            vm.NrSuplementacaoReducao = r.NrSuplementacaoReducao;
                            vm.TransmitidoProdesp = r.StatusProdesp.Equals("S") ? "Sucesso" : (r.StatusProdesp.Equals("E") ? "Erro" : "Não Transmitido");
                            vm.MensagemProdesp = r.MensagemProdesp;
                        }
                    }

                    lista.Add(vm);
                }
            }
            else
            {
                if (entity.ReducaoSuplementacao.Any())
                {
                    foreach (var rs in entity.ReducaoSuplementacao.Where(x => x.RedSup.Equals("S")))
                    {
                        var vm = new DadoDistribuicaoSuplementacaoViewModel().CreateInstance(rs);
                        vm.Fonte = entity.IdFonte.ToString().PadLeft(3, '0');

                        lista.Add(vm);
                    }
                }
            }

            return lista;
        }


        #region Estorno
        protected IEnumerable<DadoCancelamentoReducaoViewModel> InitializeDadoCancelamentoReducaoGridViewModelEstorno(MovimentacaoOrcamentaria entity)
        {
            var lista = new List<DadoCancelamentoReducaoViewModel>();

            if (entity.Distribuicao.Any())
            {
                foreach (var d in entity.Distribuicao)
                {
                    var vm = new DadoCancelamentoReducaoViewModel().CreateInstance(d, entity.UnidadeGestoraEmitente, entity.GestaoEmitente);

                    if (entity.ReducaoSuplementacao.Any())
                    {
                        var r = entity.ReducaoSuplementacao.FirstOrDefault(x => x.NrSequencia == d.NrSequencia && x.RedSup.Equals("S"));

                        if (r != null)
                        {
                            vm.UnidadeGestoraEmitente = entity.UnidadeGestoraEmitente;
                            vm.UnidadeGestoraFavorecida = d.UnidadeGestoraFavorecida;
                            vm.NrOrgao = r.NrOrgao;
                        }
                    }

                    lista.Add(vm);
                }
            }
            else
            {
                if (entity.ReducaoSuplementacao.Any())
                {
                    foreach (var rs in entity.ReducaoSuplementacao.Where(x => x.RedSup.Equals("S")))
                    {
                        var vm = new DadoCancelamentoReducaoViewModel().CreateInstance(rs, entity.UnidadeGestoraEmitente, entity.GestaoEmitente);

                        lista.Add(vm);
                    }
                }
            }

            return lista;
        }
        protected IEnumerable<DadoNotaCreditoViewModel> InitializeDadoNotaCreditoGridViewModelEstorno(MovimentacaoOrcamentaria entity)
        {
            var lista = entity.NotasCreditos.Select(model => new DadoNotaCreditoViewModel().CreateInstance(model)).ToList();
            var listaNova = new List<DadoNotaCreditoViewModel>();

            foreach (var l in lista)
            {
                var d = entity.Distribuicao.FirstOrDefault(x => x.NrSequencia == l.NrSequencia && x.NrAgrupamento == l.NrAgrupamento);

                if (d != null)
                {
                    l.UnidadeGestoraEmitente = d.UnidadeGestoraFavorecida;
                    l.IdGestaoEmitente = d.GestaoFavorecida;
                    l.UnidadeGestoraFavorecida = entity.UnidadeGestoraEmitente;
                    l.IdGestaoFavorecida = entity.GestaoEmitente;
                    l.Fonte = d.IdFonte;
                    l.CategoriaGasto = d.CategoriaGasto;
                    l.IdTipoDocumento = 1;
                }

                listaNova.Add(l);
            }

            return listaNova;
        }

        protected IEnumerable<DadoDistribuicaoSuplementacaoViewModel> InitializeDadoDistribuicaoSuplementacaoGridViewModelEstorno(MovimentacaoOrcamentaria entity)
        {
            var lista = new List<DadoDistribuicaoSuplementacaoViewModel>();

            if (entity.Cancelamento.Any())
            {
                foreach (var c in entity.Cancelamento)
                {
                    var vm = new DadoDistribuicaoSuplementacaoViewModel().CreateInstance(c, entity.UnidadeGestoraEmitente);

                    if (entity.ReducaoSuplementacao.Any())
                    {
                        var r = entity.ReducaoSuplementacao.FirstOrDefault(x => x.NrSequencia == c.NrSequencia && x.RedSup.Equals("R"));

                        if (r != null)
                        {
                            vm.NrOrgao = r.NrOrgao;
                            vm.Fonte = Convert.ToString(c.IdFonte);
                        }
                    }

                    lista.Add(vm);
                }
            }
            else
            {
                if (entity.ReducaoSuplementacao.Any())
                {
                    foreach (var rs in entity.ReducaoSuplementacao.Where(x => x.RedSup.Equals("R")))
                    {
                        var vm = new DadoDistribuicaoSuplementacaoViewModel().CreateInstance(rs);

                        lista.Add(vm);
                    }
                }
            }

            return lista;
        }
        #endregion

        protected DadoAssinaturaViewModel InitializeDadoAssinaturaViewModel(MovimentacaoOrcamentaria entity, bool isNewRecord)
        {
            return GetSignaturesFromDomainModel(entity, isNewRecord);
        }


        protected DadoAssinaturaViewModel GetSignaturesFromDomainModel(MovimentacaoOrcamentaria entity, bool isNewRecord)
        {
            if (isNewRecord)
            {
                GetSignaturesFromRepository(ref entity); 
            }

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

            return new DadoAssinaturaViewModel().CreateInstance(autorizado, examinado, responsavel);
        }


        protected void GetSignaturesFromRepository(ref MovimentacaoOrcamentaria objModel)
        {
            AssinaturasVo entity = App.MovimentacaoService.BuscarUltimaAssinatura();

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

        private static void DefinirAssinaturaPaiMovimentacao(MovimentacaoOrcamentaria movimentacao)
        {
            var reducaoQualquer = movimentacao.ReducaoSuplementacao.FirstOrDefault(x => x.RedSup.Equals("R"));
            if (reducaoQualquer != null)
            {
                movimentacao.CodigoAutorizadoAssinatura = reducaoQualquer.CodigoAutorizadoAssinatura;
                movimentacao.AutorizadoSupraFolha = reducaoQualquer.AutorizadoSupraFolha;
                movimentacao.CodigoAutorizadoGrupo = reducaoQualquer.CodigoAutorizadoGrupo;
                movimentacao.CodigoAutorizadoOrgao = reducaoQualquer.CodigoAutorizadoOrgao;
                movimentacao.DescricaoAutorizadoCargo = reducaoQualquer.DescricaoAutorizadoCargo;
                movimentacao.NomeAutorizadoAssinatura = reducaoQualquer.NomeAutorizadoAssinatura;

                movimentacao.CodigoExaminadoAssinatura = reducaoQualquer.CodigoExaminadoAssinatura;
                movimentacao.CodigoExaminadoGrupo = reducaoQualquer.CodigoExaminadoGrupo;
                movimentacao.CodigoExaminadoOrgao = reducaoQualquer.CodigoExaminadoOrgao;
                movimentacao.DescricaoExaminadoCargo = reducaoQualquer.DescricaoExaminadoCargo;
                movimentacao.NomeExaminadoAssinatura = reducaoQualquer.NomeExaminadoAssinatura;

                movimentacao.CodigoResponsavelAssinatura = reducaoQualquer.CodigoResponsavelAssinatura;
                movimentacao.CodigoResponsavelGrupo = reducaoQualquer.CodigoResponsavelGrupo;
                movimentacao.CodigoResponsavelOrgao = reducaoQualquer.CodigoResponsavelOrgao;
                movimentacao.DescricaoResponsavelCargo = reducaoQualquer.DescricaoResponsavelCargo;
                movimentacao.NomeResponsavelAssinatura = reducaoQualquer.NomeResponsavelAssinatura;
            }
        }
    }
}