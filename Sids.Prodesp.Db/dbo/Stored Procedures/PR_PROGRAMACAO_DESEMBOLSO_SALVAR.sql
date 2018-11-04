-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para salvar ou alterar programacao desembolso
-- ===================================================================  
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_SALVAR] 
    @id_programacao_desembolso int = NULL
	,@id_tipo_programacao_desembolso int = NULL
	,@id_tipo_documento int = NULL
	,@nr_siafem_siafisico varchar(11) = NULL
	,@nr_contrato varchar(13) = NULL
	,@nr_processo varchar(15) = NULL
	,@nr_documento varchar(19) = NULL
	,@cd_unidade_gestora varchar(6) = NULL
	,@cd_gestao varchar(5) = NULL
	,@vl_total decimal(18,0) = NULL
	,@dt_emissao date = NULL
	,@id_regional smallint = NULL
	,@cd_aplicacao_obra varchar(140) = NULL
	,@nr_lista_anexo varchar(11) = NULL
	,@nr_nl_referencia varchar(11) = NULL
	,@ds_finalidade varchar(40) = NULL
	,@cd_despesa varchar(2) = NULL
	,@dt_vencimento date = NULL
	,@nr_cnpj_cpf_credor varchar(15) = NULL
	,@cd_gestao_credor varchar(140) = NULL
	,@nr_banco_credor varchar(30) = NULL
	,@nr_agencia_credor varchar(10) = NULL
	,@nr_conta_credor varchar(15) = NULL
	,@nr_cnpj_cpf_pgto varchar(15) = NULL
	,@cd_gestao_pgto varchar(140) = NULL
	,@nr_banco_pgto varchar(30) = NULL
	,@nr_agencia_pgto varchar(10) = NULL
	,@nr_conta_pgto varchar(15) = NULL
	,@fl_sistema_siafem_siafisico bit = NULL
	,@cd_transmissao_status_siafem_siafisico char(1) = NULL
	,@fl_transmissao_transmitido_siafem_siafisico bit = NULL
	,@dt_transmissao_transmitido_siafem_siafisico date = NULL
	,@ds_transmissao_mensagem_siafem_siafisico varchar(140) = NULL
	,@bl_cadastro_completo bit = NULL
	,@dt_cadastro date = NULL
	,@bl_bloqueio bit = NULL
	,@bl_cancelado bit = NULL
	,@nr_documento_gerador varchar(22) = null
	,@nr_agrupamento int = null
	,@ds_causa_cancelamento varchar(200) = null
	,@obs varchar(230) = null
	,@nr_ne varchar(11) = null
    ,@nr_ct varchar(11) = null
	,@rec_despesa varchar(8) = null
	,@id_autorizacao_ob int = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_programacao_desembolso](nolock)
		where	id_programacao_desembolso = @id_programacao_desembolso
	)
	begin

		update [contaunica].[tb_programacao_desembolso] set 
			id_tipo_programacao_desembolso	= nullif(@id_tipo_programacao_desembolso,0)
			,id_tipo_documento	=	 nullif(@id_tipo_documento,0)
			,id_regional = nullif(@id_regional,0)
			,nr_siafem_siafisico = @nr_siafem_siafisico
			,nr_contrato = @nr_contrato
			,nr_processo = @nr_processo
			,nr_documento = @nr_documento
			,cd_unidade_gestora = @cd_unidade_gestora
			,cd_gestao = @cd_gestao
			,vl_total = @vl_total
			,dt_emissao = @dt_emissao
			,cd_aplicacao_obra = @cd_aplicacao_obra
			,nr_lista_anexo = @nr_lista_anexo
			,nr_nl_referencia = @nr_nl_referencia
			,ds_finalidade = @ds_finalidade
			,cd_despesa = @cd_despesa
			,dt_vencimento = @dt_vencimento
			,nr_cnpj_cpf_credor = @nr_cnpj_cpf_credor
			,cd_gestao_credor = @cd_gestao_credor
			,nr_banco_credor = @nr_banco_credor
			,nr_agencia_credor = @nr_agencia_credor
			,nr_conta_credor = @nr_conta_credor
			,nr_cnpj_cpf_pgto = @nr_cnpj_cpf_pgto
			,cd_gestao_pgto = @cd_gestao_pgto
			,nr_banco_pgto = @nr_banco_pgto
			,nr_agencia_pgto = @nr_agencia_pgto
			,nr_conta_pgto = @nr_conta_pgto
			,fl_sistema_siafem_siafisico = @fl_sistema_siafem_siafisico
			,cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico
			,fl_transmissao_transmitido_siafem_siafisico = @fl_transmissao_transmitido_siafem_siafisico
			,dt_transmissao_transmitido_siafem_siafisico = @dt_transmissao_transmitido_siafem_siafisico
			,ds_transmissao_mensagem_siafem_siafisico = @ds_transmissao_mensagem_siafem_siafisico
			,bl_cadastro_completo = @bl_cadastro_completo
			--,cd_bloqueio = @cd_bloqueio
			,nr_documento_gerador = @nr_documento_gerador
			,ds_causa_cancelamento = @ds_causa_cancelamento 
			,bl_bloqueio = @bl_bloqueio
			,bl_cancelado = @bl_cancelado
			,obs = @obs
			,nr_ct = @nr_ct
			,nr_ne = @nr_ne
			,@rec_despesa = @rec_despesa

		where	id_programacao_desembolso = @id_programacao_desembolso;

		select @id_programacao_desembolso;

	end
	else
	begin

		insert into [contaunica].[tb_programacao_desembolso] (
			[id_tipo_programacao_desembolso]
           ,[id_tipo_documento]
           ,[dt_cadastro]
           ,[nr_siafem_siafisico]
           ,[nr_contrato]
           ,[nr_processo]
           ,[nr_documento]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[vl_total]
           ,[dt_emissao]
           ,[id_regional]
           ,[cd_aplicacao_obra]
           ,[nr_lista_anexo]
           ,[nr_nl_referencia]
           ,[ds_finalidade]
           ,[cd_despesa]
           ,[dt_vencimento]
           ,[nr_cnpj_cpf_credor]
           ,[cd_gestao_credor]
           ,[nr_banco_credor]
           ,[nr_agencia_credor]
           ,[nr_conta_credor]
           ,[nr_cnpj_cpf_pgto]
           ,[cd_gestao_pgto]
           ,[nr_banco_pgto]
           ,[nr_agencia_pgto]
           ,[nr_conta_pgto]
           ,[fl_sistema_siafem_siafisico]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[bl_cadastro_completo]
		   ,[nr_documento_gerador]
		   ,[nr_agrupamento] 	
		   ,[obs]
		   ,[nr_ne]
		   ,[nr_ct]	
		   ,[rec_despesa]	
		)
		values
		(
		nullif(@id_tipo_programacao_desembolso,0)
		,nullif(@id_tipo_documento,0)
		,getdate()
		,@nr_siafem_siafisico
		,@nr_contrato
		,@nr_processo
		,@nr_documento
		,@cd_unidade_gestora
		,@cd_gestao
		,@vl_total
		,@dt_emissao
		,nullif(@id_regional,0)
		,@cd_aplicacao_obra
		,@nr_lista_anexo
		,@nr_nl_referencia
		,@ds_finalidade
		,@cd_despesa
		,@dt_vencimento
		,@nr_cnpj_cpf_credor
		,@cd_gestao_credor
		,@nr_banco_credor
		,@nr_agencia_credor
		,@nr_conta_credor
		,@nr_cnpj_cpf_pgto
		,@cd_gestao_pgto
		,@nr_banco_pgto
		,@nr_agencia_pgto
		,@nr_conta_pgto
		,@fl_sistema_siafem_siafisico
		,'N'
		,@fl_transmissao_transmitido_siafem_siafisico
		,@dt_transmissao_transmitido_siafem_siafisico
		,@ds_transmissao_mensagem_siafem_siafisico
		,@bl_cadastro_completo
		,@nr_documento_gerador
		,nullif(@nr_agrupamento,0) 	
		,@obs
		,@nr_ne
		,@nr_ct
		,@rec_despesa
		);

		select scope_identity();

	end

end