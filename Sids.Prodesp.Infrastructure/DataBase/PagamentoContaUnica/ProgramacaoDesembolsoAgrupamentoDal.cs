using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoAgrupamentoDal: ICrudProgramacaoDesembolsoAgrupamento
    {
        public int Save(ProgramacaoDesembolsoAgrupamento entity)
        {

            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@obs", "@nr_ct", "@nr_ne", "@nr_op" });
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_SALVAR", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_EXCLUIR",
                new SqlParameter("@id_programacao_desembolso_agrupamento", id)
            );
        }

        public IEnumerable<ProgramacaoDesembolsoAgrupamento> Fetch(ProgramacaoDesembolsoAgrupamento entity)
        {
            return DataHelper.List<ProgramacaoDesembolsoAgrupamento>("PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_CONSULTAR",
                new SqlParameter("@id_programacao_desembolso_agrupamento", entity.Id),
                new SqlParameter("@id_programacao_desembolso", entity.PagamentoContaUnicaId),
                new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador),
                new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafem),
                new SqlParameter("@nr_processo", entity.NumeroProcesso),
                new SqlParameter("@id_tipo_documento", entity.DocumentoTipoId),
                new SqlParameter("@nr_documento", entity.NumeroDocumento),
                new SqlParameter("@bl_bloqueio", entity.Bloquear)

            );
        }

        public int GetLastGroup()
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_CONSULTAR_NUMERO");
        }


        public IEnumerable<ProgramacaoDesembolsoAgrupamento> BuscaParametros()
        {
            return DataHelper.List<ProgramacaoDesembolsoAgrupamento>("PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR");
        }


        public IEnumerable<ProgramacaoDesembolsoAgrupamento> FetchBloqueio(ProgramacaoDesembolsoAgrupamento entity)
        {
            return DataHelper.List<ProgramacaoDesembolsoAgrupamento>("PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_BLOQUEIO_CONSULTAR",
                new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento),
                new SqlParameter("@nr_documento", entity.NumeroDocumento)
            );
        }

    }
}
