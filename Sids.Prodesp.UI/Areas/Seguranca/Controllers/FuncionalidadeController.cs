using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.Seguranca.Models.Funcionalidade;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.Seguranca.Controllers
{
    public class FuncionalidadeController : BaseController
    {

        public FuncionalidadeController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {

            if (Id == null)
                return RedirectToAction("Index", "Home");

            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            return View(new List<FuncionalidadeViewModel>());
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.FuncionalidadeService.SetCurrentFilter(f, "Funcionalidade");
                FuncionalidadeViewModel funcionalidadeViewModel = new FuncionalidadeViewModel();
                List<Funcionalidade> list = new List<Funcionalidade>();
                Funcionalidade obj = GetFiltro(f);

                list = App.FuncionalidadeService.Buscar(obj);

                var viewModels = funcionalidadeViewModel.GerarViewModels(list);

                if (list.Count == 0)
                {
                    ExibirMensagemErro("Registros não encontrados.");
                }
                return View("Index", viewModels);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }

        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            Funcionalidade funcionalidade = new Funcionalidade();
            funcionalidade.Acoes = new List<Acao>();

            ViewBag.Acoes = GerarAcoes(App.AcaoService.Buscar(new Acao()), funcionalidade);

            ViewBag.URL = App.MenuItemService.GetMenuUrl(0);

            ViewBag.Menus = App.MenuService.Buscar(new Menu());

            return View("CreateEdit", funcionalidade);
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Alterar")]
        public ActionResult Edit(int id,string tipo)
        {
            try
            {
                var funcionalidade = App.FuncionalidadeService.Buscar(new Funcionalidade { Codigo = id }).First();
                
                ViewBag.Acoes = GerarAcoes(App.AcaoService.Buscar(new Acao()), funcionalidade);
                ViewBag.tipo = tipo;
                funcionalidade.MenuId = Convert.ToInt32(App.MenuItemService.Buscar(new MenuItem { Recurso = funcionalidade.Codigo }).FirstOrDefault()?.Menu);
                ViewBag.Menus = App.MenuService.Buscar(new Menu());

                ViewBag.URL = App.MenuItemService.GetMenuUrl(0);

                return View("CreateEdit", funcionalidade);
            }
            catch
            {
                ExibirMensagemErro("Não foi possível abrir o modo edição. Verifique a Funcionalidade selecionada.");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Save(DTOSalvarFuncionalidade dtoSalvarFuncionalidade)
        {

            try
            {
                EnumAcao enumAcao = dtoSalvarFuncionalidade.Funcionalidade.Codigo > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;

                dtoSalvarFuncionalidade.Funcionalidade.URL = App.MenuItemService.GetMenuUrl(dtoSalvarFuncionalidade.Funcionalidade.MenuUrlId).FirstOrDefault()?.DescricaoUrl;

                MenuItem menuItem = new MenuItem
                {
                    Recurso = App.FuncionalidadeService.Salvar(dtoSalvarFuncionalidade.Funcionalidade, dtoSalvarFuncionalidade.Acoes, (int)_funcId, (short)enumAcao),
                    Rotulo = dtoSalvarFuncionalidade.Funcionalidade.Nome,
                    DescMenu = dtoSalvarFuncionalidade.Funcionalidade.Descricao,
                    Menu = dtoSalvarFuncionalidade.Funcionalidade.MenuId
                };
                var menus = App.MenuItemService.Buscar(new MenuItem { Recurso = menuItem.Recurso });

                if(menus.Count > 0)
                    menuItem.Codigo = menus.FirstOrDefault().Codigo;
                
                var result = App.MenuItemService.Salvar(menuItem, (int)_funcId, (short)enumAcao).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Excluir"), HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = App.FuncionalidadeService.Excluir(id, (int)_funcId, (short)EnumAcao.Excluir).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Ativar/Desativar"),HttpPost]
        public ActionResult AlterarStatus(string Id)
        {
            try
            {
                var funcionalidade = new Funcionalidade { Codigo = int.Parse(Id) };

                funcionalidade = App.FuncionalidadeService.Buscar(funcionalidade).FirstOrDefault();
                EnumAcao enumAcao = funcionalidade.Status == true ? EnumAcao.Bloquear : EnumAcao.Ativar;
                funcionalidade.Status = !funcionalidade.Status;

                App.FuncionalidadeService.Salvar(funcionalidade,null, (int)_funcId, (short)enumAcao);
                var result = "Sucesso";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        private Funcionalidade GetFiltro(FormCollection f)
        {
            FuncionalidadeViewModel funcionalidadeViewModel = new FuncionalidadeViewModel();
            Funcionalidade obj = new Funcionalidade();

            obj.Nome = f["txtNome"];
            obj.URL = f["txtUrl"];
            obj.Descricao = f["txtDetalhe"];
            obj.MenuId = Convert.ToInt32(f["ddlMenu"]);

            funcionalidadeViewModel.Nome = obj.Nome;
            funcionalidadeViewModel.Detalhes = obj.Descricao;
            funcionalidadeViewModel.CaminhoFisico = obj.URL;

            ViewBag.Filtro = funcionalidadeViewModel;
            return obj;
        }

        public class DTOSalvarFuncionalidade
        {
            public Funcionalidade Funcionalidade { get; set; }
            public List<FuncionalidadeAcao> Acoes { get; set; }
            
        }

        private List<DtoAcoesFuncionalidades> GerarAcoes(List<Acao> listAcao, Funcionalidade funcionalidade)
        {
            var AcoesIds = funcionalidade.Acoes.Select(a => a.Id).ToList();

            var acoes = listAcao.Select(x => new DtoAcoesFuncionalidades
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Associado = AcoesIds.Contains(x.Id)
            }).OrderBy(x => x.Id).ToList();

            return acoes;
        }

        [PermissaoAcesso(Controller = typeof(FuncionalidadeController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("Funcionalidade");

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
