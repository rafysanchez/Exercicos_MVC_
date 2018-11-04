-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 31/03/2017
-- Description: Procedure para exclusão de anulações de rap  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_ANULACAO_EXCLUIR]   
	@id_rap_anulacao int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_anulacao_nota
	WHERE tb_rap_anulacao_id_rap_anulacao =  @id_rap_anulacao;

	DELETE FROM pagamento.tb_rap_anulacao
	WHERE id_rap_anulacao = @id_rap_anulacao;

	SELECT @@ROWCOUNT;

END