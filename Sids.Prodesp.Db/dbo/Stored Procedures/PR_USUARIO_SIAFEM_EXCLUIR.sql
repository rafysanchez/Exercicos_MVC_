
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para exclusão de Usuários da base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_USUARIO_SIAFEM_EXCLUIR] 
	@id_usuario		INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	 DELETE FROM seguranca.tb_moq_siafem_usuario
	 WHERE id_usuario = @id_usuario

END