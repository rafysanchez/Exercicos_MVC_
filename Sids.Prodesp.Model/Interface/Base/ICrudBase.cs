namespace Sids.Prodesp.Model.Interface.Base
{
    using System.Collections.Generic;

    public interface ICrudBase<TEntity>
    {
        string GetTableName();
        int Add(TEntity entity);
        int Edit(TEntity entity);
        int Remove(int id);
        IEnumerable<TEntity> Fetch(TEntity entity);
    }
}
