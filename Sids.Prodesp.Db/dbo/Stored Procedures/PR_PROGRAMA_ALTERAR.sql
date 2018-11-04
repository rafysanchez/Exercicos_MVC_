
-- ===================================================================    
-- Author:		FLuis Fernando
-- Create date: 14/10/2016
-- Description: Procedure para alteração de Programas na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_PROGRAMA_ALTERAR]      
	@id_programa				int,    
	@cd_ptres					VARCHAR(6),
	@cd_cfp						VARCHAR(16),
	@ds_programa				VARCHAR(60),
	@nr_ano_referencia			int
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
	SET NOCOUNT ON;    
   
	UPDATE configuracao.tb_programa
		  SET cd_ptres			= @cd_ptres  
		  ,cd_cfp				= @cd_cfp
		  ,ds_programa			= @ds_programa
		  ,nr_ano_referencia	= @nr_ano_referencia  
	 WHERE id_programa			= @id_programa
END