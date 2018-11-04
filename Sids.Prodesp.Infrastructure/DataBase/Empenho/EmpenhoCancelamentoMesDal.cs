namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoCancelamentoMesDal : ICrudEmpenhoCancelamentoMes
    {
        public int Edit(EmpenhoCancelamentoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_CANCELAMENTO_MES_ALTERAR",
                new SqlParameter("@id_empenho_cancelamento_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_cancelamento_id_empenho_cancelamento", entity.Id),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public IEnumerable<EmpenhoCancelamentoMes> Fetch(EmpenhoCancelamentoMes entity)
        {
            return DataHelper.List<EmpenhoCancelamentoMes>("PR_EMPENHO_CANCELAMENTO_MES_CONSULTAR",
                new SqlParameter("@id_empenho_cancelamento_mes", entity.Codigo),
                new SqlParameter("@tb_empenho_cancelamento_id_empenho_cancelamento", entity.Id)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_EMPENHO_CANCELAMENTO_MES_EXCLUIR",
                new SqlParameter("@id_empenho_cancelamento_mes", id)
            );
        }

        public int Add(EmpenhoCancelamentoMes entity)
        {
            return DataHelper.Get<int>("PR_EMPENHO_CANCELAMENTO_MES_INCLUIR",
                new SqlParameter("@tb_empenho_cancelamento_id_empenho_cancelamento", entity.Id),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public string GetTableName()
        {
            return "tb_empenho_cancelamento_mes";
        }
    }
}
