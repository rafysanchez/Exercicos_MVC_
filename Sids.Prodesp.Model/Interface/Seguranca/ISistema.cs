namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ISistema
    {
        string GetTableName();
        IEnumerable<Sistema> Fetch(Sistema entity);
    }
}
