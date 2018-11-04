using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Infrastructure.Helpers;
using System.Data.SqlClient;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class AutorizacaoDeOBItemDal : ICrudAutorizacaoDeOBItem
    {
        public IEnumerable<OBAutorizacaoItem> Fetch(OBAutorizacaoItem entity)
         {
            var paramAgrupamento = new SqlParameter("@nr_agrupamento", entity.Agrupamento);
            var paramIdAutorizacaoOB = new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB);
            var paramNumOB = new SqlParameter("@ds_numob", entity.NumOB);

            var dbResult = DataHelper.List<OBAutorizacaoItem>("[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]", paramAgrupamento, paramIdAutorizacaoOB, paramNumOB);

            return dbResult;
        }

        public IEnumerable<OBAutorizacaoItem> Fetch(int id)
        {
            return DataHelper.List<OBAutorizacaoItem>("PR_AUTORIZACAO_DE_OB_CONSULTAR",
                 new SqlParameter("@id_autorizacao_ob", id));
        }

        public IEnumerable<OBAutorizacaoItem> FetchForGrid(OBAutorizacaoItem entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OBAutorizacaoItem> FetchForGrid(OBAutorizacaoItem entity, DateTime? since, DateTime? until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OBAutorizacaoItem> FetchForGrid(OBAutorizacaoItem entity, int? tipoExecucao, DateTime? since, DateTime? until)
        {
            var paramTipo = new SqlParameter("@tipo", 2);
            var paramNumPd = new SqlParameter("@ds_numpd", entity.NumPD);
            var paramNumOb = new SqlParameter("@ds_numob", entity.NumOB);
            var paramObCancelada = new SqlParameter("@ob_cancelada", entity.OBCancelada);
            var paramFavorecidoDesc = new SqlParameter("@favorecidoDesc", entity.FavorecidoDesc);
            var paramExecucao = new SqlParameter("@tipoExecucao", tipoExecucao);
            var paramTransmissaoItemStatusSiafem = new SqlParameter("@cd_transmissao_status_siafem", entity.TransmissaoItemStatusSiafem);
            var paramTransmissaoItemStatusProdesp = new SqlParameter("@cd_transmissao_status_prodesp", entity.TransmissaoItemStatusProdesp);
            var paramSince = new SqlParameter("@de", since);
            var paramUntil = new SqlParameter("@ate", until);
            var paramValorItem = new SqlParameter("@valor", entity.ValorItem);
            var paramCodigoDespesa = new SqlParameter("@cd_despesa", entity.CodigoDespesa);
            var paramGestaoPagadora = new SqlParameter("@gestao_pagadora", entity.GestaoPagadora);
            var paramCodigoAplicacaoObra = new SqlParameter("@filtro_cd_aplicacao_obra", entity.CodigoAplicacaoObra);
            var paramNumeroContrato = new SqlParameter("@nr_contrato", entity.NumeroContrato);
            var paramUgPagadora = new SqlParameter("@ug_pagadora", entity.UGPagadora);

            var dbResult = DataHelper.List<OBAutorizacaoItem>("[dbo].[PR_AUTORIZACAO_ITEM_GRID]", paramTipo, paramNumPd, paramNumOb, paramObCancelada, paramFavorecidoDesc, paramExecucao
                , paramTransmissaoItemStatusSiafem, paramTransmissaoItemStatusProdesp, paramSince, paramUntil, paramValorItem, paramCodigoDespesa
                , paramGestaoPagadora, paramCodigoAplicacaoObra, paramNumeroContrato, paramUgPagadora);

            return dbResult;
        }

        public OBAutorizacaoItem Get(int id)
        {
            var paramId = new SqlParameter("@id_autorizacao_ob", id);

            var dbResult = DataHelper.Get<OBAutorizacaoItem>("PR_AUTORIZACAO_OB_ITEM_CONSULTAR", paramId);

            return dbResult;
        }

        public OBAutorizacaoItem Get(int id, string numOb)
        {
            var paramId = new SqlParameter("@id_autorizacao_ob", id);
            var paramOb = new SqlParameter("@ds_numob", numOb);

            var dbResult = DataHelper.Get<OBAutorizacaoItem>("PR_AUTORIZACAO_OB_ITEM_CONSULTAR", paramId, paramOb);

            return dbResult;
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_AUTORIZACAO_OB_ITEM_EXCLUIR",
            new SqlParameter("@id_autorizacao_ob_item", id));
        }

        public int Save(OBAutorizacaoItem entity)
        {
            var paramIdAutorizacaoOBItem = new SqlParameter("@id_autorizacao_ob_item", entity.IdAutorizacaoOBItem);
            var paramIdAutorizacaoOB = new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB);
            var paramAgrupamentoItemOB = new SqlParameter("@nr_agrupamento_ob", entity.AgrupamentoItemOB);
            var paramIdExecucaoPD = new SqlParameter("@id_execucao_pd", entity.IdExecucaoPD);
            var paramIdExecucaoPDItem = new SqlParameter("@id_execucao_pd_item", entity.IdExecucaoPDItem);
            var paramNumOB = new SqlParameter("@ds_numob", entity.NumOB?.ToUpper());
            var paramNumOP = new SqlParameter("@ds_numop", entity.NumOP);
            var paramNumDoctoGerador = new SqlParameter("@nr_documento_gerador", entity.NumDoctoGerador);
            var paramIdTipoDocumento = new SqlParameter("@id_tipo_documento", entity.IdTipoDocumento);
            var paramNumeroDocumento = new SqlParameter("@nr_documento", entity.NumeroDocumento);
            var paramNumeroContrato = new SqlParameter("@nr_contrato", entity.NumeroContrato);
            var paramFavorecidoDesc = new SqlParameter("@favorecidoDesc", entity.FavorecidoDesc);
            var paramCodigoDespesa = new SqlParameter("@cd_despesa", entity.CodigoDespesa);
            var paramNumeroBanco = new SqlParameter("@nr_banco", entity.NumeroBanco);
            var paramValorItem = new SqlParameter("@valor", entity.ValorItem);
            var paramTransmissaoItemTransmitidoSiafem = new SqlParameter("@fl_transmissao_item_siafem", entity.TransmissaoItemTransmitidoSiafem);
            var paramTransmissaoItemStatusSiafem = new SqlParameter("@cd_transmissao_item_status_siafem", entity.TransmissaoItemStatusSiafem);
            var paramTransmissaoItemMensagemSiafem = new SqlParameter("@ds_transmissao_item_mensagem_siafem", entity.TransmissaoItemMensagemSiafem);
            var paramDataConfirmacaoItem = new SqlParameter("@dt_confirmacao", entity.DataConfirmacaoItem.ValidateDBNull());
            var paramCodigoAplicacaoObra = new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra);

            var dbResult = DataHelper.Get<int>("PR_AUTORIZACAO_OB_ITEM_SALVAR", paramIdAutorizacaoOBItem, paramIdAutorizacaoOB, paramAgrupamentoItemOB, paramIdExecucaoPD, paramIdExecucaoPDItem,
                paramNumOB, paramNumOP, paramNumDoctoGerador, paramIdTipoDocumento, paramNumeroDocumento, paramNumeroContrato, paramFavorecidoDesc, paramCodigoDespesa, paramNumeroBanco, paramValorItem,
                paramTransmissaoItemTransmitidoSiafem, paramTransmissaoItemStatusSiafem, paramTransmissaoItemMensagemSiafem, paramDataConfirmacaoItem, paramCodigoAplicacaoObra);

            return dbResult;
        }

        public OBAutorizacaoItem GetDadosApoio(int tipo, string dsNumPD)
        {
            return DataHelper.Get<OBAutorizacaoItem>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO",
                new SqlParameter("@tipo", tipo),
                new SqlParameter("@nr_siafem_siafisico", dsNumPD));
        }

        public int DeletarNaoAgrupados(int idAutorizacaoOB)
        {
            return DataHelper.Get<int>("PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS",
            new SqlParameter("@id_autorizacao_ob", idAutorizacaoOB));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
