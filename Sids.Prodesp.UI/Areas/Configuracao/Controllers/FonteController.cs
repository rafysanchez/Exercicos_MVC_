using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;
using Sids.Prodesp.Model.Exceptions;

namespace Sids.Prodesp.UI.Areas.Configuracao.Controllers
{
    public class FonteController : BaseController
    {
        public FonteController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(FonteController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home");

            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            var fontes = App.FonteService.Buscar(new Fonte { }).ToList();
            if (fontes.Count == 0)
            {
                ExibirMensagemErro("Nenhuma Fonte Cadastrada.");
            }
            return View(fontes);
        }


        [PermissaoAcesso(Controller = typeof(FonteController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));
            Fonte fonte = new Fonte();
            return View("CreateEdit", fonte);
        }

        [PermissaoAcesso(Controller = typeof(FonteController), Operacao = "Alterar")]
        public ActionResult Edit(int id, string tipo)
        {
            try
            {
                var fonte = App.FonteService.Buscar(new Fonte { Id = id }).First();
                return View("CreateEdit", fonte);
            }
            catch
            {
                ExibirMensagemErro("Não foi possível abrir o modo edição. Verifique o perfil selecionado.");
                return RedirectToAction("Index");
            }
        }

        [PermissaoAcesso(Controller = typeof(FonteController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                if (ObterQuantidadeReserva(int.Parse(id)) > 0)
                    throw new Exception("Não é permitida a exclusão da Fonte. Existem Reservas Vinculadas a essa Fonte");

                var fonte = App.FonteService.Buscar(new Fonte { Id = int.Parse(id) }).FirstOrDefault();
                var result = App.FonteService.Excluir(fonte, (int)_funcId, (int)EnumAcao.Excluir).ToString();
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
                var Id = _funcId.ToString();
                return RedirectToAction("Index", new { Id = Id });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        public ActionResult Save(DTOSalvarFonte dtoSalvarFonte)
        {
            try
            {
                EnumAcao enumAcao = dtoSalvarFonte.Fonte.Id > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
                if (dtoSalvarFonte.Fonte.Codigo != "000000000")
                {
                    var result = App.FonteService.Salvar(dtoSalvarFonte.Fonte, (int)_funcId, (short)enumAcao).ToString();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new SidsException("Informe um valor diferente de '000000000'");
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public class DTOSalvarFonte
        {
            public Fonte Fonte { get; set; }
        }

        [HttpPost]
        public JsonResult ObterQuatidadeReserva(int id)
        {
            try
            {
                int result = ObterQuantidadeReserva(id);

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