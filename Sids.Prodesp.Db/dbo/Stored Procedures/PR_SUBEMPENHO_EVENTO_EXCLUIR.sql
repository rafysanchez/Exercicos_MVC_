-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017  
-- Description: Procedure para exclusão de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_EVENTO_EXCLUIR]   
		@id_subempenho_evento int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_evento
	WHERE 
		id_subempenho_evento = @id_subempenho_evento
  
END