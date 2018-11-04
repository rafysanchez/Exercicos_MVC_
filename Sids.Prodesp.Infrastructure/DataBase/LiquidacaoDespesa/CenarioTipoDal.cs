namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class CenarioTipoDal : ICrudCenarioTipo
    {
        public IEnumerable<CenarioTipo> Fetch(CenarioTipo entity)
        {
            return DataHelper.List<CenarioTipo>("PR_CENARIO_TIPO_CONSULTAR",
                new SqlParameter("@id_cenario_tipo", entity.Id)
            );
        }
    }
}
