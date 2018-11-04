
-- ==============================================================    
-- Author:  Luis Fernando  
-- Create date: 18/10/2016    
-- Description: Procedure para Incluir  Estruturas
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_ESTRUTURA_INCLUIR]    
	@id_programa			int = null
	,@ds_nomenclatura		varchar(45) = null
	,@cd_natureza			varchar(6) = null
	,@cd_macro				varchar(6) = null
	,@cd_codigo_aplicacao	varchar(9) = null
	,@cd_fonte				varchar(2) = null
 
AS  
BEGIN
	BEGIN TRANSACTION
		 SET NOCOUNT ON;  
	 
		INSERT INTO [configuracao].[tb_estrutura]
			([id_programa]
			,[ds_nomenclatura]
			,[cd_natureza]
			,[cd_macro]
			,[cd_codigo_aplicacao]
			,[id_fonte])
		VALUES
			(@id_programa
			,@ds_nomenclatura
			,@cd_natureza
			,@cd_macro
			,@cd_codigo_aplicacao
			,@cd_fonte) 
  
	   COMMIT  
	 SELECT SCOPE_IDENTITY();  
END;