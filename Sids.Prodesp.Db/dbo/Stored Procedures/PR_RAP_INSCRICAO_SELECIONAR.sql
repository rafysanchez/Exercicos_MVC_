-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 30/03/2017
-- Description: Procedure para consultar uma inscrição de rap
-- =================================================================== 
CREATE procedure [dbo].[PR_RAP_INSCRICAO_SELECIONAR]
	@id_rap_inscricao int
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT TOP 1
	   ri.[id_rap_inscricao]
      ,ri.[tb_programa_id_programa]
      ,ri.[tb_estrutura_id_estrutura]
	  ,cast(SUBSTRING(e.cd_natureza,0,2) as int) cedid
      ,ri.[tb_natureza_tipo_id_natureza_tipo]
      ,ri.[tb_servico_tipo_id_servico_tipo]
      ,ri.[tb_regional_id_regional]
      ,ri.[nr_prodesp]
      ,ri.[nr_siafem_siafisico]
      ,ri.[nr_empenho_prodesp]
      ,ri.[nr_empenho_siafem_siafisico]
      ,ri.[cd_credor_organizacao]
      ,ri.[nr_despesa_processo]
      ,ri.[cd_gestao_credor]
      ,ri.[cd_unidade_gestora]
      ,ri.[cd_gestao]
      ,ri.[cd_nota_fiscal_prodesp]
      ,ri.[vl_caucao_caucionado]
      ,ri.[vl_valor]
      ,ri.[vl_realizado]
      ,ri.[nr_ano_exercicio]
      ,ri.[dt_emissao]
      ,ri.[nr_ano_medicao]
      ,ri.[nr_mes_medicao]
      ,ri.[ds_observacao_1]
      ,ri.[ds_observacao_2]
      ,ri.[ds_observacao_3]
      ,ri.[cd_despesa]
      ,ri.[ds_despesa_autorizado_supra_folha]
      ,ri.[cd_despesa_especificacao_despesa]
      ,ri.[ds_despesa_especificacao_1]
      ,ri.[ds_despesa_especificacao_2]
      ,ri.[ds_despesa_especificacao_3]
      ,ri.[ds_despesa_especificacao_4]
      ,ri.[ds_despesa_especificacao_5]
      ,ri.[ds_despesa_especificacao_6]
      ,ri.[ds_despesa_especificacao_7]
      ,ri.[ds_despesa_especificacao_8]
      ,ri.[cd_assinatura_autorizado]
      ,ri.[cd_assinatura_autorizado_grupo]
      ,ri.[cd_assinatura_autorizado_orgao]
      ,ri.[ds_assinatura_autorizado_cargo]
      ,ri.[nm_assinatura_autorizado]
      ,ri.[cd_assinatura_examinado]
      ,ri.[cd_assinatura_examinado_grupo]
      ,ri.[cd_assinatura_examinado_orgao]
      ,ri.[ds_assinatura_examinado_cargo]
      ,ri.[nm_assinatura_examinado]
      ,ri.[cd_assinatura_responsavel]
      ,ri.[cd_assinatura_responsavel_grupo]
      ,ri.[cd_assinatura_responsavel_orgao]
      ,ri.[ds_assinatura_responsavel_cargo]
      ,ri.[nm_assinatura_responsavel]
      ,ri.[nr_caucao_guia]
      ,ri.[nm_caucao_quota_geral_autorizado_por]
      ,ri.[cd_transmissao_status_prodesp]
      ,ri.[fl_transmissao_transmitido_prodesp]
      ,ri.[fl_sistema_prodesp]
      ,ri.[dt_transmissao_transmitido_prodesp]
      ,ri.[ds_transmissao_mensagem_prodesp]
      ,ri.[cd_transmissao_status_siafem_siafisico]
      ,ri.[fl_transmissao_transmitido_siafem_siafisico]
      ,ri.[fl_sistema_siafem_siafisico]
      ,ri.[dt_transmissao_transmitido_siafem_siafisico]
      ,ri.[ds_transmissao_mensagem_siafem_siafisico]
      ,ri.[fl_documento_completo]
      ,ri.[fl_documento_status]
      ,ri.[dt_cadastro]
      ,ri.[cd_aplicacao_obra]
      ,ri.[nr_contrato]
      ,ri.[ds_uso_autorizado_por]
      ,ri.[fl_sistema_siafisico]
      ,ri.[cd_transmissao_status_siafisico]
      ,ri.[fl_transmissao_transmitido_siafisico]
      ,ri.[nr_cnpj_cpf_fornecedor]
	FROM pagamento.tb_rap_inscricao ri (nolock)
		join configuracao.tb_estrutura e (nolock) on e.id_estrutura = ri.tb_estrutura_id_estrutura
	where
		( id_rap_inscricao = @id_rap_inscricao )
   
end;