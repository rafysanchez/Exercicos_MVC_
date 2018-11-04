-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description: Procedure para salvar ou alterar de eventos para programacao desembolso
-- ===================================================================
CREATE procedure [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EVENTO_SALVAR]
	 @cd_classificacao varchar(9) = null
	,@cd_fonte varchar(10) = null
	,@id_programacao_desembolso_evento int
	,@ds_inscricao varchar(22) = null
	,@cd_evento varchar(6) = null
	,@id_programacao_desembolso int
	,@vl_evento int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_evento
		where	id_programacao_desembolso_evento = @id_programacao_desembolso_evento
			   and   id_programacao_desembolso = @id_programacao_desembolso  
			
	)
	begin
	
		update contaunica.tb_programacao_desembolso_evento set 
				cd_fonte = @cd_fonte
			,	cd_evento = @cd_evento
			,	cd_classificacao = @cd_classificacao
			,	ds_inscricao = @ds_inscricao
			,	vl_evento = @vl_evento
		where	id_programacao_desembolso_evento = @id_programacao_desembolso_evento

		select @id_programacao_desembolso_evento;

	end
	else
	begin

		insert into contaunica.tb_programacao_desembolso_evento (
				id_programacao_desembolso
			,	cd_fonte
			,	cd_evento
			,	cd_classificacao
			,	ds_inscricao
			,	vl_evento
		)
		values (
				@id_programacao_desembolso
			,	@cd_fonte
			,	@cd_evento
			,	@cd_classificacao
			,	@ds_inscricao
			,	@vl_evento
		)			
           
		select scope_identity();

	end
	
end