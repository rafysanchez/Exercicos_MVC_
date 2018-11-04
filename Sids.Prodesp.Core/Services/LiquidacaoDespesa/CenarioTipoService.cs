namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class CenarioTipoService
    {
        private readonly ICrudCenarioTipo _repository;


        public CenarioTipoService(ICrudCenarioTipo repository)
        {
            _repository = repository;
        }


        public IEnumerable<CenarioTipo> Buscar(CenarioTipo entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
