namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class ObraTipoService
    {
        private readonly ICrudObraTipo _repository;


        public ObraTipoService(ICrudObraTipo repository)
        {
            _repository = repository;
        }


        public IEnumerable<ObraTipo> Buscar(ObraTipo entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
