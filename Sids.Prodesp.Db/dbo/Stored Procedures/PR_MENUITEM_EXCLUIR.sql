
-- ========================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para exclusão de um Item de Menu
-- ========================================================

CREATE PROCEDURE [dbo].[PR_MENUITEM_EXCLUIR]
	@id_menu_item		INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM seguranca.tb_menu_item
     WHERE id_menu_item = @id_menu_item

END