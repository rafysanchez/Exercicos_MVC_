-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consulta de tipo_reclassificacao_retencao
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_RECLASSIFICACAO_RETENCAO_CONSULTAR]  
	@id_tipo_reclassificacao_retencao	INT = 0  
,	@ds_tipo_reclassificacao_retencao	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_reclassificacao_retencao]
      ,[ds_tipo_reclassificacao_retencao]
  FROM [contaunica].[tb_tipo_reclassificacao_retencao]( NOLOCK )
	WHERE 
       ( @id_tipo_reclassificacao_retencao = 0 or [id_tipo_reclassificacao_retencao] = @id_tipo_reclassificacao_retencao ) and
	   ( @ds_tipo_reclassificacao_retencao is null or [ds_tipo_reclassificacao_retencao] = @ds_tipo_reclassificacao_retencao )
	   ORDER BY ds_tipo_reclassificacao_retencao
END