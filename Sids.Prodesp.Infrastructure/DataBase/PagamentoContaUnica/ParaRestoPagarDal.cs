using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ParaRestoPagarDal : ICrudRestoPagar
    {
        public IEnumerable<ParaRestoAPagar> Fatch(ParaRestoAPagar entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<ParaRestoAPagar>("PR_RESTO_APAGAR_CONSULTAR", sqlParameterList);
        }
        
    }
}
