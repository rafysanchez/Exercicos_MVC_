namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class AcaoDal : BaseDal, IAcao
    {
        public IEnumerable<Acao> FetchByFunctionality(Funcionalidade functionality)
        {
            return DataHelper.List<Acao>("PR_GET_ACAO_POR_RECURSO",
                new SqlParameter("@id_recurso", functionality.Codigo)
            );
        }

        public string GetTableName()
        {
            return "tb_acao";
        }

        public IEnumerable<Acao> Fetch(Acao entity)
        {
            return DataHelper.List<Acao>("PR_ACAO_CONSULTAR",
                new SqlParameter("@ds_acao", entity.Descricao),
                new SqlParameter("@id_acao", entity.Id)
            );
        }

        public IEnumerable<Acao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality)
        {
            return DataHelper.List<Acao>("PR_GET_ACAO_POR_USUARIO_RECURSO",
                new SqlParameter("@id_usuario", user.Codigo),
                new SqlParameter("@id_recurso", functionality.Codigo)
            );
        }
    }
}
