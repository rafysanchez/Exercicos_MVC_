using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Log;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Models.Log;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.Seguranca.Controllers
{
    public class LogController : BaseController
    {
        private LogFilter _filtro;
        private List<LogAplicacao> _listaDados;
        private LogFilter _obj;
        private List<FiltroLogViewModel> _logViewModels;

        public LogController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _filtro = new LogFilter();
            _listaDados = new List<LogAplicacao>();
            _obj = new LogFilter();
            _logViewModels = new List<FiltroLogViewModel>();
        }

        [PermissaoAcesso(Controller = typeof(LogController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home");

            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            if (ViewBag.Filtro == null)
            {
                AtualizarFiltro();
                ViewBag.Filtro = _filtro;
            }

            return View(_logViewModels);

        }

        [PermissaoAcesso(Controller = typeof(LogController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                AtualizarFiltro();
                _listaDados.Clear();
                _obj = GetFiltro(f);

                FiltroLogViewModel filtroLogViewModel = new FiltroLogViewModel();

                _listaDados = App.BaseService.Search(_obj);

                _logViewModels = filtroLogViewModel.GerarLogViewModels(_filtro, _listaDados);

                if (_logViewModels.Count == 0)
                {
                    ExibirMensagemErro("Registros não encontrados.");
                }

                return View("Index", _logViewModels);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _logViewModels);
            }
        }


        [PermissaoAcesso(Controller = typeof(LogController), Operacao = "Visualizar")]
        public ActionResult Detalhe(int idlog, string tipo)
        {
            try
            {
                var log = App.BaseService.Search(new LogFilter { Codigo = idlog }).FirstOrDefault();
                //log
                LogViewModel logViewModel = new LogViewModel();
                AtualizarFiltro();
                var logAplication = logViewModel.GerarLogViewModels(_filtro, log);
                return View(logAplication);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _logViewModels);
            }
        }



        private LogFilter GetFiltro(FormCollection f)
        {
            LogFilter obj = new LogFilter();
            obj = _filtro;
            obj.IdRecurso = !string.IsNullOrWhiteSpace(f["ddlRecurso"]) ? Convert.ToInt32(f["ddlRecurso"]) : new int?();
            obj.IdUsuario = !string.IsNullOrWhiteSpace(f["ddlUsuario"]) ? Convert.ToInt32(f["ddlUsuario"]) : new int?();
            obj.IdAcao = !string.IsNullOrWhiteSpace(f["ddlAcao"]) ? Convert.ToInt32(f["ddlAcao"]) : new int?();
            obj.IdResultado = !string.IsNullOrWhiteSpace(f["ddlResultado"]) ? Convert.ToInt32(f["ddlResultado"]) : new int?();
            obj.Argumento = !string.IsNullOrWhiteSpace(f["ddlArgumento"]) ? f["ddlArgumento"] : string.Empty;
            obj.DataInicial = !string.IsNullOrWhiteSpace(f["DataInicial"]) ? Convert.ToDateTime(f["DataInicial"]) : new DateTime?();
            obj.DataFinal = !string.IsNullOrWhiteSpace(f["DataFinal"]) ? Convert.ToDateTime(f["DataFinal"]) : new DateTime?();

            ViewBag.Filtro = obj;
            return obj;
        }


        private void AtualizarFiltro()
        {
            _filtro.Usuarios = App.UsuarioService.Buscar(new Usuario());
            _filtro.Recursos = App.FuncionalidadeService.Buscar(new Funcionalidade { Status = true });
            _filtro.LogAcao = App.AcaoService.Buscar(new Acao());
            _filtro.LogResultado = App.BaseService.GetLogResultados();
        }
    }
}