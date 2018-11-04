namespace Sids.Prodesp.Model.Interface.Reserva
{
    using Model.Entity.Reserva;
    using System.Collections.Generic;

    public interface ITipoReserva
    {
        IEnumerable<TipoReserva> Fetch(TipoReserva entity);
    }
}
