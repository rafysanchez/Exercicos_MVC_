CREATE PROCEDURE [dbo].[PR_FORMA_GERAR_NL_CONSULTAR]
	@id_parametrizacao_forma_gerar_nl int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
	id_parametrizacao_forma_gerar_nl
	,ds_gerar_nl
	FROM [pagamento].[tb_parametrizacao_forma_gerar_nl] (NOLOCK)
	WHERE
		( NULLIF( @id_parametrizacao_forma_gerar_nl, 0 ) IS NULL OR id_parametrizacao_forma_gerar_nl = @id_parametrizacao_forma_gerar_nl )
	ORDER BY id_parametrizacao_forma_gerar_nl
END;