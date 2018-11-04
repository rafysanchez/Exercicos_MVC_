
 -- ==============================================================
-- Author:  Jose Braz
-- Create date: 30/01/2018
-- Description: Procedure para Consultar pagamento.tb_confirmacao_pagamento_item
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]
@id_confirmacao_pagamento_item int = NULL,
@id_confirmacao_pagamento int = NULL,
@id_programacao_desembolso_execucao_item int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@dt_confirmacao datetime = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int  = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato int  = NULL,
@cd_obra int  = NULL,
@nr_op varchar(50) = NULL,
@nr_banco_pagador varchar(10) = NULL,
@nr_agencia_pagador varchar(10) = NULL,
@nr_conta_pagador varchar(10) = NULL,
@nr_fonte_siafem varchar(50) = NULL,
@nr_emprenho varchar(50) = NULL,
@nr_processo int  = NULL,
@nr_nota_fiscal int  = NULL,
@nr_nl_documento int  = NULL,
@vr_documento decimal(8,2) = NULL,
@nr_natureza_despesa int  = NULL,
@cd_credor_organizacao int  = NULL,
@nr_cnpj_cpf_ug_credor varchar(14)  = NULL,
@vr_total_confirmado decimal(8,2) = NULL,
@fl_transmissao bit  = NULL,
@dt_trasmissao datetime  = NULL,
@ds_referencia nvarchar(100) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@cd_transmissao_status_prodesp char(1) = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL,
@nr_empenhoSiafem varchar(11) = NULL,
@nr_banco_favorecido varchar(10) = NULL,
@nr_agencia_favorecido varchar(10) = NULL,
@nr_conta_favorecido varchar(10) = NULL,
@nr_documento varchar(30) = NULL,
@nr_documento_gerador varchar(22) = null,
@id_tipo_documento int = null
AS
BEGIN
SELECT
id_confirmacao_pagamento_item,
id_confirmacao_pagamento,
id_programacao_desembolso_execucao_item,
dt_confirmacao,
id_regional,
id_reclassificacao_retencao,
id_origem,
id_despesa_tipo,
dt_vencimento,
nr_contrato,
cd_obra,
nr_op,
nr_banco_pagador,
nr_agencia_pagador,
nr_conta_pagador,
nr_fonte_siafem,
nr_emprenho,
nr_processo,
nr_nota_fiscal,
nr_nl_documento,
vr_documento,
nr_natureza_despesa,
cd_credor_organizacao,
nr_cnpj_cpf_ug_credor,
fl_transmissao_transmitido_prodesp,
cd_transmissao_status_prodesp,
dt_transmissao_transmitido_prodesp,
ds_transmissao_mensagem_prodesp,
nr_empenhoSiafem,
nr_banco_favorecido,
nr_agencia_favorecido,
nr_conta_favorecido,
nr_documento_gerador,
nr_documento,
id_tipo_documento
FROM pagamento.tb_confirmacao_pagamento_item
WHERE
( NULLIF( @id_programacao_desembolso_execucao_item, 0 ) IS NULL OR id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item )  AND
( NULLIF( @id_confirmacao_pagamento_item, 0 ) IS NULL OR id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item )  AND
( NULLIF( @id_confirmacao_pagamento, 0 ) IS NULL OR id_confirmacao_pagamento = @id_confirmacao_pagamento )  AND
( NULLIF( @id_autorizacao_ob_item, 0) IS NULL OR id_autorizacao_ob_item = @id_autorizacao_ob_item )  AND
( NULLIF( @id_regional, 0 ) IS NULL OR id_regional = @id_regional )  AND
( NULLIF( @id_reclassificacao_retencao, 0 ) IS NULL OR id_reclassificacao_retencao = @id_reclassificacao_retencao )  AND
( NULLIF( @id_origem, 0 ) IS NULL OR id_origem = @id_origem )  AND
( NULLIF( @id_despesa_tipo, 0 ) IS NULL OR id_despesa_tipo = @id_despesa_tipo )  AND
( NULLIF( @dt_vencimento, 0 ) IS NULL OR dt_vencimento = @dt_vencimento )  AND
( NULLIF( @nr_contrato, 0 ) IS NULL OR nr_contrato = @nr_contrato )  AND
( NULLIF( @cd_obra, 0 ) IS NULL OR cd_obra = @cd_obra )  AND
( NULLIF( @nr_op, '' ) IS NULL OR nr_op LIKE '%' +  @nr_op + '%' )  AND
( NULLIF( @nr_banco_pagador, '' ) IS NULL OR nr_banco_pagador LIKE '%' +  @nr_banco_pagador + '%' )  AND
( NULLIF( @nr_agencia_pagador, '' ) IS NULL OR nr_agencia_pagador LIKE '%' +  @nr_agencia_pagador + '%' )  AND
( NULLIF( @nr_conta_pagador, '' ) IS NULL OR nr_conta_pagador LIKE '%' +  @nr_conta_pagador + '%' )  AND
( NULLIF( @nr_fonte_siafem, '' ) IS NULL OR nr_fonte_siafem LIKE '%' +  @nr_fonte_siafem + '%' )  AND
( NULLIF( @nr_emprenho, '' ) IS NULL OR nr_emprenho LIKE '%' +  @nr_emprenho + '%' )  AND
( NULLIF( @nr_processo, 0 ) IS NULL OR nr_processo = @nr_processo )  AND
( NULLIF( @nr_nota_fiscal, 0 ) IS NULL OR nr_nota_fiscal = @nr_nota_fiscal )  AND
( NULLIF( @nr_nl_documento, 0 ) IS NULL OR nr_nl_documento = @nr_nl_documento )  AND
( NULLIF( @vr_documento, 0 ) IS NULL OR vr_documento = @vr_documento )  AND
( NULLIF( @nr_natureza_despesa, 0 ) IS NULL OR nr_natureza_despesa = @nr_natureza_despesa )  AND
( NULLIF( @cd_credor_organizacao, 0 ) IS NULL OR cd_credor_organizacao = @cd_credor_organizacao )  AND
( NULLIF( @nr_cnpj_cpf_ug_credor, 0 ) IS NULL OR nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor ) AND
( NULLIF( @nr_empenhoSiafem, '' ) IS NULL OR nr_empenhoSiafem LIKE '%' +  @nr_empenhoSiafem + '%' )  AND
( NULLIF( @nr_banco_favorecido, '' ) IS NULL OR nr_banco_favorecido LIKE '%' +  @nr_banco_favorecido + '%' )  AND
( NULLIF( @nr_banco_pagador, '' ) IS NULL OR nr_agencia_favorecido LIKE '%' +  @nr_agencia_favorecido + '%' )  AND
( NULLIF( @nr_banco_pagador, '' ) IS NULL OR nr_conta_favorecido LIKE '%' +  @nr_conta_favorecido + '%' ) AND
( NULLIF( @nr_documento, '' ) IS NULL OR nr_documento LIKE '%' +  @nr_documento + '%' ) AND
( NULLIF( @nr_documento_gerador, '' ) IS NULL OR nr_documento_gerador LIKE '%' +  @nr_documento_gerador + '%' ) AND
( NULLIF( @id_tipo_documento, 0 ) IS NULL OR id_tipo_documento = @id_tipo_documento )

END

-----------------------------------------------------------------------------------------
