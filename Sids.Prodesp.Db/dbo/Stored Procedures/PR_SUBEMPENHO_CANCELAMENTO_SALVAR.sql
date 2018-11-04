-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para salvar ou alterar cancelamentos de subempenhos
-- ===================================================================  
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_SALVAR] 
	@id_subempenho_cancelamento int,
	@dt_cadastro date = NULL,
	@fl_sistema_prodesp bit = NULL,
	@fl_sistema_siafem_siafisico bit = NULL,
	@fl_sistema_siafisico bit = NULL,
	@nr_prodesp varchar(17) = NULL,
	@nr_siafem_siafisico varchar(11) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tb_cenario_id_cenario int = NULL,
	@nr_subempenho_prodesp varchar(13) = NULL,
	@vl_realizado int = NULL,
	@vl_anular int = NULL,
	@cd_cenario_prodesp varchar(140) = NULL,
	@nr_nl_referencia varchar(11) = null,
	@nr_ct varchar(11) = NULL,
	@nr_empenho_siafem_siafisico varchar(11) = NULL,
	@cd_unidade_gestora varchar(6) = NULL,
	@cd_gestao varchar(5) = NULL,
	@vl_valor int = NULL,
	@dt_emissao date = NULL,
	@cd_evento int = NULL,
	@tb_evento_tipo_id_evento_tipo int = NULL,
	@nr_cnpj_cpf_credor varchar(15) = NULL,
	@cd_gestao_credor varchar(140) = NULL,
	@nr_ano_medicao char(4) = NULL,
	@nr_mes_medicao char(2) = NULL,
	@nr_percentual varchar(140) = NULL,
	@tb_regional_id_regional smallint = NULL,
	@tb_obra_tipo_id_obra_tipo int = NULL,
	@cd_obra_tipo char(1) = NULL,
	@nr_obra  varchar(9) = NULL,
	@cd_unidade_gestora_obra varchar(6) = NULL,
	@ds_observacao_1 varchar(76) = NULL,
	@ds_observacao_2 varchar(76) = NULL,
	@ds_observacao_3 varchar(76) = NULL,
	@nr_despesa_processo varchar(60) = NULL,
	@ds_despesa_referencia varchar(60) = NULL,
	@ds_despesa_autorizado_supra_folha char(4) = NULL,
	@cd_despesa_especificacao_despesa char(3) = NULL,
	@ds_despesa_especificacao_1 varchar(79) = NULL,
	@ds_despesa_especificacao_2 varchar(79) = NULL,
	@ds_despesa_especificacao_3 varchar(79) = NULL,
	@ds_despesa_especificacao_4 varchar(79) = NULL,
	@ds_despesa_especificacao_5 varchar(79) = NULL,
	@ds_despesa_especificacao_6 varchar(79) = NULL,
	@ds_despesa_especificacao_7 varchar(79) = NULL,
	@ds_despesa_especificacao_8 varchar(79) = NULL,
	@ds_despesa_especificacao_9 varchar(79) = NULL,
	@cd_assinatura_autorizado varchar(5) = NULL,
	@cd_assinatura_autorizado_grupo int = NULL,
	@cd_assinatura_autorizado_orgao char(2) = NULL,
	@ds_assinatura_autorizado_cargo varchar(55) = NULL,
	@nm_assinatura_autorizado varchar(55) = NULL,
	@cd_assinatura_examinado varchar(5) = NULL,
	@cd_assinatura_examinado_grupo int = NULL,
	@cd_assinatura_examinado_orgao char(2) = NULL,
	@ds_assinatura_examinado_cargo varchar(55) = NULL,
	@nm_assinatura_examinado varchar(55) = NULL,
	@cd_assinatura_responsavel varchar(5) = NULL,
	@cd_assinatura_responsavel_grupo int = NULL,
	@cd_assinatura_responsavel_orgao char(2) = NULL,
	@ds_assinatura_responsavel_cargo varchar(55) = NULL,
	@nm_assinatura_responsavel varchar(55) = NULL,
	@cd_transmissao_status_prodesp char(1) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp date = NULL,
	@ds_transmissao_mensagem_prodesp varchar(140) = NULL,
	@cd_transmissao_status_siafem_siafisico char(1) = NULL,
	@cd_transmissao_status_siafisico char(1) = NULL,
	@fl_transmissao_transmitido_siafem_siafisico bit = NULL,
	@fl_transmissao_transmitido_siafisico bit = NULL,
	@dt_transmissao_transmitido_siafem_siafisico date = NULL,
	@ds_transmissao_mensagem_siafem_siafisico varchar(140) = NULL,
	@fl_documento_completo bit = NULL,
	@fl_documento_status bit = NULL
