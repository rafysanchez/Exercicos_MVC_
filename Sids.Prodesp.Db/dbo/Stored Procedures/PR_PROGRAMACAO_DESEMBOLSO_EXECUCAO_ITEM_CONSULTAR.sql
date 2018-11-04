CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tempLista 
		(--pdexecucao
		[NumeroAgrupamentoProgramacaoDesembolso] int NULL, -- Número do agrupamento da programacao de desembolso
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
		[nr_agrupamento_pd] int NULL, -- Número do agrupamento da programacao de desembolso EXECUÇÃO
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
		nr_cnpj_cpf_pgto varchar(15)
	)

	DECLARE @ds_numob varchar(50),
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
			@nr_op varchar(50),
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
			@nr_agrupamento int

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_objects CURSOR FOR

		SELECT	ITEM.id_programacao_desembolso_execucao_item, ITEM.id_execucao_pd, ITEM.ds_numpd, ITEM.ds_numob, ITEM.ob_cancelada, ITEM.ug, ITEM.gestao,ITEM.ug_pagadora, ITEM.ug_liquidante, ITEM.gestao_pagadora, ITEM.gestao_liguidante, 
				ITEM.favorecido, ITEM.favorecidoDesc, ITEM.ordem, ITEM.ano_pd, ITEM.valor, ITEM.ds_noup, ITEM.nr_agrupamento_pd, ITEM.fl_sistema_prodesp, ITEM.cd_transmissao_status_siafem, ITEM.fl_transmissao_transmitido_siafem, 
				ITEM.dt_transmissao_transmitido_siafem, ITEM.ds_transmissao_mensagem_siafem, ITEM.nr_documento_gerador, ITEM.ds_consulta_op_prodesp, ITEM.nr_op 
					
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
	
		WHERE		(@id_programacao_desembolso_execucao_item is null or ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item) AND
	  				(@id_execucao_pd is null or ITEM.id_execucao_pd = @id_execucao_pd) AND
					(@ds_numpd is null or ITEM.ds_numpd = @ds_numpd)
					--AND ITEM.nr_agrupamento_pd <> 0
	
		ORDER BY	ITEM.id_programacao_desembolso_execucao_item

	-- Abrindo Cursor para leitura
	OPEN cursor_objects

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor_objects 
	INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
			@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
			@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @dt_confirmacao = null
		set @id_confirmacao_pagamento = null
		set @id_confirmacao_pagamento_item = null
		set @cd_transmissao_status_prodesp = null
		set @fl_transmissao_transmitido_prodesp = null
		set @ds_transmissao_mensagem_prodesp = null

		--Dados da Confirmação
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@cd_transmissao_status_prodesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].cd_transmissao_status_prodesp ELSE 'E' END, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].ds_transmissao_mensagem_prodesp ELSE 'FCFG404 - PAGAMENTO CONFIRMADO EM ' + CONVERT(VARCHAR, [pi].[dt_transmissao_transmitido_prodesp], 103) END
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	1 = 1
		--AND		id_execucao_pd = @id_execucao_pd
		AND		nr_documento_gerador = @nr_documento_gerador
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programação de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao), 
					@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
					@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
					@nr_contrato = ISNULL(ITEM.nr_contrato, PD.nr_contrato), 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador, 
					@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
					@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
					@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto),
					@nr_agrupamento = ISNULL(PD.nr_agrupamento, PDA.nr_agrupamento)
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		INSERT	#tempLista
				(NumeroAgrupamentoProgramacaoDesembolso, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto)
		VALUES	(@nr_agrupamento, @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto)

		-- Lendo a próxima linha
		FETCH NEXT FROM cursor_objects 
		INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
				@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
				@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_objects

	-- Desalocando o cursor
	DEALLOCATE cursor_objects

	SELECT	NumeroAgrupamentoProgramacaoDesembolso, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
			ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
			nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
			dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto 
	FROM	#tempLista
	
	DROP TABLE #tempLista

END