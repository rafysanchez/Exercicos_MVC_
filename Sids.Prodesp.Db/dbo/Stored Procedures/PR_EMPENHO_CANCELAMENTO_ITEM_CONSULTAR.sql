-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_ITEM_CONSULTAR]
	@id_item  int = 0
,	@tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO int = 0
,	@cd_item_servico varchar(9) = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@ds_unidade_medida varchar(4) = null
,	@qt_material_servico decimal(18,0) = 0
,	@ds_justificativa_preco varchar(142) = null
,	@ds_item_siafem varchar(753) = null
,	@vr_unitario decimal(18,0) = 0
,	@vr_preco_total decimal(18,0) = 0
,   @ds_status_siafisico_item char(1) = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_item
		,	tb_empenho_cancelamento_id_empenho_cancelamento
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
		FROM empenho.tb_empenho_cancelamento_item (nolock)
		WHERE 
	  		( isnull( @id_item, 0 ) = 0 or id_item = @id_item )
		and	( tb_empenho_cancelamento_id_empenho_cancelamento = @tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO )
		ORDER BY 
			id_item 
END