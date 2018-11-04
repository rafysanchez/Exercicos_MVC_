
-- ===================================================================      
-- Author:		Luis Fernando	   
-- Create date: 01/11/2016      
-- Description: Procedure para inclusão de Valores para reservas
CREATE PROCEDURE [dbo].[PR_RESERVA_MES_INCLUIR]    
	@id_reserva				int,
	@ds_mes					VARCHAR(2),
	@vr_mes					numeric
AS    
BEGIN      
	-- SET NOCOUNT ON added to prevent extra result sets from      
	-- interfering with SELECT statements.      
	SET NOCOUNT ON;
	
BEGIN TRANSACTION

	INSERT INTO [reserva].[tb_reserva_mes]
		([id_reserva]
		,[ds_mes]
		,[vr_mes])
	VALUES    
		(@id_reserva
		,@ds_mes
		,@vr_mes)
		
           
COMMIT
    
    SELECT @@IDENTITY
END