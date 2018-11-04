namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoReforco : ICrudBase<EmpenhoReforco>
    {
       IEnumerable<EmpenhoReforco> FetchForGrid(EmpenhoReforco entity);
        EmpenhoReforco BuscarAssinaturas(EmpenhoReforco objModel);
    }
}