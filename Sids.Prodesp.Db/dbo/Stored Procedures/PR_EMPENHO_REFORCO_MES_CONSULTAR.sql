-- ==============================================================
-- Author:		Carlos Henrique Magalhães
-- Create date: 17/01/2017
-- Description:	Procedure para consulta de valor de reforço de empenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_CONSULTAR]
	@id_empenho_reforco_mes			int
,	@tb_empenho_reforco_id_empenho_reforco	int
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_empenho_reforco_mes
		,	tb_empenho_reforco_id_empenho_reforco
		,	ds_mes
		,	vr_mes
		FROM empenho.tb_empenho_reforco_mes (nolock)
		WHERE 
	  		( nullif( @id_empenho_reforco_mes, 0 ) is null or id_empenho_reforco_mes = @id_empenho_reforco_mes ) and
			( nullif( @tb_empenho_reforco_id_empenho_reforco, 0 ) is null or tb_empenho_reforco_id_empenho_reforco = @tb_empenho_reforco_id_empenho_reforco )

			ORDER BY empenho.tb_empenho_reforco_mes.ds_mes
END