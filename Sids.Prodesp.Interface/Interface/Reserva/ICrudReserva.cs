using Sids.Prodesp.Interface.Base;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Reserva
{
    public interface ICrudReserva : ICrudBase<Model.Entity.Reserva.Reserva>
    {
       IEnumerable<Model.Entity.Reserva.Reserva> BuscarGrid(Model.Entity.Reserva.Reserva objModel);
        Model.Entity.Reserva.Reserva BuscarAssinaturas(Model.Entity.Reserva.Reserva objModel);
        
    }
              
}
