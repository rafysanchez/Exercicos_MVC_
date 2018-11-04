-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para consulta de tipo de programacao desembolso
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_PROGRAMACAO_DESEMBOLSO_CONSULTAR]  
	@id_tipo_programacao_desembolso	INT = 0  
,	@ds_tipo_programacao_desembolso	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_programacao_desembolso]
      ,[ds_tipo_programacao_desembolso]
  FROM [contaunica].[tb_tipo_programacao_desembolso] ( NOLOCK )
	WHERE 
       ( @id_tipo_programacao_desembolso = 0 or id_tipo_programacao_desembolso = @id_tipo_programacao_desembolso ) and
	   ( @ds_tipo_programacao_desembolso is null or ds_tipo_programacao_desembolso = @ds_tipo_programacao_desembolso )
	   ORDER BY ds_tipo_programacao_desembolso
END