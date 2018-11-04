using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudIdentificacaoDesdobramento
    {
        IEnumerable<IdentificacaoDesdobramento> Fetch(IdentificacaoDesdobramento entity);

        int Remove(IdentificacaoDesdobramento entity);
        
        int Save(IdentificacaoDesdobramento entity);

        IdentificacaoDesdobramento Get(int id);
    }
}
