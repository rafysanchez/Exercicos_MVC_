-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consultar identificacao desdobramento
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IDENTIFICACAO_DESDOBRAMENTO_CONSULTAR]
	@id_desdobramento int = 0,
	@id_identificacao_desdobramento int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		[id_identificacao_desdobramento]
      ,[id_reter]
      ,[id_desdobramento]
	  ,id_tipo_desdobramento
      ,[ds_nome_reduzido_credor]
      ,[vr_percentual_base_calculo]
      ,[vr_desdobrado]
	  ,vr_desdobrado_inicial
      ,[bl_tipo_bloqueio]
      ,[bl_transmitido_prodesp]
      ,[ds_status_prodesp]
      ,[dt_transmitido_prodesp]
      ,[ds_transmissao_mensagem_prodesp]
	  ,vr_distribuicao
	  , nr_sequencia
  FROM [contaunica].[tb_identificacao_desdobramento] (nolock)
	where
		( nullif( @id_desdobramento, 0 ) is null or id_desdobramento = @id_desdobramento )
        And ( nullif( @id_identificacao_desdobramento, 0 ) is null or id_identificacao_desdobramento = @id_identificacao_desdobramento )
	Order by id_identificacao_desdobramento
end;