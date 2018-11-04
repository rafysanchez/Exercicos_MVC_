using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class TipoBoletoDal: ICrudTipoBoleto
    {
        public IEnumerable<TipoBoleto> Fatch(TipoBoleto entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<TipoBoleto>("PR_TIPO_BOLETO_CONSULTAR", sqlParameterList);
        }
    }
}
