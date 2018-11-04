using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudCredor
    {
        void Save(Credor entity);
        void Delete();
        IEnumerable<Credor> Fetch(Credor entity);
    }
}
