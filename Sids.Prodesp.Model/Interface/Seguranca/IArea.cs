namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface IArea
    {
        string GetTableName();
        IEnumerable<Area> Fetch(Area entity);
    }
}
