namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudFuncionalidade: ICrudBase<Funcionalidade>
    {
        IEnumerable<Funcionalidade> FetchByUser(Usuario user);
        IEnumerable<Funcionalidade> FetchByIds(IEnumerable<int> ids);
    }
}
