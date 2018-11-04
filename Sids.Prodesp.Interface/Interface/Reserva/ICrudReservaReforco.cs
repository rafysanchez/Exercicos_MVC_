using Sids.Prodesp.Interface.Base;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Interface.Interface.Reserva
{
    public interface ICrudReservaReforco : ICrudBase<Model.Entity.Reserva.ReservaReforco>
    {
        IEnumerable<Model.Entity.Reserva.ReservaReforco> BuscarGrid(Model.Entity.Reserva.ReservaReforco objModel);
        ReservaReforco BuscarAssinaturas(ReservaReforco entity);
    }
}
