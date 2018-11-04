
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar Despesas
-- =================================================================== 

CREATE PROCEDURE [dbo].[PR_DESPESA_CONSULTAR]
	@id_despesa int = null
	,@id_despesa_tipo int = null
	,@id_nl_parametrizacao int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 		  
		id_despesa
		,id_despesa_tipo
		,id_nl_parametrizacao
	FROM [pagamento].[tb_despesa] (NOLOCK)
	WHERE
		( NULLIF( @id_despesa, 0 ) IS NULL OR id_despesa = @id_despesa )
        AND ( NULLIF( @id_despesa_tipo, 0 ) IS NULL OR id_despesa_tipo = @id_despesa_tipo )
        AND ( NULLIF( @id_nl_parametrizacao, 0 ) IS NULL OR id_nl_parametrizacao = @id_nl_parametrizacao )
	ORDER BY id_despesa
END;