namespace Sids.Prodesp.Core.Services.Reserva
{
    using Model.Entity.Reserva;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using System.Collections.Generic;

    public class TipoReservaService
    {
        private readonly ITipoReserva _tipoReserva;

        public TipoReservaService(ILogError l,ITipoReserva tipoReserva)
        {
            _tipoReserva = tipoReserva;
        }

        public IEnumerable<TipoReserva> Buscar(TipoReserva tipo)
        {
            return _tipoReserva.Fetch(tipo);
        }
    }
}
