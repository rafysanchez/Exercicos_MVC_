-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para alteração de itens para subempenho
-- ===================================================================    
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_ITEM_ALTERAR]      
	@id_subempenho_item int
,	@tb_subempenho_id_subempenho int = NULL
,	@cd_servico varchar(10) = NULL
,	@cd_unidade_fornecimento varchar(5) = NULL
,	@qt_material_servico decimal(12,3) = NULL
,	@cd_status_siafisico char(1) = NULL
,	@nr_sequencia int = NULL
,	@trasmitir bit = NULL
,	@vl_valor decimal(18,2) = null
AS  
BEGIN    

	SET NOCOUNT ON;    
   
	UPDATE pagamento.tb_subempenho_item
	SET 
		tb_subempenho_id_subempenho = nullif( @tb_subempenho_id_subempenho, 0 )
	,	cd_servico = @cd_servico
	,	cd_unidade_fornecimento = @cd_unidade_fornecimento
	,	qt_material_servico = @qt_material_servico
	,	cd_status_siafisico = @cd_status_siafisico
	,	nr_sequencia = case when isnull( @nr_sequencia, 0 ) = 0 then nr_sequencia else @nr_sequencia end
	,	transmitir = @trasmitir
	,	vl_valor = @vl_valor
	WHERE 
		id_subempenho_item = @id_subempenho_item
END