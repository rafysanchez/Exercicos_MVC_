
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para exclusão de Usuários da base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_EXCLUIR] 
	@id_usuario		INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	 DELETE FROM seguranca.tb_log_aplicacao
	 WHERE id_usuario = @id_usuario
	
	 DELETE FROM seguranca.tb_perfil_usuario
	 WHERE id_usuario = @id_usuario

	 DELETE FROM seguranca.tb_usuario
	 WHERE id_usuario = @id_usuario

END