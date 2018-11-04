namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudNaturezaTipo
    {
        IEnumerable<NaturezaTipo> Fetch(NaturezaTipo entity);
    }
}
