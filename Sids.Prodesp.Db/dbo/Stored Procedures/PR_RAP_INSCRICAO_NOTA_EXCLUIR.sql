-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 24/05/2017  
-- Description: Procedure para exclusão de notas para inscrição de restos a pagar
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_INSCRICAO_NOTA_EXCLUIR]   
	@id_rap_inscricao_nota int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_inscricao_nota
	WHERE 
		id_rap_inscricao_nota  = @id_rap_inscricao_nota
  
END