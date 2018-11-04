
-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 09/11/2016  
-- Description: Procedure para exclusão de Reforço da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_REFORCO_EXCLUIR]   
	@id_reforco    INT
AS  
BEGIN  
	  
	SET NOCOUNT ON;  
	DELETE FROM reserva.tb_reforco
	 WHERE id_reforco =  @id_reforco;
	   
END