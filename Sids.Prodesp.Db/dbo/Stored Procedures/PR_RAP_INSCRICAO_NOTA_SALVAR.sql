-- ===================================================================    
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017
-- Description: Procedure para salvar ou alterar notas de inscrição de restos a pagar
-- ===================================================================
CREATE procedure [dbo].[PR_RAP_INSCRICAO_NOTA_SALVAR]
	@id_rap_inscricao_nota int
,	@tb_rap_inscricao_id_rap_inscricao int
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_rap_inscricao_nota 
		where	id_rap_inscricao_nota = @id_rap_inscricao_nota
			and tb_rap_inscricao_id_rap_inscricao = @tb_rap_inscricao_id_rap_inscricao
	)
	begin
	
		update pagamento.tb_rap_inscricao_nota set	
				cd_nota = @cd_nota
			,	nr_ordem = @nr_ordem
		where	id_rap_inscricao_nota = @id_rap_inscricao_nota
			and	tb_rap_inscricao_id_rap_inscricao	= @tb_rap_inscricao_id_rap_inscricao;

		select @id_rap_inscricao_nota;

	end
	else
	begin

		insert into pagamento.tb_rap_inscricao_nota (
				tb_rap_inscricao_id_rap_inscricao
			,	cd_nota
			,	nr_ordem
		)
		values (
				@tb_rap_inscricao_id_rap_inscricao
			,	@cd_nota
			,	@nr_ordem
		)

		select scope_identity();

	end
	
end