namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SistemaDal : BaseDal, ISistema
    {
        public string GetTableName()
        {
            return "tb_sistema";
        }

        public IEnumerable<Sistema> Fetch(Sistema entity)
        {
            return DataHelper.List<Sistema>("PR_SISTEMA_CONSULTAR",
                new SqlParameter("@ds_sistema", entity.Descricao),
                new SqlParameter("@id_sistema", entity.Id)
            );
        }
    }
}
