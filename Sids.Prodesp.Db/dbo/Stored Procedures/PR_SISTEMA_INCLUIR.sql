-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir sistema
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_SISTEMA_INCLUIR]    
   @ds_sistema  VARCHAR(100)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [seguranca].[tb_sistema] (
		ds_sistema  
	)  
	VALUES (
		@ds_sistema  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();