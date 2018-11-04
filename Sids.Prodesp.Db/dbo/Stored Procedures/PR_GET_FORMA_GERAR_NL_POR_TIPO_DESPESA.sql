CREATE PROCEDURE [dbo].[PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA]
	@id_despesa_tipo int
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
	 P.id_parametrizacao_forma_gerar_nl
	,P.id_nl_parametrizacao
	,P.id_nl_tipo
	,G.ds_gerar_nl
	,D.id_despesa
    ,D.id_despesa_tipo
	,T.cd_despesa_tipo
	,N.ds_nl_tipo 
		FROM [pagamento].[tb_nl_parametrizacao]						AS P
		INNER JOIN [pagamento].[tb_despesa]							AS D ON      P.id_nl_parametrizacao				= D.id_nl_parametrizacao
		INNER JOIN [pagamento].[tb_despesa_tipo]					AS T ON      D.id_despesa_tipo					= T.id_despesa_tipo
		INNER JOIN [pagamento].[tb_parametrizacao_forma_gerar_nl]   AS G ON      P.id_parametrizacao_forma_gerar_nl = G.id_parametrizacao_forma_gerar_nl
		INNER JOIN [pagamento].[tb_nl_tipo]                         AS N ON		 P.id_nl_tipo					    = N.id_nl_tipo
			WHERE
				( NULLIF( @id_despesa_tipo, 0 ) IS NULL OR D.id_despesa_tipo = @id_despesa_tipo )
			ORDER BY D.id_despesa_tipo
END;