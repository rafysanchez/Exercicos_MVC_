
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 16/10/2016
-- Description:	Procedure para consulta de Perfil cadastrado
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_PERFIL_CONSULTAR]
	@id_perfil				int			  = null,
	@ds_perfil				varchar(100)  = null,
	@ds_detalhe				varchar(100)  = null,
	@bl_ativo				bit			  = null
as
begin
	SET NOCOUNT ON;
	select a.id_perfil
		,a.ds_perfil
		,a.ds_detalhe
		,a.bl_ativo
		,a.dt_criacao
		,a.bl_administrador
	from seguranca.tb_perfil a
	where (id_perfil = @id_perfil or isnull(@id_perfil,0) = 0)
		and (a.ds_detalhe like '%' +@ds_detalhe +'%' or isnull(@ds_detalhe,'') = '')
		and (a.ds_perfil like '%' +@ds_perfil +'%' or isnull(@ds_perfil,'') = '')
		and (a.bl_ativo = @bl_ativo or @bl_ativo is null)

		
end