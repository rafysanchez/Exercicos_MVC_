namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ModalidadeDal : ICrudModalidade
    {
        public IEnumerable<Modalidade> Fetch(Modalidade entity)
        {
            return DataHelper.List<Modalidade>("PR_MODALIDADE_CONSULTAR",
                new SqlParameter("@id_modalidade", entity.Id),
                new SqlParameter("@ds_modalidade", entity.Descricao)
            );
        }
    }
}
