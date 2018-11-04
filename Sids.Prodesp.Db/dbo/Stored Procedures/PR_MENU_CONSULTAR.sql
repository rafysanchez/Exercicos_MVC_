
-- ====================================================================================
-- author:		Luis Fernando
-- create date: 23/10/2016
-- description:	procedure para consulta de menus cadastrados
-- ====================================================================================
CREATE PROCEDURE [dbo].[PR_MENU_CONSULTAR]
	@id_menu			integer		 = null,
	@id_aplicacao		integer		 = null,
	@id_recurso			integer		 = null,
	@ds_menu			varchar(100) = null,
	@nr_ordem			integer		 = null,
	@bl_ativo			bit			 = null
as 
begin
	SET NOCOUNT ON;
	
		select a.id_menu
			,a.id_recurso
			,a.ds_menu
			,a.nr_ordem
			,a.bl_ativo
			,a.dt_criacao		
		from seguranca.tb_menu a
	where (a.id_menu = @id_menu or isnull(@id_menu,0) = 0)
	and (a.id_recurso = @id_recurso or isnull(@id_recurso,0) = 0)
	and (a.ds_menu = @ds_menu or isnull(@ds_menu,'') = '')
	and (a.nr_ordem = @nr_ordem or isnull(@nr_ordem,0) = 0)
	and (a.bl_ativo = @bl_ativo or @bl_ativo is null)
    order by a.ds_menu, a.nr_ordem , a.bl_ativo 

end