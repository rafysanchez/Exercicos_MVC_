-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para alteração de valores de empenho
-- ===================================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_MES_ALTERAR]      
	@id_empenho_CANCELAMENTO_mes			int = null
,	@tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO	int = null
,	@vr_mes					numeric = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_cancelamento_mes
	SET 
		vr_mes					= @vr_mes
	 WHERE
		id_empenho_cancelamento_mes			= @id_empenho_CANCELAMENTO_mes and
		tb_empenho_cancelamento_id_empenho_cancelamento	= @tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO
END