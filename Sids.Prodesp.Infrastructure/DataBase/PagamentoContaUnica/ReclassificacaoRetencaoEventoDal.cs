using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoEventoDal : ICrudPagamentoContaUnicaEvento<ReclassificacaoRetencaoEvento>
    {

        public IEnumerable<ReclassificacaoRetencaoEvento> Fetch(ReclassificacaoRetencaoEvento entity)
        {
            return DataHelper.List<ReclassificacaoRetencaoEvento>("PR_RECLASSIFICACAO_RETENCAO_EVENTO_CONSULTAR",
                new SqlParameter("@id_reclassificacao_retencao_evento", entity.Id),
                new SqlParameter("@id_reclassificacao_retencao", entity.PagamentoContaUnicaId)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_EVENTO_EXCLUIR",
                new SqlParameter("@id_reclassificacao_retencao_evento", id)
            );
        }
        
        public int Save(ReclassificacaoRetencaoEvento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_EVENTO_SALVAR", sqlParameterList);
        }
    }
}
