
-- =======================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para inclusão de um Item de Menu
-- =======================================================

CREATE PROCEDURE [dbo].[PR_MENUITEM_INCLUIR]
	@id_menu				INT,
	@id_recurso				INT,
	@ds_rotulo				VARCHAR(100),
	@ds_abrir_em			VARCHAR(10)		= NULL,
	@nr_ordem				INT				= NULL,
	@bl_ativo				BIT				= 1,
	@dt_criacao				DATETIME,
	@id_usuario_criacao		VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO seguranca.tb_menu_item
		(id_menu
		,id_recurso
		,ds_rotulo
		,ds_abrir_em
		,nr_ordem
		,bl_ativo
		,dt_criacao)
	VALUES
		(@id_menu
		,@id_recurso
		,@ds_rotulo
		,@ds_abrir_em
		,@nr_ordem
		,@bl_ativo
		,@dt_criacao)
	
	SELECT SCOPE_IDENTITY()
END