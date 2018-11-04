
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 16/10/2016
-- Description:	Procedure para consulta de Perfil cadastrado
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_ACAO_CONSULTAR]
	@id_perfil_acao				int			  = null,
	@id_acao					int			  = null,
	@id_perfil					int			  = null,
	@id_recurso					int			  = null
as
begin
	SET NOCOUNT ON;
	
	select a.id_perfil_acao
		,a.id_perfil
		,a.id_acao
		,a.id_recurso_acao
	from seguranca.tb_perfil_acao a
	join seguranca.tb_recurso_acao b
		on a.id_recurso_acao = b.id_recurso_acao
	where (a.id_perfil_acao = @id_perfil_acao or isnull(@id_perfil_acao,0) = 0)
		and (a.id_perfil = @id_perfil or isnull(@id_perfil,0) = 0)
		and (a.id_acao = @id_acao or isnull(@id_acao,0) = 0)
		and (b.id_recurso = @id_recurso or isnull(@id_recurso,0) = 0)
end