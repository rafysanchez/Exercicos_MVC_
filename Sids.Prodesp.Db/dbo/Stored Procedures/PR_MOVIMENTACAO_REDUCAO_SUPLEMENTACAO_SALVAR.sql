-- ===================================================================      
-- Author:  Alessandro de Santanao  
-- Create date: 31/07/2018  
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria  
-- ===================================================================    
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR]   
  
            @id_reducao_suplementacao INT =NULL,  
           @tb_credito_movimentacao_id_nota_credito INT =NULL,  
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao INT =NULL,  
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao INT =NULL,  
           @tb_programa_id_programa INT =NULL,  
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria INT =NULL,  
           @nr_agrupamento INT =NULL,  
           @nr_seq INT =NULL,  
           @nr_suplementacao_reducao varchar(15) =NULL,  
           @fl_proc varchar(10) =NULL,  
           @nr_processo varchar(60) =NULL,  
           @nr_orgao varchar(15) =NULL,  
           @nr_obra varchar(10) =NULL,  
           @flag_red_sup char(1)= NULL,  
           @nr_cnpj_cpf_ug_credor varchar(15)= NULL,  
           @ds_autorizado_supra_folha varchar(4) =NULL,  
           @cd_origem_recurso varchar(10) =NULL,  
           @cd_destino_recurso varchar(10) =NULL,  
           @cd_especificacao_despesa varchar(10) =NULL,  
           @ds_especificacao_despesa varchar(632) =NULL,  
           @cd_autorizado_assinatura varchar(5)= NULL,  
           @cd_autorizado_grupo int =NULL,  
           @cd_autorizado_orgao varchar(2)= NULL,  
           @ds_autorizado_cargo varchar(55) =NULL,  
           @nm_autorizado_assinatura varchar(55)= NULL,  
           @cd_examinado_assinatura varchar(5) =NULL,  
           @cd_examinado_grupo int =NULL,  
           @cd_examinado_orgao varchar(2) =NULL,  
           @ds_examinado_cargo varchar(55)= NULL,  
           @nm_examinado_assinatura varchar(55) =NULL,  
           @cd_responsavel_assinatura varchar(5)= NULL,  
           @cd_responsavel_grupo int =NULL,  
           @cd_responsavel_orgao varchar(2)= NULL,  
           @ds_responsavel_cargo varchar(140)= NULL,  
           @nm_responsavel_assinatura varchar(55)= NULL,  
           @fg_transmitido_prodesp char(1) =NULL,  
           @ds_msgRetornoProdesp varchar(140)= NULL,  
           @fg_transmitido_siafem char(1)= NULL,  
           @ds_msgRetornoSiafem varchar(140)= NULL,  
			 @valor decimal(18,2) = NULL,  
			 @cd_unidade_gestora varchar(10)= NULL ,
			 @cd_gestao_favorecido varchar(10) =NULL ,
			 @TotalQ1 numeric = null,
			 @TotalQ2 numeric = null,
			 @TotalQ3 numeric = null,
			 @TotalQ4 numeric = null
	
  
