
CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_ITEM_PRODESP]        
     
  @nr_documento_gerador varchar(30)  = NULL,        
  @cd_transmissao_status_prodesp varchar(1) = NULL,
  @fl_transmissao_transmitido_prodesp bit = NULL,
  @dt_transmissao_transmitido_prodesp datetime = NULL,
  @ds_transmissao_mensagem_prodesp varchar(140) = NULL,
  @id_confirmacao_pagamento int = NULL    
       
AS          
BEGIN        
	UPDATE [pagamento].[tb_confirmacao_pagamento_item]
	SET cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,      
     fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,      
     dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp,    
     ds_transmissao_mensagem_prodesp =  @ds_transmissao_mensagem_prodesp   
	WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento AND
    LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
End