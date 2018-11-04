-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para salvar ou alterar de identificacao desdobramento 
-- ===================================================================  
CREATE procedure [dbo].[PR_IDENTIFICACAO_DESDOBRAMENTO_SALVAR] 
	@id_identificacao_desdobramento int = null
	,@id_reter int = null
    ,@id_desdobramento int = null
	,@id_tipo_desdobramento int = null
    ,@ds_nome_reduzido_credor varchar(20) = null
    ,@vr_percentual_base_calculo decimal(18,2) = null
    ,@vr_desdobrado decimal(18,2) = null
	,@vr_distribuicao decimal(18,2) = null
	,@vr_desdobrado_inicial decimal(18,2) = null
    ,@bl_tipo_bloqueio varchar(10) = null
	,@bl_transmitido_prodesp bit = null
	,@ds_status_prodesp char(1) = NULL
	,@dt_transmitido_prodesp date = null
	,@ds_transmissao_mensagem_prodesp varchar(128) = NULL
	,@nr_sequencia int = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_identificacao_desdobramento](nolock)
		where	id_identificacao_desdobramento = @id_identificacao_desdobramento
	)
	begin

		update [contaunica].[tb_identificacao_desdobramento]set 
			 id_reter = nullif(@id_reter,0)
			,id_desdobramento = nullif(@id_desdobramento,0)
			,id_tipo_desdobramento = nullif(@id_tipo_desdobramento,0)
			,ds_nome_reduzido_credor = @ds_nome_reduzido_credor
			,vr_percentual_base_calculo = @vr_percentual_base_calculo
			,vr_desdobrado = @vr_desdobrado
			,vr_desdobrado_inicial = @vr_desdobrado_inicial
			,vr_distribuicao = @vr_distribuicao
			,bl_tipo_bloqueio = @bl_tipo_bloqueio
			,bl_transmitido_prodesp = @bl_transmitido_prodesp
			,ds_status_prodesp = @ds_status_prodesp
			,dt_transmitido_prodesp = @dt_transmitido_prodesp
			,ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
			,nr_sequencia  = @nr_sequencia 
		where	id_identificacao_desdobramento = @id_identificacao_desdobramento;

		select @id_identificacao_desdobramento;

	end
	else
	begin

		INSERT INTO [contaunica].[tb_identificacao_desdobramento]
           (id_reter
		  ,id_desdobramento
		  ,id_tipo_desdobramento
		  ,ds_nome_reduzido_credor
		  ,vr_percentual_base_calculo
		  ,vr_desdobrado
		  ,vr_desdobrado_inicial
		  ,vr_distribuicao
		  ,bl_tipo_bloqueio
		  ,bl_transmitido_prodesp
		  ,ds_status_prodesp
		  ,dt_transmitido_prodesp
		  ,ds_transmissao_mensagem_prodesp
		  ,nr_sequencia )
     VALUES
           (nullif(@id_reter,0)
           ,nullif(@id_desdobramento,0)
		   ,nullif(@id_tipo_desdobramento,0)
           ,@ds_nome_reduzido_credor
           ,@vr_percentual_base_calculo
           ,@vr_desdobrado
		   ,@vr_desdobrado_inicial
		   ,@vr_distribuicao
           ,@bl_tipo_bloqueio
		   ,@bl_transmitido_prodesp
		  ,@ds_status_prodesp
		  ,@dt_transmitido_prodesp
		  ,@ds_transmissao_mensagem_prodesp
		  ,@nr_sequencia );

		select scope_identity();

	end

end