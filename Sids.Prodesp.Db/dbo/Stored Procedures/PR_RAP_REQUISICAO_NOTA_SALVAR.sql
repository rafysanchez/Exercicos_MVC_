-- ===================================================================    
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017
-- Description: Procedure para salvar ou alterar notas de requisição de restos a pagar
-- ===================================================================
CREATE procedure [dbo].[PR_RAP_REQUISICAO_NOTA_SALVAR]
	@id_rap_requisicao_nota int
,	@tb_rap_requisicao_id_rap_requisicao int
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_rap_requisicao_nota 
		where	id_rap_requisicao_nota = @id_rap_requisicao_nota
			and tb_rap_requisicao_id_rap_requisicao = @tb_rap_requisicao_id_rap_requisicao
	)
	begin
	
		update pagamento.tb_rap_requisicao_nota set	
				cd_nota = @cd_nota
			,	nr_ordem = @nr_ordem
		where	id_rap_requisicao_nota = @id_rap_requisicao_nota
			and	tb_rap_requisicao_id_rap_requisicao	= @tb_rap_requisicao_id_rap_requisicao;

		select @id_rap_requisicao_nota;

	end
	else
	begin

		insert into pagamento.tb_rap_requisicao_nota (
				tb_rap_requisicao_id_rap_requisicao
			,	cd_nota
			,	nr_ordem
		)
		values (
				@tb_rap_requisicao_id_rap_requisicao
			,	@cd_nota
			,	@nr_ordem
		)

		select scope_identity();

	end
	
end