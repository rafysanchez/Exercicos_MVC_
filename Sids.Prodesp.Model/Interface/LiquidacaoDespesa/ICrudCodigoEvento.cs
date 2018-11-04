namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudCodigoEvento
    {
        IEnumerable<CodigoEvento> Fetch(CodigoEvento entity);
    }
}
