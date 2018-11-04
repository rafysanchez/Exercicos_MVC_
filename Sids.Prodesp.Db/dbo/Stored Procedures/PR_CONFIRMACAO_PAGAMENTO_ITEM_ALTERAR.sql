 -- ==============================================================
-- Author:  Jose Braz
-- Create date: 30/01/2018
-- Description: Procedure para Alterar pagamento.tb_confirmacao_pagamento_item
-- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]
@id_confirmacao_pagamento_item int = NULL  ,
@id_confirmacao_pagamento int = NULL  ,
@id_execucao_pd int = NULL,
@id_programacao_desembolso_execucao_item int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(19) = NULL,
@dt_confirmacao datetime = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int  = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato varchar(20)  = NULL,
@cd_obra int  = NULL,
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
@vr_total_confirmado decimal(8,2) = NULL,
@ds_referencia nvarchar(100) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL

AS

 SET NOCOUNT ON;

 DECLARE @chave varchar(11),
		@cd_orgao_assinatura varchar(2),
		@nm_reduzido_credor varchar(14)

IF NOT ISNULL(@id_execucao_pd, 0) = 0
BEGIN
	SET @id_origem = 1 --EXEC. PD

	--SELECT	@chave = ds_numpd
	--FROM	contaunica.tb_programacao_desembolso_execucao_item
	--WHERE	id_execucao_pd = @id_execucao_pd
	--AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item	
END
ELSE
	SET @id_origem = 2 --OB

--SELECT		@id_regional = PD.id_regional,
--			@nr_agencia_pagador	= PP.nr_agencia_pgto,
--			@nr_banco_pagador = PP.nr_banco_pgto,
--			@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--			@nr_conta_pagador = PP.nr_conta_pgto,
--			@nr_contrato = PD.nr_contrato,
--			@cd_credor_organizacao = PP.cd_credor_organizacao,
--			@nr_op = PDI.nr_op,
--			@id_tipo_documento = PD.id_tipo_documento,
--			@nr_documento = PD.nr_documento,
--			@id_despesa_tipo = PD.cd_despesa,
--			@dt_vencimento = PD.dt_vencimento,
--			@nr_fonte_siafem = PDE.cd_fonte,
--			@nr_emprenho = SUBSTRING(PD.nr_documento, 1, 9),
--			@nr_processo = PD.nr_processo,
--			@nr_nl_documento = PD.nr_nl_referencia,
--			@vr_documento = PP.vr_documento,
--			@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--			@cd_obra = PD.cd_aplicacao_obra
--FROM		contaunica.tb_programacao_desembolso (NOLOCK) PD
--LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PD.nr_siafem_siafisico
--LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
--LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
--WHERE		nr_siafem_siafisico = @chave

