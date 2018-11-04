-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para salvar ou alterar de agrupamentos para programacao desembolso agrupamento
-- ===================================================================
CREATE procedure [dbo].[PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_SALVAR]
	@id_programacao_desembolso_agrupamento int = null
	,@id_programacao_desembolso	 int	 = null
	,@id_tipo_documento	 int	 = null
	,@nr_agrupamento	 varchar(10)	 = null
	,@vl_valor	 decimal(18)	 = null
	,@nr_programacao_desembolso	 varchar(11)	 = null
	,@nr_sequencia	 int	 = null
	,@id_regional	 smallint	 = null
	,@nr_documento	 varchar(19)	 = null
	,@cd_unidade_gestora	 varchar(6)	 = null
	,@cd_gestao	 varchar(5)	 = null
	,@nr_nl_referencia	 varchar(11)	 = null
	,@nr_cnpj_cpf_credor	 varchar(15)	 = null
	,@cd_gestao_credor	 varchar(140)	 = null
	,@nr_banco_credor	 varchar(30)	 = null
	,@nr_agencia_credor	 varchar(10)	 = null
	,@nr_conta_credor	 varchar(15)	 = null
	,@nm_reduzido_credor	 varchar(14)	 = null
	,@nr_cnpj_cpf_pgto	 varchar(15)	 = null
	,@cd_gestao_pgto	 varchar(140)	 = null
	,@nr_banco_pgto	 varchar(30)	 = null
	,@nr_agencia_pgto	 varchar(10)	 = null
	,@nr_conta_pgto	 varchar(15)	 = null
	,@nr_processo	 varchar(15)	 = null
	,@ds_finalidade	 varchar(40)	 = null
	,@cd_fonte	 varchar(10)	 = null
	,@cd_evento	 varchar(6)	 = null
	,@cd_classificacao	 varchar(9)	 = null
	,@ds_inscricao	 varchar(22)	 = null
	,@cd_despesa	 varchar(2)	 = null
	,@dt_emissao	 date	 = null
	,@dt_vencimento	 date	 = null
	,@nr_lista_anexo	 varchar(11)	 = null
	,@cd_transmissao_status_siafem_siafisico	 char(1)	 = null
	,@fl_transmissao_transmitido_siafem_siafisico	 bit	 = null
	,@dt_transmissao_transmitido_siafem_siafisico	 date	 = null
	,@ds_msg_retorno	 varchar(256)	 = null
	,@nr_documento_gerador	 varchar(22)	 = null
	,@dt_cadastro date = null
	,@ds_causa_cancelamento varchar(200) = null
	,@bl_bloqueio bit = NULL
	,@bl_cancelado bit = NULL
	,@nr_siafem_siafisico varchar(11) = NULL
	,@fl_sistema_siafem_siafisico bit = NULL
	,@bl_cadastro_completo bit = NULL
	,@cd_aplicacao_obra varchar(140) = NULL
	,@nr_contrato varchar(13) = NULL
	,@id_tipo_programacao_desembolso int = null
	,@rec_despesa varchar(8) = null
	,@nr_op int = null
	,@cd_transmissao_status_prodesp varchar(1) = null
	,@ds_transmissao_mensagem_prodesp varchar(100) = null
	,@id_execucao_pd int = null
	,@id_autorizacao_ob int = null


as

