-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 30/03/2017
-- Description: Procedure para exclusão de inscrições de rap  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RAP_INSCRICAO_EXCLUIR]   
	@id_rap_inscricao int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM pagamento.tb_rap_inscricao_nota
	WHERE tb_rap_inscricao_id_rap_inscricao =  @id_rap_inscricao;

	DELETE FROM pagamento.tb_rap_inscricao
	WHERE id_rap_inscricao = @id_rap_inscricao;

	SELECT @@ROWCOUNT;

END