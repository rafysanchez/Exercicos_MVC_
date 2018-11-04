CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS]   
	@id_autorizacao_ob int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_autorizacao_ob_itens]
	WHERE	nr_agrupamento_ob = 0
	AND		cd_transmissao_item_status_siafem IS NULL
	AND		NOT EXISTS (SELECT	id_autorizacao_ob 
						FROM	pagamento.tb_confirmacao_pagamento_item WHERE [contaunica].[tb_autorizacao_ob_itens].id_autorizacao_ob = pagamento.tb_confirmacao_pagamento_item.id_autorizacao_ob
																		AND	 LEFT([contaunica].[tb_autorizacao_ob_itens].nr_documento_gerador, 17) = LEFT(pagamento.tb_confirmacao_pagamento_item.nr_documento_gerador, 17))

	SELECT @@ROWCOUNT;

END