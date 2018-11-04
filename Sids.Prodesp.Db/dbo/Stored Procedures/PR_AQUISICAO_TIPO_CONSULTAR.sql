-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 29/12/2016
-- Description: Procedure para consulta de tipos de aquisição
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_AQUISICAO_TIPO_CONSULTAR]  
	@id_aquisicao_tipo	INT = 0  
,	@ds_aquisicao_tipo	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_aquisicao_tipo
	,	ds_aquisicao_tipo 
	FROM empenho.tb_aquisicao_tipo ( NOLOCK )
	WHERE 
       ( @id_aquisicao_tipo = 0 or id_aquisicao_tipo = @id_aquisicao_tipo ) and
	   ( @ds_aquisicao_tipo is null or ds_aquisicao_tipo = @ds_aquisicao_tipo )
	   ORDER BY ds_aquisicao_tipo
END