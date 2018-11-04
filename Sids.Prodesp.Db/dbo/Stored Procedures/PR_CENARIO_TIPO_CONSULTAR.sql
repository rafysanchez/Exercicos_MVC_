-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 09/02/2017
-- Description: Procedure para consulta de tipos de cenario
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_CENARIO_TIPO_CONSULTAR]  
	@id_cenario_tipo	INT = 0
AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_cenario_tipo
	,	ds_cenario_tipo
	,	nm_servico
	FROM pagamento.tb_cenario_tipo ( NOLOCK )
	WHERE 
       ( nullif( @id_cenario_tipo, 0 ) is null or id_cenario_tipo = @id_cenario_tipo )
	   ORDER BY ds_cenario_tipo
END