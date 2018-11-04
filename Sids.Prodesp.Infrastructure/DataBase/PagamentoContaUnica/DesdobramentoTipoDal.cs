using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class DesdobramentoTipoDal : ICrudDesdobramentoTipo
    {
        public IEnumerable<DesdobramentoTipo> Fatch(DesdobramentoTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<DesdobramentoTipo>("PR_TIPO_DESDOBRAMENTO_CONSULTAR", sqlParameterList);
        }
        
    }
}
