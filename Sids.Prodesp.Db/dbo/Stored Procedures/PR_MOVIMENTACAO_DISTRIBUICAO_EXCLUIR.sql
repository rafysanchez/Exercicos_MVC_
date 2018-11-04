-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR]   
	@id_distribuicao_movimentacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_distribuicao_movimentacao]
	WHERE 
		id_distribuicao_movimentacao = @id_distribuicao_movimentacao
  
END