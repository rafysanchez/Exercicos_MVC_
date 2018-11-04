
-- ========================================================================
-- Author:		Cássio Andreatta
-- Create date: 19/10/2016
-- Description:	Procedure que Verifica se há Usuários para determinado Perfil
-- ========================================================================

CREATE PROCEDURE [dbo].[PR_GET_USUARIOS_POR_PERFIL]
	@id_perfil   INT
AS
BEGIN
		
		SET NOCOUNT ON;
		
		SELECT COUNT (id_perfil_usuario) FROM seguranca.tb_perfil_usuario(nolock)  WHERE id_perfil = @id_perfil 
END