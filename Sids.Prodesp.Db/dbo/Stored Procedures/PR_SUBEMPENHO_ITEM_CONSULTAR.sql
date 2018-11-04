-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description:	Procedure para consulta de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_ITEM_CONSULTAR]
	@id_subempenho_item int = NULL
,	@tb_subempenho_id_subempenho int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_item
		,	tb_subempenho_id_subempenho
		,	cd_servico
		,	cd_unidade_fornecimento
		,	qt_material_servico
		,	cd_status_siafisico
		,	nr_sequencia
		,	transmitir
		,	vl_valor
		FROM pagamento.tb_subempenho_item (nolock)
		WHERE 
	  		( nullif( @id_subempenho_item, 0 ) is null or id_subempenho_item = @id_subempenho_item )
		and	( tb_subempenho_id_subempenho = @tb_subempenho_id_subempenho )
		ORDER BY 
			id_subempenho_item 
END