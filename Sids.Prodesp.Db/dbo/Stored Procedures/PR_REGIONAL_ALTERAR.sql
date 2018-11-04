-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de regional na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_REGIONAL_ALTERAR]      
	@id_regional 				int,    
	@cd_uge				VARCHAR(6),
	@ds_regional				VARCHAR(100)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_regional
	     SET cd_uge					= @cd_uge,  
		     ds_regional			= @ds_regional		  
	 WHERE   id_regional			= @id_regional
END