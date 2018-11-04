
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO]
AS

	SELECT	ISNULL(MAX(nr_agrupamento_ob), 0) + 1
	FROM	contaunica.tb_autorizacao_ob_itens (NOLOCK)