
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Recurso de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_PERFILACAO_POR_PERFIL]
	@id_perfil			INT			= NULL
AS
BEGIN
	SET NOCOUNT ON;
	

	SELECT id_perfil_acao
		,id_perfil
		,A.id_recurso_acao
		,A.id_acao
	FROM seguranca.tb_perfil_acao(nolock)  A
	WHERE (id_perfil = @id_perfil OR isNull(@id_perfil,0) = 0)
END