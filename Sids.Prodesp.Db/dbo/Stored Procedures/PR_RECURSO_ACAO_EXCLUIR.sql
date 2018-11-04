
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 19/02/2016
-- Description:	Procedure para inclusão de Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_RECURSO_ACAO_EXCLUIR]
	@id_recurso_acao	INT
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM seguranca.tb_perfil_acao
	where id_recurso_acao = @id_recurso_acao

	DELETE FROM seguranca.tb_recurso_acao
	where id_recurso_acao = @id_recurso_acao

COMMIT