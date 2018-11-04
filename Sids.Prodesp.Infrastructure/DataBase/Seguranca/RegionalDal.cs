namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class RegionalDal : BaseDal, IRegional
    {
        public string GetTableName()
        {
            return "tb_regional";
        }

        public IEnumerable<Regional> Fetch(Regional entity)
        {
            return DataHelper.List<Regional>("PR_REGIONAL_CONSULTAR",
                new SqlParameter("@ds_regional", entity.Descricao),
                new SqlParameter("@id_regional", entity.Id),
                new SqlParameter("@cd_orgao", entity.Orgao),
                new SqlParameter("@cd_uge", entity.Uge)
            );
        }
    }
}
