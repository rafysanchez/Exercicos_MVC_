using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class CodigoBarraTaxaDal: ICrudLists<CodigoBarraTaxa>
    {
        public int Save(CodigoBarraTaxa entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_TAXAS_SALVAR", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_TAXAS_EXCLUIR",
                new SqlParameter("@id_codigo_de_barras_taxas", id)
            );
        }

        public IEnumerable<CodigoBarraTaxa> Fetch(CodigoBarraTaxa entity)
        {
            return DataHelper.List<CodigoBarraTaxa>("PR_CODIGO_DE_BARRAS_TAXAS_CONSULTAR",
                new SqlParameter("@id_codigo_de_barras_taxas", entity.Id),
                new SqlParameter("@id_codigo_de_barras", entity.CodigoBarraId));
        }
    }
}
