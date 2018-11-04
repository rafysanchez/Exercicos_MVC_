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
    public class NlParametrizacaoNlTipoDal : ICrudBase<NlTipo>
    {

        public NlParametrizacaoNlTipoDal() { }

        public string GetTableName()
        {
            return "tb_nl_tipo";
        }

        public int Add(NlTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_NL_TIPO_INCLUIR]", sqlParameterList);
        }

        public int Edit(NlTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_NL_TIPO_ALTERAR]", sqlParameterList);
        }

        public IEnumerable<NlTipo> Fetch(NlTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<NlTipo>("[dbo].[PR_NL_TIPO_CONSULTAR]", sqlParameterList);
        }
        
        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_NL_TIPO_EXCLUIR]", new SqlParameter("@id_despesa_tipo", id));
        }
    }
}
