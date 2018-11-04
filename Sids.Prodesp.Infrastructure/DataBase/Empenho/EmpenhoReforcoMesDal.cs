namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoReforcoMesDal : ICrudEmpenhoReforcoMes
    {
        public int Edit(EmpenhoReforcoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_REFORCO_MES_ALTERAR",
                new SqlParameter("@id_empenho_reforco_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_reforco_id_empenho_reforco", entity.Id),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public IEnumerable<EmpenhoReforcoMes> Fetch(EmpenhoReforcoMes entity)
        {
            return DataHelper.List<EmpenhoReforcoMes>("PR_EMPENHO_REFORCO_MES_CONSULTAR",
                new SqlParameter("@id_empenho_reforco_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_reforco_id_empenho_reforco", entity.Id)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_EMPENHO_REFORCO_MES_EXCLUIR",
                new SqlParameter("@id_empenho_reforco_mes", id)
            );
        }

        public int Add(EmpenhoReforcoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_REFORCO_MES_INCLUIR",
                new SqlParameter("@tb_empenho_reforco_id_empenho_reforco", entity.Id),
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
