-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para exclusão de programacao desembolso 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXCLUIR]   
	@id_programacao_desembolso int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  
	DELETE FROM [contaunica].[tb_programacao_desembolso_agrupamento]
	WHERE id_programacao_desembolso = @id_programacao_desembolso;

	DELETE FROM [contaunica].[tb_programacao_desembolso_evento]
	WHERE id_programacao_desembolso = @id_programacao_desembolso;

	DELETE FROM [contaunica].[tb_programacao_desembolso]
	WHERE id_programacao_desembolso = @id_programacao_desembolso;

	SELECT @@ROWCOUNT;

END