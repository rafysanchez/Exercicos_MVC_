using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class PreparacaoPagamentoTipoDal : ICrudPreparacaoPagamentoTipo
    {
        public IEnumerable<PreparacaoPagamentoTipo> Fatch(PreparacaoPagamentoTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<PreparacaoPagamentoTipo>("PR_TIPO_PREPARACAO_PAGAMENTO_CONSULTAR", sqlParameterList);
        }
    }
}
