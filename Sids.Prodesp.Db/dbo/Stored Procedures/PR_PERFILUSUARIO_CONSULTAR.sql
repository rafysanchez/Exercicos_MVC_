
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Recurso de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_PERFILUSUARIO_CONSULTAR]
	@id_perfil		INT			= NULL,
	@id_usuario		INT			= NULL,
	@bl_ativo		BIT			= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT A.id_perfil_usuario
		,A.id_perfil
		,B.ds_perfil
		,A.id_usuario
		,A.bl_ativo
		,A.dt_criacao
	FROM seguranca.tb_perfil_usuario A
	JOIN seguranca.tb_perfil		 B
	on A.id_perfil = B.id_perfil
	WHERE (A.id_perfil = @id_perfil OR ISNULL(@id_perfil,0) = 0)
	AND	(A.id_usuario = @id_usuario OR ISNULL(@id_usuario,0) = 0)
	AND	(A.bl_ativo = @bl_ativo OR ISNULL(@bl_ativo,0) = 0)
END