-- ===================================================================    
-- Author:		Carlos Henrique Magalhães
-- Create date: 31/03/2017
-- Description: Procedure para salvar ou alterar anulações de rap
-- ===================================================================  
CREATE procedure [dbo].[PR_RAP_ANULACAO_SALVAR]
		
			@id_rap_anulacao int  = NULL,
			@tb_estrutura_id_estrutura int = null
           ,@tb_servico_tipo_id_servico_tipo int = null
           ,@tb_programa_id_programa int = null
           ,@tb_regional_id_regional smallint = null
           ,@nr_siafem_siafisico varchar(11) = null
           ,@nr_prodesp varchar(19) = null
           ,@nr_prodesp_original varchar(15) = null
           ,@nr_empenho_siafem_siafisico varchar(11) = null
           ,@nr_contrato varchar(13) = null
           ,@nr_cnpj_cpf_credor varchar(15) = null
           ,@nr_despesa_processo varchar(60) = null
           ,@nr_recibo varchar(9) = null
           ,@nr_requisicao_rap varchar(17) = null
           ,@cd_unidade_gestora varchar(6) = null
           ,@cd_unidade_gestora_obra varchar(6) = null
           ,@cd_gestao_credor varchar(140) = null
           ,@cd_gestao varchar(5) = null
           ,@cd_aplicacao_obra varchar(8) = null
           ,@nr_medicao varchar(3) = null
           ,@vl_valor int = null
           ,@vl_anulado varchar(20) = null
           ,@cd_nota_fiscal_prodesp varchar(6) = null
           ,@cd_tarefa varchar(2) = null
           ,@nr_classificacao varchar(9) = null
           ,@nr_ano_medicao char(4) = null
           ,@nr_mes_medicao char(2) = null
           ,@ds_prazo_pagamento varchar(3) = null
           ,@dt_realizado date = null
		   ,@dt_emissao date = null
           ,@ds_despesa_autorizado_supra_folha char(4) = null
           ,@ds_observacao_1 varchar(76) = null
           ,@ds_observacao_2 varchar(76) = null
           ,@ds_observacao_3 varchar(76) = null
           ,@ds_despesa_referencia varchar(60) = null
           ,@cd_despesa varchar(2) = null
           ,@cd_despesa_especificacao_despesa char(3) = null
           ,@ds_despesa_especificacao_1 varchar(79) = null
           ,@ds_despesa_especificacao_2 varchar(79) = null
           ,@ds_despesa_especificacao_3 varchar(79) = null
           ,@ds_despesa_especificacao_4 varchar(79) = null
           ,@ds_despesa_especificacao_5 varchar(79) = null
           ,@ds_despesa_especificacao_6 varchar(79) = null
           ,@ds_despesa_especificacao_7 varchar(79) = null
           ,@ds_despesa_especificacao_8 varchar(79) = null
           ,@cd_assinatura_autorizado int = null
           ,@cd_assinatura_autorizado_grupo int = null
           ,@cd_assinatura_autorizado_orgao char(2) = null
           ,@ds_assinatura_autorizado_cargo varchar(55) = null
           ,@nm_assinatura_autorizado varchar(55) = null
           ,@cd_assinatura_examinado int = null
           ,@cd_assinatura_examinado_grupo int = null
           ,@cd_assinatura_examinado_orgao char(2) = null
           ,@ds_assinatura_examinado_cargo varchar(55) = null
           ,@nm_assinatura_examinado varchar(55) = null
           ,@cd_assinatura_responsavel int = null
           ,@cd_assinatura_responsavel_grupo int = null
           ,@cd_assinatura_responsavel_orgao char(2) = null
           ,@ds_assinatura_responsavel_cargo varchar(55) = null
           ,@nm_assinatura_responsavel varchar(55) = null
           ,@vl_saldo_anterior_subempenho varchar(10) = null
           ,@vl_saldo_apos_anulacao varchar(20) = null
           ,@cd_transmissao_status_prodesp char(1) = null
           ,@fl_transmissao_transmitido_prodesp bit = null
           ,@dt_transmissao_transmitido_prodesp date = null
           ,@ds_transmissao_mensagem_prodesp varchar(140) = null
           ,@cd_transmissao_status_siafem_siafisico char(1) = null
           ,@fl_transmissao_transmitido_siafem_siafisico bit = null
           ,@dt_transmissao_transmitido_siafem_siafisico date = null
           ,@dt_cadastro date = null
           ,@ds_transmissao_mensagem_siafem_siafisico varchar(140) = null
           ,@fl_documento_completo bit = null
           ,@fl_documento_status bit = null
           ,@fl_sistema_siafisico bit = null
           ,@cd_transmissao_status_siafisico char(1) = null
           ,@fl_transmissao_transmitido_siafisico bit = null
		   ,@cd_cenario_prodesp varchar(140) = null
		   ,@fl_sistema_prodesp bit = null
		   ,@fl_sistema_siafem_siafisico bit = null
 		
