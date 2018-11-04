
-- ===================================================================    
-- Author:  Carlos Henrique Magalhaes
-- Create date: 31/03/2017  
-- Description: Procedure para listar requisições de rap
-- ===================================================================   
CREATE procedure [dbo].[PR_RAP_REQUISICAO_CONSULTAR_GRID]    
		
	@nr_prodesp varchar(18) = NULL,
	@nr_siafem_siafisico varchar(11)= NULL,
	@nr_despesa_processo varchar(60) = NULL,
	@cd_aplicacao_obra  varchar(8) = NULL,
	@nr_prodesp_original varchar(15) = NULL,  
	@cd_transmissao_status_siafem_siafisico char(1) = NULL,
	@cd_transmissao_status_prodesp char(1)= NULL,
	@nr_contrato varchar(13) = NULL,
	@dt_cadastramento_de date = NULL,
	@dt_cadastramento_ate date = NULL,
	@tb_servico_tipo_id_servico_tipo int = null
	
as    
begin    
    
	set nocount on;    
    
	select	id_rap_requisicao,
		 	nr_prodesp,
		 	nr_siafem_siafisico,
		    tb_servico_tipo_id_servico_tipo,
		 	vl_valor,
			cd_transmissao_status_prodesp,
			cd_transmissao_status_siafem_siafisico,
			fl_transmissao_transmitido_prodesp,	
		    fl_transmissao_transmitido_siafem_siafisico,
			fl_sistema_prodesp,
			fl_sistema_siafem_siafisico,
			fl_documento_completo,
			dt_cadastro,
			ds_transmissao_mensagem_prodesp,
			ds_transmissao_mensagem_siafem_siafisico
		
	
	from	pagamento.tb_rap_requisicao (nolock) as rap
	        left join pagamento.tb_servico_tipo (nolock) as servico
			on rap.tb_servico_tipo_id_servico_tipo = servico.id_servico_tipo

	where	( @nr_prodesp is null or nr_prodesp like '%'+@nr_prodesp+'%' )
		and	( @nr_siafem_siafisico is null or nr_siafem_siafisico like '%'+@nr_siafem_siafisico+'%' )
		and ( @nr_prodesp_original is null or nr_prodesp_original like '%'+@nr_prodesp_original+'%' )
		and	( @nr_despesa_processo is null or nr_despesa_processo like '%'+@nr_despesa_processo+'%' )
		and	( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico ) 
		and	( @cd_transmissao_status_prodesp is null or cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) 
		and	( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )
		and	( @nr_contrato is null or nr_contrato = @nr_contrato )
		and	( @dt_cadastramento_de is null or dt_cadastro >= @dt_cadastramento_de )
		and	( @dt_cadastramento_ate is null or dt_cadastro <= @dt_cadastramento_ate )
		and	( nullif( @tb_servico_tipo_id_servico_tipo, 0 ) is null or tb_servico_tipo_id_servico_tipo = @tb_servico_tipo_id_servico_tipo )
		
	
end;