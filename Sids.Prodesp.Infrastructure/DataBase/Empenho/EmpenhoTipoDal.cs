namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoTipoDal : ICrudEmpenhoTipo
    {
        public IEnumerable<EmpenhoTipo> Fetch(EmpenhoTipo entity)
        {
            return DataHelper.List<EmpenhoTipo>("PR_EMPENHO_TIPO_CONSULTAR",
                new SqlParameter("@id_empenho_tipo", entity.Id),
                new SqlParameter("@ds_empenho_tipo", entity.Descricao)
            );
        }
    }
}
