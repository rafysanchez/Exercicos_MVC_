-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de regionals  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_REGIONAL_EXCLUIR]   
	@id_regional     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM seguranca.tb_regional
	 WHERE id_regional =  @id_regional;
  
END