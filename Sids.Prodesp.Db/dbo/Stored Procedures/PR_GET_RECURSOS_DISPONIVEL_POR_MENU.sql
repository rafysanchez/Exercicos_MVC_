
-- ========================================================================    
-- Author:  Luis Fernando
-- Create date: 04/07/2016    
-- Description: Procedure que retorna uma lista de Recursos 
-- disponíveis por Menu
-- ========================================================================    
    
CREATE PROCEDURE [dbo].[PR_GET_RECURSOS_DISPONIVEL_POR_MENU] 
		 @id_menu	   int,
		 @id_menu_item int = null 
as    
begin    
	SET NOCOUNT ON;

  if object_id('tempdb..#temp_recursos') is not null
	 drop table #temp_recursos
  create table #temp_recursos (id_recurso int)

  insert into #temp_recursos(id_recurso)
  select id_recurso
  from seguranca.tb_menu(nolock) 
  where id_menu = @id_menu
	and id_recurso is not null
  
  insert into #temp_recursos(id_recurso)
  select id_recurso
  from seguranca.tb_menu_item(nolock) 
  where id_menu = @id_menu
	and id_recurso is not null
	and id_menu_item <> @id_menu_item
  
  
  select distinct   
    a.id_recurso  
   ,a.no_recurso
   ,a.ds_recurso  
   ,a.ds_keywords  
   ,a.ds_url  
   ,a.bl_publico
   ,a.bl_ativo
   ,a.dt_criacao
  from seguranca.tb_recurso(nolock) a    
  inner join seguranca.tb_menu(nolock)   b    
    on b.id_menu = @id_menu    
  where a.id_recurso not in (select id_recurso from #temp_recursos)
    
end