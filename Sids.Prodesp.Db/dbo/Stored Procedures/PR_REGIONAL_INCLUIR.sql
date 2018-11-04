-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir regionals
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_REGIONAL_INCLUIR]    
   @id_regional  int=null,
   @cd_uge		 VARCHAR(6)=null,
   @ds_regional  VARCHAR(100)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [seguranca].[tb_regional] (
		id_regional,
		cd_uge,
		ds_regional  
	)  
	VALUES (
		@id_regional,
		@cd_uge,  
		@ds_regional  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();