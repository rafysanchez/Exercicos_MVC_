CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCEL_CONSULTAR]  
           --@id_cancelamento_movimentacao int =NULL,
           --@tb_fonte_id_fonte int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL
           --@nr_seq int =NULL
           --@nr_nota_cancelamento varchar(10) =NULL,
           --@cd_unidade_gestora varchar(15) =NULL,
           --@nr_categoria_gasto varchar(15) =NULL,
           --@ds_observacao varchar(231) =NULL,
           --@fg_transmitido_prodesp char(1) =NULL,
           --@fg_transmitido_siafem char(1)= NULL

		
AS    
BEGIN    
 SET NOCOUNT ON;  



SELECT c.[id_cancelamento_movimentacao] as IdCancelamento
      ,c.[tb_fonte_id_fonte]  as Fonte
      ,c.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao
      ,c.[nr_agrupamento] as NrAgrupamento
      ,c.[nr_seq] as NrSequencia
      ,c.[nr_siafem] as NrSiafem
      ,c.[cd_unidade_gestora] as UnidadeGestoraFavorecida
      ,c.[nr_categoria_gasto] as CategoriaGasto
      ,c.[ds_observacao] as ObservacaoCancelamento
	  ,c.[ds_observacao2] as ObservacaoCancelamento2
	  ,c.[ds_observacao3] as ObservacaoCancelamento3
       ,c.[fg_transmitido_prodesp]   as StatusProdespItem
      ,c.[ds_msgRetornoProdesp]   as MensagemProdespItem
      ,c.[fg_transmitido_siafem]   as StatusSiafemItem
      ,c.[ds_msgRetornoSiafem]  as MensagemSiafemItem
	  ,c.valor as ValorTotal
	  ,c.cd_gestao_favorecido  
	  ,c.eventoNC as EventoNC
	  ,c.cd_gestao_favorecido as IdGestaoFavorecida
	  ,mo.cd_gestao_emitente as IdGestaoEmitente
	  
  FROM [movimentacao].[tb_cancelamento_movimentacao]   c
  JOIN [movimentacao].[tb_movimentacao_orcamentaria] mo ON mo.id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
 

  WHERE 
  ( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  ( @nr_agrupamento is null or c.nr_agrupamento = @nr_agrupamento )    
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_cancelamento_movimentacao,c.nr_seq



  END