-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017  
-- Description: Procedure para exclusão de notas para subempenho
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_NOTA_EXCLUIR]   
	@id_subempenho_cancelamento_nota int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_cancelamento_nota
	WHERE 
		id_subempenho_cancelamento_nota  = @id_subempenho_cancelamento_nota
  
END