namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudEventoTipo
    {
        IEnumerable<EventoTipo> Fetch(EventoTipo entity);
    }
}
