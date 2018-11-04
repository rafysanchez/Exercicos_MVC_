using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System;
using System.Linq;
using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using System.Data.SqlClient;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class AutorizacaoDeOBDal : ICrudAutorizacaoDeOB
    {
        public IEnumerable<OBAutorizacao> Fetch(int id)
        {
            return DataHelper.List<OBAutorizacao>("PR_AUTORIZACAO_DE_OB_CONSULTAR",
                 new SqlParameter("@id_autorizacao_ob", id));
        }

        public OBAutorizacao Get(int id)
        {
            var dbResult = DataHelper.Get<OBAutorizacao>("PR_AUTORIZACAO_DE_OB_CONSULTAR",
                 new SqlParameter("@id_autorizacao_ob", id));

            return dbResult;
        }

        public OBAutorizacao Get(int id, string numOb)
        {
            var paramId = new SqlParameter("@id_autorizacao_ob", id);
            var paramNumOb= new SqlParameter("@ds_numob", numOb);

            var dbResult = DataHelper.Get<OBAutorizacao>("PR_AUTORIZACAO_DE_OB_CONSULTAR", paramId, paramNumOb);

            return dbResult;
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_AUTORIZACAO_DE_OB_EXCLUIR",
                 new SqlParameter("@id_autorizacao_ob", id));
        }

        public int Save(OBAutorizacao entity)
        {
            var paramIdAutorizacaoOB = new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB);
            var paramIdExecucaoPD = new SqlParameter("@id_execucao_pd", entity.IdExecucaoPD);
            var paramNumeroAgrupamento = new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento);
            var paramUnidadeGestora = new SqlParameter("@unidade_gestora", entity.UnidadeGestora);
            var paramAnoOB = new SqlParameter("@ano_ob", entity.AnoOB);
            var paramValor = new SqlParameter("@valor_total_autorizacao", entity.Valor);
            var paramQuantidadeAutorizacao = new SqlParameter("@qtde_autorizacao", entity.QuantidadeAutorizacao);
            var paramTransmissaoStatusSiafem = new SqlParameter("@cd_transmissao_status_siafem", entity.TransmissaoStatusSiafem);
            var TransmissaoMensagemSiafem = new SqlParameter("@ds_transmissao_mensagem_siafem", entity.TransmissaoMensagemSiafem);
            var paramNumeroContrato = new SqlParameter("@nr_contrato", entity.NumeroContrato);
            var paramCodigoAplicacaoObra = new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra);

            var paramGestao = new SqlParameter("@gestao", entity.GestaoPagadora);
            var paramUgPagadora = new SqlParameter("@ug_pagadora", entity.UgPagadora);

            #region Confirmacao
            var paramTipoPagamento = new SqlParameter("@id_tipo_pagamento", entity.TipoPagamento);
            var paramEhConfirmacaoPagamento = new SqlParameter("@fl_confirmacao", entity.EhConfirmacaoPagamento);
            var paramDataConfirmacao = new SqlParameter("@dt_confirmacao", entity.DataConfirmacao.ValidateDBNull());
            #endregion

            var dbResult = DataHelper.Get<int>("PR_AUTORIZACAO_OB_SALVAR", paramIdAutorizacaoOB, paramIdExecucaoPD, paramNumeroAgrupamento
                , paramUnidadeGestora, paramAnoOB, paramValor, paramQuantidadeAutorizacao, paramTransmissaoStatusSiafem, TransmissaoMensagemSiafem
                , paramNumeroContrato, paramCodigoAplicacaoObra, paramGestao, paramUgPagadora
                , paramTipoPagamento, paramEhConfirmacaoPagamento, paramDataConfirmacao);

            return dbResult;
        }
        
        public IEnumerable<PDExecucaoTipoPagamento> FetchTiposPagamento()
        {
            return DataHelper.List<PDExecucaoTipoPagamento>("[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_TIPO_PAGAMENTO]").AsEnumerable();
        }

        public IEnumerable<OBAutorizacao> Fetch(OBAutorizacao entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OBAutorizacao> FetchForGrid(OBAutorizacao entity, DateTime? since, DateTime? until)
        {
            return DataHelper.List<OBAutorizacao>("[dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]",
                new SqlParameter("@ug_pagadora", entity.UgPagadora),
                new SqlParameter("@gestao_pagadora", entity.GestaoPagadora),
                new SqlParameter("@ds_numob", entity.NumOB),
                new SqlParameter("@favorecidoDesc", entity.FavorecidoDesc),
                new SqlParameter("@cd_despesa", entity.CodigoDespesa),
                new SqlParameter("@valor", entity.Valor),
                new SqlParameter("@cd_transmissao_status_siafem", entity.TransmissaoStatusSiafem),
                new SqlParameter("@de", since),
                new SqlParameter("@ate", until),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@nr_contrato", entity.NumeroContrato)
            );
        }

        public IEnumerable<OBAutorizacao> FetchForGrid(OBAutorizacao entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public int GetNumeroAgrupamento()
        {
            return DataHelper.Get<int>("PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO");
        }
    }
}
