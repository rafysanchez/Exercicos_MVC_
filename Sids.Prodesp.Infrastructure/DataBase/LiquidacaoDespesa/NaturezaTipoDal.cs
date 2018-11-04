namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class NaturezaTipoDal : ICrudNaturezaTipo
    {
        public IEnumerable<NaturezaTipo> Fetch(NaturezaTipo entity)
        {
            return DataHelper.List<NaturezaTipo>("PR_NATUREZA_TIPO_CONSULTAR",
                new SqlParameter("@id_natureza_tipo", entity.Id));
        }
    }
}
