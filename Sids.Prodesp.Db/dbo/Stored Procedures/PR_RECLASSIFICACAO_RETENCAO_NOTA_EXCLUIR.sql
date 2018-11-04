-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 11/07/2017
-- Description: Procedure para exclusão de notas para reclassificacao_retencao
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_NOTA_EXCLUIR]   
	@id_reclassificacao_retencao_nota int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM contaunica.tb_reclassificacao_retencao_nota
	WHERE 
		id_reclassificacao_retencao_nota  = @id_reclassificacao_retencao_nota
  
END