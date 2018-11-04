namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class ModalidadeService
    {
        private readonly ICrudModalidade _crud;

        public ModalidadeService(ILogError l, ICrudModalidade crud)
        {
            _crud = crud;
        }

        public IEnumerable<Modalidade> Buscar(Modalidade objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
