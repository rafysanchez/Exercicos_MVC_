namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudPerfilUsuario : ICrudBase<PerfilUsuario>
    {
        new int Remove(int id);
        IEnumerable<PerfilUsuario> FetchByProfile(Perfil entity);
        IEnumerable<PerfilUsuario> FetchByUser(Usuario entity);
    }
}
