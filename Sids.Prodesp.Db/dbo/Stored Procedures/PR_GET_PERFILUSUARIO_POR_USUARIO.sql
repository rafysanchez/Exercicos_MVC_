
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Usuario de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_PERFILUSUARIO_POR_USUARIO] 
@id_usuario		INT			= null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		B.id_perfil_usuario
		,A.id_perfil
		,A.ds_perfil
		,B.bl_ativo
		,B.dt_criacao
		,B.id_usuario
	FROM seguranca.tb_perfil(nolock)  A
	INNER JOIN seguranca.tb_perfil_usuario B
	ON A.id_perfil = B.id_perfil 
		AND (B.id_usuario = @id_usuario)
	WHERE A.bl_ativo = 1
END