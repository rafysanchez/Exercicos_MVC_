
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO]
AS

	SELECT	ISNULL(MAX(nr_agrupamento_pd), 0) + 1
	FROM	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK)