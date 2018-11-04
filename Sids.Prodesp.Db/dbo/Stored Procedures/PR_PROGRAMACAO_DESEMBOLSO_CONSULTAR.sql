-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 23/09/2017
-- Description: Procedure para consultar programacao desembolso
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_CONSULTAR]
	@id_programacao_desembolso int = 0,
	@id_regional int = 0,
	@nr_documento_gerador varchar(22) = null,
	@nr_siafem_siafisico varchar(11) = null
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
	   [id_programacao_desembolso]
      ,[id_tipo_programacao_desembolso]
      ,[id_tipo_documento]
      ,[dt_cadastro]
      ,[nr_siafem_siafisico]
      ,[nr_contrato]
      ,[nr_processo]
      ,[nr_documento]
      ,[cd_unidade_gestora]
      ,[cd_gestao]
      ,[vl_total]
      ,[dt_emissao]
      ,[id_regional]
      ,[cd_aplicacao_obra]
      ,[nr_lista_anexo]
      ,[nr_nl_referencia]
      ,[ds_finalidade]
      ,[cd_despesa]
      ,[dt_vencimento]
      ,[nr_cnpj_cpf_credor]
      ,[cd_gestao_credor]
      ,[nr_banco_credor]
      ,[nr_agencia_credor]
      ,[nr_conta_credor]
      ,[nr_cnpj_cpf_pgto]
      ,[cd_gestao_pgto]
      ,[nr_banco_pgto]
      ,[nr_agencia_pgto]
      ,[nr_conta_pgto]
      ,[fl_sistema_siafem_siafisico]
      ,[cd_transmissao_status_siafem_siafisico]
      ,[fl_transmissao_transmitido_siafem_siafisico]
      ,[dt_transmissao_transmitido_siafem_siafisico]
      ,[ds_transmissao_mensagem_siafem_siafisico]
      ,[bl_cadastro_completo]
	  ,[nr_documento_gerador]
	  ,[nr_agrupamento]
	  ,ds_causa_cancelamento 
	,bl_bloqueio
	,bl_cancelado 
	,[obs]
	,[nr_ne]
	,[nr_ct]	
  FROM [contaunica].[tb_programacao_desembolso] (nolock)
	where
		( nullif( @id_programacao_desembolso, 0 ) is null or id_programacao_desembolso = @id_programacao_desembolso )
        And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
		and	( @nr_documento_gerador is null or nr_documento_gerador = @nr_documento_gerador )
		and	( @nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico )
	Order by id_programacao_desembolso
end;