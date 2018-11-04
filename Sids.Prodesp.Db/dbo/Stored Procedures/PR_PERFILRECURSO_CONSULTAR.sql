
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Recurso de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_PERFILRECURSO_CONSULTAR]
	@id_perfil		INT			= NULL,
	@id_recurso		INT			= NULL,
	@bl_ativo		BIT			= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT id_perfil
		,A.id_recurso
		,B.no_recurso
		,A.bl_ativo
		,A.dt_criacao
	FROM seguranca.tb_perfil_recurso A
	JOIN seguranca.tb_recurso		 B
	on A.id_recurso = B.id_recurso
	WHERE (id_perfil = @id_perfil OR ISNULL(@id_perfil,0) = 0)
	AND	(A.id_recurso = @id_recurso OR ISNULL(@id_recurso,0) = 0)
	AND	(A.bl_ativo = @bl_ativo OR @bl_ativo IS NULL)
END