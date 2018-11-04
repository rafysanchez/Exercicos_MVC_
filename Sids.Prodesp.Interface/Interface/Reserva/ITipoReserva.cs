using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Interface.Interface.Reserva
{
    public interface ITipoReserva
    {
        IEnumerable<TipoReserva> Buscar(TipoReserva tipo);
    }
}
