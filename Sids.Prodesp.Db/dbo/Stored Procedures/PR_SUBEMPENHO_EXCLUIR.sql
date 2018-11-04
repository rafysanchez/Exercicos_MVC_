-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para exclusão de submpenhos  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_EXCLUIR]   
	@id_subempenho int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_subempenho_nota
	WHERE tb_subempenho_id_subempenho =  @id_subempenho;

	DELETE FROM pagamento.tb_subempenho_evento
	WHERE tb_subempenho_id_subempenho =  @id_subempenho;

	DELETE FROM pagamento.tb_subempenho_item
	WHERE tb_subempenho_id_subempenho =  @id_subempenho;

	DELETE FROM pagamento.tb_subempenho
	WHERE id_subempenho = @id_subempenho;

	SELECT @@ROWCOUNT;

END