-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de acao 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_ACAO_EXCLUIR]   
	@id_acao     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM seguranca.tb_acao
	 WHERE id_acao =  @id_acao;
  
END