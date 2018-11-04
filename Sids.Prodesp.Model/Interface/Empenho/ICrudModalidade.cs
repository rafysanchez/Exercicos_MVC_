namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudModalidade
    {
        IEnumerable<Modalidade> Fetch(Modalidade entity);
    }
}
