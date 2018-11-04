

-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 01/11/2016  
-- Description: Procedure para exclusão de Valores de Reservas
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_MES_EXCLUIR]   
	@id_cancelamento_mes     INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  

	DELETE FROM reserva.tb_cancelamento_mes
	 WHERE id_cancelamento_mes =  @id_cancelamento_mes;
  
END