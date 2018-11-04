using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface ICrudLists<TEntity>
    {
        int Save(TEntity entity);
        int Remove(int id);
        IEnumerable<TEntity> Fetch(TEntity entity);
    }
}
