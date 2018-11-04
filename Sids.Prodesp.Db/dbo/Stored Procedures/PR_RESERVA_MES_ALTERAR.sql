
-- ===================================================================    
-- Author:		FLuis Fernando
-- Create date: 01/11/2016
-- Description: Procedure para alteração de Valores de reservas
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_RESERVA_MES_ALTERAR]      
	@id_reserva_mes			int,
	@id_reserva				int,
	@ds_mes					VARCHAR(2),
	@vr_mes					numeric
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
	SET NOCOUNT ON;    
   
	UPDATE reserva.tb_reserva_mes
		  SET id_reserva		= @id_reserva  
			,ds_mes				= @ds_mes
			,vr_mes				= @vr_mes
	 WHERE id_reserva_mes		= @id_reserva_mes
END