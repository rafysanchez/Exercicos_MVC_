
-- ===================================================================
-- Author:		Luis Fernando
-- Alter date: 29/09/2016
-- Description:	Procedure para alteração de Usuários na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_ALTERAR] 
	@id_usuario							INT					= NULL,
		@id_regional						int				= NULL,
		@id_sistema							INT				= NULL,
		@id_area							INT				= NULL,
		@ds_email							VARCHAR(100)	= NULL,
		@ds_login							VARCHAR(20)		= NULL,
		@ds_senha							VARCHAR(64)		= NULL,	
		@ds_senha_siafem					VARCHAR(64)		= NULL,
		@bl_senha_expirada					BIT	            = NULL,
		@dt_expiracao_senha					DATETIME        = NULL,
		@dt_ultimo_acesso					DATETIME		= NULL,
		@nr_tentativa_login_invalidas		INTEGER			= NULL,
		@bl_bloqueado						BIT				= NULL,
		@bl_alterar_senha					BIT				= NULL,
		@ds_token	                        VARCHAR(128)    = NULL,
		@ds_nome							VARCHAR(100)	= NULL,
		@ds_cpf								VARCHAR(11)		= NULL,
		@bl_ativo							BIT				= null,
		@bl_acessa_siafem					BIT				= NULL,
		@bl_senha_siafem_expirada			BIT				= NULL,
		@bl_alterar_senha_siafem			BIT				= NULL,
		@cd_impressora132 varchar(4) = null,
		@cd_impressora80 varchar(4) = null
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION
		UPDATE seguranca.tb_usuario
		   SET [id_regional] = @id_regional
			  ,[id_sistema] = @id_sistema
			  ,[id_area] = @id_area
			  ,[ds_email] = @ds_email
			  ,[ds_login] = @ds_login
			  ,[ds_senha] = @ds_senha
			  ,[bl_senha_expirada] = @bl_senha_expirada
			  ,[dt_expiracao_senha] = @dt_expiracao_senha
			  ,[dt_ultimo_acesso] = @dt_ultimo_acesso
			  ,[nr_tentativa_login_invalidas] = @nr_tentativa_login_invalidas
			  ,[bl_bloqueado] = @bl_bloqueado
			  ,[bl_alterar_senha] = @bl_alterar_senha
			  ,[bl_ativo] = @bl_ativo
			  ,[ds_senha_siafem] = @ds_senha_siafem
			  ,[nr_cpf] = @ds_cpf
			  ,[ds_nome] = @ds_nome
			  ,[ds_token] = @ds_token
			  ,[bl_acesso_siafem] = @bl_acessa_siafem
			  ,[bl_senha_siafem_expirada] = @bl_senha_siafem_expirada
			  ,[bl_alterar_senha_siafem] = @bl_alterar_senha_siafem
			  ,cd_impressora132 = @cd_impressora132
			  ,cd_impressora80 = @cd_impressora80
		 WHERE id_usuario = @id_usuario

	IF
		@@ERROR <> 0
		ROLLBACK
	ELSE
		COMMIT

END