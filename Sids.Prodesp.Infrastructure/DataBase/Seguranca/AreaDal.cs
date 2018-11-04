namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class AreaDal : BaseDal, IArea
    {
        public string GetTableName()
        {
            return "tb_area";
        }

        public IEnumerable<Area> Fetch(Area entity)
        {
            return DataHelper.List<Area>("PR_AREA_CONSULTAR",
                new SqlParameter("@ds_area", entity.Descricao),
                new SqlParameter("@id_area", entity.Id)
            );
        }
    }
}
