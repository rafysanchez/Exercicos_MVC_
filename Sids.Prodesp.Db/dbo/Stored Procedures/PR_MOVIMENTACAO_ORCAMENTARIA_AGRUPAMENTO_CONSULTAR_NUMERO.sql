-- ===================================================================    
-- Author:  Alessandro de Santana 
-- Create date: 31/07/2018  
-- Description: Procedure para consultar ultimo agrupamento 
-- ===================================================================   
CREATE procedure [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO]  
as    
begin    
    
 SET NOCOUNT ON;    
    
 SELECT TOP 1   
  [nr_agrupamento_movimentacao]  
 FROM  [movimentacao].[tb_movimentacao_orcamentaria]  (nolock)  
 ORDER BY  
  [nr_agrupamento_movimentacao] desc  
     
end;