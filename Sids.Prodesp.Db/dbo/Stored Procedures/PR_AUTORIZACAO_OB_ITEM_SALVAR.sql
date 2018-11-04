
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_SALVAR]   
	@id_autorizacao_ob_item int = NULL,
	@id_autorizacao_ob int = NULL,
	@nr_agrupamento_ob int = NULL,
	@id_execucao_pd int = NULL,
	@id_execucao_pd_item int = NULL,
	@ds_numob varchar(20) = NULL,
	@ds_numop varchar(20) = NULL,
	@id_tipo_documento int = NULL,
	@nr_documento varchar(50) = NULL,
	@nr_contrato varchar(50) = NULL,
	@nr_documento_gerador varchar(50) = NULL,
	@favorecidoDesc varchar(120) = NULL,
	@ug_pagadora varchar(20) = NULL,
	@ug_liquidante varchar(20) = NULL,
	@gestao_pagadora varchar(20) = NULL,
	@gestao_liguidante varchar(20) = NULL,
	@cd_despesa varchar(2) = NULL,
	@nr_banco varchar(30) = NULL,
	@valor varchar(20) = NULL,
	@fl_transmissao_item_siafem bit = NULL,
	@cd_transmissao_item_status_siafem char(1) = NULL,
	@ds_transmissao_item_mensagem_siafem varchar(140) = NULL,
	@dt_confirmacao datetime = NULL,
	@cd_aplicacao_obra varchar(140) = NULL
AS  
BEGIN  

set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_autorizacao_ob_itens
		where	id_autorizacao_ob_item = @id_autorizacao_ob_item
		and		id_autorizacao_ob = @id_autorizacao_ob
	)
	begin
	
		update contaunica.tb_autorizacao_ob_itens set  
			id_autorizacao_ob = @id_autorizacao_ob,
			id_execucao_pd = @id_execucao_pd,
			id_execucao_pd_item = @id_execucao_pd_item,
			nr_agrupamento_ob = @nr_agrupamento_ob,
			ds_numob = @ds_numob, 
			ds_numop = @ds_numop, 
			nr_documento_gerador = @nr_documento_gerador,
			id_tipo_documento = @id_tipo_documento,
			nr_documento = @nr_documento,
			nr_contrato = @nr_contrato,
			favorecidoDesc = @favorecidoDesc,
			ug_pagadora = @ug_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_pagadora = @gestao_pagadora, 
			gestao_liguidante = @gestao_liguidante,  
			cd_despesa = @cd_despesa, 
			nr_banco = @nr_banco, 
			valor = @valor,
			fl_transmissao_item_siafem = @fl_transmissao_item_siafem,
			cd_transmissao_item_status_siafem = @cd_transmissao_item_status_siafem, 
			dt_transmissao_item_transmitido_siafem = GETDATE(), 
			ds_transmissao_item_mensagem_siafem = @ds_transmissao_item_mensagem_siafem, 
			dt_confirmacao = @dt_confirmacao,
			cd_aplicacao_obra = @cd_aplicacao_obra
		WHERE 
			id_autorizacao_ob_item = @id_autorizacao_ob_item 
 
		select @id_autorizacao_ob_item
	end
	else
	begin

		insert into contaunica.tb_autorizacao_ob_itens (
			id_autorizacao_ob,
			id_execucao_pd,
			id_execucao_pd_item,
			nr_agrupamento_ob,
			ds_numob,
			ds_numop,
			nr_documento_gerador,
			id_tipo_documento,
			nr_documento,
			nr_contrato,
			favorecidoDesc,
			ug_pagadora, 
			ug_liquidante, 
			gestao_pagadora, 
			gestao_liguidante, 
			cd_despesa,
			nr_banco,
			valor,
			fl_transmissao_item_siafem,
			cd_transmissao_item_status_siafem, 
			dt_transmissao_item_transmitido_siafem, 
			ds_transmissao_item_mensagem_siafem,
			dt_confirmacao,
			dt_cadastro,
			cd_aplicacao_obra
		)
		values (
			  @id_autorizacao_ob
			, @id_execucao_pd
			, @id_execucao_pd_item
			, @nr_agrupamento_ob
			, @ds_numob
			, @ds_numop
			, @nr_documento_gerador
			, @id_tipo_documento
			, @nr_documento
			, @nr_contrato
			, @favorecidoDesc
			, @ug_pagadora
			, @ug_liquidante
			, @gestao_pagadora
			, @gestao_liguidante
			, @cd_despesa
			, @nr_banco
			, @valor
			, @fl_transmissao_item_siafem
			, @cd_transmissao_item_status_siafem
			, GETDATE() 
			, @ds_transmissao_item_mensagem_siafem
			, @dt_confirmacao
			, GETDATE()
			, @cd_aplicacao_obra
		)			
		select scope_identity();
	end

END

/* 

SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +'('+ CONVERT(VARCHAR(10), ISNULL(CHARACTER_OCTET_LENGTH,0))+') = NULL,' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'-- AND CHARACTER_OCTET_LENGTH IS NOT NULL

*/