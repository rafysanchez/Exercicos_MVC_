-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 128/12/2016
-- Description: Procedure para consulta de origem do material
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_ORIGEM_MATERIAL_CONSULTAR]  
	@id_origem_material	INT = 0  
,	@ds_origem_material	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_origem_material
	,	ds_origem_material
	FROM empenho.tb_origem_material ( NOLOCK )
	WHERE 
       ( @id_origem_material = 0 or id_origem_material = @id_origem_material ) and
	   ( @ds_origem_material is null or ds_origem_material = @ds_origem_material )
	   ORDER BY id_origem_material
END