
-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 18/10/2016  
-- Description: Procedure para exclusão de Fontes da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_FONTE_EXCLUIR]   
	@id_fonte     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM configuracao.tb_fonte
	 WHERE id_fonte =  @id_fonte;
  
END