namespace Sids.Prodesp.Model.Interface.Reserva
{
    using Base;
    using Model.Entity.Reserva;
    using System.Collections.Generic;

    public interface ICrudReservaCancelamento : ICrudBase<ReservaCancelamento>
    {
        IEnumerable<ReservaCancelamento> FetchForGrid(ReservaCancelamento entity);
        ReservaCancelamento BuscarAssinaturas(ReservaCancelamento entity);
    }
}
