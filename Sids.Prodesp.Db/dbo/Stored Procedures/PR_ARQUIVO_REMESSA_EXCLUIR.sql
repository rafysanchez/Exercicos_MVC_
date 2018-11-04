
CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_EXCLUIR]     
 @id_arquivo_remessa int = NULL  
AS    
BEGIN    
  
 SET NOCOUNT ON;    
   
 DELETE FROM [contaunica].[tb_arquivo_remessa]
 WHERE id_arquivo_remessa = @id_arquivo_remessa;  
  
 SELECT @@ROWCOUNT;  
  
END