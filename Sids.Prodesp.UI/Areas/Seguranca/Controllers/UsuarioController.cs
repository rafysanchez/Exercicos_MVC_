using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Areas.Seguranca.Models.Usuario;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Security;

namespace Sids.Prodesp.UI.Areas.Seguranca.Controllers
{
    public class UsuarioController : BaseController
    {
        List<GridUsuarioViewModel> _gridUsuarioViewModel;
        public UsuarioController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual() ?? 0;
            _gridUsuarioViewModel = new List<GridUsuarioViewModel>();
        }


        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Listar")]
        public ActionResult Index(string Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home");

            App.BaseService.DeleteCurrentFilter("Usuarios");
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));
            AtualizarFiltros(new UsuarioViewModel());

            return View(_gridUsuarioViewModel);
        }


        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(f, "Usuarios");
                var grid = new GridUsuarioViewModel();
                var obj = GetFiltro(f);
                var usuarios = App.UsuarioService.Buscar(obj);

                _gridUsuarioViewModel = grid.GerarGrid(usuarios);

                if (_gridUsuarioViewModel.Count == 0)
                {
                    ExibirMensagemErro("Registros não encontrados.");
                }
                return View("Index", _gridUsuarioViewModel);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }



        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Incluir")]
        public ActionResult Create(string Id)
        {

            AtualizarFiltros(new UsuarioViewModel());

            ViewBag.Perfil = GetPerfis(new List<PerfilUsuario>());

            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));
            var usuario = new Usuario();
            return View("CreateEdit", usuario);
        }

        private static List<DtoPerfilUsuarioViewModel> GetPerfis(List<PerfilUsuario> perfilUsuario)
        {
            var ids = perfilUsuario.Select(y => y.Perfil).ToList();
            var perfil = App.PerfilService.Buscar(new Perfil { Status = true }).Where(x => x.Status == true).ToList();
            var dtoPerfilUsuario = perfil.Select(x => new DtoPerfilUsuarioViewModel
            {
                Id = x.Codigo,
                Descricao = x.Descricao,
                Associado = ids.Contains(x.Codigo)
            }).ToList();

            return dtoPerfilUsuario;
        }


        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Alterar")]
        public ActionResult Edit(int id)
        {
            try
            {
                var usuario = App.UsuarioService.Buscar(new Usuario { Codigo = id }).First();
                var filtro = new UsuarioViewModel();

                filtro.SistemaId = usuario.SistemaId;
                filtro.AreaId = usuario.AreaId;
                filtro.RegionalId = usuario.RegionalId;
                usuario.Senha = App.UsuarioService.Decrypt(usuario.Senha);
                AtualizarFiltros(filtro);
                ViewBag.Perfil = GetPerfis(App.PerfilUsuarioService.ObterPerfilUsuarioPorUsuario(usuario));
                return View("CreateEdit", usuario);
            }
            catch
            {
                ExibirMensagemErro("Não foi possível abrir o modo edição. Verifique o usuário selecionado.");
                return RedirectToAction("Index","Usuario");
            }
        }


        public ActionResult Save(DTOSalvarUsuario dtoSalvarUsuario)
        {
            try
            {
                var acaoId = dtoSalvarUsuario.Usuario.Codigo > 0 ? (short)EnumAcao.Alterar : (short)EnumAcao.Inserir;
                dtoSalvarUsuario.Usuario.Status = true;
                dtoSalvarUsuario.Usuario.Senha = App.BaseService.Encrypt(dtoSalvarUsuario.Usuario.Senha);
                var result = App.UsuarioService.Salvar(dtoSalvarUsuario.Usuario, dtoSalvarUsuario.PerfisUsuario, (int)_funcId, acaoId).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Excluir"), HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = App.UsuarioService.Excluir(id, (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SalvarImpressora(string imp132, string imp80)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var acaoId = (short) EnumAcao.Alterar;
                usuario.Impressora132 = imp132;
                usuario.Impressora80 = imp80;
                 var result = App.UsuarioService.Salvar(usuario, null, (int)_funcId, acaoId).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Alterar")]
        public ActionResult GerarNovaSenha(int id)
        {
            try
            {
                if (App.AutenticacaoService.GetUsuarioLogado().Codigo == id)
                {
                    return RedirectToAction("AlterarSenha");
                }

                Usuario u = App.UsuarioService.Buscar(new Usuario { Codigo = id }).FirstOrDefault();
                string novaSenha = App.UsuarioService.GerarNovaSenha(u);

                TempData["Sucesso"] = true;
                TempData["Mensagem"] = String.Format("{0} possui agora uma nova senha: {1}", u.ChaveDeAcesso, novaSenha);
            }
            catch (Exception ex)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = String.Format("Erro ao gerar nova senha.\n{0}", ex.Message);
            }
            return RedirectToAction("Index");
        }



        public ActionResult AlterarSenha(string Id)
        {
            if (Id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(Id));

            var codigo = App.BaseService.GetUserIdLogado();
            ViewBag.AcessoSiafem = App.UsuarioService.Buscar(new Usuario { Codigo = codigo }).FirstOrDefault().AcessaSiafem;
            return View();
        }



        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Ativar/Desativar"), HttpPost]
        public ActionResult AlterarStatus(string Id)
        {
            try
            {
                var usuario = new Usuario { Codigo = int.Parse(Id) };

                usuario = App.UsuarioService.Buscar(usuario).FirstOrDefault();
                EnumAcao enumAcao = usuario.Status == true ? EnumAcao.Bloquear : EnumAcao.Ativar;
                usuario.Status = !usuario.Status;


                var result = App.UsuarioService.Salvar(usuario, App.PerfilUsuarioService.ObterPerfilUsuarioPorUsuario(usuario), (int)_funcId, (short)enumAcao).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [PermissaoAcesso(Controller = typeof(UsuarioController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("Usuarios");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }


        [HttpPost]
        public ActionResult AlterarSenha(DTOSalvarSenha dtoSalvarSenha)
        {
            try
            {
                _funcId = _funcId ?? 0;

                dtoSalvarSenha.Senha = App.UsuarioService.Encrypt(dtoSalvarSenha.Senha);
                dtoSalvarSenha.NovaSenha = App.UsuarioService.Encrypt(dtoSalvarSenha.NovaSenha);
                Usuario obj = App.AutenticacaoService.GetUsuarioLogado();
                var result = App.UsuarioService.AlterarSenha(obj, dtoSalvarSenha.NovaSenha, dtoSalvarSenha.Senha, (int)_funcId).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        private Usuario GetFiltro(FormCollection f)
        {
            Usuario usuario = new Usuario();
            UsuarioViewModel obj = new UsuarioViewModel();

            obj.Nome = f["txtNome"];
            obj.ChaveDeAcesso = f["txtChaveAcesso"];
            obj.CPF = f["txtCPF"] == null? null: f["txtCPF"].Replace(".", "").Replace("-", "");
            obj.Email = f["txtEmail"];
            usuario.Email = f["txtEmail"];
            usuario.Nome = f["txtNome"];
            usuario.ChaveDeAcesso = f["txtChaveAcesso"];
            usuario.CPF = f["txtCPF"] == null ? null : f["txtCPF"].Replace(".", "").Replace("-", "");


            if (!String.IsNullOrEmpty(f["ddlRegional"]))
            {
                obj.RegionalId = short.Parse(f["ddlRegional"]);
                usuario.RegionalId = short.Parse(f["ddlRegional"]);
            }

            if (!String.IsNullOrEmpty(f["ddlArea"]))
            {
                obj.AreaId = short.Parse(f["ddlArea"]);
                usuario.AreaId = short.Parse(f["ddlArea"]);
            }

            if (!String.IsNullOrEmpty(f["ddlSistema"]))
            {
                obj.SistemaId = short.Parse(f["ddlSistema"]);
                usuario.SistemaId = short.Parse(f["ddlSistema"]);
            }

            AtualizarFiltros(obj);
            return usuario;
        }
        private void AtualizarFiltros(UsuarioViewModel obj)
        {
            obj.Area = App.AreaService.Buscar(new Area()).ToList();
            obj.Regional = App.RegionalService.Buscar(new Regional()).ToList();
            obj.Sistema = App.SistemaService.Buscar(new Sistema()).ToList();

            ViewBag.Filtro = obj;
        }
        public class DTOSalvarUsuario
        {
            public Usuario Usuario { get; set; }
            public List<PerfilUsuario> PerfisUsuario { get; set; }

        }
        public class DTOSalvarSenha
        {
            public string Senha { get; set; }
            public string NovaSenha { get; set; }

        }
    }

}