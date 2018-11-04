
-- ===================================================================      
-- Author:		Bruno Destro (FI054)      
-- Create date: 23/11/2012      
-- Description: Procedure para inclusão de Recursos na base de dados      
CREATE PROCEDURE [dbo].[PR_RECURSO_INCLUIR]       
	@no_recurso					VARCHAR(100),
	@ds_recurso					VARCHAR(1000)	= NULL,
	@ds_keywords				VARCHAR(1000)	= NULL,
	@ds_url						VARCHAR(2048),
	@bl_publico					BIT				= 1,
	@bl_ativo					BIT             = 1,    
	@dt_criacao					DATETIME		= null,
	@id_menu_url				int				= null
AS    
BEGIN      
	-- SET NOCOUNT ON added to prevent extra result sets from      
	-- interfering with SELECT statements.      
	SET NOCOUNT ON;
	
BEGIN TRANSACTION

	INSERT INTO seguranca.tb_recurso
		(no_recurso
		,ds_recurso
		,ds_keywords
		,ds_url
		,bl_publico
		,bl_ativo
		,dt_criacao
		,id_menu_url )
	VALUES    
		(@no_recurso
		,@ds_recurso
		,@ds_keywords
		,@ds_url
		,@bl_publico
		,@bl_ativo
		,getdate()
		,@id_menu_url )
		
           
COMMIT
    
    SELECT @@IDENTITY
END