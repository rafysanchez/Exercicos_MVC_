using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoTipoService
    {
        private readonly ICrudReclassificacaoRetencaoTipo _repository;

        public ReclassificacaoRetencaoTipoService(ICrudReclassificacaoRetencaoTipo repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<ReclassificacaoRetencaoTipo> Listar(ReclassificacaoRetencaoTipo entity)
        {
            return _repository.Fatch(entity);
        }
        
    }
}
