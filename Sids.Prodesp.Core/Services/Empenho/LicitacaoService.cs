namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class LicitacaoService
    {
        private readonly ICrudLicitacao _crud;

        public LicitacaoService(ILogError l, ICrudLicitacao crud)
        {
            _crud = crud;
        }

        public IEnumerable<Licitacao> Buscar(Licitacao objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
