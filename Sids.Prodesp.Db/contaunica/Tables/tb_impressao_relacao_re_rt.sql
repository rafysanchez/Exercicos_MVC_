﻿CREATE TABLE [contaunica].[tb_impressao_relacao_re_rt] (
    [id_impressao_relacao_re_rt]     INT             IDENTITY (1, 1) NOT NULL,
    [cd_relob]                       VARCHAR (11)    NULL,
    [nr_ob]                          VARCHAR (11)    NULL,
    [cd_relatorio]                   VARCHAR (10)    NULL,
    [nr_agrupamento]                 INT             NULL,
    [cd_unidade_gestora]             VARCHAR (6)     NULL,
    [ds_nome_unidade_gestora]        VARCHAR (30)    NULL,
    [cd_gestao]                      VARCHAR (5)     NULL,
    [ds_nome_gestao]                 VARCHAR (30)    NULL,
    [cd_banco]                       VARCHAR (3)     NULL,
    [ds_nome_banco]                  VARCHAR (30)    NULL,
    [ds_texto_autorizacao]           VARCHAR (70)    NULL,
    [ds_cidade]                      VARCHAR (30)    NULL,
    [ds_nome_gestor_financeiro]      VARCHAR (30)    NULL,
    [ds_nome_ordenador_assinatura]   VARCHAR (30)    NULL,
    [dt_solicitacao]                 DATETIME        NULL,
    [dt_referencia]                  DATETIME        NULL,
    [dt_cadastramento]               DATETIME        NULL,
    [dt_emissao]                     DATETIME        NULL,
    [vl_total_documento]             DECIMAL (15, 2) NULL,
    [vl_extenso]                     VARCHAR (255)   NULL,
    [fg_transmitido_siafem]          BIT             NULL,
    [fg_transmitir_siafem]           BIT             NULL,
    [dt_transmitido_siafem]          DATETIME        NULL,
    [ds_status_siafem]               VARCHAR (1)     NULL,
    [ds_msgRetornoTransmissaoSiafem] VARCHAR (140)   NULL,
    [fg_cancelamento_relacao_re_rt]  BIT             NULL,
    [nr_agencia]                     VARCHAR (5)     NULL,
    [ds_nome_agencia]                VARCHAR (30)    NULL,
    [nr_conta_c]                     VARCHAR (10)    NULL,
    CONSTRAINT [PK_tb_impressao_relacao_re_rt] PRIMARY KEY CLUSTERED ([id_impressao_relacao_re_rt] ASC)
);
