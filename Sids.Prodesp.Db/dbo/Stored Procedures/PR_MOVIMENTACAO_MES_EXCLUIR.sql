-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para exclusão de valores de empenho
create PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_EXCLUIR]   
	@id_mes  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes]
	WHERE 
		id_mes = @id_mes

END