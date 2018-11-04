-- ===================================================================    
-- Author:  Luis Fernando  
-- Create date: 11/07/2017  
-- Description: Procedure para consultar programacao desembolso grid  
-- ===================================================================   

CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_CONSULTA_GRID]   
  @id_programacao_desembolso int = null  
 ,@nr_siafem_siafisico varchar(11) = null  
 ,@nr_processo varchar(15) = NULL  
 ,@cd_aplicacao_obra varchar(15) = NULL  
 ,@id_tipo_programacao_desembolso int= NULL  
 ,@cd_transmissao_status_siafem_siafisico char(1)= NULL  
 ,@nr_contrato varchar(13)= NULL  
 ,@dt_vencimento date = NULL  
 ,@dt_cadastramento_de date = NULL  
 ,@dt_cadastramento_ate date = NULL  
 ,@id_regional smallint = null  
 ,@nr_agrupamento int= NULL  
 ,@cd_despesa varchar(2) = NULL  
 ,@id_tipo_documento int = NULL  
 ,@nr_documento varchar(19) = NULL  
 ,@bl_bloqueio bit = null
as    
begin    
    
 SET NOCOUNT ON;    
    
 SELECT  DISTINCT
     pd.[id_programacao_desembolso]  
    ,pd.[id_tipo_programacao_desembolso]  
    ,pd.[id_tipo_documento]  
    ,pd.[dt_cadastro]  
    ,pd.[nr_siafem_siafisico]  
    ,pd.[nr_contrato]  
    ,pd.[nr_processo]  
    ,pd.[nr_documento]  
    ,pd.[cd_unidade_gestora]  
    ,pd.[cd_gestao]  
    ,pd.[vl_total]  
    ,pd.[dt_emissao]  
    ,pd.[id_regional]  
    ,pd.[cd_aplicacao_obra]  
    ,pd.[nr_lista_anexo]  
    ,pd.[nr_nl_referencia]  
    ,pd.[ds_finalidade]  
    ,pd.[cd_despesa]  
    ,pd.[dt_vencimento]  
    ,pd.[nr_cnpj_cpf_credor]  
    ,pd.[cd_gestao_credor]  
    ,pd.[nr_banco_credor]  
    ,pd.[nr_agencia_credor]  
    ,pd.[nr_conta_credor]  
    ,pd.[nr_cnpj_cpf_pgto]  
    ,pd.[cd_gestao_pgto]  
    ,pd.[nr_banco_pgto]  
    ,pd.[nr_agencia_pgto]  
    ,pd.[nr_conta_pgto]  
    ,pd.[fl_sistema_siafem_siafisico]  
    ,pd.[cd_transmissao_status_siafem_siafisico]  
    ,pd.[fl_transmissao_transmitido_siafem_siafisico]  
    ,pd.[dt_transmissao_transmitido_siafem_siafisico]  
    ,pd.[ds_transmissao_mensagem_siafem_siafisico]  
    ,pd.[bl_cadastro_completo]  
    ,pd.[obs]  
    ,pd.[nr_ne]  
    ,pd.[nr_ct]  
    ,pd.[bl_bloqueio]  
    ,pd.[bl_cancelado]  
	,pd.[rec_despesa]
 FROM [contaunica].[tb_programacao_desembolso_agrupamento] (nolock) ag 
   right join [contaunica].[tb_programacao_desembolso] (nolock) pd   on ag.id_programacao_desembolso = pd.id_programacao_desembolso

   where
      ( nullif( @id_programacao_desembolso, 0 ) is null or pd.id_programacao_desembolso = @id_programacao_desembolso ) 
  and ( nullif( @id_regional, 0 ) is null or pd.[id_regional] = @id_regional )  
  and (nullif( @id_tipo_programacao_desembolso, 0 ) is null or [id_tipo_programacao_desembolso] = @id_tipo_programacao_desembolso )  
  and (@nr_processo is null or pd.nr_processo = @nr_processo )  
  and (@cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )  
  and (@nr_contrato is null or nr_contrato = @nr_contrato )  
  and ( @cd_despesa is null or pd.cd_despesa = @cd_despesa )   
  and (@dt_vencimento is null or pd.dt_vencimento = @dt_vencimento )   
  and (nullif(@id_tipo_documento,0)is null or pd.[id_tipo_documento] = @id_tipo_documento  )
  and ( @nr_documento is null or   (REPLACE(ag.nr_documento,'/','') like '%'+ REPLACE( @nr_documento,'/','') +'%'  or REPLACE(pd.nr_documento,'/','') like '%'+ REPLACE( @nr_documento,'/','') +'%') )
  and ( @dt_cadastramento_de is null or pd.[dt_cadastro] >= @dt_cadastramento_de )
  and ( @dt_cadastramento_ate is null or pd.[dt_cadastro] <= @dt_cadastramento_ate  )
  and ( (@cd_transmissao_status_siafem_siafisico is null or pd.cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico) or (@cd_transmissao_status_siafem_siafisico is null or ag.cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico)   )
  and ( (@nr_siafem_siafisico is null or pd.nr_siafem_siafisico = @nr_siafem_siafisico) or  (@nr_siafem_siafisico is null or ag.nr_programacao_desembolso = @nr_siafem_siafisico) )
  and (nullif(@nr_agrupamento,0) is null or ag.nr_agrupamento = @nr_agrupamento)
  and ((@bl_bloqueio is null or pd.bl_bloqueio = @bl_bloqueio )  or (@bl_bloqueio is null or ag.bl_bloqueio = @bl_bloqueio ) )

 Order by pd.id_programacao_desembolso  
end;