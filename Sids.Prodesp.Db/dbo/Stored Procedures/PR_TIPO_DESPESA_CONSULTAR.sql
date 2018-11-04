
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar tipo de despesa
-- =================================================================== 

CREATE PROCEDURE [dbo].[PR_TIPO_DESPESA_CONSULTAR]
	@id_despesa_tipo int = null
	,@cd_despesa_tipo int = null
	,@ds_despesa_tipo varchar(50) = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  
	id_despesa_tipo
	,cd_despesa_tipo 
	,ds_despesa_tipo
	FROM [pagamento].[tb_despesa_tipo] (NOLOCK)
	WHERE
		( NULLIF( @id_despesa_tipo, 0 ) IS NULL OR id_despesa_tipo = @id_despesa_tipo )
		AND ( NULLIF( @cd_despesa_tipo, 0 ) IS NULL OR cd_despesa_tipo = @cd_despesa_tipo )
        AND ( NULLIF( @ds_despesa_tipo, '' ) IS NULL OR ds_despesa_tipo LIKE '%' +  @ds_despesa_tipo + '%' )
	ORDER BY id_despesa_tipo
END;