-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 44/08/2017
-- Description: Procedure para exclusão de itens para programacao desembolso agrupamento
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_EXCLUIR]   
		@id_programacao_desembolso_agrupamento int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM contaunica.tb_programacao_desembolso_agrupamento
	WHERE 
		id_programacao_desembolso_agrupamento = @id_programacao_desembolso_agrupamento
  
END