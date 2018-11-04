---- ==============================================================
---- Author:  Jose Braz
---- Create date: 30/43/2018
---- Description: Procedure para Incluir pagamento.tb_confirmacao_pagamento_item
---- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]
@id_confirmacao_pagamento int,
@id_programacao_desembolso_execucao_item int = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL,
@dt_confirmacao datetime = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(19) = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato varchar(20) = NULL,
@cd_obra varchar(20)  = NULL,
@nr_op varchar(50) = NULL,
@nr_banco_pagador varchar(10) = NULL,
@nr_agencia_pagador varchar(10) = NULL,
@nr_conta_pagador varchar(10) = NULL,
@nr_fonte_siafem varchar(50) = NULL,
@nr_emprenho varchar(50) = NULL,
@nr_processo varchar(20)  = NULL,
@nr_nota_fiscal int  = NULL,
@nr_nl_documento varchar(20)  = NULL,
@vr_documento decimal(8,2) = NULL,
@nr_natureza_despesa int  = NULL,
@cd_credor_organizacao int  = NULL,
@nr_cnpj_cpf_ug_credor varchar(14)  = NULL,
@fl_transmissao bit  = NULL,
@dt_trasmissao datetime  = NULL,
@ds_referencia nvarchar(100) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL
AS
BEGIN
BEGIN TRANSACTION
SET NOCOUNT ON;

