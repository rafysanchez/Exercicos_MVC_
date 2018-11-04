-- ==============================================================          
-- Author:  Alessandro de Santana          
-- Create date: 25/07/2018         
-- Description: Procedure para consulta de valor de movimentacao          
-- ==============================================================          
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_CONSULTAR]          
	@id_mes   int = null          
	, @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int = null   
	, @tb_reducao_suplementacao_id_reducao_suplementacao int = null      
	, @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int = null     
	, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null       
	, @nr_agrupamento int = null    
	, @nr_seq int = null    
	, @ds_mes varchar(9) = null  
	, @vr_mes decimal = null
	, @red_sup char(1) = null
AS          
BEGIN          
 SET NOCOUNT ON;          
          
  SELECT          
   mom.[id_mes]          
  , mom.[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]          
  , mom.[tb_reducao_suplementacao_id_reducao_suplementacao]        
  , mom.[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]        
  , mom.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]        
  , mom.[nr_agrupamento]        
  , mom.[nr_seq]        
  , mom.[ds_mes]         
  , mom.[vr_mes]  
  , mom.[cd_unidade_gestora]        
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock) mom
  LEFT JOIN [movimentacao].[tb_reducao_suplementacao] (nolock) rs on rs.id_reducao_suplementacao = mom.tb_reducao_suplementacao_id_reducao_suplementacao
  WHERE           
	( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and          
	( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or mom.tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao ) and
	( nullif( [tb_reducao_suplementacao_id_reducao_suplementacao], 0 ) is null or mom.tb_reducao_suplementacao_id_reducao_suplementacao = [tb_reducao_suplementacao_id_reducao_suplementacao] ) and
	( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or mom.tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao ) and
	( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0) is null or mom.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria ) and
	( nullif( @nr_agrupamento, 0) is null or mom.nr_agrupamento = @nr_agrupamento ) and
	( nullif( @nr_seq, 0) is null or mom.nr_seq = @nr_seq ) and
	( @red_sup is null or rs.flag_red_sup = @red_sup)
END