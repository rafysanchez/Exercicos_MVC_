-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_MES_EXCLUIR]   
	@id_empenho_CANCELAMENTO_mes			int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_cancelamento_mes
	WHERE 
		id_empenho_cancelamento_mes  = @id_empenho_CANCELAMENTO_mes 
  
END