namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class CodigoEventoService
    {
        private readonly ICrudCodigoEvento _repository;


        public CodigoEventoService(ICrudCodigoEvento repository)
        {
            _repository = repository;
        }


        public IEnumerable<CodigoEvento> Buscar(CodigoEvento entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
