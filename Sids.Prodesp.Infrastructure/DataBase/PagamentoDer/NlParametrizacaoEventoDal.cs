using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class NlParametrizacaoEventoDal : ICrudBase<NlParametrizacaoEvento>
    {

        public string GetTableName()
        {
            return "tb_evento";
        }

        public int Add(NlParametrizacaoEvento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@id_evento" });

            return DataHelper.Get<int>("[dbo].[PR_EVENTO_INCLUIR]", sqlParameterList);
        }
        public int Edit(NlParametrizacaoEvento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_EVENTO_ALTERAR]", sqlParameterList);
        }
        public IEnumerable<NlParametrizacaoEvento> Fetch(NlParametrizacaoEvento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<NlParametrizacaoEvento>("[dbo].[PR_EVENTO_CONSULTAR]", sqlParameterList);
        }
        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_EVENTO_EXCLUIR]", new SqlParameter("@id_evento", id));
        }
    }
}