-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir tipo_reserva
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_TIPO_RESERVA_INCLUIR]    
   @ds_tipo_reserva  VARCHAR(100)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [reserva].[tb_tipo_reserva] (
		ds_tipo_reserva  
	)  
	VALUES (
		@ds_tipo_reserva  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();