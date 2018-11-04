

-- ========================================================
-- Author:		Luis Fernando
-- Create date: 16/10/2016
-- Description:	Procedure para consulta de um Item de Menu
--				por menu
-- ========================================================

CREATE PROCEDURE [dbo].[PR_GET_MENU_ITEM_POR_MENU]
	@id_menu				INT
AS
BEGIN
	SET NOCOUNT ON;
	
	
	SELECT A.id_menu_item
		,A.id_menu
		,ISNULL(B.ds_menu,'') ds_menu
		,A.id_recurso
		,ISNULL(C.no_recurso,'') no_recurso
		,A.ds_rotulo
		,A.ds_abrir_em
		,A.nr_ordem
		,A.bl_ativo
		,A.dt_criacao
	FROM seguranca.tb_menu_item A
	JOIN seguranca.tb_menu      B
		ON A.id_menu = B.id_menu
	JOIN seguranca.tb_recurso   C
		ON A.id_recurso = C.id_recurso
	WHERE   (A.id_menu = @id_menu AND @id_menu > 0)
		AND A.bl_ativo = 1 AND B.bl_ativo = 1
	ORDER BY A.id_menu, A.nr_ordem, A.id_menu_item
	
END