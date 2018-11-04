
-- ==========================================================================
-- Author:		Luis Fernando
-- Create date: 30/10/2016
-- Description: Procedure para inclusão do relacionamento Perfil x Usuário
-- ==========================================================================

CREATE PROCEDURE [dbo].[PR_PERFILUSUARIO_INCLUIR]
	@id_perfil					INT,
	@id_usuario					VARCHAR(20),
	@bl_ativo					BIT,
	@dt_criacao					DATETIME
AS    
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from    
	-- interfering with SELECT statements.    
	SET NOCOUNT ON;

	INSERT INTO seguranca.tb_perfil_usuario(
		id_perfil,
		id_usuario,   
		bl_ativo,  
		dt_criacao)  
	VALUES (
		@id_perfil,
		@id_usuario,  
		@bl_ativo,  
		@dt_criacao)  
		
COMMIT

	SELECT SCOPE_IDENTITY();