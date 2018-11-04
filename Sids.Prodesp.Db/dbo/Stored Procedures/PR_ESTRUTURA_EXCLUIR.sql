
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 18/10/2016  
-- Description: Procedure para EXCLUIR Estrutura da base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_ESTRUTURA_EXCLUIR]   
	@id_estrutura           int = null
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
	BEGIN TRANSACTION
		DELETE FROM configuracao.tb_estrutura
		 WHERE id_estrutura	= @id_estrutura
	COMMIT;
END