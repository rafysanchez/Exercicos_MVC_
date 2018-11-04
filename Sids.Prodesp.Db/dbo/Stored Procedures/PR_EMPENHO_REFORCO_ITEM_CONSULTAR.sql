-- ==============================================================
-- Author:		Carlos Henrique magalhaes
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_ITEM_CONSULTAR]
	@id_item_reforco  int = null
,	@tb_empenho_reforco_id_empenho_reforco int = null
,	@cd_item_servico varchar(9) = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@ds_unidade_medida varchar(4) = null
,	@qt_material_servico decimal(18,0) = null
,	@ds_justificativa_preco varchar(142) = null
,	@ds_item_siafem varchar(753) = null
,	@vr_unitario decimal(18,0) = null
,	@vr_preco_total decimal(18,0) = null
,   @ds_status_siafisico_item char(1) = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_item_reforco
		,	tb_empenho_reforco_id_empenho_reforco
		,	cd_item_servico
		,	cd_unidade_fornecimento
		,	ds_unidade_medida
		,	qt_material_servico
		,	ds_justificativa_preco
		,	ds_item_siafem
		,	vr_unitario
		,	vr_preco_total
		,   ds_status_siafisico_item
		,	nr_sequeincia

		FROM empenho.tb_empenho_reforco_item (nolock)
		
		WHERE 
	  		( isnull( @id_item_reforco, 0 ) = 0 or id_item_reforco = @id_item_reforco )
		and	( tb_empenho_reforco_id_empenho_reforco = @tb_empenho_reforco_id_empenho_reforco )
		ORDER BY 
			id_item_reforco 
END