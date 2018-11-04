
-- ========================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para alteração de um Item de Menu
-- ========================================================

CREATE PROCEDURE [dbo].[PR_MENUITEM_ALTERAR]
	@id_menu_item				INT,
	@id_menu					INT,
	@id_recurso					INT,
	@ds_rotulo					VARCHAR(100),
	@ds_abrir_em				VARCHAR(10)		= NULL,
	@nr_ordem					INT				= NULL,
	@bl_ativo					BIT				= 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE seguranca.tb_menu_item
	   SET id_menu				= @id_menu
		  ,id_recurso			= @id_recurso
		  ,ds_rotulo			= @ds_rotulo
		  ,ds_abrir_em			= @ds_abrir_em
		  ,nr_ordem				= @nr_ordem
		  ,bl_ativo				= @bl_ativo
	 WHERE id_menu_item			= @id_menu_item
		
END