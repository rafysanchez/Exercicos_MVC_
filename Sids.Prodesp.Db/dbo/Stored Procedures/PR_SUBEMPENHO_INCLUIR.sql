-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description:	Procedure para incluir subempenhos 
-- ==============================================================
CREATE procedure [dbo].[PR_SUBEMPENHO_INCLUIR]
	@dt_cadastro date = NULL,
	@fl_sistema_prodesp bit = NULL,
	@fl_sistema_siafem_siafisico bit = NULL,
	@fl_sistema_siafisico bit = NULL,
	@nr_prodesp varchar(11) = NULL,
	@nr_siafem_siafisico varchar(11) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tb_cenario_id_cenario int = NULL,
	@nr_empenho_prodesp varchar(11) = NULL,
	@cd_tarefa varchar(2) = NULL,
	@cd_despesa varchar(2) = NULL,
	@vl_realizado int = NULL,
	@nr_recibo varchar(9) = NULL,
	@ds_prazo_pagamento varchar(3) = NULL,
	@dt_realizado date = NULL,
	@cd_cenario_prodesp varchar(140) = NULL,
	@nr_ct varchar(11) = NULL,
	@nr_empenho_siafem_siafisico varchar(11) = NULL,
	@cd_unidade_gestora varchar(6) = NULL,
	@cd_gestao varchar(5) = NULL,
	@cd_nota_fiscal_prodesp varchar(6) = NULL,
	@nr_medicao varchar(3) = NULL,
	@tb_natureza_tipo_id_natureza_tipo char(1) = NULL,
	@vl_valor int = NULL,
	@cd_evento int = NULL,
	@dt_emissao date = NULL,
	@tb_evento_tipo_id_evento_tipo int = NULL,
	@nr_cnpj_cpf_credor varchar(15) = NULL,
	@cd_gestao_credor varchar(140) = NULL,
	@nr_ano_medicao char(4) = NULL,
	@nr_mes_medicao char(2) = NULL,
	@nr_percentual varchar(140) = NULL,
	@tb_regional_id_regional smallint = NULL,
	@tb_programa_id_programa int = NULL,
	@tb_estrutura_id_estrutura int = NULL,
	@tb_fonte_id_fonte int = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@tb_obra_tipo_id_obra_tipo int = NULL,
	@cd_obra_tipo char(1) = NULL,
	@nr_obra varchar(9) = null,
	@cd_unidade_gestora_obra varchar(6) = NULL,
	@cd_credor_organizacao int = NULL,
	@nr_cnpj_cpf_fornecedor varchar(15) = NULL,
	@ds_observacao_1 varchar(76) = NULL,
	@ds_observacao_2 varchar(76) = NULL,
	@ds_observacao_3 varchar(76) = NULL,
	@nr_despesa_processo varchar(60) = NULL,
	@ds_nl_retencao_inss varchar(11) = NULL,
	@ds_lista varchar(11) = NULL,
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
	@nr_caucao_guia varchar(6) = NULL,
	@nm_caucao_quota_geral_autorizado_por varchar(35) = NULL,
	@vl_caucao_caucionado int = NULL,
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
	@fl_documento_status bit = NULL,
	@nm_credor varchar(14) = NULL,
	@fl_referencia_digitada bit = null

as
begin

	SET NOCOUNT ON;
	
	begin transaction

		INSERT INTO pagamento.tb_subempenho (
			dt_cadastro
		,	fl_sistema_prodesp
		,	fl_sistema_siafem_siafisico
		,	fl_sistema_siafisico
		,	nr_prodesp
		,	nr_siafem_siafisico
		,	nr_contrato
		,	tb_cenario_id_cenario
		,	nr_empenho_prodesp
		,	cd_tarefa
		,	cd_despesa
		,	vl_realizado
		,	nr_recibo
		,	ds_prazo_pagamento
		,	dt_realizado
		,	cd_cenario_prodesp
		,	nr_ct
		,	nr_empenho_siafem_siafisico
		,	cd_unidade_gestora
		,	cd_gestao
		,	cd_nota_fiscal_prodesp
		,	nr_medicao
		,	tb_natureza_tipo_id_natureza_tipo
		,	vl_valor
		,	cd_evento
		,	dt_emissao
		,	tb_evento_tipo_id_evento_tipo
		,	nr_cnpj_cpf_credor
		,	cd_gestao_credor
		,	nr_ano_medicao
		,	nr_mes_medicao
		,	nr_percentual
		,	tb_regional_id_regional
		,	tb_programa_id_programa
		,	tb_estrutura_id_estrutura
		,	tb_fonte_id_fonte
		,	cd_aplicacao_obra
		,	tb_obra_tipo_id_obra_tipo
		,   cd_obra_tipo
		,	nr_obra
		,	cd_unidade_gestora_obra
		,	cd_credor_organizacao
		,	nr_cnpj_cpf_fornecedor
		,	ds_observacao_1
		,	ds_observacao_2
		,	ds_observacao_3
		,	nr_despesa_processo
		,	ds_nl_retencao_inss
		,	ds_lista
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
		,	nr_caucao_guia
		,	nm_caucao_quota_geral_autorizado_por
		,	vl_caucao_caucionado
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
		,	nm_credor
		,	fl_referencia_digitada
		)
		VALUES 
		(
			@dt_cadastro
		,	@fl_sistema_prodesp
		,	@fl_sistema_siafem_siafisico
		,	@fl_sistema_siafisico
		,	@nr_prodesp
		,	@nr_siafem_siafisico
		,	@nr_contrato
		,	nullif( @tb_cenario_id_cenario, 0 )
		,	@nr_empenho_prodesp
		,	@cd_tarefa
		,	@cd_despesa
		,	@vl_realizado
		,	@nr_recibo
		,	@ds_prazo_pagamento
		,	@dt_realizado
		,	@cd_cenario_prodesp
		,	@nr_ct
		,	@nr_empenho_siafem_siafisico
		,	@cd_unidade_gestora
		,	@cd_gestao
		,	@cd_nota_fiscal_prodesp
		,	@nr_medicao
		,	@tb_natureza_tipo_id_natureza_tipo
		,	@vl_valor
		,	@cd_evento
		,	@dt_emissao
		,	nullif( @tb_evento_tipo_id_evento_tipo, 0 )
		,	@nr_cnpj_cpf_credor
		,	@cd_gestao_credor
		,	@nr_ano_medicao
		,	@nr_mes_medicao
		,	@nr_percentual
		,	nullif( @tb_regional_id_regional, 0 )
		,	nullif( @tb_programa_id_programa, 0 )
		,	nullif( @tb_estrutura_id_estrutura, 0 )
		,	nullif( @tb_fonte_id_fonte, 0 )
		,	@cd_aplicacao_obra
		,	nullif( @tb_obra_tipo_id_obra_tipo, 0 )
		,   @cd_obra_tipo
		,   @nr_obra
		,	@cd_unidade_gestora_obra
		,	@cd_credor_organizacao
		,	@nr_cnpj_cpf_fornecedor
		,	@ds_observacao_1
		,	@ds_observacao_2
		,	@ds_observacao_3
		,	@nr_despesa_processo
		,	@ds_nl_retencao_inss
		,	@ds_lista
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
		,	@nr_caucao_guia
		,	@nm_caucao_quota_geral_autorizado_por
		,	@vl_caucao_caucionado
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
		,	@nm_credor
		,	@fl_referencia_digitada
		);
	
	Commit	
    
    SELECT SCOPE_IDENTITY();
End