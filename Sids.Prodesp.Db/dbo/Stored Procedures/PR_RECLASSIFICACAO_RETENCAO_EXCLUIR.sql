-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para exclusão de reclassificacao retencao 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_EXCLUIR]   
	@id_reclassificacao_retencao int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  
	DELETE FROM [contaunica].[tb_reclassificacao_retencao_nota]
	WHERE id_reclassificacao_retencao = @id_reclassificacao_retencao;

	DELETE FROM [contaunica].[tb_reclassificacao_retencao_evento]
	WHERE id_reclassificacao_retencao = @id_reclassificacao_retencao;

	DELETE FROM [contaunica].[tb_reclassificacao_retencao]
	WHERE id_reclassificacao_retencao = @id_reclassificacao_retencao;

	SELECT @@ROWCOUNT;

END