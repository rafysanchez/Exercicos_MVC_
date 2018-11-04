-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para inclusão de notas para subempenho
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_NOTA_INCLUIR]    
	@tb_subempenho_id_subempenho int = null
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO pagamento.tb_subempenho_nota (
			tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		)
		VALUES (			
			nullif( @tb_subempenho_id_subempenho, 0 )
		,	@cd_nota
		,	@nr_ordem
		)		
           
	COMMIT
    
    SELECT SCOPE_IDENTITY();
END