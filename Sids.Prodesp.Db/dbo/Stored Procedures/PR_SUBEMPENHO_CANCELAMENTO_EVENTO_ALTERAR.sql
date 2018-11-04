-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para alteração de itens para subempenho
-- ===================================================================    
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_EVENTO_ALTERAR]      
	@id_subempenho_cancelamento_evento int
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = NULL
,	@cd_fonte varchar(10) = NULL
,	@cd_evento varchar(6) = NULL
,	@cd_classificacao varchar(9) = NULL
,	@ds_inscricao varchar(22) = NULL
,	@vl_evento int = NULL
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE pagamento.tb_subempenho_cancelamento_evento
	SET 
		tb_subempenho_cancelamento_id_subempenho_cancelamento = nullif( @tb_subempenho_cancelamento_id_subempenho_cancelamento, 0 )
	,	cd_fonte = @cd_fonte
	,	cd_evento = @cd_evento
	,	cd_classificacao = @cd_classificacao
	,	ds_inscricao = @ds_inscricao
	,	vl_evento = @vl_evento
	WHERE 
		id_subempenho_cancelamento_evento = @id_subempenho_cancelamento_evento
END