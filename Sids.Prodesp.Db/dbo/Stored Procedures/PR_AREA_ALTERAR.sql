-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de area na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_AREA_ALTERAR]      
	@id_area 				int,    
	@ds_area				VARCHAR(100)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_area
	     SET ds_area			= @ds_area		  
	 WHERE   id_area			= @id_area
END