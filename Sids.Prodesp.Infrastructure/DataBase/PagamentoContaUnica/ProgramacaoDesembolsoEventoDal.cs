using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoEventoDal: ICrudPagamentoContaUnicaEvento<ProgramacaoDesembolsoEvento>
    {
        public int Save(ProgramacaoDesembolsoEvento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EVENTO_SALVAR", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EVENTO_EXCLUIR",
                new SqlParameter("@id_programacao_desembolso_evento", id)
            );
        }

        public IEnumerable<ProgramacaoDesembolsoEvento> Fetch(ProgramacaoDesembolsoEvento entity)
        {
            return DataHelper.List<ProgramacaoDesembolsoEvento>("PR_PROGRAMACAO_DESEMBOLSO_EVENTO_CONSULTAR",
                new SqlParameter("@id_programacao_desembolso_evento", entity.Id),
                new SqlParameter("@id_programacao_desembolso", entity.PagamentoContaUnicaId)
            );
        }
    }
}
