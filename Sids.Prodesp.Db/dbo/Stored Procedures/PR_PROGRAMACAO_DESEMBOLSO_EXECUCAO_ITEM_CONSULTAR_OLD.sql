-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description:	Procedure para consulta de execução de pd
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD] --NULL, 556
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
		ITEM.id_programacao_desembolso_execucao_item, 
		ITEM.id_execucao_pd, 
		ITEM.ds_numpd,
		ITEM.ds_numob, 
		ITEM.ob_cancelada,
		ITEM.ug,
		ITEM.gestao,
		ITEM.ug_pagadora, 
		ITEM.ug_liquidante, 
		ITEM.gestao_pagadora, 
		ITEM.gestao_liguidante, 
		ITEM.favorecido, 
		ITEM.favorecidoDesc, 
		ITEM.ordem, 
		ITEM.ano_pd, 
		ITEM.valor, 
		ITEM.ds_noup, 
		ITEM.nr_agrupamento_pd, 
		ITEM.fl_sistema_prodesp,
		[pi].dt_confirmacao,
		[pi].id_confirmacao_pagamento,
		[pi].id_confirmacao_pagamento_item,
		[pi].cd_transmissao_status_prodesp, 
		[pi].fl_transmissao_transmitido_prodesp, 
		[pi].dt_transmissao_transmitido_prodesp, 
		[pi].ds_transmissao_mensagem_prodesp, 
		ITEM.cd_transmissao_status_siafem, 
		ITEM.fl_transmissao_transmitido_siafem, 
		ITEM.dt_transmissao_transmitido_siafem, 
		ITEM.ds_transmissao_mensagem_siafem,
		ITEM.nr_documento_gerador,
		ITEM.ds_consulta_op_prodesp,
		ITEM.nr_op,
		ISNULL(PD.dt_emissao, PDA.dt_emissao) AS dt_emissao,
		ISNULL(PD.dt_vencimento, PDA.dt_vencimento) as dt_vencimento,
		ISNULL(PD.nr_documento, PDA.nr_documento) as nr_documento,
		PD.nr_contrato,
		ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador,
		ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) as id_tipo_documento,
		ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) as nr_cnpj_cpf_credor,
		ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) as nr_cnpj_cpf_pgto
  FROM [contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
  		--LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE 
	  		( nullif( @id_programacao_desembolso_execucao_item, 0 ) is null or ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item ) AND
	  		( nullif( @id_execucao_pd, 0 ) is null or ITEM.id_execucao_pd = @id_execucao_pd ) AND
			( nullif( @ds_numpd, 0 ) is null or ITEM.ds_numpd = @ds_numpd )
			AND ITEM.nr_agrupamento_pd <> 0
		ORDER BY 
			ITEM.id_programacao_desembolso_execucao_item
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
*/