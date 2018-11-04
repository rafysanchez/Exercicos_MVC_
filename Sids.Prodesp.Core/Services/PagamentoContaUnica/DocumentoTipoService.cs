using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class DocumentoTipoService
    {
        private readonly ICrudDocumentoTipo _repository;

        public DocumentoTipoService(ICrudDocumentoTipo repository)
        {
            _repository = repository;
        }

        public IEnumerable<DocumentoTipo> Listar(DocumentoTipo entity)
        {
            return _repository.Fatch(entity);
        }
    }
}
