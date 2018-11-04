-- ==============================================================                
-- Author:  Alessandro de Santana                
-- Create date: 25/07/2018               
-- Description: Procedure para consulta de valor de movimentacao                
-- ==============================================================                
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR]     
	@id_mes   int = null                  
	,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao int = null           
	,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null             
	,@nr_agrupamento int = null          
	,@nr_seq int = null          
	,@ds_mes varchar(9) = null        
	,@vr_mes decimal = null        
AS                
BEGIN                
 SET NOCOUNT ON;                
                
  SELECT                
	[id_mes]                
	, [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
	, [tb_reducao_suplementacao_id_reducao_suplementacao]
	, [tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
	, [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	, [nr_agrupamento]
	, [nr_seq]
	, [ds_mes]
	, [vr_mes]
	, [cd_unidade_gestora]
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)                
  WHERE                 
	( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and                
	( tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and          
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and         
	( nr_agrupamento = @nr_agrupamento )   and           
	( nr_seq = @nr_seq )                  
END