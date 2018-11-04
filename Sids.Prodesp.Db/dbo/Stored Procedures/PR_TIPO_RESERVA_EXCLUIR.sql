-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de tipo_reserva 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_TIPO_RESERVA_EXCLUIR]   
	@id_tipo_reserva     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM reserva.tb_tipo_reserva
	 WHERE id_tipo_reserva =  @id_tipo_reserva;
  
END