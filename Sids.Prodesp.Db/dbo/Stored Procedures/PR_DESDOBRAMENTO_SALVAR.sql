-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para salvar ou alterar desdobramento
-- ===================================================================  
CREATE procedure [dbo].[PR_DESDOBRAMENTO_SALVAR] 
	@id_desdobramento int = null
	,@id_tipo_desdobramento int = NULL
	,@id_tipo_documento int = NULL
	,@id_credor int = NULL
	,@nr_documento varchar(19) = NULL
	,@nr_contrato varchar(13) = NULL
	,@cd_servico varchar(5) = NULL
	,@ds_servico varchar(50) = NULL
	,@ds_credor varchar(50) = NULL
	,@nm_reduzido_credor varchar(14) = NULL
	,@cd_tipo_despesa varchar(25) = NULL
	,@bl_aceitar_credor bit = NULL
	,@nr_tipo_desdobramento int = NULL
	,@vr_distribuicao decimal(18,2) = NULL
	,@vr_total_issqn decimal(18,2) = NULL
	,@vr_total_ir decimal(18,2) = NULL
	,@vr_total_inss decimal(18,2) = NULL
	,@dt_emissao datetime = NULL
	,@cd_aplicacao_obra varchar(8) = NULL
	,@bl_transmitir_prodesp bit = NULL
	,@bl_transmitido_prodesp bit = NULL
	,@ds_status_prodesp char(1) = NULL
	,@dt_transmitido_prodesp datetime = NULL
	,@ds_transmissao_mensagem_prodesp varchar(128) = NULL
	,@ds_status_documento bit = NULL
	,@bl_cadastro_completo bit = NULL
	,@bl_situacao_desdobramento char(1) = NULL
	,@id_regional smallint = NULL
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_desdobramento](nolock)
		where	id_desdobramento = @id_desdobramento
	)
	begin

		update [contaunica].[tb_desdobramento] set 
			id_tipo_desdobramento	=	 nullif(@id_tipo_desdobramento,0)
			,id_tipo_documento	=	 nullif(@id_tipo_documento,0)
			,id_credor	=	 @id_credor
			,nr_documento	=	 @nr_documento
			,nr_contrato	=	 @nr_contrato
			,cd_servico	=	 @cd_servico
			,ds_servico	=	 @ds_servico
			,ds_credor	=	 @ds_credor
			,nm_reduzido_credor	=	 @nm_reduzido_credor
			,cd_tipo_despesa	=	 @cd_tipo_despesa
			,bl_aceitar_credor	=	 @bl_aceitar_credor
			,nr_tipo_desdobramento	=	 nullif(@nr_tipo_desdobramento,0)
			,vr_distribuicao	=	 @vr_distribuicao
			,vr_total_issqn	=	 @vr_total_issqn
			,vr_total_ir	=	 @vr_total_ir
			,vr_total_inss	=	 @vr_total_inss
			,dt_emissao	=	 @dt_emissao
			,cd_aplicacao_obra	=	 @cd_aplicacao_obra
			,bl_transmitir_prodesp	=	 @bl_transmitir_prodesp
			,bl_transmitido_prodesp	=	 @bl_transmitido_prodesp
			,ds_status_prodesp	=	 @ds_status_prodesp
			,dt_transmitido_prodesp	=	 @dt_transmitido_prodesp
			,ds_transmissao_mensagem_prodesp	=	 @ds_transmissao_mensagem_prodesp
			,ds_status_documento	=	 @ds_status_documento
			,bl_cadastro_completo	=	 @bl_cadastro_completo
			,bl_situacao_desdobramento	=	 @bl_situacao_desdobramento
			,id_regional = @id_regional
		where	id_desdobramento = @id_desdobramento;

		select @id_desdobramento;

	end
	else
	begin

		insert into [contaunica].[tb_desdobramento] (
			dt_cadastro										
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
		   ,id_regional							
		)
		values
		(
			getdate()
			,nullif(@id_tipo_desdobramento,0)
			,nullif(@id_tipo_documento,0)
			,@id_credor
			,@nr_documento
			,@nr_contrato
			,@cd_servico
			,@ds_servico
			,@ds_credor
			,@nm_reduzido_credor
			,@cd_tipo_despesa
			,@bl_aceitar_credor
			,nullif(@nr_tipo_desdobramento,0)
			,@vr_distribuicao
			,@vr_total_issqn
			,@vr_total_ir
			,@vr_total_inss
			,@dt_emissao
			,@cd_aplicacao_obra
			,@bl_transmitir_prodesp
			,@bl_transmitido_prodesp
			,'N'
			,@dt_transmitido_prodesp
			,@ds_transmissao_mensagem_prodesp
			,@ds_status_documento
			,@bl_cadastro_completo
			,'N'
			,nullif(@id_regional,0)
		);

		select scope_identity();

	end

end