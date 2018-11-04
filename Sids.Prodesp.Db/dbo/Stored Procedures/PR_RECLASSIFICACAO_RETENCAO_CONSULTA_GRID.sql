-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 11/07/2017
-- Description: Procedure para consultar reclassificacao_retencao grid
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_CONSULTA_GRID]	
	 @id_reclassificacao_retencao int = null
	,@nr_siafem_siafisico varchar(11) = null
	,@nr_processo varchar(15) = NULL
	,@cd_aplicacao_obra varchar(15) = NULL
    ,@nr_empenho_siafem_siafisico varchar(11)= NULL
    ,@ds_normal_estorno char(1)= NULL
    ,@id_tipo_reclassificacao_retencao int= NULL
    ,@cd_transmissao_status_siafem_siafisico char(1)= NULL
    ,@nr_contrato varchar(13)= NULL
	,@dt_cadastramento_de date = NULL
	,@dt_cadastramento_ate date = NULL
	,@id_regional smallint = null
	,@cd_origem smallint = null
	,@cd_agrupamento_confirmacao smallint = null
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
	  ,[cd_origem]
	  ,[cd_agrupamento_confirmacao]
  FROM [contaunica].[tb_reclassificacao_retencao] (nolock)
	where
		( nullif( @id_reclassificacao_retencao, 0 ) is null or id_reclassificacao_retencao = @id_reclassificacao_retencao )
        And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
		and (nullif( @id_tipo_reclassificacao_retencao, 0 ) is null or [id_tipo_reclassificacao_retencao] = @id_tipo_reclassificacao_retencao )
		and(@nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico)
		and (@nr_processo is null or nr_processo = @nr_processo )
		and (@cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )
		and (@nr_empenho_siafem_siafisico is null or nr_empenho_siafem_siafisico = @nr_empenho_siafem_siafisico )
		and (@ds_normal_estorno is null or ds_normal_estorno = @ds_normal_estorno )
		and (@cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico )
		and (@nr_contrato is null or nr_contrato = @nr_contrato )
	
		and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de ) 
		and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate ) 

		
		and (@cd_origem is null or cd_origem = @cd_origem )
		and (@cd_agrupamento_confirmacao is null or cd_agrupamento_confirmacao = @cd_agrupamento_confirmacao )

	Order by id_reclassificacao_retencao
end;