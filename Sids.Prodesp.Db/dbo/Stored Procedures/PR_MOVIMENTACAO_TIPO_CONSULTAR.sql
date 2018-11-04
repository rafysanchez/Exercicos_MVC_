  
-- ==============================================================  
-- Author:  Alessandro Santana  
-- Create date: 19/09/2016  
-- Description: Procedure para consulta de tipo de movimentação  
-- ==============================================================  
 CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_CONSULTAR]  
 @ds_tipo_movimentacao_orcamentaria varchar(100) = null,  
 @id_tipo_movimentacao_orcamentaria int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 SELECT mov.id_tipo_movimentacao_orcamentaria
      ,mov.ds_tipo_movimentacao_orcamentariao 
  FROM [movimentacao].[tb_tipo_movimentacao_orcamentaria] mov (nolock)  
  where (mov.ds_tipo_movimentacao_orcamentariao = @ds_tipo_movimentacao_orcamentaria or @ds_tipo_movimentacao_orcamentaria is null)  
  and (mov.id_tipo_movimentacao_orcamentaria = @id_tipo_movimentacao_orcamentaria or isnull(@id_tipo_movimentacao_orcamentaria,0) = 0)  

  order by mov.id_tipo_movimentacao_orcamentaria
  
END