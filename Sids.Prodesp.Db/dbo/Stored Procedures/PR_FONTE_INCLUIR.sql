
-- ==============================================================    
-- Author:  Carlos Henrique   
-- Create date: 07/10/2016    
-- Description: Procedure para Incluir  fontes utilizadas na reserva  
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_FONTE_INCLUIR]    
   @cd_fonte  VARCHAR (10)= NULL,  
   @ds_fonte  VARCHAR(45)= NULL 
 
  
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
     INSERT INTO configuracao.tb_fonte  
     (cd_fonte,  
      ds_fonte  
     )  
 VALUES  
     (@cd_fonte,  
      @ds_fonte  
     )  
  
   COMMIT  
 SELECT SCOPE_IDENTITY();