
-- ==============================================================    
-- Author:  Carlos Henrique   
-- Create date: 07/10/2016    
-- Description: Procedure para consulta se fontes alteradas estao duplicadas 
-- exec PR_FONTE_ALTERAR_DUPLICADO ,'',''
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_FONTE_ALTERAR_DUPLICADO]    
  @id_fonte     INT =NULL,    
  @cd_fonte     VARCHAR(10) = NULL,  
  @ds_fonte     VARCHAR(45)= NULL  
  
AS    
BEGIN    
 SET NOCOUNT ON;  
   
  SELECT   
     f.[id_fonte],    
     f.[cd_fonte] ,   
     f.[ds_fonte]    
      
    FROM configuracao.tb_fonte (NOLOCK) f     
 WHERE   
    (id_fonte != @id_fonte) and  
    ((f.cd_fonte = @cd_fonte  or isnull(@cd_fonte,'')='') Or
    (f.ds_fonte = @ds_fonte  or isnull(@ds_fonte,'')=''))

    ORDER BY f.[cd_fonte]    
END