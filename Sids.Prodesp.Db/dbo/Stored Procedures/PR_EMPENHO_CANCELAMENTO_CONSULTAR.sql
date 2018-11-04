-- ===================================================================  
-- Author: Carlos Henrique Magalhães
-- Create date: 06/01/2016
-- Description: Procedure para consultar Reforços de empenhos
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_CONSULTAR]
	@id_empenho_cancelamento int = 0,
	@tb_regional_id_regional int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
	   id_empenho_cancelamento
	  ,tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo
	  ,tb_regional_id_regional
      ,tb_programa_id_programa
      ,tb_estrutura_id_estrutura
      ,tb_fonte_id_fonte
      ,tb_licitacao_id_licitacao
      ,tb_modalidade_id_modalidade
      ,tb_aquisicao_tipo_id_aquisicao_tipo
      ,tb_origem_material_id_origem_material
      ,tb_destino_cd_destino
	  ,cd_fonte_siafisico
      ,cd_empenho
      ,nr_ano_exercicio
      ,nr_processo
      ,nr_processo_ne
      ,nr_processo_siafisico
      ,nr_contrato
      ,nr_ct
	  ,nr_ct_original
      ,nr_empenhoProdesp
      ,nr_empenhoSiafem
      ,nr_empenhoSiafisico
	  ,cd_aplicacao_obra
	  ,cd_aplicacao_obra
	  ,nr_natureza_item
	  ,nr_natureza_ne
      ,dt_cadastramento
      ,nr_cnpj_cpf_ug_credor
      ,cd_gestao_credor
      ,cd_credor_organizacao
      ,cd_unidade_gestora
      ,cd_gestao
      ,cd_evento
      ,dt_emissao
      ,cd_unidade_gestora_fornecedora
      ,cd_gestao_fornecedora
      ,cd_uo
      ,cd_unidade_fornecimento
      ,ds_autorizado_supra_folha
      ,cd_especificacao_despesa
      ,ds_especificacao_despesa
      ,cd_autorizado_assinatura
      ,cd_autorizado_grupo
      ,cd_autorizado_orgao
      ,nm_autorizado_assinatura
      ,ds_autorizado_cargo
      ,cd_examinado_assinatura
      ,cd_examinado_grupo
      ,cd_examinado_orgao
      ,nm_examinado_assinatura
      ,ds_examinado_cargo
      ,cd_responsavel_assinatura
      ,cd_responsavel_grupo
      ,cd_responsavel_orgao
      ,nm_responsavel_assinatura
      ,ds_responsavel_cargo
      ,bl_transmitir_prodesp
      ,fg_transmitido_prodesp
      ,dt_transmitido_prodesp
      ,bl_transmitir_siafem
      ,fg_transmitido_siafem
      ,dt_transmitido_siafem
      ,bl_transmitir_siafisico
      ,fg_transmitido_siafisico
      ,dt_transmitido_siafisico
      ,ds_status_prodesp
      ,ds_status_siafem
      ,ds_status_siafisico
      ,ds_status_documento
      ,bl_cadastro_completo
      ,ds_msgRetornoTransmissaoProdesp
      ,ds_msgRetornoTransmissaoSiafem
      ,ds_msgRetornoTransmissaoSiafisico
	  ,nr_cnpj_cpf_fornecedor
	  ,cd_municipio
	  ,ds_acordo
	  ,cd_empenho_original
	  ,ds_status_siafisico_ct
	  ,ds_local_entrega_siafem
	  	  
	FROM empenho.tb_empenho_cancelamento (nolock)
	where
		( nullif( @id_empenho_cancelamento, 0 ) is null or id_empenho_cancelamento = @id_empenho_cancelamento ) and
		( nullif( @tb_regional_id_regional, 0 ) is null or tb_regional_id_regional = @tb_regional_id_regional )
   
end;