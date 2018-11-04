-- ===================================================================    
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017
-- Description: Procedure para salvar ou alterar notas de anulação de restos a pagar
-- ===================================================================
CREATE procedure [dbo].[PR_RAP_ANULACAO_NOTA_SALVAR]
	@id_rap_anulacao_nota int
,	@tb_rap_anulacao_id_rap_anulacao int
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_rap_anulacao_nota 
		where	id_rap_anulacao_nota = @id_rap_anulacao_nota
			and tb_rap_anulacao_id_rap_anulacao = @tb_rap_anulacao_id_rap_anulacao
	)
	begin
	
		update pagamento.tb_rap_anulacao_nota set	
				cd_nota = @cd_nota
			,	nr_ordem = @nr_ordem
		where	id_rap_anulacao_nota = @id_rap_anulacao_nota
			and	tb_rap_anulacao_id_rap_anulacao	= @tb_rap_anulacao_id_rap_anulacao;

		select @id_rap_anulacao_nota;

	end
	else
	begin

		insert into pagamento.tb_rap_anulacao_nota (
				tb_rap_anulacao_id_rap_anulacao
			,	cd_nota
			,	nr_ordem
		)
		values (
				@tb_rap_anulacao_id_rap_anulacao
			,	@cd_nota
			,	@nr_ordem
		)

		select scope_identity();

	end
	
end