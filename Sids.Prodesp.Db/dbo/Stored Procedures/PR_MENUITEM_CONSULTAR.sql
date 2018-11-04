
-- ========================================================
-- Author:		Luis Fernando
-- Create date: 16/10/2016
-- Description:	Procedure para consulta de um Item de Menu
-- ========================================================

CREATE PROCEDURE [dbo].[PR_MENUITEM_CONSULTAR]
	@id_menu_item		int				= null,
	@id_menu			int				= null,
	@id_recurso			int				= null,
	@ds_rotulo			varchar(100)	= null,
	@ds_abrir_em			varchar(10)		= null,
	@nr_ordem			int				= null,
	@bl_ativo			bit				= null
as
begin
	SET NOCOUNT ON;
	
	select a.id_menu_item
		,a.id_menu
		,isnull(b.ds_menu,'') ds_menu
		,a.id_recurso
		,isnull(c.ds_recurso,'') ds_recurso
		,a.ds_rotulo
		,a.ds_abrir_em
		,a.nr_ordem
		,a.bl_ativo
		,a.dt_criacao
	from seguranca.tb_menu_item a
	join seguranca.tb_menu b
		on a.id_menu = b.id_menu
	join seguranca.tb_recurso c
		on a.id_recurso = c.id_recurso
	where   (a.id_menu_item = @id_menu_item or isnull(@id_menu_item,0) = 0)
		and (a.id_menu = @id_menu or isnull(@id_menu,0) = 0)
		and (a.id_recurso = @id_recurso or isnull(@id_recurso,0) = 0)
		and (a.ds_rotulo = @ds_rotulo or isnull(@ds_rotulo,'') = '')
		and (a.ds_abrir_em = @ds_abrir_em or isnull(@ds_abrir_em,'') = '')
		and (a.nr_ordem = @nr_ordem or isnull(@nr_ordem,0) = 0)
		and (a.bl_ativo = @bl_ativo or @bl_ativo is null )
	order by a.id_menu, a.nr_ordem, a.id_menu_item
	
end