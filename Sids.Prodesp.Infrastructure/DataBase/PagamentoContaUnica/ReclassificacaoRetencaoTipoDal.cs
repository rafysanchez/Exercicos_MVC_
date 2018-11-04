using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoTipoDal : ICrudReclassificacaoRetencaoTipo
    {
        public IEnumerable<ReclassificacaoRetencaoTipo> Fatch(ReclassificacaoRetencaoTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<ReclassificacaoRetencaoTipo>("PR_TIPO_RECLASSIFICACAO_RETENCAO_CONSULTAR", sqlParameterList);
        }
        
    }
}
