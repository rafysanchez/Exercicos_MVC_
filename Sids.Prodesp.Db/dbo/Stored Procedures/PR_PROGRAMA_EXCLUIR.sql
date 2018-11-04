
-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 11/10/2016  
-- Description: Procedure para exclusão de Programa da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMA_EXCLUIR]   
	@id_programa     INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
	DELETE FROM configuracao.tb_estrutura
	 WHERE id_programa =  @id_programa;

	DELETE FROM configuracao.tb_programa
	 WHERE id_programa =  @id_programa;
  
END