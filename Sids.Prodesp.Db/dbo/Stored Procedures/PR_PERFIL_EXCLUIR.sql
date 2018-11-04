
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 09/09/2016
-- Description:	Procedure para exclusão de Perfil na base de dados
-- ====================================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_EXCLUIR]
	@id_perfil INT
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DELETE from seguranca.tb_perfil_recurso
	 WHERE id_perfil = @id_perfil;
	
	DELETE from seguranca.tb_perfil_acao
	 WHERE id_perfil = @id_perfil;

	DELETE from seguranca.tb_perfil
	 WHERE id_perfil = @id_perfil;
	 
COMMIT