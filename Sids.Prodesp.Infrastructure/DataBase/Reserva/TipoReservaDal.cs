namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Entity.Reserva;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class TipoReservaDal: ITipoReserva
    {
        public IEnumerable<TipoReserva> Fetch(TipoReserva entity)
        {
            return DataHelper.List<TipoReserva>("PR_TIPO_RESERVA_CONSULTAR",
                new SqlParameter("@id_tipo_reserva", entity.Codigo),
                new SqlParameter("@ds_tipo_reserva", entity.Descricao)
            );
        }
    }
}
