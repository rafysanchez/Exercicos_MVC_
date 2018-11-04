using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.ValueObject;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class NlParametrizacaoDal : ICrudBase<NlParametrizacao>
    {
        public NlParametrizacaoDal() { }

        public string GetTableName()
        {
            return "tb_nl_parametrizacao";
        }
        public int Add(NlParametrizacao entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@id_nl_parametrizacao" });

            return DataHelper.Get<int>("[dbo].[PR_NL_PARAMETRIZACAO_INCLUIR]", sqlParameterList);
        }
        public int Edit(NlParametrizacao entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_NL_PARAMETRIZACAO_ALTERAR]", sqlParameterList);
        }
        public IEnumerable<NlParametrizacao> Fetch(NlParametrizacao entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@bl_transmitir" });

            return DataHelper.List<NlParametrizacao>("[dbo].[PR_NL_PARAMETRIZACAO_CONSULTAR]", sqlParameterList);
        }
        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_NL_PARAMETRIZACAO_EXCLUIR]", new SqlParameter("@id_nl_parametrizacao", id));
        }

        public ConsultaDespesaNlVo ObterTipoNlDaDespesa(int codigo)
        {
            var paramCodigoTipoDespesa = new SqlParameter("@cd_despesa_tipo", codigo);

            var retornoBd = DataHelper.Get<ConsultaDespesaNlVo>("[dbo].[PR_NL_PARAMETRIZACAO_OBTER_TIPONL_DA_DESPESA]", paramCodigoTipoDespesa);

            return retornoBd;
        }
    }
}
