namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PerfilAcaoService : Base.BaseService
    {
        ICrudPerfilAcao perfilAcao;

        public PerfilAcaoService(ILogError l, ICrudPerfilAcao r)
            : base(l)
        {
            perfilAcao = r;
        }

        public void Salvar(PerfilAcao obj)
        {
            try
            {
                    PreInsertModel(obj);
                    perfilAcao.Add(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public void Excluir(int id)
        {
            try
            {
                perfilAcao.Remove(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilAcao> Buscar(PerfilAcao obj)
        {
            try
            {
                return perfilAcao.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilAcao> ObterPerfilAcaoPorPerfil(int id)
        {
            try
            {
                var obj = new Perfil { Codigo = id};

                return perfilAcao.FetchByProfile(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilAcao> ObterPerfilAcaoPorRecursoEUsuario(int recursoId)
        {

            try
            {
                var recurso = new Funcionalidade { Codigo = recursoId };
                var usuario = new Usuario { Codigo = GetUserIdLogado() };

                return perfilAcao.FetchByUserAndFunctionality(usuario, recurso).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
            
    }
        
}
