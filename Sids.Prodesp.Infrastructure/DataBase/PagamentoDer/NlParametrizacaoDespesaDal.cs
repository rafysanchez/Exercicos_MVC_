using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class NlParametrizacaoDespesaDal : ICrudBase<NlParametrizacaoDespesa>
    {
        public int Add(NlParametrizacaoDespesa entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@id_despesa" });

            return DataHelper.Get<int>("[dbo].[PR_DESPESA_INCLUIR]", sqlParameterList);
        }

        public int Edit(NlParametrizacaoDespesa entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_DESPESA_ALTERAR]", sqlParameterList);
        }

        public IEnumerable<NlParametrizacaoDespesa> Fetch(NlParametrizacaoDespesa entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<NlParametrizacaoDespesa>("[dbo].[PR_DESPESA_CONSULTAR]", sqlParameterList);
        }

        public string GetTableName()
        {
            return "tb_despesa";
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_DESPESA_EXCLUIR]", new SqlParameter("@id_despesa", id));
        }
    }
}
