-- ===================================================================    
-- Author:  Rodrigo Cesar de Freitas  
-- Create date: 09/02/2017  
-- Description: Procedure para listar cancelamentos de subempenho
-- ===================================================================   
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_CONSULTAR_GRID]    
	@nr_prodesp varchar(17) = null
,	@nr_siafem_siafisico varchar(11) = null
,	@nr_subempenho_prodesp varchar(13) = null
,	@nr_despesa_processo varchar(60) = null
,	@tb_cenario_id_cenario int = null
,	@cd_transmissao_status_siafem_siafisico char(1) = null
,   @cd_transmissao_status_siafisico char(1) = null
,	@cd_transmissao_status_prodesp char(1) = null
,	@dt_cadastramento_de date = null
,	@dt_cadastramento_ate date = null
,	@nr_cnpj_cpf_credor varchar(15) = null
,	@cd_gestao_credor varchar(140) = null
,	@nr_contrato varchar(13) = null
,	@cd_aplicacao_obra varchar(140) = null

as    
begin    
    
	set nocount on;    
    
	select	id_subempenho_cancelamento
		,	nr_prodesp  
		,	nr_siafem_siafisico  
		,	case when vl_valor > 0
			then vl_valor 
			else ( 
				select	sum(e.vl_evento)
				from	pagamento.tb_subempenho_cancelamento_evento e
				where	e.tb_subempenho_cancelamento_id_subempenho_cancelamento = tb_subempenho_cancelamento.id_subempenho_cancelamento
			)
		end as vl_valor
		,	cd_transmissao_status_prodesp  
		,	cd_transmissao_status_siafem_siafisico  
		,   cd_transmissao_status_siafisico
		,   fl_transmissao_transmitido_prodesp
		,   fl_transmissao_transmitido_siafem_siafisico
		,   fl_transmissao_transmitido_siafisico

		,   fl_sistema_prodesp
		,   fl_sistema_siafem_siafisico
		,   fl_sistema_siafisico
		,   fl_documento_completo

		,	[ds_transmissao_mensagem_prodesp]
		,	[ds_transmissao_mensagem_siafem_siafisico]
		,	tb_cenario_id_cenario
		

	from	pagamento.tb_subempenho_cancelamento (nolock)
	where	( @nr_prodesp is null or nr_prodesp = @nr_prodesp ) 
		and	( @nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico ) 
		and ( @nr_subempenho_prodesp is null or nr_subempenho_prodesp = @nr_subempenho_prodesp ) 
		and	( @nr_despesa_processo is null or nr_despesa_processo = @nr_despesa_processo ) 
		and	( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra ) 
		and	( nullif( @tb_cenario_id_cenario, 0 ) is null or tb_cenario_id_cenario = @tb_cenario_id_cenario ) 
		
		and	(( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico ) 
		or	( @cd_transmissao_status_siafisico is null or cd_transmissao_status_siafisico = @cd_transmissao_status_siafisico )) 
		
		and	( @cd_transmissao_status_prodesp is null or cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) 
		and	( @nr_contrato is null or nr_contrato = @nr_contrato ) 
		and	( @dt_cadastramento_de is null or dt_cadastro >= @dt_cadastramento_de ) 
		and	( @dt_cadastramento_ate is null or dt_cadastro <= @dt_cadastramento_ate ) 
		and	( @nr_cnpj_cpf_credor is null or nr_cnpj_cpf_credor = @nr_cnpj_cpf_credor ) 
		and	( @cd_gestao_credor is null or cd_gestao_credor = @cd_gestao_credor ) 

end;