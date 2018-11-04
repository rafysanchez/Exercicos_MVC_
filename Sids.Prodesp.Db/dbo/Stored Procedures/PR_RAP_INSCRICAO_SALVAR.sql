-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 30/03/2017
-- Description: Procedure para salvar ou alterar inscrições de rap
-- ===================================================================  
CREATE procedure [dbo].[PR_RAP_INSCRICAO_SALVAR]
			@id_rap_inscricao int = null
		   ,@tb_programa_id_programa int = null
           ,@tb_estrutura_id_estrutura int = null
           ,@tb_natureza_tipo_id_natureza_tipo char(1) = null
           ,@tb_servico_tipo_id_servico_tipo int = null
           ,@tb_regional_id_regional smallint = null
           ,@nr_prodesp varchar(13) = null
           ,@nr_siafem_siafisico varchar(11) = null
           ,@nr_empenho_prodesp varchar(15) = null
           ,@nr_empenho_siafem_siafisico varchar(11) = null
           ,@cd_credor_organizacao int = null
           ,@nr_despesa_processo varchar(60) = null
           ,@cd_gestao_credor varchar(140) = null
           ,@cd_unidade_gestora varchar(6) = null
           ,@cd_gestao varchar(5) = null
           ,@cd_nota_fiscal_prodesp varchar(6) = null
           ,@vl_caucao_caucionado int = null
           ,@vl_valor int = null
           ,@vl_realizado int = null
           ,@nr_ano_exercicio int = null
           ,@dt_emissao date = null
           ,@nr_ano_medicao char(4) = null
           ,@nr_mes_medicao char(2) = null
           ,@ds_observacao_1 varchar(76) = null
           ,@ds_observacao_2 varchar(76) = null
           ,@ds_observacao_3 varchar(76) = null
           ,@cd_despesa varchar(2) = null
           ,@ds_despesa_autorizado_supra_folha char(4) = null
           ,@cd_despesa_especificacao_despesa char(3) = null
           ,@ds_despesa_especificacao_1 varchar(79) = null
           ,@ds_despesa_especificacao_2 varchar(79) = null
           ,@ds_despesa_especificacao_3 varchar(79) = null
           ,@ds_despesa_especificacao_4 varchar(79) = null
           ,@ds_despesa_especificacao_5 varchar(79) = null
           ,@ds_despesa_especificacao_6 varchar(79) = null
           ,@ds_despesa_especificacao_7 varchar(79) = null
           ,@ds_despesa_especificacao_8 varchar(79) = null
           ,@cd_assinatura_autorizado varchar(5) = null
           ,@cd_assinatura_autorizado_grupo int = null
           ,@cd_assinatura_autorizado_orgao char(2) = null
           ,@ds_assinatura_autorizado_cargo varchar(55) = null
           ,@nm_assinatura_autorizado varchar(55) = null
           ,@cd_assinatura_examinado varchar(5) = null
           ,@cd_assinatura_examinado_grupo int = null
           ,@cd_assinatura_examinado_orgao char(2) = null
           ,@ds_assinatura_examinado_cargo varchar(55) = null
           ,@nm_assinatura_examinado varchar(55) = null
           ,@cd_assinatura_responsavel varchar(5) = null
           ,@cd_assinatura_responsavel_grupo int = null
           ,@cd_assinatura_responsavel_orgao char(2) = null
           ,@ds_assinatura_responsavel_cargo varchar(55) = null
           ,@nm_assinatura_responsavel varchar(55) = null
           ,@nr_caucao_guia varchar(6) = null
           ,@nm_caucao_quota_geral_autorizado_por varchar(35) = null
           ,@cd_transmissao_status_prodesp char(1) = null
           ,@fl_transmissao_transmitido_prodesp bit = null
           ,@fl_sistema_prodesp bit = null
           ,@dt_transmissao_transmitido_prodesp date = null
           ,@ds_transmissao_mensagem_prodesp varchar(140) = null
           ,@cd_transmissao_status_siafem_siafisico char(1) = null
           ,@fl_transmissao_transmitido_siafem_siafisico bit = null
           ,@fl_sistema_siafem_siafisico bit = null
           ,@dt_transmissao_transmitido_siafem_siafisico date = null
           ,@ds_transmissao_mensagem_siafem_siafisico varchar(140) = null
           ,@fl_documento_completo bit = null
           ,@fl_documento_status bit = null
           ,@dt_cadastro date = null
           ,@cd_aplicacao_obra varchar(8) = null
           ,@nr_contrato varchar(13) = null
           ,@ds_uso_autorizado_por varchar(50) = null
           ,@fl_sistema_siafisico bit = null
           ,@cd_transmissao_status_siafisico char(1) = null
           ,@fl_transmissao_transmitido_siafisico bit = null
		   ,@nr_cnpj_cpf_fornecedor varchar(15) = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	pagamento.tb_rap_inscricao
		where	id_rap_inscricao = @id_rap_inscricao
	)
	begin

		update pagamento.tb_rap_inscricao set 
			[tb_programa_id_programa] = nullif( @tb_programa_id_programa,0)
      ,[tb_estrutura_id_estrutura] = nullif( @tb_estrutura_id_estrutura,0)
      ,[tb_natureza_tipo_id_natureza_tipo] = nullif( @tb_natureza_tipo_id_natureza_tipo,0)
      ,[tb_servico_tipo_id_servico_tipo] = nullif( @tb_servico_tipo_id_servico_tipo,0)
      ,[tb_regional_id_regional] = nullif( @tb_regional_id_regional,0)
      ,[nr_prodesp] = @nr_prodesp
      ,[nr_siafem_siafisico] = @nr_siafem_siafisico
      ,[nr_empenho_prodesp] = @nr_empenho_prodesp
      ,[nr_empenho_siafem_siafisico] = @nr_empenho_siafem_siafisico
      ,[cd_credor_organizacao] = @cd_credor_organizacao
      ,[nr_despesa_processo] = @nr_despesa_processo
      ,[cd_gestao_credor] = @cd_gestao_credor
      ,[cd_unidade_gestora] = @cd_unidade_gestora
      ,[cd_gestao] = @cd_gestao
      ,[cd_nota_fiscal_prodesp] = @cd_nota_fiscal_prodesp
      ,[vl_caucao_caucionado] = @vl_caucao_caucionado
      ,[vl_valor] = @vl_valor
      ,[vl_realizado] = @vl_realizado
      ,[nr_ano_exercicio] = @nr_ano_exercicio
      ,[dt_emissao] = @dt_emissao
      ,[nr_ano_medicao] = @nr_ano_medicao
      ,[nr_mes_medicao] = @nr_mes_medicao
      ,[ds_observacao_1] = @ds_observacao_1
      ,[ds_observacao_2] = @ds_observacao_2
      ,[ds_observacao_3] = @ds_observacao_3
      ,[cd_despesa] = @cd_despesa
      ,[ds_despesa_autorizado_supra_folha] = @ds_despesa_autorizado_supra_folha
      ,[cd_despesa_especificacao_despesa] = @cd_despesa_especificacao_despesa
      ,[ds_despesa_especificacao_1] = @ds_despesa_especificacao_1
      ,[ds_despesa_especificacao_2] = @ds_despesa_especificacao_2
      ,[ds_despesa_especificacao_3] = @ds_despesa_especificacao_3
      ,[ds_despesa_especificacao_4] = @ds_despesa_especificacao_4
      ,[ds_despesa_especificacao_5] = @ds_despesa_especificacao_5
      ,[ds_despesa_especificacao_6] = @ds_despesa_especificacao_6
      ,[ds_despesa_especificacao_7] = @ds_despesa_especificacao_7
      ,[ds_despesa_especificacao_8] = @ds_despesa_especificacao_8
      ,[cd_assinatura_autorizado] = @cd_assinatura_autorizado
      ,[cd_assinatura_autorizado_grupo] = @cd_assinatura_autorizado_grupo
      ,[cd_assinatura_autorizado_orgao] = @cd_assinatura_autorizado_orgao
      ,[ds_assinatura_autorizado_cargo] = @ds_assinatura_autorizado_cargo
      ,[nm_assinatura_autorizado] = @nm_assinatura_autorizado
      ,[cd_assinatura_examinado] = @cd_assinatura_examinado
      ,[cd_assinatura_examinado_grupo] = @cd_assinatura_examinado_grupo
      ,[cd_assinatura_examinado_orgao] = @cd_assinatura_examinado_orgao
      ,[ds_assinatura_examinado_cargo] = @ds_assinatura_examinado_cargo
      ,[nm_assinatura_examinado] = @nm_assinatura_examinado
      ,[cd_assinatura_responsavel] = @cd_assinatura_responsavel
      ,[cd_assinatura_responsavel_grupo] = @cd_assinatura_responsavel_grupo
      ,[cd_assinatura_responsavel_orgao] = @cd_assinatura_responsavel_orgao
      ,[ds_assinatura_responsavel_cargo] = @ds_assinatura_responsavel_cargo
      ,[nm_assinatura_responsavel] = @nm_assinatura_responsavel
      ,[nr_caucao_guia] = @nr_caucao_guia
      ,[nm_caucao_quota_geral_autorizado_por] = @nm_caucao_quota_geral_autorizado_por
      ,[cd_transmissao_status_prodesp] = @cd_transmissao_status_prodesp
      ,[fl_transmissao_transmitido_prodesp] = @fl_transmissao_transmitido_prodesp
      ,[fl_sistema_prodesp] = @fl_sistema_prodesp
      ,[dt_transmissao_transmitido_prodesp] = @dt_transmissao_transmitido_prodesp
      ,[ds_transmissao_mensagem_prodesp] = @ds_transmissao_mensagem_prodesp
      ,[cd_transmissao_status_siafem_siafisico] = @cd_transmissao_status_siafem_siafisico
      ,[fl_transmissao_transmitido_siafem_siafisico] = @fl_transmissao_transmitido_siafem_siafisico
      ,[fl_sistema_siafem_siafisico] = @fl_sistema_siafem_siafisico
      ,[dt_transmissao_transmitido_siafem_siafisico] = @dt_transmissao_transmitido_siafem_siafisico
      ,[ds_transmissao_mensagem_siafem_siafisico] = @ds_transmissao_mensagem_siafem_siafisico
      ,[fl_documento_completo] = @fl_documento_completo
      ,[fl_documento_status] = @fl_documento_status
      ,[cd_aplicacao_obra] = @cd_aplicacao_obra
      ,[nr_contrato] = @nr_contrato
      ,[ds_uso_autorizado_por] = @ds_uso_autorizado_por
      ,[fl_sistema_siafisico] = @fl_sistema_siafisico
      ,[cd_transmissao_status_siafisico] = @cd_transmissao_status_siafisico
      ,[fl_transmissao_transmitido_siafisico] = @fl_transmissao_transmitido_siafisico
	  ,[nr_cnpj_cpf_fornecedor] = @nr_cnpj_cpf_fornecedor
		where	id_rap_inscricao = @id_rap_inscricao;

		select @id_rap_inscricao;

	end
	else
	begin

		insert into pagamento.tb_rap_inscricao(
			[tb_programa_id_programa]
           ,[tb_estrutura_id_estrutura]
           ,[tb_natureza_tipo_id_natureza_tipo]
           ,[tb_servico_tipo_id_servico_tipo]
           ,[tb_regional_id_regional]
           ,[nr_prodesp]
           ,[nr_siafem_siafisico]
           ,[nr_empenho_prodesp]
           ,[nr_empenho_siafem_siafisico]
           ,[cd_credor_organizacao]
           ,[nr_despesa_processo]
           ,[cd_gestao_credor]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[cd_nota_fiscal_prodesp]
           ,[vl_caucao_caucionado]
           ,[vl_valor]
           ,[vl_realizado]
           ,[nr_ano_exercicio]
           ,[dt_emissao]
           ,[nr_ano_medicao]
           ,[nr_mes_medicao]
           ,[ds_observacao_1]
           ,[ds_observacao_2]
           ,[ds_observacao_3]
           ,[cd_despesa]
           ,[ds_despesa_autorizado_supra_folha]
           ,[cd_despesa_especificacao_despesa]
           ,[ds_despesa_especificacao_1]
           ,[ds_despesa_especificacao_2]
           ,[ds_despesa_especificacao_3]
           ,[ds_despesa_especificacao_4]
           ,[ds_despesa_especificacao_5]
           ,[ds_despesa_especificacao_6]
           ,[ds_despesa_especificacao_7]
           ,[ds_despesa_especificacao_8]
           ,[cd_assinatura_autorizado]
           ,[cd_assinatura_autorizado_grupo]
           ,[cd_assinatura_autorizado_orgao]
           ,[ds_assinatura_autorizado_cargo]
           ,[nm_assinatura_autorizado]
           ,[cd_assinatura_examinado]
           ,[cd_assinatura_examinado_grupo]
           ,[cd_assinatura_examinado_orgao]
           ,[ds_assinatura_examinado_cargo]
           ,[nm_assinatura_examinado]
           ,[cd_assinatura_responsavel]
           ,[cd_assinatura_responsavel_grupo]
           ,[cd_assinatura_responsavel_orgao]
           ,[ds_assinatura_responsavel_cargo]
           ,[nm_assinatura_responsavel]
           ,[nr_caucao_guia]
           ,[nm_caucao_quota_geral_autorizado_por]
           ,[cd_transmissao_status_prodesp]
           ,[fl_transmissao_transmitido_prodesp]
           ,[fl_sistema_prodesp]
           ,[dt_transmissao_transmitido_prodesp]
           ,[ds_transmissao_mensagem_prodesp]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[fl_sistema_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[fl_documento_completo]
           ,[fl_documento_status]
           ,[dt_cadastro]
           ,[cd_aplicacao_obra]
           ,[nr_contrato]
           ,[ds_uso_autorizado_por]
           ,[fl_sistema_siafisico]
           ,[cd_transmissao_status_siafisico]
           ,[fl_transmissao_transmitido_siafisico]
		   ,[nr_cnpj_cpf_fornecedor]
		)
		values
		(
			nullif( @tb_programa_id_programa,0)
           ,nullif( @tb_estrutura_id_estrutura,0)
           ,nullif( @tb_natureza_tipo_id_natureza_tipo,0)
           ,nullif( @tb_servico_tipo_id_servico_tipo,0)
           ,nullif( @tb_regional_id_regional,0)
           ,@nr_prodesp
           ,@nr_siafem_siafisico
           ,@nr_empenho_prodesp
           ,@nr_empenho_siafem_siafisico
           ,@cd_credor_organizacao
           ,@nr_despesa_processo
           ,@cd_gestao_credor
           ,@cd_unidade_gestora
           ,@cd_gestao
           ,@cd_nota_fiscal_prodesp
           ,@vl_caucao_caucionado
           ,@vl_valor 
           ,@vl_realizado
           ,@nr_ano_exercicio
           ,@dt_emissao
           ,@nr_ano_medicao
           ,@nr_mes_medicao
           ,@ds_observacao_1
           ,@ds_observacao_2
           ,@ds_observacao_3
           ,@cd_despesa 
           ,@ds_despesa_autorizado_supra_folha
           ,@cd_despesa_especificacao_despesa 
           ,@ds_despesa_especificacao_1 
           ,@ds_despesa_especificacao_2
           ,@ds_despesa_especificacao_3 
           ,@ds_despesa_especificacao_4 
           ,@ds_despesa_especificacao_5 
           ,@ds_despesa_especificacao_6 
           ,@ds_despesa_especificacao_7 
           ,@ds_despesa_especificacao_8 
           ,@cd_assinatura_autorizado 
           ,@cd_assinatura_autorizado_grupo
           ,@cd_assinatura_autorizado_orgao
           ,@ds_assinatura_autorizado_cargo 
           ,@nm_assinatura_autorizado 
           ,@cd_assinatura_examinado 
           ,@cd_assinatura_examinado_grupo 
           ,@cd_assinatura_examinado_orgao 
           ,@ds_assinatura_examinado_cargo 
           ,@nm_assinatura_examinado 
           ,@cd_assinatura_responsavel 
           ,@cd_assinatura_responsavel_grupo 
           ,@cd_assinatura_responsavel_orgao 
           ,@ds_assinatura_responsavel_cargo 
           ,@nm_assinatura_responsavel 
           ,@nr_caucao_guia
           ,@nm_caucao_quota_geral_autorizado_por
           ,@cd_transmissao_status_prodesp 
           ,@fl_transmissao_transmitido_prodesp
           ,@fl_sistema_prodesp 
           ,@dt_transmissao_transmitido_prodesp 
           ,@ds_transmissao_mensagem_prodesp 
           ,@cd_transmissao_status_siafem_siafisico 
           ,@fl_transmissao_transmitido_siafem_siafisico 
           ,@fl_sistema_siafem_siafisico 
           ,@dt_transmissao_transmitido_siafem_siafisico 
           ,@ds_transmissao_mensagem_siafem_siafisico 
           ,@fl_documento_completo 
           ,@fl_documento_status
           ,isnull(@dt_cadastro,getdate())
           ,@cd_aplicacao_obra 
           ,@nr_contrato 
           ,@ds_uso_autorizado_por
           ,@fl_sistema_siafisico 
           ,@cd_transmissao_status_siafisico
           ,@fl_transmissao_transmitido_siafisico
		   ,@nr_cnpj_cpf_fornecedor
			
		);

		select scope_identity();

	end

end