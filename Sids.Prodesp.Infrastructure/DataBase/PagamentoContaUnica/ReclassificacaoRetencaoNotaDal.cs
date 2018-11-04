using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoNotaDal : ICrudPagamentoContaUnicaNota<ReclassificacaoRetencaoNota>
    {

        public IEnumerable<ReclassificacaoRetencaoNota> Fetch(ReclassificacaoRetencaoNota entity)
        {
            return DataHelper.List<ReclassificacaoRetencaoNota>("PR_RECLASSIFICACAO_RETENCAO_NOTA_CONSULTAR",
                new SqlParameter("@id_reclassificacao_retencao_nota", entity.Id),
                new SqlParameter("@id_reclassificacao_retencao", entity.IdReclassificacaoRetencao)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_NOTA_EXCLUIR",
                new SqlParameter("@id_reclassificacao_retencao_nota", id)
            );
        }
        
        public int Save(ReclassificacaoRetencaoNota entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_NOTA_SALVAR", sqlParameterList);
        }
    }
}
