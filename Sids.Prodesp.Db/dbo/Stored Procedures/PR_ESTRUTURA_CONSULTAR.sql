
-- ==============================================================    
-- Author:  Luis Fernando   
-- Create date: 18/10/2016    
-- Description: Procedure para consulta de Estruturas 
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_ESTRUTURA_CONSULTAR]    
	@id_estrutura           int = null
	,@id_programa			int = null
	,@ds_nomenclatura		varchar(45) = null
	,@cd_natureza			varchar(6) = null
	,@cd_macro				varchar(6) = null
	,@cd_codigo_aplicacao	varchar(9) = null
	,@cd_fonte				varchar(2) = null
AS  
BEGIN  
	SET NOCOUNT ON;

	SELECT    
     	 E.[id_estrutura]
		,E.[id_programa]
		,E.[ds_nomenclatura]
		,E.[cd_natureza]
		,E.[cd_macro]
		,E.[cd_codigo_aplicacao]
		,E.[id_fonte]    
	FROM configuracao.tb_estrutura(nolock)   E  
	  WHERE (id_estrutura = @id_estrutura OR ISNULL(@id_estrutura,0) = 0)
			AND (ds_nomenclatura like '%'+ @ds_nomenclatura+'%' OR ISNULL(@ds_nomenclatura,'') = '')
			AND (id_programa = @id_programa OR ISNULL(@id_programa,0) = 0)
			AND (cd_natureza = @cd_natureza OR ISNULL(@cd_natureza,'') = '')
			AND (cd_macro = @cd_macro OR ISNULL(@cd_macro,'') = '')
			AND (cd_codigo_aplicacao = @cd_codigo_aplicacao OR ISNULL(@cd_codigo_aplicacao,'') = '')
			AND (id_fonte = @cd_fonte OR ISNULL(@cd_fonte,0) = 0)
END ;