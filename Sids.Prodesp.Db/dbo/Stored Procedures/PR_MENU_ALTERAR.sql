
-- ====================================================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para alteração de Menu na base de dados
-- ====================================================================================


CREATE PROCEDURE [dbo].[PR_MENU_ALTERAR] 
	@id_menu					INTEGER,
	@id_aplicacao				INTEGER,
	@id_recurso					INTEGER = NULL,
	@ds_menu					VARCHAR(100),
	@nr_ordem					INTEGER      = NULL,
	@bl_ativo					BIT,
	@dt_alteracao				DATETIME,
    @id_usuario_alteracao		VARCHAR(20)
    
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE seguranca.tb_menu
	   SET id_recurso			= @id_recurso
		  ,ds_menu				= @ds_menu
		  ,nr_ordem				= @nr_ordem
		  ,bl_ativo				= @bl_ativo
	 WHERE id_menu				= @id_menu

END