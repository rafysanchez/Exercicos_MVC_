-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description: Procedure para exclusão de execusao de pd
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR]   
	@id_execucao_pd int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
	WHERE id_execucao_pd = @id_execucao_pd

	DELETE FROM [contaunica].[tb_programacao_desembolso_execucao]
	WHERE id_execucao_pd = @id_execucao_pd

	SELECT @@ROWCOUNT;

END