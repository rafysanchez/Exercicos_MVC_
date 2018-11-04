using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoTipoService
    {
        private readonly ICrudProgramacaoDesembolsoTipo _repository;

        public ProgramacaoDesembolsoTipoService(ICrudProgramacaoDesembolsoTipo repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<ProgramacaoDesembolsoTipo> Listar(ProgramacaoDesembolsoTipo entity)
        {
            return _repository.Fatch(entity);
        }
        
    }
}
