namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface IRegional
    {
        string GetTableName();
        IEnumerable<Regional> Fetch(Regional entity);
    }
}
