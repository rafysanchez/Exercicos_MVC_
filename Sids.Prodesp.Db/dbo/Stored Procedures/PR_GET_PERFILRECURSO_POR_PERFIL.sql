
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Recurso de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_PERFILRECURSO_POR_PERFIL]
	@id_perfil			INT			= NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	

	SELECT id_perfil_recurso
		,id_perfil
		,A.id_recurso
		,A.no_recurso
		,B.bl_ativo
		,B.dt_criacao
	FROM seguranca.tb_recurso(nolock)  A
	LEFT JOIN seguranca.tb_perfil_recurso(nolock)  B
	on A.id_recurso = B.id_recurso
		AND (id_perfil = @id_perfil OR id_perfil IS NULL)
	WHERE A.bl_ativo = 1
END