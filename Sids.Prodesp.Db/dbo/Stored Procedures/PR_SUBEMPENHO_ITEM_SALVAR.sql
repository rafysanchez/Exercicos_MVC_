-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description: Procedure para salvar ou alterar de itens para subempenho
-- ===================================================================
CREATE procedure [dbo].[PR_SUBEMPENHO_ITEM_SALVAR]
	@id_subempenho_item int
,	@tb_subempenho_id_subempenho int
,	@cd_servico varchar(10) = null
,	@cd_unidade_fornecimento varchar(5) = null
,	@qt_material_servico decimal(12,3) = null
,	@cd_status_siafisico char(1) = null
,	@nr_sequencia int = null
,	@transmitir bit = null
,	@vl_valor decimal(18,2) = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_subempenho_item
		where	id_subempenho_item = @id_subempenho_item
			--and tb_subempenho_id_subempenho = @tb_subempenho_id_subempenho 
	)
	begin
	
		update pagamento.tb_subempenho_item set 
				cd_servico = @cd_servico
			,	cd_unidade_fornecimento = @cd_unidade_fornecimento
			,	qt_material_servico = @qt_material_servico
			,	cd_status_siafisico = @cd_status_siafisico
			,	nr_sequencia = @nr_sequencia
			,	transmitir = @transmitir
			,	vl_valor = @vl_valor
		where	id_subempenho_item = @id_subempenho_item
			and tb_subempenho_id_subempenho = @tb_subempenho_id_subempenho 

		select @id_subempenho_item;

	end
	else
	begin

		insert into pagamento.tb_subempenho_item (
				tb_subempenho_id_subempenho
			,	cd_servico
			,	cd_unidade_fornecimento
			,	qt_material_servico
			,	cd_status_siafisico
			,	nr_sequencia
			,	transmitir
			,	vl_valor
		)
		values (
				@tb_subempenho_id_subempenho
			,	@cd_servico
			,	@cd_unidade_fornecimento
			,	@qt_material_servico
			,	@cd_status_siafisico
			,	@nr_sequencia
			,	@transmitir
			,	@vl_valor
		)		
           
		select scope_identity();

	end
	
end