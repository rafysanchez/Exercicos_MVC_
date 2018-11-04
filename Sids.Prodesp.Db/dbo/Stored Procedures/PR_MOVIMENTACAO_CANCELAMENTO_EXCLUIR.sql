-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR]   
	@id_cancelamento_movimentacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [movimentacao].[tb_cancelamento_movimentacao]
	WHERE 
		id_cancelamento_movimentacao = @id_cancelamento_movimentacao
  
END