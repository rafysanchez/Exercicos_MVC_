

using Sids.Prodesp.Interface.Base;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Shared
{
    public interface ICrudShared : ICrudBase<Model.Entity.Reserva.Reserva>
    {
        IEnumerable<Model.Entity.Reserva.Reserva> BuscarGrid(Model.Entity.Reserva.Reserva objModel);
    }
}
