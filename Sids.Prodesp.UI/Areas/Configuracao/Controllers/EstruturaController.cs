using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.UI.Areas.Configuracao.Models.Estrutura;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;
using Sids.Prodesp.Model.Exceptions;

namespace Sids.Prodesp.UI.Areas.Configuracao.Controllers
{
    public class EstruturaController : BaseController
    {
        private List<EstruturaViewModel> _estruturas;

        // GET: Estrutura
        public EstruturaController()
        {
            _estruturas = new List<EstruturaViewModel>();
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home", new { Area = ""});

            App.PerfilService.SetCurrentFilter(null, "Estrutura");

            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            GerarFiltro(new FormCollection());

            return View(_estruturas);
        }



        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(f, "Estrutura");

                var obj = GerarFiltro(f);


                _estruturas = GerarEstruturaViewModels(App.EstruturaService.ObterPorPrograma(obj).ToList());

                if (_estruturas.Count == 0)
                {
                    ExibirMensagemErro("Nenhum registro Cadastrado.");
                }

                return View("Index", _estruturas);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        private Programa GerarFiltro(FormCollection f)
        {

            var obj = new Programa();

            var filtro = new FiltroViewModel
            {
                Anos = App.ProgramaService.ObterAnos(),
                Programas = App.ProgramaService.Buscar(new Programa()).ToList(),
                Fontes = App.FonteService.ObterFontes()
            };

            ExtrairDadosFiltro(f, ref obj, ref filtro);

            filtro.Ano = DateTime.Now.Year;

            ViewBag.Filtro = filtro;

            return obj;
        }

        private static void ExtrairDadosFiltro(FormCollection f, ref Programa obj, ref FiltroViewModel filtro)
        {
            if (!String.IsNullOrEmpty(f["AnoExercicio"]))
            {
                obj.Ano = int.Parse(f["AnoExercicio"]);
                filtro.Ano = int.Parse(f["AnoExercicio"]);
            }

            if (!String.IsNullOrEmpty(f["Programa"]))
            {
                obj.Codigo = int.Parse(f["Programa"]);
                filtro.Programa = int.Parse(f["Programa"]);
            }
        }


        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            GerarFiltro(new FormCollection());

            return View("CreateEdit", new Estrutura());
        }


        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Alterar")]
        public ActionResult Edit(int id)
        {
            var estrutura = App.EstruturaService.Buscar(new Estrutura { Codigo = id }).FirstOrDefault();
            var programa = App.ProgramaService.Buscar(new Programa { Codigo = (int)estrutura.Programa }).FirstOrDefault();
            GerarFiltro(new FormCollection());

            ViewBag.Filtro.Ano = programa.Ano;
            ViewBag.Filtro.Ptres = programa.Ptres;
            ViewBag.Filtro.Cfp = programa.Cfp;
            ViewBag.Filtro.Programa = programa.Codigo;

            return View("CreateEdit", estrutura);
        }

        [HttpPost]
        public ActionResult Save(Estrutura estrutura)
        {
            try
            {
                EnumAcao enumAcao = estrutura.Codigo > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
                var result = App.EstruturaService.Salvar(estrutura, (int)_funcId, (short)enumAcao).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Excluir"), HttpPost]
        public JsonResult Delete(string Id)
        {
            try
            {

                if (ObterQuantidadeReserva(int.Parse(Id)) > 0)
                    throw new SidsException("Não é permitida a exclusão da Fonte. Existem Reservas Vinculadas a essa Estrutura");

                var estrutura = App.EstruturaService.Buscar(new Estrutura { Codigo = int.Parse(Id) }).FirstOrDefault();
                var result = App.EstruturaService.Excluir(estrutura, (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(EstruturaController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("Estrutura");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });

            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        public JsonResult ObterProgramas()
        {
            try
            {
                var result = App.ProgramaService.Buscar(new Programa());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private static List<EstruturaViewModel> GerarEstruturaViewModels(IEnumerable<Estrutura> estruturas)
        {
            var programas = App.ProgramaService.Buscar(new Programa()).ToList();

            var estruturaViewModels = new List<EstruturaViewModel>();

            foreach (var estrutura in estruturas)
            {
                var programa = programas.FirstOrDefault(y => y.Codigo == estrutura.Programa);

                var estruturaViewModel = new EstruturaViewModel();

                estruturaViewModel.Codigo = estrutura.Codigo;
                estruturaViewModel.Programa = programa.Descricao;
                estruturaViewModel.Nomenclatura = estrutura.Nomenclatura;
                estruturaViewModel.Natureza = estrutura.Natureza;
                estruturaViewModel.Macro = estrutura.Macro;
                estruturaViewModel.Aplicacao = estrutura.Aplicacao;
                estruturaViewModel.Fonte = estrutura.Fonte;
                estruturaViewModel.Ptres = programa.Ptres;
                estruturaViewModel.Cfp = programa.Cfp.Formatar("00.000.0000.0000");
                estruturaViewModel.Ano = programa.Ano;

                estruturaViewModels.Add(estruturaViewModel);
            }



            return estruturaViewModels;
        }

        /// <summary>
        /// Retorna quantidade de recursos para um programa
        /// </summary>
        [HttpPost]
        public JsonResult ObterQuatidadeReserva(int id)
        {
            try
            {
                int result = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva { Estrutura = id }).Count();
                result += App.ReservaReforcoService.Buscar(new ReservaReforco { Programa = id }).Count();
                result += App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Programa = id }).Count();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private static int ObterQuantidadeReserva(int id)
        {
            int result = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva { Programa = id }).Count();
            result += App.ReservaReforcoService.Buscar(new ReservaReforco { Programa = id }).Count();
            result += App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Programa = id }).Count();
            return result;
        }
    }
}
