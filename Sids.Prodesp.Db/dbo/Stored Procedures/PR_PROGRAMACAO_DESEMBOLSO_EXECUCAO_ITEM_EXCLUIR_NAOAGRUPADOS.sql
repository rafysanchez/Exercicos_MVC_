CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS]   
	@id_execucao_pd int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	--IF ISNULL(@id_execucao_pd, 0) = 0
		DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
		WHERE	nr_agrupamento_pd = 0
		AND		cd_transmissao_status_siafem IS NULL
		AND		NOT EXISTS (SELECT	id_execucao_pd 
							FROM	pagamento.tb_confirmacao_pagamento_item WHERE [contaunica].[tb_programacao_desembolso_execucao_item].id_execucao_pd = pagamento.tb_confirmacao_pagamento_item.id_execucao_pd
																			AND	 LEFT([contaunica].[tb_programacao_desembolso_execucao_item].nr_documento_gerador, 17) = LEFT(pagamento.tb_confirmacao_pagamento_item.nr_documento_gerador, 17))


	--ELSE
	--	DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
	--	WHERE	id_execucao_pd = @id_execucao_pd
	--	AND		nr_agrupamento_pd = 0

	SELECT @@ROWCOUNT;

END