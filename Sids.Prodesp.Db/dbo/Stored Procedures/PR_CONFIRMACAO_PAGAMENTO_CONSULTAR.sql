 -- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/01/2018
-- Description: Procedure para Consultar pagamento.tb_confirmacao_pagamento
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_CONSULTAR]
@id_confirmacao_pagamento INT = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@id_tipo_documento int  = NULL,
@ano_referencia varchar(4) = NULL,
@nr_documento int  = NULL,
@id_tipo_pagamento int  = NULL,
@dt_confirmacao datetime  = NULL,
@dt_preparacao datetime  = NULL,
@nr_conta varchar(3) = NULL,
@dt_cadastro datetime = NULL,
@dt_modificacao datetime  = NULL,
@nr_agrupamento INT = NULL
AS
BEGIN
SELECT
C.id_confirmacao_pagamento,
I.id_execucao_pd,
I.id_autorizacao_ob,
I.id_tipo_documento,
ano_referencia,
I.nr_documento,
id_tipo_pagamento,
I.dt_confirmacao,
dt_preparacao,
nr_conta,
dt_cadastro,
dt_modificacao,
nr_agrupamento,
I.fl_transmissao_transmitido_prodesp,
I.cd_transmissao_status_prodesp,
I.dt_transmissao_transmitido_prodesp,
I.ds_transmissao_mensagem_prodesp
FROM pagamento.tb_confirmacao_pagamento AS C
LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS I ON I.id_Confirmacao_pagamento = C.id_confirmacao_pagamento
WHERE
( NULLIF( @id_confirmacao_pagamento, 0) IS NULL OR C.id_confirmacao_pagamento = @id_confirmacao_pagamento) AND
( NULLIF( @id_execucao_pd, 0 ) IS NULL OR I.id_execucao_pd = @id_execucao_pd )  AND
( NULLIF( @id_autorizacao_ob, 0 ) IS NULL OR I.id_autorizacao_ob = @id_autorizacao_ob )  AND
( NULLIF( @id_tipo_documento, 0 ) IS NULL OR I.id_tipo_documento = @id_tipo_documento )  AND
( NULLIF( @ano_referencia, '' ) IS NULL OR ano_referencia LIKE '%' +  @ano_referencia + '%' )  AND
( NULLIF( @nr_documento, 0 ) IS NULL OR I.nr_documento = @nr_documento )  AND
( NULLIF( @id_tipo_pagamento, 0 ) IS NULL OR id_tipo_pagamento = @id_tipo_pagamento )  AND
( NULLIF( @dt_confirmacao, 0 ) IS NULL OR I.dt_confirmacao = @dt_confirmacao )  AND
( NULLIF( @dt_preparacao, 0 ) IS NULL OR dt_preparacao = @dt_preparacao )  AND
( NULLIF( @nr_conta, '' ) IS NULL OR nr_conta LIKE '%' +  @nr_conta + '%' )  AND
( NULLIF( @dt_cadastro, 0 ) IS NULL OR dt_cadastro = @dt_cadastro )  AND
( NULLIF( @dt_modificacao, 0 ) IS NULL OR dt_modificacao = @dt_modificacao ) AND
( NULLIF( @nr_agrupamento, 0 ) IS NULL OR nr_agrupamento = @nr_agrupamento )
END

-----------------------------------------------------------------------------------------