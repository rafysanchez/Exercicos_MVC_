-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 30/10/2017
-- Description:	LISTAR TODOS OS TIPOS DE EXECUCAO
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_TIPO_EXECUCAO]
	@id_execucao_pd int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM contaunica.tb_programacao_desembolso_execucao_tipo (nolock)
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