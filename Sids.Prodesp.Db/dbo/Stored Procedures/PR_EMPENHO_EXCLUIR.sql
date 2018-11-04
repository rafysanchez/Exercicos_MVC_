-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para exclusão de Empenhos  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_EXCLUIR]   
	@id_empenho INT
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_mes
	WHERE tb_empenho_id_empenho =  @id_empenho;

	DELETE FROM empenho.tb_empenho_item
	WHERE tb_empenho_id_empenho =  @id_empenho;

	DELETE FROM empenho.tb_empenho
	WHERE id_empenho = @id_empenho;
  
END