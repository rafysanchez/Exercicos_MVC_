namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EventoTipoDal : ICrudEventoTipo
    {
        public IEnumerable<EventoTipo> Fetch(EventoTipo entity)
        {
            return DataHelper.List<EventoTipo>("PR_EVENTO_TIPO_CONSULTAR",
                new SqlParameter("@id_evento_tipo", entity.Id)
            );
        }
    }
}
