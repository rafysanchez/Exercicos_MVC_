
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 19/09/2016
-- Description:	Procedure para consulta de Usuários cadastrados
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_CONSULTAR]
		@id_usuario							INT				= NULL,
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
		@bl_bloqueado						BIT				= NULL,
		@bl_alterar_senha					BIT				= NULL,
		@ds_token	                        VARCHAR(128)    = NULL,
		@ds_nome							VARCHAR(100)	= NULL,
		@ds_cpf								VARCHAR(11)		= NULL,
		@bl_ativo							BIT				= null
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY
    
	SELECT A.[id_usuario]
		  ,A.[id_regional]
		  ,A.[id_sistema]
		  ,A.[id_area]
		  ,A.[ds_email]
		  ,A.[ds_login]
		  ,A.[ds_senha]
		  ,A.[bl_senha_expirada]
		  ,A.[dt_expiracao_senha]
		  ,A.[dt_ultimo_acesso]
		  ,A.[nr_tentativa_login_invalidas]
		  ,A.[bl_bloqueado]
		  ,A.[bl_alterar_senha]
		  ,A.[bl_ativo]
		  ,A.[dt_criacao]
		  ,A.[ds_senha_siafem]
		  ,A.[nr_cpf]
		  ,A.[ds_nome]
		  ,A.[ds_token]
		  ,A.[bl_acesso_siafem]
		  ,A.[bl_senha_siafem_expirada]
		  ,A.[bl_alterar_senha_siafem]
		  ,A.cd_impressora132
		  ,A.cd_impressora80
    FROM seguranca.tb_usuario		A 
	WHERE (@id_usuario = A.id_usuario OR ISNULL(@id_usuario,0) = 0) AND
			(A.ds_email like '%' + @ds_email +'%' OR ISNULL(@ds_email,'')	= '') AND
			(@ds_login = A.ds_login OR ISNULL(@ds_login,'') = '') AND
			(A.ds_nome like '%' + @ds_nome + '%' OR ISNULL(@ds_nome,'') = '') AND
			(@ds_senha = A.ds_senha OR ISNULL(@ds_senha,'')	= '') AND
			(@ds_senha_siafem = A.ds_senha_siafem OR ISNULL(@ds_senha_siafem,'')	= '') AND
			(@bl_senha_expirada = A.bl_senha_expirada OR isnull(@bl_senha_expirada,0) = 0) AND
			(@dt_expiracao_senha = A.dt_expiracao_senha OR	@dt_expiracao_senha IS NULL) AND
			(@dt_ultimo_acesso = A.dt_ultimo_acesso OR @dt_ultimo_acesso IS NULL) AND
			(@bl_bloqueado = A.bl_bloqueado OR ISNULL(@bl_bloqueado,0) = 0) AND
			(@bl_alterar_senha = A.bl_alterar_senha OR ISNULL(@bl_alterar_senha,0) = 0) AND
			(@ds_token = A.ds_token OR ISNULL(@ds_token,'') = '') AND
			(@ds_cpf = A.nr_cpf OR ISNULL(@ds_cpf,'')	= '') AND
			(@bl_ativo = A.bl_ativo OR ISNULL(@bl_ativo,0) = 0) AND
			(@id_regional = A.id_regional OR ISNULL(@id_regional,0) = 0) AND
			(@id_sistema = A.id_sistema OR ISNULL(@id_sistema,0) = 0) AND
			(@id_area = A.id_area OR ISNULL(@id_area,0) = 0)
	ORDER BY A.ds_nome;
	
END