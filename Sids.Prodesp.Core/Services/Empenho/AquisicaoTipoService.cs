namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class AquisicaoTipoService
    {
        private readonly ICrudAquisicaoTipo _crud;

        public AquisicaoTipoService(ILogError l, ICrudAquisicaoTipo crud)
        {
            _crud = crud;
        }

        public IEnumerable<AquisicaoTipo> Buscar(AquisicaoTipo objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
