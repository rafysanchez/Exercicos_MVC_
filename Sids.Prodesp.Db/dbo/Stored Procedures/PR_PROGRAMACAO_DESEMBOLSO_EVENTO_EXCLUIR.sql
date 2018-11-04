-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para exclusão de itens para programacao desembolso
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EVENTO_EXCLUIR]   
		@id_programacao_desembolso_evento int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM contaunica.tb_programacao_desembolso_evento
	WHERE 
		id_programacao_desembolso_evento = @id_programacao_desembolso_evento
  
END