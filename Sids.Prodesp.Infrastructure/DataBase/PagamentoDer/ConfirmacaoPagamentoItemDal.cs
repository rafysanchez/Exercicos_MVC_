using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Interface.Base;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class ConfirmacaoPagamentoItemDal : ICrudBase<ConfirmacaoPagamentoItem>
    {
        public ConfirmacaoPagamentoItemDal() { }

        public string GetTableName()
        {
            return "tb_confirmacao_pagamento_item";
        }

        public int Add(ConfirmacaoPagamentoItem entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@id_confirmacao_pagamento_item" });
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]", sqlParameterList);
        }

        public int Add(PDExecucaoItem entity)
        {

           var paramOrigem = new SqlParameter("@id_origem", 1);
           var paramIdConfirmacao = new SqlParameter("@id_confirmacao_pagamento", entity.id_confirmacao_pagamento);
           var paramIdItem = new SqlParameter("@id_programacao_desembolso_execucao_item", entity.Codigo);
           var paramIdExecucaoPd = new SqlParameter("@id_execucao_pd", entity.id_execucao_pd);
           var paramDataConfirmacao = new SqlParameter("@dt_confirmacao", entity.Dt_confirmacao.ValidateDBNull());
           var paramNumeroDocumentoGerador = new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador);
           var paramIdTipoDocumento = new SqlParameter("@id_tipo_documento", entity.IdTipoDocumento);
           var paramNumeroDocumento = new SqlParameter("@nr_documento", entity.NumeroDocumento);
           //var param = new SqlParameter("@vr_total_confirmado", entity.Valor);
           var paramCodigoTransmissaoProdesp = new SqlParameter("@cd_transmissao_status_prodesp", entity.cd_transmissao_status_prodesp);
           var paramMensagemProdesp = new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.ds_transmissao_mensagem_prodesp);
           var paramTransmitidoProdesp = new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.fl_transmissao_transmitido_prodesp);
           var paramDataTransmissaoProdesp = new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.dt_transmissao_transmitido_prodesp.ValidateDBNull());

            var retorno = DataHelper.Get<int>("PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR", paramOrigem, paramIdConfirmacao, paramIdItem, paramIdExecucaoPd, 
                paramDataConfirmacao, paramNumeroDocumentoGerador, paramIdTipoDocumento, paramNumeroDocumento, paramCodigoTransmissaoProdesp, paramMensagemProdesp, paramTransmitidoProdesp, paramDataTransmissaoProdesp );

            return retorno;
        }

        public int Add(OBAutorizacaoItem entity)
        {
            return DataHelper.Get<int>("PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR",
            new SqlParameter("@id_origem", 2),
            new SqlParameter("@id_confirmacao_pagamento", entity.id_confirmacao_pagamento),
            new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB),
            new SqlParameter("@id_autorizacao_ob_item", entity.IdAutorizacaoOBItem),
            new SqlParameter("@id_execucao_pd", entity.IdExecucaoPD),
            new SqlParameter("@id_programacao_desembolso_execucao_item", entity.IdExecucaoPDItem),
            new SqlParameter("@id_tipo_documento", entity.IdTipoDocumento),
            new SqlParameter("@nr_documento", entity.NumeroDocumento),
            new SqlParameter("@dt_confirmacao", entity.DataConfirmacaoItem),
            new SqlParameter("@nr_documento_gerador", entity.NumDoctoGerador),
            new SqlParameter("@cd_transmissao_status_prodesp", entity.TransmissaoItemStatusProdesp),
            new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.TransmissaoItemMensagemProdesp),
            new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmissaoItemTransmitidoProdesp),
            new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.TransmissaoItemDataProdesp.ValidateDBNull()));
        }

        public int Edit(PDExecucaoItem entity)
        {
            return DataHelper.Get<int>("PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR",
            new SqlParameter("@id_confirmacao_pagamento", entity.id_confirmacao_pagamento),
            new SqlParameter("@id_programacao_desembolso_execucao_item", entity.Codigo),
            new SqlParameter("@id_execucao_pd", entity.id_execucao_pd),
            new SqlParameter("@dt_confirmacao", entity.Dt_confirmacao),
            new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador),
            new SqlParameter("@id_tipo_documento", entity.IdTipoDocumento),
            new SqlParameter("@nr_documento", entity.NumeroDocumento),
            new SqlParameter("@cd_transmissao_status_prodesp", entity.cd_transmissao_status_prodesp),
            new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.ds_transmissao_mensagem_prodesp),
            new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.fl_transmissao_transmitido_prodesp),
            new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.dt_transmissao_transmitido_prodesp.ValidateDBNull()));

        }

        public int Edit(OBAutorizacaoItem entity)
        {
            return DataHelper.Get<int>("PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR",
            new SqlParameter("@id_confirmacao_pagamento", entity.id_confirmacao_pagamento),
            new SqlParameter("@id_programacao_desembolso_execucao_item", entity.Codigo),
            new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB),
            new SqlParameter("@id_autorizacao_ob_item", entity.IdAutorizacaoOBItem),
            new SqlParameter("@id_execucao_pd", entity.IdExecucaoPD),
            new SqlParameter("@dt_confirmacao", entity.DataConfirmacaoItem.ValidateDBNull()),
            new SqlParameter("@id_tipo_documento", entity.IdTipoDocumento),
            new SqlParameter("@nr_documento", entity.NumeroDocumento),
            new SqlParameter("@cd_transmissao_status_prodesp", entity.TransmissaoItemStatusProdesp),
            new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.TransmissaoItemMensagemProdesp),
            new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmissaoItemTransmitidoProdesp),
            new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.TransmissaoItemDataProdesp.ValidateDBNull()));

        }

        public int RelacionarExecucaoComPagamentoDesdobrado(PDExecucaoItem entity)
        {
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]",
            new SqlParameter("@id_execucao_pd", entity.id_execucao_pd),
            new SqlParameter("@id_programacao_desembolso_execucao_item", entity.Codigo),
            new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador));
        }

        public int RelacionarExecucaoComPagamentoDesdobrado(OBAutorizacaoItem entity)
        {
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]",
            new SqlParameter("@id_autorizacao_ob", entity.IdAutorizacaoOB),
            new SqlParameter("@id_autorizacao_ob_item", entity.IdAutorizacaoOBItem),
            new SqlParameter("@nr_documento_gerador", entity.NumeroDocumentoGerador));
        }

        public int Edit(ConfirmacaoPagamentoItem entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]", sqlParameterList);
        }

        public IEnumerable<ConfirmacaoPagamentoItem> Fetch(ConfirmacaoPagamentoItem entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<ConfirmacaoPagamentoItem>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]", sqlParameterList);
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_EXCLUIR]", new SqlParameter("@id_confirmacao_pagamento_item", id));
        }

        public int EditReclassificacaoRetencao(ConfirmacaoPagamentoItem entity)
        {
            return DataHelper.Get<int>("[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR_ID_RETENCAO_RECLASSIFICACAO]", new SqlParameter("@id_confirmacao_pagamento_item", entity.Id), new SqlParameter("@id_reclassificacao_retencao", entity.IdReclassificacaoRetencao));
        }
    }
}
