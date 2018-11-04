
-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 01/11/2016  
-- Description: Procedure para exclusão de Valores de Reservas
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_MES_EXCLUIR]   
	@id_reserva_mes     INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  

	DELETE FROM reserva.tb_reserva_mes
	 WHERE id_reserva_mes =  @id_reserva_mes;
  
END