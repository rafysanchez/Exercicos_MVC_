
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 23/10/2016
-- Description:	Procedure para consulta de Recursos cadastrados
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RECURSO_CONSULTAR]
	@id_recurso				INT				= NULL,
	@no_recurso				VARCHAR(100)	= NULL,
	@ds_recurso				VARCHAR(1000)	= NULL,
	@ds_keywords			VARCHAR(1000)	= NULL,
	@ds_url					VARCHAR(2048)	= NULL,
	@id_aplicacao			INT				= NULL,
	@bl_publico				BIT				= NULL,
	@bl_ativo				BIT				= NULL
AS
BEGIN

	SET NOCOUNT ON;
	SELECT id_recurso
			,A.no_recurso
			,A.ds_recurso
			,A.ds_keywords
			,A.ds_url
			,A.bl_publico
			,A.bl_ativo
			,A.dt_criacao
			,A.id_menu_url
		FROM seguranca.tb_recurso	  A
	  WHERE (id_recurso = @id_recurso OR ISNULL(@id_recurso,0) = 0)
			AND (no_recurso like '%'+ @no_recurso+'%' OR ISNULL(@no_recurso,'') = '')
			AND (ds_recurso like '%'+ @ds_recurso+'%'	OR ISNULL(@ds_recurso,'') = '')
			AND (ds_keywords = @ds_keywords	OR ISNULL(@ds_keywords,'') = '')
			AND (A.ds_url like '%'+ @ds_url+'%' OR ISNULL(@ds_url,'') = '')
			AND (bl_publico = @bl_publico OR @bl_publico IS NULL)
			AND (A.bl_ativo = @bl_ativo	OR @bl_ativo IS NULL)
	ORDER BY A.no_recurso 
END