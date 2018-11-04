namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudLicitacao
    {
        IEnumerable<Licitacao> Buscar(Licitacao objModel);
    }
}
