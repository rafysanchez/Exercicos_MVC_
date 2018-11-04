-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria
-- ===================================================================  
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR] 
            @id_evento int =NULL,
            @cd_evento int =NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @cd_inscricao_evento varchar(10) =NULL,
           @cd_classificacao int =NULL,
           @cd_fonte varchar(15) =NULL,
           @rec_despesa varchar(10) =NULL,
           @vr_evento int =NULL

as
begin

	set nocount on;

	if exists (
		select	1 
		from	movimentacao.tb_movimentacao_orcamentaria_evento (nolock)
		where	id_evento  = @id_evento
	)
	begin


	UPDATE [movimentacao].[tb_movimentacao_orcamentaria_evento]
   SET [cd_evento] = @cd_evento
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
      ,[nr_agrupamento] = @nr_agrupamento
      ,[nr_seq] = @nr_seq
      ,[cd_inscricao_evento] = @cd_inscricao_evento
      ,[cd_classificacao] = @cd_classificacao
      ,[cd_fonte] = @cd_fonte
      ,[rec_despesa] = @rec_despesa
      ,[vr_evento] = @vr_evento
	

	   

	     		where	id_evento = @id_evento;

		select @id_evento;


			end
	else
	begin

	INSERT INTO [movimentacao].[tb_movimentacao_orcamentaria_evento]
           ([cd_evento]
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
           ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
           ,[nr_agrupamento]
           ,[nr_seq]
           ,[cd_inscricao_evento]
           ,[cd_classificacao]
           ,[cd_fonte]
           ,[rec_despesa]
           ,[vr_evento])
     VALUES
           (@cd_evento
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao
           ,@tb_distribuicao_movimentacao_id_distribuicao_movimentacao
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
           ,@nr_agrupamento
           ,@nr_seq
           ,@cd_inscricao_evento
           ,@cd_classificacao
           ,@cd_fonte
           ,@rec_despesa
           ,@vr_evento)


		select scope_identity();

	end

end