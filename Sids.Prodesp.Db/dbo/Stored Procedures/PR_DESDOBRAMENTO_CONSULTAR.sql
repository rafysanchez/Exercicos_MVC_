-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consultar desdobramento
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_DESDOBRAMENTO_CONSULTAR]
	@id_desdobramento int = 0,
	@id_regional int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  [id_desdobramento]
      ,[id_tipo_desdobramento]
      ,[id_tipo_documento]
      ,[id_credor]
      ,[nr_documento]
      ,[nr_contrato]
      ,[cd_servico]
      ,[ds_servico]
      ,[ds_credor]
      ,[nm_reduzido_credor]
      ,[cd_tipo_despesa]
      ,[bl_aceitar_credor]
      ,[nr_tipo_desdobramento]
      ,[vr_distribuicao]
      ,[vr_total_issqn]
      ,[vr_total_ir]
      ,[vr_total_inss]
      ,[dt_emissao]
      ,[cd_aplicacao_obra]
      ,[bl_transmitir_prodesp]
      ,[bl_transmitido_prodesp]
      ,[ds_status_prodesp]
      ,[dt_transmitido_prodesp]
      ,[ds_transmissao_mensagem_prodesp]
      ,[ds_status_documento]
      ,[bl_cadastro_completo]
      ,[bl_situacao_desdobramento]
      ,[dt_cadastro]
	  ,[id_regional]
  FROM [contaunica].[tb_desdobramento] (nolock)
	where
		( nullif( @id_desdobramento, 0 ) is null or id_desdobramento = @id_desdobramento )
        And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
	Order by id_desdobramento
end;