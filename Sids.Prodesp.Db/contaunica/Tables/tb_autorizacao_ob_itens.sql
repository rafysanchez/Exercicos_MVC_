CREATE TABLE [contaunica].[tb_autorizacao_ob_itens] (
    [id_autorizacao_ob_item]                 INT           IDENTITY (1, 1) NOT NULL,
    [id_autorizacao_ob]                      INT           NOT NULL,
    [nr_agrupamento_ob]                      INT           NULL,
    [id_execucao_pd]                         INT           NULL,
    [id_execucao_pd_item]                    INT           NULL,
    [ds_numob]                               VARCHAR (20)  NULL,
    [ds_numop]                               VARCHAR (20)  NULL,
    [ds_consulta_op_prodesp]                 VARCHAR (140) NULL,
    [nr_documento_gerador]                   VARCHAR (50)  NULL,
    [dt_cadastro]                            DATETIME      NULL,
    [id_tipo_documento]                      INT           NULL,
    [nr_documento]                           VARCHAR (20)  NULL,
    [nr_contrato]                            VARCHAR (13)  NULL,
    [favorecido]                             VARCHAR (20)  NULL,
    [favorecidoDesc]                         VARCHAR (120) NULL,
    [ug_pagadora]                            VARCHAR (50)  NULL,
    [ug_liquidante]                          VARCHAR (50)  NULL,
    [gestao_pagadora]                        VARCHAR (50)  NULL,
    [gestao_liguidante]                      VARCHAR (50)  NULL,
    [cd_despesa]                             VARCHAR (2)   NULL,
    [nr_banco]                               VARCHAR (30)  NULL,
    [valor]                                  VARCHAR (50)  NULL,
    [cd_transmissao_status_prodesp]          VARCHAR (1)   NULL,
    [fl_transmissao_transmitido_prodesp]     BIT           NULL,
    [dt_transmissao_transmitido_prodesp]     DATETIME      NULL,
    [ds_transmissao_mensagem_prodesp]        VARCHAR (140) NULL,
    [cd_transmissao_item_status_siafem]      CHAR (1)      NULL,
    [dt_transmissao_item_transmitido_siafem] DATETIME      NULL,
    [fl_transmissao_item_siafem]             BIT           NULL,
    [ds_transmissao_item_mensagem_siafem]    VARCHAR (140) NULL,
    [dt_confirmacao]                         DATETIME      NULL,
    [cd_aplicacao_obra]                      VARCHAR (140) NULL,
    CONSTRAINT [PK_tb_autorizacao_ob_itens] PRIMARY KEY CLUSTERED ([id_autorizacao_ob_item] ASC)
);

























