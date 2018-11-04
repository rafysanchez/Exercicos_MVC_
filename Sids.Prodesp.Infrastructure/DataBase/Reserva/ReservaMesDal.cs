namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Entity.Reserva;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ReservaMesDal: ICrudReservaMes
    {
        public string GetTableName()
        {
            return "tb_reserva_mes";
        }

        public int Add(ReservaMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_MES_INCLUIR",
                new SqlParameter("@id_reserva", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public int Edit(ReservaMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_MES_ALTERAR",
                new SqlParameter("@id_reserva_mes", entity.Codigo),
                new SqlParameter("@id_reserva", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RESERVA_MES_EXCLUIR",
                new SqlParameter("@id_reserva_mes", id)
            );
        }

        public IEnumerable<ReservaMes> Fetch(ReservaMes entity)
        {
            return DataHelper.List<ReservaMes>("PR_RESERVA_MES_CONSULTAR",
                new SqlParameter("@id_reserva_mes", entity.Codigo),
                new SqlParameter("@id_reserva", entity.Id)
            );
        }
    }
}
