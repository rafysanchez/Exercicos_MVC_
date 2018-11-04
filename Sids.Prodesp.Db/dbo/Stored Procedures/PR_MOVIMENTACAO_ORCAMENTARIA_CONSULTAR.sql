CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR]        
   @id_movimentacao_orcamentaria int =NULL,      
 @nr_agrupamento_movimentacao int= NULL,      
 @nr_siafem varchar(15)= NULL,      
 @tb_regional_id_regional int= NULL,      
 @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao int =NULL,      
 @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int =NULL,      
 @cd_unidade_gestora_emitente varchar(10) =NULL,      
 @cd_gestao_emitente varchar(10) =NULL,      
 @nr_ano_exercicio int= NULL,      
 @fg_transmitido_siafem char(1) =NULL,      
 @dt_cadastro datetime =NULL,      
 @fg_transmitido_prodesp char(1) =NULL      
      
      
AS          
BEGIN          
 SET NOCOUNT ON;        
      
      
      
      
SELECT 
	[id_movimentacao_orcamentaria]      
	,[nr_agrupamento_movimentacao]
	,[nr_siafem]      
	,[tb_regional_id_regional]
	,[tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria]      
	,[cd_unidade_gestora_emitente]      
	,[cd_gestao_emitente]      
	,[nr_ano_exercicio]      
	,[fg_transmitido_siafem]      
	,[bl_transmitido_siafem]      
	,[bl_transmitir_siafem]      
	,[dt_trasmitido_siafem]      
	,[fg_transmitido_prodesp]      
	,[bl_transmitido_prodesp]      
	,[bl_transmitir_prodesp]      
	,[dt_trasmitido_prodesp]      
	,[ds_msgRetornoProdesp]      
	,[ds_msgRetornoSiafem]      
	,[bl_cadastro_completo]      
	,[dt_cadastro]     
	,[tb_programa_id_programa]
	,[tb_fonte_id_fonte] 
	,[tb_estrutura_id_estrutura]
  FROM [movimentacao].[tb_movimentacao_orcamentaria]      
      
  WHERE       
  ( nullif( @id_movimentacao_orcamentaria, 0 ) is null or id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria )   and       
  ( nullif( @nr_agrupamento_movimentacao, 0 ) is null or nr_agrupamento_movimentacao = @nr_agrupamento_movimentacao )   and       
  (@nr_siafem is null or @nr_siafem = @nr_siafem) and      
    ( nullif( @tb_regional_id_regional, 0 ) is null or tb_regional_id_regional = @tb_regional_id_regional )   and       
  ( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and       
  ( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and       
     (@cd_unidade_gestora_emitente is null or cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and      
   (@cd_gestao_emitente is null or cd_gestao_emitente = @cd_gestao_emitente) and      
 ( nullif( @nr_ano_exercicio, 0 ) is null or nr_ano_exercicio = @nr_ano_exercicio )   and       
   (@fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem) and      
  (@fg_transmitido_prodesp is null or fg_transmitido_prodesp = @fg_transmitido_prodesp) and      
  ([dt_cadastro] is null or dt_cadastro = [dt_cadastro] )         
       
      
      
      
      
  ORDER BY id_movimentacao_orcamentaria      
      
      
      
  END