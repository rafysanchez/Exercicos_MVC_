
-- ========================================================================    
-- Author:  Luis Fernando
-- Create date: 07/10/2016    
-- Description: Procedure que retorna uma lista de Recursos 
-- disponíveis por Usuario
-- ========================================================================    
    
CREATE PROCEDURE [dbo].[PR_GET_RECURSOS_POR_USUARIO] 
		 @id_usuario	   int
as    
begin    
		SET NOCOUNT ON;

		IF OBJECT_ID('tempdb..#perfis') is not null DROP TABLE #perfis
			create table #perfis(id_perfil int, ds_perfil varchar(100));
			
		insert into #perfis
		SELECT A.id_perfil
			   ,B.ds_perfil
			FROM seguranca.tb_perfil_usuario(nolock)  A
			INNER JOIN seguranca.tb_perfil(nolock)  B
			ON B.id_perfil = A.id_perfil
		 WHERE id_usuario = @id_usuario
		 and B.bl_ativo = 1;

		 
		IF OBJECT_ID('tempdb..#temp_recursos') is not null DROP TABLE #temp_recursos
			create table #temp_recursos(id_recurso int);
  
		insert into #temp_recursos
		SELECT DISTINCT
				C.id_recurso
		  FROM seguranca.tb_perfil_acao(nolock)  A
	    INNER JOIN #perfis B
			ON B.id_perfil = A.id_perfil
		inner join seguranca.tb_recurso_acao(nolock)  C
			on C.id_recurso_acao = A.id_recurso_acao
  
  
		select distinct   id_recurso
				,A.no_recurso
				,A.ds_recurso
				,A.ds_keywords
				,A.ds_url
				,A.bl_publico
				,A.bl_ativo
				,A.dt_criacao
		from seguranca.tb_recurso(nolock) 		a    
		where a.id_recurso in (select id_recurso from #temp_recursos)
    
end