CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR] 
		@id_nota_credito int = NULL
		,@tb_programa_id_programa int = NULL
		,@tb_fonte_id_fonte int = NULL
		,@tb_estrutura_id_estrutura int = NULL
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL
		,@nr_agrupamento int = NULL
		,@nr_seq int = NULL
		,@cd_candis char(1) = null
		,@nr_siafem VARCHAR(15 )= NULL
		,@cd_unidade_gestora_favorecido varchar(10) = NULL
		,@cd_gestao_favorecido varchar(10) = NULL 
		,@cd_uo VARCHAR(10) = NULL
		,@plano_interno VARCHAR(10) = NULL
AS    
BEGIN    
 SET NOCOUNT ON;  

SELECT [id_nota_credito]
	,[tb_programa_id_programa]
	,[tb_fonte_id_fonte]
	,[tb_estrutura_id_estrutura]
	,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[cd_candis]
	,[nr_siafem]
	,[vr_credito]   
	,[cd_unidade_gestora_emitente]
	,[cd_unidade_gestora_favorecido]
	,[cd_uo]
	,[cd_ugo]
	,[cd_gestao_favorecido]
	,[plano_interno] 
	,[vr_credito]
	,[fonte_recurso]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
	,[eventoNC]
	,[fg_transmitido_prodesp]
	,[ds_msgRetornoProdesp]
	,[fg_transmitido_siafem]
	,[ds_msgRetornoSiafem]
  FROM [movimentacao].[tb_credito_movimentacao]

  WHERE 
	( nullif( @id_nota_credito, 0 ) is null or id_nota_credito = @id_nota_credito )   and 
	( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa )   and 
	( nullif(@tb_fonte_id_fonte, 0) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
	( nullif( @tb_estrutura_id_estrutura, 0 ) is null or tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura )   and 
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
	( nr_agrupamento = @nr_agrupamento )   and 
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
	( @cd_candis is null or cd_candis = @cd_candis )   and 
	( @nr_siafem is null or nr_siafem = @nr_siafem  ) and
	( @cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido  ) and
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	( @cd_uo is null or cd_uo = @cd_uo  ) and
	( @plano_interno is null or plano_interno = @plano_interno  )


  ORDER BY id_nota_credito


  END