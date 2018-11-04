
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Excluir uma Despesa
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_DESPESA_EXCLUIR]
	@id_despesa int
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM [pagamento].[tb_despesa]
	WHERE id_despesa =  @id_despesa;
END