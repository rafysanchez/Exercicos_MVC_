-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria    
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR]     
	@id_cancelamento_movimentacao int = NULL 
	, @tb_fonte_id_fonte varchar(10) = NULL
	, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL
	, @nr_agrupamento int = NULL
	, @nr_seq int = NULL
	, @nr_siafem varchar(15) = NULL
	, @cd_unidade_gestora varchar(10) = NULL
	, @cd_gestao_favorecido varchar(10) = NULL
	, @evento varchar(10) = NULL
	, @nr_categoria_gasto varchar(10) = NULL
	, @eventoNC varchar(10) = NULL 
	, @ds_observacao varchar(77) = NULL
	, @ds_observacao2 varchar(77) = NULL
	, @ds_observacao3 varchar(77) = NULL
	, @fg_transmitido_prodesp char(1) = NULL
	, @ds_msgRetornoProdesp varchar(140) = NULL
	, @fg_transmitido_siafem char(1) = NULL
	, @ds_msgRetornoSiafem varchar(140) = NULL
	, @valor decimal(18,2) = null
as    
begin    
    
 set nocount on;    
    
 if exists (    
  select 1     
  from [movimentacao].[tb_cancelamento_movimentacao] (nolock)    
  where id_cancelamento_movimentacao = @id_cancelamento_movimentacao    
 )    
 begin    
    
	 update [movimentacao].[tb_cancelamento_movimentacao] set    
		[tb_fonte_id_fonte] = @tb_fonte_id_fonte
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
		,[nr_agrupamento] = @nr_agrupamento
		,[nr_seq] = @nr_seq
		,[nr_siafem] = @nr_siafem
		,[cd_unidade_gestora] = @cd_unidade_gestora
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

	where id_cancelamento_movimentacao = @id_cancelamento_movimentacao;  

	select @id_cancelamento_movimentacao;

   end    
 else    
 begin    
    
 INSERT INTO [movimentacao].[tb_cancelamento_movimentacao]
	([tb_fonte_id_fonte]
	,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[nr_siafem]
	,[cd_unidade_gestora]
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
	(@tb_fonte_id_fonte
	,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
	,@nr_agrupamento
	,@nr_seq
	,@nr_siafem
	,@cd_unidade_gestora
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