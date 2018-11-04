-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 21/12/2016
-- Description: Procedure para exclusão de tipo de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_TIPO_EXCLUIR]   
	@id_empenho_tipo int
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_tipo
	WHERE id_empenho_tipo =  @id_empenho_tipo;
  
END;