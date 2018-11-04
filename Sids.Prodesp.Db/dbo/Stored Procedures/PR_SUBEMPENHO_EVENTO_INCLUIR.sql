-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para inclusão de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_EVENTO_INCLUIR]    
	@tb_subempenho_id_subempenho int = NULL
,	@cd_fonte varchar(10) = NULL
,	@cd_evento varchar(6) = NULL
,	@cd_classificacao varchar(9) = NULL
,	@ds_inscricao varchar(22) = NULL
,	@vl_evento int = NULL

AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO pagamento.tb_subempenho_evento (
			tb_subempenho_id_subempenho
		,	cd_fonte
		,	cd_evento
		,	cd_classificacao
		,	ds_inscricao
		,	vl_evento
		)
		VALUES (
			@tb_subempenho_id_subempenho
		,	@cd_fonte
		,	@cd_evento
		,	@cd_classificacao
		,	@ds_inscricao
		,	@vl_evento
		)		
           
	COMMIT
    
    SELECT SCOPE_IDENTITY();
END