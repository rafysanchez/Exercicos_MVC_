using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ListaCodigoBarrasDal: ICrudListaCodigoBarras
    {
        public int Save(ListaCodigoBarras entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_SALVAR", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_CODIGO_DE_BARRAS_EXCLUIR",
                new SqlParameter("@id_codigo_de_barras", id)
            );
        }

        public IEnumerable<ListaCodigoBarras> Fetch(ListaCodigoBarras entity)
        {
            return DataHelper.List<ListaCodigoBarras>("PR_CODIGO_DE_BARRAS_CONSULTAR",
                new SqlParameter("@id_codigo_de_barras", entity.Id),
                new SqlParameter("@id_lista_de_boletos", entity.ListaBoletosId)
            );
        }
    }
}
