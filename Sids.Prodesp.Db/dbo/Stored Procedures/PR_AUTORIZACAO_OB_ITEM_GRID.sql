CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_GRID]
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
	@filtro_cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null

AS

BEGIN
	
	SET NOCOUNT ON;

	CREATE TABLE #tempListaAutorizacaoGrid 
		(--ob_autorizacao
		id_autorizacao_ob int NULL,
		id_autorizacao_ob_item int NULL,
		nr_agrupamento_ob int NULL,
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
		[fl_transmissao_item_siafem] [bit] NULL,
		[dt_transmissao_item_transmitido_siafem] [date] NULL,
		[ds_transmissao_item_mensagem_siafem] varchar(140) NULL,
		
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
		id_tipo_execucao_pd int,
		cd_aplicacao_obra varchar(140) NULL
	)

	DECLARE @id_autorizacao_ob int,
			@id_autorizacao_ob_item int,
			@nr_agrupamento_ob int,
			@id_execucao_pd_item int,
			@cd_transmissao_item_status_siafem char(1),
			@nr_documento_gerador varchar(50), --também na PD ou PDA
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
			@TransmissaoStatusProdesp char(1),
			@GestaoPagadora varchar(50),
			@UGPagadora varchar(50),
			@cd_aplicacao_obra varchar(140)


	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_autorizacao CURSOR FOR

		SELECT	ITEM.id_autorizacao_ob, ITEM.id_autorizacao_ob_item, ITEM.nr_agrupamento_ob, ITEM.[favorecido] , ITEM.[favorecidoDesc], ITEM.[valor], ITEM.[ds_numob], ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem]
				, ITEM.id_execucao_pd_item, ITEM.id_execucao_pd, ITEM.[dt_cadastro]
				, ITEM.fl_transmissao_item_siafem, ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem], ITEM.[ds_transmissao_item_mensagem_siafem] 
				, '3377775'
		FROM	contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		JOIN contaunica.tb_autorizacao_ob (NOLOCK) AOB ON AOB.id_autorizacao_ob = ITEM.id_autorizacao_ob
		
		WHERE	
		--ITEM.nr_agrupamento_ob <> 0 AND
			(ITEM.[ds_numob] IS NOT NULL)
		
			AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
			AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
			AND (@valor is null or ITEM.valor = @valor)
			AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
			AND (@de is null or ITEM.dt_cadastro >= @de )
			AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
			AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)
			AND (@filtro_cd_aplicacao_obra is null or ITEM.cd_aplicacao_obra = @filtro_cd_aplicacao_obra)

	OPEN cursor_autorizacao

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor_autorizacao 
	INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem 
					,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
					,@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
	print @cd_aplicacao_obra
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
		AND		id_programacao_desembolso_execucao_item = @id_execucao_pd_item
		
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
					--,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
					,@ug_liquidante = ITEM.ug_liquidante
					,@UGPagadora = ITEM.ug_pagadora
					,@gestao_liguidante = ITEM.gestao_liguidante
					,@GestaoPagadora = ITEM.gestao_pagadora
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_execucao_pd_item

		INSERT	#tempListaAutorizacaoGrid
				(id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro, 
				fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem,
				dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
				,cd_aplicacao_obra)
		VALUES	(@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd_item, @id_execucao_pd, @ds_numpd, @ds_numob, @ug_liquidante, @UGPagadora, @gestao_liguidante, @GestaoPagadora, @dt_cadastro,
				@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem,
				@dt_confirmacao, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp
				,@cd_aplicacao_obra)

		FETCH NEXT FROM cursor_autorizacao 
		INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem
						,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
						,@fl_transmissao_transmitido_siafem, @cd_transmissao_item_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_autorizacao

	-- Desalocando o cursor
	DEALLOCATE cursor_autorizacao

	SELECT	id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro
			,fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem 
			,dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
			, cd_aplicacao_obra
	FROM	#tempListaAutorizacaoGrid
	WHERE	(@ug_pagadora is null or ug_pagadora = @ug_pagadora)
	AND		(@gestao_pagadora is null or gestao_pagadora = @gestao_pagadora)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	DROP TABLE #tempListaAutorizacaoGrid
	
END