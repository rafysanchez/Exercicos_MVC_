using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoDal : ICrudProgramacaoDesembolso
    {
        public IEnumerable<ProgramacaoDesembolso> FetchForGrid(ProgramacaoDesembolso entity, DateTime since, DateTime until)
        {
            var paramId = new SqlParameter("@id_programacao_desembolso", entity.Id);
            var paramNumeroSiafem = new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafem);
            var paramNumeroProcesso = new SqlParameter("@nr_processo", entity.NumeroProcesso);
            var paramCodigoAplicacaoObra = new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra);
            var paramStatusSiafem = new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafem);
            var paramNumeroContrato = new SqlParameter("@nr_contrato", entity.NumeroContrato);
            var paramDataVencimento = new SqlParameter("@dt_vencimento", entity.DataVencimento.ValidateDBNull());
            var paramSince = new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull());
            var paramUntil = new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull());
            var paramRegionalId = new SqlParameter("@id_regional", entity.RegionalId);
            var paramCodigoDespesa = new SqlParameter("@cd_despesa", entity.CodigoDespesa);
            var paramDocumentoTipoId = new SqlParameter("@id_tipo_documento", entity.DocumentoTipoId);
            var paramNumeroDocumento = new SqlParameter("@nr_documento", entity.NumeroDocumento);
            var paramProgramacaoDesembolsoTipoId = new SqlParameter("@id_tipo_programacao_desembolso", entity.ProgramacaoDesembolsoTipoId);
            var paramNumeroAgrupamento = new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento);
            var paramBloqueio = new SqlParameter("@bl_bloqueio", entity.Bloqueio);


            return DataHelper.List<ProgramacaoDesembolso>("PR_PROGRAMACAO_DESEMBOLSO_CONSULTA_GRID",
                    paramId, paramNumeroSiafem, paramNumeroProcesso, paramCodigoAplicacaoObra, paramProgramacaoDesembolsoTipoId,
                    paramStatusSiafem,                paramNumeroContrato, paramDataVencimento, paramSince, paramUntil, paramRegionalId,
                    paramNumeroAgrupamento, paramCodigoDespesa, paramDocumentoTipoId, paramNumeroDocumento,paramBloqueio );

        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXCLUIR",
                new SqlParameter("@id_programacao_desembolso", id));
        }

        public IEnumerable<ProgramacaoDesembolso> Fetch(ProgramacaoDesembolso entity)
        {
            return DataHelper.List<ProgramacaoDesembolso>("PR_PROGRAMACAO_DESEMBOLSO_CONSULTAR",
                new SqlParameter("@id_programacao_desembolso", entity.Id),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafem));
        }

        public int Save(ProgramacaoDesembolso entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@cd_transmissao_status_prodesp", "@ds_transmissao_mensagem_prodesp", "@nr_op", "@id_execucao_pd" });

            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_SALVAR", sqlParameterList);
        }

        public ProgramacaoDesembolso Get(int id)
        {
            return DataHelper.Get<ProgramacaoDesembolso>("PR_PROGRAMACAO_DESEMBOLSO_CONSULTAR",
                new SqlParameter("@id_programacao_desembolso", id));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
