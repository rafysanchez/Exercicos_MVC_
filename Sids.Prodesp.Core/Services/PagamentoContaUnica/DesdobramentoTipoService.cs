using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class DesdobramentoTipoService
    {
        private readonly ICrudDesdobramentoTipo _repository;

        public DesdobramentoTipoService(ICrudDesdobramentoTipo repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<DesdobramentoTipo> Listar(DesdobramentoTipo entity)
        {
            return _repository.Fatch(entity);
        }
        
    }
}
