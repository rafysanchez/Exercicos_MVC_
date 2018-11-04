namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Entity.Reserva;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ReservaCancelamentoMesDal : ICrudReservaCancelamentoMes
    {
        public string GetTableName()
        {
            return "tb_Cancelamento_mes";
        }

        public int Add(ReservaCancelamentoMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_CANCELAMENTO_MES_INCLUIR",
                new SqlParameter("@id_Cancelamento", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public int Edit(ReservaCancelamentoMes entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_CANCELAMENTO_MES_ALTERAR",
                new SqlParameter("@id_Cancelamento_mes", entity.Codigo),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes),
                new SqlParameter("@id_Cancelamento", entity.Id)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RESERVA_CANCELAMENTO_MES_EXCLUIR",
                new SqlParameter("@id_Cancelamento_mes", id)
            );
        }

        public IEnumerable<ReservaCancelamentoMes> Fetch(ReservaCancelamentoMes entity)
        {
            return DataHelper.List<ReservaCancelamentoMes>("PR_RESERVA_CANCELAMENTO_MES_CONSULTAR",
                new SqlParameter("@id_Cancelamento_mes", entity.Codigo),
                new SqlParameter("@id_Cancelamento", entity.Id)
            );
        }
    }
}
