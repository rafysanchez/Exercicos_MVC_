-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 11/07/2017
-- Description: Procedure para exclusão de itens para reclassificacao_retencao
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_EVENTO_EXCLUIR]   
		@id_reclassificacao_retencao_evento int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM contaunica.tb_reclassificacao_retencao_evento
	WHERE 
		id_reclassificacao_retencao_evento = @id_reclassificacao_retencao_evento
  
END