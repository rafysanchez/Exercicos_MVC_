-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consultar desdobramento grid
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_DESDOBRAMENTO_CONSULTAR_GRID]
	@id_desdobramento int = null
	,@id_tipo_desdobramento int = NULL
	,@id_tipo_documento int = NULL
	,@nr_documento varchar(19) = NULL
	,@nr_contrato varchar(11) = NULL
	,@cd_aplicacao_obra varchar(9) = NULL
	,@ds_status_prodesp char(1) = NULL
	,@dt_cadastramento_de date = NULL
	,@dt_cadastramento_ate date = NULL
	,@id_regional smallint = null
	,@bl_situacao_desdobramento char(1) = null
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
      --,[nr_tipo_documento]
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
		and (nullif( @id_tipo_desdobramento, 0 ) is null or [id_tipo_desdobramento] = @id_tipo_desdobramento )
		and (nullif( @id_tipo_documento, 0 ) is null or [id_tipo_documento] = @id_tipo_documento )
		and (@nr_documento is null or nr_documento = @nr_documento )
		and (@ds_status_prodesp is null or ds_status_prodesp = @ds_status_prodesp )
		and (@nr_contrato is null or nr_contrato = @nr_contrato )
		and (@cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )
	
		and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de ) 
		and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate ) 

		and ( @bl_situacao_desdobramento is null or bl_situacao_desdobramento = @bl_situacao_desdobramento ) 
	Order by id_desdobramento
end;