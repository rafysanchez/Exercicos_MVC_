namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudLicitacao
    {
        IEnumerable<Licitacao> Fetch(Licitacao entity);
    }
}
