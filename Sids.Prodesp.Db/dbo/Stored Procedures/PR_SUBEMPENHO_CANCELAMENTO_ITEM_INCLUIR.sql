-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para inclusão de itens para subempenho
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_ITEM_INCLUIR]    
	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = NULL
,	@cd_subempenho_item_servico varchar(6) = NULL
,	@cd_unidade_fornecimento varchar(5) = NULL
,	@qt_material_servico decimal(12,3) = NULL
,	@cd_status_siafisico char(1) = NULL
,	@nr_sequencia int = NULL
,	@transmitir bit  = NULL
AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO pagamento.tb_subempenho_cancelamento_item (
			tb_subempenho_cancelamento_id_subempenho_cancelamento
		,	cd_servico
		,	cd_unidade_fornecimento
		,	qt_material_servico
		,	cd_status_siafisico
		,	nr_sequencia
		,	transmitir
		)
		VALUES (
			@tb_subempenho_cancelamento_id_subempenho_cancelamento
		,	@cd_subempenho_item_servico
		,	@cd_unidade_fornecimento
		,	@qt_material_servico
		,	@cd_status_siafisico
		,	@nr_sequencia
		,	@transmitir
		)		
           
	COMMIT
    
    SELECT SCOPE_IDENTITY();
END