-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_MES_CONSULTAR]
	@id_empenho_mes			int = null
,	@tb_empenho_id_empenho	int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_empenho_mes
		,	tb_empenho_id_empenho
		,	ds_mes
		,	vr_mes
		FROM empenho.tb_empenho_mes (nolock)
		WHERE 
	  		( nullif( @id_empenho_mes, 0 ) is null or id_empenho_mes = @id_empenho_mes ) and
			( nullif( @tb_empenho_id_empenho, 0 ) is null or tb_empenho_id_empenho = @tb_empenho_id_empenho )

			ORDER BY empenho.tb_empenho_mes.ds_mes
END