-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para exclusão de identificacao desdobramento 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IDENTIFICACAO_DESDOBRAMENTO_EXCLUIR]   
	@id_identificacao_desdobramento int = NULL
	,@id_desdobramento int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_identificacao_desdobramento]
	WHERE (nullif( @id_desdobramento, 0 ) is null or id_desdobramento = @id_desdobramento )
        And ( nullif( @id_identificacao_desdobramento, 0 ) is null or id_identificacao_desdobramento = @id_identificacao_desdobramento )

	SELECT @@ROWCOUNT;

END