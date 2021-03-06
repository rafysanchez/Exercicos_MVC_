﻿-- ===================================================================    
-- Author:		Carlos Henrique Magalhaes
-- Create date: 11/01/2017
-- Description: Procedure para alteração de itens de reforco de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_ITEM_ALTERAR]      
	@id_item_reforco  int = null
,	@tb_empenho_reforco_id_empenho_reforco int = null
,	@cd_item_servico varchar(9) = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@ds_unidade_medida varchar(4) = null
,	@qt_material_servico decimal(18,2) = null
,	@ds_justificativa_preco varchar(142) = null
,	@ds_item_siafem varchar(753) = null
,	@vr_unitario decimal(18,0) = null
,	@vr_preco_total decimal(18,0) = null
,   @ds_status_siafisico_item char(1) = null
,  @nr_sequeincia int = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_reforco_item
	SET 
		tb_empenho_reforco_id_empenho_reforco = @tb_empenho_reforco_id_empenho_reforco
	,	cd_item_servico = @cd_item_servico
	,	cd_unidade_fornecimento = @cd_unidade_fornecimento
	,	ds_unidade_medida = @ds_unidade_medida
	,	qt_material_servico = @qt_material_servico
	,	ds_justificativa_preco = @ds_justificativa_preco
	,	ds_item_siafem = @ds_item_siafem
	,	vr_unitario = @vr_unitario
	,	vr_preco_total = @vr_preco_total
	,   ds_status_siafisico_item = @ds_status_siafisico_item
	,	nr_sequeincia = case when isnull(@nr_sequeincia,0) = 0 then nr_sequeincia else @nr_sequeincia end
	 WHERE 
		id_item_reforco = @id_item_reforco
END