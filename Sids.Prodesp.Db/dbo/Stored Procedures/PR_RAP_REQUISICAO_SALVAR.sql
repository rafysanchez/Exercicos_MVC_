-- ===================================================================      
-- Author:  Carlos Henrique Magalhães  
-- Create date: 31/03/2017  
-- Description: Procedure para salvar ou alterar requisições de rap  
-- ===================================================================    
CREATE procedure [dbo].[PR_RAP_REQUISICAO_SALVAR]  
    
			@id_rap_requisicao int = NULL,  
			@tb_estrutura_id_estrutura int= null
           ,@tb_natureza_tipo_id_natureza_tipo char(1)= null
           ,@tb_servico_tipo_id_servico_tipo int= null
           ,@tb_regional_id_regional smallint= null
		   ,@tb_programa_id_programa int = null
           ,@nr_prodesp varchar(18)= null
           ,@nr_siafem_siafisico varchar(11)= null
           ,@nr_prodesp_original varchar(15)= null
           ,@nr_empenho_siafem_siafisico varchar(11)= null
           ,@nr_subempenho varchar(15)= null
           ,@nr_cnpj_cpf_credor varchar(15)= null
           ,@cd_gestao_credor varchar(140)= null
           ,@nr_despesa_processo varchar(60)= null
           ,@nr_recibo varchar(9)= null
           ,@nr_contrato varchar(13)= null
           ,@cd_unidade_gestora varchar(6)= null
           ,@cd_aplicacao_obra varchar(8)= null
           ,@cd_unidade_gestora_obra varchar(6)= null
           ,@cd_gestao varchar(5)= null
           ,@cd_gestao_fornecedora varchar(5)= null
           ,@vl_valor int= null
           ,@vl_realizado int= null
           ,@vl_caucao_caucionado int= null
		   ,@nr_caucao_guia varchar(5) = null
           ,@cd_nota_fiscal_prodesp varchar(6)= null
           ,@cd_tarefa varchar(2)= null
		   ,@tarefa varchar(2) = null
           ,@nr_classificacao varchar(9)= null
           ,@nr_ano_medicao char(4)= null
           ,@nr_mes_medicao char(2)= null
           ,@ds_prazo_pagamento varchar(3)= null
           ,@dt_realizado date= null
           ,@ds_despesa_referencia varchar(60)= null
           ,@ds_nl_retencao_inss varchar(11)= null
           ,@ds_lista varchar(11)= null
           ,@ds_despesa_autorizado_supra_folha char(4)= null
           ,@ds_observacao_1 varchar(76)= null
           ,@ds_observacao_2 varchar(76)= null
           ,@ds_observacao_3 varchar(76)= null
           ,@nr_medicao varchar(3)= null
           ,@cd_despesa varchar(2)= null
           ,@cd_despesa_especificacao_despesa char(3)= null
           ,@ds_despesa_especificacao_1 varchar(79)= null
           ,@ds_despesa_especificacao_2 varchar(79)= null
           ,@ds_despesa_especificacao_3 varchar(79)= null
           ,@ds_despesa_especificacao_4 varchar(79)= null
           ,@ds_despesa_especificacao_5 varchar(79)= null
           ,@ds_despesa_especificacao_6 varchar(79)= null
           ,@ds_despesa_especificacao_7 varchar(79)= null
           ,@ds_despesa_especificacao_8 varchar(79)= null
           ,@cd_assinatura_autorizado varchar(5)= null
           ,@cd_assinatura_autorizado_grupo int= null
           ,@cd_assinatura_autorizado_orgao char(2)= null
           ,@ds_assinatura_autorizado_cargo varchar(55)= null
           ,@nm_assinatura_autorizado varchar(55)= null
           ,@cd_assinatura_examinado varchar(5)= null
           ,@cd_assinatura_examinado_grupo int= null
           ,@cd_assinatura_examinado_orgao char(2)= null
           ,@ds_assinatura_examinado_cargo varchar(55)= null
           ,@nm_assinatura_examinado varchar(55)= null
           ,@cd_assinatura_responsavel varchar(5)= null
           ,@cd_assinatura_responsavel_grupo int= null
           ,@cd_assinatura_responsavel_orgao char(2)= null
           ,@ds_assinatura_responsavel_cargo varchar(55)= null
           ,@nm_assinatura_responsavel varchar(55)= null
           ,@nm_caucao_autorizado_por varchar(35)= null
           ,@nm_dados_caucao varchar(6)= null
           ,@cd_transmissao_status_prodesp char(1)= null
           ,@fl_transmissao_transmitido_prodesp bit= null
           ,@fl_sistema_prodesp bit= null
           ,@dt_transmissao_transmitido_prodesp date= null
           ,@ds_transmissao_mensagem_prodesp varchar(140)= null
           ,@cd_transmissao_status_siafem_siafisico char(1)= null
           ,@fl_transmissao_transmitido_siafem_siafisico bit= null
           ,@fl_sistema_siafem_siafisico bit= null
           ,@dt_transmissao_transmitido_siafem_siafisico date= null
           ,@dt_cadastro date= null
           ,@dt_emissao date= null
           ,@ds_transmissao_mensagem_siafem_siafisico varchar(140)= null
           ,@fl_documento_completo bit= null
           ,@fl_documento_status bit= null
           ,@fl_sistema_siafisico bit= null
           ,@cd_transmissao_status_siafisico char(1)= null
           ,@fl_transmissao_transmitido_siafisico bit= null
           ,@cd_cenario_prodesp varchar(140)= null
		   ,@nm_caucao_quota_geral_autorizado_por varchar(35) = null
		   ,@cd_credor_organizacao int = null
		   ,@nr_cnpj_cpf_fornecedor varchar(15) = null
		   ,@dt_vencimento date = null
		   ,@fl_referencia_digitada bit = null
		   ,@nr_empenho varchar(9)= null
   
