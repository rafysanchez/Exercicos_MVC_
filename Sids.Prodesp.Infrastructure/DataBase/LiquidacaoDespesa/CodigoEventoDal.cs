namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class CodigoEventoDal : ICrudCodigoEvento
    {
        public IEnumerable<CodigoEvento> Fetch(CodigoEvento entity)
        {
            return DataHelper.List<CodigoEvento>("PR_CODIGO_EVENTO_TIPO_CONSULTAR",
                new SqlParameter("@id_evento_tipo", entity.Id)
            );
        }
    }
}
