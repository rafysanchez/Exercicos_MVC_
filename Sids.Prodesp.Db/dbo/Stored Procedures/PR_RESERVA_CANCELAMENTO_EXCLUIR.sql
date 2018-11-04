

-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 09/11/2016  
-- Description: Procedure para exclusão de Cancelamento da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_EXCLUIR]   
	@id_cancelamento    INT
AS  
BEGIN  
	  
	SET NOCOUNT ON;  
	DELETE FROM reserva.tb_cancelamento
	 WHERE id_cancelamento =  @id_cancelamento;
	   
END