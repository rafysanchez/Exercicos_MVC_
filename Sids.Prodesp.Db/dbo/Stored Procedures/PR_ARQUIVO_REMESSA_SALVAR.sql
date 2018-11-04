-- ===================================================================    
-- Author:		Alessandro de Santana
-- Create date: 29/06/2018
-- Description: Procedure para salvar ou alterar arquivo remessa
-- ===================================================================  
CREATE procedure [dbo].[PR_ARQUIVO_REMESSA_SALVAR] 

		@id_arquivo_remessa INTEGER  = NULL,
		@tb_arquivo_id_arquivo INTEGER  = NULL,
		@nr_geracao_arquivo INTEGER  = NULL,
		@dt_preparacao_pagamento DATE = NULL,
		@dt_pagamento DATE = NULL,
		@cd_assinatura  VARCHAR(5)  = NULL,
		@cd_grupo_assinatura VARCHAR(1)  = NULL,
		@cd_orgao_assinatura VARCHAR(2)  = NULL,
		@nm_assinatura VARCHAR(55) = NULL,
		@ds_cargo VARCHAR(55) = NULL,
		@cd_contra_assinatura VARCHAR(5)  = NULL,
		@cd_grupo_contra_assinatura VARCHAR(2)  = NULL,
		@cd_orgao_contra_assinatura VARCHAR(2)  = NULL,
		@nm_contra_assinatura VARCHAR(55) = NULL,
		@ds_cargo_contra_assinatura VARCHAR(55) = NULL,
		@cd_conta INTEGER  = NULL,
		@ds_banco VARCHAR(50) = NULL,
		@ds_agencia VARCHAR(50) = NULL,
		@ds_conta VARCHAR(50) = NULL,
		@qt_ordem_pagamento_arquivo INTEGER  = NULL,
		@qt_deposito_arquivo INTEGER  = NULL,
		@vr_total_pago INTEGER  = NULL,
		@qt_doc_ted_arquivo INTEGER  = NULL,
		@dt_cadastro DATE = NULL,
		@fg_trasmitido_prodesp CHAR(1) = NULL,
		@dt_trasmitido_prodesp DATE = NULL,
		@fg_arquivo_cancelado BIT = NULL,
		@id_regional INTEGER  = NULL,
		@bl_cadastro_completo BIT = NULL,
	    @ds_msg_retorno	 VARCHAR(256)  = NULL,
		@bl_transmitir_prodesp BIT = NULL,
		@bl_transmitido_prodesp BIT = NULL



as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_arquivo_remessa](nolock)
		where	id_arquivo_remessa = @id_arquivo_remessa
	)
	begin

		UPDATE [contaunica].[tb_arquivo_remessa]
   SET [tb_arquivo_id_arquivo] = @tb_arquivo_id_arquivo
      ,[nr_geracao_arquivo] = @nr_geracao_arquivo
      ,[dt_preparacao_pagamento] = @dt_preparacao_pagamento
      ,[dt_pagamento] = @dt_pagamento
      ,[cd_assinatura] = @cd_assinatura
      ,[cd_grupo_assinatura] = @cd_grupo_assinatura
      ,[cd_orgao_assinatura] = @cd_orgao_assinatura
      ,[nm_assinatura] = @nm_assinatura
      ,[ds_cargo] = @ds_cargo
      ,[cd_contra_assinatura] = @cd_contra_assinatura
      ,[cd_grupo_contra_assinatura] = @cd_grupo_contra_assinatura
      ,[cd_orgao_contra_assinatura] = @cd_orgao_contra_assinatura
      ,[nm_contra_assinatura] = @nm_contra_assinatura
      ,[ds_cargo_contra_assinatura] = @ds_cargo_contra_assinatura
      ,[cd_conta] = @cd_conta
      ,[ds_banco] = @ds_banco
      ,[ds_agencia] = @ds_agencia
      ,[ds_conta] = @ds_conta
      ,[qt_ordem_pagamento_arquivo] = @qt_ordem_pagamento_arquivo
      ,[qt_deposito_arquivo] = @qt_deposito_arquivo
      ,[vr_total_pago] = @vr_total_pago
      ,[qt_doc_ted_arquivo] = @qt_doc_ted_arquivo
      --,[dt_cadastro] = @dt_cadastro
      ,[fg_trasmitido_prodesp] = @fg_trasmitido_prodesp
      ,[dt_trasmitido_prodesp] = @dt_trasmitido_prodesp
      ,[fg_arquivo_cancelado] = @fg_arquivo_cancelado
      --,[id_regional] = @id_regional
	  ,[ds_msg_retorno] = @ds_msg_retorno
      ,[bl_cadastro_completo] = @bl_cadastro_completo
	  ,[bl_transmitido_prodesp] = @bl_transmitido_prodesp

		where	id_arquivo_remessa = @id_arquivo_remessa;

		select @id_arquivo_remessa;

	end
	else
	begin

      INSERT INTO	[contaunica].[tb_arquivo_remessa]
           (
		    [tb_arquivo_id_arquivo]
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
           ,[id_regional]
           ,[bl_cadastro_completo]
		   ,[ds_msg_retorno]
		   ,[bl_transmitir_prodesp]
		   ,[bl_transmitido_prodesp]
		   )
		values
		(
		nullif(@tb_arquivo_id_arquivo,0)
		,nullif(@nr_geracao_arquivo,0)
		,@dt_preparacao_pagamento
		,@dt_pagamento
		,@cd_assinatura
		,@cd_grupo_assinatura
		,@cd_orgao_assinatura
		,@nm_assinatura
		,@ds_cargo
		,@cd_contra_assinatura
		,@cd_grupo_contra_assinatura
		,@cd_orgao_contra_assinatura
		,@nm_contra_assinatura
		,@ds_cargo_contra_assinatura
		,nullif(@cd_conta,0)
		,@ds_banco
		,@ds_agencia
		,@ds_conta
		,nullif(@qt_ordem_pagamento_arquivo,0)
		,nullif(@qt_deposito_arquivo,0)
		,nullif(@vr_total_pago,0)
		,nullif(@qt_doc_ted_arquivo,0)
		,GETDATE()
		,'N'
		,@dt_trasmitido_prodesp
		,0
		,nullif(@id_regional,0)
		,@bl_cadastro_completo
		,@ds_msg_retorno
		,@bl_transmitir_prodesp
		,@bl_transmitido_prodesp
		);

		select scope_identity();

	end

end