
-- ========================================================================
-- Author:		Luis Fernando
-- Create date: 08/09/2016
-- Description:	Procedure que retorna uma lista de Perfil Acão por Usuário
-- ========================================================================

CREATE PROCEDURE [dbo].[PR_GET_ACAO_POR_USUARIO_RECURSO]
	@id_usuario   INT, 
	@id_recurso   INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
		IF OBJECT_ID('tempdb..#perfis') is not null DROP TABLE #perfis
			Create table #perfis(id_perfil int, ds_perfil varchar(100));

		insert into #perfis
		SELECT A.id_perfil
			   ,B.ds_perfil
			FROM seguranca.tb_perfil_usuario(nolock)  A
			INNER JOIN seguranca.tb_perfil(nolock)  B
			ON B.id_perfil = A.id_perfil
		 WHERE id_usuario = @id_usuario
		 and B.bl_ativo = 1;

		SELECT DISTINCT
				D.ds_acao
				,D.id_acao
		  FROM seguranca.tb_perfil_acao(nolock)  A
	    INNER JOIN #perfis B
			ON B.id_perfil = A.id_perfil
		inner join seguranca.tb_recurso_acao(nolock)  C
			on C.id_recurso_acao = A.id_recurso_acao
			and C.id_recurso = @id_recurso
		inner join seguranca.tb_acao(nolock)  D
			on D.id_acao = A.id_acao;
		 
END