using System.Collections.Generic;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface ICrudCombos<T> where T : class
    {
        IEnumerable<T> Fatch(T entity);
    }
}
