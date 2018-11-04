using Sids.Prodesp.Infrastructure.DataBase.Seguranca;

namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Model.Enum;
    using Model.Exceptions;

    public class FuncionalidadeService : Base.BaseService
    {
        private readonly ICrudFuncionalidade _funcionalidade;
        private readonly ICrudFuncionalidadeAcao _funcionalidadeAcao;
        private readonly ICrudPerfilAcao _perfilAcao;
        private readonly IAcao _acao;
        private readonly MenuItemService _menuItemService;
        public FuncionalidadeService(ILogError l, ICrudFuncionalidade f, ICrudFuncionalidadeAcao ia, ICrudPerfilAcao pa, IAcao a) : base(l)
        {
            _funcionalidade = f;
            _funcionalidadeAcao = ia;
            _perfilAcao = pa;
            _acao = a;
            _menuItemService = new MenuItemService(l, new MenuItemDal());
        }

        public int Salvar(Funcionalidade obj, List<FuncionalidadeAcao> funcionalidadeAcoes, int FuncionalidadeId, short actionId)
        {
            try
            {
                var funcionalidades = _funcionalidade.Fetch(new Funcionalidade { Nome = obj.Nome }).ToList();
                int id = 0;

                if (obj.Codigo == 0)
                {

                    if (funcionalidades.Count(x => x.Nome == obj.Nome) > 0)
                    {
                        if(_menuItemService.Buscar(new MenuItem {Menu = obj.MenuId, Rotulo = obj.Nome }).Any())
                            throw new SidsException("Não é possível realizar cadastro, funcionalidade já cadastrada.");
                    }

                    PreInsertModel(obj);
                    id = _funcionalidade.Add(obj);
                    SalvarAcoes(id, funcionalidadeAcoes);

                    var arg = String.Format("Nome {0}", obj.Nome);

                    LogSucesso(actionId, FuncionalidadeId, arg.ToString());
                }
                else
                {

                    if (funcionalidades.Count(x => x.Nome == obj.Nome && x.Codigo != obj.Codigo) > 0)
                        throw new SidsException("Nome de funcionalidade já cadastrada.");

                    _funcionalidade.Edit(obj);

                    if (funcionalidadeAcoes != null)
                        SalvarAcoes(obj.Codigo, funcionalidadeAcoes);

                    id = obj.Codigo;

                    var arg = String.Format("Nome {0}", obj.Nome);

                    LogSucesso(actionId, FuncionalidadeId, arg.ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, FuncionalidadeId);
            }
        }
        public AcaoEfetuada Excluir(int id, int FuncionalidadeId, short actionId)
        {
            try
            {
                var perfilAcoes = _perfilAcao.Fetch(new PerfilAcao { Funcionalidade = id }).ToList();

                if (perfilAcoes.Count > 0)
                    throw new SidsException("Não é possível continuar a operação, a funcionalidade possui associação a perfis de usuário.");

                _funcionalidade.Remove(id);
                return LogSucesso(actionId, FuncionalidadeId, id.ToString());
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, FuncionalidadeId);
            }
        }
        public List<Funcionalidade> Buscar(Funcionalidade obj)
        {
            try
            {
                var funcionalidades = _funcionalidade.Fetch(obj).ToList();

                foreach (var func in funcionalidades)
                {
                    func.Acoes = _acao.FetchByFunctionality(func).ToList();
                }

                return funcionalidades;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public dynamic ObterFuncionalidadePorUsuario(Usuario obj)
        {
            try
            {
                return _funcionalidade.FetchByUser(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<Funcionalidade> ObterFuncionalidadePorId(List<int> ids)
        {
            try
            {
                return _funcionalidade.FetchByIds(ids).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        private void SalvarAcoes(int id, List<FuncionalidadeAcao> acoes)
        {
            try
            {
                var acoesSalvas = _funcionalidadeAcao.FetchByFunctionality(new Funcionalidade { Codigo = id }).ToList();

                foreach (FuncionalidadeAcao acao in acoesSalvas)
                {
                    if (acoes.Count(x => x.Acao == acao.Acao && x.Funcionalidade == acao.Funcionalidade) == 0)
                        _funcionalidadeAcao.Remove(acao.Codigo);
                }

                foreach (FuncionalidadeAcao item in acoes)
                {
                    if (acoesSalvas.Count(x => x.Acao == item.Acao && x.Funcionalidade == item.Funcionalidade) == 0)
                    {
                        item.Funcionalidade = id;
                        item.Status = true;
                        _funcionalidadeAcao.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public int? ObterFuncionalidadeAtual()
        {
            System.Web.HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if(cookie != null) { 
                string func = cookie.Values["func"];

                if (cookie?.Values["func"] != null)
                    return int.Parse(cookie.Values["func"]);
            }
            return null;
        }

        public void SalvarFuncionalidadeAtual(int id)
        {
            System.Web.HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];

            cookie = cookie ?? new HttpCookie("UserInfo");

            if (cookie.Values["func"] == null)
                cookie.Values.Add("func", Convert.ToString(id));
            else
                cookie.Values["func"] = Convert.ToString(id);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
