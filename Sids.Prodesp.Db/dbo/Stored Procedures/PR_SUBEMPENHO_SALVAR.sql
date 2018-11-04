-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para salvar ou alterar subempenhos
-- ===================================================================  
CREATE procedure [dbo].[PR_SUBEMPENHO_SALVAR] 
	@id_subempenho int,
	@dt_cadastro date = null,
	@fl_sistema_prodesp bit = null,
	@fl_sistema_siafem_siafisico bit = null,
	@fl_sistema_siafisico bit = null,
	@nr_prodesp varchar(13) = null,
	@nr_siafem_siafisico varchar(11) = null,
	@nr_contrato varchar(13) = null,
	@tb_cenario_id_cenario int = null,
	@nr_empenho_prodesp varchar(11) = null,
	@cd_tarefa varchar(2) = null,
	@cd_despesa varchar(2) = null,
	@vl_realizado int = null,
	@nr_recibo varchar(9) = null,
	@ds_prazo_pagamento varchar(3) = null,
	@dt_realizado date = null,
	@dt_vencimento date = null,
	@cd_cenario_prodesp varchar(140) = null,
	@nr_ct varchar(11) = null,
	@nr_empenho_siafem_siafisico varchar(11) = null,
	@cd_unidade_gestora varchar(6) = null,
	@cd_gestao varchar(5) = null,
	@cd_nota_fiscal_prodesp varchar(6) = null,
	@nr_medicao varchar(3) = null,
	@tb_natureza_tipo_id_natureza_tipo char(1) = null,
	@vl_valor int = null,
	@cd_evento int = null,
	@dt_emissao date = null,
	@tb_evento_tipo_id_evento_tipo int = null,
	@nr_cnpj_cpf_credor varchar(15) = null,
	@cd_gestao_credor varchar(140) = null,
	@nr_ano_medicao char(4) = null,
	@nr_mes_medicao char(2) = null,
	@nr_percentual varchar(140) = null,
	@tb_regional_id_regional smallint = null,
	@tb_programa_id_programa int = null,
	@tb_estrutura_id_estrutura int = null,
	@tb_fonte_id_fonte int = null,
	@cd_aplicacao_obra varchar(140) = null,
	@tb_obra_tipo_id_obra_tipo int = null,
	@cd_obra_tipo char(1) = null,
	@nr_obra varchar(9) = null,
	@cd_unidade_gestora_obra varchar(6) = null,
	@cd_credor_organizacao int = null,
	@nr_cnpj_cpf_fornecedor varchar(15) = null,
	@ds_observacao_1 varchar(76) = null,
	@ds_observacao_2 varchar(76) = null,
	@ds_observacao_3 varchar(76) = null,
	@nr_despesa_processo varchar(60) = null,
	@ds_nl_retencao_inss varchar(11) = null,
	@ds_lista varchar(11) = null,
	@ds_despesa_referencia varchar(60) = null,
	@ds_despesa_autorizado_supra_folha char(4) = null,
	@cd_despesa_especificacao_despesa char(3) = null,
	@ds_despesa_especificacao_1 varchar(79) = null,
	@ds_despesa_especificacao_2 varchar(79) = null,
	@ds_despesa_especificacao_3 varchar(79) = null,
	@ds_despesa_especificacao_4 varchar(79) = null,
	@ds_despesa_especificacao_5 varchar(79) = null,
	@ds_despesa_especificacao_6 varchar(79) = null,
	@ds_despesa_especificacao_7 varchar(79) = null,
	@ds_despesa_especificacao_8 varchar(79) = null,
	@ds_despesa_especificacao_9 varchar(79) = null,
	@cd_assinatura_autorizado varchar(5) = null,
	@cd_assinatura_autorizado_grupo int = null,
	@cd_assinatura_autorizado_orgao char(2) = null,
	@ds_assinatura_autorizado_cargo varchar(55) = null,
	@nm_assinatura_autorizado varchar(55) = null,
	@cd_assinatura_examinado varchar(5) = null,
	@cd_assinatura_examinado_grupo int = null,
	@cd_assinatura_examinado_orgao char(2) = null,
	@ds_assinatura_examinado_cargo varchar(55) = null,
	@nm_assinatura_examinado varchar(55) = null,
	@cd_assinatura_responsavel varchar(5) = null,
	@cd_assinatura_responsavel_grupo int = null,
	@cd_assinatura_responsavel_orgao char(2) = null,
	@ds_assinatura_responsavel_cargo varchar(55) = null,
	@nm_assinatura_responsavel varchar(55) = null,
	@nr_caucao_guia varchar(6) = null,
	@nm_caucao_quota_geral_autorizado_por varchar(35) = null,
	@vl_caucao_caucionado int = null,
	@cd_transmissao_status_prodesp char(1) = null,
	@fl_transmissao_transmitido_prodesp bit = null,
	@dt_transmissao_transmitido_prodesp date = null,
	@ds_transmissao_mensagem_prodesp varchar(140) = null,
	@cd_transmissao_status_siafem_siafisico char(1) = null,
	@cd_transmissao_status_siafisico char(1) = null,
	@fl_transmissao_transmitido_siafem_siafisico bit = null,
	@fl_transmissao_transmitido_siafisico bit = null,
	@dt_transmissao_transmitido_siafem_siafisico date = null,
	@ds_transmissao_mensagem_siafem_siafisico varchar(140) = null,
	@fl_documento_completo bit = null,
	@fl_documento_status bit = null,
	@nm_credor varchar(14) = null,
	@fl_referencia_digitada bit = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	pagamento.tb_subempenho 
		where	id_subempenho = @id_subempenho
	)
	begin

		update pagamento.tb_subempenho set 
				fl_sistema_prodesp = @fl_sistema_prodesp
			,	fl_sistema_siafem_siafisico = @fl_sistema_siafem_siafisico
			,	fl_sistema_siafisico = @fl_sistema_siafisico
			,	nr_prodesp = @nr_prodesp
			,	nr_siafem_siafisico = @nr_siafem_siafisico
			,	nr_contrato = @nr_contrato
			,	tb_cenario_id_cenario = nullif( @tb_cenario_id_cenario, 0 )
			,	nr_empenho_prodesp = @nr_empenho_prodesp
			,	cd_tarefa = @cd_tarefa
			,	cd_despesa = @cd_despesa
			,	vl_realizado = @vl_realizado
			,	nr_recibo = @nr_recibo
			,	ds_prazo_pagamento = @ds_prazo_pagamento
			,	dt_realizado= @dt_realizado
			, 	dt_vencimento= @dt_vencimento 
			,	cd_cenario_prodesp = @cd_cenario_prodesp
			,	nr_ct = @nr_ct
			,	nr_empenho_siafem_siafisico = @nr_empenho_siafem_siafisico
			,	cd_unidade_gestora = @cd_unidade_gestora
			,	cd_gestao = @cd_gestao
			,	cd_nota_fiscal_prodesp = @cd_nota_fiscal_prodesp
			,	nr_medicao = @nr_medicao
			,	tb_natureza_tipo_id_natureza_tipo = @tb_natureza_tipo_id_natureza_tipo
			,	vl_valor = @vl_valor
			,	cd_evento = @cd_evento
			,	dt_emissao = @dt_emissao
			,	tb_evento_tipo_id_evento_tipo = nullif( @tb_evento_tipo_id_evento_tipo, 0 )
			,	nr_cnpj_cpf_credor = @nr_cnpj_cpf_credor
			,	cd_gestao_credor = @cd_gestao_credor
			,	nr_ano_medicao = @nr_ano_medicao
			,	nr_mes_medicao = @nr_mes_medicao
			,	nr_percentual = @nr_percentual
			,	tb_regional_id_regional = nullif( @tb_regional_id_regional, 0 )
			,	tb_programa_id_programa = nullif( @tb_programa_id_programa, 0 )
			,	tb_estrutura_id_estrutura = nullif( @tb_estrutura_id_estrutura, 0 )
			,	tb_fonte_id_fonte = nullif( @tb_fonte_id_fonte, 0 )
			,	cd_aplicacao_obra = @cd_aplicacao_obra
			,	tb_obra_tipo_id_obra_tipo = nullif( @tb_obra_tipo_id_obra_tipo, 0 )
			,   cd_obra_tipo = @cd_obra_tipo
			,   nr_obra = @nr_obra
			,	cd_unidade_gestora_obra = @cd_unidade_gestora_obra
			,	cd_credor_organizacao = @cd_credor_organizacao
			,	nr_cnpj_cpf_fornecedor = @nr_cnpj_cpf_fornecedor
			,	ds_observacao_1 = @ds_observacao_1
			,	ds_observacao_2 = @ds_observacao_2
			,	ds_observacao_3 = @ds_observacao_3
			,	nr_despesa_processo = @nr_despesa_processo
			,	ds_nl_retencao_inss = @ds_nl_retencao_inss
			,	ds_lista = @ds_lista
			,	ds_despesa_referencia = @ds_despesa_referencia
			,	ds_despesa_autorizado_supra_folha = @ds_despesa_autorizado_supra_folha
			,	cd_despesa_especificacao_despesa = @cd_despesa_especificacao_despesa
			,	ds_despesa_especificacao_1 = @ds_despesa_especificacao_1
			,	ds_despesa_especificacao_2 = @ds_despesa_especificacao_2
			,	ds_despesa_especificacao_3 = @ds_despesa_especificacao_3
			,	ds_despesa_especificacao_4 = @ds_despesa_especificacao_4
			,	ds_despesa_especificacao_5 = @ds_despesa_especificacao_5
			,	ds_despesa_especificacao_6 = @ds_despesa_especificacao_6
			,	ds_despesa_especificacao_7 = @ds_despesa_especificacao_7
			,	ds_despesa_especificacao_8 = @ds_despesa_especificacao_8
			,	ds_despesa_especificacao_9 = @ds_despesa_especificacao_9
			,	cd_assinatura_autorizado = @cd_assinatura_autorizado
			,	cd_assinatura_autorizado_grupo = @cd_assinatura_autorizado_grupo
			,	cd_assinatura_autorizado_orgao = @cd_assinatura_autorizado_orgao
			,	ds_assinatura_autorizado_cargo = @ds_assinatura_autorizado_cargo
			,	nm_assinatura_autorizado = @nm_assinatura_autorizado
			,	cd_assinatura_examinado = @cd_assinatura_examinado
			,	cd_assinatura_examinado_grupo = @cd_assinatura_examinado_grupo
			,	cd_assinatura_examinado_orgao = @cd_assinatura_examinado_orgao
			,	ds_assinatura_examinado_cargo = @ds_assinatura_examinado_cargo
			,	nm_assinatura_examinado = @nm_assinatura_examinado
			,	cd_assinatura_responsavel = @cd_assinatura_responsavel
			,	cd_assinatura_responsavel_grupo = @cd_assinatura_responsavel_grupo
			,	cd_assinatura_responsavel_orgao = @cd_assinatura_responsavel_orgao
			,	ds_assinatura_responsavel_cargo = @ds_assinatura_responsavel_cargo
			,	nm_assinatura_responsavel = @nm_assinatura_responsavel
			,	nr_caucao_guia = @nr_caucao_guia
			,	nm_caucao_quota_geral_autorizado_por = @nm_caucao_quota_geral_autorizado_por
			,	vl_caucao_caucionado = @vl_caucao_caucionado
			,	cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp
			,	fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp
			,	dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp
			,	ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
			,	cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico
			,	cd_transmissao_status_siafisico = @cd_transmissao_status_siafisico
			,	fl_transmissao_transmitido_siafem_siafisico = @fl_transmissao_transmitido_siafem_siafisico
			,	fl_transmissao_transmitido_siafisico = @fl_transmissao_transmitido_siafisico
			,	dt_transmissao_transmitido_siafem_siafisico = @dt_transmissao_transmitido_siafem_siafisico
			,	ds_transmissao_mensagem_siafem_siafisico = @ds_transmissao_mensagem_siafem_siafisico
			,	fl_documento_completo = @fl_documento_completo
			,	fl_documento_status = @fl_documento_status
			,   nm_credor = @nm_credor
			,	fl_referencia_digitada = @fl_referencia_digitada
			
		where	id_subempenho = @id_subempenho;

		select @id_subempenho;

	end
	else
	begin

		insert into pagamento.tb_subempenho (
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
			,	dt_vencimento 
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
			,   fl_referencia_digitada
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
			,	@nr_empenho_prodesp
			,	@cd_tarefa
			,	@cd_despesa
			,	@vl_realizado
			,	@nr_recibo
			,	@ds_prazo_pagamento
			,	@dt_realizado
			,   @dt_vencimento 
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
			,	@nr_obra 
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
			,   @nm_credor
			,   @fl_referencia_digitada
		);

		select scope_identity();

	end

end