
-- ===================================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para exclusão de Menu na base de dados
-- ===================================================================

CREATE PROCEDURE [dbo].[PR_MENU_EXCLUIR] 
	@id_menu		INTEGER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM seguranca.tb_menu_item
	 WHERE id_menu = @id_menu

	DELETE FROM seguranca.tb_menu
	 WHERE id_menu = @id_menu

END