using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.Seguranca.Controllers;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.Configuracao.Controllers
{
    public class ProgramaController : BaseController
    {
        private List<Programa> _programas;
        public ProgramaController()
        {
            _programas = new List<Programa>();
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home", new { Area = "" });

            App.PerfilService.SetCurrentFilter(null, "Programa");
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            GerarComboAno();

            ViewBag.AnosSelect = DateTime.Now.Year;

            _programas = App.ProgramaService.Buscar(new Programa { Ano = ViewBag.AnosSelect }).ToList();

            if (_programas.Count == 0)
            {
                ExibirMensagemErro("Nenhum Programa Cadastrado.");
            }

            return View(_programas);
        }

        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Listar"), HttpPost]

        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(f, "Programa");
                int ano = 0;

                if (!String.IsNullOrEmpty(f["ddlAnos"]))
                    ano = int.Parse(f["ddlAnos"]);

                _programas = App.ProgramaService.Buscar(new Programa { Ano = ano }).ToList();

                GerarComboAno();

                ViewBag.AnosSelect = ano;

                if (_programas.Count == 0)
                {
                    ExibirMensagemErro("Nenhum Programa Cadastrado.");
                }

                return View("Index", _programas);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        private void GerarComboAno()
        {
            var anos = App.ProgramaService.GetAnosPrograma().OrderBy(x => x).ToList();

            ViewBag.AnoCadastrado = anos.Max();

            ViewBag.AnoCadastrado = anos.Max();

            if (anos.Count(x => x == DateTime.Now.Year) == 0)
                anos.Add(DateTime.Now.Year);


            //ViewBag.AnosSelect = anos.Max();

            ViewBag.Anos = anos.Select(x => new
            {
                Text = x.ToString(),
                Value = x.ToString()
            });

        }

        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            GerarComboAno();

            ViewBag.AnosSelect = DateTime.Now.Year;

            return View("CreateEdit", new Programa { Ano = ViewBag.AnosSelect });
        }

        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Alterar")]
        public ActionResult Edit(int id, string tipo)
        {
            try
            {
                var programa = App.ProgramaService.Buscar(new Programa { Codigo = id }).First();

                GerarComboAno();

                ViewBag.AnosSelect = programa.Ano;

                return View("CreateEdit", programa);
            }
            catch (Exception)
            {
                ExibirMensagemErro("Não foi possível abrir o modo edição. Verifique o perfil selecionado.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Save(Programa programa)
        {
            try
            {
                EnumAcao enumAcao = programa.Codigo > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
                var result = App.ProgramaService.Salvar(programa, (int)_funcId, (short)enumAcao).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Excluir"), HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                if (ObterQuantidadeReserva(int.Parse(Id)) > 0)
                    throw new Exception("Não é permitida a exclusão da Fonte. Existem Reservas Vinculadas a esse Programa");

                var programa = App.ProgramaService.Buscar(new Programa { Codigo = int.Parse(Id) }).FirstOrDefault();
                var result = App.ProgramaService.Excluir(programa, (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("Programa");
                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        [PermissaoAcesso(Controller = typeof(ProgramaController), Operacao = "Incluir")]
        public ActionResult GerarEstruturaAnoAtual()
        {
            try
            {
                int ano = App.ProgramaService.GerarEstruturaAnoAtual((int)_funcId, (short)EnumAcao.Inserir);
                var result = new { Status = "Sucesso", Msg = "Estrutura gerada com sucesso para o ano " + ano + "." };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Erro", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Retorna quantidade de recursos para um programa
        /// </summary>
        [HttpPost]
        public JsonResult ObterQuatidadeReserva(int id)
        {
            try
            {
                var result = ObterQuantidadeReserva(id);

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
            int result = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva {Programa = id}).Count();
            result += App.ReservaReforcoService.Buscar(new ReservaReforco {Programa = id}).Count();
            result += App.ReservaCancelamentoService.Buscar(new ReservaCancelamento {Programa = id}).Count();
            return result;
        }
    }
}