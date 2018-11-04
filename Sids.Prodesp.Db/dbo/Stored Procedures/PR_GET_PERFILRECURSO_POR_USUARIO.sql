

-- ========================================================================
-- Author:		Luis Fernando
-- Create date: 19/10/2016
-- Description:	Procedure que retorna uma lista de Perfil Recurso por Usuário
-- ========================================================================

CREATE PROCEDURE [dbo].[PR_GET_PERFILRECURSO_POR_USUARIO]
	@id_usuario   INT, 
	@id_recurso   INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
		IF OBJECT_ID('tempdb..#perfis') is not null DROP TABLE #perfis
		SELECT id_perfil 
			INTO #perfis
			FROM seguranca.tb_perfil_usuario(nolock) 
		 WHERE id_usuario = @id_usuario

		SELECT DISTINCT
			     A.id_perfil
				,A.id_recurso
                ,A.bl_ativo
				,A.dt_criacao
		  FROM seguranca.tb_perfil_recurso(nolock)  A
	    INNER JOIN #perfis B
			ON B.id_perfil = A.id_perfil
		 WHERE A.bl_ativo = 1
		 AND   (A.id_recurso = @id_recurso OR @id_recurso IS NULL)
		 
END