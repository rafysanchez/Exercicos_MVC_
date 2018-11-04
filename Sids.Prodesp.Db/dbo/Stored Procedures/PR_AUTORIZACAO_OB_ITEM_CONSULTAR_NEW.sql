CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW]
--DECLARE
	@nr_agrupamento int = NULL,
	@id_autorizacao_ob int = 82,
	@ds_numob varchar(20) = NULL

AS
BEGIN
	/*

	SELECT		a.id_autorizacao_ob, 		a.id_autorizacao_ob_item,		a.nr_agrupamento_ob,		a.id_execucao_pd,		a.id_execucao_pd_item,		a.ds_numob, 		a.ds_numop, 		a.nr_documento_gerador, 		a.id_tipo_documento,
		a.nr_documento,		a.nr_contrato,		a.favorecidoDesc, 		a.cd_despesa, 		a.nr_banco, 		a.valor,		b.id_autorizacao_ob, 		b.id_tipo_pagamento, 		b.id_execucao_pd, 		b.nr_agrupamento, 		b.ug_pagadora, 
		b.gestao_pagadora, 		b.ug_liquidante, 		b.gestao_liquidante, 		b.unidade_gestora, 		b.gestao, 		b.ano_ob, 		b.valor_total_autorizacao, 		b.qtde_autorizacao, 		b.dt_cadastro, 		b.cd_aplicacao_obra,
		a.cd_transmissao_item_status_siafem, 		a.dt_transmissao_item_transmitido_siafem, 		a.ds_transmissao_item_mensagem_siafem, 	a.fl_transmissao_item_siafem,		ISNULL(cpi.id_confirmacao_pagamento, 
		PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 		ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
		ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
		ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,		a.ds_consulta_op_prodesp
	FROM [contaunica].[tb_autorizacao_ob_itens] (nolock) a
	INNER JOIN	[contaunica].[tb_autorizacao_ob] b (nolock) ON b.id_autorizacao_ob = a.id_autorizacao_ob
	LEFT JOIN pagamento.tb_confirmacao_pagamento c (nolock) ON c.id_confirmacao_pagamento = b.id_confirmacao_pagamento
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item cpi (nolock) ON cpi.id_autorizacao_ob = a.id_autorizacao_ob and cpi.id_autorizacao_ob_item = a.id_autorizacao_ob_item
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item PI_2 (nolock) ON PI_2.id_execucao_pd = a.id_execucao_pd and PI_2.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
		--WHERE
		--	ds_numob IS NOT NULL AND 
	  	--	(@nr_agrupamento_pd IS NULL OR nr_agrupamento_pd = @nr_agrupamento_pd)
		WHERE 
	  		--( nullif( @id_programacao_desembolso_execucao_item, 0 ) is null or id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item ) AND
	  		( nullif( @id_autorizacao_ob, 0 ) is null or a.id_autorizacao_ob = @id_autorizacao_ob ) AND
			(@nr_agrupamento IS NULL OR b.nr_agrupamento = @nr_agrupamento) AND
			(@ds_numob IS NULL OR a.ds_numob = @ds_numob)
		--ORDER BY 
			--id_programacao_desembolso_execucao_item
			
	*/


	SET NOCOUNT ON;

	CREATE TABLE #tempConultaItemOB 
		(--autorizacaoOB
		id_autorizacao_ob int NULL,
		id_autorizacao_ob_item int NULL,
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		
		[cd_transmissao_item_status_siafem] [char](1) NULL,
		fl_transmissao_item_siafem [bit] NULL,
		dt_transmissao_item_transmitido_siafem [date] NULL,
		ds_transmissao_item_mensagem_siafem varchar(140) NULL,
		
		[nr_documento_gerador] varchar(50) NULL, --também na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[ds_numop] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		
		[cd_transmissao_status_prodesp] varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		cd_despesa varchar(2),
		nr_banco varchar(30)
	)

	DECLARE @id_autorizacao_ob_item int,
			@nr_agrupamento_ob int,
			@id_execucao_pd int,
			@id_execucao_pd_item int,
			@ob_cancelada bit,
			@ug varchar(50),
			@gestao varchar(50),
			@ug_pagadora varchar(50),
			@ug_liquidante varchar(50),
			@gestao_pagadora varchar(50),
			@gestao_liguidante varchar(50),
			@favorecido varchar(20),
			@favorecidoDesc varchar(120),
			@ordem varchar(50),
			@ano_pd varchar(4),
			@valor varchar(50),
			@ds_noup varchar(1),
			@nr_agrupamento_pd int,
			@fl_sistema_prodesp bit,
			@cd_transmissao_status_siafem char(1),
			@fl_transmissao_transmitido_siafem bit,
			@dt_transmissao_transmitido_siafem date,
			@ds_transmissao_mensagem_siafem varchar(140),
			@nr_documento_gerador varchar(50), --também na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@ds_numop varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@cd_transmissao_status_prodesp varchar(1),
			@fl_transmissao_transmitido_prodesp bit,
			@dt_transmissao_transmitido_prodesp datetime,
			@ds_transmissao_mensagem_prodesp varchar(140),
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@nr_contrato varchar(13), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@cd_despesa varchar(2),
			@nr_banco varchar(30)
			

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_ItemOB CURSOR FOR

		SELECT		a.id_autorizacao_ob, a.id_autorizacao_ob_item, a.nr_agrupamento_ob, a.id_execucao_pd, a.id_execucao_pd_item, a.ds_numob, a.ds_numop, a.nr_documento_gerador, a.id_tipo_documento, a.nr_documento, a.nr_contrato, a.favorecidoDesc,
					a.cd_despesa, a.nr_banco, a.valor,		
					a.cd_transmissao_item_status_siafem, a.dt_transmissao_item_transmitido_siafem, a.ds_transmissao_item_mensagem_siafem

					--b.id_autorizacao_ob, b.id_tipo_pagamento, b.id_execucao_pd, b.nr_agrupamento, b.ug_pagadora, b.gestao_pagadora, b.ug_liquidante, b.gestao_liquidante, b.unidade_gestora, 		b.gestao, 		b.ano_ob, 		b.valor_total_autorizacao, 		b.qtde_autorizacao, 		b.dt_cadastro, 		b.cd_aplicacao_obra,
					--ISNULL(cpi.id_confirmacao_pagamento, 
					--PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 		ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
					--ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
					--ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,		a.ds_consulta_op_prodesp

		FROM		[contaunica].[tb_autorizacao_ob_itens] (nolock) a
		--LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (nolock) b on b.id_execucao_pd = a.id_execucao_pd and b.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
		WHERE		(@id_autorizacao_ob IS NULL OR a.id_autorizacao_ob = @id_autorizacao_ob) AND
					(@ds_numob IS NULL OR a.ds_numob = @ds_numob)

	-- Abrindo Cursor para leitura
	OPEN cursor_ItemOB

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor_ItemOB 
	INTO	@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd, @id_execucao_pd_item, @ds_numob, @ds_numop, @nr_documento_gerador, @id_tipo_documento, @nr_documento, @nr_contrato, @favorecidoDesc,
			@cd_despesa, @nr_banco, @valor, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
			
	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		--SELECT @id_execucao_pd, @id_execucao_pd_item

		--Dados da Confirmação
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@cd_transmissao_status_prodesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_execucao_pd_item
		--AND		id_autorizacao_ob_item = @id_autorizacao_ob_item
		
		IF @dt_confirmacao IS NULL
		BEGIN
			
			SELECT	@dt_confirmacao = [pi].dt_confirmacao,
					@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
					@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
					@cd_transmissao_status_prodesp = [pi].cd_transmissao_status_prodesp, 
					@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
					@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
					@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
			FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
			WHERE	id_autorizacao_ob = @id_autorizacao_ob
			AND		id_autorizacao_ob_item = @id_autorizacao_ob_item
		END

		--Dados da Programação de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao), 
					@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
					@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
					@nr_contrato = PD.nr_contrato, 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador, 
					@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
					@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
					@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_execucao_pd_item
				
		INSERT	#tempConultaItemOB
				(id_autorizacao_ob, id_autorizacao_ob_item, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_item_status_siafem, fl_transmissao_item_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				ds_numop, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto, cd_despesa, nr_banco)
		VALUES	(@id_autorizacao_ob, @id_autorizacao_ob_item, @id_execucao_pd_item, @id_execucao_pd, @ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@ds_numop, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto, @cd_despesa, @nr_banco)

		-- Lendo a próxima linha
		FETCH NEXT FROM cursor_ItemOB 
		INTO	@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd, @id_execucao_pd_item, @ds_numob, @ds_numop, @nr_documento_gerador, @id_tipo_documento, @nr_documento, @nr_contrato, @favorecidoDesc,
				@cd_despesa, @nr_banco, @valor, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem

	END

	-- Fechando Cursor para leitura
	CLOSE cursor_ItemOB

	-- Desalocando o cursor
	DEALLOCATE cursor_ItemOB

	SELECT * FROM #tempConultaItemOB
	--SELECT * FROM pagamento.tb_confirmacao_pagamento_item WHERE id_autorizacao_ob = @id_autorizacao_ob
	--SELECT * FROM pagamento.tb_confirmacao_pagamento_item WHERE id_execucao_pd IN (563)-- and id_programacao_desembolso_execucao_item IN (2186,2190,2203) and nr_documento_gerador IS NOT NULL
	DROP TABLE #tempConultaItemOB

END