declare @num_sequencia int;
begin
	set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_agrupamento
		where	id_programacao_desembolso_agrupamento = @id_programacao_desembolso_agrupamento
			   and   id_programacao_desembolso = @id_programacao_desembolso  
			
	)
	begin
	
		update contaunica.tb_programacao_desembolso_agrupamento set 
				id_tipo_documento	=	 nullif(@id_tipo_documento,0)
				,id_regional = nullif(@id_regional,0)
				,id_programacao_desembolso = nullif(@id_programacao_desembolso,0)
				--,nr_agrupamento = @nr_agrupamento
				,vl_valor = @vl_valor
				,nr_programacao_desembolso = @nr_siafem_siafisico
				,nr_sequencia = @nr_sequencia
				,nr_documento = @nr_documento
				,cd_unidade_gestora = @cd_unidade_gestora
				,cd_gestao = @cd_gestao
				,nr_nl_referencia = @nr_nl_referencia
				,nr_cnpj_cpf_credor = @nr_cnpj_cpf_credor
				,cd_gestao_credor = @cd_gestao_credor
				,nr_banco_credor = @nr_banco_credor
				,nr_agencia_credor = @nr_agencia_credor
				,nr_conta_credor = @nr_conta_credor
				,nm_reduzido_credor = @nm_reduzido_credor
				,nr_cnpj_cpf_pgto = @nr_cnpj_cpf_pgto
				,cd_gestao_pgto = @cd_gestao_pgto
				,nr_banco_pgto = @nr_banco_pgto
				,nr_agencia_pgto = @nr_agencia_pgto
				,nr_conta_pgto = @nr_conta_pgto
				,nr_processo = @nr_processo
				,ds_finalidade = @ds_finalidade
				,cd_fonte = @cd_fonte
				,cd_evento = @cd_evento
				,cd_classificacao = @cd_classificacao
				,ds_inscricao = @ds_inscricao
				,cd_despesa = @cd_despesa
				,dt_emissao = @dt_emissao
				,dt_vencimento = @dt_vencimento
				,nr_lista_anexo = @nr_lista_anexo
				,cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico
				,fl_transmissao_transmitido_siafem_siafisico = @fl_transmissao_transmitido_siafem_siafisico
				,dt_transmissao_transmitido_siafem_siafisico = @dt_transmissao_transmitido_siafem_siafisico
				,ds_msg_retorno = @ds_msg_retorno
				,nr_documento_gerador = @nr_documento_gerador
				,ds_causa_cancelamento = @ds_causa_cancelamento
				,bl_bloqueio = @bl_bloqueio
				,bl_cancelado = @bl_cancelado
				,rec_despesa = @rec_despesa

		where	id_programacao_desembolso_agrupamento = @id_programacao_desembolso_agrupamento

		select @id_programacao_desembolso_agrupamento;

	end
	else
	begin
	


		insert into contaunica.tb_programacao_desembolso_agrupamento 
				([id_programacao_desembolso]
           ,[id_tipo_documento]
           ,[nr_agrupamento]
           ,[vl_valor]
           ,[nr_programacao_desembolso]
           ,[nr_sequencia]
           ,[id_regional]
           ,[nr_documento]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[nr_nl_referencia]
           ,[nr_cnpj_cpf_credor]
           ,[cd_gestao_credor]
           ,[nr_banco_credor]
           ,[nr_agencia_credor]
           ,[nr_conta_credor]
           ,[nm_reduzido_credor]
           ,[nr_cnpj_cpf_pgto]
           ,[cd_gestao_pgto]
           ,[nr_banco_pgto]
           ,[nr_agencia_pgto]
           ,[nr_conta_pgto]
           ,[nr_processo]
           ,[ds_finalidade]
           ,[cd_fonte]
           ,[cd_evento]
           ,[cd_classificacao]
           ,[ds_inscricao]
           ,[cd_despesa]
           ,[dt_emissao]
           ,[dt_vencimento]
           ,[nr_lista_anexo]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_msg_retorno]
           ,[nr_documento_gerador]
           ,[dt_cadastro]
		   ,[rec_despesa])
		
		values (
		nullif(@id_programacao_desembolso,0)
		,nullif(@id_tipo_documento,0)
			,nullif(@nr_agrupamento,0)
			,@vl_valor
			,@nr_siafem_siafisico
			,@nr_sequencia
		,nullif(@id_regional,0)
			,@nr_documento
			,@cd_unidade_gestora
			,@cd_gestao
			,@nr_nl_referencia
			,@nr_cnpj_cpf_credor
			,@cd_gestao_credor
			,@nr_banco_credor
			,@nr_agencia_credor
			,@nr_conta_credor
			,@nm_reduzido_credor
			,@nr_cnpj_cpf_pgto
			,@cd_gestao_pgto
			,@nr_banco_pgto
			,@nr_agencia_pgto
			,@nr_conta_pgto
			,@nr_processo
			,@ds_finalidade
			,@cd_fonte
			,@cd_evento
			,@cd_classificacao
			,@ds_inscricao
			,@cd_despesa
			,@dt_emissao
			,@dt_vencimento
			,@nr_lista_anexo
			,@cd_transmissao_status_siafem_siafisico
			,@fl_transmissao_transmitido_siafem_siafisico
			,@dt_transmissao_transmitido_siafem_siafisico
			,@ds_msg_retorno
			,@nr_documento_gerador
			,getdate()
			,@rec_despesa
		)			
           
		select scope_identity();

	end
	
end