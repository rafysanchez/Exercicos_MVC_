-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR]   
	@id_evento  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria_evento]
	WHERE 
		id_evento = @id_evento
  
END