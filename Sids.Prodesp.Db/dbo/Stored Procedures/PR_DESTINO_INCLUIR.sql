-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir destinos
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_DESTINO_INCLUIR]    
   @cd_destino  CHAR(2)= NULL,  
   @ds_destino  VARCHAR(140)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [configuracao].[tb_destino] (
		cd_destino,
		ds_destino  
	)  
	VALUES (
		@cd_destino,  
		@ds_destino  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();