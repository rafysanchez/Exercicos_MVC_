-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de destino na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_DESTINO_ALTERAR]      
	@id_destino 				int,    
	@cd_destino				CHAR(2),
	@ds_destino				VARCHAR(140)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE configuracao.tb_destino
	     SET cd_destino			= @cd_destino,  
		     ds_destino			= @ds_destino		  
	 WHERE   id_destino			= @id_destino
END