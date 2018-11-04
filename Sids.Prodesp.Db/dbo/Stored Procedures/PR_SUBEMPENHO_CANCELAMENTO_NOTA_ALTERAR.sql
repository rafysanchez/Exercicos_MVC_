-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para alteração de notas para subempenho
-- ===================================================================
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_NOTA_ALTERAR]      
	@id_subempenho_cancelamento_nota int = null
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = null
,	@cd_nota varchar(12) = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE pagamento.tb_subempenho_cancelamento_nota
	SET 
		cd_nota = @cd_nota
	 WHERE
		id_subempenho_cancelamento_nota			= @id_subempenho_cancelamento_nota and
		tb_subempenho_cancelamento_id_subempenho_cancelamento	= @tb_subempenho_cancelamento_id_subempenho_cancelamento
END