namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class MesDal : ICrudMes
    {
        public Mes Get(int id)
        {
            return DataHelper.Get<Mes>("PR_MES_CONSULTAR",
                new SqlParameter("@id_mes", id),
                new SqlParameter("@cd_mes", null),
                new SqlParameter("@ds_mes", null)
            );
        }

        public IEnumerable<Mes> FetchAll()
        {
            return DataHelper.List<Mes>("PR_MES_CONSULTAR",
                new SqlParameter("@id_mes", default(int)),
                new SqlParameter("@cd_mes", null),
                new SqlParameter("@ds_mes", null)
            );
        }
    }
}
