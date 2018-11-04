-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017  
-- Description: Procedure para exclusão de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_ITEM_EXCLUIR]   
	@id_subempenho_cancelamento_item int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_cancelamento_item
	WHERE 
		id_subempenho_cancelamento_item = @id_subempenho_cancelamento_item
  
END