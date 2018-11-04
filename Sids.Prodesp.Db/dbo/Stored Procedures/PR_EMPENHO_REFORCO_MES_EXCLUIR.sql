-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 16/01/2016  
-- Description: Procedure para exclusão de valores de reforço
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_EXCLUIR]   
	@id_empenho_reforco_mes			int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_reforco_mes
	WHERE 
		id_empenho_reforco_mes  = @id_empenho_reforco_mes 
  
END