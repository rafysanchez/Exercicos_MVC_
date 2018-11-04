
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/02/2016
-- Description:	Procedure para inclusão de Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_INCLUIR]
	@ds_perfil				VARCHAR(100),
	@ds_detalhe				VARCHAR(200) = null,
	@bl_ativo				BIT = 1,
	@dt_criacao				DATETIME
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO seguranca.tb_perfil
		(ds_perfil
		,ds_detalhe
		,bl_ativo
		,dt_criacao)
	VALUES
		(@ds_perfil
		,@ds_detalhe
		,@bl_ativo
		,@dt_criacao)

COMMIT
	SELECT SCOPE_IDENTITY();