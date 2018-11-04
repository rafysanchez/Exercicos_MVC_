
-- ===================================================================
-- Author:		Luis Fernando
-- Alter date: 29/09/2016
-- Description:	Procedure para alteração de Usuários na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_SIAFEM_ALTERAR] 
	@id_usuario							INT					= NULL,
		@ds_login							VARCHAR(20)		= NULL,
		@ds_senha							VARCHAR(64)		= NULL,	
		@bl_senha_expirada					BIT	            = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION
		UPDATE seguranca.tb_moq_siafem_usuario
		   SET [ds_login] = @ds_login
			  ,[ds_senha] = @ds_senha
			  ,[bl_senha_expirada] = @bl_senha_expirada
		 WHERE id_usuario = @id_usuario

	IF
		@@ERROR <> 0
		ROLLBACK
	ELSE
		COMMIT

END