-- ===================================================================    
-- Author:		Carlos Henrique Magalhaes
-- Create date: 20/12/2016
-- Description: Procedure para alteração de valores de REFORÇO
-- ===================================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_ALTERAR]      
	@id_empenho_reforco_mes			int = null
,	@tb_empenho_reforco_id_empenho_reforco	int = null
,	@vr_mes					numeric = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_reforco_mes
	SET 
		vr_mes					= @vr_mes
	 WHERE
		id_empenho_reforco_mes			= @id_empenho_reforco_mes and
		tb_empenho_reforco_id_empenho_reforco	= @tb_empenho_reforco_id_empenho_reforco
END