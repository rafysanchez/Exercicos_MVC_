using System;
using System.Collections.Generic;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface ICrudPagamentoContaUnica<T> where T: class 
    {
        IEnumerable<T> FetchForGrid(T entity, DateTime since, DateTime until);

        int Remove(int id);

        IEnumerable<T> Fetch(T entity);

        int Save(T entity);

        T Get(int id);

        int GetNumeroAgrupamento();
    }
}
