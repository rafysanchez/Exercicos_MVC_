
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description:	Procedure que retorna registros relacionamentos 
--              Perfil x Recurso de acordo com os filtros informados
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_ACAO_POR_RECURSO]
	@id_recurso			INT			= NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT A.id_acao
		  ,A.ds_acao
	FROM seguranca.tb_acao(nolock)  A
	Join seguranca.tb_recurso_acao(nolock)  B
		On A.id_acao = B.id_acao
	WHERE (B.id_recurso = @id_recurso OR isNull(@id_recurso,0) = 0)
END