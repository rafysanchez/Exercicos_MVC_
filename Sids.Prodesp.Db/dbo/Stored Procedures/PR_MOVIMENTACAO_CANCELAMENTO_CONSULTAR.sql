CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR]      
	@id_cancelamento_movimentacao int = NULL,    
	@tb_fonte_id_fonte int = NULL,    
	@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL,    
	@nr_agrupamento int = NULL,    
	@nr_seq int = NULL,    
	@nr_siafem varchar(10) = NULL,    
	@cd_unidade_gestora varchar(15) = NULL,   
	@cd_gestao_favorecido varchar(10) = NULL,  
	@nr_categoria_gasto varchar(15) = NULL,    
	@ds_observacao varchar(231) = NULL,    
	@fg_transmitido_prodesp char(1) = NULL,    
	@fg_transmitido_siafem char(1) = NULL    
      
AS        
BEGIN        
 SET NOCOUNT ON;      

SELECT [id_cancelamento_movimentacao]    
		,[tb_fonte_id_fonte]    
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
		,[nr_agrupamento]    
		,[nr_seq]    
		,[nr_siafem]
		,[valor]   
		,[cd_unidade_gestora]
		,[evento]    
		,[nr_categoria_gasto]
		,[eventoNC]   
		,[ds_observacao]    
		,[ds_observacao2]   
		,[ds_observacao3]   
		,[fg_transmitido_prodesp]    
		,[ds_msgRetornoProdesp]    
		,[fg_transmitido_siafem]    
		,[ds_msgRetornoSiafem]    
		,[cd_gestao_favorecido]
  FROM [movimentacao].[tb_cancelamento_movimentacao]    
    
  WHERE     
	( nullif( @id_cancelamento_movimentacao, 0 ) is null or id_cancelamento_movimentacao = @id_cancelamento_movimentacao )   and     
	( nullif( @tb_fonte_id_fonte, 0) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and     
	( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and
	( nullif( @nr_agrupamento, 0 ) is null or nr_agrupamento = @nr_agrupamento )   and     
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and     
	( @nr_siafem is null or nr_siafem = @nr_siafem  ) and    
	( @cd_unidade_gestora is null or cd_unidade_gestora= @cd_unidade_gestora  ) and    
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and  
	( @nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) and    
	( @ds_observacao is null or ds_observacao= @ds_observacao  ) and    
	( @fg_transmitido_prodesp is null or fg_transmitido_prodesp= @fg_transmitido_prodesp  ) and    
	( @fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem  )     
    
  ORDER BY id_cancelamento_movimentacao,nr_seq

  END