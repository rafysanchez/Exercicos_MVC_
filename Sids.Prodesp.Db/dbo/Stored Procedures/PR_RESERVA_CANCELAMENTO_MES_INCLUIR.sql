

-- ===================================================================      
-- Author:	Carlos Henrique	   
-- Create date: 01/11/2016      
-- Description: Procedure para inclusão de Valores para cancelamentos
CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_MES_INCLUIR]    
	@id_cancelamento				int,
	@ds_mes					VARCHAR(2),
	@vr_mes					numeric
AS    
BEGIN      
	-- SET NOCOUNT ON added to prevent extra result sets from      
	-- interfering with SELECT statements.      
	SET NOCOUNT ON;
	
BEGIN TRANSACTION

	INSERT INTO [reserva].[tb_cancelamento_mes]
		([id_cancelamento]
		,[ds_mes]
		,[vr_mes])
	VALUES    
		(@id_cancelamento
		,@ds_mes
		,@vr_mes)
		
           
COMMIT
    
    SELECT @@IDENTITY
END