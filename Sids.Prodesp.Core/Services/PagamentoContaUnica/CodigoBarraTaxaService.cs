using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class CodigoBarraBoletoService
    {
        private readonly ICrudLists<CodigoBarraBoleto> _repository;

        public CodigoBarraBoletoService(ICrudLists<CodigoBarraBoleto> repository)
        {
            _repository = repository;
        }

        public CodigoBarraBoleto Selecionar(CodigoBarraBoleto entity)
        {
            return _repository.Fetch(entity).FirstOrDefault();
        }

        public int SalvarOuAlterar(CodigoBarraBoleto entity)
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
