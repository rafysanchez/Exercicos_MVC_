namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoMesDal : ICrudEmpenhoMes
    {
        public int Edit(EmpenhoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_MES_ALTERAR",
                new SqlParameter("@id_empenho_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_id_empenho", entity.Id),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public IEnumerable<EmpenhoMes> Fetch(EmpenhoMes entity)
        {
            return DataHelper.List<EmpenhoMes>("PR_EMPENHO_MES_CONSULTAR",
                new SqlParameter("@id_empenho_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_id_empenho", entity.Id)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_EMPENHO_MES_EXCLUIR",
                new SqlParameter("@id_empenho_mes", id)
            );
        }

        public int Add(EmpenhoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_MES_INCLUIR",
                new SqlParameter("@tb_empenho_id_empenho", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public string GetTableName()
        {
            return "tb_empenho_mes";
        }
    }
}
