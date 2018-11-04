namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;

    public class ServicoTipoService
    {
        private readonly ICrudServicoTipo _repository;


        public ServicoTipoService(ICrudServicoTipo repository)
        {
            _repository = repository;
        }


        public IEnumerable<ServicoTipo> Buscar(ServicoTipo entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
