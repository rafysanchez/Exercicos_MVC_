-- ===================================================================  
-- Author:		Carlos Henrique magalhaes
-- Create date: 11/01/2017
-- Description: Procedure para exclusão de itens de reforço de  empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_ITEM_EXCLUIR]   
	@id_item_reforco  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_reforco_item
	WHERE 
		id_item_reforco = @id_item_reforco
  
END