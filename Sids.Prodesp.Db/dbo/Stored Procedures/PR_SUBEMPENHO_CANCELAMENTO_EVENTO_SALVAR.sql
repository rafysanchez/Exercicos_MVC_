-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description: Procedure para salvar ou alterar de eventos para subempenho
-- ===================================================================
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_EVENTO_SALVAR]
	@id_subempenho_cancelamento_evento int
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int
,	@cd_fonte varchar(10) = null
,	@cd_evento varchar(6) = null
,	@cd_classificacao varchar(9) = null
,	@ds_inscricao varchar(22) = null
,	@vl_evento int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	pagamento.tb_subempenho_cancelamento_evento
		where	id_subempenho_cancelamento_evento = @id_subempenho_cancelamento_evento
			and tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento
	)
	begin
	
		update pagamento.tb_subempenho_cancelamento_evento set 
				cd_fonte = @cd_fonte
			,	cd_evento = @cd_evento
			,	cd_classificacao = @cd_classificacao
			,	ds_inscricao = @ds_inscricao
			,	vl_evento = @vl_evento
		where	id_subempenho_cancelamento_evento = @id_subempenho_cancelamento_evento

		select @id_subempenho_cancelamento_evento;

	end
	else
	begin

		insert into pagamento.tb_subempenho_cancelamento_evento (
				tb_subempenho_cancelamento_id_subempenho_cancelamento
			,	cd_fonte
			,	cd_evento
			,	cd_classificacao
			,	ds_inscricao
			,	vl_evento
		)
		values (
				@tb_subempenho_cancelamento_id_subempenho_cancelamento
			,	@cd_fonte
			,	@cd_evento
			,	@cd_classificacao
			,	@ds_inscricao
			,	@vl_evento
		)			
           
		select scope_identity();

	end
	
end