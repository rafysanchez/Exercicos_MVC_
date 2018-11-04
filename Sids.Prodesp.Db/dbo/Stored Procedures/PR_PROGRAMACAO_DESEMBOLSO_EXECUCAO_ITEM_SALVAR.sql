-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 06/10/2017
-- Description: Procedure para salvar item de pd
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR]   
@id_programacao_desembolso_execucao_item int = NULL,
@id_execucao_pd int = NULL,
@ds_numpd varchar(40) = NULL,
@nr_documento_gerador varchar(50) = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(20) = NULL,
@nr_contrato varchar(13) = NULL,
@nr_op varchar(50) = NULL,
@id_tipo_pagamento int = NULL,
@dt_confirmacao datetime = NULL,
@ug varchar(20) = NULL,
@gestao varchar(20) = NULL,
@ug_pagadora varchar(20) = NULL,
@ug_liquidante varchar(20) = NULL,
@gestao_pagadora varchar(20) = NULL,
@gestao_liguidante varchar(20) = NULL,
@nr_cnpj_cpf_pgto varchar(15) = NULL,
@favorecido varchar(40) = NULL,
@favorecidoDesc varchar(100) = NULL,
@ordem varchar(20) = NULL,
@ano_pd varchar(20) = NULL,
@valor varchar(20) = NULL,
@ds_noup varchar(2) = NULL,
@nr_agrupamento_pd int = NULL,
@ds_numob varchar(50) = NULL,
@ob_cancelada bit = NULL,
@fl_sistema_prodesp bit = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL,
@cd_transmissao_status_siafem char(1) = NULL,
@fl_transmissao_transmitido_siafem bit = NULL,
@dt_transmissao_transmitido_siafem date = NULL,
@ds_transmissao_mensagem_siafem varchar(280) = NULL,
@ds_consulta_op_prodesp varchar(140) = NULL,
@dt_emissao datetime = NULL,
@dt_vencimento datetime = NULL,
@ds_causa_cancelamento varchar(200) = null
AS  
BEGIN  
set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_execucao_item
		where	id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		--and		nr_documento_gerador = @nr_documento_gerador
		--and		nr_agrupamento_pd = 0
	)
	begin
	
		update contaunica.tb_programacao_desembolso_execucao_item set  
			--id_execucao_pd = @id_execucao_pd, 
			ds_numpd = @ds_numpd,
			nr_documento_gerador = @nr_documento_gerador,
			id_tipo_documento = @id_tipo_documento,
			nr_documento = @nr_documento,
			nr_contrato = @nr_contrato,
			nr_op = @nr_op,
			ug = @ug,
			gestao = @gestao,
			ug_pagadora = @ug_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_pagadora = @gestao_pagadora, 
			gestao_liguidante = @gestao_liguidante, 
			favorecido = @favorecido, 
			favorecidoDesc = @favorecidoDesc,
			nr_cnpj_cpf_pgto = @nr_cnpj_cpf_pgto, 
			ordem = @ordem, 
			ano_pd = @ano_pd, 
			valor = @valor, 
			ds_noup = @ds_noup, 
			nr_agrupamento_pd = @nr_agrupamento_pd, 
			ds_numob = @ds_numob,
			ob_cancelada = @ob_cancelada,
			fl_sistema_prodesp = @fl_sistema_prodesp, 
			cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp, 
			fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp, 
			dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp, 
			ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp, 
			cd_transmissao_status_siafem = @cd_transmissao_status_siafem, 
			fl_transmissao_transmitido_siafem = @fl_transmissao_transmitido_siafem, 
			dt_transmissao_transmitido_siafem = GETDATE(),
			ds_transmissao_mensagem_siafem = @ds_transmissao_mensagem_siafem,
			dt_emissao = @dt_emissao,
			dt_vencimento = @dt_vencimento,
			ds_consulta_op_prodesp = @ds_consulta_op_prodesp,
			id_tipo_pagamento = @id_tipo_pagamento,
			dt_confirmacao = @dt_confirmacao,
			ds_causa_cancelamento = @ds_causa_cancelamento
		WHERE 
			id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
 
		select @id_programacao_desembolso_execucao_item;
	end
	else
	begin

		--Select @nr_agrupamento_pd = ISNULL(nr_agrupamento_pd, 0) + 1 from contaunica.tb_programacao_desembolso_execucao_item

		insert into contaunica.tb_programacao_desembolso_execucao_item (
			id_execucao_pd, 
			ds_numpd,
			nr_documento_gerador,
			id_tipo_documento,
			nr_documento,
			nr_contrato,
			nr_op, 
			ug,
			gestao,
			ug_pagadora, 
			ug_liquidante, 
			gestao_pagadora, 
			gestao_liguidante, 
			favorecido, 
			favorecidoDesc,
			nr_cnpj_cpf_pgto,
			ordem, 
			ano_pd, 
			valor, 
			ds_noup, 
			nr_agrupamento_pd, 
			ds_numob,
			ob_cancelada,
			fl_sistema_prodesp, 
			cd_transmissao_status_prodesp, 
			fl_transmissao_transmitido_prodesp, 
			dt_transmissao_transmitido_prodesp, 
			ds_transmissao_mensagem_prodesp, 
			cd_transmissao_status_siafem, 
			fl_transmissao_transmitido_siafem, 
			dt_transmissao_transmitido_siafem, 
			ds_transmissao_mensagem_siafem,
			ds_consulta_op_prodesp,
			dt_emissao,
			dt_vencimento,
			id_tipo_pagamento,
			dt_confirmacao
		)
		values (
			  @id_execucao_pd
			, @ds_numpd
			, @nr_documento_gerador
			, @id_tipo_documento
			, @nr_documento
			, @nr_contrato
			, @nr_op
			, @ug
			, @gestao
			, @ug_pagadora
			, @ug_liquidante
			, @gestao_pagadora
			, @gestao_liguidante
			, @favorecido
			, @favorecidoDesc
			, @nr_cnpj_cpf_pgto
			, @ordem
			, @ano_pd
			, @valor
			, @ds_noup
			, @nr_agrupamento_pd
			, @ds_numob
			, @ob_cancelada
			, @fl_sistema_prodesp
			, @cd_transmissao_status_prodesp
			, @fl_transmissao_transmitido_prodesp
			, @dt_transmissao_transmitido_prodesp
			, @ds_transmissao_mensagem_prodesp
			, @cd_transmissao_status_siafem
			, @fl_transmissao_transmitido_siafem
			, GETDATE()
			, @ds_transmissao_mensagem_siafem
			, @ds_consulta_op_prodesp
			, @dt_emissao
			, @dt_vencimento
			, @id_tipo_pagamento
			, @dt_confirmacao
		)			
		select scope_identity();
	end

END

/* 

SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +'('+ CONVERT(VARCHAR(10), ISNULL(CHARACTER_OCTET_LENGTH,0))+') = NULL,' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'-- AND CHARACTER_OCTET_LENGTH IS NOT NULL

*/