    
    
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR]    
	@id_reducao_suplementacao int =NULL,    
	@tb_credito_movimentacao_id_nota_credito int =NULL,    
	@tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,    
	@tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,    
	@tb_programa_id_programa int =NULL,    
	@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
	@nr_agrupamento int =NULL,    
	@nr_seq int =NULL,    
	@nr_suplementacao_reducao varchar(10) =NULL,    
	@fl_proc varchar(10) =NULL,    
	@nr_processo varchar(60) =NULL,    
	@nr_orgao varchar(10) =NULL,    
	@nr_obra varchar(15) =NULL,    
	@flag_red_sup char(1) =NULL,    
	@nr_cnpj_cpf_ug_credor varchar(15) =NULL,    
	@ds_autorizado_supra_folha varchar(10) =NULL,    
	@cd_origem_recurso varchar(10) =NULL,    
	@cd_destino_recurso varchar(10) =NULL,    
	@cd_especificacao_despesa varchar(10) =NULL,    
	@fg_transmitido_prodesp char(1) =NULL,    
	@fg_transmitido_siafem char(1) =NULL
		       
AS        
BEGIN        
 SET NOCOUNT ON;      
    
    
SELECT 
	[id_reducao_suplementacao]
	,[tb_credito_movimentacao_id_nota_credito]
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
	,[cd_unidade_gestora]
	,[cd_gestao_favorecido]
	,[valor]
	,[TotalQ1]  
	,[TotalQ2]  
	,[TotalQ3]  
	,[TotalQ4]  
  FROM [movimentacao].[tb_reducao_suplementacao]
    
  WHERE         
	( nullif( @id_reducao_suplementacao, 0 ) is null or id_reducao_suplementacao = @id_reducao_suplementacao )   and     
	( nullif( @tb_credito_movimentacao_id_nota_credito, 0 ) is null or tb_credito_movimentacao_id_nota_credito = @tb_credito_movimentacao_id_nota_credito )   and     
	( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and     
	( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and     
	( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa )   and     
	( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0) is null or  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria ) 	and     
	( nullif( @nr_agrupamento, 0) is null or nr_agrupamento  = @nr_agrupamento )   and     
	( nullif( @nr_seq, 0) is null or nr_seq = @nr_seq )   and     
  
  (@nr_suplementacao_reducao is null or nr_suplementacao_reducao = @nr_suplementacao_reducao  ) and    
  (@fl_proc is null or fl_proc = @fl_proc  ) and    
  (@nr_processo is null or nr_processo = @nr_processo  ) and    
  (@nr_orgao is null or nr_processo = @nr_orgao  ) and    
  (@nr_obra is null or nr_obra = @nr_obra  ) and    
  (@flag_red_sup is null or flag_red_sup = @flag_red_sup  ) and    
  (@nr_cnpj_cpf_ug_credor is null or nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor  ) and    
  (@ds_autorizado_supra_folha is null or ds_autorizado_supra_folha = @ds_autorizado_supra_folha  ) and    
    
  (@cd_origem_recurso is null or cd_origem_recurso = @cd_origem_recurso  ) and    
  (@cd_destino_recurso is null or cd_destino_recurso = @cd_destino_recurso  ) and    
  (@cd_especificacao_despesa is null or cd_especificacao_despesa = @cd_especificacao_despesa  ) and    
  (@fg_transmitido_prodesp is null or fg_transmitido_prodesp = @fg_transmitido_prodesp  ) and    
  (@fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem  )     
     
  ORDER BY id_reducao_suplementacao,nr_seq    
    
    
  END