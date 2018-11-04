
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 09/09/2016
-- Description:	Procedure para inclusão de Ações Perfil na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_ACAO_EXCLUIR]
	@id_perfil_acao	INT
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM seguranca.tb_perfil_acao
	WHERE id_perfil_acao = @id_perfil_acao

COMMIT