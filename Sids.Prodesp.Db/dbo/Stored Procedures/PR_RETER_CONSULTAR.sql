-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de reter
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_RETER_CONSULTAR]  
	@id_Reter	INT = 0  
,	@ds_Reter	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_Reter]
      ,[ds_Reter]
	FROM [contaunica].[tb_Reter] ( NOLOCK )
	WHERE 
       ( @id_Reter = 0 or [id_Reter] = @id_Reter ) and
	   ( @ds_Reter is null or [ds_Reter] = @ds_Reter )
	   ORDER BY [ds_Reter]
END