as  
begin  
  
 set nocount on;  
  
 if exists (  
  select 1   
  from pagamento.tb_rap_requisicao  
  where id_rap_requisicao = @id_rap_requisicao  
 )  
 begin  
  
  update pagamento.tb_rap_requisicao set   
      
       tb_estrutura_id_estrutura  = nullif( @tb_estrutura_id_estrutura, 0 )
      ,tb_natureza_tipo_id_natureza_tipo  = @tb_natureza_tipo_id_natureza_tipo
	  ,tb_servico_tipo_id_servico_tipo  = nullif( @tb_servico_tipo_id_servico_tipo, 0 )
	  ,tb_regional_id_regional  = nullif( @tb_regional_id_regional, 0 )
	  ,[tb_programa_id_programa] = nullif(@tb_programa_id_programa,0)
      ,[nr_prodesp] = @nr_prodesp 
      ,[nr_siafem_siafisico] = @nr_siafem_siafisico 
      ,[nr_prodesp_original] = @nr_prodesp_original 
      ,[nr_empenho_siafem_siafisico] = @nr_empenho_siafem_siafisico 
      ,[nr_subempenho] = @nr_subempenho 
      ,[nr_cnpj_cpf_credor] = @nr_cnpj_cpf_credor 
      ,[cd_gestao_credor] = @cd_gestao_credor 
      ,[nr_despesa_processo] = @nr_despesa_processo 
      ,[nr_recibo] = @nr_recibo 
      ,[nr_contrato] = @nr_contrato 
      ,[cd_unidade_gestora] = @cd_unidade_gestora 
      ,[cd_aplicacao_obra] = @cd_aplicacao_obra 
      ,[cd_unidade_gestora_obra] = @cd_unidade_gestora_obra 
      ,[cd_gestao] = @cd_gestao 
      ,[cd_gestao_fornecedora] = @cd_gestao_fornecedora 
      ,[vl_valor] = @vl_valor 
      ,[vl_realizado] = @vl_realizado 
      ,[vl_caucao_caucionado] = @vl_caucao_caucionado 
      ,[cd_nota_fiscal_prodesp] = @cd_nota_fiscal_prodesp 
      ,[cd_tarefa] = @cd_tarefa 
	  ,[tarefa] = @tarefa
      ,[nr_classificacao] = @nr_classificacao 
      ,[nr_ano_medicao] = @nr_ano_medicao 
      ,[nr_mes_medicao] = @nr_mes_medicao 
      ,[ds_prazo_pagamento] = @ds_prazo_pagamento 
      ,[dt_realizado] = @dt_realizado 
      ,[ds_despesa_referencia] = @ds_despesa_referencia 
      ,[ds_nl_retencao_inss] = @ds_nl_retencao_inss 
      ,[ds_lista] = @ds_lista 
      ,[ds_despesa_autorizado_supra_folha] = @ds_despesa_autorizado_supra_folha 
      ,[ds_observacao_1] = @ds_observacao_1 
      ,[ds_observacao_2] = @ds_observacao_2 
      ,[ds_observacao_3] = @ds_observacao_3 
      ,[nr_medicao] = @nr_medicao 
      ,[cd_despesa] = @cd_despesa 
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
      ,[nm_caucao_autorizado_por] = @nm_caucao_autorizado_por 
      ,[nm_dados_caucao] = @nm_dados_caucao 
      ,[cd_transmissao_status_prodesp] = @cd_transmissao_status_prodesp 
      ,[fl_transmissao_transmitido_prodesp] = @fl_transmissao_transmitido_prodesp 
      ,[fl_sistema_prodesp] = @fl_sistema_prodesp 
      ,[dt_transmissao_transmitido_prodesp] = @dt_transmissao_transmitido_prodesp 
      ,[ds_transmissao_mensagem_prodesp] = @ds_transmissao_mensagem_prodesp 
      ,[cd_transmissao_status_siafem_siafisico] = @cd_transmissao_status_siafem_siafisico 
      ,[fl_transmissao_transmitido_siafem_siafisico] = @fl_transmissao_transmitido_siafem_siafisico 
      ,[fl_sistema_siafem_siafisico] = @fl_sistema_siafem_siafisico 
      ,[dt_transmissao_transmitido_siafem_siafisico] = @dt_transmissao_transmitido_siafem_siafisico 
      ,[dt_emissao] = @dt_emissao 
      ,[ds_transmissao_mensagem_siafem_siafisico] = @ds_transmissao_mensagem_siafem_siafisico 
      ,[fl_documento_completo] = @fl_documento_completo 
      ,[fl_documento_status] = @fl_documento_status 
      ,[fl_sistema_siafisico] = @fl_sistema_siafisico 
      ,[cd_transmissao_status_siafisico] = @cd_transmissao_status_siafisico 
      ,[fl_transmissao_transmitido_siafisico] = @fl_transmissao_transmitido_siafisico 
      ,[cd_cenario_prodesp] = @cd_cenario_prodesp 
	  ,nr_caucao_guia = @nr_caucao_guia
	  ,nm_caucao_quota_geral_autorizado_por = @nm_caucao_quota_geral_autorizado_por
	  ,cd_credor_organizacao = @cd_credor_organizacao 
	  ,nr_cnpj_cpf_fornecedor = @nr_cnpj_cpf_fornecedor
	  ,dt_vencimento = @dt_vencimento
	  ,fl_referencia_digitada = @fl_referencia_digitada
	  ,nr_empenho = @nr_empenho
    
  where id_rap_requisicao = @id_rap_requisicao  
  
  select @id_rap_requisicao;  
  
 end  
 else  
 begin  
  
  insert into pagamento.tb_rap_requisicao(  
			[tb_estrutura_id_estrutura]
           ,[tb_natureza_tipo_id_natureza_tipo]
           ,[tb_servico_tipo_id_servico_tipo]
           ,[tb_regional_id_regional]
		   ,tb_programa_id_programa
           ,[nr_prodesp]
           ,[nr_siafem_siafisico]
           ,[nr_prodesp_original]
           ,[nr_empenho_siafem_siafisico]
           ,[nr_subempenho]
           ,[nr_cnpj_cpf_credor]
           ,[cd_gestao_credor]
           ,[nr_despesa_processo]
           ,[nr_recibo]
           ,[nr_contrato]
           ,[cd_unidade_gestora]
           ,[cd_aplicacao_obra]
           ,[cd_unidade_gestora_obra]
           ,[cd_gestao]
           ,[cd_gestao_fornecedora]
           ,[vl_valor]
           ,[vl_realizado]
           ,[vl_caucao_caucionado]
           ,[cd_nota_fiscal_prodesp]
           ,[cd_tarefa]
		   ,[tarefa]
           ,[nr_classificacao]
           ,[nr_ano_medicao]
           ,[nr_mes_medicao]
           ,[ds_prazo_pagamento]
           ,[dt_realizado]
           ,[ds_despesa_referencia]
           ,[ds_nl_retencao_inss]
           ,[ds_lista]
           ,[ds_despesa_autorizado_supra_folha]
           ,[ds_observacao_1]
           ,[ds_observacao_2]
           ,[ds_observacao_3]
           ,[nr_medicao]
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
           ,[nm_caucao_autorizado_por]
           ,[nm_dados_caucao]
           ,[cd_transmissao_status_prodesp]
           ,[fl_transmissao_transmitido_prodesp]
           ,[fl_sistema_prodesp]
           ,[dt_transmissao_transmitido_prodesp]
           ,[ds_transmissao_mensagem_prodesp]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[fl_sistema_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[dt_cadastro]
           ,[dt_emissao]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[fl_documento_completo]
           ,[fl_documento_status]
           ,[fl_sistema_siafisico]
           ,[cd_transmissao_status_siafisico]
           ,[fl_transmissao_transmitido_siafisico]
           ,[cd_cenario_prodesp] 
		   ,nr_caucao_guia
		   ,nm_caucao_quota_geral_autorizado_por
		   ,cd_credor_organizacao
		   ,nr_cnpj_cpf_fornecedor
		   ,dt_vencimento
		   ,fl_referencia_digitada
		   ,nr_empenho
  )  
  values  
  (  
       nullif( @tb_estrutura_id_estrutura, 0 )    
   ,@tb_natureza_tipo_id_natureza_tipo
   ,nullif( @tb_servico_tipo_id_servico_tipo, 0 )  
   ,nullif( @tb_regional_id_regional, 0 )  
   ,nullif(@tb_programa_id_programa,0)
           ,@nr_prodesp
           ,@nr_siafem_siafisico
           ,@nr_prodesp_original
           ,@nr_empenho_siafem_siafisico
           ,@nr_subempenho
           ,@nr_cnpj_cpf_credor
           ,@cd_gestao_credor
           ,@nr_despesa_processo
           ,@nr_recibo
           ,@nr_contrato
           ,@cd_unidade_gestora
           ,@cd_aplicacao_obra
           ,@cd_unidade_gestora_obra
           ,@cd_gestao
           ,@cd_gestao_fornecedora
           ,@vl_valor
           ,@vl_realizado
           ,@vl_caucao_caucionado
           ,@cd_nota_fiscal_prodesp
           ,@cd_tarefa
		   ,@tarefa
           ,@nr_classificacao
           ,@nr_ano_medicao
           ,@nr_mes_medicao
           ,@ds_prazo_pagamento
           ,@dt_realizado
           ,@ds_despesa_referencia
           ,@ds_nl_retencao_inss
           ,@ds_lista
           ,@ds_despesa_autorizado_supra_folha
           ,@ds_observacao_1
           ,@ds_observacao_2
           ,@ds_observacao_3
           ,@nr_medicao
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
           ,@nm_caucao_autorizado_por
           ,@nm_dados_caucao
           ,@cd_transmissao_status_prodesp
           ,@fl_transmissao_transmitido_prodesp
           ,@fl_sistema_prodesp
           ,@dt_transmissao_transmitido_prodesp
           ,@ds_transmissao_mensagem_prodesp
           ,@cd_transmissao_status_siafem_siafisico
           ,@fl_transmissao_transmitido_siafem_siafisico
           ,@fl_sistema_siafem_siafisico
           ,@dt_transmissao_transmitido_siafem_siafisico 
           ,isnull( @dt_cadastro, sysdatetime() )
           ,@dt_emissao
           ,@ds_transmissao_mensagem_siafem_siafisico
           ,@fl_documento_completo
           ,@fl_documento_status
           ,@fl_sistema_siafisico
           ,@cd_transmissao_status_siafisico
           ,@fl_transmissao_transmitido_siafisico
           ,@cd_cenario_prodesp
		   ,@nr_caucao_guia
		   ,@nm_caucao_quota_geral_autorizado_por
		   ,@cd_credor_organizacao 
		   ,@nr_cnpj_cpf_fornecedor
		   ,@dt_vencimento
		   ,@fl_referencia_digitada
		   ,@nr_empenho
  );  
  
  select scope_identity();  
  
 end  
  
end