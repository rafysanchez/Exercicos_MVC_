﻿-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para consultar cancelamentos de subempenho
-- =================================================================== 
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_CONSULTAR]
	@id_subempenho_cancelamento int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		id_subempenho_cancelamento
	,	dt_cadastro
	,	fl_sistema_prodesp
	,	fl_sistema_siafem_siafisico
	,	nr_prodesp
	,	nr_siafem_siafisico
	,	nr_contrato
	,	tb_cenario_id_cenario
	,	nr_subempenho_prodesp
	,	vl_realizado
	,	vl_anular
	,	cd_cenario_prodesp
	,	nr_ct
	,	nr_empenho_siafem_siafisico
	,	cd_unidade_gestora
	,	cd_gestao
	,	vl_valor
	,	dt_emissao
	,	cd_evento
	,	tb_evento_tipo_id_evento_tipo
	,	nr_cnpj_cpf_credor
	,	cd_gestao_credor
	,	nr_ano_medicao
	,	nr_mes_medicao
	,	nr_percentual
	,	cd_classificacao_despesa
	,	tb_regional_id_regional
	,	cd_aplicacao_obra
	,   nr_obra
	,	tb_obra_tipo_id_obra_tipo
	,	cd_unidade_gestora_obra
	,	ds_observacao_1
	,	ds_observacao_2
	,	ds_observacao_3
	,	nr_despesa_processo
	,	ds_despesa_referencia
	,	ds_despesa_autorizado_supra_folha
	,	cd_despesa_especificacao_despesa
	,	ds_despesa_especificacao_1
	,	ds_despesa_especificacao_2
	,	ds_despesa_especificacao_3
	,	ds_despesa_especificacao_4
	,	ds_despesa_especificacao_5
	,	ds_despesa_especificacao_6
	,	ds_despesa_especificacao_7
	,	ds_despesa_especificacao_8
	,	ds_despesa_especificacao_9
	,	cd_assinatura_autorizado
	,	cd_assinatura_autorizado_grupo
	,	cd_assinatura_autorizado_orgao
	,	ds_assinatura_autorizado_cargo
	,	nm_assinatura_autorizado
	,	cd_assinatura_examinado
	,	cd_assinatura_examinado_grupo
	,	cd_assinatura_examinado_orgao
	,	ds_assinatura_examinado_cargo
	,	nm_assinatura_examinado
	,	cd_assinatura_responsavel
	,	cd_assinatura_responsavel_grupo
	,	cd_assinatura_responsavel_orgao
	,	ds_assinatura_responsavel_cargo
	,	nm_assinatura_responsavel
	,	cd_transmissao_status_prodesp
	,	fl_transmissao_transmitido_prodesp
	,	dt_transmissao_transmitido_prodesp
	,	ds_transmissao_mensagem_prodesp
	,	cd_transmissao_status_siafem_siafisico
	,	fl_transmissao_transmitido_siafem_siafisico
	,	dt_transmissao_transmitido_siafem_siafisico
	,	ds_transmissao_mensagem_siafem_siafisico
	,	fl_documento_completo
	,	fl_documento_status
	FROM pagamento.tb_subempenho_cancelamento (nolock)
	where
		( nullif( @id_subempenho_cancelamento, 0 ) is null or id_subempenho_cancelamento = @id_subempenho_cancelamento )
   
end;