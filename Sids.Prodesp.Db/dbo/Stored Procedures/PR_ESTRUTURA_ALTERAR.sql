
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 18/10/2016  
-- Description: Procedure para alteração de Estrutura na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_ESTRUTURA_ALTERAR]   
	@id_estrutura           int = null
	,@id_programa			int = null
	,@ds_nomenclatura		varchar(45) = null
	,@cd_natureza			varchar(6) = null
	,@cd_macro				varchar(6) = null
	,@cd_codigo_aplicacao	varchar(9) = null
	,@cd_fonte				varchar(2) = null
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
	BEGIN TRANSACTION
		UPDATE configuracao.tb_estrutura
		  SET  [id_programa]		= @id_programa
			,[ds_nomenclatura]		= @ds_nomenclatura
			,[cd_natureza]			= @cd_natureza
			,[cd_macro]				= @cd_macro
			,[cd_codigo_aplicacao]	= @cd_codigo_aplicacao
			,[id_fonte]				= @cd_fonte
		 WHERE id_estrutura				= @id_estrutura
	COMMIT;
END