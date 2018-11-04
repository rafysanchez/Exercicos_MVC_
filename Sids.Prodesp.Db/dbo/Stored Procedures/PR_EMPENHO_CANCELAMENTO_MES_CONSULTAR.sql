-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_MES_CONSULTAR]  
	@id_empenho_cancelamento_mes			int = null
,	@tb_empenho_cancelamento_id_empenho_cancelamento	int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_empenho_cancelamento_mes
		,	tb_empenho_cancelamento_id_empenho_cancelamento
		,	ds_mes
		,	vr_mes
		FROM empenho.tb_empenho_cancelamento_mes (nolock)
		WHERE 
	  		( nullif( @id_empenho_cancelamento_mes, 0 ) is null or id_empenho_cancelamento_mes = @id_empenho_cancelamento_mes ) and
			( nullif( @tb_empenho_cancelamento_id_empenho_cancelamento, 0 ) is null or tb_empenho_cancelamento_id_empenho_cancelamento = @tb_empenho_cancelamento_id_empenho_cancelamento )

       ORDER BY empenho.tb_empenho_cancelamento_mes.ds_mes
END