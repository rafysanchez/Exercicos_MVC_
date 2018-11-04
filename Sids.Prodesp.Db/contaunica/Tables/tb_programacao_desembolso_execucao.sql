CREATE TABLE [contaunica].[tb_programacao_desembolso_execucao] (
    [id_execucao_pd]              INT          IDENTITY (1, 1) NOT NULL,
    [id_tipo_execucao_pd]         INT          NOT NULL,
    [ug_pagadora]                 VARCHAR (50) NULL,
    [gestao_pagadora]             VARCHAR (50) NULL,
    [ug_liquidante]               VARCHAR (50) NULL,
    [gestao_liquidante]           VARCHAR (50) NULL,
    [unidade_gestora]             VARCHAR (50) NULL,
    [gestao]                      VARCHAR (50) NULL,
    [ano_pd]                      VARCHAR (50) NULL,
    [valor_total]                 DECIMAL (18) NULL,
    [nr_agrupamento]              INT          NULL,
    [dt_cadastro]                 DATETIME     NOT NULL,
    [fl_sistema_prodesp]          BIT          NULL,
    [fl_sistema_siafem_siafisico] BIT          NULL,
    [id_tipo_pagamento]           INT          NULL,
    [fl_confirmacao]              BIT          CONSTRAINT [DF_tb_programacao_desembolso_execucao_fl_confirmacao] DEFAULT ((0)) NOT NULL,
    [dt_confirmacao]              DATETIME     NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_execucao] PRIMARY KEY CLUSTERED ([id_execucao_pd] ASC)
);







