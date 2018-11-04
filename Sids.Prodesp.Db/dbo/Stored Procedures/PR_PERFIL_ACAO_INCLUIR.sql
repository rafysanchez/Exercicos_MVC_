
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/02/2016
-- Description:	Procedure para inclusão de Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_ACAO_INCLUIR]
	@id_perfil			INT,
	@id_acao			INT = null,
	@id_recurso_acao	INT
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO seguranca.tb_perfil_acao
		(id_perfil
		,id_acao
		,id_recurso_acao)
	VALUES
		(@id_perfil
		,@id_acao
		,@id_recurso_acao)

COMMIT


	SELECT SCOPE_IDENTITY()