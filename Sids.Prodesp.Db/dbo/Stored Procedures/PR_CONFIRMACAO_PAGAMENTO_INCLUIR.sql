-- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/33/2018
-- Description: Procedure para Incluir pagamento.tb_confirmacao_pagamento
-- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR]
@id_tipo_documento int  = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@ano_referencia varchar(4) = NULL,
@nr_documento varchar(19)  = NULL,
@id_tipo_pagamento int  = NULL,
@dt_confirmacao datetime  = NULL,
@dt_preparacao datetime  = NULL,
@nr_conta varchar(3) = NULL,
@dt_cadastro datetime = NULL,
@dt_modificacao datetime  = NULL,
@vr_total_confirmado decimal(18,2) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(200) = NULL
AS
BEGIN

	DECLARE @nr_agrupamento_novo INT;

	SET @nr_agrupamento_novo = COALESCE((SELECT MAX(nr_agrupamento) FROM pagamento.tb_confirmacao_pagamento), 0) + 1

	BEGIN TRANSACTION
	SET NOCOUNT ON;

	 IF @cd_transmissao_status_prodesp = 'S'
		SET @vr_total_confirmado = ISNULL(@vr_total_confirmado, 0)
	ELSE
		SET @vr_total_confirmado = 0

	INSERT INTO pagamento.tb_confirmacao_pagamento (
	id_tipo_documento,
	id_execucao_pd,
	id_autorizacao_ob,
	ano_referencia,
	--nr_documento,
	id_tipo_pagamento,
	dt_confirmacao,
	--dt_preparacao,
	--nr_conta,
	dt_cadastro,
	dt_modificacao,
	vr_total_confirmado,
	cd_transmissao_status_prodesp,
	fl_transmissao_transmitido_prodesp,
	dt_transmissao_transmitido_prodesp,
	ds_transmissao_mensagem_prodesp
	,nr_agrupamento
	)
	VALUES(
	@id_tipo_documento,
	@id_execucao_pd,
	@id_autorizacao_ob,
	@ano_referencia,
	--@nr_documento,
	@id_tipo_pagamento,
	@dt_confirmacao,
	--@dt_preparacao,
	--@nr_conta,
	GETDATE(),
	@dt_modificacao,
	@vr_total_confirmado,
	@cd_transmissao_status_prodesp,
	@fl_transmissao_transmitido_prodesp,
	@dt_transmissao_transmitido_prodesp,
	@ds_transmissao_mensagem_prodesp
	,@nr_agrupamento_novo)
	COMMIT
	SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------