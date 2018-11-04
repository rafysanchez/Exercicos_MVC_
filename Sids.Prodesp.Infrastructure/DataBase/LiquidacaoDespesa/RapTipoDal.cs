using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDER;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    public class RapTipoDal : ICrudBase<RapTipo>
    {
        public RapTipoDal()
        {

        }

        public string GetTableName()
        {
            return "tb_rap_tipo";
        }
        public int Add(RapTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_RAP_TIPO_INCLUIR]", sqlParameterList);
        }
        public int Edit(RapTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_RAP_TIPO_ALTERAR]", sqlParameterList);
        }
        public IEnumerable<RapTipo> Fetch(RapTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<RapTipo>("[dbo].[PR_RAP_TIPO_CONSULTAR]", sqlParameterList);
        }
        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_RAP_TIPO_EXCLUIR]",
                new SqlParameter("@id_rap_tipo", id));
        }
    }
}
