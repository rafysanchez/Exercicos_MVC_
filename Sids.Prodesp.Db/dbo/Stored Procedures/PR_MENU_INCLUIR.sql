
-- ====================================================================================
-- Author:		Francsico Edcarlo 
-- Create date: 23/10/2016
-- Description:	Procedure para inclusão de Menu na base de dados
-- ====================================================================================

CREATE PROCEDURE [dbo].[PR_MENU_INCLUIR] 
	@id_recurso					INTEGER = NULL,
	@ds_menu					VARCHAR(100),
	@nr_ordem					INTEGER	= NULL,
	@bl_ativo					BIT,
	@dt_criacao					DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO seguranca.tb_menu
		(id_recurso
		,ds_menu
		,nr_ordem
		,bl_ativo
		,dt_criacao)
	VALUES
		(@id_recurso
		,@ds_menu
		,@nr_ordem
		,@bl_ativo
		,@dt_criacao)
	
	SELECT SCOPE_IDENTITY()
END