-- ===================================================================          
-- Author:  Alessandro de Santanao      
-- Create date: 31/07/2018      
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria      
-- ===================================================================        
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR]       
		@id_nota_credito int = NULL      
		,@tb_programa_id_programa int = NULL      
		,@tb_fonte_id_fonte int = NULL      
		,@tb_estrutura_id_estrutura int = NULL      
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL      
		,@nr_agrupamento int = NULL      
		,@nr_seq int = NULL      
		,@cd_candis char(1) = NULL
		,@nr_siafem varchar(15) = NULL      
		,@cd_unidade_gestora_emitente varchar(15) = NULL     
		,@cd_unidade_gestora_favorecido varchar(15) = NULL     
		,@cd_gestao_favorecido varchar(10) = NULL    
		,@cd_uo varchar(10) = NULL      
		,@cd_ugo varchar(10) = NULL    
		,@plano_interno varchar(10) = NULL      
		,@vr_credito decimal(18,2) = NULL      
		,@ds_observacao varchar(77) = NULL    
		,@ds_observacao2 varchar(77) = NULL      
		,@ds_observacao3 varchar(77) = NULL    
		,@fg_transmitido_prodesp char(1) = NULL      
		,@ds_msgRetornoProdesp varchar(140) = NULL     
		,@fg_transmitido_siafem char(1) = NULL      
		,@ds_msgRetornoSiafem varchar(140) = NULL      
		,@eventoNC varchar(10) = NULL 
		,@fonte_recurso varchar(10) = NULL   
      
      
AS      
BEGIN      
      
 SET NOCOUNT ON;      
      
 IF EXISTS (      
  SELECT 1       
  FROM [movimentacao].[tb_credito_movimentacao] (nolock)      
  WHERE id_nota_credito = @id_nota_credito      
 )      
 begin      
      
 UPDATE [movimentacao].[tb_credito_movimentacao]      
    SET 
		[tb_programa_id_programa] = @tb_programa_id_programa      
		,[tb_fonte_id_fonte] = @tb_fonte_id_fonte      
		,[tb_estrutura_id_estrutura] = @tb_estrutura_id_estrutura      
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria      
		,[nr_agrupamento] = @nr_agrupamento      
		,[nr_seq] = @nr_seq
		,[cd_candis] = @cd_candis
		,[nr_siafem] = @nr_siafem
		,[cd_unidade_gestora_emitente] = @cd_unidade_gestora_emitente   
		,[cd_unidade_gestora_favorecido] = @cd_unidade_gestora_favorecido   
		,[cd_gestao_favorecido] = @cd_gestao_favorecido      
		,[cd_ugo] = @cd_uGo    
		,[cd_uo] = @cd_uo      
		,[plano_interno] = @plano_interno      
		,[vr_credito] = @vr_credito      
		,[ds_observacao] = @ds_observacao      
		,[ds_observacao2] = @ds_observacao2    
		,[ds_observacao3] = @ds_observacao3     
		,[fg_transmitido_prodesp] = @fg_transmitido_prodesp      
		,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp      
		,[fg_transmitido_siafem] = @fg_transmitido_siafem      
		,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem      
		,[eventoNC] = @eventoNC   
		,[fonte_recurso] = @fonte_recurso       
        where id_nota_credito = @id_nota_credito;      
      
  select @id_nota_credito;      
      
      
END      
ELSE      
BEGIN      
      
 INSERT INTO [movimentacao].[tb_credito_movimentacao]      
		([tb_programa_id_programa]      
		,[tb_fonte_id_fonte]      
		,[tb_estrutura_id_estrutura]      
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]      
		,[nr_agrupamento]      
		,[nr_seq]
		,[cd_candis]
		,[nr_siafem]      
		,[cd_unidade_gestora_emitente]  
		,[cd_unidade_gestora_favorecido]    
		,[cd_gestao_favorecido]     
		,[cd_uo]   
		,[cd_ugo]      
		,[plano_interno]      
		,[vr_credito]      
		,[ds_observacao]     
		,[ds_observacao2]      
		,[ds_observacao3]       
		,fg_transmitido_prodesp,      
		ds_msgRetornoProdesp,      
		fg_transmitido_siafem,      
		ds_msgRetornoSiafem,    
		eventoNC,
		fonte_recurso)      
     VALUES      
		(@tb_programa_id_programa      
		,@tb_fonte_id_fonte      
		,@tb_estrutura_id_estrutura      
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria      
		,@nr_agrupamento      
		,@nr_seq
		,@cd_candis    
		,@nr_siafem
		,@cd_unidade_gestora_emitente
		,@cd_unidade_gestora_favorecido   
		,@cd_gestao_favorecido     
		,@cd_uo      
		,@cd_ugo   
		,@plano_interno      
		,@vr_credito      
		,@ds_observacao     
		,@ds_observacao2     
		,@ds_observacao3      
		,'N'      
		,@ds_msgRetornoProdesp      
		,'N'      
		,@ds_msgRetornoSiafem    
		,@eventoNC
		,@fonte_recurso)      
      
      
    
      
      
  select scope_identity();      
      
 end      
      
end