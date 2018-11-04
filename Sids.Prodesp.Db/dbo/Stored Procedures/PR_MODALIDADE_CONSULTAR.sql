-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 128/12/2016
-- Description: Procedure para consulta de modalidades
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_MODALIDADE_CONSULTAR]  
	@id_modalidade	INT = 0  
,	@ds_modalidade	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_modalidade
	,	ds_modalidade 
	FROM empenho.tb_modalidade ( NOLOCK )
	WHERE 
       ( @id_modalidade = 0 or id_modalidade = @id_modalidade ) and
	   ( @ds_modalidade is null or ds_modalidade = @ds_modalidade )
	   ORDER BY id_modalidade
END