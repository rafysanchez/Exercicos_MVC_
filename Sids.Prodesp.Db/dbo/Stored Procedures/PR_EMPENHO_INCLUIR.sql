-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para incluir empenhos 
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_INCLUIR]
	@tb_regional_id_regional smallint = NULL,
	@tb_empenho_tipo_id_empenho_tipo int = NULL,
	@tb_programa_id_programa int = NULL,
	@tb_estrutura_id_estrutura int = NULL,
	@tb_fonte_id_fonte int = NULL,
	@tb_licitacao_id_licitacao varchar(2) = NULL,
	@tb_modalidade_id_modalidade int = NULL,
	@tb_aquisicao_tipo_id_aquisicao_tipo int = NULL,
	@tb_origem_material_id_origem_material int = NULL,
	@tb_destino_cd_destino char(2) = NULL,
	@cd_tipo_obra int = NULL,
	@cd_reserva varchar(11) = NULL,
	@nr_ano_exercicio int = NULL,
	@nr_processo varchar(58) =  NULL,
	@nr_processo_ne varchar(15) =  NULL,
	@nr_processo_siafisico varchar(11) =  NULL,
	@nr_contrato varchar(13) = NULL,
	@nr_ct varchar(11) =  NULL,
	@nr_empenhoProdesp varchar(11) =  NULL,
	@nr_empenhoSiafem varchar(11) =  NULL,
	@nr_empenhoSiafisico varchar(11) =  NULL,
	@cd_aplicacao_obra varchar(8) = NULL,
	@nr_natureza_item varchar(2) = NULL,
	@dt_cadastramento date = NULL,
	@nr_cnpj_cpf_ug_credor varchar(15) = NULL,
	@cd_gestao_credor varchar(6) = NULL,
	@cd_credor_organizacao int = NULL,
	@nr_cnpj_cpf_fornecedor varchar(15) = NULL,
	@cd_ugo_obra int = NULL,
	@nr_ano_contrato int = NULL,
	@nr_mes_contrato int = NULL,
	@nr_obra varchar(9) = NULL,
	@dt_entrega_material date = NULL,
	@ds_logradouro_entrega varchar(45) =  NULL,
	@ds_bairro_entrega varchar(45) =  NULL,
	@ds_cidade_entrega varchar(45) =  NULL,
	@cd_cep_entrega varchar(8) = NULL,
	@ds_informacoes_adicionais_entrega varchar(213) =  NULL,
	@cd_unidade_gestora varchar(6) = NULL,
	@cd_gestao varchar(5) = NULL,
	@cd_evento int = NULL,
	@dt_emissao date = NULL,
	@nr_oc varchar(11) = NULL,
	@cd_unidade_gestora_fornecedora varchar(6) = NULL,
	@cd_gestao_fornecedora varchar(5) = NULL,
	@cd_uo int = NULL,
	@cd_ugo int = NULL,
	@cd_municipio varchar(4) = NULL,
	@ds_acordo varchar(7) =  NULL,
	@nr_contrato_fornecedor varchar(22) = NULL,
	@nr_edital varchar(20) = NULL,
	@ds_referencia_legal varchar(20) =  NULL,
	@ds_local_entrega_siafem varchar(45) =  NULL,
	@cd_unidade_fornecimento varchar(5) =  NULL,
	@ds_autorizado_supra_folha varchar(4) =  NULL,
	@cd_especificacao_despesa varchar(3) = NULL,
	@ds_especificacao_despesa varchar(711) =  NULL,
	@cd_autorizado_assinatura varchar(5) = NULL,
	@cd_autorizado_grupo int = NULL,
	@cd_autorizado_orgao char(2) = NULL,
	@nm_autorizado_assinatura varchar(55) =  NULL,
	@ds_autorizado_cargo varchar(55) =  NULL,
	@cd_examinado_assinatura varchar(5) = NULL,
	@cd_examinado_grupo int = NULL,
	@cd_examinado_orgao char(2) = NULL,
	@nm_examinado_assinatura varchar(55) =  NULL,
	@ds_examinado_cargo varchar(55) =  NULL,
	@cd_responsavel_assinatura varchar(5) = NULL,
	@cd_responsavel_grupo int = NULL,
	@cd_responsavel_orgao char(2) = NULL,
	@nm_responsavel_assinatura varchar(55) =  NULL,
	@ds_responsavel_cargo varchar(140) =  NULL,
	@bl_transmitir_prodesp bit = NULL,
	@fg_transmitido_prodesp bit = NULL,
	@dt_transmitido_prodesp date = NULL,
	@bl_transmitir_siafem bit = NULL,
	@fg_transmitido_siafem bit = NULL,
	@dt_transmitido_siafem date = NULL,
	@bl_transmitir_siafisico bit = NULL,
	@fg_transmitido_siafisico bit = NULL,
	@dt_transmitido_siafisico date = NULL,
	@ds_status_prodesp varchar(1) =  NULL,
	@ds_status_siafem varchar(1) =  NULL,
	@ds_status_siafisico varchar(1) =  NULL,
	@ds_status_documento bit = NULL,
	@bl_cadastro_completo bit = NULL,
	@ds_msgRetornoTransmissaoProdesp varchar(140) =  NULL,
	@ds_msgRetornoTransmissaoSiafem varchar(140) =  NULL,
	@ds_msgRetornoTransmissaoSiafisico varchar(140) =  NULL,
	@ds_status_siafisico_ct char(1) = NULL,
	@ds_status_siafisico_ne char(1) = NULL,
	@bl_contBec bit = NULL
