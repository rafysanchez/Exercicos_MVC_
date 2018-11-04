-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para salvar ou alterar de notas para subempenho
-- ===================================================================
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_NOTA_SALVAR]
	@id_subempenho_cancelamento_nota int
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_subempenho_cancelamento_nota 
		where	id_subempenho_cancelamento_nota = @id_subempenho_cancelamento_nota
			and tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento 
	)
	begin
	
		update pagamento.tb_subempenho_cancelamento_nota set	
				cd_nota = @cd_nota
			,	nr_ordem = @nr_ordem
		where	id_subempenho_cancelamento_nota = @id_subempenho_cancelamento_nota
			and	tb_subempenho_cancelamento_id_subempenho_cancelamento	= @tb_subempenho_cancelamento_id_subempenho_cancelamento;

		select @id_subempenho_cancelamento_nota;

	end
	else
	begin

		insert into pagamento.tb_subempenho_cancelamento_nota (
				tb_subempenho_cancelamento_id_subempenho_cancelamento
			,	cd_nota
			,	nr_ordem
		)
		values (
				@tb_subempenho_cancelamento_id_subempenho_cancelamento
			,	@cd_nota
			,	@nr_ordem
		)

		select scope_identity();

	end
	
end