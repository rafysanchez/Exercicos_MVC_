-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 16/01/2017
-- Description: Procedure para exclusão de Reforços
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_EXCLUIR]   
	@id_empenho_reforco INT
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM empenho.tb_empenho_reforco_mes
	WHERE tb_empenho_reforco_id_empenho_reforco =  @id_empenho_reforco;

	DELETE FROM empenho.tb_empenho_reforco_item
	WHERE tb_empenho_reforco_id_empenho_reforco =  @id_empenho_reforco;

	DELETE FROM empenho.tb_empenho_reforco
	WHERE id_empenho_reforco = @id_empenho_reforco;
  
END