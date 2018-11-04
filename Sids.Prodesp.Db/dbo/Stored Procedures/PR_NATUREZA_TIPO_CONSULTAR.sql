-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 09/02/2017
-- Description: Procedure para consulta de tipos de natureza
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_NATUREZA_TIPO_CONSULTAR]  
	@id_natureza_tipo	INT = 0
AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_natureza_tipo
	,	ds_natureza_tipo 
	FROM pagamento.tb_natureza_tipo ( NOLOCK )
	WHERE 
       ( nullif( @id_natureza_tipo, 0 ) is null or id_natureza_tipo = @id_natureza_tipo )
	   ORDER BY ds_natureza_tipo
END