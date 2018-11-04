-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 11/07/2017
-- Description: Procedure para salvar ou alterar de eventos para reclassificacao_retencao
-- ===================================================================
CREATE procedure [dbo].[PR_RECLASSIFICACAO_RETENCAO_EVENTO_SALVAR]
	 @cd_classificacao varchar(9) = null
	,@cd_fonte varchar(10) = null
	,@id_reclassificacao_retencao_evento int
	,@ds_inscricao varchar(22) = null
	,@cd_evento varchar(6) = null
	,@id_reclassificacao_retencao int
	,@vl_evento int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_reclassificacao_retencao_evento
		where	id_reclassificacao_retencao_evento = @id_reclassificacao_retencao_evento
			   and   id_reclassificacao_retencao = @id_reclassificacao_retencao  
			
	)
	begin
	
		update contaunica.tb_reclassificacao_retencao_evento set 
				cd_fonte = @cd_fonte
			,	cd_evento = @cd_evento
			,	cd_classificacao = @cd_classificacao
			,	ds_inscricao = @ds_inscricao
			,	vl_evento = @vl_evento
		where	id_reclassificacao_retencao_evento = @id_reclassificacao_retencao_evento

		select @id_reclassificacao_retencao_evento;

	end
	else
	begin

		insert into contaunica.tb_reclassificacao_retencao_evento (
				id_reclassificacao_retencao
			,	cd_fonte
			,	cd_evento
			,	cd_classificacao
			,	ds_inscricao
			,	vl_evento
		)
		values (
				@id_reclassificacao_retencao
			,	@cd_fonte
			,	@cd_evento
			,	@cd_classificacao
			,	@ds_inscricao
			,	@vl_evento
		)			
           
		select scope_identity();

	end
	
end