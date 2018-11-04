
namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ServicoTipoDal : ICrudServicoTipo
    {
        public IEnumerable<ServicoTipo> Fetch(ServicoTipo entity)
        {
            return DataHelper.List<ServicoTipo>("PR_SERVICO_TIPO_CONSULTAR",
                new SqlParameter("@id_servico_tipo", entity.Id)//,
                //new SqlParameter("@cd_rap_tipo", entity.TipoRap)
            );
        }
    }
}