as  
begin  
  
 set nocount on;  
  
 if exists (  
  select 1   
  from [movimentacao].[tb_reducao_suplementacao] (nolock)  
  where id_reducao_suplementacao = @id_reducao_suplementacao  
 )  
 begin  
  
 UPDATE [movimentacao].[tb_reducao_suplementacao]  
   SET [tb_credito_movimentacao_id_nota_credito] = @tb_credito_movimentacao_id_nota_credito  
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao  
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao  
      ,[tb_programa_id_programa] = @tb_programa_id_programa  
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria  
      ,[nr_agrupamento] = @nr_agrupamento  
      ,[nr_seq] = @nr_seq  
      ,[nr_suplementacao_reducao] = @nr_suplementacao_reducao  
      ,[fl_proc] = @fl_proc  
      ,[nr_processo] = @nr_processo  
      ,[nr_orgao] = @nr_orgao  
      ,[nr_obra] = @nr_obra  
      ,[flag_red_sup] = @flag_red_sup  
      ,[nr_cnpj_cpf_ug_credor] = @nr_orgao  
      ,[ds_autorizado_supra_folha] = @ds_autorizado_supra_folha  
      ,[cd_origem_recurso] = @cd_origem_recurso  
      ,[cd_destino_recurso] = @cd_destino_recurso  
      ,[cd_especificacao_despesa] = @cd_especificacao_despesa  
      ,[ds_especificacao_despesa] = @ds_especificacao_despesa  
      ,[cd_autorizado_assinatura] = @cd_autorizado_assinatura  
      ,[cd_autorizado_grupo] = @cd_autorizado_grupo  
      ,[cd_autorizado_orgao] = @cd_autorizado_orgao  
      ,[ds_autorizado_cargo] = @ds_autorizado_cargo  
      ,[nm_autorizado_assinatura] = @nm_autorizado_assinatura  
      ,[cd_examinado_assinatura] = @cd_examinado_assinatura  
      ,[cd_examinado_grupo] = @cd_examinado_grupo  
      ,[cd_examinado_orgao] = @cd_examinado_orgao  
      ,[ds_examinado_cargo] = @ds_examinado_cargo  
      ,[nm_examinado_assinatura] = @nm_examinado_assinatura  
      ,[cd_responsavel_assinatura] = @cd_responsavel_assinatura  
      ,[cd_responsavel_grupo] = @cd_responsavel_grupo  
      ,[cd_responsavel_orgao] = @cd_responsavel_orgao  
      ,[ds_responsavel_cargo] = @ds_responsavel_cargo  
      ,[nm_responsavel_assinatura] = @nm_responsavel_assinatura  
      ,[fg_transmitido_prodesp] = @fg_transmitido_prodesp  
      ,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp  
      ,[fg_transmitido_siafem] = @fg_transmitido_siafem  
      ,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem  
   ,[valor] = @valor  
   ,[cd_unidade_gestora] = @cd_unidade_gestora 
   ,[cd_gestao_favorecido] = @cd_gestao_favorecido 
   ,[TotalQ1] = @TotalQ1
   ,[TotalQ2] = @TotalQ2
   ,[TotalQ3] = @TotalQ3
   ,[TotalQ4] = @TotalQ4
  
      
  
        where id_reducao_suplementacao = @id_reducao_suplementacao;  
  
  select @id_reducao_suplementacao;  
  
  
   end  
 else  
 begin  
  
 INSERT INTO [movimentacao].[tb_reducao_suplementacao]  
           ([tb_credito_movimentacao_id_nota_credito]  
           ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]  
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]  
           ,[tb_programa_id_programa]  
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]  
           ,[nr_agrupamento]  
           ,[nr_seq]  
           ,[nr_suplementacao_reducao]  
           ,[fl_proc]  
           ,[nr_processo]  
           ,[nr_orgao]  
           ,[nr_obra]  
           ,[flag_red_sup]  
           ,[nr_cnpj_cpf_ug_credor]  
           ,[ds_autorizado_supra_folha]  
           ,[cd_origem_recurso]  
           ,[cd_destino_recurso]  
           ,[cd_especificacao_despesa]  
           ,[ds_especificacao_despesa]  
           ,[cd_autorizado_assinatura]  
           ,[cd_autorizado_grupo]  
           ,[cd_autorizado_orgao]  
           ,[ds_autorizado_cargo]  
           ,[nm_autorizado_assinatura]  
           ,[cd_examinado_assinatura]  
           ,[cd_examinado_grupo]  
           ,[cd_examinado_orgao]  
           ,[ds_examinado_cargo]  
           ,[nm_examinado_assinatura]  
           ,[cd_responsavel_assinatura]  
           ,[cd_responsavel_grupo]  
           ,[cd_responsavel_orgao]  
           ,[ds_responsavel_cargo]  
           ,[nm_responsavel_assinatura]  
           ,[fg_transmitido_prodesp]  
           ,[ds_msgRetornoProdesp]  
           ,[fg_transmitido_siafem]  
           ,[ds_msgRetornoSiafem]  
     ,[valor]  
     ,[cd_unidade_gestora]
	 ,[cd_gestao_favorecido]
	 ,[TotalQ1]
	 ,[TotalQ2]
	 ,[TotalQ3]
	 ,[TotalQ4])  
     VALUES  
           (@tb_credito_movimentacao_id_nota_credito  
           ,@tb_distribuicao_movimentacao_id_distribuicao_movimentacao  
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao  
           ,@tb_programa_id_programa  
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria  
           ,@nr_agrupamento  
           ,@nr_seq  
           ,@nr_suplementacao_reducao  
           ,@fl_proc  
           ,@nr_processo  
           ,@nr_orgao  
           ,@nr_obra  
           ,@flag_red_sup  
           ,@nr_orgao  
           ,@ds_autorizado_supra_folha  
           ,@cd_origem_recurso  
           ,@cd_destino_recurso  
           ,@cd_especificacao_despesa  
           ,@ds_especificacao_despesa  
           ,@cd_autorizado_assinatura  
           ,@cd_autorizado_grupo  
           ,@cd_autorizado_orgao  
           ,@ds_autorizado_cargo  
           ,@nm_autorizado_assinatura  
           ,@cd_examinado_assinatura  
           ,@cd_examinado_grupo  
           ,@cd_examinado_orgao  
           ,@ds_examinado_cargo  
           ,@nm_examinado_assinatura  
           ,@cd_responsavel_assinatura  
           ,@cd_responsavel_grupo  
           ,@cd_responsavel_orgao  
           ,@ds_responsavel_cargo  
      ,@nm_responsavel_assinatura  
           ,'N'  
           ,@ds_msgRetornoProdesp  
           ,'N'  
           ,@ds_msgRetornoSiafem  
     ,@valor  
     ,@cd_unidade_gestora
	 ,@cd_gestao_favorecido
	 ,@TotalQ1
	 ,@TotalQ2
	 ,@TotalQ3
	 ,@TotalQ4)  
  
  
  
  select scope_identity();  
  
 end  
  
end