
namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoReforco : ICrudBase<EmpenhoReforco>
    {
       IEnumerable<EmpenhoReforco> BuscarGrid(EmpenhoReforco objModel);
        EmpenhoReforco BuscarAssinaturas(EmpenhoReforco objModel);
    }
}



