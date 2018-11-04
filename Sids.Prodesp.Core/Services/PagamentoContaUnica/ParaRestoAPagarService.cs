using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ParaRestoAPagarService
    {
        private readonly ICrudRestoPagar _repository;

        public ParaRestoAPagarService(ICrudRestoPagar repository)
        {
            _repository = repository;
        }

        public IEnumerable<ParaRestoAPagar> Listar(ParaRestoAPagar entity)
        {
            return _repository.Fatch(entity);
        }
    }
}
