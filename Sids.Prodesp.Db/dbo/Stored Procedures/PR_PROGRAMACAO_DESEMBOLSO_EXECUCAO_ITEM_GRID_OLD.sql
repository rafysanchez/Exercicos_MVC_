-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 11/10/2017
-- Description:	Procedure para consulta de execução de pd no grid
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD]
	@tipo int,
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null
AS
BEGIN
	
	SET NOCOUNT ON;

	IF ISNULL(@tipo, 1) = 1
		SELECT DISTINCT
			  ITEM.[id_programacao_desembolso_execucao_item] 
			, ITEM.[nr_agrupamento_pd] 
			, ITEM.[id_execucao_pd] 
			, ITEM.[ds_numpd] 
			, ITEM.[ug] 
			, ITEM.[gestao] 
			, ITEM.[ug_pagadora] 
			, ITEM.[ug_liquidante] 
			, ITEM.[gestao_pagadora] 
			, ITEM.[gestao_liguidante] 
			, ITEM.[favorecido] 
			, ITEM.[favorecidoDesc] 
			, ITEM.[ordem] 
			, ITEM.[ano_pd] 
			, ITEM.[valor] 
			, ITEM.[ds_noup] 
			, ITEM.[nr_agrupamento_pd] 
			, ITEM.[ds_numob] 
			, ITEM.[ob_cancelada]
			, ITEM.[fl_sistema_prodesp] 
			--, ITEM.[cd_transmissao_status_prodesp] 
			--, ITEM.[fl_transmissao_transmitido_prodesp] 
			--, ITEM.[dt_transmissao_transmitido_prodesp] 
			--, ITEM.[ds_transmissao_mensagem_prodesp] 
			, [pi].dt_confirmacao
			, [pi].cd_transmissao_status_prodesp
			, [pi].fl_transmissao_transmitido_prodesp
			, [pi].dt_transmissao_transmitido_prodesp
			, [pi].ds_transmissao_mensagem_prodesp
			, ITEM.nr_documento_gerador
			, ITEM.nr_op
			, ITEM.[cd_transmissao_status_siafem] 
			, ITEM.[fl_transmissao_transmitido_siafem] 
			, ITEM.[dt_transmissao_transmitido_siafem] 
			, ITEM.[ds_transmissao_mensagem_siafem] 
			, ex.[dt_cadastro]
			, ISNULL(PD.dt_vencimento, PDA.dt_vencimento) AS dt_vencimento
			, ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) AS nr_cnpj_cpf_credor
			, ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) AS nr_cnpj_cpf_pgto
		FROM contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) ITEM
		LEFT JOIN contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE
		--ITEM.nr_agrupamento_pd <> 0 AND
		(@ob_cancelada is null or ITEM.ob_cancelada = @ob_cancelada) AND
		(@ds_numpd is null or ITEM.ds_numpd LIKE '%' + @ds_numpd + '%') AND
		(@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%') AND
		(@favorecido is null or ITEM.favorecido = @favorecido) AND
		(@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		(@tipoExecucao is null or Ex.id_tipo_execucao_pd = @tipoExecucao) AND
		(@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_status_siafem = @cd_transmissao_status_siafem) AND
		(@cd_transmissao_status_prodesp IS NULL OR ISNULL([pi].cd_transmissao_status_prodesp, 'N') = @cd_transmissao_status_prodesp OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) AND
		(@de is null or ex.dt_cadastro >= @de ) AND
		(@ate is null or ex.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	ELSE
		SELECT
			ITEM.id_autorizacao_ob
			, ITEM.nr_agrupamento_ob
			, ITEM.[favorecido] 
			, ITEM.[favorecidoDesc] 
			, ITEM_EX.ug_liquidante
			, ITEM_EX.ug_pagadora
			, ITEM_EX.gestao_liguidante
			, ITEM_EX.gestao_pagadora
			--, ITEM.ug_liquidante
			--, ITEM.ug_pagadora
			--, ITEM.gestao_liguidante
			--, ITEM.gestao_pagadora
			--, ITEM.[ordem] 
			--, ITEM.[ano_pd] 
			, ITEM.[valor] 
			--, ITEM.[ds_noup] 
			--, ITEM.[nr_agrupamento_pd] 
			, ITEM.[ds_numob] 
			--, ITEM.[ob_cancelada]
			--, ITEM.[fl_sistema_prodesp]
			, ITEM.fl_transmissao_item_siafem 
			, ITEM.cd_transmissao_item_status_siafem 
			, ITEM.[dt_transmissao_item_transmitido_siafem] 
			, ITEM.[ds_transmissao_item_mensagem_siafem] 

			--, ITEM.[fl_transmissao_transmitido_prodesp] 
			--, ITEM.[dt_transmissao_transmitido_prodesp] 
			--, ITEM.[ds_transmissao_mensagem_prodesp]
			--, [pi].dt_confirmacao
			--, [pi].cd_transmissao_status_prodesp
			--, [pi].fl_transmissao_transmitido_prodesp
			--, [pi].dt_transmissao_transmitido_prodesp
			--, [pi].ds_transmissao_mensagem_prodesp 
			,ISNULL([pi].dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao -- Transmissão Confirmação de Pagto
			,ISNULL([PI].cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp 
			,ISNULL([PI].fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp
			,ISNULL([PI].dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp
			,ISNULL([PI].ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp

			, ITEM.[dt_cadastro]
			--, PD.dt_vencimento
		FROM contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_autorizacao_ob = ITEM.id_autorizacao_ob AND [pi].id_autorizacao_ob_item = ITEM.id_autorizacao_ob_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].tb_programacao_desembolso_execucao_item (NOLOCK) as ITEM_EX ON ITEM_EX.id_execucao_pd = ITEM.id_execucao_pd AND ITEM_EX.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS PI_2 ON PI_2.id_execucao_pd = ITEM.id_execucao_pd AND PI_2.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		
		WHERE
		--ITEM.nr_agrupamento_ob <> 0 AND
		(ITEM.[ds_numob] IS NOT NULL)
		AND (@ug_pagadora is null or ITEM_EX.ug_pagadora = @ug_pagadora)
		AND (@gestao_pagadora is null or ITEM_EX.gestao_pagadora = @gestao_pagadora)
		AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			--(@favorecido is null or ITEM.favorecido = @favorecido) AND
		AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
		AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
		AND (@valor is null or ITEM.valor = @valor)
		AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
		AND (@cd_transmissao_status_prodesp IS NULL OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp OR [pi_2].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp )
		AND (@de is null or ITEM.dt_cadastro >= @de )
		AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
		--AND (@cd_aplicacao_obra IS NULL OR ITEM.cd_aplicacao_obra = @cd_aplicacao_obra)
		AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)

END