-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para exclusão de Empenhos  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_EXCLUIR]   
	@id_empenho_cancelamento INT
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_cancelamento_mes
	WHERE tb_empenho_cancelamento_id_empenho_cancelamento =  @id_empenho_cancelamento;

	DELETE FROM empenho.tb_empenho_cancelamento_item
	WHERE tb_empenho_cancelamento_id_empenho_cancelamento =  @id_empenho_cancelamento;

	DELETE FROM empenho.tb_empenho_cancelamento
	WHERE id_empenho_cancelamento = @id_empenho_cancelamento;
  
END