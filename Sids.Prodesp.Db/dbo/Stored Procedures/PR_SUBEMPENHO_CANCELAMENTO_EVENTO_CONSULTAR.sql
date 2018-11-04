-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description:	Procedure para consulta de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_EVENTO_CONSULTAR]
	@id_subempenho_cancelamento_evento int = NULL
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_cancelamento_evento as id_subempenho_evento
		,	tb_subempenho_cancelamento_id_subempenho_cancelamento as tb_subempenho_id_subempenho
		,	cd_fonte
		,	cd_evento
		,	cd_classificacao
		,	ds_inscricao
		,	vl_evento
		FROM pagamento.tb_subempenho_cancelamento_evento (nolock)
		WHERE 
	  		( nullif( @id_subempenho_cancelamento_evento, 0 ) is null or id_subempenho_cancelamento_evento = @id_subempenho_cancelamento_evento )
		and	( tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento )
		ORDER BY 
			id_subempenho_cancelamento_evento
END