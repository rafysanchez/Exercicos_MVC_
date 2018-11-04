-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017  
-- Description: Procedure para exclusão de notas para requisição de restos a pagar
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_REQUISICAO_NOTA_EXCLUIR]   
	@id_rap_requisicao_nota int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_requisicao_nota
	WHERE 
		id_rap_requisicao_nota  = @id_rap_requisicao_nota
  
END