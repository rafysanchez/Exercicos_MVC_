using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System;
using System.Linq;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Infrastructure.Helpers;
using System.Data.SqlClient;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoExecucaoDal : ICrudProgramacaoDesembolsoExecucao
    {
        public IEnumerable<PDExecucao> Fetch(PDExecucao entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PDExecucao> FetchForGrid(PDExecucao entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public PDExecucao Get(int id)
        {
            var paramId = new SqlParameter("@id_execucao_pd", id);

            var dbResult = DataHelper.Get<PDExecucao>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR", paramId);                 

            return dbResult;
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR",
                 new SqlParameter("@id_execucao_pd", id));
        }

        public int Save(PDExecucao entity)
        {
            var paramIdExecucaoPD = new SqlParameter("@id_execucao_pd", entity.IdExecucaoPD);
            var paramTipoExecucao = new SqlParameter("@id_tipo_execucao_pd", entity.TipoExecucao);
            var paramUgPagadora = new SqlParameter("@ug_pagadora", entity.UgPagadora);
            var paramGestaoPagadora = new SqlParameter("@gestao_pagadora", entity.GestaoPagadora);
            var paramUgLiquidante = new SqlParameter("@ug_liquidante", entity.UgLiquidante);
            var paramGestaoLiquidante = new SqlParameter("@gestao_liquidante", entity.GestaoLiquidante);
            var paramUnidadeGestora = new SqlParameter("@unidade_gestora", entity.UnidadeGestora);
            var paramGestao = new SqlParameter("@gestao", entity.Gestao);
            var paramValor = new SqlParameter("@valor_total", entity.Valor);
            var paramAgrupamento = new SqlParameter("@nr_agrupamento", entity.Agrupamento);
            var paramTransmitirProdesp = new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp);
            var paramTransmitirSiafem = new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem);
            
            #region Confirmacao
            var paramTipoPagamento = new SqlParameter("@id_tipo_pagamento", entity.TipoPagamento);
            var paramEhConfirmacaoPagamento = new SqlParameter("@fl_confirmacao", entity.EhConfirmacaoPagamento);
            var paramDataConfirmacao = new SqlParameter("@dt_confirmacao", entity.DataConfirmacao.ValidateDBNull()); 
            #endregion

            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR",
                paramIdExecucaoPD, paramTipoExecucao, paramUgPagadora, paramGestaoPagadora, paramUgLiquidante, paramGestaoLiquidante,
                paramUnidadeGestora, paramGestao, paramValor, paramAgrupamento, paramTransmitirProdesp, paramTransmitirSiafem,
                paramTipoPagamento, paramEhConfirmacaoPagamento, paramDataConfirmacao);
        }

        public IEnumerable<PDExecucaoTipoExecucao> FetchTiposExecucao()
        {
            return DataHelper.List<PDExecucaoTipoExecucao>("[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_TIPO_EXECUCAO]").AsEnumerable();
        }

        public IEnumerable<PDExecucaoTipoPagamento> FetchTiposPagamento()
        {
            return DataHelper.List<PDExecucaoTipoPagamento>("[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_TIPO_PAGAMENTO]").AsEnumerable();
        }

        public IEnumerable<ProgramacaoDesembolso> Fetch(ProgramacaoDesembolso entity)
        {
            throw new NotImplementedException();
        }

        public ProgramacaoDesembolso Get(string id)
        {
            return DataHelper.Get<ProgramacaoDesembolso>("PR_PROGRAMACAO_DESEMBOLSO_CONSULTAR",
                new SqlParameter("@nr_siafem_siafisico", id));
        }

        public ProgramacaoDesembolsoDadosApoio GetDadosApoio(int tipo, string dsNumPD)
        {
            var dbResult = DataHelper.Get<ProgramacaoDesembolsoDadosApoio>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO",
                new SqlParameter("@tipo ", tipo),
                new SqlParameter("@nr_siafem_siafisico", dsNumPD));

            return dbResult;
        }

        public IEnumerable<ProgramacaoDesembolso> ListDesdobradas(string dsNumPD)
        {
            return DataHelper.List<ProgramacaoDesembolso>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS",
                new SqlParameter("@nr_siafem_siafisico", dsNumPD));
        }

        public int GetNumeroAgrupamento()
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO");
        }

    }
}
