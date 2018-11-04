
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Excluir um Evento
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_EVENTO_EXCLUIR]
	@id_evento int
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM [pagamento].[tb_evento]
	WHERE id_evento =  @id_evento;
END