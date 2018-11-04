namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudObraTipo
    {
        IEnumerable<ObraTipo> Fetch(ObraTipo entity);
    }
}
