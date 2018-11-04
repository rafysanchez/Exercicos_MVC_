
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/02/2016
-- Description:	Procedure para inclusão de Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_RECURSO_ACAO_INCLUIR]
	@id_recurso			INT,
	@id_acao			INT
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO seguranca.tb_recurso_acao
		(id_recurso
		,id_acao)
	VALUES
		(@id_recurso
		,@id_acao)

COMMIT


	SELECT SCOPE_IDENTITY()