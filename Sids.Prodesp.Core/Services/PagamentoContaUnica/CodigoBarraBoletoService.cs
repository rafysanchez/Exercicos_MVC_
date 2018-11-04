using System;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class CodigoBarraTaxaService
    {
        private readonly ICrudLists<CodigoBarraTaxa> _repository;

        public CodigoBarraTaxaService(ICrudLists<CodigoBarraTaxa> repository)
        {
            _repository = repository;
        }

        public CodigoBarraTaxa Selecionar(CodigoBarraTaxa entity)
        {
            return _repository.Fetch(entity).FirstOrDefault()?? new CodigoBarraTaxa();
        }

        public int SalvarOuAlterar(CodigoBarraTaxa entity)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                return entity.Id;
            }
            catch
            {
                throw;
            }
        }
    }
}
