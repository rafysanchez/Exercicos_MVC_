  
  
-- ===================================================================    
-- Author:  Alessandro de Santana  
-- Create date: 22/06/2018  
-- Description: Procedure para consultar arquivo de remessa grid  
-- ===================================================================   
CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID]  
  @id_arquivo_remessa int = null  
    ,@nr_codigo_conta int = null  
 ,@nr_geracao_arquivo INT = NULL  
 ,@id_regional int = null  
 ,@dt_cadastramento_de date = NULL  
 ,@dt_cadastramento_ate date = NULL  
 ,@fg_trasmitido_prodesp char(1) = null  
 ,@fg_arquivo_cancelado bit = null  
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
  FROM [contaunica].[tb_arquivo_remessa] (nolock)  
 where  
  
      ( nullif( @id_arquivo_remessa, 0 ) is null or id_arquivo_remessa = @id_arquivo_remessa )    
        and ( nullif( @nr_codigo_conta, 0 ) is null or cd_conta = @nr_codigo_conta )   
        and ( nullif( @nr_geracao_arquivo, 0 ) is null or nr_geracao_arquivo = @nr_geracao_arquivo )  
     and ( nullif( @id_regional, 0 ) is null or id_regional = @id_regional )  
  and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de )   
  and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate )   
  and (@fg_trasmitido_prodesp  is null or fg_trasmitido_prodesp  = @fg_trasmitido_prodesp  )  
  and (@fg_arquivo_cancelado  is null or fg_arquivo_cancelado  = @fg_arquivo_cancelado  )  
  
  
 Order by id_arquivo_remessa  
end