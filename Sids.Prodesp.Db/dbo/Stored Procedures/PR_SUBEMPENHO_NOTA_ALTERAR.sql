-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para alteração de notas para subempenho
-- ===================================================================
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_NOTA_ALTERAR]      
	@id_subempenho_nota int = null
,	@tb_subempenho_id_subempenho int = null
,	@cd_nota varchar(12) = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE pagamento.tb_subempenho_nota
	SET 
		cd_nota = @cd_nota
	 WHERE
		id_subempenho_nota			= @id_subempenho_nota and
		tb_subempenho_id_subempenho	= @tb_subempenho_id_subempenho
END