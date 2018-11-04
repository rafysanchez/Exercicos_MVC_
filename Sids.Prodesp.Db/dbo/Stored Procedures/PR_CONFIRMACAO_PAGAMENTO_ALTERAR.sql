 -- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/01/2018
-- Description: Procedure para Alterar pagamento.tb_confirmacao_pagamento
-- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ALTERAR]
@id_confirmacao_pagamento int  ,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@id_tipo_documento int  = NULL,
@id_tipo_pagamento int  = NULL,
@nr_conta varchar(3) = NULL,
@nr_documento varchar(19)  = NULL,
@ano_referencia varchar(4) = NULL,
@dt_cadastro datetime = NULL,
@dt_confirmacao datetime  = NULL,
@dt_modificacao datetime  = NULL,
@dt_preparacao datetime  = NULL,
@nr_agrupamento int = NULL,
@vr_total_confirmado decimal(18,2) = NULL,
@cd_transmissao_status_prodesp	varchar(50) = NULL,
@fl_transmissao_transmitido_prodesp	bit = NULL,
@dt_transmissao_transmitido_prodesp	datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(200) = NULL
AS
BEGIN
 SET NOCOUNT ON;

 IF @cd_transmissao_status_prodesp = 'S'
	SET @vr_total_confirmado = ISNULL(@vr_total_confirmado, 0)
 ELSE
	SET @vr_total_confirmado = 0

UPDATE
pagamento.tb_confirmacao_pagamento
 SET
id_execucao_pd = @id_execucao_pd,
id_autorizacao_ob = @id_autorizacao_ob,
--id_tipo_documento =  @id_tipo_documento,
--ano_referencia =  @ano_referencia,
--nr_documento =  @nr_documento,
--id_tipo_pagamento =  @id_tipo_pagamento,
dt_confirmacao =  @dt_confirmacao,
dt_preparacao =  @dt_preparacao,
nr_conta =  @nr_conta,
--dt_cadastro =  @dt_cadastro,
nr_agrupamento = @nr_agrupamento,
dt_modificacao =  GETDATE(),
vr_total_confirmado = ISNULL(vr_total_confirmado, 0) + @vr_total_confirmado,
cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,
dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp,
ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
WHERE
id_confirmacao_pagamento = @id_confirmacao_pagamento
END

-----------------------------------------------------------------------------------------