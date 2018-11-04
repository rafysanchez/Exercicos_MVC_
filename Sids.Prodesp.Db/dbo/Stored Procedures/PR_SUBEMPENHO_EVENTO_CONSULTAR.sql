-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description:	Procedure para consulta de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_EVENTO_CONSULTAR]
	@id_subempenho_evento int = NULL
,	@tb_subempenho_id_subempenho int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_evento
		,	tb_subempenho_id_subempenho
		,	cd_fonte
		,	cd_evento
		,	cd_classificacao
		,	ds_inscricao
		,	vl_evento
		FROM pagamento.tb_subempenho_evento (nolock)
		WHERE 
	  		( nullif( @id_subempenho_evento, 0 ) is null or id_subempenho_evento = @id_subempenho_evento )
		and	( tb_subempenho_id_subempenho = @tb_subempenho_id_subempenho )
		ORDER BY 
			id_subempenho_evento
END