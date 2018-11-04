
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_SALVAR] 
    @id_autorizacao_ob int = NULL,
	@id_confirmacao_pagamento int = NULL,
	@id_execucao_pd int = NULL,
	@nr_agrupamento int = NULL,
	@unidade_gestora varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ano_ob varchar(4) = NULL,
	@valor_total_autorizacao decimal(18,2) = NULL,
	@qtde_autorizacao int = NULL,
	@dt_cadastro datetime = NULL,
	@cd_transmissao_status_siafem char(1) = NULL,
	@ds_transmissao_mensagem_siafem varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@ug_pagadora varchar(50) = null,
	@id_tipo_pagamento int = NULL,
	@fl_confirmacao bit = 0,
	@dt_confirmacao datetime = null
as
begin

	set nocount on;

	if exists (
		select	id_autorizacao_ob 
		from	[contaunica].[tb_autorizacao_ob](nolock)
		where	id_autorizacao_ob = @id_autorizacao_ob
	)
	begin
		print 'existe'
		update [contaunica].[tb_autorizacao_ob] set 
		nr_agrupamento = @nr_agrupamento, 
		unidade_gestora = @unidade_gestora, 
		gestao = @gestao, 
		ano_ob = @ano_ob, 
		valor_total_autorizacao = @valor_total_autorizacao, 
		qtde_autorizacao = @qtde_autorizacao, 
		cd_transmissao_status_siafem = @cd_transmissao_status_siafem, 
		dt_transmissao_transmitido_siafem = GETDATE(), 
		ds_transmissao_mensagem_siafem = @ds_transmissao_mensagem_siafem, 
		nr_contrato = @nr_contrato, 
		cd_aplicacao_obra = @cd_aplicacao_obra,
		ug_pagadora = @ug_pagadora,
		id_tipo_pagamento = @id_tipo_pagamento, 
		fl_confirmacao = @fl_confirmacao,
		dt_confirmacao = @dt_confirmacao
		where	id_autorizacao_ob = @id_autorizacao_ob

		select @id_autorizacao_ob;

	end
	else
	begin
		print 'não existe'
		print @gestao
		print '16055'
		insert into [contaunica].[tb_autorizacao_ob] (
			id_confirmacao_pagamento, 
			id_execucao_pd, 
			nr_agrupamento, 
			unidade_gestora, 
			gestao, 
			ano_ob, 
			valor_total_autorizacao, 
			qtde_autorizacao, 
			dt_cadastro, 
			cd_transmissao_status_siafem, 
			dt_transmissao_transmitido_siafem, 
			ds_transmissao_mensagem_siafem, 
			nr_contrato, 
			cd_aplicacao_obra,
			ug_pagadora,
			id_tipo_pagamento, 
			fl_confirmacao,
			dt_confirmacao
		)
		values
		(
			@id_confirmacao_pagamento
			, @id_execucao_pd
			, @nr_agrupamento
			, @unidade_gestora
			, @gestao
			, @ano_ob
			, @valor_total_autorizacao
			, @qtde_autorizacao
			, GETDATE()
			, @cd_transmissao_status_siafem
			, GETDATE()
			, @ds_transmissao_mensagem_siafem
			, @nr_contrato
			, @cd_aplicacao_obra
			, @ug_pagadora
			, @id_tipo_pagamento
			, @fl_confirmacao
			, @dt_confirmacao
		)

		select scope_identity()

	end

end

/*

SELECT '' + COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT 'b.' + COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'


SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'


*/