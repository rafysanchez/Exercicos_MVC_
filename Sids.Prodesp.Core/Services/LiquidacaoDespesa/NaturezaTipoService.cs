namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class NaturezaTipoService
    {
        private readonly ICrudNaturezaTipo _repository;


        public NaturezaTipoService(ICrudNaturezaTipo repository)
        {
            _repository = repository;
        }


        public IEnumerable<NaturezaTipo> Buscar(NaturezaTipo entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
