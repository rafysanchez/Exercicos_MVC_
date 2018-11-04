-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_ITEM_EXCLUIR]   
	@id_item  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_item
	WHERE 
		id_item = @id_item
  
END