as
begin

	SET NOCOUNT ON;
	
	begin transaction

		INSERT INTO [empenho].[tb_empenho] (
			tb_regional_id_regional
		,	tb_empenho_tipo_id_empenho_tipo
		,	tb_programa_id_programa
		,	tb_estrutura_id_estrutura
		,	tb_fonte_id_fonte
		,	tb_licitacao_id_licitacao
		,	tb_modalidade_id_modalidade
		,	tb_aquisicao_tipo_id_aquisicao_tipo
		,	tb_origem_material_id_origem_material
		,	tb_destino_cd_destino
		,	cd_tipo_obra
		,	cd_reserva
		,	nr_ano_exercicio
		,	nr_processo
		,	nr_processo_ne
		,	nr_processo_siafisico
		,	nr_contrato
		,	nr_ct
		,	nr_empenhoProdesp
		,	nr_empenhoSiafem
		,	nr_empenhoSiafisico
		,	cd_aplicacao_obra
		,	nr_natureza_item
		,	dt_cadastramento
		,	nr_cnpj_cpf_ug_credor
		,	cd_gestao_credor
		,	cd_credor_organizacao
		,	nr_cnpj_cpf_fornecedor
		,	cd_ugo_obra
		,	nr_ano_contrato
		,	nr_mes_contrato
		,	nr_obra
		,	dt_entrega_material
		,	ds_logradouro_entrega
		,	ds_bairro_entrega
		,	ds_cidade_entrega
		,	cd_cep_entrega
		,	ds_informacoes_adicionais_entrega
		,	cd_unidade_gestora
		,	cd_gestao
		,	cd_evento
		,	dt_emissao
		,	nr_oc
		,	cd_unidade_gestora_fornecedora
		,	cd_gestao_fornecedora
		,	cd_uo
		,	cd_ugo
		,	cd_municipio
		,	ds_acordo
		,	nr_contrato_fornecedor
		,	nr_edital
		,	ds_referencia_legal
		,	ds_local_entrega_siafem
		,	cd_unidade_fornecimento
		,	ds_autorizado_supra_folha
		,	cd_especificacao_despesa
		,	ds_especificacao_despesa
		,	cd_autorizado_assinatura
		,	cd_autorizado_grupo
		,	cd_autorizado_orgao
		,	nm_autorizado_assinatura
		,	ds_autorizado_cargo
		,	cd_examinado_assinatura
		,	cd_examinado_grupo
		,	cd_examinado_orgao
		,	nm_examinado_assinatura
		,	ds_examinado_cargo
		,	cd_responsavel_assinatura
		,	cd_responsavel_grupo
		,	cd_responsavel_orgao
		,	nm_responsavel_assinatura
		,	ds_responsavel_cargo
		,	bl_transmitir_prodesp
		,	fg_transmitido_prodesp
		,	dt_transmitido_prodesp
		,	bl_transmitir_siafem
		,	fg_transmitido_siafem
		,	dt_transmitido_siafem
		,	bl_transmitir_siafisico
		,	fg_transmitido_siafisico
		,	dt_transmitido_siafisico
		,	ds_status_prodesp
		,	ds_status_siafem
		,	ds_status_siafisico
		,	ds_status_documento
		,	bl_cadastro_completo
		,	ds_msgRetornoTransmissaoProdesp
		,	ds_msgRetornoTransmissaoSiafem
		,	ds_msgRetornoTransmissaoSiafisico
		,	ds_status_siafisico_ct
		,	ds_status_siafisico_ne
		,   bl_contBec
		)
		VALUES 
		(
			nullif( @tb_regional_id_regional, 0 )
		,	nullif( @tb_empenho_tipo_id_empenho_tipo, 0 )
		,	nullif( @tb_programa_id_programa, 0 )
		,	nullif( @tb_estrutura_id_estrutura, 0 )
		,	nullif( @tb_fonte_id_fonte, 0 )
		,	nullif( @tb_licitacao_id_licitacao, '' )
		,	nullif( @tb_modalidade_id_modalidade, 0 )
		,	nullif( @tb_aquisicao_tipo_id_aquisicao_tipo, 0 )
		,	nullif( @tb_origem_material_id_origem_material, 0 )
		,	nullif( @tb_destino_cd_destino, '' )
		,	@cd_tipo_obra
		,	@cd_reserva
		,	@nr_ano_exercicio
		,	@nr_processo
		,	@nr_processo_ne
		,	@nr_processo_siafisico
		,	@nr_contrato
		,	@nr_ct
		,	@nr_empenhoProdesp
		,	@nr_empenhoSiafem
		,	@nr_empenhoSiafisico
		,	@cd_aplicacao_obra
		,	@nr_natureza_item
		,	getdate()
		,	@nr_cnpj_cpf_ug_credor
		,	@cd_gestao_credor
		,	@cd_credor_organizacao
		,	@nr_cnpj_cpf_fornecedor
		,	@cd_ugo_obra
		,	@nr_ano_contrato
		,	@nr_mes_contrato
		,	@nr_obra
		,	@dt_entrega_material
		,	@ds_logradouro_entrega
		,	@ds_bairro_entrega
		,	@ds_cidade_entrega
		,	@cd_cep_entrega
		,	@ds_informacoes_adicionais_entrega
		,	@cd_unidade_gestora
		,	@cd_gestao
		,	@cd_evento
		,	@dt_emissao
		,	@nr_oc
		,	@cd_unidade_gestora_fornecedora
		,	@cd_gestao_fornecedora
		,	@cd_uo
		,	@cd_ugo
		,	@cd_municipio
		,	@ds_acordo
		,	@nr_contrato_fornecedor
		,	@nr_edital
		,	@ds_referencia_legal
		,	@ds_local_entrega_siafem
		,	@cd_unidade_fornecimento
		,	@ds_autorizado_supra_folha
		,	@cd_especificacao_despesa
		,	@ds_especificacao_despesa
		,	@cd_autorizado_assinatura
		,	@cd_autorizado_grupo
		,	@cd_autorizado_orgao
		,	@nm_autorizado_assinatura
		,	@ds_autorizado_cargo
		,	@cd_examinado_assinatura
		,	@cd_examinado_grupo
		,	@cd_examinado_orgao
		,	@nm_examinado_assinatura
		,	@ds_examinado_cargo
		,	@cd_responsavel_assinatura
		,	@cd_responsavel_grupo
		,	@cd_responsavel_orgao
		,	@nm_responsavel_assinatura
		,	@ds_responsavel_cargo
		,	@bl_transmitir_prodesp
		,	@fg_transmitido_prodesp
		,	@dt_transmitido_prodesp
		,	@bl_transmitir_siafem
		,	@fg_transmitido_siafem
		,	@dt_transmitido_siafem
		,	@bl_transmitir_siafisico
		,	@fg_transmitido_siafisico
		,	@dt_transmitido_siafisico
		,	@ds_status_prodesp
		,	@ds_status_siafem
		,	@ds_status_siafisico
		,	@ds_status_documento
		,	@bl_cadastro_completo
		,	@ds_msgRetornoTransmissaoProdesp
		,	@ds_msgRetornoTransmissaoSiafem
		,	@ds_msgRetornoTransmissaoSiafisico
		,	@ds_status_siafisico_ct
		,	@ds_status_siafisico_ne
		,   @bl_contBec
		);
	
	Commit	
    
    SELECT @@IDENTITY
End