
CREATE PROCEDURE [dbo].[sp_confirmacao_pagamento_incluir] 
		   @id_confirmacao_pagamento int,
           @id_execucao_pd int = null,
           @id_programacao_desembolso_execucao_item int  = null,
           @id_autorizacao_ob int  = null,
           @id_autorizacao_ob_item int = null,
           @dt_confirmacao datetime  = null,           
           @id_tipo_documento int  = null,
		   @nr_documento varchar(30)  = null,
           @id_regional smallint  = null,
           @id_reclassificacao_retencao int  = null,
           @id_origem int  = null,
           @id_despesa_tipo int  = null,
           @dt_vencimento datetime  = null,
           @nr_contrato varchar(13)  = null,
           @cd_obra varchar(20)  = null,
           @nr_op varchar(50) = null,
           @nr_banco_pagador varchar(10) = null,
           @nr_agencia_pagador varchar(10) = null,
           @nr_conta_pagador varchar(10) = null,
		   @nr_banco_favorecido varchar(10) = null,
           @nr_agencia_favorecido varchar(10) = null,
           @nr_conta_favorecido varchar(10) = null,
           @nr_fonte_siafem varchar(50) = null,
           @nr_emprenho varchar(50) = null,
           @nr_processo varchar(20) = null,
           @nr_nota_fiscal int = null,
           @nr_nl_documento varchar(20) = null,
           @vr_documento decimal(8,2) = null,
           @nr_natureza_despesa int = null,
           @cd_credor_organizacao int = null,
           @nr_cnpj_cpf_ug_credor varchar(14) = null,
           @ds_referencia nvarchar(100) = null,
           @cd_orgao_assinatura varchar(2) = null,
           @nm_reduzido_credor varchar(14) = null,
           @fl_transmissao_transmitido_prodesp bit  = null,
           @cd_transmissao_status_prodesp varchar(50)  = null,
           @dt_transmissao_transmitido_prodesp datetime  = null,
           @ds_transmissao_mensagem_prodesp varchar(200)  = null,
		   @nr_empenhoSiafem varchar(11) = null,
		   @cd_credor_organizacao_docto int = null,
		   @nr_cnpj_cpf_ug_credor_docto varchar(14) = null,
		   @vr_desdobramento decimal(8,2) = null,
		   @dt_realizacao datetime = null,
		   @nm_reduzido_credor_docto varchar(14) = null,
		   @nr_documento_gerador varchar(22) = null

As
begin 
Begin TRANSACTION 
 SET NOCOUNT ON;
		insert into pagamento.tb_confirmacao_pagamento_item
           (
		    id_confirmacao_pagamento
           ,id_execucao_pd
           ,id_programacao_desembolso_execucao_item
           ,id_autorizacao_ob
           ,id_autorizacao_ob_item
           ,dt_confirmacao
           ,id_tipo_documento
           ,nr_documento
           ,id_regional
           ,id_reclassificacao_retencao
           ,id_origem
           ,id_despesa_tipo
           ,dt_vencimento
           ,nr_contrato
           ,cd_obra
           ,nr_op
           ,nr_banco_pagador
           ,nr_agencia_pagador
           ,nr_conta_pagador
           ,nr_banco_favorecido
           ,nr_agencia_favorecido
           ,nr_conta_favorecido
           ,nr_fonte_siafem
           ,nr_emprenho
           ,nr_processo
           ,nr_nota_fiscal
           ,nr_nl_documento
           ,vr_documento
           ,nr_natureza_despesa
           ,cd_credor_organizacao
           ,nr_cnpj_cpf_ug_credor
           ,ds_referencia
           ,cd_orgao_assinatura
           ,nm_reduzido_credor
           ,fl_transmissao_transmitido_prodesp
           ,cd_transmissao_status_prodesp
           ,dt_transmissao_transmitido_prodesp
           ,ds_transmissao_mensagem_prodesp
	       ,nr_empenhoSiafem
		   ,cd_credor_organizacao_docto
           ,nr_cnpj_cpf_ug_credor_docto
		   ,vr_desdobramento
		   ,dt_realizacao
		   ,nm_reduzido_credor_docto
		   ,nr_documento_gerador
		    )
     VALUES
           (
			@id_confirmacao_pagamento
			,@id_execucao_pd
			,@id_programacao_desembolso_execucao_item
			,@id_autorizacao_ob
			,@id_autorizacao_ob_item
			,@dt_confirmacao
			,@id_tipo_documento
			,@nr_documento
			,@id_regional
			,@id_reclassificacao_retencao
			,@id_origem
			,@id_despesa_tipo
			,@dt_vencimento
			,@nr_contrato
			,@cd_obra
			,@nr_op
			,@nr_banco_pagador
			,@nr_agencia_pagador
			,@nr_conta_pagador
			,@nr_banco_favorecido
			,@nr_agencia_favorecido
			,@nr_conta_favorecido
			,@nr_fonte_siafem
			,@nr_emprenho
			,@nr_processo
			,@nr_nota_fiscal
			,@nr_nl_documento
			,@vr_documento
			,@nr_natureza_despesa
			,@cd_credor_organizacao
			,@nr_cnpj_cpf_ug_credor
			,@ds_referencia
			,@cd_orgao_assinatura
			,@nm_reduzido_credor
			,@fl_transmissao_transmitido_prodesp
			,@cd_transmissao_status_prodesp
			,@dt_transmissao_transmitido_prodesp
			,@ds_transmissao_mensagem_prodesp
			,@nr_empenhoSiafem
			,@cd_credor_organizacao_docto
			,@nr_cnpj_cpf_ug_credor_docto
			,@vr_desdobramento
			,@dt_realizacao
			,@nm_reduzido_credor_docto
			,@nr_documento_gerador)
end
commit