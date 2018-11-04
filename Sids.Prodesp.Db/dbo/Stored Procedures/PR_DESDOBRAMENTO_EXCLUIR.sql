-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para exclusão de desdobramento 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_DESDOBRAMENTO_EXCLUIR]   
	@id_desdobramento int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_identificacao_desdobramento]
	WHERE id_desdobramento = @id_desdobramento;

	DELETE FROM [contaunica].[tb_desdobramento]
	WHERE id_desdobramento = @id_desdobramento;

	SELECT @@ROWCOUNT;

END