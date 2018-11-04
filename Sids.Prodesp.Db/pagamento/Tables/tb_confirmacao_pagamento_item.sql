CREATE TABLE [pagamento].[tb_confirmacao_pagamento_item] (
    [id_confirmacao_pagamento_item]           INT            IDENTITY (1, 1) NOT NULL,
    [id_confirmacao_pagamento]                INT            NOT NULL,
    [id_execucao_pd]                          INT            NULL,
    [id_programacao_desembolso_execucao_item] INT            NULL,
    [id_autorizacao_ob]                       INT            NULL,
    [id_autorizacao_ob_item]                  INT            NULL,
    [nr_documento_gerador]                    VARCHAR (22)   NULL,
    [dt_confirmacao]                          DATETIME       NULL,
    [id_tipo_documento]                       INT            NULL,
    [nr_documento]                            VARCHAR (30)   NULL,
    [id_regional]                             SMALLINT       NULL,
    [id_reclassificacao_retencao]             INT            NULL,
    [id_origem]                               INT            NULL,
    [id_despesa_tipo]                         INT            NULL,
    [dt_vencimento]                           DATETIME       NULL,
    [nr_contrato]                             VARCHAR (13)   NULL,
    [cd_obra]                                 VARCHAR (20)   NULL,
    [nr_op]                                   VARCHAR (50)   NULL,
    [nr_banco_pagador]                        VARCHAR (10)   NULL,
    [nr_agencia_pagador]                      VARCHAR (10)   NULL,
    [nr_conta_pagador]                        VARCHAR (10)   NULL,
    [nr_fonte_siafem]                         VARCHAR (50)   NULL,
    [nr_emprenho]                             VARCHAR (50)   NULL,
    [nr_processo]                             VARCHAR (20)   NULL,
    [nr_nota_fiscal]                          INT            NULL,
    [nr_nl_documento]                         VARCHAR (20)   NULL,
    [vr_documento]                            DECIMAL (8, 2) NULL,
    [nr_natureza_despesa]                     INT            NULL,
    [cd_credor_organizacao]                   INT            NULL,
    [nr_cnpj_cpf_ug_credor]                   VARCHAR (14)   NULL,
    [ds_referencia]                           NVARCHAR (100) NULL,
    [cd_orgao_assinatura]                     VARCHAR (2)    NULL,
    [nm_reduzido_credor]                      VARCHAR (14)   NULL,
    [fl_transmissao_transmitido_prodesp]      BIT            NULL,
    [cd_transmissao_status_prodesp]           CHAR (1)       NULL,
    [dt_transmissao_transmitido_prodesp]      DATETIME       NULL,
    [ds_transmissao_mensagem_prodesp]         VARCHAR (140)  NULL,
    [nr_empenhoSiafem]                        VARCHAR (11)   NULL,
    [nr_banco_favorecido]                     VARCHAR (10)   NULL,
    [nr_agencia_favorecido]                   VARCHAR (10)   NULL,
    [nr_conta_favorecido]                     VARCHAR (10)   NULL,
    [vr_desdobramento]                        DECIMAL (8, 2) NULL,
    [cd_credor_organizacao_docto]             INT            NULL,
    [nr_cnpj_cpf_ug_credor_docto]             VARCHAR (14)   NULL,
    [dt_realizacao]                           DATETIME       NULL,
    [nm_reduzido_credor_docto]                VARCHAR (14)   NULL,
    CONSTRAINT [PK_tb_confirmacao_pagamento_item] PRIMARY KEY CLUSTERED ([id_confirmacao_pagamento_item] ASC, [id_confirmacao_pagamento] ASC),
    CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento] FOREIGN KEY ([id_confirmacao_pagamento]) REFERENCES [pagamento].[tb_confirmacao_pagamento] ([id_confirmacao_pagamento]),
    CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo] FOREIGN KEY ([id_despesa_tipo]) REFERENCES [pagamento].[tb_despesa_tipo] ([id_despesa_tipo]),
    CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_origem] FOREIGN KEY ([id_origem]) REFERENCES [pagamento].[tb_origem] ([id_origem]),
    CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao] FOREIGN KEY ([id_reclassificacao_retencao]) REFERENCES [contaunica].[tb_reclassificacao_retencao] ([id_reclassificacao_retencao])
);



















