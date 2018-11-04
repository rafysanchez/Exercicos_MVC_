namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PerfilUsuarioService : Base.BaseService
    {
        ICrudPerfilUsuario perfilUsuario;

        public PerfilUsuarioService(ILogError l, ICrudPerfilUsuario r)
            : base(l)
        {
            perfilUsuario = r;
        }

        public void Salvar(PerfilUsuario obj)
        {
            try
            {
                if (obj.Codigo == 0)
                {
                    PreInsertModel(obj);
                    perfilUsuario.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public void Excluir(PerfilUsuario obj)
        {
            try
            {
                perfilUsuario.Remove(obj.Codigo);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilUsuario> Buscar(PerfilUsuario obj)
        {
            try
            {
                return perfilUsuario.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<PerfilUsuario> ObterPerfilUsuarioPorUsuario(Usuario obj)
        {
            try
            {
                if (obj == null)
                    obj = new Usuario();

                return perfilUsuario.FetchByUser(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }

}
