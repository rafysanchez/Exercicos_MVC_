-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description:	Procedure para consulta de execução de pd
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR]
	@id_execucao_pd int = NULL,
	@ds_numob varchar(12) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF ISNULL(@ds_numob, 0) = 0
		/* Listar Execução de PD*/

		SELECT
			EI.[id_execucao_pd],
			EI.nr_agrupamento_pd,
			E.[id_tipo_execucao_pd], 
			E.[ug_pagadora],
			E.[gestao_pagadora], 
			E.[ug_liquidante], 
			E.[gestao_liquidante], 
			E.[unidade_gestora], 
			E.[gestao], 
			E.[ano_pd], 
			E.[valor_total], 
			E.[nr_agrupamento], 
			E.[dt_cadastro], 
			E.[fl_sistema_prodesp], 
			E.[fl_sistema_siafem_siafisico],
			PD.id_tipo_documento,
			PD.nr_contrato,
			PD.nr_documento,
			PD.nr_documento_gerador,
			CP.id_confirmacao_pagamento,
			CP.ds_transmissao_mensagem_prodesp,			
			E.[id_tipo_pagamento],
			E.[fl_confirmacao],
			ISNULL(E.[dt_confirmacao], CP.dt_confirmacao) as dt_confirmacao
		FROM contaunica.tb_programacao_desembolso_execucao AS E (nolock)
		LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON E.id_execucao_pd = EI.id_execucao_pd 
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON EI.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN pagamento.tb_confirmacao_pagamento AS CP (nolock) ON CP.id_execucao_pd = EI.id_execucao_pd
		--LEFT JOIN pagamento.tb_confirmacao_pagamento_item CPI (nolock) ON CPI.id_programacao_desembolso_execucao_item = EI.id_programacao_desembolso_execucao_item
			WHERE 
	  			( nullif( @id_execucao_pd, 0 ) is null or EI.id_execucao_pd = @id_execucao_pd )
				OR (@id_execucao_pd IS NULL OR EI.nr_agrupamento_pd = @id_execucao_pd)
				AND nr_agrupamento_pd <> 0
			ORDER BY 
				EI.[id_execucao_pd]
	ELSE
		
		/* Listar Autorização de OB*/

		SELECT
			A.[id_execucao_pd],
			A.[id_tipo_execucao_pd], 
			A.[ug_pagadora], 
			A.[gestao_pagadora], 
			A.[ug_liquidante], 
			A.[gestao_liquidante], 
			A.[unidade_gestora], 
			A.[gestao], 
			A.[ano_pd], 
			A.[valor_total], 
			A.[nr_agrupamento], 
			A.[dt_cadastro], 
			A.[fl_sistema_prodesp], 
			A.[fl_sistema_siafem_siafisico],
			A.[id_tipo_pagamento], 
			A.[fl_confirmacao],
			A.[dt_confirmacao]
	  FROM [contaunica].[tb_programacao_desembolso_execucao] (nolock) A
	  INNER JOIN [contaunica].[tb_programacao_desembolso_execucao_item] (nolock) B ON B.id_execucao_pd = A.id_execucao_pd
			WHERE 
	  			( nullif( @ds_numob, 0 ) is null or B.ds_numob = @ds_numob )
				AND nr_agrupamento_pd <> 0
			ORDER BY 
				A.[id_execucao_pd]

END









/* 

SELECT  + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/