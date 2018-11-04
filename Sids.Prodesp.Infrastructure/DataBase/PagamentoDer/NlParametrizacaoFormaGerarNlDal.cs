using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.FormaGerarNl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class NlParametrizacaoFormaGerarNlDal
    {
        public IEnumerable<FormaGerarNl> Fetch(FormaGerarNl entity)
        {
            var sqlParameterList = new SqlParameter("@id_parametrizacao_forma_gerar_nl", entity.Id);
            return DataHelper.List<FormaGerarNl>("[dbo].[PR_FORMA_GERAR_NL_CONSULTAR]", sqlParameterList);
        }

        public FormaGerarNl GetFormaGerarNlPorTipoDespesa(FormaGerarNl entity)
        {
            return DataHelper.Get<FormaGerarNl>("[dbo].[PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA]", new SqlParameter("@id_despesa_tipo", entity.IdDespesaTipo));
        }
    }
}
