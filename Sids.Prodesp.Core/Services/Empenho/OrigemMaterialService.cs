namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class OrigemMaterialService
    {
        private readonly ICrudOrigemMaterial _crud;

        public OrigemMaterialService(ILogError l, ICrudOrigemMaterial crud)
        {
            _crud = crud;
        }

        public IEnumerable<OrigemMaterial> Buscar(OrigemMaterial objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
