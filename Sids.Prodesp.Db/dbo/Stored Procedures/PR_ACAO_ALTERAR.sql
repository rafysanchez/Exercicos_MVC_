-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de acao na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_ACAO_ALTERAR]      
	@id_acao 				int,    
	@ds_acao				VARCHAR(50)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_acao
	     SET ds_acao			= @ds_acao		  
	 WHERE   id_acao			= @id_acao
END