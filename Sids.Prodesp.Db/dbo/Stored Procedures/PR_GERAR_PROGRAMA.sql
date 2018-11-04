
-- ===================================================================      
-- Author:		Luis Fernando	   
-- Create date: 14/10/2016      
-- Description: Procedure para inclusão de Programas na base de dados do ano selecionado      
CREATE PROCEDURE [dbo].[PR_GERAR_PROGRAMA]
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
	select cd_ptres
		,cd_cfp
		,ds_programa
		,@nr_ano_referencia
	from [configuracao].[tb_programa](nolock)
	Where nr_ano_referencia = @nr_ano_referencia - 1;
		
	INSERT INTO [configuracao].[tb_estrutura]
		(id_programa
		,ds_nomenclatura
		,cd_natureza
		,cd_macro
		,cd_codigo_aplicacao
		,id_fonte)
    select distinct C.id_programa
	,A.ds_nomenclatura
	,A.cd_natureza
	,A.cd_macro
	,A.cd_codigo_aplicacao
	,A.id_fonte
	 from [configuracao].[tb_estrutura] A(nolock)
	 inner join [configuracao].[tb_programa] B(nolock)
	 on A.id_programa = B.id_programa
	 and nr_ano_referencia = @nr_ano_referencia - 1
	 inner join [configuracao].[tb_programa] C(nolock)
	 on B.cd_ptres = C.cd_ptres
	 and B.cd_cfp = C.cd_cfp
	 and B.ds_programa = C.ds_programa
	 and C.nr_ano_referencia = @nr_ano_referencia;
COMMIT
    
    SELECT @@IDENTITY
END