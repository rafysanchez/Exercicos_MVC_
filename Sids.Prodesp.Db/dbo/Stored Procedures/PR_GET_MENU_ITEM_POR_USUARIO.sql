
-- ========================================================================
-- Author:		Luis Fernando
-- Create date: 05/10/2016
-- Description:	Procedure que retorna uma lista de itens menu disponíveis 
--				para o usuário
-- ========================================================================

CREATE PROCEDURE [dbo].[PR_GET_MENU_ITEM_POR_USUARIO] 
	@id_usuario			int			 = null
as
begin
	SET NOCOUNT ON;
	

	if object_id('tempdb..#perfis') is not null drop table #perfis
		select id_perfil 
		  into #perfis
		  from seguranca.tb_perfil_usuario(nolock) 
		 where (id_usuario = @id_usuario or isnull(@id_usuario,0) = 0)
	
	declare @recursos table
	(	
		id_recurso int,
		ds_url varchar(255),
		id_menu_url int
	)
		insert into @recursos (id_recurso, ds_url,id_menu_url)
		select distinct
			     a.id_recurso,
				 a.ds_url,
				 a.id_menu_url
		from seguranca.tb_recurso(nolock)  a
			left join seguranca.tb_menu(nolock)  b
				on a.id_recurso = b.id_recurso
				and b.bl_ativo = 1
			left join seguranca.tb_menu_item(nolock)  c
				on a.id_recurso = c.id_recurso
				and c.bl_ativo = 1
		 where a.bl_ativo = 1

		 if (isnull(@id_usuario,0) > 0)
		 begin
			 delete @recursos
			 where id_recurso not in 
			 (select distinct id_recurso
			 from seguranca.tb_perfil(nolock)  a
				join seguranca.tb_perfil_usuario(nolock)  b
					on a.id_perfil = b.id_perfil
					and b.bl_ativo = 1
			join seguranca.tb_perfil_acao(nolock)  c
				on a.id_perfil = c.id_perfil
			join seguranca.tb_recurso_acao(nolock)  d
				on d.id_recurso_acao = c.id_recurso_acao
			where a.bl_ativo = 1
				and (@id_usuario = b.id_usuario))
		end

	declare  @retorno table
	(	ds_menu   varchar(255),
		ds_rotulo		varchar(255),
		ds_url	varchar(255),
		id_menu		int,
		ds_url_recurso_menu	varchar(255),
		ordem_menu int,
		ordem_item int,
		id_item int,
		id_recurso int,
		id_menu_url int
	)
	
	insert into @retorno (ds_menu, ds_rotulo, ds_url, id_menu, ds_url_recurso_menu, ordem_menu, ordem_item, id_item, id_recurso,id_menu_url)
	select 
		 a.ds_menu
		,b.ds_rotulo
		,c.ds_url
		,a.id_menu
		,'#'
		,a.nr_ordem, b.nr_ordem, b.id_menu_item,b.id_recurso,c.id_menu_url
	from seguranca.tb_menu(nolock)  a
	left join seguranca.tb_menu_item(nolock)  b
	on a.id_menu = b.id_menu
	left join @recursos c
	on b.id_recurso = c.id_recurso
	where a.bl_ativo = 1 and (b.bl_ativo = 1 or b.bl_ativo is null)
	
	insert into @retorno (ds_menu, ds_rotulo, ds_url, id_menu, ds_url_recurso_menu, ordem_menu, ordem_item, id_item, id_recurso,id_menu_url)
	select b.ds_menu, '', '', b.id_menu, c.ds_url, nr_ordem, 0, 0,c.id_recurso,a.id_menu_url
	from @retorno a
	right join seguranca.tb_menu(nolock)  b 
		on a.id_menu = b.id_menu
	join @recursos c
		on b.id_recurso = c.id_recurso

	update @retorno
	set ds_url_recurso_menu = c.ds_url
	from @retorno a
	join seguranca.tb_menu(nolock)  b 
		on a.id_menu = b.id_menu
	join @recursos c
		on b.id_recurso = c.id_recurso
		
	select a.ds_menu, a.ds_rotulo, a.ds_url, a.id_menu, a.ds_url_recurso_menu, a.ordem_menu, a.ordem_item, a.id_item, a.id_recurso,b.ds_controller,b.ds_area,b.ds_action 
	from @retorno a
	join seguranca.tb_menu_url b
		on b.id_menu_url = a.id_menu_url
	where a.ds_url is not null or a.ds_url_recurso_menu <> '#'
	order by id_item
end