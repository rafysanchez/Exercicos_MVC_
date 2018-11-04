
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/09/2016
-- Description:	Procedure para inclusão de Usuários na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_INCLUIR] 
		@id_regional						SMALLINT,
		@id_sistema							SMALLINT = null,
		@id_area							SMALLINT = null,
		@ds_email							VARCHAR(100),	
		@ds_login							VARCHAR(20)	,	
		@ds_senha							VARCHAR(64)	,	
		@ds_senha_siafem					VARCHAR(64)	= null,	
		@bl_senha_expirada					BIT	            = 0,
		@dt_expiracao_senha					SMALLDATETIME        = NULL,
		@dt_ultimo_acesso					SMALLDATETIME		= NULL,
		@nr_tentativa_login_invalidas		int			= 0,
		@bl_bloqueado						BIT				= 0,
		@bl_alterar_senha					BIT				= 0,
		@ds_token	                        VARCHAR(128)    = NULL,
		@ds_nome							VARCHAR(100),
		@ds_cpf								VARCHAR(11),
		@bl_ativo							BIT				= 1,
		@dt_criacao							SMALLDATETIME,
		@id_usuario_criacao					INT,
		@bl_acesso_siafem					BIT,
		@cd_impressora132 varchar(4) = null,
		@cd_impressora80 varchar(4) = null
			
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION
					
				
					BEGIN
						INSERT INTO seguranca.tb_usuario
						([id_regional]
						,[id_sistema]
						,[id_area]
						,[ds_email]
						,[ds_login]
						,[ds_senha]
						,[bl_senha_expirada]
						,[dt_expiracao_senha]
						,[dt_ultimo_acesso]
						,[nr_tentativa_login_invalidas]
						,[bl_bloqueado]
						,[bl_alterar_senha]
						,[bl_ativo]
						,[dt_criacao]
						,[ds_senha_siafem]
						,[nr_cpf]
						,[ds_nome]
						,[ds_token]
						,[bl_acesso_siafem]
						,[bl_senha_siafem_expirada]
						,[bl_alterar_senha_siafem]
						,cd_impressora132
						,cd_impressora80 )
					VALUES
					   (@id_regional
					   ,@id_sistema
					   ,@id_area
					   ,@ds_email
					   ,@ds_login
					   ,@ds_senha
					   ,@bl_senha_expirada
					   ,@dt_expiracao_senha
					   ,@dt_ultimo_acesso
					   ,@nr_tentativa_login_invalidas
					   ,@bl_bloqueado
					   ,@bl_alterar_senha
					   ,@bl_ativo
					   ,@dt_criacao
					   ,@ds_senha_siafem
					   ,@ds_cpf
					   ,@ds_nome
					   ,@ds_token
					   ,@bl_acesso_siafem
					   ,0
					   ,0
					   ,@cd_impressora132
					   ,@cd_impressora80)

						SELECT SCOPE_IDENTITY() AS id_usuario
					END	
		IF
			@@ERROR <> 0
			ROLLBACK
		ELSE
			COMMIT
			
    SELECT @@IDENTITY
END