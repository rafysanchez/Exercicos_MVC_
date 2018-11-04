using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Helpers;
    using Model.Interface.Reserva;
    using System.Data.SqlClient;

    public class ChaveCicsmoDal: IChaveCicsmo
    {
        public ChaveCicsmo Retrieve(int userId)
        {
            return DataHelper.Get<ChaveCicsmo>("PR_GET_CHAVE",
                new SqlParameter("@id_usuario", userId)
            );
        }

        public int Release(int id)
        {
            return DataHelper.Get<int>("PR_LIBERAR_CHAVE",
                new SqlParameter("@id_chave", id)
            );
        }
    }
}