--IF @@ROWCOUNT = 0
--	SELECT		@id_regional = PP.id_regional,
--				@nr_agencia_pagador	= PP.nr_agencia_pgto,
--				@nr_banco_pagador = PP.nr_banco_pgto,
--				@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--				@nr_conta_pagador = PP.nr_conta_pgto,
--				@cd_credor_organizacao = PP.cd_credor_organizacao,
--				@nr_op = PDI.nr_op,
--				@id_tipo_documento = PDA.id_tipo_documento,
--				@nr_documento = PDA.nr_documento,
--				@id_despesa_tipo = PDA.cd_despesa,
--				@dt_vencimento = PDA.dt_vencimento,
--				@nr_fonte_siafem = PDE.cd_fonte,
--				@nr_emprenho = SUBSTRING(PDA.nr_documento, 1, 9),
--				@nr_processo = PDA.nr_processo,
--				@nr_nl_documento = PDA.nr_nl_referencia,
--				@vr_documento = PP.vr_documento,
--				@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--				@nm_reduzido_credor = PDA.nm_reduzido_credor
--	FROM		contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA
--	LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PDA.nr_programacao_desembolso
--	LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
--	LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
--	WHERE		nr_programacao_desembolso = @chave

 IF ISNULL(@id_autorizacao_ob, 0) = 0
 BEGIN

	--UPDATE
	--pagamento.tb_confirmacao_pagamento_item
	--	SET
	----id_confirmacao_pagamento =  @id_confirmacao_pagamento,
	--id_tipo_documento = @id_tipo_documento,
	--nr_documento = @nr_documento,
	--dt_confirmacao = @dt_confirmacao,
	--id_regional =  @id_regional,
	--id_reclassificacao_retencao =  @id_reclassificacao_retencao,
	--id_origem =  @id_origem,
	--id_despesa_tipo =  @id_despesa_tipo,
	--dt_vencimento =  @dt_vencimento,
	--nr_contrato =  @nr_contrato,
	--cd_obra =  @cd_obra,
	--nr_op =  @nr_op,
	--nr_banco_pagador =  @nr_banco_pagador,
	--nr_agencia_pagador =  @nr_agencia_pagador,
	--nr_conta_pagador =  @nr_conta_pagador,
	--nr_fonte_siafem =  @nr_fonte_siafem,
	--nr_emprenho =  @nr_emprenho,
	--nr_processo =  @nr_processo,
	--nr_nota_fiscal =  @nr_nota_fiscal,
	--nr_nl_documento =  @nr_nl_documento,
	--vr_documento =  @vr_documento,
	--nr_natureza_despesa =  @nr_natureza_despesa,
	--cd_credor_organizacao =  @cd_credor_organizacao,
	--nr_cnpj_cpf_ug_credor =  @nr_cnpj_cpf_ug_credor,
	--ds_referencia = @ds_referencia,
	--cd_orgao_assinatura = @cd_orgao_assinatura,
	--nm_reduzido_credor = @nm_reduzido_credor,
	--cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
	--fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,
	--dt_transmissao_transmitido_prodesp = GETDATE(),
	--ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
	--WHERE
	----id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
	----AND id_confirmacao_pagamento = @id_confirmacao_pagamento
	--id_execucao_pd = @id_execucao_pd
	--AND id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

	IF @id_origem = 1
	BEGIN
		--SELECT	*
		--FROM	contaunica.tb_programacao_desembolso_execucao_item
		--WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
	
		DELETE pagamento.tb_confirmacao_pagamento_item WHERE LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		
		INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
					(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
					dt_confirmacao, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
					dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
					nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
					nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
					nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
					fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

		SELECT		@id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PDA.id_tipo_documento, PDA.nr_documento,
					@dt_confirmacao, PDA.id_regional, @id_reclassificacao_retencao, @id_origem, PDA.cd_despesa, 
					PDA.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
					PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PDA.nr_documento, 1, 9), 0), PDA.nr_processo,
					@nr_nota_fiscal, PDA.nr_nl_referencia, ISNULL(PDA.vl_valor/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
					nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, PDA.nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
		FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
		LEFT JOIN	contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA ON PDI.ds_numpd = PDA.nr_programacao_desembolso
		LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
		LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
		WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

		IF @@ROWCOUNT = 0
		BEGIN 
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
					(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
					dt_confirmacao, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
					dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
					nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
					nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
					nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
					fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT	@id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PD.id_tipo_documento, PD.nr_documento,
					@dt_confirmacao, PD.id_regional, @id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
					PD.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
					PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
					@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
					nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
			LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
			LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
			WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		END

	END

END
ELSE
BEGIN

	UPDATE
	pagamento.tb_confirmacao_pagamento_item
		SET
	--id_confirmacao_pagamento =  @id_confirmacao_pagamento,
	id_tipo_documento = @id_tipo_documento,
	nr_documento = @nr_documento,
	dt_confirmacao = @dt_confirmacao,
	id_regional =  @id_regional,
	id_reclassificacao_retencao =  @id_reclassificacao_retencao,
	id_origem =  @id_origem,
	id_despesa_tipo =  @id_despesa_tipo,
	dt_vencimento =  @dt_vencimento,
	nr_contrato =  @nr_contrato,
	cd_obra =  @cd_obra,
	nr_op =  @nr_op,
	nr_banco_pagador =  @nr_banco_pagador,
	nr_agencia_pagador =  @nr_agencia_pagador,
	nr_conta_pagador =  @nr_conta_pagador,
	nr_fonte_siafem =  @nr_fonte_siafem,
	nr_emprenho =  @nr_emprenho,
	nr_processo =  @nr_processo,
	nr_nota_fiscal =  @nr_nota_fiscal,
	nr_nl_documento =  @nr_nl_documento,
	vr_documento =  @vr_documento,
	nr_natureza_despesa =  @nr_natureza_despesa,
	cd_credor_organizacao =  @cd_credor_organizacao,
	nr_cnpj_cpf_ug_credor =  @nr_cnpj_cpf_ug_credor,
	ds_referencia = @ds_referencia,
	cd_orgao_assinatura = @cd_orgao_assinatura,
	nm_reduzido_credor = @nm_reduzido_credor,
	cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
	fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,
	dt_transmissao_transmitido_prodesp = GETDATE(),
	ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
	WHERE
	id_autorizacao_ob = @id_autorizacao_ob
	AND id_autorizacao_ob_item = @id_autorizacao_ob_item
END