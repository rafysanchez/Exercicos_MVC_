namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudCenarioTipo
    {
        IEnumerable<CenarioTipo> Fetch(CenarioTipo entity);
    }
}
