CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_CONSULTAR]  
           @id_evento int= NULL,
           @cd_evento int= NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int= NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @cd_inscricao_evento varchar(10)= NULL,
           @cd_classificacao int =NULL,
           @cd_fonte varchar(15)= NULL,
           @rec_despesa varchar(10)= NULL



AS    
BEGIN    
 SET NOCOUNT ON;  


SELECT [id_evento]
      ,[cd_evento]
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
      ,[nr_agrupamento]
      ,[nr_seq]
      ,[cd_inscricao_evento]
      ,[cd_classificacao]
      ,[cd_fonte]
      ,[rec_despesa]
      ,[vr_evento]
  FROM [movimentacao].[tb_movimentacao_orcamentaria_evento]



  WHERE 
  ( nullif( @id_evento, 0 ) is null or id_evento = @id_evento )   and 
  ( nullif( @cd_evento, 0 ) is null or cd_evento = @cd_evento )   and 
  ( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and 
  ( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and 
  (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
    (  nr_agrupamento = @nr_agrupamento )   and 
	(  nr_seq = @nr_seq )   and 

   (@cd_inscricao_evento is null or cd_inscricao_evento = @cd_inscricao_evento  ) and
    (@cd_classificacao is null or cd_classificacao= @cd_classificacao  ) and
	 (@cd_fonte is null or cd_fonte= @cd_fonte  ) and
	 (@rec_despesa is null or rec_despesa   = @rec_despesa  ) 

  ORDER BY id_evento,nr_seq



  END