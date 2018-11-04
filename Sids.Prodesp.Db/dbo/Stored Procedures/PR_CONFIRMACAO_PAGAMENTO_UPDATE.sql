
CREATE PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_UPDATE
(
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null,
	  @id_confirmacao_pagamento_tipo int = null,
      @id_execucao_pd int = null,
      @id_autorizacao_ob int = null,
      @id_tipo_documento int = null,
      @nr_agrupamento int  = null,
      @ano_referencia int = null,
      @id_tipo_pagamento int = null,
      @dt_confirmacao datetime = null,	  
      @dt_cadastro datetime = null,
      @dt_modificacao datetime = null,
      @vr_total_confirmado decimal(18,2) = null,
      @cd_transmissao_status_prodesp varchar(50) = null,
      @fl_transmissao_transmitido_prodesp bit = null,
      @dt_transmissao_transmitido_prodesp datetime = null,
      @ds_transmissao_mensagem_prodesp varchar(200) = null,
	  @dt_preparacao datetime = null,
      @nr_conta varchar(20) = null,
      @nr_documento varchar(30) = null,
      @id_programacao_desembolso_execucao_item int = null,
      @id_autorizacao_ob_item int = null,
      @id_regional smallint = null,
      @id_reclassificacao_retencao int = null,
      @id_origem int = null,
	  @id_despesa_tipo int = null,
      @dt_vencimento datetime = null,
      @nr_contrato varchar(13) = null,
      @cd_obra varchar(20) = null,
      @nr_op varchar(50) = null,
      @nr_banco_pagador varchar(10) = null,
      @nr_agencia_pagador varchar(10) = null,
      @nr_conta_pagador varchar(10) = null,
      @nr_fonte_siafem varchar(50) = null,
      @nr_emprenho varchar(50) = null,
      @nr_processo varchar(20)= null,
      @nr_nota_fiscal int = null,
      @nr_nl_documento varchar(20) = null,
      @vr_documento decimal(8,2) = null,
      @nr_natureza_despesa int = null,
      @cd_credor_organizacao int = null,
      @nr_cnpj_cpf_ug_credor varchar(14) = null,
      @ds_referencia nvarchar(100) = null,
      @cd_orgao_assinatura varchar(2) = null,
      @nm_reduzido_credor varchar(14) = null

)
AS      
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  
Begin
UPDATE pagamento.tb_confirmacao_pagamento
   SET id_confirmacao_pagamento_tipo = @id_confirmacao_pagamento_tipo
      ,id_execucao_pd = @id_execucao_pd
      ,id_autorizacao_ob = @id_autorizacao_ob
      ,id_tipo_documento = @id_tipo_documento
      ,nr_agrupamento = @nr_agrupamento
	  ,ano_referencia = @ano_referencia
      ,id_tipo_pagamento = @id_tipo_pagamento
      ,dt_confirmacao = @dt_confirmacao
      ,dt_cadastro = @dt_cadastro
      ,dt_modificacao = @dt_modificacao
      ,vr_total_confirmado = @vr_total_confirmado
      ,cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp
      ,fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp
      ,dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp
      ,ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
      ,dt_preparacao = @dt_preparacao
      ,nr_conta = @nr_conta
      ,nr_documento = @nr_documento
 WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento
End

select * from pagamento.tb_confirmacao_pagamento

Begin
UPDATE pagamento.tb_confirmacao_pagamento_item
   SET id_confirmacao_pagamento = @id_confirmacao_pagamento
      ,id_execucao_pd = @id_execucao_pd
      ,id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
      ,id_autorizacao_ob = @id_autorizacao_ob
      ,id_autorizacao_ob_item = @id_autorizacao_ob_item
      ,dt_confirmacao = @dt_confirmacao
      ,id_tipo_documento = @id_tipo_documento
      ,nr_documento = @nr_documento
      ,id_regional = @id_regional
      ,id_reclassificacao_retencao = @id_reclassificacao_retencao
      ,id_origem = @id_origem
      ,id_despesa_tipo = @id_despesa_tipo
      ,dt_vencimento = @dt_vencimento
      ,nr_contrato = @nr_contrato
      ,cd_obra = @cd_obra
      ,nr_op = @nr_op
      ,nr_banco_pagador = @nr_banco_pagador
      ,nr_agencia_pagador = @nr_agencia_pagador
      ,nr_conta_pagador = @nr_conta_pagador
      ,nr_fonte_siafem = @nr_fonte_siafem
      ,nr_emprenho = @nr_emprenho
      ,nr_processo = @nr_processo
      ,nr_nota_fiscal = @nr_nota_fiscal
      ,nr_nl_documento = @nr_nl_documento
      ,vr_documento = @vr_documento
      ,nr_natureza_despesa = @nr_natureza_despesa
      ,cd_credor_organizacao = @cd_credor_organizacao
      ,nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor
      ,ds_referencia = @ds_referencia
      ,cd_orgao_assinatura = @cd_orgao_assinatura
      ,nm_reduzido_credor = @nm_reduzido_credor
      ,fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp
      ,cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp
      ,dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp
      ,ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
 WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento And id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
 End
 COMMIT
 End
GO

