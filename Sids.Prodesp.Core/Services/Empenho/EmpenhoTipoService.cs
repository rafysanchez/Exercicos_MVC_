namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class EmpenhoTipoService
    {
        private readonly ICrudEmpenhoTipo _crud;

        public EmpenhoTipoService(ILogError l, ICrudEmpenhoTipo crud)
        {
            _crud = crud;
        }

        public IEnumerable<EmpenhoTipo> Buscar(EmpenhoTipo objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
