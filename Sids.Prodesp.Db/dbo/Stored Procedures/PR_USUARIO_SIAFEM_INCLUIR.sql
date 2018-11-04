
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/09/2016
-- Description:	Procedure para inclusão de Usuários na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_SIAFEM_INCLUIR] 
		@ds_login							VARCHAR(20)	,	
		@ds_senha							VARCHAR(64)	,	
		@bl_senha_expirada					BIT	            = 0
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION
					
				
					BEGIN
						INSERT INTO seguranca.tb_moq_siafem_usuario
						([ds_login]
						,[ds_senha]
						,[bl_senha_expirada])
					VALUES
					   (@ds_login
					   ,@ds_senha
					   ,@bl_senha_expirada)

						SELECT SCOPE_IDENTITY() AS id_usuario
					END	
		IF
			@@ERROR <> 0
			ROLLBACK
		ELSE
			COMMIT

END