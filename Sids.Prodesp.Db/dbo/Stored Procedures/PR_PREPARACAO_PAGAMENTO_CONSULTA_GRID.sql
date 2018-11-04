-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 08/08/2017
-- Description: Procedure para consultar id_preparacao pagamento grid
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PREPARACAO_PAGAMENTO_CONSULTA_GRID]
	@id_preparacao_pagamento int = null
	,@nr_op_inicial varchar(18) = NULL
	,@id_tipo_preparacao_pagamento int = null
	,@ds_status_prodesp char(1) = NULL
	,@dt_cadastramento_de date = NULL
	,@dt_cadastramento_ate date = NULL
	,@id_regional smallint = null
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
		   [id_preparacao_pagamento]
      ,[id_regional]
      ,[id_tipo_documento]
      ,[id_tipo_preparacao_pagamento]
      ,[nr_op_inicial]
      ,[nr_op_final]
      ,[nr_ano_exercicio]
      ,[cd_credor_organizacao]
      ,[nr_cpf_cnpj_credor]
      ,[cd_assinatura]
      ,[cd_grupo_assinatura]
      ,[cd_orgao_assinatura]
      ,[ds_cargo_assinatura]
      ,[nm_assinatura]
      ,[cd_contra_assinatura]
      ,[cd_grupo_contra_assinatura]
      ,[cd_orgao_contra_assinatura]
      ,[nm_contra_assinatura]
      ,[ds_cargo_contra_assinatura]
      ,[nr_documento]
      ,[vr_documento]
      ,[cd_conta]
      ,[nr_banco]
      ,[nr_agencia]
      ,[nr_conta]
      ,[cd_despesa]
      ,[dt_vencimento]
      ,[ds_referencia]
      ,[ds_despesa_credor]
      ,[nr_contrato]
      ,[ds_credor1]
      ,[ds_credor2]
      ,[ds_endereco]
      ,[nr_cep]
      ,[nr_banco_credor]
      ,[nr_agencia_credor]
      ,[nr_conta_credor]
      ,[nr_banco_pgto]
      ,[nr_agencia_pgto]
      ,[nr_conta_pgto]
      ,[dt_emissao]
      ,[qt_op_preparada]
      ,[vr_total]
      ,[bl_transmitir_prodesp]
      ,[bl_transmitido_prodesp]
      ,[ds_status_prodesp]
      ,[ds_transmissao_mensagem_prodesp]
      ,[dt_transmitido_prodesp]
      ,[ds_status_documento]
      ,[bl_cadastro_completo]
      ,[dt_cadastro]
  FROM [contaunica].[tb_preparacao_pagamento] (nolock)
	where
		( nullif( @id_preparacao_pagamento, 0 ) is null or id_preparacao_pagamento = @id_preparacao_pagamento )
        --And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
        And ( nullif( @id_tipo_preparacao_pagamento, 0 ) is null or id_tipo_preparacao_pagamento = @id_tipo_preparacao_pagamento )

		and (
			(@nr_op_inicial is null or nr_op_inicial like '%'+@nr_op_inicial+'%')
			or (@nr_op_inicial is null or nr_op_final like '%'+@nr_op_inicial+'%')
		 )

		and (@ds_status_prodesp  is null or ds_status_prodesp  = @ds_status_prodesp  )

		and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de ) 
		and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate ) 


	Order by id_preparacao_pagamento
end;