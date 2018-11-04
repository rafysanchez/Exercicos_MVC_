namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudMenuItem : ICrudBase<MenuItem>
    {
        IEnumerable<MenuItem> FetchByItem(MenuItem entity);
        IEnumerable<MenuItem> FetchByMenu(Menu menu);
        IEnumerable<MenuItem> FetchByUser(Usuario user);
        IEnumerable<Url> FetchUrlById(int urlId);
    }
}
