
-- ===================================================================    
-- Author:		Carlos Henrique
-- Create date: 18/10/2016
-- Description: Procedure para alteração de Fontes na base de dados    
-- ===================================================================  

	CREATE PROCEDURE [dbo].[PR_FONTE_ALTERAR]      
	@id_fonte 				int,    
	@cd_fonte				VARCHAR(10),
	@ds_fonte				VARCHAR(45)
		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE configuracao.tb_fonte
	     SET cd_fonte			= @cd_fonte,  
		     ds_fonte			= @ds_fonte		  
	 WHERE   id_fonte			= @id_fonte
END