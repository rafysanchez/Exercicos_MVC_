-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 09/02/2017
-- Description: Procedure para consulta de tipos de evento
-- ==============================================================  
Create PROCEDURE [dbo].[PR_CODIGO_EVENTO_TIPO_CONSULTAR]  
	@id_evento_tipo	INT = 0

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_evento_tipo
	,	ds_evento_tipo 
	FROM pagamento.tb_codigo_evento_tipo ( NOLOCK )
	WHERE 
       ( nullif( @id_evento_tipo, 0 ) is null or id_evento_tipo = @id_evento_tipo )
	   ORDER BY ds_evento_tipo
END