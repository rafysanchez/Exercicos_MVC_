
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 16/10/2016
-- Description:	Procedure para consulta de Perfil cadastrado
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_GET_PERFIL_POR_USUARIO]
	@id_usuario				int
as
begin
	SET NOCOUNT ON;
	
	select a.id_perfil
		,a.ds_perfil
		,a.ds_detalhe
		,a.bl_ativo
		,a.dt_criacao
		,a.bl_administrador
	from seguranca.tb_perfil(nolock)  a
	join seguranca.tb_perfil_usuario(nolock)  b
	on a.id_perfil = b.id_perfil
	where b.id_usuario = @id_usuario
		and a.bl_ativo = 1

		
end