using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System.Collections.Generic;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class TipoBoletoService
    {

        private readonly ICrudTipoBoleto _repository;

        public TipoBoletoService(ICrudTipoBoleto repository)
        {
            _repository = repository;
        }

        public IEnumerable<TipoBoleto> Listar(TipoBoleto entity)
        {
            return _repository.Fatch(entity);
        }

    }
}


    
       
    


