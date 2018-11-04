-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para inclusão de valores para empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_ITEM_INCLUIR]    
	@tb_empenho_id_empenho int = null
,	@cd_item_servico varchar(9) = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@ds_unidade_medida varchar(4) = null
,	@qt_material_servico decimal(18,2) = null
,   @nr_sequeincia int = null
,	@ds_justificativa_preco varchar(142) = null
,	@ds_item_siafem varchar(753) = null
,	@vr_unitario decimal(18,0) = null
,	@vr_preco_total decimal(18,0) = null
,	@ds_status_siafisico_item char(1) = null

AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO empenho.tb_empenho_item (
			tb_empenho_id_empenho
		,	cd_item_servico
		,	cd_unidade_fornecimento
		,	ds_unidade_medida
		,	qt_material_servico
		,   nr_sequeincia
		,	ds_justificativa_preco
		,	ds_item_siafem
		,	vr_unitario
		,	vr_preco_total
		,	ds_status_siafisico_item
		)
		VALUES (
			@tb_empenho_id_empenho
		,	@cd_item_servico
		,	@cd_unidade_fornecimento
		,	@ds_unidade_medida
		,	@qt_material_servico
		,   @nr_sequeincia
		,	@ds_justificativa_preco
		,	@ds_item_siafem
		,	@vr_unitario
		,	@vr_preco_total
		,	@ds_status_siafisico_item
		)		
           
	COMMIT
    
    SELECT @@IDENTITY
END