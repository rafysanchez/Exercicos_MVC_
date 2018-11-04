
-- ==============================================================    
-- Author:  Jose Antonio
-- Create date: 15/01/2018
-- Description: Procedure para Excluir tipo de despesa
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_TIPO_DESPESA_EXCLUIR]   
	@id_despesa_tipo     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM [pagamento].[tb_despesa_tipo]
	WHERE id_despesa_tipo =  @id_despesa_tipo;
END