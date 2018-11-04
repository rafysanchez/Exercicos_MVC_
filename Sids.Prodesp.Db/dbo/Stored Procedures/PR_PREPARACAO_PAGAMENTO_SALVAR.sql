-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 08/08/2017
-- Description: Procedure para salvar ou alterar preparacao pagamento
-- ===================================================================  
CREATE procedure [dbo].[PR_PREPARACAO_PAGAMENTO_SALVAR] 
	@id_preparacao_pagamento INTEGER  = NULL,
	@id_regional smallint  = NULL,
	@id_tipo_documento INTEGER  = NULL,
	@id_tipo_preparacao_pagamento INTEGER  = NULL,
	@nr_op_inicial VARCHAR(18) = NULL,
	@nr_op_final VARCHAR(18) = NULL,
	@nr_ano_exercicio INTEGER  = NULL,
	@cd_credor_organizacao CHAR(1) = NULL,
	@nr_cpf_cnpj_credor VARCHAR(15) = NULL,
	@cd_assinatura VARCHAR(5) = NULL,
	@cd_grupo_assinatura VARCHAR(1) = NULL,
	@cd_orgao_assinatura VARCHAR(2) = NULL,
	@ds_cargo_assinatura VARCHAR(55) = NULL,
	@nm_assinatura VARCHAR(55) = NULL,
	@cd_contra_assinatura VARCHAR(5) = NULL,
	@cd_grupo_contra_assinatura VARCHAR(1) = NULL,
	@cd_orgao_contra_assinatura VARCHAR(2) = NULL,
	@nm_contra_assinatura VARCHAR(55) = NULL,
	@ds_cargo_contra_assinatura VARCHAR(55) = NULL,
	@nr_documento VARCHAR(19) = NULL,
	@vr_documento DECIMAL(18,2) = NULL,
	@cd_conta VARCHAR(3) = NULL,
	@nr_banco VARCHAR(30) = NULL,
	@nr_agencia VARCHAR(10) = NULL,
	@nr_conta VARCHAR(15) = NULL,
	@cd_despesa VARCHAR(2) = NULL,
	@dt_vencimento DATE = NULL,
	@ds_referencia VARCHAR(60) = NULL,
	@ds_despesa_credor VARCHAR(50) = NULL,
	@nr_contrato VARCHAR(9) = NULL,
	@ds_credor1 VARCHAR(70) = NULL,
	@ds_credor2 VARCHAR(70) = NULL,
	@ds_endereco VARCHAR(40) = NULL,
	@nr_cep VARCHAR(9) = NULL,
	@nr_banco_credor VARCHAR(30) = NULL,
	@nr_agencia_credor VARCHAR(10) = NULL,
	@nr_conta_credor VARCHAR(15) = NULL,
	@nr_banco_pgto VARCHAR(30) = NULL,
	@nr_agencia_pgto VARCHAR(10) = NULL,
	@nr_conta_pgto VARCHAR(15) = NULL,
	@dt_emissao DATETIME = NULL,
	@qt_op_preparada INTEGER  = NULL,
	@vr_total DECIMAL(18,2) = NULL,
	@bl_transmitir_prodesp BIT = NULL,
	@bl_transmitido_prodesp BIT = NULL,
	@ds_status_prodesp CHAR(1) = NULL,
	@ds_transmissao_mensagem_prodesp VARCHAR(256) = NULL,
	@dt_transmitido_prodesp DATE = NULL,
	@ds_status_documento BIT = NULL,
	@bl_cadastro_completo BIT = NULL,
	@dt_cadastro DATE = NULL
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_preparacao_pagamento](nolock)
		where	id_preparacao_pagamento = @id_preparacao_pagamento
	)
	begin

		update [contaunica].[tb_preparacao_pagamento] set 
			id_tipo_preparacao_pagamento	=	 nullif(@id_tipo_preparacao_pagamento,0)
			,id_tipo_documento	=	 nullif(@id_tipo_documento,0)
			,id_regional = nullif(@id_regional,0),
			nr_op_inicial = @nr_op_inicial,
			nr_op_final = @nr_op_final,
			nr_ano_exercicio = @nr_ano_exercicio,
			cd_credor_organizacao = @cd_credor_organizacao,
			nr_cpf_cnpj_credor = @nr_cpf_cnpj_credor,
			cd_assinatura = @cd_assinatura,
			cd_grupo_assinatura = @cd_grupo_assinatura,
			cd_orgao_assinatura = @cd_orgao_assinatura,
			ds_cargo_assinatura = @ds_cargo_assinatura,
			nm_assinatura = @nm_assinatura,
			cd_contra_assinatura = @cd_contra_assinatura,
			cd_grupo_contra_assinatura = @cd_grupo_contra_assinatura,
			cd_orgao_contra_assinatura = @cd_orgao_contra_assinatura,
			nm_contra_assinatura = @nm_contra_assinatura,
			ds_cargo_contra_assinatura = @ds_cargo_contra_assinatura,
			nr_documento = @nr_documento,
			vr_documento = @vr_documento,
			cd_conta = @cd_conta,
			nr_banco = @nr_banco,
			nr_agencia = @nr_agencia,
			nr_conta = @nr_conta,
			cd_despesa = @cd_despesa,
			dt_vencimento = @dt_vencimento,
			ds_referencia = @ds_referencia,
			ds_despesa_credor = @ds_despesa_credor,
			nr_contrato = @nr_contrato,
			ds_credor1 = @ds_credor1,
			ds_credor2 = @ds_credor2,
			ds_endereco = @ds_endereco,
			nr_cep = @nr_cep,
			nr_banco_credor = @nr_banco_credor,
			nr_agencia_credor = @nr_agencia_credor,
			nr_conta_credor = @nr_conta_credor,
			nr_banco_pgto = @nr_banco_pgto,
			nr_agencia_pgto = @nr_agencia_pgto,
			nr_conta_pgto = @nr_conta_pgto,
			dt_emissao = @dt_emissao,
			qt_op_preparada = @qt_op_preparada,
			vr_total = @vr_total,
			bl_transmitir_prodesp = @bl_transmitir_prodesp,
			bl_transmitido_prodesp = @bl_transmitido_prodesp,
			ds_status_prodesp = @ds_status_prodesp,
			ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp,
			dt_transmitido_prodesp = @dt_transmitido_prodesp,
			ds_status_documento = @ds_status_documento,
			bl_cadastro_completo = @bl_cadastro_completo

		where	id_preparacao_pagamento = @id_preparacao_pagamento;

		select @id_preparacao_pagamento;

	end
	else
	begin

		insert into [contaunica].[tb_preparacao_pagamento] (
			[id_regional]
           ,[id_tipo_documento]
           ,[id_tipo_preparacao_pagamento]
           ,[nr_op_inicial]
           ,[nr_op_final]
           ,[nr_ano_exercicio]
           ,[cd_credor_organizacao]
           ,[nr_cpf_cnpj_credor]
           ,[cd_assinatura]
           ,[cd_grupo_assinatura]
           ,[cd_orgao_assinatura]
           ,[ds_cargo_assinatura]
           ,[nm_assinatura]
           ,[cd_contra_assinatura]
           ,[cd_grupo_contra_assinatura]
           ,[cd_orgao_contra_assinatura]
           ,[nm_contra_assinatura]
           ,[ds_cargo_contra_assinatura]
           ,[nr_documento]
           ,[vr_documento]
           ,[cd_conta]
           ,[nr_banco]
           ,[nr_agencia]
           ,[nr_conta]
           ,[cd_despesa]
           ,[dt_vencimento]
           ,[ds_referencia]
           ,[ds_despesa_credor]
           ,[nr_contrato]
           ,[ds_credor1]
           ,[ds_credor2]
           ,[ds_endereco]
           ,[nr_cep]
           ,[nr_banco_credor]
           ,[nr_agencia_credor]
           ,[nr_conta_credor]
           ,[nr_banco_pgto]
           ,[nr_agencia_pgto]
           ,[nr_conta_pgto]
           ,[dt_emissao]
           ,[qt_op_preparada]
           ,[vr_total]
           ,[bl_transmitir_prodesp]
           ,[bl_transmitido_prodesp]
           ,[ds_status_prodesp]
           ,[ds_transmissao_mensagem_prodesp]
           ,[dt_transmitido_prodesp]
           ,[ds_status_documento]
           ,[bl_cadastro_completo]
           ,[dt_cadastro]					
		)
		values
		(
		nullif(@id_regional,0)
		,nullif(@id_tipo_documento,0)
		,nullif(@id_tipo_preparacao_pagamento,0)
		,@nr_op_inicial
		,@nr_op_final
		,@nr_ano_exercicio
		,@cd_credor_organizacao
		,@nr_cpf_cnpj_credor
		,@cd_assinatura
		,@cd_grupo_assinatura
		,@cd_orgao_assinatura
		,@ds_cargo_assinatura
		,@nm_assinatura
		,@cd_contra_assinatura
		,@cd_grupo_contra_assinatura
		,@cd_orgao_contra_assinatura
		,@nm_contra_assinatura
		,@ds_cargo_contra_assinatura
		,@nr_documento
		,@vr_documento
		,@cd_conta
		,@nr_banco
		,@nr_agencia
		,@nr_conta
		,@cd_despesa
		,@dt_vencimento
		,@ds_referencia
		,@ds_despesa_credor
		,@nr_contrato
		,@ds_credor1
		,@ds_credor2
		,@ds_endereco
		,@nr_cep
		,@nr_banco_credor
		,@nr_agencia_credor
		,@nr_conta_credor
		,@nr_banco_pgto
		,@nr_agencia_pgto
		,@nr_conta_pgto
		,@dt_emissao
		,@qt_op_preparada
		,@vr_total
		,@bl_transmitir_prodesp
		,@bl_transmitido_prodesp
		,'N'
		,@ds_transmissao_mensagem_prodesp
		,@dt_transmitido_prodesp
		,@ds_status_documento
		,@bl_cadastro_completo
		,getdate()

		);

		select scope_identity();

	end

end