as
begin

	set nocount on;

	if exists (
		select	1 
		from	pagamento.tb_rap_anulacao
		where	id_rap_anulacao = @id_rap_anulacao
	)
	begin

		update pagamento.tb_rap_anulacao set 
		
		tb_estrutura_id_estrutura  = nullif( @tb_estrutura_id_estrutura, 0 )
		,tb_servico_tipo_id_servico_tipo  = nullif( @tb_servico_tipo_id_servico_tipo, 0 )
		,tb_programa_id_programa  = nullif(@tb_programa_id_programa, 0)
		,tb_regional_id_regional  = nullif( @tb_regional_id_regional, 0 )
		,nr_siafem_siafisico 	=	@nr_siafem_siafisico
		,nr_prodesp 	=	@nr_prodesp
		,nr_prodesp_original 	=	@nr_prodesp_original
		,nr_empenho_siafem_siafisico 	=	@nr_empenho_siafem_siafisico
		,nr_contrato 	=	@nr_contrato
		,nr_cnpj_cpf_credor 	=	@nr_cnpj_cpf_credor
		,nr_despesa_processo 	=	@nr_despesa_processo
		,nr_recibo 	=	@nr_recibo
		,nr_requisicao_rap 	=	@nr_requisicao_rap
		,cd_unidade_gestora 	=	@cd_unidade_gestora
		,cd_unidade_gestora_obra 	=	@cd_unidade_gestora_obra
		,cd_gestao_credor 	=	@cd_gestao_credor
		,cd_gestao 	=	@cd_gestao
		,cd_aplicacao_obra 	=	@cd_aplicacao_obra
		,nr_medicao 	=	@nr_medicao
		,vl_valor 	=	@vl_valor
		,vl_anulado 	=	@vl_anulado
		,cd_nota_fiscal_prodesp 	=	@cd_nota_fiscal_prodesp
		,cd_tarefa 	=	@cd_tarefa
		,nr_classificacao 	=	@nr_classificacao
		,nr_ano_medicao 	=	@nr_ano_medicao
		,nr_mes_medicao 	=	@nr_mes_medicao
		,ds_prazo_pagamento 	=	@ds_prazo_pagamento
		,dt_realizado 	=	@dt_realizado
		,dt_emissao =	@dt_emissao
		,ds_despesa_autorizado_supra_folha 	=	@ds_despesa_autorizado_supra_folha
		,ds_observacao_1 	=	@ds_observacao_1
		,ds_observacao_2 	=	@ds_observacao_2
		,ds_observacao_3 	=	@ds_observacao_3
		,ds_despesa_referencia 	=	@ds_despesa_referencia
		,cd_despesa 	=	@cd_despesa
		,cd_despesa_especificacao_despesa 	=	@cd_despesa_especificacao_despesa
		,ds_despesa_especificacao_1 	=	@ds_despesa_especificacao_1
		,ds_despesa_especificacao_2 	=	@ds_despesa_especificacao_2
		,ds_despesa_especificacao_3 	=	@ds_despesa_especificacao_3
		,ds_despesa_especificacao_4 	=	@ds_despesa_especificacao_4
		,ds_despesa_especificacao_5 	=	@ds_despesa_especificacao_5
		,ds_despesa_especificacao_6 	=	@ds_despesa_especificacao_6
		,ds_despesa_especificacao_7 	=	@ds_despesa_especificacao_7
		,ds_despesa_especificacao_8 	=	@ds_despesa_especificacao_8
		,cd_assinatura_autorizado 	=	@cd_assinatura_autorizado
		,cd_assinatura_autorizado_grupo 	=	@cd_assinatura_autorizado_grupo
		,cd_assinatura_autorizado_orgao 	=	@cd_assinatura_autorizado_orgao
		,ds_assinatura_autorizado_cargo 	=	@ds_assinatura_autorizado_cargo
		,nm_assinatura_autorizado 	=	@nm_assinatura_autorizado
		,cd_assinatura_examinado 	=	@cd_assinatura_examinado
		,cd_assinatura_examinado_grupo 	=	@cd_assinatura_examinado_grupo
		,cd_assinatura_examinado_orgao 	=	@cd_assinatura_examinado_orgao
		,ds_assinatura_examinado_cargo 	=	@ds_assinatura_examinado_cargo
		,nm_assinatura_examinado 	=	@nm_assinatura_examinado
		,cd_assinatura_responsavel 	=	@cd_assinatura_responsavel
		,cd_assinatura_responsavel_grupo 	=	@cd_assinatura_responsavel_grupo
		,cd_assinatura_responsavel_orgao 	=	@cd_assinatura_responsavel_orgao
		,ds_assinatura_responsavel_cargo 	=	@ds_assinatura_responsavel_cargo
		,nm_assinatura_responsavel 	=	@nm_assinatura_responsavel
		,vl_saldo_anterior_subempenho 	=	@vl_saldo_anterior_subempenho
		,vl_saldo_apos_anulacao 	=	@vl_saldo_apos_anulacao
		,cd_transmissao_status_prodesp 	=	@cd_transmissao_status_prodesp
		,fl_transmissao_transmitido_prodesp 	=	@fl_transmissao_transmitido_prodesp
		,dt_transmissao_transmitido_prodesp 	=	@dt_transmissao_transmitido_prodesp
		,ds_transmissao_mensagem_prodesp 	=	@ds_transmissao_mensagem_prodesp
		,cd_transmissao_status_siafem_siafisico 	=	@cd_transmissao_status_siafem_siafisico
		,fl_transmissao_transmitido_siafem_siafisico 	=	@fl_transmissao_transmitido_siafem_siafisico
		,dt_transmissao_transmitido_siafem_siafisico 	=	@dt_transmissao_transmitido_siafem_siafisico
		,ds_transmissao_mensagem_siafem_siafisico 	=	@ds_transmissao_mensagem_siafem_siafisico
		,fl_documento_completo 	=	@fl_documento_completo
		,fl_documento_status 	=	@fl_documento_status
		,fl_sistema_siafisico 	=	@fl_sistema_siafisico
		,cd_transmissao_status_siafisico 	=	@cd_transmissao_status_siafisico
		,fl_transmissao_transmitido_siafisico 	=	@fl_transmissao_transmitido_siafisico
		,cd_cenario_prodesp = @cd_cenario_prodesp
		,fl_sistema_prodesp = @fl_sistema_prodesp 
		,fl_sistema_siafem_siafisico = @fl_sistema_siafem_siafisico 

		
		where	id_rap_anulacao = @id_rap_anulacao

		select @id_rap_anulacao;

	end
	else
	begin

		insert into pagamento.tb_rap_anulacao(
				 [tb_estrutura_id_estrutura]
           ,[tb_servico_tipo_id_servico_tipo]
           ,[tb_programa_id_programa]
           ,[tb_regional_id_regional]
           ,[nr_siafem_siafisico]
           ,[nr_prodesp]
           ,[nr_prodesp_original]
           ,[nr_empenho_siafem_siafisico]
           ,[nr_contrato]
           ,[nr_cnpj_cpf_credor]
           ,[nr_despesa_processo]
           ,[nr_recibo]
           ,[nr_requisicao_rap]
           ,[cd_unidade_gestora]
           ,[cd_unidade_gestora_obra]
           ,[cd_gestao_credor]
           ,[cd_gestao]
           ,[cd_aplicacao_obra]
           ,[nr_medicao]
           ,[vl_valor]
           ,[vl_anulado]
           ,[cd_nota_fiscal_prodesp]
           ,[cd_tarefa]
           ,[nr_classificacao]
           ,[nr_ano_medicao]
           ,[nr_mes_medicao]
           ,[ds_prazo_pagamento]
           ,[dt_realizado]
		   ,[dt_emissao]
           ,[ds_despesa_autorizado_supra_folha]
           ,[ds_observacao_1]
           ,[ds_observacao_2]
           ,[ds_observacao_3]
           ,[ds_despesa_referencia]
           ,[cd_despesa]
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
           ,[vl_saldo_anterior_subempenho]
           ,[vl_saldo_apos_anulacao]
           ,[cd_transmissao_status_prodesp]
           ,[fl_transmissao_transmitido_prodesp]
           ,[dt_transmissao_transmitido_prodesp]
           ,[ds_transmissao_mensagem_prodesp]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[dt_cadastro]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[fl_documento_completo]
           ,[fl_documento_status]
           ,[fl_sistema_siafisico]
           ,[cd_transmissao_status_siafisico]
           ,[fl_transmissao_transmitido_siafisico]
		   ,[cd_cenario_prodesp]
		   ,fl_sistema_prodesp 
			,fl_sistema_siafem_siafisico 
			
		)
		values
		(
			  nullif( @tb_estrutura_id_estrutura, 0 )  
			 ,nullif( @tb_servico_tipo_id_servico_tipo, 0 )  
			 ,nullif( @tb_programa_id_programa, 0)  
			 ,nullif( @tb_regional_id_regional, 0 )
           ,@nr_siafem_siafisico 
           ,@nr_prodesp 
           ,@nr_prodesp_original 
           ,@nr_empenho_siafem_siafisico 
           ,@nr_contrato 
           ,@nr_cnpj_cpf_credor 
           ,@nr_despesa_processo 
           ,@nr_recibo 
           ,@nr_requisicao_rap 
           ,@cd_unidade_gestora 
           ,@cd_unidade_gestora_obra 
           ,@cd_gestao_credor 
           ,@cd_gestao 
           ,@cd_aplicacao_obra 
           ,@nr_medicao 
           ,@vl_valor 
           ,@vl_anulado 
           ,@cd_nota_fiscal_prodesp 
           ,@cd_tarefa 
           ,@nr_classificacao 
           ,@nr_ano_medicao 
           ,@nr_mes_medicao 
           ,@ds_prazo_pagamento 
           ,@dt_realizado 
		   ,@dt_emissao
           ,@ds_despesa_autorizado_supra_folha 
           ,@ds_observacao_1 
           ,@ds_observacao_2 
           ,@ds_observacao_3 
           ,@ds_despesa_referencia 
           ,@cd_despesa 
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
           ,@vl_saldo_anterior_subempenho 
           ,@vl_saldo_apos_anulacao 
           ,@cd_transmissao_status_prodesp 
           ,@fl_transmissao_transmitido_prodesp 
           ,@dt_transmissao_transmitido_prodesp 
           ,@ds_transmissao_mensagem_prodesp 
           ,@cd_transmissao_status_siafem_siafisico 
           ,@fl_transmissao_transmitido_siafem_siafisico 
           ,@dt_transmissao_transmitido_siafem_siafisico 
           ,isnull(@dt_cadastro,getdate())
           ,@ds_transmissao_mensagem_siafem_siafisico 
           ,@fl_documento_completo 
           ,@fl_documento_status 
           ,@fl_sistema_siafisico 
           ,@cd_transmissao_status_siafisico 
           ,@fl_transmissao_transmitido_siafisico
		   ,@cd_cenario_prodesp
		   ,@fl_sistema_prodesp 
			,@fl_sistema_siafem_siafisico 
			
		);

		select scope_identity();

	end

end