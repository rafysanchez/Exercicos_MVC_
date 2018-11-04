CREATE TABLE [contaunica].[tb_autorizacao_ob] (
    [id_autorizacao_ob]                 INT           IDENTITY (1, 1) NOT NULL,
    [id_confirmacao_pagamento]          INT           NULL,
    [id_execucao_pd]                    INT           NULL,
    [nr_agrupamento]                    INT           NULL,
    [ug_pagadora]                       VARCHAR (50)  NULL,
    [gestao_pagadora]                   VARCHAR (50)  NULL,
    [ug_liquidante]                     VARCHAR (50)  NULL,
    [gestao_liquidante]                 VARCHAR (50)  NULL,
    [unidade_gestora]                   VARCHAR (50)  NULL,
    [gestao]                            VARCHAR (50)  NULL,
    [ano_ob]                            VARCHAR (4)   NULL,
    [valor_total_autorizacao]           VARCHAR (50)  NULL,
    [qtde_autorizacao]                  INT           NULL,
    [dt_cadastro]                       DATETIME      CONSTRAINT [DF_tb_autorizacao_ob_dt_cadastro] DEFAULT (getdate()) NULL,
    [cd_transmissao_status_siafem]      CHAR (1)      NULL,
    [dt_transmissao_transmitido_siafem] DATETIME      NULL,
    [fl_transmissao_siafem]             BIT           NULL,
    [ds_transmissao_mensagem_siafem]    VARCHAR (140) NULL,
    [nr_contrato]                       VARCHAR (13)  NULL,
    [cd_aplicacao_obra]                 VARCHAR (140) NULL,
    [id_tipo_pagamento]                 INT           NULL,
    [fl_confirmacao]                    BIT           CONSTRAINT [DF_tb_autorizacao_ob_fl_confirmacao] DEFAULT ((0)) NOT NULL,
    [dt_confirmacao]                    DATETIME      NULL,
    CONSTRAINT [PK_tb_autorizacao_ob] PRIMARY KEY CLUSTERED ([id_autorizacao_ob] ASC),
    CONSTRAINT [FK_tb_autorizacao_ob_tb_confirmacao_pagamento] FOREIGN KEY ([id_confirmacao_pagamento]) REFERENCES [pagamento].[tb_confirmacao_pagamento] ([id_confirmacao_pagamento])
);















