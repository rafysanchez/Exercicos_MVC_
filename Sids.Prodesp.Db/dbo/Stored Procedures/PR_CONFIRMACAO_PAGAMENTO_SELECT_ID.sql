
CREATE PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_SELECT_ID
(	
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null
)
AS
BEGIN

	SELECT pag.id_confirmacao_pagamento
      ,pag.id_confirmacao_pagamento_tipo
      ,pag.id_execucao_pd
      ,pag.id_autorizacao_ob
      ,pag.id_tipo_documento
      ,pag.nr_agrupamento
      ,pag.ano_referencia
      ,pag.id_tipo_pagamento
      ,pag.dt_confirmacao
      ,pag.dt_cadastro
      ,pag.dt_modificacao
      ,pag.vr_total_confirmado
      ,pag.cd_transmissao_status_prodesp
      ,pag.fl_transmissao_transmitido_prodesp
      ,pag.dt_transmissao_transmitido_prodesp
      ,pag.ds_transmissao_mensagem_prodesp
      ,pag.dt_preparacao
      ,pag.nr_conta
      ,pag.nr_documento
	  ,item.id_confirmacao_pagamento_item
      ,item.id_confirmacao_pagamento
      ,item.id_execucao_pd
      ,item.id_programacao_desembolso_execucao_item
      ,item.id_autorizacao_ob
      ,item.id_autorizacao_ob_item
      ,item.dt_confirmacao
      ,item.id_tipo_documento
      ,item.nr_documento
      ,item.id_regional
      ,item.id_reclassificacao_retencao
      ,item.id_origem
      ,item.id_despesa_tipo
      ,item.dt_vencimento
      ,item.nr_contrato
      ,item.cd_obra
      ,item.nr_op
      ,item.nr_banco_pagador
      ,item.nr_agencia_pagador
      ,item.nr_conta_pagador
      ,item.nr_fonte_siafem
      ,item.nr_emprenho
      ,item.nr_processo
      ,item.nr_nota_fiscal
      ,item.nr_nl_documento
      ,item.vr_documento
      ,item.nr_natureza_despesa
      ,item.cd_credor_organizacao
      ,item.nr_cnpj_cpf_ug_credor
      ,item.ds_referencia
      ,item.cd_orgao_assinatura
      ,item.nm_reduzido_credor
      ,item.fl_transmissao_transmitido_prodesp
      ,item.cd_transmissao_status_prodesp
      ,item.dt_transmissao_transmitido_prodesp
      ,item.ds_transmissao_mensagem_prodesp
  FROM pagamento.tb_confirmacao_pagamento pag WITH(NOLOCK)
  INNER JOIN pagamento.tb_confirmacao_pagamento_item item WITH(NOLOCK)
  ON pag.id_confirmacao_pagamento = item.id_confirmacao_pagamento
  Where pag.id_confirmacao_pagamento = @id_confirmacao_pagamento
  And item.id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
  End