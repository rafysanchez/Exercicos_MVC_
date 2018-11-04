CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT]        
(    
  @id_tipo_documento int  = NULL,         
  @nr_documento varchar(30)  = NULL,        
  @nr_conta varchar(3) = NULL,         
  @dt_preparacao varchar(20)   = NULL,    
  @dt_cadastro datetime = NULL,        
  @id_tipo_pagamento int  = NULL,         
  @nr_orgao varchar(50) = NULL,        
  @id_despesa_tipo varchar(50) = null,        
  @nr_contrato int = null,        
  @cd_obra int = null,        
  @id_origem int = null,        
  @nm_reduzido_credor  varchar(14) = null,          
  @cd_cpf_cnpj  varchar(14) = null,          
  @dt_confirmacao varchar(20)   = NULL,          
  @cd_transmissao_status_prodesp varchar(50) = null,        
  --@dt_cadastro varchar(20) = NULL,    
  @de date = null,    
  @ate date = null        
 )        
AS          
BEGIN        
	SELECT       
     cpi.cd_orgao_assinatura as Orgao,
	 cpi.id_despesa_tipo as DespesaTipo,
	 cpi.nr_documento as NumeroDocumento,
	 cpi.nm_reduzido_credor as NomeReduzidoCredor,
	 cpi.nr_cnpj_cpf_ug_credor as CPF_CNPJ_Credor,
	 cpi.vr_documento as ValorDocumentoDecimal,
	 cpi.id_origem as Origem,
	 cpi.cd_transmissao_status_prodesp as StatusProdesp,
	 cpi.nr_nl_documento as NumeroNLBaixaRepasse,
	 cp.id_execucao_pd,
	 cp.dt_confirmacao as DataConfirmacao,
	 cp.nr_agrupamento as CodigoAgrupamentoConfirmacaoPagamento
   FROM [pagamento].[tb_confirmacao_pagamento_item] cpi (nolock)        
   left join pagamento.tb_confirmacao_pagamento cp (NOLOCK)
   on cp.id_confirmacao_pagamento = cpi.id_confirmacao_pagamento 
   Where 1=1 AND
	(NULLIF(@id_tipo_documento, 0) IS NULL OR cpi.id_tipo_documento = @id_tipo_documento )  AND  
	(NULLIF(@nr_documento, '') IS NULL OR cpi.nr_documento = @nr_documento )  AND  
	(NULLIF(@nr_conta, '') IS NULL OR cpi.nr_conta_pagador = @nr_conta )  AND  
	(@dt_preparacao IS NULL OR cp.dt_preparacao = @dt_preparacao )  AND  
	(@de is null or cp.dt_cadastro >= @de )AND  
	(@ate is null or cp.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00')) AND  
	(NULLIF(@id_tipo_pagamento, 0) IS NULL OR cp.id_tipo_pagamento = @id_tipo_pagamento )  AND  
	(NULLIF(@id_despesa_tipo, '') IS NULL OR cpi.id_despesa_tipo = @id_despesa_tipo )  AND  
	(NULLIF(@nr_contrato, '') IS NULL OR cpi.nr_contrato = @nr_contrato )  AND  
	(@id_origem IS NULL OR cpi.id_origem = @id_origem )  AND  
	(NULLIF(@cd_cpf_cnpj, '') IS NULL OR cpi.nr_cnpj_cpf_ug_credor = @cd_cpf_cnpj )  AND  
	(NULLIF(@cd_transmissao_status_prodesp, '') IS NULL OR cpi.cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp) AND  
	(NULLIF(@nr_orgao, '') IS NULL OR cpi.cd_orgao_assinatura = @nr_orgao) AND  
	(NULLIF(@nm_reduzido_credor, '') IS NULL OR cpi.nm_reduzido_credor = @nm_reduzido_credor)     
     
End