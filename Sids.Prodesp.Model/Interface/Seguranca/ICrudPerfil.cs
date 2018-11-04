namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudPerfil : ICrudBase<Perfil>
    {
        IEnumerable<Perfil> FetchByUser(Usuario user);
        int GetUserCountByProfileId(int id);
    }
}
