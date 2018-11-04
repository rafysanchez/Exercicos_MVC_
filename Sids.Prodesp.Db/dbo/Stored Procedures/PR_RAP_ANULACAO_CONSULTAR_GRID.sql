  
-- ===================================================================      
-- Author:  Carlos Henrique Magalhaes  
-- Create date: 31/03/2017    
-- Description: Procedure para listar anulações de rap  
-- ===================================================================     
CREATE procedure [dbo].[PR_RAP_ANULACAO_CONSULTAR_GRID]      
  
 @nr_prodesp varchar(13) = NULL ,
 @nr_siafem_siafisico varchar(11)= NULL ,
 @nr_contrato varchar(13) = NULL,  
 @cd_aplicacao_obra  varchar(8) = NULL ,
 @nr_empenho_siafem_siafisico varchar(11) = NULL,  
 @nr_despesa_processo varchar(60) = NULL ,
 @cd_transmissao_status_siafem_siafisico char(1) = NULL,  
 @cd_transmissao_status_prodesp char(1) = NULL,
 @dt_cadastramento_de date = NULL,  
 @dt_cadastramento_ate date = NULL ,
 @tb_servico_tipo_id_servico_tipo int = NULL  
   
as      
begin      
      
 set nocount on;      
      
 select id_rap_anulacao,  
    nr_prodesp,
    nr_siafem_siafisico,  
    tb_servico_tipo_id_servico_tipo ,
    vl_valor,  
    dt_cadastro,  
    cd_transmissao_status_prodesp,  
	cd_transmissao_status_siafem_siafisico  
	,ds_transmissao_mensagem_prodesp
	,ds_transmissao_mensagem_siafem_siafisico
	,fl_transmissao_transmitido_siafem_siafisico
	,fl_transmissao_transmitido_siafisico
	, fl_transmissao_transmitido_prodesp
	,fl_documento_completo
	
	from	pagamento.tb_rap_anulacao (nolock)
	        left join pagamento.tb_servico_tipo (nolock)
			on tb_rap_anulacao.tb_servico_tipo_id_servico_tipo = tb_servico_tipo.id_servico_tipo

	where	( @nr_prodesp is null or nr_prodesp like '%'+@nr_prodesp+'%' )
		and	( @nr_siafem_siafisico is null or nr_siafem_siafisico like '%'+@nr_siafem_siafisico+'%' )
		and	( @nr_despesa_processo is null or nr_despesa_processo like '%'+@nr_despesa_processo+'%' )
		and	( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico ) 
		and	( @cd_transmissao_status_prodesp is null or cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) 
		and	( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )
		and	( @nr_contrato is null or nr_contrato = @nr_contrato )
		and	( @dt_cadastramento_de is null or dt_cadastro >= @dt_cadastramento_de )
		and	( @dt_cadastramento_ate is null or dt_cadastro <= @dt_cadastramento_ate );
	
  
end;