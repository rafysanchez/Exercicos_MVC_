-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de para resto pagar
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_RESTO_APAGAR_CONSULTAR]  
	@id_resto_pagar	INT = 0  
,	@ds_resto_pagar	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_resto_pagar]
      ,[ds_resto_pagar]
  FROM [contaunica].[tb_para_resto_pagar] ( NOLOCK )
	WHERE 
       ( @id_resto_pagar = 0 or [id_resto_pagar] = @id_resto_pagar ) and
	   ( @ds_resto_pagar is null or [ds_resto_pagar] = @ds_resto_pagar )
	   ORDER BY ds_resto_pagar
END