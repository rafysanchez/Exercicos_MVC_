  
-- ==============================================================  
-- Author:  Carlos Henrique Magalhaes  
-- alter date: 09/11/2016
-- carlos henrique  
-- Description: Procedure para preencher resultados do filtro no grid 
-- exec PR_RESERVA_CONSULTAR_GRID @cd_contrato
-- ==============================================================  
  
CREATE PROCEDURE [dbo].[PR_RESERVA_CONSULTAR_GRID]  
   @id_reserva      int = null  
  ,@cd_contrato     varchar(12) = null  
  ,@cd_processo     varchar(50) = null  
  ,@nr_reserva_prodesp   varchar(9) = null  
  ,@nr_reserva_siafem_siafisico varchar(11) = null  
  ,@cd_obra      int = null  
  ,@id_tipo_reserva    int = null  
  ,@id_regional     smallint = null 
  ,@nr_ano_exercicio  smallint = null 
  ,@id_programa     int = null  
  ,@id_estrutura     int = null  
  ,@fg_transmitido_siafem   varchar(1) = null  
  ,@fg_transmitido_prodesp  varchar(1) = null  
  ,@cd_ugo      varchar(6) = null  
  ,@dt_emissao_reservaDe   date = null  
  ,@dt_emissao_reservaAte   date = null 
  
  
as  
begin  
  
 SET NOCOUNT ON;  
  
 SELECT R.[id_reserva]  
	   ,R.[id_estrutura]  
       ,R.[id_programa] 
       ,R.[nr_reserva_siafem_siafisico]  
	   ,R.[nr_reserva_prodesp]  
	   ,R.[cd_contrato]  
	   ,R.[cd_processo] 
	   ,P.[cd_cfp]
	   ,P.[ds_programa]
	   ,E.[cd_natureza]
	   ,R.[cd_origem_recurso]
	   ,R.[cd_destino_recurso]
	   ,R.[fg_transmitido_siafem]
	   ,R.[fg_transmitido_siafisico]
	   ,R.[fg_transmitido_prodesp]
	   ,R.[bl_transmitir_siafem]
	   ,R.[bl_transmitir_siafisico]
	   ,R.[bl_transmitir_prodesp]
	   ,R.[ds_status_siafem_siafisico]
	   ,R.[ds_status_prodesp]
	   ,R.bl_cadastro_completo
	   ,R.[ds_msgRetornoTransmissaoProdesp]
	   ,R.[ds_msgRetornoTransSiafemSiafisico]
	    
	   	   
   ,sum(M.[vr_mes]) vr_mes
   FROM [reserva].[tb_reserva]  R (nolock)
        left join  [configuracao].[tb_programa] P (nolock)
		on R.[id_programa] = P.[id_programa]  
		left join  [configuracao].[tb_estrutura] E (nolock)
		on R.[id_estrutura] = E.[id_estrutura]
		left join [reserva].[tb_reserva_mes] M (nolock)
		on R.[id_reserva] = M.[id_reserva] 

   where (@id_reserva = R.id_reserva OR ISNULL(@id_reserva,0) = 0) AND  
         (@cd_contrato = R.cd_contrato OR ISNULL(@cd_contrato,'') = '') AND  
		 (R.cd_processo like '%'+@cd_processo+'%' OR ISNULL(@cd_processo,'') = '') AND  
		 (@nr_reserva_prodesp = R.nr_reserva_prodesp OR ISNULL(@nr_reserva_prodesp,'') = '') AND  
		 (@nr_reserva_siafem_siafisico = R.nr_reserva_siafem_siafisico OR ISNULL(@nr_reserva_siafem_siafisico,'') = '') AND  
		 (@cd_obra = R.cd_obra OR ISNULL(@cd_obra,0) = 0) AND  
		 (@id_tipo_reserva = R.id_tipo_reserva OR ISNULL(@id_tipo_reserva,0) = 0) AND  
		 (@id_regional = R.id_regional OR ISNULL(@id_regional,0) = 0) AND  
		 (@nr_ano_exercicio = R.nr_ano_exercicio OR ISNULL(@nr_ano_exercicio,0) = 0) AND  
		 (@id_programa = R.id_programa OR ISNULL(@id_programa,0) = 0) AND  
		 (@id_estrutura = R.id_estrutura OR ISNULL(@id_estrutura,0) = 0) AND  
		 (R.ds_status_siafem_siafisico = @fg_transmitido_siafem OR @fg_transmitido_siafem is null) AND
         (R.ds_status_prodesp = @fg_transmitido_prodesp OR @fg_transmitido_prodesp is null) AND  
		 (@cd_ugo = R.cd_ugo OR ISNULL(@cd_ugo,0) = 0) AND  
		 (R.dt_cadastro  >= @dt_emissao_reservaDe or @dt_emissao_reservaDe is null)  AND
         (R.dt_cadastro  <= @dt_emissao_reservaAte or @dt_emissao_reservaAte is null)    
group by 
    R.[id_reserva]  
	,R.[id_estrutura]  
    ,R.[id_programa] 
    ,R.[nr_reserva_siafem_siafisico]  
	,R.[nr_reserva_prodesp]  
	,R.[cd_contrato]  
	,R.[cd_processo] 
	,P.[cd_cfp]
	,P.[ds_programa]
	,E.[cd_natureza]
	,R.[cd_origem_recurso]
	,R.[cd_destino_recurso]
	,R.[fg_transmitido_siafem]
	,R.[fg_transmitido_siafisico]
	,R.[fg_transmitido_prodesp]
	   ,R.[bl_transmitir_siafem]
	   ,R.[bl_transmitir_siafisico]
	   ,R.[bl_transmitir_prodesp]
	,R.[ds_status_siafem_siafisico]
	,R.[ds_status_prodesp]
	,R.bl_cadastro_completo
	,R.[ds_msgRetornoTransmissaoProdesp]
	,R.[ds_msgRetornoTransSiafemSiafisico]
   
end;