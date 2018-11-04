-- ===================================================================          
-- Author:  Luis Fernando    
-- Create date: 23/08/2017    
-- Description: Procedure para consulta de itens para programacao desembolso agrupamento    
-- ===================================================================          
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_CONSULTAR]    
 @id_programacao_desembolso_agrupamento int = NULL    
, @id_programacao_desembolso int = NULL    
, @nr_documento_gerador varchar(22) = NULL    
 ,@nr_agrupamento int= NULL 
 ,@nr_siafem_siafisico varchar(11) = null  
 ,@nr_processo varchar(15) = NULL  
 ,@id_tipo_documento int = NULL  
 ,@nr_documento varchar(19) = NULL  
 ,@bl_bloqueio bit = null


AS    
BEGIN    
 SET NOCOUNT ON;    
    
  SELECT    
    pda.[id_programacao_desembolso_agrupamento]    
      ,pda.[id_programacao_desembolso]    
      ,pd.[id_tipo_programacao_desembolso]   
      ,pda.[id_tipo_documento]    
      ,pda.[nr_agrupamento]    
      ,pda.[vl_valor]    
      ,pda.[nr_programacao_desembolso] as nr_siafem_siafisico    
      ,pda.[nr_sequencia]    
      ,pda.[id_regional]    
      ,pda.[nr_documento]    
      ,pda.[cd_unidade_gestora]    
      ,pda.[cd_gestao]    
      ,pda.[nr_nl_referencia]    
      ,pda.[nr_cnpj_cpf_credor]    
      ,pda.[cd_gestao_credor]    
      ,pda.[nr_banco_credor]    
      ,pda.[nr_agencia_credor]    
      ,pda.[nr_conta_credor]    
      ,pda.[nm_reduzido_credor]    
      ,pda.[nr_cnpj_cpf_pgto]    
      ,pda.[cd_gestao_pgto]    
      ,pda.[nr_banco_pgto]    
      ,pda.[nr_agencia_pgto]    
      ,pda.[nr_conta_pgto]    
      ,pda.[nr_processo]    
      ,pda.[ds_finalidade]    
      ,pda.[cd_fonte]    
      ,pda.[cd_evento]    
      ,pda.[cd_classificacao]    
      ,pda.[ds_inscricao]    
      ,pda.[cd_despesa]    
      ,pda.[dt_emissao]    
      ,pda.[dt_vencimento]    
      ,pda.[nr_lista_anexo]    
      ,pda.[cd_transmissao_status_siafem_siafisico]    
      ,pda.[fl_transmissao_transmitido_siafem_siafisico]    
      ,pda.[dt_transmissao_transmitido_siafem_siafisico]    
      ,pda.[ds_msg_retorno]    
	  ,pda.[nr_documento_gerador]    
	  ,pda.dt_cadastro    
	  ,pda.ds_causa_cancelamento     
	  ,pda.bl_bloqueio    
      ,pda.bl_cancelado     
      , pd.fl_sistema_siafem_siafisico   
	  ,pda.rec_despesa 
  FROM [contaunica].[tb_programacao_desembolso_agrupamento] pda (nolock)  
  INNER JOIN   [contaunica].[tb_programacao_desembolso] pd (nolock)  on pda.id_programacao_desembolso = pd.id_programacao_desembolso
  WHERE     
 ( nullif( @id_programacao_desembolso_agrupamento, 0 ) is null or pda.id_programacao_desembolso_agrupamento = @id_programacao_desembolso_agrupamento )    
  and ( nullif( @id_programacao_desembolso, 0 ) is null or pda.id_programacao_desembolso = @id_programacao_desembolso )    
  and ( @nr_documento_gerador is null or pda.nr_documento_gerador = @nr_documento_gerador )  
  and (@nr_processo is null or pda.nr_processo = @nr_processo )    
  and (nullif(@id_tipo_documento,0)is null or pda.[id_tipo_documento] = @id_tipo_documento  )
  and ( @nr_documento is null or   (REPLACE(pda.nr_documento,'/','') like '%'+ REPLACE( @nr_documento,'/','') +'%' ) )
  and (@nr_siafem_siafisico is null or pda.nr_programacao_desembolso = @nr_siafem_siafisico  )
  and (nullif(@nr_agrupamento,0) is null or pda.nr_agrupamento = @nr_agrupamento)
  and (@bl_bloqueio is null or pda.bl_bloqueio = @bl_bloqueio  )
  
  ORDER BY     
   pda.id_programacao_desembolso_agrupamento    
END