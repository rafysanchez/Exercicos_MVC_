-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de sistema na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_SISTEMA_ALTERAR]      
	@id_sistema 				int,    
	@ds_sistema				VARCHAR(100)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_sistema
	     SET ds_sistema			= @ds_sistema		  
	 WHERE   id_sistema			= @id_sistema
END