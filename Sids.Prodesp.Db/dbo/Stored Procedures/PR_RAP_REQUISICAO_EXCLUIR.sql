-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 31/03/2017
-- Description: Procedure para exclusão de requisicões de rap  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_REQUISICAO_EXCLUIR]   
	@id_rap_requisicao int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_requisicao_nota
	WHERE tb_rap_requisicao_id_rap_requisicao =  @id_rap_requisicao;

	DELETE FROM pagamento.tb_rap_requisicao
	WHERE id_rap_requisicao = @id_rap_requisicao;

	SELECT @@ROWCOUNT;

END