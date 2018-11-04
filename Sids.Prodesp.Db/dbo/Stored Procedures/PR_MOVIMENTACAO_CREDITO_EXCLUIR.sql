-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR]   
	@id_nota_credito  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_credito_movimentacao]
	WHERE 
		id_nota_credito = @id_nota_credito
  
END