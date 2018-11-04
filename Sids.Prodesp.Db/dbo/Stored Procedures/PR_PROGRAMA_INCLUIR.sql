
-- ===================================================================      
-- Author:		Luis Fernando	   
-- Create date: 14/10/2016      
-- Description: Procedure para inclusão de Programas na base de dados      
CREATE PROCEDURE [dbo].[PR_PROGRAMA_INCLUIR]       
	@cd_ptres					VARCHAR(6),
	@cd_cfp						VARCHAR(16),
	@ds_programa				VARCHAR(60),
	@nr_ano_referencia			int
AS    
BEGIN      
	-- SET NOCOUNT ON added to prevent extra result sets from      
	-- interfering with SELECT statements.      
	SET NOCOUNT ON;
	
BEGIN TRANSACTION

	INSERT INTO [configuracao].[tb_programa]
		([cd_ptres]
		,[cd_cfp]
		,[ds_programa]
		,[nr_ano_referencia])
	VALUES    
		(@cd_ptres
		,@cd_cfp
		,@ds_programa
		,@nr_ano_referencia)
		
           
COMMIT
    
    SELECT @@IDENTITY
END