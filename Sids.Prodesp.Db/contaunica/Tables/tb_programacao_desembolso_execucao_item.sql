CREATE TABLE [contaunica].[tb_programacao_desembolso_execucao_item] (
    [id_programacao_desembolso_execucao_item] INT           IDENTITY (1, 1) NOT NULL,
    [id_execucao_pd]                          INT           NOT NULL,
    [ds_numpd]                                VARCHAR (50)  NULL,
    [nr_documento_gerador]                    VARCHAR (50)  NULL,
    [id_tipo_documento]                       INT           NULL,
    [nr_documento]                            VARCHAR (20)  NULL,
    [nr_contrato]                             VARCHAR (13)  NULL,
    [nr_op]                                   VARCHAR (50)  NULL,
    [id_tipo_pagamento]                       INT           NULL,
    [dt_confirmacao]                          DATETIME      NULL,
    [ug]                                      VARCHAR (50)  NULL,
    [gestao]                                  VARCHAR (50)  NULL,
    [ug_pagadora]                             VARCHAR (50)  NULL,
    [ug_liquidante]                           VARCHAR (50)  NULL,
    [gestao_pagadora]                         VARCHAR (50)  NULL,
    [gestao_liguidante]                       VARCHAR (50)  NULL,
    [favorecido]                              VARCHAR (20)  NULL,
    [favorecidoDesc]                          VARCHAR (120) NULL,
    [nr_cnpj_cpf_pgto]                        VARCHAR (15)  NULL,
    [ordem]                                   VARCHAR (50)  NULL,
    [ano_pd]                                  VARCHAR (4)   NULL,
    [valor]                                   VARCHAR (50)  NULL,
    [ds_noup]                                 VARCHAR (1)   NULL,
    [nr_agrupamento_pd]                       INT           NULL,
    [ds_numob]                                VARCHAR (50)  NULL,
    [ob_cancelada]                            BIT           NULL,
    [fl_sistema_prodesp]                      BIT           NULL,
    [cd_transmissao_status_prodesp]           VARCHAR (1)   NULL,
    [fl_transmissao_transmitido_prodesp]      BIT           NULL,
    [dt_transmissao_transmitido_prodesp]      DATETIME      NULL,
    [ds_transmissao_mensagem_prodesp]         VARCHAR (140) NULL,
    [cd_transmissao_status_siafem]            CHAR (1)      CONSTRAINT [DF_tb_programacao_desembolso_execucao_item_cd_transmissao_status_siafem] DEFAULT ('N') NULL,
    [fl_transmissao_transmitido_siafem]       BIT           NULL,
    [dt_transmissao_transmitido_siafem]       DATETIME      NULL,
    [ds_transmissao_mensagem_siafem]          VARCHAR (140) NULL,
    [ds_consulta_op_prodesp]                  VARCHAR (140) NULL,
    [dt_emissao]                              DATETIME      NULL,
    [dt_vencimento]                           DATETIME      NULL,
    [ds_causa_cancelamento]                   VARCHAR (200) NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_execucao_item] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_execucao_item] ASC),
    CONSTRAINT [FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao] FOREIGN KEY ([id_execucao_pd]) REFERENCES [contaunica].[tb_programacao_desembolso_execucao] ([id_execucao_pd])
);













