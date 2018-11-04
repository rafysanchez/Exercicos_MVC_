namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class LicitacaoDal : ICrudLicitacao
    {
        public IEnumerable<Licitacao> Fetch(Licitacao entity)
        {
            return DataHelper.List<Licitacao>("PR_LICITACAO_CONSULTAR",
                new SqlParameter("@id_licitacao", entity.Id),
                new SqlParameter("@ds_licitacao", entity.Descricao)
            );
        }
    }
}
