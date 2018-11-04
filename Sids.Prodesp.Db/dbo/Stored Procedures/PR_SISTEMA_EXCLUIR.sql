-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de sistema 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_SISTEMA_EXCLUIR]   
	@id_sistema     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM seguranca.tb_sistema
	 WHERE id_sistema =  @id_sistema;
  
END