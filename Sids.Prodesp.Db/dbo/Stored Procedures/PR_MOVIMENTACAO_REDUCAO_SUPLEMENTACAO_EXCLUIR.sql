-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR]   
	@id_reducao_suplementacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_reducao_suplementacao]
	WHERE 
		id_reducao_suplementacao = @id_reducao_suplementacao
  
END