
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]
	@nr_agrupamento int = NULL,
	@id_autorizacao_ob int = NULL,
	@ds_numob varchar(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
		a.id_autorizacao_ob, 
		a.id_autorizacao_ob_item,
		a.id_execucao_pd as nr_agrupamento_ob,
		a.id_execucao_pd,
		a.id_execucao_pd_item,
		UPPER(a.ds_numob) ds_numob, 
		a.ds_numop, 
		a.nr_documento_gerador, 
		a.id_tipo_documento,
		a.nr_documento,
		a.nr_contrato,
		a.favorecidoDesc, 
		a.cd_despesa, 
		a.nr_banco, 
		a.valor,
		b.id_autorizacao_ob, 
		b.id_tipo_pagamento, 
		b.id_execucao_pd, 
		b.nr_agrupamento, 
		b.ug_pagadora, 
		b.gestao_pagadora, 
		b.ug_liquidante, 
		b.gestao_liquidante, 
		b.unidade_gestora, 
		b.gestao, 
		b.ano_ob, 
		b.valor_total_autorizacao, 
		b.qtde_autorizacao, 
		b.dt_cadastro, 
		a.cd_transmissao_item_status_siafem, 
		a.dt_transmissao_item_transmitido_siafem, 
		a.ds_transmissao_item_mensagem_siafem, 
		a.fl_transmissao_item_siafem,
		ISNULL(cpi.id_confirmacao_pagamento, PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 
		--ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 
		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
		ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,
		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
		ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,
		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,
		a.ds_consulta_op_prodesp,
		a.cd_aplicacao_obra
	FROM [contaunica].[tb_autorizacao_ob_itens] (nolock) a
	INNER JOIN	[contaunica].[tb_autorizacao_ob] b (nolock) ON b.id_autorizacao_ob = a.id_autorizacao_ob
	LEFT JOIN pagamento.tb_confirmacao_pagamento c (nolock) ON c.id_confirmacao_pagamento = b.id_confirmacao_pagamento
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item cpi (nolock) ON cpi.id_autorizacao_ob = a.id_autorizacao_ob and cpi.id_autorizacao_ob_item = a.id_autorizacao_ob_item
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item PI_2 (nolock) ON PI_2.id_execucao_pd = a.id_execucao_pd and PI_2.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
	WHERE 1 = 1
	  	AND (nullif( @id_autorizacao_ob, 0 ) is null or a.id_autorizacao_ob = @id_autorizacao_ob )
		AND (@nr_agrupamento IS NULL OR b.nr_agrupamento = @nr_agrupamento)
		AND (@ds_numob IS NULL OR a.ds_numob = @ds_numob)
END