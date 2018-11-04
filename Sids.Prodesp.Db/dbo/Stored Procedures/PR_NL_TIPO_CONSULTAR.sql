
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 17/01/2018
-- Description: Procedure para consultar tipos de NL
-- =================================================================== 

CREATE PROCEDURE [dbo].[PR_NL_TIPO_CONSULTAR]
	@id_nl_tipo int = null
	,@ds_nl_tipo varchar(50) = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  
	id_nl_tipo
	,ds_nl_tipo
	FROM [pagamento].[tb_nl_tipo] (NOLOCK)
	WHERE
		( NULLIF( @id_nl_tipo, 0 ) IS NULL OR id_nl_tipo = @id_nl_tipo )
        AND ( NULLIF( @ds_nl_tipo, '' ) IS NULL OR ds_nl_tipo = @ds_nl_tipo )
	ORDER BY id_nl_tipo
END;