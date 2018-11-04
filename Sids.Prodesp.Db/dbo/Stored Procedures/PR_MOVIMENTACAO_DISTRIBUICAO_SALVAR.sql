-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria    
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR]     
	@id_distribuicao_movimentacao int = NULL,
	@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL,
	@nr_agrupamento int = NULL,
	@nr_seq int = NULL,
	@tb_fonte_id_fonte varchar(10) = NULL,
	@nr_siafem varchar(15) = NULL,
	@cd_unidade_gestora_favorecido varchar(10) = NULL,
	@cd_gestao_favorecido varchar(10) = NULL,
	@evento varchar(10) = NULL,
	@nr_categoria_gasto int = NULL,
	@eventoNC varchar(10) = NULL,
	@ds_observacao varchar(77) = NULL,
	@ds_observacao2 varchar(77) = NULL,
	@ds_observacao3 varchar(77) = NULL,
	@fg_transmitido_prodesp char(1) = NULL,
	@ds_msgRetornoProdesp varchar(140) = NULL,
	@fg_transmitido_siafem char(1) = NULL,
	@ds_msgRetornoSiafem varchar(140) = NULL,
	@valor decimal(18,2) = null    
as    
begin    
    
 set nocount on;    
    
 if exists (    
  select 1     
  from [movimentacao].[tb_distribuicao_movimentacao] (nolock)    
  where id_distribuicao_movimentacao = @id_distribuicao_movimentacao    
 )    
 begin    
    
 UPDATE [movimentacao].[tb_distribuicao_movimentacao]
	SET [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
		,[nr_agrupamento] = @nr_agrupamento
		,[nr_seq] = @nr_seq
		,[tb_fonte_id_fonte] = @tb_fonte_id_fonte
		,[nr_siafem] = @nr_siafem
		,[cd_unidade_gestora_favorecido] = @cd_unidade_gestora_favorecido
		,[cd_gestao_favorecido] = @cd_gestao_favorecido
		,[evento] = @evento
		,[nr_categoria_gasto] = @nr_categoria_gasto
		,[eventoNC] = @eventoNC
		,[ds_observacao] = @ds_observacao
		,[ds_observacao2] = @ds_observacao2
		,[ds_observacao3] = @ds_observacao3
		,[fg_transmitido_prodesp] = @fg_transmitido_prodesp
		,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp
		,[fg_transmitido_siafem] = @fg_transmitido_siafem
		,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem
		,[valor] = @valor
		          
    WHERE id_distribuicao_movimentacao = @id_distribuicao_movimentacao;    
    
	select @id_distribuicao_movimentacao;    
        
   end    
 else    
 begin    
    
 INSERT INTO [movimentacao].[tb_distribuicao_movimentacao]
	([tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[tb_fonte_id_fonte]
	,[nr_siafem]
	,[cd_unidade_gestora_favorecido]
	,[cd_gestao_favorecido]
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
	,[valor])
VALUES    
	(@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
	,@nr_agrupamento
	,@nr_seq
	,@tb_fonte_id_fonte
	,@nr_siafem
	,@cd_unidade_gestora_favorecido
	,@cd_gestao_favorecido
	,@evento
	,@nr_categoria_gasto
	,@eventoNC
	,@ds_observacao
	,@ds_observacao2
	,@ds_observacao3
	,'N'
	,@ds_msgRetornoProdesp
	,'N'
	,@ds_msgRetornoSiafem
	,@valor)
    
  select scope_identity();    
    
 end    
    
end