  
-- ==============================================================  
-- Author:  Alessandro Santana  
-- Create date: 19/09/2016  
-- Description: Procedure para consulta de tipo de documento de  movimentação  
-- ==============================================================  
 CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR]  
 @ds_tipo_documento_movimentacao varchar(100) = null,  
 @id_tipo_documento_movimentacao int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 SELECT doc.id_tipo_documento_movimentacao 
      ,doc.ds_tipo_documento_movimentacao  
  FROM [movimentacao].[tb_tipo_documento_movimentacao] doc (nolock)  
  where (doc.ds_tipo_documento_movimentacao = @ds_tipo_documento_movimentacao or @ds_tipo_documento_movimentacao is null)  
  and (doc.id_tipo_documento_movimentacao = @id_tipo_documento_movimentacao or isnull(@id_tipo_documento_movimentacao,0) = 0)  

  order by doc.ds_tipo_documento_movimentacao 
  
END