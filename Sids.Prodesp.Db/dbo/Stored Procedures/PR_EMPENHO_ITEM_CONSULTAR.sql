-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_ITEM_CONSULTAR]
	@id_item  int = 0
,	@tb_empenho_id_empenho int = 0
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_item
		,	tb_empenho_id_empenho
		,	cd_item_servico
		,	cd_unidade_fornecimento
		,	ds_unidade_medida
		,	qt_material_servico
		,	ds_justificativa_preco
		,	ds_item_siafem
		,	vr_unitario
		,	vr_preco_total
		,	ds_status_siafisico_item
		,	nr_sequeincia
		FROM empenho.tb_empenho_item (nolock)
		WHERE 
	  		( isnull( @id_item, 0 ) = 0 or id_item = @id_item )
		and	( tb_empenho_id_empenho = @tb_empenho_id_empenho )
		ORDER BY 
			id_item 
END