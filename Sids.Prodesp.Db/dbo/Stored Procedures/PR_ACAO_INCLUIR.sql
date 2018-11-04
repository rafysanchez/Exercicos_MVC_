-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para Incluir acao
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_ACAO_INCLUIR]    
   @ds_acao  VARCHAR(50)= NULL
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
	INSERT INTO [seguranca].[tb_acao] (
		ds_acao  
	)  
	VALUES (
		@ds_acao  
	)  
  
COMMIT

SELECT SCOPE_IDENTITY();