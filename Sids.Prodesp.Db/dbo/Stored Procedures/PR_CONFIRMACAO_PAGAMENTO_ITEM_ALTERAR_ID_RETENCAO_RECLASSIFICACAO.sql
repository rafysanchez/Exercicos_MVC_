-- ===================================================================    
-- Author:		Rodrigo de Camargo Borghi
-- Create date: 05/10/2018
-- Description: Procedure para alteração da reclassificação retenção
CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR_ID_RETENCAO_RECLASSIFICACAO]   
	@id_confirmacao_pagamento_item  int = null
,	@id_reclassificacao_retencao int = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE pagamento.tb_confirmacao_pagamento_item
	SET 
		id_reclassificacao_retencao = @id_reclassificacao_retencao
	 WHERE 
		id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
END