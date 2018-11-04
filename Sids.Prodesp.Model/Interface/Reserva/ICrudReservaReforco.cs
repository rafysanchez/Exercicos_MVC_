namespace Sids.Prodesp.Model.Interface.Reserva
{
    using Base;
    using Model.Entity.Reserva;
    using System.Collections.Generic;

    public interface ICrudReservaReforco : ICrudBase<ReservaReforco>
    {
        IEnumerable<Model.Entity.Reserva.ReservaReforco> FetchForGrid(ReservaReforco entity);
        ReservaReforco BuscarAssinaturas(ReservaReforco entity);
    }
}