as
begin

	set nocount on;

	if exists (
		select	1 
		from	pagamento.tb_subempenho_cancelamento
		where	id_subempenho_cancelamento = @id_subempenho_cancelamento
	)
	begin

		update pagamento.tb_subempenho_cancelamento set 
				fl_sistema_prodesp								= @fl_sistema_prodesp
			,	fl_sistema_siafem_siafisico						= @fl_sistema_siafem_siafisico
			,	fl_sistema_siafisico							= @fl_sistema_siafisico
			,	nr_prodesp										= @nr_prodesp
			,	nr_siafem_siafisico								= @nr_siafem_siafisico
			,	nr_contrato										= @nr_contrato
			,	tb_cenario_id_cenario							= nullif( @tb_cenario_id_cenario, 0  )
			,	nr_subempenho_prodesp							= @nr_subempenho_prodesp
			,	vl_realizado									= @vl_realizado
			,	vl_anular							    		= @vl_anular
			,	cd_cenario_prodesp								= @cd_cenario_prodesp
			,	nr_nl_referencia								= @nr_nl_referencia
			,	nr_ct											= @nr_ct
			,	nr_empenho_siafem_siafisico						= @nr_empenho_siafem_siafisico
			,	cd_unidade_gestora								= @cd_unidade_gestora
			,	cd_gestao										= @cd_gestao
			,	vl_valor										= @vl_valor
			,	dt_emissao										= @dt_emissao
			,	cd_evento										= nullif( @cd_evento, 0 )
			,	tb_evento_tipo_id_evento_tipo					= nullif( @tb_evento_tipo_id_evento_tipo, 0 )
			,	nr_cnpj_cpf_credor								= @nr_cnpj_cpf_credor
			,	cd_gestao_credor								= @cd_gestao_credor
			,	nr_ano_medicao									= @nr_ano_medicao
			,	nr_mes_medicao									= @nr_mes_medicao
			,	nr_percentual									= @nr_percentual
			,	tb_regional_id_regional							= nullif( @tb_regional_id_regional, 0 )
			,	tb_obra_tipo_id_obra_tipo						= nullif( @tb_obra_tipo_id_obra_tipo, 0 )
			,   cd_obra_tipo									= @cd_obra_tipo
			,   nr_obra											= @nr_obra
			,	cd_unidade_gestora_obra							= @cd_unidade_gestora_obra
			,	ds_observacao_1									= @ds_observacao_1
			,	ds_observacao_2									= @ds_observacao_2
			,	ds_observacao_3									= @ds_observacao_3
			,	nr_despesa_processo								= @nr_despesa_processo
			,	ds_despesa_referencia							= @ds_despesa_referencia
			,	ds_despesa_autorizado_supra_folha				= @ds_despesa_autorizado_supra_folha
			,	cd_despesa_especificacao_despesa				= @cd_despesa_especificacao_despesa
			,	ds_despesa_especificacao_1						= @ds_despesa_especificacao_1
			,	ds_despesa_especificacao_2						= @ds_despesa_especificacao_2
			,	ds_despesa_especificacao_3						= @ds_despesa_especificacao_3
			,	ds_despesa_especificacao_4						= @ds_despesa_especificacao_4
			,	ds_despesa_especificacao_5						= @ds_despesa_especificacao_5
			,	ds_despesa_especificacao_6						= @ds_despesa_especificacao_6
			,	ds_despesa_especificacao_7						= @ds_despesa_especificacao_7
			,	ds_despesa_especificacao_8						= @ds_despesa_especificacao_8
			,	ds_despesa_especificacao_9						= @ds_despesa_especificacao_9
			,	cd_assinatura_autorizado						= @cd_assinatura_autorizado
			,	cd_assinatura_autorizado_grupo					= @cd_assinatura_autorizado_grupo
			,	cd_assinatura_autorizado_orgao					= @cd_assinatura_autorizado_orgao
			,	ds_assinatura_autorizado_cargo					= @ds_assinatura_autorizado_cargo
			,	nm_assinatura_autorizado						= @nm_assinatura_autorizado
			,	cd_assinatura_examinado							= @cd_assinatura_examinado
			,	cd_assinatura_examinado_grupo					= @cd_assinatura_examinado_grupo
			,	cd_assinatura_examinado_orgao					= @cd_assinatura_examinado_orgao
			,	ds_assinatura_examinado_cargo					= @ds_assinatura_examinado_cargo
			,	nm_assinatura_examinado							= @nm_assinatura_examinado
			,	cd_assinatura_responsavel						= @cd_assinatura_responsavel
			,	cd_assinatura_responsavel_grupo					= @cd_assinatura_responsavel_grupo
			,	cd_assinatura_responsavel_orgao					= @cd_assinatura_responsavel_orgao
			,	ds_assinatura_responsavel_cargo					= @ds_assinatura_responsavel_cargo
			,	nm_assinatura_responsavel						= @nm_assinatura_responsavel
			,	cd_transmissao_status_prodesp					= @cd_transmissao_status_prodesp
			,	fl_transmissao_transmitido_prodesp				= @fl_transmissao_transmitido_prodesp
			,	dt_transmissao_transmitido_prodesp				= @dt_transmissao_transmitido_prodesp
			,	ds_transmissao_mensagem_prodesp					= @ds_transmissao_mensagem_prodesp
			,	cd_transmissao_status_siafem_siafisico			= @cd_transmissao_status_siafem_siafisico
			,	cd_transmissao_status_siafisico					= @cd_transmissao_status_siafisico
			,	fl_transmissao_transmitido_siafem_siafisico		= @fl_transmissao_transmitido_siafem_siafisico
			,   fl_transmissao_transmitido_siafisico			= @fl_transmissao_transmitido_siafisico
			,	dt_transmissao_transmitido_siafem_siafisico		= @dt_transmissao_transmitido_siafem_siafisico
			,	ds_transmissao_mensagem_siafem_siafisico		= @ds_transmissao_mensagem_siafem_siafisico
			,	fl_documento_completo							= @fl_documento_completo
			,	fl_documento_status								= @fl_documento_status
		where	id_subempenho_cancelamento = @id_subempenho_cancelamento;

		select @id_subempenho_cancelamento;

	end
	else
	begin

		insert into pagamento.tb_subempenho_cancelamento (
				dt_cadastro										
			,	fl_sistema_prodesp								
			,	fl_sistema_siafem_siafisico		
			,	fl_sistema_siafisico	
			,	nr_prodesp										
			,	nr_siafem_siafisico					
			,	nr_contrato
			,	tb_cenario_id_cenario							
			,	nr_subempenho_prodesp							
			,	vl_realizado									
			,	vl_anular	
			,	cd_cenario_prodesp								
			,	nr_nl_referencia
			,	nr_ct											
			,	nr_empenho_siafem_siafisico						
			,	cd_unidade_gestora								
			,	cd_gestao										
			,	vl_valor										
			,	dt_emissao										
			,	cd_evento
			,	tb_evento_tipo_id_evento_tipo					
			,	nr_cnpj_cpf_credor								
			,	cd_gestao_credor								
			,	nr_ano_medicao									
			,	nr_mes_medicao									
			,	nr_percentual	
			,	tb_regional_id_regional							
			,	tb_obra_tipo_id_obra_tipo	
			,	cd_obra_tipo	
			,   nr_obra				
			,	cd_unidade_gestora_obra							
			,	ds_observacao_1									
			,	ds_observacao_2									
			,	ds_observacao_3									
			,	nr_despesa_processo								
			,	ds_despesa_referencia							
			,	ds_despesa_autorizado_supra_folha				
			,	cd_despesa_especificacao_despesa				
			,	ds_despesa_especificacao_1						
			,	ds_despesa_especificacao_2						
			,	ds_despesa_especificacao_3						
			,	ds_despesa_especificacao_4						
			,	ds_despesa_especificacao_5						
			,	ds_despesa_especificacao_6						
			,	ds_despesa_especificacao_7						
			,	ds_despesa_especificacao_8						
			,	ds_despesa_especificacao_9						
			,	cd_assinatura_autorizado						
			,	cd_assinatura_autorizado_grupo					
			,	cd_assinatura_autorizado_orgao					
			,	ds_assinatura_autorizado_cargo					
			,	nm_assinatura_autorizado						
			,	cd_assinatura_examinado							
			,	cd_assinatura_examinado_grupo					
			,	cd_assinatura_examinado_orgao					
			,	ds_assinatura_examinado_cargo					
			,	nm_assinatura_examinado							
			,	cd_assinatura_responsavel						
			,	cd_assinatura_responsavel_grupo					
			,	cd_assinatura_responsavel_orgao					
			,	ds_assinatura_responsavel_cargo					
			,	nm_assinatura_responsavel						
			,	cd_transmissao_status_prodesp					
			,	fl_transmissao_transmitido_prodesp				
			,	dt_transmissao_transmitido_prodesp				
			,	ds_transmissao_mensagem_prodesp					
			,	cd_transmissao_status_siafem_siafisico			
			,	cd_transmissao_status_siafisico			
			,	fl_transmissao_transmitido_siafem_siafisico		
			,	fl_transmissao_transmitido_siafisico		
			,	dt_transmissao_transmitido_siafem_siafisico		
			,	ds_transmissao_mensagem_siafem_siafisico		
			,	fl_documento_completo							
			,	fl_documento_status								
		)
		values
		(
				isnull( @dt_cadastro, getdate() )
			,	@fl_sistema_prodesp
			,	@fl_sistema_siafem_siafisico
			,	@fl_sistema_siafisico
			,	@nr_prodesp
			,	@nr_siafem_siafisico
			,	@nr_contrato
			,	nullif( @tb_cenario_id_cenario, 0 )
			,	@nr_subempenho_prodesp
			,	@vl_realizado
			,	@vl_anular
			,	@cd_cenario_prodesp
			,	@nr_nl_referencia
			,	@nr_ct
			,	@nr_empenho_siafem_siafisico
			,	@cd_unidade_gestora
			,	@cd_gestao
			,	@vl_valor
			,	@dt_emissao
			,	nullif( @cd_evento, 0 )
			,	nullif( @tb_evento_tipo_id_evento_tipo, 0 )
			,	@nr_cnpj_cpf_credor
			,	@cd_gestao_credor
			,	@nr_ano_medicao
			,	@nr_mes_medicao
			,	@nr_percentual
			,	nullif( @tb_regional_id_regional, 0 )
			,	nullif( @tb_obra_tipo_id_obra_tipo, 0 )
			,   @cd_obra_tipo
			,   @nr_obra
			,	@cd_unidade_gestora_obra
			,	@ds_observacao_1
			,	@ds_observacao_2
			,	@ds_observacao_3
			,	@nr_despesa_processo
			,	@ds_despesa_referencia
			,	@ds_despesa_autorizado_supra_folha
			,	@cd_despesa_especificacao_despesa
			,	@ds_despesa_especificacao_1
			,	@ds_despesa_especificacao_2
			,	@ds_despesa_especificacao_3
			,	@ds_despesa_especificacao_4
			,	@ds_despesa_especificacao_5
			,	@ds_despesa_especificacao_6
			,	@ds_despesa_especificacao_7
			,	@ds_despesa_especificacao_8
			,	@ds_despesa_especificacao_9
			,	@cd_assinatura_autorizado
			,	@cd_assinatura_autorizado_grupo
			,	@cd_assinatura_autorizado_orgao
			,	@ds_assinatura_autorizado_cargo
			,	@nm_assinatura_autorizado
			,	@cd_assinatura_examinado
			,	@cd_assinatura_examinado_grupo
			,	@cd_assinatura_examinado_orgao
			,	@ds_assinatura_examinado_cargo
			,	@nm_assinatura_examinado
			,	@cd_assinatura_responsavel
			,	@cd_assinatura_responsavel_grupo
			,	@cd_assinatura_responsavel_orgao
			,	@ds_assinatura_responsavel_cargo
			,	@nm_assinatura_responsavel
			,	@cd_transmissao_status_prodesp
			,	@fl_transmissao_transmitido_prodesp
			,	@dt_transmissao_transmitido_prodesp
			,	@ds_transmissao_mensagem_prodesp
			,	@cd_transmissao_status_siafem_siafisico
			,	@cd_transmissao_status_siafisico
			,	@fl_transmissao_transmitido_siafem_siafisico
			,	@fl_transmissao_transmitido_siafisico
			,	@dt_transmissao_transmitido_siafem_siafisico
			,	@ds_transmissao_mensagem_siafem_siafisico
			,	@fl_documento_completo
			,	@fl_documento_status
		);

		select scope_identity();

	end

end