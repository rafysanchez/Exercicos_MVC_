-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir area
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_AREA_INCLUIR]    
   @ds_area  VARCHAR(100)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [seguranca].[tb_area] (
		ds_area  
	)  
	VALUES (
		@ds_area  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();