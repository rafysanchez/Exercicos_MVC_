-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de tipo de desdobramento
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_DESDOBRAMENTO_CONSULTAR]  
	@id_tipo_desdobramento	INT = 0  
,	@ds_tipo_desdobramento	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_desdobramento]
      ,[ds_tipo_desdobramento]
  FROM [contaunica].[tb_tipo_desdobramento] ( NOLOCK )
	WHERE 
       ( @id_tipo_desdobramento = 0 or id_tipo_desdobramento = @id_tipo_desdobramento ) and
	   ( @ds_tipo_desdobramento is null or ds_tipo_desdobramento = @ds_tipo_desdobramento )
	   ORDER BY ds_tipo_desdobramento
END