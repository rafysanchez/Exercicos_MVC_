  
-- ===================================================================      
-- Author:  Rodrigo Cesar de Freitas    
-- Create date: 09/02/2017    
-- Description: Procedure para listar subempenhos   
-- ===================================================================     
CREATE procedure [dbo].[PR_SUBEMPENHO_CONSULTAR_GRID]      
 @nr_prodesp varchar(13) = null  
, @nr_siafem_siafisico varchar(11) = null  
, @nr_empenho_prodesp varchar(11) = null  
, @nr_despesa_processo varchar(60) = null  
, @id_regional smallint = null  
, @tb_cenario_id_cenario int = null  
, @cd_transmissao_status_siafem_siafisico char(1) = null  
, @cd_transmissao_status_siafisico char(1) = null  
, @cd_transmissao_status_prodesp char(1) = null  
, @cd_aplicacao_obra varchar(140) = null  
, @nr_contrato varchar(13) = null  
, @dt_cadastramento_de date = null  
, @dt_cadastramento_ate date = null  
, @nr_cnpj_cpf_credor varchar(15) = null  
, @cd_gestao_credor varchar(140) = null  
, @cd_credor_organizacao int = null  
, @nr_cnpj_cpf_fornecedor varchar(15) = null  
,@nr_empenho_siafem_siafisico varchar(11) = null  
as      
begin      
      
 set nocount on;      
      
 select id_subempenho    
  , nr_prodesp    
  , nr_siafem_siafisico    
  , cd_nota_fiscal_prodesp  
  ,   tb_cenario_id_cenario--tb_cenario_tipo.ds_cenario_tipo as ds_cenario_tipo  
  /*, case when vl_valor > 0  
   then vl_valor   
   else (   
    select sum(e.vl_evento)  
    from pagamento.tb_subempenho_evento e  
    where e.tb_subempenho_id_subempenho = tb_subempenho.id_subempenho  
   )  
  end as vl_valor  
  */  
  --, case when vl_realizado > 0  
  -- then vl_realizado   
  -- else vl_valor  
  -- end as vl_valor  
  , vl_valor  
  , cd_transmissao_status_prodesp    
  , cd_transmissao_status_siafem_siafisico    
  , cd_transmissao_status_siafisico    
  ,   fl_transmissao_transmitido_prodesp  
  ,   fl_transmissao_transmitido_siafem_siafisico  
  ,   fl_transmissao_transmitido_siafisico  
  ,   fl_sistema_prodesp  
  ,   fl_sistema_siafem_siafisico  
  ,   fl_sistema_siafisico  
  ,   fl_documento_completo  
  , [ds_transmissao_mensagem_prodesp]  
  , [ds_transmissao_mensagem_siafem_siafisico]  
  ,   [vl_realizado]  
  
  
  
 from pagamento.tb_subempenho (nolock)  
         --left join pagamento.tb_cenario_tipo (nolock)  
   --on tb_subempenho.tb_cenario_id_cenario = tb_cenario_tipo.id_cenario_tipo   
  
 where ( @nr_prodesp is null or nr_prodesp like '%'+@nr_prodesp+'%' )  
  and ( @nr_siafem_siafisico is null or nr_siafem_siafisico like '%'+@nr_siafem_siafisico+'%' )  
  and ( @nr_empenho_prodesp is null or nr_empenho_prodesp like '%'+@nr_empenho_prodesp+'%' )  
  and ( @nr_despesa_processo is null or nr_despesa_processo like '%'+@nr_despesa_processo+'%' )   
  and ( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )   
  and ( nullif( @tb_cenario_id_cenario, 0 ) is null or tb_cenario_id_cenario = @tb_cenario_id_cenario )   
  and (( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico )   
   or ( @cd_transmissao_status_siafisico is null or cd_transmissao_status_siafisico = @cd_transmissao_status_siafisico))  
  and ( @cd_transmissao_status_prodesp is null or cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp )   
  and ( @nr_contrato is null or nr_contrato = @nr_contrato )   
  and ( @dt_cadastramento_de is null or dt_cadastro >= @dt_cadastramento_de )   
  and ( @dt_cadastramento_ate is null or dt_cadastro <= @dt_cadastramento_ate )   
  and ( @nr_cnpj_cpf_credor is null or nr_cnpj_cpf_credor = @nr_cnpj_cpf_credor )   
  and ( @cd_gestao_credor is null or cd_gestao_credor = @cd_gestao_credor )   
  and ( nullif( @cd_credor_organizacao, 0 ) is null or cd_credor_organizacao = @cd_credor_organizacao )   
  and ( @nr_cnpj_cpf_fornecedor is null or nr_cnpj_cpf_fornecedor = @nr_cnpj_cpf_fornecedor )  
  and ( nullif( @id_regional, 1 ) is null or nullif( @id_regional, 0 ) is null or tb_regional_id_regional = @id_regional)
  and ( @nr_empenho_siafem_siafisico is null or nr_empenho_siafem_siafisico like '%'+@nr_empenho_siafem_siafisico+'%' )  ;  
  
end;