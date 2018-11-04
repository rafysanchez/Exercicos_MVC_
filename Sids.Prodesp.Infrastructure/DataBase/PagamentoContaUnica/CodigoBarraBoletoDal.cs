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
    public class CodigoBarraBoletoDal : ICrudLists<CodigoBarraBoleto>
    {
        public int Save(CodigoBarraBoleto entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_BOLETO_SALVAR", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_BOLETO_EXCLUIR",
                new SqlParameter("@id_codigo_de_barras_boleto", id)
            );
        }

        public IEnumerable<CodigoBarraBoleto> Fetch(CodigoBarraBoleto entity)
        {
            return DataHelper.List<CodigoBarraBoleto>("PR_CODIGO_DE_BARRAS_BOLETO_CONSULTAR",
                new SqlParameter("@id_codigo_de_barras", entity.CodigoBarraId));
        }
    }
}
