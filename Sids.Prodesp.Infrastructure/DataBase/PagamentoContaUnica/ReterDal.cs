using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ReterDal: ICrudReter
    {
        public IEnumerable<Reter> Fatch(Reter entity)
        {
            return DataHelper.List<Reter>("PR_RETER_CONSULTAR",
              new SqlParameter("@id_reter", entity.Id),
              new SqlParameter("@ds_reter", entity.Descricao)
          );
        }
    }
}
