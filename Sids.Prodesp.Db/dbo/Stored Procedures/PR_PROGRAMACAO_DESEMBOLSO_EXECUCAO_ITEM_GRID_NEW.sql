CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW] --NULL, NULL, NULL, '2018PD00621'
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
	BEGIN

	CREATE TABLE #tempListaGrid 
		(--pdexecucao
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
		[cd_transmissao_status_siafem] [char](1) NULL,
		[fl_transmissao_transmitido_siafem] [bit] NULL,
		[dt_transmissao_transmitido_siafem] [date] NULL,
		[ds_transmissao_mensagem_siafem] varchar(140) NULL,
		[nr_documento_gerador] varchar(50) NULL, --também na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		cd_transmissao_status_prodesp varchar(1) NULL,
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
		dt_cadastro datetime,
		id_tipo_execucao_pd int
	)

	DECLARE @nr_documento_gerador varchar(50), --também na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@dt_cadastro datetime,
			@id_tipo_execucao_pd int,
			@TransmissaoStatusProdesp char(1)

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_execucao CURSOR FOR

		SELECT	ITEM.[id_programacao_desembolso_execucao_item], ITEM.[nr_agrupamento_pd],ITEM.[id_execucao_pd],ITEM.[ds_numpd],ITEM.[ug],ITEM.[gestao],ITEM.[ug_pagadora],ITEM.[ug_liquidante],ITEM.[gestao_pagadora],ITEM.[gestao_liguidante],
				ITEM.[favorecido],ITEM.[favorecidoDesc],ITEM.[ordem],ITEM.[ano_pd],ITEM.[valor],ITEM.[ds_noup],ITEM.[nr_agrupamento_pd],ITEM.[ds_numob],ITEM.[ob_cancelada],ITEM.[fl_sistema_prodesp],
				ITEM.nr_documento_gerador,ITEM.nr_op,ITEM.[cd_transmissao_status_siafem],ITEM.[fl_transmissao_transmitido_siafem],ITEM.[dt_transmissao_transmitido_siafem],ITEM.[ds_transmissao_mensagem_siafem]
				--ex.[dt_cadastro], 
				--ISNULL(PD.dt_vencimento, PDA.dt_vencimento) AS dt_vencimento, ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) AS nr_cnpj_cpf_credor, ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) AS nr_cnpj_cpf_pgto
		FROM contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) ITEM
		--LEFT JOIN contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		--LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		--LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		--LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		--LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE
		--ITEM.nr_agrupamento_pd <> 0 AND
		(@ob_cancelada is null or ITEM.ob_cancelada = @ob_cancelada) AND
		(@ds_numpd is null or ITEM.ds_numpd LIKE '%' + @ds_numpd + '%') AND
		(@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%') AND
		(@favorecido is null or ITEM.favorecido = @favorecido) AND
		(@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		--(@tipoExecucao is null or Ex.id_tipo_execucao_pd = @tipoExecucao) AND
		(@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_status_siafem = @cd_transmissao_status_siafem)
		--(@cd_transmissao_status_prodesp IS NULL OR ISNULL([pi].cd_transmissao_status_prodesp, 'N') = @cd_transmissao_status_prodesp OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) AND
		--(@de is null or ex.dt_cadastro >= @de ) AND
		--(@ate is null or ex.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	OPEN cursor_execucao

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor_execucao 
	INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
			@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		--SELECT @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd

		----Dados da Confirmação
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@TransmissaoStatusProdesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programação de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao) 
					,@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento) 
					,@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento) 
					,@nr_contrato = PD.nr_contrato 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador 
					,@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) 
					,@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor)
					,@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
					,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

		INSERT	#tempListaGrid
				(id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto, dt_cadastro, id_tipo_execucao_pd)
		VALUES	(@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto, @dt_cadastro, @id_tipo_execucao_pd)

		FETCH NEXT FROM cursor_execucao 
		INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_execucao

	-- Desalocando o cursor
	DEALLOCATE cursor_execucao

	SELECT	* 
	FROM	#tempListaGrid
	WHERE	(@tipoExecucao IS NULL OR id_tipo_execucao_pd = @tipoExecucao)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))


	DROP TABLE #tempListaGrid

	END

	ELSE
	BEGIN

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

END