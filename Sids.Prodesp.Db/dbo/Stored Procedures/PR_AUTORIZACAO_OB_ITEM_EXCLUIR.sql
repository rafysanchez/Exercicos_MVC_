-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 14/08/2018
-- Description: Procedure para exclusão de autorização de pd
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_EXCLUIR]
	@id_autorizacao_ob_item int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_autorizacao_ob_itens]
	WHERE id_autorizacao_ob_item = @id_autorizacao_ob_item

	SELECT @@ROWCOUNT;

END