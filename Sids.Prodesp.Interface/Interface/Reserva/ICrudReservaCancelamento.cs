using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;
using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Interface.Interface.Reserva
{
    public interface ICrudReservaCancelamento : ICrudBase<ReservaCancelamento>
    {
        IEnumerable<ReservaCancelamento> BuscarGrid(ReservaCancelamento objModel);
        ReservaCancelamento BuscarAssinaturas(ReservaCancelamento entity);
    }
}
