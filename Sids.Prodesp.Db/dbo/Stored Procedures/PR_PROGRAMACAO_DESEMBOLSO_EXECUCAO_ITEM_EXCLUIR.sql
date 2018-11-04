-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description: Procedure para exclusão de execusao de item pd
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR]   
	@id_programacao_desembolso_execucao_item int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
	WHERE id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

	SELECT @@ROWCOUNT;

END