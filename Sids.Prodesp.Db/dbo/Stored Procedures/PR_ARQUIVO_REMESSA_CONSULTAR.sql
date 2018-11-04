CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTAR]  
 @id_arquivo_remessa int = 0
as    
begin    
    
 SET NOCOUNT ON;    

SELECT [id_arquivo_remessa]
      ,[tb_arquivo_id_arquivo]
      ,[nr_geracao_arquivo]
      ,[dt_preparacao_pagamento]
      ,[dt_pagamento]
      ,[cd_assinatura]
      ,[cd_grupo_assinatura]
      ,[cd_orgao_assinatura]
      ,[nm_assinatura]
      ,[ds_cargo]
      ,[cd_contra_assinatura]
      ,[cd_grupo_contra_assinatura]
      ,[cd_orgao_contra_assinatura]
      ,[nm_contra_assinatura]
      ,[ds_cargo_contra_assinatura]
      ,[cd_conta]
      ,[ds_banco]
      ,[ds_agencia]
      ,[ds_conta]
      ,[qt_ordem_pagamento_arquivo]
      ,[qt_deposito_arquivo]
      ,[vr_total_pago]
      ,[qt_doc_ted_arquivo]
      ,[dt_cadastro]
      ,[fg_trasmitido_prodesp]
      ,[dt_trasmitido_prodesp]
      ,[fg_arquivo_cancelado]
	  ,[bl_cadastro_completo]
	  ,[ds_msg_retorno]
	  ,[bl_transmitir_prodesp]
	  ,[bl_transmitido_prodesp]
  FROM [contaunica].[tb_arquivo_remessa]  (nolock) 
   where  
  ( nullif( @id_arquivo_remessa, 0 ) is null or id_arquivo_remessa = @id_arquivo_remessa )  
 
 Order by id_arquivo_remessa  


  END