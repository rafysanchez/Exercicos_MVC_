-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017  
-- Description: Procedure para exclusão de notas para anulação de restos a pagar
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_ANULACAO_NOTA_EXCLUIR]   
	@id_rap_anulacao_nota int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_anulacao_nota
	WHERE 
		id_rap_anulacao_nota  = @id_rap_anulacao_nota
  
END