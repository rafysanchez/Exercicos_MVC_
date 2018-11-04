-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consultar reclassificacao retencao
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_CONSULTAR]
	@id_reclassificacao_retencao int = 0,
	@id_regional int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  [id_reclassificacao_retencao]
      ,[id_resto_pagar]
      ,[id_tipo_reclassificacao_retencao]
      ,[id_tipo_documento]
      ,[dt_cadastro]
      ,[nr_siafem_siafisico]
      ,[nr_contrato]
	  ,[nr_processo]
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
      ,[id_regional]
      ,[cd_aplicacao_obra]
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
  FROM [contaunica].[tb_reclassificacao_retencao] (nolock)
	where
		( nullif( @id_reclassificacao_retencao, 0 ) is null or id_reclassificacao_retencao = @id_reclassificacao_retencao )
        And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
	Order by id_reclassificacao_retencao
end;