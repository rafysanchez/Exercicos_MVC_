
-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 09/11/2016  
-- Description: Procedure para exclusão de Reservas da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_EXCLUIR]   
	@id_reserva    INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
	DELETE FROM reserva.tb_reserva_mes
	 WHERE id_reserva =  @id_reserva;

	DELETE FROM reserva.tb_reserva
	 WHERE id_reserva =  @id_reserva;
  
END