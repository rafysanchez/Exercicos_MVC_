-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description:	Procedure para consulta de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_ITEM_CONSULTAR]
	@id_subempenho_cancelamento_item int = NULL
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_cancelamento_item
		,	tb_subempenho_cancelamento_id_subempenho_cancelamento
		,	cd_servico
		,	cd_unidade_fornecimento
		,	qt_material_servico
		,	cd_status_siafisico
		,	nr_sequencia
		,	transmitir
		FROM pagamento.tb_subempenho_cancelamento_item (nolock)
		WHERE 
	  		( nullif( @id_subempenho_cancelamento_item, 0 ) is null or id_subempenho_cancelamento_item = @id_subempenho_cancelamento_item )
		and	( tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento )
		ORDER BY 
			id_subempenho_cancelamento_item 
END