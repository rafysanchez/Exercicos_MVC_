
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Incluir Despesa
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_DESPESA_INCLUIR]
   @id_despesa_tipo int,
   @id_nl_parametrizacao int
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
INSERT INTO [pagamento].[tb_despesa]
           ([id_despesa_tipo]
           ,[id_nl_parametrizacao])
     VALUES
           (@id_despesa_tipo
           ,@id_nl_parametrizacao)
  
COMMIT

SELECT SCOPE_IDENTITY();