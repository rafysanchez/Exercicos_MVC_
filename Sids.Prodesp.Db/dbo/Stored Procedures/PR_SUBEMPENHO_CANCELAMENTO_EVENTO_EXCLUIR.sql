-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017  
-- Description: Procedure para exclusão de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_EVENTO_EXCLUIR]   
		@id_subempenho_cancelamento_evento int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_cancelamento_evento
	WHERE 
		id_subempenho_cancelamento_evento = @id_subempenho_cancelamento_evento
  
END