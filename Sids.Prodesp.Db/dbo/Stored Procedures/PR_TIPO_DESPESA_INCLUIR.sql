
-- ==============================================================    
-- Author:  Jose Antonio
-- Create date: 15/01/2018
-- Description: Procedure para Incluir tipo de despesa
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_TIPO_DESPESA_INCLUIR]    
   @cd_despesa_tipo int,
   @ds_despesa_tipo varchar(50)
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [pagamento].[tb_despesa_tipo] (
		cd_despesa_tipo,
		ds_despesa_tipo  
	)  
	VALUES (
		@cd_despesa_tipo,
		@ds_despesa_tipo
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();