DECLARE @chave varchar(11),
		@cd_orgao_assinatura varchar(2),
		@nm_reduzido_credor varchar(14),
		@vl_valor_desdobrado	decimal,
		@EhPDAAgrupamento		bit = 0

	IF @id_origem = 1
	BEGIN
		
		--Verificar se já existe desdobramento confirmado
		IF EXISTS(SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
					AND		id_execucao_pd = @id_execucao_pd)
		BEGIN
			PRINT 'já existe confirmação para o desdobramento ' + LEFT(@nr_documento_gerador, 17)

			--Caso o desdobrado não esteja em algum agrupamento
			IF EXISTS (	SELECT	id_programacao_desembolso_execucao_item 
							FROM	contaunica.tb_programacao_desembolso_execucao_item
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_pd = 0)
			BEGIN
				PRINT 'o desdobramento já possui grupopd'
						
				SELECT	@id_execucao_pd = id_execucao_pd FROM contaunica.tb_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_pd <> 0
				
				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_execucao_pd = @id_execucao_pd,
						id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
				WHERE	nr_documento_gerador = @nr_documento_gerador

				--Atualizar status das mensagens para todos desdobrados
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp,
						cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
						dt_confirmacao = @dt_confirmacao 
				WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_execucao_pd = @id_execucao_pd

			END
			ELSE
			BEGIN
				print 'o documento gerador ' + @nr_documento_gerador + ' ainda não foi agrupado'
				SELECT 0
			END
		END

		IF NOT EXISTS(SELECT	id_confirmacao_pagamento_item
			FROM	pagamento.tb_confirmacao_pagamento_item 
			WHERE	nr_documento_gerador = @nr_documento_gerador)
		BEGIN
			PRINT 'criando confirmacao'

			DECLARE	@nr_cnpj_cpf_credor varchar(15),
			@cd_despesa varchar(20),
			@nr_banco_pgto varchar(30), 
			@nr_agencia_pgto varchar(10), 
			@nr_conta_pgto varchar(15),
			@nr_nl_referencia varchar(11),
			@vl_total decimal(18,2),
			@id_programacao_desembolso int,
			@cd_fonte varchar(10)
				
			print 'meh1'	
			--Dados da Programação de Desembolso (PD)
			SELECT
				@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
				@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
				@nr_contrato = PD.nr_contrato, 
				@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
				@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
				@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento),
				@id_regional = ISNULL(PD.id_regional, PDA.id_regional),
				@cd_despesa = ISNULL(PD.cd_despesa, PDA.cd_despesa),
				@nr_processo = ISNULL(PD.nr_processo, PDA.nr_processo),
				@nr_nl_referencia = ISNULL(PD.nr_nl_referencia, PDA.nr_nl_referencia),
				@vl_total = ISNULL(PD.vl_total, PDA.vl_valor),
				@nm_reduzido_credor = PDA.nm_reduzido_credor,
				@id_programacao_desembolso = ISNULL(PD.id_programacao_desembolso, PDA.id_programacao_desembolso)
			FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
			LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
			LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
			WHERE		ITEM.id_execucao_pd = @id_execucao_pd
			AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

			SELECT		@nr_banco_pgto = PP.nr_banco_pgto, 
						@nr_agencia_pgto = PP.nr_agencia_pgto, 
						@nr_conta_pgto = PP.nr_conta_pgto, 
						@cd_credor_organizacao = PP.cd_credor_organizacao, 
						@cd_orgao_assinatura = PP.cd_orgao_assinatura 
			FROM		contaunica.tb_preparacao_pagamento (NOLOCK) PP
			WHERE		nr_documento = @nr_documento
						
			SELECT		@cd_fonte = PDE.cd_fonte
			FROM		contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE
			WHERE		id_programacao_desembolso = @id_programacao_desembolso
			
			print 'meh2'
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
						nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			VALUES	(@id_confirmacao_pagamento, @id_programacao_desembolso_execucao_item, @id_execucao_pd, @id_tipo_documento, @nr_documento,
					GETDATE(), @nr_documento_gerador, @id_regional, @id_reclassificacao_retencao, @id_origem, @cd_despesa, 
					@dt_vencimento, @nr_contrato, @cd_obra, @nr_op, @nr_banco_pgto, 
					@nr_agencia_pgto, @nr_conta_pgto, @cd_fonte, ISNULL(SUBSTRING(@nr_documento, 1, 9), 0), @nr_processo,
					@nr_nota_fiscal, @nr_nl_referencia, ISNULL(@vl_total/100, 0), @nr_natureza_despesa, @cd_credor_organizacao, 
					@nr_cnpj_cpf_credor, @ds_referencia, @cd_orgao_assinatura, @nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp)

		
		END
	END
	ELSE IF @id_origem = 2
	BEGIN
		PRINT 'já existe confirmação para o desdobramento ' + LEFT(@nr_documento_gerador, 17)

		--Verificar se já existe desdobramento confirmado
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
					AND		id_autorizacao_ob = @id_autorizacao_ob)
		BEGIN
			
			--Caso o desdobrado não esteja em algum agrupamento
			IF EXISTS (	    SELECT	id_autorizacao_ob_item 
							FROM	contaunica.tb_autorizacao_ob_itens
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_ob = 0)
			BEGIN
				PRINT 'o desdobramento já possui grupoob'
				IF (@id_autorizacao_ob IS NULL)
				BEGIN				
					SELECT	@id_autorizacao_ob = id_autorizacao_ob FROM contaunica.tb_autorizacao_ob_itens 
					WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_ob <> 0
				END

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_autorizacao_ob = @id_autorizacao_ob,
						id_autorizacao_ob_item = @id_autorizacao_ob_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_autorizacao_ob = @id_autorizacao_ob

				--Atualizar status das mensagens para todos desdobrados
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp,
						cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
						dt_confirmacao = @dt_confirmacao 
				WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_autorizacao_ob = @id_autorizacao_ob
			END
			ELSE
			BEGIN
				print 'o documento gerador ' + @nr_documento_gerador + ' ainda não foi agrupado'
				SELECT 0
			END		 
		END

		IF NOT EXISTS(SELECT	id_confirmacao_pagamento_item
			FROM	pagamento.tb_confirmacao_pagamento_item 
			WHERE	nr_documento_gerador = @nr_documento_gerador)
		BEGIN
			PRINT '...'
			DELETE pagamento.tb_confirmacao_pagamento_item WHERE LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_execucao_pd, id_programacao_desembolso_execucao_item, id_autorizacao_ob, id_autorizacao_ob_item,
						id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, 
						id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, 
						nr_banco_pagador, nr_agencia_pagador, nr_conta_pagador, 
						nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT		@id_confirmacao_pagamento, @id_execucao_pd, AUI.id_execucao_pd_item, AUI.id_autorizacao_ob, AUI.id_autorizacao_ob_item, 
						PD.id_tipo_documento, PD.nr_documento,
						ISNULL(@dt_confirmacao, GETDATE()), AUI.nr_documento_gerador, PD.id_regional, 
						@id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
						PD.dt_vencimento, @nr_contrato, @cd_obra, AUI.ds_numop, 
						PP.nr_banco_pgto, PP.nr_agencia_pgto, PP.nr_conta_pgto, 
						PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
						@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
						nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
						@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		[contaunica].[tb_autorizacao_ob_itens] (NOLOCK) AUI
			LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numob = AUI.ds_numob AND PDI.id_execucao_pd = AUI.id_execucao_pd
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = AUI.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
			LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
			WHERE 1 = 1
				AND LEFT(AUI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
				AND AUI.id_autorizacao_ob = @id_autorizacao_ob

		END

	END

COMMIT

	IF SCOPE_IDENTITY() IS NOT NULL
	BEGIN
		SELECT SCOPE_IDENTITY() as [id_confirmacao_pagamento_item];
	END
END

-----------------------------------------------------------------------------------------