-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de area 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_AREA_EXCLUIR]   
	@id_area     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM seguranca.tb_area
	 WHERE id_area =  @id_area;
  
END