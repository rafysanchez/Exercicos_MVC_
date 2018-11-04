-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para listar empenhos
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_CONSULTAR_GRID]  
	@tb_regional_id_regional smallint = NULL,
	@tb_programa_id_programa int = NULL,
	@tb_estrutura_id_estrutura int = NULL,
	@tb_fonte_id_fonte int = NULL,
	@tb_licitacao_id_licitacao varchar(2) = NULL,
	@nr_empenhoProdesp varchar(11) =  NULL,
	@nr_empenhoSiafem varchar(11) =  NULL,
	@nr_empenhoSiafisico varchar(11) =  NULL,
	@nr_processo varchar(58) =  NULL,
	@nr_processo_ne varchar(15) =  NULL,
	@nr_processo_siafisico varchar(11) =  NULL,
	@cd_aplicacao_obra varchar(8) = NULL,
	@nr_ano_exercicio int = NULL,
	@nr_natureza_item varchar(2) = NULL,
	@ds_status_prodesp varchar(1) =  NULL,
	@ds_status_siafem varchar(1) =  NULL,
	@ds_status_siafisico varchar(1) =  NULL,
	@nr_contrato varchar(13) = NULL,
	@dt_cadastramento_de date = NULL,
	@dt_cadastramento_ate date = NULL,
	@nr_cnpj_cpf_ug_credor varchar(15) = NULL,
	@cd_gestao_credor varchar(6) = NULL,
	@cd_credor_organizacao int = NULL,
	@nr_cnpj_cpf_fornecedor varchar(15) = NULL
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  id_empenho
		, tb_regional_id_regional
		, tb_empenho_tipo_id_empenho_tipo
		, tb_programa_id_programa
		, tb_estrutura_id_estrutura
		, tb_fonte_id_fonte
		, tb_licitacao_id_licitacao
		, tb_modalidade_id_modalidade
		, tb_aquisicao_tipo_id_aquisicao_tipo
		, tb_origem_material_id_origem_material
		, tb_destino_cd_destino
		, cd_tipo_obra
		, cd_reserva
		, nr_ano_exercicio
		, nr_processo
		, nr_processo_ne
		, nr_processo_siafisico
		, nr_contrato
		, nr_ct
		, nr_empenhoProdesp
		, nr_empenhoSiafem
		, nr_empenhoSiafisico
		, cd_aplicacao_obra
		, nr_natureza_item
		, dt_cadastramento
		, nr_cnpj_cpf_ug_credor
		, cd_gestao_credor
		, cd_credor_organizacao
		, nr_cnpj_cpf_fornecedor
		, cd_ugo_obra
		, nr_ano_contrato
		, nr_mes_contrato
		, nr_obra
		, dt_entrega_material
		, ds_logradouro_entrega
		, ds_bairro_entrega
		, ds_cidade_entrega
		, cd_cep_entrega
		, ds_informacoes_adicionais_entrega
		, cd_unidade_gestora
		, cd_gestao
		, cd_evento
		, dt_emissao
		, nr_oc
		, cd_unidade_gestora_fornecedora
		, cd_gestao_fornecedora
		, cd_uo
		, cd_ugo
		, cd_municipio
		, ds_acordo
		, nr_contrato_fornecedor
		, nr_edital
		, ds_referencia_legal
		, ds_local_entrega_siafem
		, cd_unidade_fornecimento
		, ds_autorizado_supra_folha
		, cd_especificacao_despesa
		, ds_especificacao_despesa
		, cd_autorizado_assinatura
		, cd_autorizado_grupo
		, cd_autorizado_orgao
		, nm_autorizado_assinatura
		, ds_autorizado_cargo
		, cd_examinado_assinatura
		, cd_examinado_grupo
		, cd_examinado_orgao
		, nm_examinado_assinatura
		, ds_examinado_cargo
		, cd_responsavel_assinatura
		, cd_responsavel_grupo
		, cd_responsavel_orgao
		, nm_responsavel_assinatura
		, ds_responsavel_cargo
		, bl_transmitir_prodesp
		, fg_transmitido_prodesp
		, dt_transmitido_prodesp
		, bl_transmitir_siafem
		, fg_transmitido_siafem
		, dt_transmitido_siafem
		, bl_transmitir_siafisico
		, fg_transmitido_siafisico
		, dt_transmitido_siafisico
		, ds_status_prodesp
		, ds_status_siafem
		, ds_status_siafisico
		, ds_status_documento
		, bl_cadastro_completo
		, ds_msgRetornoTransmissaoProdesp
		, ds_msgRetornoTransmissaoSiafem
		, ds_msgRetornoTransmissaoSiafisico
	FROM empenho.tb_empenho (nolock)
	where
		( nullif( @tb_fonte_id_fonte, 0 ) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte ) and
		( nullif( @tb_regional_id_regional, 0 ) is null or tb_regional_id_regional = @tb_regional_id_regional ) and
		( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa ) and
		( nullif( @tb_estrutura_id_estrutura, 0 ) is null or tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura ) and
		( @tb_licitacao_id_licitacao is null or tb_licitacao_id_licitacao = @tb_licitacao_id_licitacao ) and
		( @nr_empenhoProdesp is null or nr_empenhoProdesp = @nr_empenhoProdesp ) and
		( @nr_empenhoSiafem is null or nr_empenhoSiafem = @nr_empenhoSiafem ) and
		( @nr_empenhoSiafisico is null or nr_empenhoSiafisico = @nr_empenhoSiafisico ) and
		( @nr_processo is null or nr_processo = @nr_processo ) and
		( @nr_processo_ne is null or nr_processo_ne = @nr_processo_ne ) and
		( @nr_processo_siafisico is null or nr_processo_siafisico = @nr_processo_siafisico ) and
		( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra ) and
		( nullif( @nr_ano_exercicio, 0 ) is null or nr_ano_exercicio = @nr_ano_exercicio ) and
		( @nr_natureza_item is null or nr_natureza_item = @nr_natureza_item ) and
		( @ds_status_prodesp is null or ds_status_prodesp = @ds_status_prodesp ) and
		
		
		(( @ds_status_siafem is null or ds_status_siafem = @ds_status_siafem ) 
		or	( @ds_status_siafisico is null or ds_status_siafisico = @ds_status_siafisico )) and
		
		( @nr_contrato is null or nr_contrato = @nr_contrato ) and
	
		( dt_cadastramento   >= @dt_cadastramento_de or @dt_cadastramento_de is null ) and 
		( dt_cadastramento   <= @dt_cadastramento_ate or @dt_cadastramento_ate is null ) and

		( @nr_cnpj_cpf_ug_credor is null or nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor ) and
		( @cd_gestao_credor is null or cd_gestao_credor = @cd_gestao_credor ) and
		( nullif( @cd_credor_organizacao, 0 ) is null or cd_credor_organizacao = @cd_credor_organizacao ) and
		( @nr_cnpj_cpf_fornecedor is null or nr_cnpj_cpf_fornecedor = @nr_cnpj_cpf_fornecedor )   
end;