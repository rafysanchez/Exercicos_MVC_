namespace Sids.Prodesp.Model.Interface.Reserva
{
    using Base;
    using System.Collections.Generic;

    public interface ICrudReserva : ICrudBase<Model.Entity.Reserva.Reserva>
    {
       IEnumerable<Model.Entity.Reserva.Reserva> FetchForGrid(Model.Entity.Reserva.Reserva entity);
        Model.Entity.Reserva.Reserva BuscarAssinaturas(Model.Entity.Reserva.Reserva entity);
        
    }
              
}
