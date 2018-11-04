-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de tipo de documento
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_DOCUMENTO_CONSULTAR]  
	@id_tipo_documento	INT = 0  
,	@ds_tipo_documento	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_documento]
      ,[ds_tipo_documento]
  FROM [contaunica].[tb_tipo_documento] ( NOLOCK )
	WHERE 
       ( @id_tipo_documento = 0 or id_tipo_documento = @id_tipo_documento ) and
	   ( @ds_tipo_documento is null or ds_tipo_documento = @ds_tipo_documento )
	   ORDER BY ds_tipo_documento
END