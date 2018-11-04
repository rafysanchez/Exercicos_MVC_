using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.Seguranca.Models.Perfil;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.Seguranca.Controllers
{
    public class PerfilController : BaseController
    {
        List<Perfil> ListaDados = new List<Perfil>();
        public PerfilController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }
        
        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home");

            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));
            return View(ListaDados);
        }

        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(f, "Perfil");

                List<Perfil> list = new List<Perfil>();
                Perfil obj = GetFiltro(f);

                list = App.PerfilService.Buscar(obj);
                if (list.Count == 0)
                {
                    ExibirMensagemErro("Registros não encontrados.");
                }

                return View("Index", list);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }

        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            ViewBag.Funcionalidades = GerarViewBagFuncionalidades();

            return View("CreateEdit", new Perfil());
        }


        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Alterar")]
        public ActionResult Edit(int id, string tipo)
        {
            try
            {
                var perfil = App.PerfilService.Buscar(new Perfil { Codigo = id }).First();
                var perfilAcao = App.PerfilAcaoService.Buscar(new PerfilAcao { Perfil = id });

                ViewBag.Funcionalidades = GerarViewBagFuncionalidades(perfilAcao);

                return View("CreateEdit", perfil);
            }
            catch(Exception ex)
            {
                ExibirMensagemErro("Não foi possível abrir o modo edição. Verifique o perfil selecionado." + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Save(DTOSalvarPerfil dtoSalvarPerfil)
        {
            try
            {
                EnumAcao enumAcao = dtoSalvarPerfil.perfil.Codigo > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
                var result = App.PerfilService.Salvar(dtoSalvarPerfil.perfil, dtoSalvarPerfil.perfilAcao, (int)_funcId, (short)enumAcao).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Excluir"),HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                var result = App.PerfilService.Excluir(int.Parse(Id), (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }




        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Ativar/Desativar"), HttpPost]
        public ActionResult AlterarStatus(string Id)
        {
            try
            {
                var perfil = new Perfil { Codigo = int.Parse(Id) };

                perfil = App.PerfilService.Buscar(perfil).FirstOrDefault();
                EnumAcao enumAcao = perfil.Status == true ? EnumAcao.Bloquear : EnumAcao.Ativar;
                perfil.Status = !perfil.Status;


                var result = App.PerfilService.Salvar(perfil, App.PerfilAcaoService.ObterPerfilAcaoPorPerfil(perfil.Codigo), (int)_funcId, (short)enumAcao).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //private static List<PerfilFuncionalidade> ExtrairPerfilFuncionalidade(Perfil obj, FormCollection f)
        //{
        //    List<PerfilFuncionalidade> perfilfuncionalidades = new List<PerfilFuncionalidade>();
        //    var funcionalidadeAcoes = App.FuncionalidadeAcaoService.Buscar(new FuncionalidadeAcao());

        //    foreach (FuncionalidadeAcao fa in funcionalidadeAcoes)
        //    {
        //        if ((f.AllKeys.Contains("PF_" + fa.Codigo) && f["PF_" + fa.Codigo] == "on") && (f.AllKeys.Contains("F_" + fa.Funcionalidade) && f["F_" + fa.Funcionalidade] == "on"))
        //        {
        //            PerfilFuncionalidade perfilFuncionalidade = new PerfilFuncionalidade { Perfil = obj.Codigo, Funcionalidade = fa.Funcionalidade };

        //            if (!perfilfuncionalidades.Contains(perfilFuncionalidade))
        //                perfilfuncionalidades.Add(perfilFuncionalidade);
        //        }
        //    }

        //    return perfilfuncionalidades.Distinct().ToList();
        //}


        private Perfil GetFiltro(FormCollection f)
        {
            Perfil obj = new Perfil();
            obj.Descricao = f["txtDescricao"];
            obj.Detalhe = f["txtDetalhe"];
            ViewBag.Filtro = obj;
            return obj;
        }


        private List<MenuViewModel> GerarViewBagFuncionalidades(List<PerfilAcao> perfilAcao = null)
        {

            var funcionalidadeAcao = App.FuncionalidadeAcaoService.Buscar(new FuncionalidadeAcao());
            var funcionalidade = App.FuncionalidadeService.Buscar(new Funcionalidade { Status = true });
            var menuItem = App.MenuItemService.Buscar(new MenuItem { Status = true });
            var menu = App.MenuService.Buscar(new Menu { Status = true });
            var acao = App.AcaoService.Buscar(new Acao());


            List<AcaoViewModel> acaoViewModel = funcionalidadeAcao.Select(x => new AcaoViewModel
            {
                Id = x.Acao,
                Descricao = acao.FirstOrDefault(a => a.Id == x.Acao)?.Descricao,
                FuncionalidadeAcaoId = x.Codigo,
                FuncionalidadeId = x.Funcionalidade,
                Associado = perfilAcao?.Select(p => p.Funcionalidade).Contains(x.Codigo) ?? false
            }).OrderBy(x => x.Id).ToList();

            List<FuncionalidadeAcaoViewModel> funcionalidadeAcoes = funcionalidade.Select(x => new FuncionalidadeAcaoViewModel
            {
                Id = x.Codigo,
                Menu = menuItem.FirstOrDefault(m => m.Recurso == x.Codigo)?.Menu?? default(int),
                Funcionalidade = x.Nome,
                Acoes = acaoViewModel.Where(a => a.FuncionalidadeId == x.Codigo).ToList()
            }).OrderBy(x => x.Menu).ThenBy(n => n.Id).ToList();

            List<MenuViewModel> menusList = menu.Select(x => new MenuViewModel
            {
                Id = x.Codigo,
                Descricao = x.Descricao,
                FuncionalidadeAcoes = funcionalidadeAcoes.Where(y => y.Menu == x.Codigo).ToList()
            }).ToList();

            return menusList;
        }
        public class DTOSalvarPerfil
        {
            public List<PerfilAcao> perfilAcao { get; set; }
            public Perfil perfil { get; set; }
        }

        [PermissaoAcesso(Controller = typeof(PerfilController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("Perfil");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }
        
    }
}

