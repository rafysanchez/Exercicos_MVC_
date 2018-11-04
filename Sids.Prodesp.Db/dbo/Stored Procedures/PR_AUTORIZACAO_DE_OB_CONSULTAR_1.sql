
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_DE_OB_CONSULTAR]
	@id_autorizacao_ob int = NULL,
	@ds_numob varchar(12) = NULL 
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			A.[id_autorizacao_ob], 
			B.nr_agrupamento_ob,
			CP.[id_confirmacao_pagamento], 
			A.[id_execucao_pd], 
			A.[nr_agrupamento], 
			A.[ug_pagadora], 
			A.[gestao_pagadora], 
			A.[ug_liquidante], 
			A.[gestao_liquidante], 
			A.[unidade_gestora], 
			A.[gestao], 
			A.[ano_ob], 
			A.[valor_total_autorizacao], 
			A.[qtde_autorizacao], 
			A.[dt_cadastro], 
			A.[nr_contrato], 
			A.[cd_aplicacao_obra],
			B.[id_autorizacao_ob_item], 
			B.[id_autorizacao_ob], 
			B.[ds_numob], 
			B.[ds_numop], 
			B.[nr_documento_gerador], 
			B.[favorecidoDesc], 
			B.[cd_despesa], 
			B.[nr_banco], 
			B.[valor],
			B.[cd_transmissao_item_status_siafem], 
			B.[dt_transmissao_item_transmitido_siafem], 
			B.[ds_transmissao_item_mensagem_siafem],
			A.cd_transmissao_status_siafem,
			A.ds_transmissao_mensagem_siafem,
			A.dt_transmissao_transmitido_siafem,
			A.fl_transmissao_siafem,
			A.[id_tipo_pagamento], 
			A.[fl_confirmacao],
			ISNULL(A.[dt_confirmacao], CP.dt_confirmacao) as dt_confirmacao
	  FROM contaunica.tb_autorizacao_ob (nolock) A
	  LEFT JOIN contaunica.tb_autorizacao_ob_itens (nolock) B ON B.id_autorizacao_ob = A.id_autorizacao_ob
	  LEFT JOIN pagamento.tb_confirmacao_pagamento AS CP (nolock) ON CP.id_autorizacao_ob = A.id_autorizacao_ob
			WHERE 
	  		--	( nullif( @ds_numob, 0 ) is null or B.ds_numob = @ds_numob )
			( nullif( @id_autorizacao_ob, 0 ) is null or A.id_autorizacao_ob = @id_autorizacao_ob )
			ORDER BY 
				A.id_autorizacao_ob

END

/* 
SELECT 'A.' + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT 'B.' + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/