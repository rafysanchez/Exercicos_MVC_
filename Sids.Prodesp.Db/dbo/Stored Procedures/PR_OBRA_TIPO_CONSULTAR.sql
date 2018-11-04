-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 09/02/2017
-- Description: Procedure para consulta de tipos de obra
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_OBRA_TIPO_CONSULTAR]  
	@id_obra_tipo	INT = 0
AS  
BEGIN  
	SET NOCOUNT ON;

	SELECT 
		id_obra_tipo
	,	ds_obra_tipo 
	FROM pagamento.tb_obra_tipo ( NOLOCK )
	WHERE
       (
	    nullif( @id_obra_tipo, 0 ) is null or id_obra_tipo = @id_obra_tipo 
	   )
	   ORDER BY ds_obra_tipo
END