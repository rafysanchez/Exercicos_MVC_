namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class AquisicaoTipoDal : ICrudAquisicaoTipo
    {
        public IEnumerable<AquisicaoTipo> Fetch(AquisicaoTipo entity)
        {
            return DataHelper.List<AquisicaoTipo>("PR_AQUISICAO_TIPO_CONSULTAR",
                new SqlParameter("@id_aquisicao_tipo", entity.Id),
                new SqlParameter("@ds_aquisicao_tipo", entity.Descricao)
            );
        }
    }
}
