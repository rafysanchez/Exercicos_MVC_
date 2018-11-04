-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para exclusão de cancelamentos de subempenho  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_EXCLUIR]   
	@id_subempenho_cancelamento int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_cancelamento_nota
	WHERE tb_subempenho_cancelamento_id_subempenho_cancelamento =  @id_subempenho_cancelamento;

	DELETE FROM pagamento.tb_subempenho_cancelamento_evento
	WHERE tb_subempenho_cancelamento_id_subempenho_cancelamento =  @id_subempenho_cancelamento;

	DELETE FROM pagamento.tb_subempenho_cancelamento_item
	WHERE tb_subempenho_cancelamento_id_subempenho_cancelamento =  @id_subempenho_cancelamento;

	DELETE FROM pagamento.tb_subempenho_cancelamento
	WHERE id_subempenho_cancelamento = @id_subempenho_cancelamento;

	SELECT @@ROWCOUNT;

END