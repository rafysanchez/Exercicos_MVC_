
-- ===================================================================    
-- Author:  Rodrigo Cesar de Freitas  
-- Create date: 30/03/2017  
-- Description: Procedure para listar inscrições de rap
-- ===================================================================   
CREATE procedure [dbo].[PR_RAP_INSCRICAO_CONSULTAR_GRID]    
	@nr_prodesp varchar(13) = null
,	@nr_siafem_siafisico varchar(11) = null
,	@nr_empenho_prodesp varchar(11) = null
,	@nr_despesa_processo varchar(60) = null
,	@tb_servico_tipo_id_servico_tipo int = null
,	@cd_transmissao_status_siafem_siafisico char(1) = null
,	@cd_transmissao_status_prodesp char(1) = null
,	@cd_aplicacao_obra varchar(140) = null
,	@nr_contrato varchar(13) = null
,	@dt_cadastramento_de date = null
,	@dt_cadastramento_ate date = null
as    
begin    
    
	set nocount on;    
    
	select	id_rap_inscricao
		,	nr_prodesp
		,	nr_siafem_siafisico
		,	cd_nota_fiscal_prodesp
		,   tb_servico_tipo_id_servico_tipo
		,	vl_valor
		,	cd_transmissao_status_prodesp
		,	cd_transmissao_status_siafem_siafisico
		,   fl_transmissao_transmitido_prodesp
		,   fl_transmissao_transmitido_siafem_siafisico
		,   fl_sistema_prodesp
		,   fl_sistema_siafem_siafisico
		,   fl_documento_completo
		,   dt_cadastro
		,   [ds_transmissao_mensagem_siafem_siafisico]
		,   [ds_transmissao_mensagem_prodesp]
	
	from	pagamento.tb_rap_inscricao (nolock) as inscricao
	        left join pagamento.tb_servico_tipo (nolock) as servico
			on inscricao.tb_servico_tipo_id_servico_tipo = servico.id_servico_tipo

	where	( @nr_prodesp is null or nr_prodesp like '%'+@nr_prodesp+'%' )
		and	( @nr_siafem_siafisico is null or nr_siafem_siafisico like '%'+@nr_siafem_siafisico+'%' )
		and ( @nr_empenho_prodesp is null or nr_empenho_prodesp like '%'+@nr_empenho_prodesp+'%' )
		and	( @nr_despesa_processo is null or nr_despesa_processo like '%'+@nr_despesa_processo+'%' )
		and	( nullif( @tb_servico_tipo_id_servico_tipo, 0 ) is null or tb_servico_tipo_id_servico_tipo = @tb_servico_tipo_id_servico_tipo )
		and	( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico ) 
		and	( @cd_transmissao_status_prodesp is null or cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) 
		and	( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )
		and	( @nr_contrato is null or nr_contrato = @nr_contrato )
		and	( @dt_cadastramento_de is null or dt_cadastro >= @dt_cadastramento_de )
		and	( @dt_cadastramento_ate is null or dt_cadastro <= @dt_cadastramento_ate );

end;