CREATE TABLE [pagamento].[tb_confirmacao_pagamento] (
    [id_confirmacao_pagamento]           INT             IDENTITY (1, 1) NOT NULL,
    [id_confirmacao_pagamento_tipo]      INT             CONSTRAINT [DF_tb_confirmacao_pagamento_id_confirmacao_pagamento_tipo] DEFAULT ((1)) NOT NULL,
    [id_execucao_pd]                     INT             NULL,
    [id_autorizacao_ob]                  INT             NULL,
    [id_tipo_documento]                  INT             NULL,
    [nr_agrupamento]                     INT             NULL,
    [ano_referencia]                     INT             NULL,
    [id_tipo_pagamento]                  INT             NULL,
    [dt_confirmacao]                     DATETIME        NULL,
    [dt_cadastro]                        DATETIME        CONSTRAINT [DF_tb_confirmacao_pagamento_dt_cadastro] DEFAULT (getdate()) NOT NULL,
    [dt_modificacao]                     DATETIME        NULL,
    [vr_total_confirmado]                DECIMAL (18, 2) NULL,
    [cd_transmissao_status_prodesp]      VARCHAR (50)    NULL,
    [fl_transmissao_transmitido_prodesp] BIT             NULL,
    [dt_transmissao_transmitido_prodesp] DATETIME        NULL,
    [ds_transmissao_mensagem_prodesp]    VARCHAR (200)   NULL,
    [dt_preparacao]                      DATETIME        NULL,
    [nr_conta]                           VARCHAR (20)    NULL,
    [nr_documento]                       VARCHAR (30)    NULL,
    CONSTRAINT [PK_tb_confirmacao_pagamento] PRIMARY KEY CLUSTERED ([id_confirmacao_pagamento] ASC),
    CONSTRAINT [FK_tb_confirmacao_pagamento_tb_confirmacao_pagamento_tipo] FOREIGN KEY ([id_confirmacao_pagamento_tipo]) REFERENCES [pagamento].[tb_confirmacao_pagamento_tipo] ([id_confirmacao_tipo]),
    CONSTRAINT [FK_tb_confirmacao_pagamento_tb_programacao_desembolso_execucao_tipo_pagamento] FOREIGN KEY ([id_tipo_pagamento]) REFERENCES [contaunica].[tb_programacao_desembolso_execucao_tipo_pagamento] ([id_programacao_desembolso_execucao_tipo_pagamento])
);

















