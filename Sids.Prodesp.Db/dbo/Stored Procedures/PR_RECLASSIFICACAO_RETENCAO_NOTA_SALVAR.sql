-- ===============Luis Fernando
-- Create date: 11/07/2017
-- Description: Procedure para salvar ou alterar de notas para reclassificacao_retencao
-- ===================================================================
CREATE procedure [dbo].[PR_RECLASSIFICACAO_RETENCAO_NOTA_SALVAR]
	@id_reclassificacao_retencao_nota int
,	@id_reclassificacao_retencao int
,	@cd_nota varchar(12) = null
,	@nr_ordem int = null
as
begin

	set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_reclassificacao_retencao_nota 
		where	id_reclassificacao_retencao_nota = @id_reclassificacao_retencao_nota
			and id_reclassificacao_retencao = @id_reclassificacao_retencao 
	)
	begin
	
		update contaunica.tb_reclassificacao_retencao_nota set	
				cd_nota = @cd_nota
			,	nr_ordem = @nr_ordem
		where	id_reclassificacao_retencao_nota = @id_reclassificacao_retencao_nota
			and	id_reclassificacao_retencao	= @id_reclassificacao_retencao;

		select @id_reclassificacao_retencao_nota;

	end
	else
	begin

		insert into contaunica.tb_reclassificacao_retencao_nota (
				id_reclassificacao_retencao
			,	cd_nota
			,	nr_ordem
		)
		values (
				@id_reclassificacao_retencao
			,	@cd_nota
			,	@nr_ordem
		)

		select scope_identity();

	end
	
end