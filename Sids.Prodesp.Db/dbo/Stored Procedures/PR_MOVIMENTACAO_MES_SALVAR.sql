-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria
-- ===================================================================  
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_SALVAR] 
           @id_mes int =NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_reducao_suplementacao_id_reducao_suplementacao int =NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int= NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @ds_mes varchar(9) =NULL,
           @vr_mes decimal(18,2) = null,
		   @cd_unidade_gestora varchar(10) = null

as
begin

	set nocount on;

	if exists (
		select	1 
		from	[movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)
		where	id_mes = @id_mes
	)
	begin

	UPDATE [movimentacao].[tb_movimentacao_orcamentaria_mes]
   SET [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao
      ,[tb_reducao_suplementacao_id_reducao_suplementacao] = @tb_reducao_suplementacao_id_reducao_suplementacao
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
      ,[nr_agrupamento] = @nr_agrupamento
      ,[nr_seq] = @nr_seq
      ,[ds_mes] = @ds_mes
      ,[vr_mes] = @vr_mes
	  ,[cd_unidade_gestora] = @cd_unidade_gestora

	   

	     		where	id_mes = @id_mes;

		select @id_mes;


			end
	else
	begin

	INSERT INTO [movimentacao].[tb_movimentacao_orcamentaria_mes]
           ([tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
           ,[tb_reducao_suplementacao_id_reducao_suplementacao]
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
           ,[nr_agrupamento]
           ,[nr_seq]
           ,[ds_mes]
           ,[vr_mes]
		   ,[cd_unidade_gestora])
     VALUES
           (@tb_distribuicao_movimentacao_id_distribuicao_movimentacao
           ,@tb_reducao_suplementacao_id_reducao_suplementacao
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
           ,@nr_agrupamento
           ,@nr_seq
           ,@ds_mes
           ,@vr_mes
		   ,@cd_unidade_gestora)



		select scope_identity();

	end

end