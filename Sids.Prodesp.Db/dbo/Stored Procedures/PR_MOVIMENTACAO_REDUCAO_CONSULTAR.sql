CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_CONSULTAR]  
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
	  rs.id_reducao_suplementacao
	  ,rs.cd_destino_recurso
	  ,rs.nr_orgao
	  ,rs.nr_suplementacao_reducao
	  ,rs.valor as ValorTotal
	  ,rs.cd_gestao_favorecido
	  ,rs.cd_autorizado_assinatura
	  ,rs.cd_autorizado_grupo
	  ,rs.cd_autorizado_orgao
	  ,rs.ds_autorizado_cargo
	  ,rs.ds_autorizado_supra_folha
	  ,rs.nm_autorizado_assinatura
	  ,rs.cd_examinado_assinatura
	  ,rs.cd_examinado_grupo
	  ,rs.cd_examinado_orgao
	  ,rs.ds_examinado_cargo
	  ,rs.nm_examinado_assinatura
	  ,rs.cd_responsavel_assinatura
	  ,rs.cd_responsavel_grupo
	  ,rs.cd_responsavel_orgao
	  ,rs.ds_responsavel_cargo
	  ,rs.nm_responsavel_assinatura
	  ,rs.nr_seq
	  ,rs.flag_red_sup
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs 
  WHERE 
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  rs.nr_agrupamento = @nr_agrupamento )    and flag_red_sup = 'R'
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_reducao_suplementacao,nr_seq



  END