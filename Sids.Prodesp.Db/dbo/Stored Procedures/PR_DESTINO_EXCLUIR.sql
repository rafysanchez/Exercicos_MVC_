-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016  
-- Description: Procedure para exclusão de destinos  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_DESTINO_EXCLUIR]   
	@id_destino     INT
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM configuracao.tb_destino
	 WHERE id_destino =  @id_destino;
  
END