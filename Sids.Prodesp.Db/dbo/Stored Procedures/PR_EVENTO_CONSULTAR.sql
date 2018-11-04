
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar parametrização de NL
-- =================================================================== 

CREATE PROCEDURE [dbo].[PR_EVENTO_CONSULTAR]
	@id_evento int = null
	,@id_nl_parametrizacao int = null
	,@id_rap_tipo int = null
	,@id_documento_tipo int = null
	,@nr_evento varchar(50) = null
	,@nr_classificacao varchar(50) = null
	,@ds_entrada_saida char(10) = null
	,@nr_fonte varchar(50) = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  
	id_evento
	,id_nl_parametrizacao 
	,id_rap_tipo
	,id_documento_tipo
	,nr_evento
	,nr_classificacao
	,ds_entrada_saida
	,nr_fonte
	FROM [pagamento].[tb_evento] (NOLOCK)
	WHERE
		( NULLIF( @id_evento, 0 ) IS NULL OR id_evento = @id_evento )
        AND ( NULLIF( @id_nl_parametrizacao, 0 ) IS NULL OR id_nl_parametrizacao = @id_nl_parametrizacao )
		AND ( NULLIF( @id_rap_tipo, 0 ) IS NULL OR id_rap_tipo = @id_rap_tipo )
		AND ( NULLIF( @id_documento_tipo, 0 ) IS NULL OR id_documento_tipo = @id_documento_tipo )
		AND ( NULLIF( @nr_evento,'' ) IS NULL OR nr_evento = @nr_evento )
		AND ( NULLIF( @nr_classificacao, '' ) IS NULL OR nr_classificacao = @nr_classificacao )
		AND ( NULLIF( @ds_entrada_saida, '' ) IS NULL OR ds_entrada_saida LIKE '%' + @ds_entrada_saida + '%' )
		AND ( NULLIF( @nr_fonte, '' ) IS NULL OR nr_fonte = @nr_fonte )
	ORDER BY id_evento
END;