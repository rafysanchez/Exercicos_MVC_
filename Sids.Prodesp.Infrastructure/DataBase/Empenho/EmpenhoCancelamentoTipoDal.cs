namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoCancelamentoTipoDal : ICrudEmpenhoCancelamentoTipo
    {
        public IEnumerable<EmpenhoCancelamentoTipo> Fetch(EmpenhoCancelamentoTipo entity)
        {
            return DataHelper.List<EmpenhoCancelamentoTipo>("PR_EMPENHO_CANCELAMENTO_TIPO_CONSULTAR",
                new SqlParameter("@id_empenho_cancelamento_tipo", entity.Id)
            );
        }
    }
}
