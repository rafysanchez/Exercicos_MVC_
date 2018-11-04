-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de tipo de de boleto
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_BOLETO_CONSULTAR]  
	@id_tipo_de_boleto	INT = 0  
,	@ds_tipo_de_boleto	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_de_boleto]
      ,[ds_tipo_de_boleto]
  FROM [contaunica].[tb_tipo_de_boleto] ( NOLOCK )
	WHERE 
       ( @id_tipo_de_boleto = 0 or id_tipo_de_boleto = @id_tipo_de_boleto ) and
	   ( @ds_tipo_de_boleto is null or ds_tipo_de_boleto = @ds_tipo_de_boleto )
	   ORDER BY ds_tipo_de_boleto
END