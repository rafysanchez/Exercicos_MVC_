namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class EventoTipoService
    {
        private readonly ICrudEventoTipo _repository;


        public EventoTipoService(ICrudEventoTipo repository)
        {
            _repository = repository;
        }


        public IEnumerable<EventoTipo> Buscar(EventoTipo entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
