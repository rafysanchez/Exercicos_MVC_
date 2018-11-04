-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para alteração de valores de empenho
-- ===================================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_MES_ALTERAR]      
	@id_empenho_mes			int = null
,	@tb_empenho_id_empenho	int = null
,	@vr_mes					numeric = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_mes
	SET 
		vr_mes					= @vr_mes
	 WHERE
		id_empenho_mes			= @id_empenho_mes and
		tb_empenho_id_empenho	= @tb_empenho_id_empenho
END