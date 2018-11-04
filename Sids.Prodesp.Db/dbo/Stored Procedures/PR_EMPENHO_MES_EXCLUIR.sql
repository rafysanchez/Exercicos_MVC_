-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_MES_EXCLUIR]   
	@id_empenho_mes			int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_mes
	WHERE 
		id_empenho_mes  = @id_empenho_mes 
  
END