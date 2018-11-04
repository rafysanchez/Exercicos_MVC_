CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR]  
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
	  rs.id_reducao_suplementacao as IdReducaoSuplementacao
	  ,rs.cd_destino_recurso as DestinoRecurso
	  ,rs.nr_orgao as NrOrgao
	  ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao
	  ,rs.valor as ValorTotal
	  
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs 

  WHERE 
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  rs.nr_agrupamento = @nr_agrupamento )    and flag_red_sup = 'S'
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_reducao_suplementacao,nr_seq



  END