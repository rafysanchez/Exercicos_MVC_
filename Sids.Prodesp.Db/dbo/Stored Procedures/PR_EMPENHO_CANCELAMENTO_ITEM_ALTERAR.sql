
-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para alteração de valores de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_ITEM_ALTERAR]      
	@id_item  int = null
,	@tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO int = null
,	@cd_item_servico int = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@ds_unidade_medida varchar(4) = null
,	@qt_material_servico decimal(18,2) = null
,	@ds_justificativa_preco varchar(142) = null
,	@ds_item_siafem varchar(753) = null
,	@vr_unitario decimal(18,0) = null
,	@vr_preco_total decimal(18,0) = null
,	@ds_status_siafisico_item varchar(1) = null
,	@nr_sequeincia int = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_cancelamento_item
	SET 
		tb_empenho_cancelamento_id_empenho_cancelamento = @tb_empenho_CANCELAMENTO_id_empenho_CANCELAMENTO
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
		id_item = @id_item
END