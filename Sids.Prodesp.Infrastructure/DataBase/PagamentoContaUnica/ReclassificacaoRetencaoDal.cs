using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoDal: ICrudReclassificacaoRetencao
    {
        public IEnumerable<ReclassificacaoRetencao> FetchForGrid(ReclassificacaoRetencao entity, DateTime since, DateTime until)
        {
            return DataHelper.List<ReclassificacaoRetencao>("PR_RECLASSIFICACAO_RETENCAO_CONSULTA_GRID",
                new SqlParameter("@id_reclassificacao_retencao", entity.Id),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafem),
                new SqlParameter("@nr_processo", entity.NumeroProcesso),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
                new SqlParameter("@id_tipo_reclassificacao_retencao", entity.ReclassificacaoRetencaoTipoId),
                new SqlParameter("@ds_normal_estorno", entity.NormalEstorno),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafem),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@cd_origem", entity.Origem),
                new SqlParameter("@cd_agrupamento_confirmacao", entity.AgrupamentoConfirmacao));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_EXCLUIR",
                new SqlParameter("@id_reclassificacao_retencao", id));
        }

        public IEnumerable<ReclassificacaoRetencao> Fetch(ReclassificacaoRetencao entity)
        {
            return DataHelper.List<ReclassificacaoRetencao>("PR_RECLASSIFICACAO_RETENCAO_CONSULTAR",
                new SqlParameter("@id_reclassificacao_retencao", entity.Id),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public int Save(ReclassificacaoRetencao entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.Get<int>("PR_RECLASSIFICACAO_RETENCAO_SALVAR", sqlParameterList);
        }

        public ReclassificacaoRetencao Get(int id)
        {
            return DataHelper.Get<ReclassificacaoRetencao>("PR_RECLASSIFICACAO_RETENCAO_CONSULTAR",
                new SqlParameter("@id_reclassificacao_retencao", id));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
