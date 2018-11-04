namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Entity.Reserva;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ReservaReforcoMesDal: ICrudReservaReforcoMes
    {
        public string GetTableName()
        {
            return "tb_reforco_mes";
        }

        public int Add(ReservaReforcoMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_MES_INCLUIR",
                new SqlParameter("@id_reforco", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public int Edit(ReservaReforcoMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_MES_ALTERAR",
                new SqlParameter("@id_reforco_mes", entity.Codigo),
                new SqlParameter("@id_reforco", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_MES_EXCLUIR",
                new SqlParameter("@id_reforco_mes", id)
            );
        }

        public IEnumerable<ReservaReforcoMes> Fetch(ReservaReforcoMes entity)
        {
            return DataHelper.List<ReservaReforcoMes>("PR_RESERVA_REFORCO_MES_CONSULTAR",
                new SqlParameter("@id_reforco_mes", entity.Codigo),
                new SqlParameter("@id_reforco", entity.Id)
            );
        }
    }
}
