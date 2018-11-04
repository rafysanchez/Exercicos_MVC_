
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description: Procedure para alteração de Recursos na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_RECURSO_ALTERAR]      
	@id_recurso				INT,  
	@no_recurso				VARCHAR(100),
	@ds_recurso				VARCHAR(1000)	= NULL,
	@ds_keywords			VARCHAR(1000)	= NULL,
	@ds_url					VARCHAR(2048)	= NULL,
	@bl_publico				BIT				= 1,
	@bl_ativo				BIT				= 1,
	@id_menu_url			INT				= NULL
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_recurso
		  SET no_recurso		= @no_recurso  
		  ,ds_recurso			= @ds_recurso
		  ,ds_keywords			= @ds_keywords
		  ,ds_url				= @ds_url  
		  ,bl_publico			= @bl_publico
		  ,bl_ativo				= @bl_ativo  
		  ,id_menu_url			= @id_menu_url
	 WHERE id_recurso			= @id_recurso
END