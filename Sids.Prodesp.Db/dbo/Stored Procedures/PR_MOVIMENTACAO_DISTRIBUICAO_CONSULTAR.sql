CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR]  
           @id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @tb_fonte_id_fonte int =NULL,
           @nr_siafem VARCHAR(10) =NULL,
           @cd_unidade_gestora_favorecido varchar(10) =NULL,
		   @cd_gestao_favorecido varchar(10) =NULL,  
           @nr_categoria_gasto varchar(10)= NULL
AS    
BEGIN    
 SET NOCOUNT ON;  


 SELECT [id_distribuicao_movimentacao]
	,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[tb_fonte_id_fonte]
	,[nr_siafem]
	,[valor]
	,[cd_unidade_gestora_favorecido]
	,[evento]
	,[nr_categoria_gasto]
	,[eventoNC]
	,[cd_gestao_favorecido]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
	,[fg_transmitido_prodesp]
	,[fg_transmitido_siafem]
	,[ds_msgRetornoSiafem]
  FROM [movimentacao].[tb_distribuicao_movimentacao]
  WHERE 
	( nullif( @id_distribuicao_movimentacao, 0 ) is null or id_distribuicao_movimentacao = @id_distribuicao_movimentacao )   and 
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
	( nr_agrupamento = @nr_agrupamento )   and 
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
	( nullif( @tb_fonte_id_fonte, 0 ) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
	( @nr_siafem is null or nr_siafem = @nr_siafem  ) and
	( @cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido= @cd_unidade_gestora_favorecido  ) and
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	( @nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) 

  ORDER BY id_distribuicao_movimentacao,nr_seq


END