namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ObraTipoDal : ICrudObraTipo
    {
        public IEnumerable<ObraTipo> Fetch(ObraTipo entity)
        {
            return DataHelper.List<ObraTipo>("PR_OBRA_TIPO_CONSULTAR",
                new SqlParameter("@id_obra_tipo", entity.Id));
        }
    }
}
