
-- ==============================================================    
-- Author:  Luis Fernando   
-- Create date: 19/10/2016    
-- Description: Procedure para consulta de Estruturas por P_rograma
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_GET_ESTRUTURA_POR_PROGRAMA]    
	@id_programa				int = null,    
	@cd_ptres					int = null,
	@cd_cfp						VARCHAR(16) = null,
	@ds_programa				VARCHAR(60) = null,
	@nr_ano_referencia			int = null
AS
BEGIN
	SET NOCOUNT ON;

	
	IF OBJECT_ID('tempdb..#programa') is not null DROP TABLE #programa
	CREATE TABLE  #programa(id_programa int);

	insert into #programa
	SELECT [id_programa]
		FROM [configuracao].[tb_programa](nolock)	  A
	  WHERE ([id_programa] = @id_programa OR ISNULL(@id_programa,0) = 0)
			AND ([ds_programa] like '%'+ @ds_programa+'%' OR ISNULL(@ds_programa,'') = '')
			AND ([cd_ptres] = @cd_ptres OR ISNULL(@cd_ptres,'') = '')
			AND ([cd_cfp] = @cd_cfp	OR ISNULL(@cd_cfp,'') = '')
			AND ([nr_ano_referencia] = @nr_ano_referencia OR ISNULL(@nr_ano_referencia,0) = 0);


	SELECT    
     	 E.[id_estrutura]
		,E.[id_programa]
		,E.[ds_nomenclatura]
		,E.[cd_natureza]
		,E.[cd_macro]
		,E.[cd_codigo_aplicacao]
		,E.[id_fonte]    
	FROM [configuracao].tb_estrutura(nolock)  E
	join #programa P on E.id_programa = P.id_programa;
END ;