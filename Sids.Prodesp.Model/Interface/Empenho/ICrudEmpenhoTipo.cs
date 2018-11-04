namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoTipo
    {
        IEnumerable<EmpenhoTipo> Fetch(EmpenhoTipo entity);
    }
}
