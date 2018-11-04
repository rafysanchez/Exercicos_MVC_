--CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR]  
--           --@id_cancelamento_movimentacao int =NULL,
--           --@tb_fonte_id_fonte int =NULL,
--           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
--           @nr_agrupamento int =NULL
--           --@nr_seq int =NULL
--           --@nr_nota_cancelamento varchar(10) =NULL,
--           --@cd_unidade_gestora varchar(15) =NULL,
--           --@nr_categoria_gasto varchar(15) =NULL,
--           --@ds_observacao varchar(231) =NULL,
--           --@fg_transmitido_prodesp char(1) =NULL,
--           --@fg_transmitido_siafem char(1)= NULL

		
--AS    
--BEGIN    
-- SET NOCOUNT ON;  



--SELECT d.id_distribuicao_movimentacao as IdDistribuicao
--      ,d.[tb_fonte_id_fonte] as Fonte
--      ,d.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao
--      ,d.[nr_agrupamento] as NrAgrupamento
--      ,d.[nr_seq] as NrSequencia
--      ,d.[nr_nota_distribuicao] as NrNotaDistribuicao
--      ,d.[cd_unidade_gestora_favorecido] as UnidadeGestoraFavorecida
--      ,d.[nr_categoria_gasto] as CategoriaGasto
--      ,d.[ds_observacao] as ObservacaoCancelamento
--	  ,d.[ds_observacao2] as ObservacaoCancelamento2
--	  ,d.[ds_observacao3] as ObservacaoCancelamento3 
--	  ,d.[valor] as ValorTotal
--	  ,d.cd_gestao_favorecido
--	  ,d.eventoNC as EventoNC
--	   ,d.[fg_transmitido_prodesp]   as StatusProdespItem
--      ,d.[ds_msgRetornoProdesp]   as MensagemProdespItem
--      ,d.[fg_transmitido_siafem]   as StatusSiafemItem
--      ,d.[ds_msgRetornoSiafem]  as MensagemSiafemItem

	  
--  FROM [movimentacao].[tb_distribuicao_movimentacao]   D


--  WHERE 
--  (  d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
--  (  d.nr_agrupamento = @nr_agrupamento )    
--  --( nullif( @nr_seq, 0 ) is null or d.nr_seq = @nr_seq )    


--  ORDER BY id_distribuicao_movimentacao,d.nr_seq



--  END