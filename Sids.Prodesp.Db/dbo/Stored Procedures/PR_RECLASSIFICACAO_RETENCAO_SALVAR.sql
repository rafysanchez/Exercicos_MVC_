-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para salvar ou alterar reclassificacao retencao
-- ===================================================================  
CREATE procedure [dbo].[PR_RECLASSIFICACAO_RETENCAO_SALVAR] 
	@id_reclassificacao_retencao int= NULL
    ,@id_resto_pagar char(1)= NULL
    ,@id_tipo_reclassificacao_retencao int= NULL
    ,@id_tipo_documento int= NULL
    ,@dt_cadastro date= NULL
    ,@nr_siafem_siafisico varchar(11)= NULL
    ,@nr_contrato varchar(13)= NULL
	,@nr_processo varchar(15) = NULL
    ,@nr_empenho_siafem_siafisico varchar(11)= NULL
    ,@nr_documento varchar(19)= NULL
    ,@cd_unidade_gestora varchar(6)= NULL
    ,@cd_gestao varchar(5)= NULL
    ,@nr_medicao varchar(3)= NULL
    ,@vl_valor decimal(18)= NULL
    ,@cd_evento varchar(6)= NULL
    ,@ds_inscricao varchar(22)= NULL
    ,@cd_classificacao varchar(9)= NULL
    ,@cd_fonte varchar(10)= NULL
    ,@dt_emissao date= NULL
    ,@nr_cnpj_cpf_credor varchar(15)= NULL
    ,@cd_gestao_credor varchar(140)= NULL
    ,@nr_ano_medicao char(4)= NULL
    ,@nr_mes_medicao char(2)= NULL
    ,@id_regional smallint= NULL
    ,@cd_aplicacao_obra varchar(140)= NULL
    ,@tb_obra_tipo_id_obra_tipo int= NULL
    ,@cd_credor_organizacao int= NULL
    ,@nr_cnpj_cpf_fornecedor varchar(15)= NULL
    ,@ds_normal_estorno char(1)= NULL
    ,@nr_nota_lancamento_medicao varchar(11)= NULL
    ,@nr_cnpj_prefeitura varchar(15)= NULL
    ,@ds_observacao_1 varchar(76)= NULL
    ,@ds_observacao_2 varchar(76)= NULL
    ,@ds_observacao_3 varchar(76)= NULL
    ,@fl_sistema_siafem_siafisico bit= NULL
    ,@cd_transmissao_status_siafem_siafisico char(1)= NULL
    ,@fl_transmissao_transmitido_siafem_siafisico bit= NULL
    ,@dt_transmissao_transmitido_siafem_siafisico date= NULL
    ,@ds_transmissao_mensagem_siafem_siafisico varchar(140)= NULL
    ,@bl_cadastro_completo bit= NULL
	,@nr_ano_exercicio int = null
	,@cd_origem int = null
	,@cd_agrupamento_confirmacao int = null
	,@id_confirmacao_pagamento int= NULL
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_reclassificacao_retencao](nolock)
		where	id_reclassificacao_retencao = @id_reclassificacao_retencao
	)
	begin

		update [contaunica].[tb_reclassificacao_retencao] set 
			id_resto_pagar	=	@id_resto_pagar
			,id_tipo_reclassificacao_retencao	=	nullif(@id_tipo_reclassificacao_retencao,0)
			,id_tipo_documento	=	nullif(@id_tipo_documento,0)
			,id_regional	=	nullif(@id_regional,0)
			,nr_siafem_siafisico	=	@nr_siafem_siafisico
			,nr_contrato	=	@nr_contrato
			,nr_processo = @nr_processo
			,nr_empenho_siafem_siafisico	=	@nr_empenho_siafem_siafisico
			,nr_documento	=	@nr_documento
			,cd_unidade_gestora	=	@cd_unidade_gestora
			,cd_gestao	=	@cd_gestao
			,nr_medicao	=	@nr_medicao
			,vl_valor	=	@vl_valor
			,cd_evento	=	@cd_evento
			,ds_inscricao	=	@ds_inscricao
			,cd_classificacao	=	@cd_classificacao
			,cd_fonte	=	@cd_fonte
			,dt_emissao	=	@dt_emissao
			,nr_cnpj_cpf_credor	=	@nr_cnpj_cpf_credor
			,cd_gestao_credor	=	@cd_gestao_credor
			,nr_ano_medicao	=	@nr_ano_medicao
			,nr_mes_medicao	=	@nr_mes_medicao
			,cd_aplicacao_obra	=	@cd_aplicacao_obra
			,tb_obra_tipo_id_obra_tipo	=	@tb_obra_tipo_id_obra_tipo
			,cd_credor_organizacao	=	@cd_credor_organizacao
			,nr_cnpj_cpf_fornecedor	=	@nr_cnpj_cpf_fornecedor
			,ds_normal_estorno	=	@ds_normal_estorno
			,nr_nota_lancamento_medicao	=	@nr_nota_lancamento_medicao
			,nr_cnpj_prefeitura	=	@nr_cnpj_prefeitura
			,ds_observacao_1	=	@ds_observacao_1
			,ds_observacao_2	=	@ds_observacao_2
			,ds_observacao_3	=	@ds_observacao_3
			,fl_sistema_siafem_siafisico	=	@fl_sistema_siafem_siafisico
			,cd_transmissao_status_siafem_siafisico	=	@cd_transmissao_status_siafem_siafisico
			,fl_transmissao_transmitido_siafem_siafisico	=	@fl_transmissao_transmitido_siafem_siafisico
			,dt_transmissao_transmitido_siafem_siafisico	=	@dt_transmissao_transmitido_siafem_siafisico
			,ds_transmissao_mensagem_siafem_siafisico	=	@ds_transmissao_mensagem_siafem_siafisico
			,bl_cadastro_completo	=	@bl_cadastro_completo
			,nr_ano_exercicio = @nr_ano_exercicio
			,cd_origem = @cd_origem
			,cd_agrupamento_confirmacao = @cd_agrupamento_confirmacao
			,id_confirmacao_pagamento = @id_confirmacao_pagamento

		where	id_reclassificacao_retencao = @id_reclassificacao_retencao;

		select @id_reclassificacao_retencao;

	end
	else
	begin

		insert into [contaunica].[tb_reclassificacao_retencao] (
			[id_resto_pagar]
           ,[id_tipo_reclassificacao_retencao]
           ,[id_tipo_documento]
           ,[id_regional]
		   ,[dt_cadastro]
           ,[nr_siafem_siafisico]
           ,[nr_contrato]
		   ,nr_processo
           ,[nr_empenho_siafem_siafisico]
           ,[nr_documento]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[nr_medicao]
           ,[vl_valor]
           ,[cd_evento]
           ,[ds_inscricao]
           ,[cd_classificacao]
           ,[cd_fonte]
           ,[dt_emissao]
           ,[nr_cnpj_cpf_credor]
           ,[cd_gestao_credor]
           ,[nr_ano_medicao]
           ,[nr_mes_medicao]
           ,[cd_aplicacao_obra]
           ,[tb_obra_tipo_id_obra_tipo]
           ,[cd_credor_organizacao]
           ,[nr_cnpj_cpf_fornecedor]
           ,[ds_normal_estorno]
           ,[nr_nota_lancamento_medicao]
           ,[nr_cnpj_prefeitura]
           ,[ds_observacao_1]
           ,[ds_observacao_2]
           ,[ds_observacao_3]
           ,[fl_sistema_siafem_siafisico]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[bl_cadastro_completo]	
		   ,[nr_ano_exercicio]					
		   ,[cd_origem]
		   ,[cd_agrupamento_confirmacao]
		   ,[id_confirmacao_pagamento]
		)
		values
		(
			nullif(@id_resto_pagar,'')
			,nullif(@id_tipo_reclassificacao_retencao,0)
			,nullif(@id_tipo_documento,0)
			,nullif(@id_regional,0)
			,getdate()
			,@nr_siafem_siafisico
			,@nr_contrato
			,@nr_processo
			,@nr_empenho_siafem_siafisico
			,@nr_documento
			,@cd_unidade_gestora
			,@cd_gestao
			,@nr_medicao
			,@vl_valor
			,@cd_evento
			,@ds_inscricao
			,@cd_classificacao
			,@cd_fonte
			,@dt_emissao
			,@nr_cnpj_cpf_credor
			,@cd_gestao_credor
			,@nr_ano_medicao
			,@nr_mes_medicao
			,@cd_aplicacao_obra
			,@tb_obra_tipo_id_obra_tipo
			,@cd_credor_organizacao
			,@nr_cnpj_cpf_fornecedor
			,@ds_normal_estorno
			,@nr_nota_lancamento_medicao
			,@nr_cnpj_prefeitura
			,@ds_observacao_1
			,@ds_observacao_2
			,@ds_observacao_3
			,@fl_sistema_siafem_siafisico
			,'N'
			,@fl_transmissao_transmitido_siafem_siafisico
			,@dt_transmissao_transmitido_siafem_siafisico
			,@ds_transmissao_mensagem_siafem_siafisico
			,@bl_cadastro_completo
			,@nr_ano_exercicio
			,@cd_origem
			,@cd_agrupamento_confirmacao
			,@id_confirmacao_pagamento
		);

		select scope_identity();

	end

end