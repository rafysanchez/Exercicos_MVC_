
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 30/09/2016
-- Description:	Procedure para consulta de Usuários cadastrados
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_SIAFEM_CONSULTAR]
		@id_usuario							INT				= NULL,
		@ds_login							VARCHAR(20)		= NULL,
		@ds_senha							VARCHAR(64)		= NULL,	
		@bl_senha_expirada					BIT	            = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY
    
	SELECT A.[id_usuario]
		  ,A.[ds_login]
		  ,A.[ds_senha]
		  ,A.[bl_senha_expirada]
    FROM seguranca.tb_moq_siafem_usuario		A 
	WHERE (@id_usuario = A.id_usuario OR ISNULL(@id_usuario,0) = 0) AND
		(@ds_login = A.ds_login OR ISNULL(@ds_login,'') = '') AND
			(@ds_senha = A.ds_senha OR ISNULL(@ds_senha,'')	= '') AND
			(@bl_senha_expirada = A.bl_senha_expirada OR isnull(@bl_senha_expirada,0) = 0)
	ORDER BY A.[ds_login];
	
END