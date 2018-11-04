namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PerfilFuncionalidadeService : Base.BaseService
    {
        ICrudPerfilFuncionalidade perfilRecurso;

        public PerfilFuncionalidadeService(ILogError l, ICrudPerfilFuncionalidade r)
            : base(l)
        {
            perfilRecurso = r;
        }

        public void Salvar(PerfilFuncionalidade obj)
        {
            try
            {
                if (obj.Codigo == 0)
                {
                    PreInsertModel(obj);
                    perfilRecurso.Add(obj);
                }
                else
                {
                    perfilRecurso.Edit(obj);
                }
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
                perfilRecurso.Remove(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilFuncionalidade> Buscar(PerfilFuncionalidade obj)
        {
            try
            {
                return perfilRecurso.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilFuncionalidade> ObterPerfilRecursoPorPerfil(Perfil obj)
        {
            try
            {
                if (obj == null)
                    obj = new Perfil();

                return perfilRecurso.FetchByProfile(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
        
}
