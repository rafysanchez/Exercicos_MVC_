
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR]     
 @id  int  
AS    
BEGIN    
  
 SET NOCOUNT ON;    



 DELETE  [movimentacao].[tb_cancelamento_movimentacao] 
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id




  DELETE  [movimentacao].[tb_credito_movimentacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id


  DELETE  [movimentacao].[tb_distribuicao_movimentacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id


  DELETE  [movimentacao].[tb_movimentacao_orcamentaria_mes]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id

  DELETE  [movimentacao].[tb_reducao_suplementacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id

  


 DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria]  
 WHERE   
  id_movimentacao_orcamentaria = @id







    
END