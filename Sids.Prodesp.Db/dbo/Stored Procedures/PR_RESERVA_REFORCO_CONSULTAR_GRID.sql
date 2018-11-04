

-- ==============================================================  
-- Author:  Carlos Henrique Magalhaes  
-- alter date: 28//11/2016
-- carlos henrique  
-- Description: Procedure para preencher resultados do filtro no grid 
-- exec PR_REFORCO_CONSULTAR_GRID 
-- ==============================================================  
  
CREATE PROCEDURE [dbo].[PR_RESERVA_REFORCO_CONSULTAR_GRID]  
 --  @id_reforco      int = null  ,
   @cd_processo     varchar(50) = null  
  ,@nr_reforco_prodesp   varchar(13) = null  
  ,@nr_reforco_siafem_siafisico varchar(11) = null  
  ,@id_regional     smallint = null 
  ,@nr_ano_exercicio  smallint = null 
  ,@id_programa     int = null 
  ,@id_estrutura     int = null  
  ,@ds_status_siafem_siafisico varchar(1) = null
  ,@ds_status_prodesp  varchar(1) = null 
  ,@cd_contrato     varchar(12) = null  
  ,@cd_obra      int = null  
  ,@dt_emissao_reforcoDe   date = null  
  ,@dt_emissao_reforcoAte   date = null 
  
 as  
 begin  
  
 SET NOCOUNT ON;  
  
 SELECT Rf.[id_reforco]  
	   ,Rf.[cd_processo]    
	   ,Rf.[nr_reforco_prodesp]
	   ,Rf.[nr_reforco_siafem_siafisico]  
	   ,Rf.[id_programa] 
	   ,Rf.[id_estrutura]  
       ,Rf.[ds_status_siafem_siafisico]
	   ,Rf.[ds_status_prodesp]
	   ,Rf.[cd_contrato]  
	   ,P.[cd_cfp]
	   ,P.[ds_programa]
	   ,E.[cd_natureza]
	   ,Rf.[cd_origem_recurso]
	   ,Rf.[cd_destino_recurso]
       ,Rf.[bl_transmitir_siafem]
       ,Rf.[bl_transmitir_siafisico]
	   ,Rf.[bl_transmitir_prodesp]
       ,Rf.[fg_transmitido_siafem]
       ,Rf.[fg_transmitido_siafisico]
	   ,Rf.[fg_transmitido_prodesp]
	   ,Rf.bl_cadastro_completo
	   ,Rf.[ds_msgRetornoTransmissaoProdesp] 
	   ,Rf.[ds_msgRetornoTransSiafemSiafisico]

   FROM [reserva].[tb_reforco]  Rf (nolock)
        left join  [configuracao].[tb_programa] P (nolock)
		on Rf.[id_programa] = P.[id_programa]  
		left join  [configuracao].[tb_estrutura] E (nolock)
		on Rf.[id_estrutura] = E.[id_estrutura]
	
   where 
          (Rf.cd_processo like '%'+@cd_processo+'%' OR ISNULL(@cd_processo,'') = '') AND  
		  (@nr_reforco_prodesp = Rf.nr_reforco_prodesp OR ISNULL(@nr_reforco_prodesp,'') = '') AND  
		  (@nr_reforco_siafem_siafisico = Rf.nr_reforco_siafem_siafisico OR ISNULL(@nr_reforco_siafem_siafisico,'') ='') AND  
		  (@id_regional = Rf.id_regional OR ISNULL(@id_regional,0) = 0) AND  
		  (@nr_ano_exercicio = Rf.nr_ano_exercicio OR ISNULL(@nr_ano_exercicio,0) = 0) AND  
		  (@id_programa = Rf.id_programa OR ISNULL(@id_programa,0) = 0) AND  
		  (@id_estrutura = Rf.id_estrutura OR ISNULL(@id_estrutura,0) = 0) AND  
		  (@ds_status_siafem_siafisico = Rf.ds_status_siafem_siafisico OR @ds_status_siafem_siafisico is null) AND
          (@ds_status_prodesp = Rf.ds_status_prodesp OR @ds_status_prodesp is null) AND  
		  (@cd_contrato = Rf.cd_contrato OR ISNULL(@cd_contrato,'') = '') AND  
          (@cd_obra = Rf.cd_obra OR ISNULL(@cd_obra,0) = 0) AND  		
		  (Rf.dt_cadastramento  >= @dt_emissao_reforcoDe or @dt_emissao_reforcoDe is null)  AND
          (Rf.dt_cadastramento  <= @dt_emissao_reforcoAte or @dt_emissao_reforcoAte is null)    
		    
end;