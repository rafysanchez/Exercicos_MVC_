using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ReterService
    {
        private readonly ICrudReter _repository;

        public ReterService(ICrudReter repository)
        {
            _repository = repository;
        }

        public IEnumerable<Reter> Listar(Reter entity)
        {
            return _repository.Fatch(entity);
        }
    }
}
