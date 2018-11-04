
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/02/2016
-- Description:	Procedure para inclusão de Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_RECURSO_ACAO_CONSULTAR]
	@id_recurso_acao	INT = null,
	@id_recurso			INT = null,
	@id_acao			INT = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select id_recurso_acao
			,id_recurso
			,id_acao
	FROM seguranca.tb_recurso_acao
	Where (id_recurso_acao = @id_recurso_acao OR isnull(@id_recurso_acao,0) = 0)
	and (id_recurso = @id_recurso OR isnull(@id_recurso,0) = 0)
	and (id_acao = @id_acao OR isnull(@id_acao,0) = 0)



END