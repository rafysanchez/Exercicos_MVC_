CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR]   
           --@id_cancelamento_movimentacao int =NULL,    
           --@tb_fonte_id_fonte int =NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
           @nr_agrupamento int =NULL    
           --@nr_seq int =NULL    
           --@nr_nota_cancelamento varchar(10) =NULL,    
           --@cd_unidade_gestora varchar(15) =NULL,    
           --@nr_categoria_gasto varchar(15) =NULL,    
           --@ds_observacao varchar(231) =NULL,    
           --@fg_transmitido_prodesp char(1) =NULL,    
           --@fg_transmitido_siafem char(1)= NULL    
    
      
AS        
BEGIN        
 SET NOCOUNT ON;      
    
    
    
SELECT 
      [id_reducao_suplementacao]
      ,[tb_credito_movimentacao_id_nota_credito]
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
      ,[tb_programa_id_programa]
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
      ,[nr_agrupamento]
      ,[nr_seq]
      ,[nr_suplementacao_reducao]
      ,[fl_proc]
      ,[nr_processo]
      ,[nr_orgao]
      ,[nr_obra]
      ,[flag_red_sup]
      ,[nr_cnpj_cpf_ug_credor]
      ,[ds_autorizado_supra_folha]
      ,[cd_origem_recurso]
      ,[cd_destino_recurso]
      ,[cd_especificacao_despesa] 
      ,[ds_especificacao_despesa] 
      ,[cd_autorizado_assinatura] 
      ,[cd_autorizado_grupo]
      ,[cd_autorizado_orgao]
      ,[ds_autorizado_cargo]
      ,[nm_autorizado_assinatura]
      ,[cd_examinado_assinatura]  
      ,[cd_examinado_grupo]
      ,[cd_examinado_orgao]
      ,[ds_examinado_cargo]
      ,[nm_examinado_assinatura]
      ,[cd_responsavel_assinatura]
      ,[cd_responsavel_grupo]
      ,[cd_responsavel_orgao]
      ,[ds_responsavel_cargo]
	  ,[nm_responsavel_assinatura]
      ,[fg_transmitido_prodesp]
      ,[ds_msgRetornoProdesp]
      ,[fg_transmitido_siafem]
      ,[ds_msgRetornoSiafem]
	  ,[cd_unidade_gestora]
	  ,[cd_gestao_favorecido]
	  ,[TotalQ1]
	  ,[TotalQ2]
	  ,[TotalQ3]
	  ,[TotalQ4]
	  ,[valor]  
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs   
    
  WHERE     
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
  (  rs.nr_agrupamento = @nr_agrupamento )      
  -- ( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
  --( nullif( @nr_agrupamento, 0 ) is null or c.nr_agrupamento = @nr_agrupamento )        
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )        
        
  ORDER BY id_reducao_suplementacao,nr_seq
    
  END