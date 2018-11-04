USE [dbSIDS];
--teste jane
GO

IF (OBJECT_ID('[contaunica].[DF_tb_autorizacao_ob_dt_cadastro]', 'D') IS NOT NULL)
BEGIN
	PRINT N'Dropping [contaunica].[DF_tb_autorizacao_ob_dt_cadastro]...';
	ALTER TABLE [contaunica].[tb_autorizacao_ob] DROP CONSTRAINT [DF_tb_autorizacao_ob_dt_cadastro];
END
GO

IF (OBJECT_ID('[contaunica].[FK_tb_autorizacao_ob_tb_confirmacao_pagamento]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [contaunica].[FK_tb_autorizacao_ob_tb_confirmacao_pagamento]...';
	ALTER TABLE [contaunica].[tb_autorizacao_ob] DROP CONSTRAINT [FK_tb_autorizacao_ob_tb_confirmacao_pagamento];
END
GO

IF (OBJECT_ID('[contaunica].[FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [contaunica].[FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao]...';
	ALTER TABLE [contaunica].[tb_programacao_desembolso_execucao_item] DROP CONSTRAINT [FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao];
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento];
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_origem]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_item_tb_origem]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_origem];
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao];
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo];
END
GO


PRINT N'Creating [movimentacao]...';


GO
CREATE SCHEMA [movimentacao]
    AUTHORIZATION [dbo];


GO
PRINT N'Starting rebuilding table [contaunica].[tb_autorizacao_ob]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_autorizacao_ob] (
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
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_autorizacao_ob1] PRIMARY KEY CLUSTERED ([id_autorizacao_ob] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_autorizacao_ob])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_autorizacao_ob] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_autorizacao_ob] ([id_autorizacao_ob], [id_confirmacao_pagamento], [id_tipo_pagamento], [id_execucao_pd], [nr_agrupamento], [ug_pagadora], [gestao_pagadora], [ug_liquidante], [gestao_liquidante], [unidade_gestora], [gestao], [ano_ob], [valor_total_autorizacao], [qtde_autorizacao], [dt_cadastro], [cd_transmissao_status_siafem], [dt_transmissao_transmitido_siafem], [fl_transmissao_siafem], [ds_transmissao_mensagem_siafem], [nr_contrato], [cd_aplicacao_obra])
        SELECT   [id_autorizacao_ob],
                 [id_confirmacao_pagamento],
                 [id_tipo_pagamento],
                 [id_execucao_pd],
                 [nr_agrupamento],
                 [ug_pagadora],
                 [gestao_pagadora],
                 [ug_liquidante],
                 [gestao_liquidante],
                 [unidade_gestora],
                 [gestao],
                 [ano_ob],
                 [valor_total_autorizacao],
                 [qtde_autorizacao],
                 [dt_cadastro],
                 [cd_transmissao_status_siafem],
                 [dt_transmissao_transmitido_siafem],
                 [fl_transmissao_siafem],
                 [ds_transmissao_mensagem_siafem],
                 [nr_contrato],
                 [cd_aplicacao_obra]
        FROM     [contaunica].[tb_autorizacao_ob]
        ORDER BY [id_autorizacao_ob] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_autorizacao_ob] OFF;
    END

DROP TABLE [contaunica].[tb_autorizacao_ob];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_autorizacao_ob]', N'tb_autorizacao_ob';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_autorizacao_ob1]', N'PK_tb_autorizacao_ob', N'OBJECT';


GO
PRINT N'Starting rebuilding table [contaunica].[tb_autorizacao_ob_itens]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_autorizacao_ob_itens] (
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
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_autorizacao_ob_itens1] PRIMARY KEY CLUSTERED ([id_autorizacao_ob_item] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_autorizacao_ob_itens])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_autorizacao_ob_itens] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_autorizacao_ob_itens] ([id_autorizacao_ob_item], [id_autorizacao_ob], [id_execucao_pd], [id_execucao_pd_item], [ds_numob], [ds_numop], [ds_consulta_op_prodesp], [nr_documento_gerador], [dt_cadastro], [id_tipo_documento], [nr_documento], [nr_contrato], [favorecido], [favorecidoDesc], [ug_pagadora], [ug_liquidante], [gestao_pagadora], [gestao_liguidante], [cd_despesa], [nr_banco], [valor], [cd_transmissao_status_prodesp], [fl_transmissao_transmitido_prodesp], [dt_transmissao_transmitido_prodesp], [ds_transmissao_mensagem_prodesp], [cd_transmissao_item_status_siafem], [dt_transmissao_item_transmitido_siafem], [fl_transmissao_item_siafem], [ds_transmissao_item_mensagem_siafem], [dt_confirmacao])
        SELECT   [id_autorizacao_ob_item],
                 [id_autorizacao_ob],
                 [id_execucao_pd],
                 [id_execucao_pd_item],
                 [ds_numob],
                 [ds_numop],
                 [ds_consulta_op_prodesp],
                 [nr_documento_gerador],
                 [dt_cadastro],
                 [id_tipo_documento],
                 [nr_documento],
                 [nr_contrato],
                 [favorecido],
                 [favorecidoDesc],
                 [ug_pagadora],
                 [ug_liquidante],
                 [gestao_pagadora],
                 [gestao_liguidante],
                 [cd_despesa],
                 [nr_banco],
                 [valor],
                 [cd_transmissao_status_prodesp],
                 [fl_transmissao_transmitido_prodesp],
                 [dt_transmissao_transmitido_prodesp],
                 [ds_transmissao_mensagem_prodesp],
                 [cd_transmissao_item_status_siafem],
                 [dt_transmissao_item_transmitido_siafem],
                 [fl_transmissao_item_siafem],
                 [ds_transmissao_item_mensagem_siafem],
                 [dt_confirmacao]
        FROM     [contaunica].[tb_autorizacao_ob_itens]
        ORDER BY [id_autorizacao_ob_item] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_autorizacao_ob_itens] OFF;
    END

DROP TABLE [contaunica].[tb_autorizacao_ob_itens];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_autorizacao_ob_itens]', N'tb_autorizacao_ob_itens';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_autorizacao_ob_itens1]', N'PK_tb_autorizacao_ob_itens', N'OBJECT';

GO
PRINT N'Starting rebuilding table [contaunica].[tb_programacao_desembolso_execucao]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_programacao_desembolso_execucao] (
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
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_programacao_desembolso_execucao1] PRIMARY KEY CLUSTERED ([id_execucao_pd] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_programacao_desembolso_execucao])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_programacao_desembolso_execucao] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_programacao_desembolso_execucao] ([id_execucao_pd], [id_tipo_execucao_pd], [id_tipo_pagamento], [ug_pagadora], [gestao_pagadora], [ug_liquidante], [gestao_liquidante], [unidade_gestora], [gestao], [ano_pd], [valor_total], [nr_agrupamento], [dt_cadastro], [fl_sistema_prodesp], [fl_sistema_siafem_siafisico])
        SELECT   [id_execucao_pd],
                 [id_tipo_execucao_pd],
                 [id_tipo_pagamento],
                 [ug_pagadora],
                 [gestao_pagadora],
                 [ug_liquidante],
                 [gestao_liquidante],
                 [unidade_gestora],
                 [gestao],
                 [ano_pd],
                 [valor_total],
                 [nr_agrupamento],
                 [dt_cadastro],
                 [fl_sistema_prodesp],
                 [fl_sistema_siafem_siafisico]
        FROM     [contaunica].[tb_programacao_desembolso_execucao]
        ORDER BY [id_execucao_pd] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_programacao_desembolso_execucao] OFF;
    END

DROP TABLE [contaunica].[tb_programacao_desembolso_execucao];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_programacao_desembolso_execucao]', N'tb_programacao_desembolso_execucao';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_programacao_desembolso_execucao1]', N'PK_tb_programacao_desembolso_execucao', N'OBJECT';


GO
PRINT N'Altering [contaunica].[tb_programacao_desembolso_execucao_item]...';


GO
ALTER TABLE [contaunica].[tb_programacao_desembolso_execucao_item] ALTER COLUMN [dt_transmissao_transmitido_siafem] DATETIME NULL;


GO
PRINT N'Starting rebuilding table [pagamento].[tb_confirmacao_pagamento_item]...';


GO

CREATE TABLE [pagamento].[tmp_ms_xx_tb_confirmacao_pagamento_item] (
    [id_confirmacao_pagamento_item]           INT            IDENTITY (1, 1) NOT NULL,
    [id_confirmacao_pagamento]                INT            NOT NULL,
    [id_execucao_pd]                          INT            NULL,
    [id_programacao_desembolso_execucao_item] INT            NULL,
    [id_autorizacao_ob]                       INT            NULL,
    [id_autorizacao_ob_item]                  INT            NULL,
    [nr_documento_gerador]                    VARCHAR (22)   NULL,
    [dt_confirmacao]                          DATETIME       NULL,
    [id_tipo_documento]                       INT            NULL,
    [nr_documento]                            VARCHAR (19)   NULL,
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
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_confirmacao_pagamento_item1] PRIMARY KEY CLUSTERED ([id_confirmacao_pagamento_item] ASC, [id_confirmacao_pagamento] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [pagamento].[tb_confirmacao_pagamento_item])
    BEGIN
        SET IDENTITY_INSERT [pagamento].[tmp_ms_xx_tb_confirmacao_pagamento_item] ON;
        INSERT INTO [pagamento].[tmp_ms_xx_tb_confirmacao_pagamento_item] ([id_confirmacao_pagamento_item], [id_confirmacao_pagamento], [id_execucao_pd], [id_programacao_desembolso_execucao_item], [id_autorizacao_ob], [id_autorizacao_ob_item], [dt_confirmacao], [id_tipo_documento], [nr_documento], [id_regional], [id_reclassificacao_retencao], [id_origem], [id_despesa_tipo], [dt_vencimento], [nr_contrato], [cd_obra], [nr_op], [nr_banco_pagador], [nr_agencia_pagador], [nr_conta_pagador], [nr_fonte_siafem], [nr_emprenho], [nr_processo], [nr_nota_fiscal], [nr_nl_documento], [vr_documento], [nr_natureza_despesa], [cd_credor_organizacao], [nr_cnpj_cpf_ug_credor], [ds_referencia], [fl_transmissao_transmitido_prodesp], [cd_transmissao_status_prodesp], [dt_transmissao_transmitido_prodesp], [ds_transmissao_mensagem_prodesp])
        SELECT   [id_confirmacao_pagamento_item],
                 [id_confirmacao_pagamento],
                 [id_execucao_pd],
                 [id_programacao_desembolso_execucao_item],
                 [id_autorizacao_ob],
                 [id_autorizacao_ob_item],
                 [dt_confirmacao],
                 [id_tipo_documento],
                 [nr_documento],
                 [id_regional],
                 [id_reclassificacao_retencao],
                 [id_origem],
                 [id_despesa_tipo],
                 [dt_vencimento],
                 [nr_contrato],
                 [cd_obra],
                 [nr_op],
                 [nr_banco_pagador],
                 [nr_agencia_pagador],
                 [nr_conta_pagador],
                 [nr_fonte_siafem],
                 [nr_emprenho],
                 [nr_processo],
                 [nr_nota_fiscal],
                 [nr_nl_documento],
                 [vr_documento],
                 [nr_natureza_despesa],
                 [cd_credor_organizacao],
                 [nr_cnpj_cpf_ug_credor],
                 [ds_referencia],
                 [fl_transmissao_transmitido_prodesp],
                 [cd_transmissao_status_prodesp],
                 [dt_transmissao_transmitido_prodesp],
                 [ds_transmissao_mensagem_prodesp]
        FROM     [pagamento].[tb_confirmacao_pagamento_item]
        ORDER BY [id_confirmacao_pagamento_item] ASC, [id_confirmacao_pagamento] ASC;
        SET IDENTITY_INSERT [pagamento].[tmp_ms_xx_tb_confirmacao_pagamento_item] OFF;
    END

DROP TABLE [pagamento].[tb_confirmacao_pagamento_item];

EXECUTE sp_rename N'[pagamento].[tmp_ms_xx_tb_confirmacao_pagamento_item]', N'tb_confirmacao_pagamento_item';

EXECUTE sp_rename N'[pagamento].[tmp_ms_xx_constraint_PK_tb_confirmacao_pagamento_item1]', N'PK_tb_confirmacao_pagamento_item', N'OBJECT';


GO
PRINT N'Creating [movimentacao].[tb_tipo_documento_movimentacao]...';


GO
CREATE TABLE [movimentacao].[tb_tipo_documento_movimentacao] (
    [id_tipo_documento_movimentacao] INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_documento_movimentacao] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_documento_movimentacao] PRIMARY KEY CLUSTERED ([id_tipo_documento_movimentacao] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_movimentacao_orcamentaria_mes]...';


GO
CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria_mes] (
    [id_mes]                                                    INT          IDENTITY (1, 1) NOT NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT          NULL,
    [tb_reducao_suplementacao_id_reducao_suplementacao]         INT          NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT          NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT          NULL,
    [nr_agrupamento]                                            INT          NULL,
    [nr_seq]                                                    INT          NULL,
    [ds_mes]                                                    VARCHAR (9)  NULL,
    [vr_mes]                                                    DECIMAL (20) NULL,
    [cd_unidade_gestora]                                        VARCHAR (10) NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria_mes] PRIMARY KEY CLUSTERED ([id_mes] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_cancelamento_movimentacao]...';


GO
CREATE TABLE [movimentacao].[tb_cancelamento_movimentacao] (
    [id_cancelamento_movimentacao]                              INT           IDENTITY (1, 1) NOT NULL,
    [tb_fonte_id_fonte]                                         VARCHAR (10)  NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT           NULL,
    [nr_agrupamento]                                            INT           NULL,
    [nr_seq]                                                    INT           NULL,
    [nr_nota_cancelamento]                                      VARCHAR (15)  NULL,
    [cd_unidade_gestora]                                        VARCHAR (10)  NULL,
    [nr_categoria_gasto]                                        VARCHAR (15)  NULL,
    [ds_observacao]                                             VARCHAR (77)  NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)      NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140) NULL,
    [fg_transmitido_siafem]                                     CHAR (1)      NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140) NULL,
    [valor]                                                     INT           NULL,
    [ds_observacao2]                                            VARCHAR (77)  NULL,
    [ds_observacao3]                                            VARCHAR (77)  NULL,
    [eventoNC]                                                  VARCHAR (10)  NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)  NULL,
    CONSTRAINT [PK_tb_cancelamento_movimentacao] PRIMARY KEY CLUSTERED ([id_cancelamento_movimentacao] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_movimentacao_orcamentaria]...';


GO
CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria] (
    [id_movimentacao_orcamentaria]                                        INT           IDENTITY (1, 1) NOT NULL,
    [nr_agrupamento_movimentacao]                                         INT           NULL,
    [nr_siafem]                                                           VARCHAR (15)  NULL,
    [tb_regional_id_regional]                                             INT           NULL,
    [tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao]       INT           NULL,
    [tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria] INT           NULL,
    [cd_unidade_gestora_emitente]                                         VARCHAR (10)  NULL,
    [cd_gestao_emitente]                                                  VARCHAR (10)  NULL,
    [nr_ano_exercicio]                                                    INT           NULL,
    [fg_transmitido_siafem]                                               CHAR (1)      NULL,
    [bl_transmitido_siafem]                                               BIT           NULL,
    [dt_trasmitido_siafem]                                                DATETIME      NULL,
    [fg_transmitido_prodesp]                                              CHAR (1)      NULL,
    [bl_transmitido_prodesp]                                              BIT           NULL,
    [dt_trasmitido_prodesp]                                               DATETIME      NULL,
    [ds_msgRetornoProdesp]                                                VARCHAR (140) NULL,
    [ds_msgRetornoSiafem]                                                 VARCHAR (140) NULL,
    [bl_cadastro_completo]                                                BIT           NULL,
    [dt_cadastro]                                                         DATETIME      NULL,
    [bl_transmitir_siafem]                                                BIT           NULL,
    [bl_transmitir_prodesp]                                               BIT           NULL,
    [tb_programa_id_programa]                                             INT           NULL,
    [tb_fonte_id_fonte]                                                   VARCHAR (10)  NULL,
    [tb_estrutura_id_estrutura]                                           INT           NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria] PRIMARY KEY CLUSTERED ([id_movimentacao_orcamentaria] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_movimentacao_orcamentaria_evento]...';


GO
CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria_evento] (
    [id_evento]                                                 INT          IDENTITY (1, 1) NOT NULL,
    [cd_evento]                                                 VARCHAR (6)  NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT          NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT          NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT          NULL,
    [nr_agrupamento]                                            INT          NULL,
    [nr_seq]                                                    INT          NULL,
    [cd_inscricao_evento]                                       VARCHAR (10) NULL,
    [cd_classificacao]                                          VARCHAR (10) NULL,
    [cd_fonte]                                                  VARCHAR (10) NULL,
    [rec_despesa]                                               VARCHAR (8)  NULL,
    [vr_evento]                                                 INT          NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria_evento] PRIMARY KEY CLUSTERED ([id_evento] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_distribuicao_movimentacao]...';


GO
CREATE TABLE [movimentacao].[tb_distribuicao_movimentacao] (
    [id_distribuicao_movimentacao]                              INT           IDENTITY (1, 1) NOT NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT           NULL,
    [nr_agrupamento]                                            INT           NULL,
    [nr_seq]                                                    INT           NULL,
    [tb_fonte_id_fonte]                                         VARCHAR (10)  NULL,
    [nr_nota_distribuicao]                                      VARCHAR (15)  NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (10)  NULL,
    [nr_categoria_gasto]                                        VARCHAR (10)  NULL,
    [ds_observacao]                                             VARCHAR (77)  NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)      NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140) NULL,
    [fg_transmitido_siafem]                                     CHAR (1)      NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140) NULL,
    [valor]                                                     INT           NULL,
    [ds_observacao2]                                            VARCHAR (77)  NULL,
    [ds_observacao3]                                            VARCHAR (77)  NULL,
    [eventoNC]                                                  VARCHAR (10)  NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)  NULL,
    CONSTRAINT [PK_tb_distribuicao_movimentacao] PRIMARY KEY CLUSTERED ([id_distribuicao_movimentacao] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_credito_movimentacao]...';


GO
CREATE TABLE [movimentacao].[tb_credito_movimentacao] (
    [id_nota_credito]                                           INT           IDENTITY (1, 1) NOT NULL,
    [tb_programa_id_programa]                                   INT           NULL,
    [tb_fonte_id_fonte]                                         VARCHAR (10)  NULL,
    [tb_estrutura_id_estrutura]                                 INT           NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT           NULL,
    [nr_agrupamento]                                            INT           NULL,
    [nr_seq]                                                    INT           NULL,
    [nr_nota_credito]                                           VARCHAR (15)  NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (15)  NULL,
    [cd_uo]                                                     VARCHAR (10)  NULL,
    [plano_interno]                                             VARCHAR (10)  NULL,
    [vr_credito]                                                INT           NULL,
    [ds_observacao]                                             VARCHAR (77)  NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)      NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140) NULL,
    [fg_transmitido_siafem]                                     CHAR (1)      NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140) NULL,
    [ds_observacao2]                                            VARCHAR (77)  NULL,
    [ds_observacao3]                                            VARCHAR (77)  NULL,
    [eventoNC]                                                  VARCHAR (10)  NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)  NULL,
    [cd_ugo]                                                    VARCHAR (10)  NULL,
    CONSTRAINT [PK_tb_credito_movimentacao] PRIMARY KEY CLUSTERED ([id_nota_credito] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_reducao_suplementacao]...';


GO
CREATE TABLE [movimentacao].[tb_reducao_suplementacao] (
    [id_reducao_suplementacao]                                  INT           IDENTITY (1, 1) NOT NULL,
    [tb_credito_movimentacao_id_nota_credito]                   INT           NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT           NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT           NULL,
    [tb_programa_id_programa]                                   INT           NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT           NULL,
    [nr_agrupamento]                                            INT           NULL,
    [nr_seq]                                                    INT           NULL,
    [nr_suplementacao_reducao]                                  VARCHAR (15)  NULL,
    [fl_proc]                                                   VARCHAR (10)  NULL,
    [nr_processo]                                               VARCHAR (60)  NULL,
    [nr_orgao]                                                  VARCHAR (10)  NULL,
    [nr_obra]                                                   VARCHAR (15)  NULL,
    [flag_red_sup]                                              CHAR (1)      NULL,
    [nr_cnpj_cpf_ug_credor]                                     VARCHAR (15)  NULL,
    [ds_autorizado_supra_folha]                                 VARCHAR (4)   NULL,
    [cd_origem_recurso]                                         VARCHAR (10)  NULL,
    [cd_destino_recurso]                                        VARCHAR (10)  NULL,
    [cd_especificacao_despesa]                                  VARCHAR (10)  NULL,
    [ds_especificacao_despesa]                                  VARCHAR (632) NULL,
    [cd_autorizado_assinatura]                                  VARCHAR (5)   NULL,
    [cd_autorizado_grupo]                                       INT           NULL,
    [cd_autorizado_orgao]                                       CHAR (2)      NULL,
    [ds_autorizado_cargo]                                       VARCHAR (55)  NULL,
    [nm_autorizado_assinatura]                                  VARCHAR (55)  NULL,
    [cd_examinado_assinatura]                                   VARCHAR (5)   NULL,
    [cd_examinado_grupo]                                        INT           NULL,
    [cd_examinado_orgao]                                        CHAR (2)      NULL,
    [ds_examinado_cargo]                                        VARCHAR (55)  NULL,
    [nm_examinado_assinatura]                                   VARCHAR (55)  NULL,
    [cd_responsavel_assinatura]                                 VARCHAR (5)   NULL,
    [cd_responsavel_grupo]                                      INT           NULL,
    [cd_responsavel_orgao]                                      CHAR (2)      NULL,
    [ds_responsavel_cargo]                                      VARCHAR (140) NULL,
    [nm_responsavel_assinatura]                                 VARCHAR (55)  NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)      NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140) NULL,
    [fg_transmitido_siafem]                                     CHAR (1)      NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140) NULL,
    [valor]                                                     INT           NULL,
    [cd_unidade_gestora]                                        VARCHAR (10)  NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)  NULL,
    [TotalQ1]                                                   DECIMAL (20)  NULL,
    [TotalQ2]                                                   DECIMAL (20)  NULL,
    [TotalQ3]                                                   DECIMAL (20)  NULL,
    [TotalQ4]                                                   DECIMAL (20)  NULL,
    CONSTRAINT [PK_tb_reducao_suplementacao] PRIMARY KEY CLUSTERED ([id_reducao_suplementacao] ASC)
);


GO
PRINT N'Creating [movimentacao].[tb_tipo_movimentacao_orcamentaria]...';


GO
CREATE TABLE [movimentacao].[tb_tipo_movimentacao_orcamentaria] (
    [id_tipo_movimentacao_orcamentaria]  INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_movimentacao_orcamentariao] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_movimentacao_orcamentaria] PRIMARY KEY CLUSTERED ([id_tipo_movimentacao_orcamentaria] ASC)
);


GO
PRINT N'Creating [contaunica].[tb_arquivo]...';


GO
CREATE TABLE [contaunica].[tb_arquivo] (
    [id_arquivo] INT IDENTITY (1, 1) NOT NULL,
    [ds_arquivo] INT NULL,
    CONSTRAINT [PK_contaunica]].[tb_arquivo] PRIMARY KEY CLUSTERED ([id_arquivo] ASC)
);


GO
PRINT N'Creating [contaunica].[tb_arquivo_remessa]...';


GO
CREATE TABLE [contaunica].[tb_arquivo_remessa] (
    [id_arquivo_remessa]         INT           IDENTITY (1, 1) NOT NULL,
    [tb_arquivo_id_arquivo]      INT           NULL,
    [nr_geracao_arquivo]         INT           NULL,
    [dt_preparacao_pagamento]    DATETIME      NULL,
    [dt_pagamento]               DATETIME      NULL,
    [cd_assinatura]              VARCHAR (5)   NULL,
    [cd_grupo_assinatura]        VARCHAR (1)   NULL,
    [cd_orgao_assinatura]        VARCHAR (2)   NULL,
    [nm_assinatura]              VARCHAR (55)  NULL,
    [ds_cargo]                   VARCHAR (55)  NULL,
    [cd_contra_assinatura]       VARCHAR (5)   NULL,
    [cd_grupo_contra_assinatura] VARCHAR (1)   NULL,
    [cd_orgao_contra_assinatura] VARCHAR (2)   NULL,
    [nm_contra_assinatura]       VARCHAR (55)  NULL,
    [ds_cargo_contra_assinatura] VARCHAR (55)  NULL,
    [cd_conta]                   INT           NULL,
    [ds_banco]                   VARCHAR (50)  NULL,
    [ds_agencia]                 VARCHAR (50)  NULL,
    [ds_conta]                   VARCHAR (50)  NULL,
    [qt_ordem_pagamento_arquivo] INT           NULL,
    [qt_deposito_arquivo]        INT           NULL,
    [vr_total_pago]              INT           NULL,
    [qt_doc_ted_arquivo]         INT           NULL,
    [dt_cadastro]                DATETIME      NULL,
    [fg_trasmitido_prodesp]      CHAR (1)      NULL,
    [dt_trasmitido_prodesp]      DATETIME      NULL,
    [fg_arquivo_cancelado]       BIT           NULL,
    [id_regional]                SMALLINT      NULL,
    [bl_cadastro_completo]       BIT           NULL,
    [ds_msg_retorno]             VARCHAR (256) NULL,
    [bl_transmitir_prodesp]      BIT           NULL,
    [bl_transmitido_prodesp]     BIT           NULL,
    CONSTRAINT [PK_contaunica.tb_arquivo_remessa] PRIMARY KEY CLUSTERED ([id_arquivo_remessa] ASC)
);


GO
PRINT N'Creating [contaunica].[tb_itens_obs_re]...';


GO
CREATE TABLE [contaunica].[tb_itens_obs_re] (
    [id_tb_itens_obs_re]                                    INT          IDENTITY (1, 1) NOT NULL,
    [tb_impressao_relacao_re_rt_id_impressao_relacao_re_rt] INT          NULL,
    [nr_ob]                                                 VARCHAR (10) NULL,
    [fg_prioridade]                                         BIT          NULL,
    [cd_tipo_oba]                                           INT          NULL,
    [ds_nome_favorecido]                                    VARCHAR (15) NULL,
    [ds_banco_favorecido]                                   VARCHAR (15) NULL,
    [cd_agencia_favorecida]                                 INT          NULL,
    [ds_conta_favorecida]                                   VARCHAR (15) NULL,
    [vl_ob]                                                 INT          NULL,
    CONSTRAINT [PK_tb_itens_obs_re] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_re] ASC)
);


GO
PRINT N'Creating [contaunica].[tb_itens_obs_rt]...';


GO
CREATE TABLE [contaunica].[tb_itens_obs_rt] (
    [id_tb_itens_obs_rt]                                    INT          IDENTITY (1, 1) NOT NULL,
    [tb_impressao_relacao_re_rt_id_impressao_relacao_re_rt] INT          NULL,
    [nr_ob]                                                 VARCHAR (10) NULL,
    [cd_conta_bancaria_emitente]                            INT          NULL,
    [cd_unidade_gestora]                                    INT          NULL,
    [cd_gestao]                                             INT          NULL,
    [ds_mnemonico_ug_favorecida]                            VARCHAR (15) NULL,
    [ds_banco_favorecido]                                   VARCHAR (15) NULL,
    [cd_agencia_favorecida]                                 INT          NULL,
    [ds_conta_favorecida]                                   VARCHAR (15) NULL,
    [vl_ob]                                                 INT          NULL,
    CONSTRAINT [PK_tb_itens_obs_rt] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_rt] ASC)
);


GO

IF (OBJECT_ID('[contaunica].[FK_tb_autorizacao_ob_tb_confirmacao_pagamento]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [contaunica].[FK_tb_autorizacao_ob_tb_confirmacao_pagamento]...';
	ALTER TABLE [contaunica].[tb_autorizacao_ob] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_autorizacao_ob_tb_confirmacao_pagamento] FOREIGN KEY ([id_confirmacao_pagamento]) REFERENCES [pagamento].[tb_confirmacao_pagamento] ([id_confirmacao_pagamento]);
END
GO

IF (OBJECT_ID('[contaunica].[FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [contaunica].[FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao]...';
	ALTER TABLE [contaunica].[tb_programacao_desembolso_execucao_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao] FOREIGN KEY ([id_execucao_pd]) REFERENCES [contaunica].[tb_programacao_desembolso_execucao] ([id_execucao_pd]);
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento] FOREIGN KEY ([id_confirmacao_pagamento]) REFERENCES [pagamento].[tb_confirmacao_pagamento] ([id_confirmacao_pagamento]);
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_origem]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_confirmacao_pagamento_item_tb_origem]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_origem] FOREIGN KEY ([id_origem]) REFERENCES [pagamento].[tb_origem] ([id_origem]);
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao] FOREIGN KEY ([id_reclassificacao_retencao]) REFERENCES [contaunica].[tb_reclassificacao_retencao] ([id_reclassificacao_retencao]);
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo] FOREIGN KEY ([id_despesa_tipo]) REFERENCES [pagamento].[tb_despesa_tipo] ([id_despesa_tipo]);
END
GO

IF (OBJECT_ID('[pagamento].[FK_tb_arquivo_remessa_tb_regional]', 'F') IS NULL)
BEGIN
	PRINT N'Creating [contaunica].[FK_tb_arquivo_remessa_tb_regional]...';
	ALTER TABLE [contaunica].[tb_arquivo_remessa] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_arquivo_remessa_tb_regional] FOREIGN KEY ([id_regional]) REFERENCES [seguranca].[tb_regional] ([id_regional]);
END
GO


PRINT N'Altering [dbo].[PR_AUTORIZACAO_DE_OB_CONSULTAR]...';
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_DE_OB_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_DE_OB_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_DE_OB_CONSULTAR]
	@id_autorizacao_ob int = NULL,
	@ds_numob varchar(12) = NULL 
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			A.[id_autorizacao_ob], 
			B.nr_agrupamento_ob,
			CP.[id_confirmacao_pagamento], 
			A.[id_execucao_pd], 
			A.[nr_agrupamento], 
			A.[ug_pagadora], 
			A.[gestao_pagadora], 
			A.[ug_liquidante], 
			A.[gestao_liquidante], 
			A.[unidade_gestora], 
			A.[gestao], 
			A.[ano_ob], 
			A.[valor_total_autorizacao], 
			A.[qtde_autorizacao], 
			A.[dt_cadastro], 
			A.[nr_contrato], 
			A.[cd_aplicacao_obra],
			B.[id_autorizacao_ob_item], 
			B.[id_autorizacao_ob], 
			B.[ds_numob], 
			B.[ds_numop], 
			B.[nr_documento_gerador], 
			B.[favorecidoDesc], 
			B.[cd_despesa], 
			B.[nr_banco], 
			B.[valor],
			B.[cd_transmissao_item_status_siafem], 
			B.[dt_transmissao_item_transmitido_siafem], 
			B.[ds_transmissao_item_mensagem_siafem],
			A.cd_transmissao_status_siafem,
			A.ds_transmissao_mensagem_siafem,
			A.dt_transmissao_transmitido_siafem,
			A.fl_transmissao_siafem,
			A.[id_tipo_pagamento], 
			A.[fl_confirmacao],
			ISNULL(A.[dt_confirmacao], CP.dt_confirmacao) as dt_confirmacao
	  FROM contaunica.tb_autorizacao_ob (nolock) A
	  LEFT JOIN contaunica.tb_autorizacao_ob_itens (nolock) B ON B.id_autorizacao_ob = A.id_autorizacao_ob
	  LEFT JOIN pagamento.tb_confirmacao_pagamento AS CP (nolock) ON CP.id_autorizacao_ob = A.id_autorizacao_ob
			WHERE 
	  		--	( nullif( @ds_numob, 0 ) is null or B.ds_numob = @ds_numob )
			( nullif( @id_autorizacao_ob, 0 ) is null or A.id_autorizacao_ob = @id_autorizacao_ob )
			ORDER BY 
				A.id_autorizacao_ob

END

/* 
SELECT 'A.' + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT 'B.' + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/
GO


PRINT N'Altering [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]
	@nr_agrupamento int = NULL,
	@id_autorizacao_ob int = NULL,
	@ds_numob varchar(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
		a.id_autorizacao_ob, 
		a.id_autorizacao_ob_item,
		a.id_execucao_pd as nr_agrupamento_ob,
		a.id_execucao_pd,
		a.id_execucao_pd_item,
		UPPER(a.ds_numob) ds_numob, 
		a.ds_numop, 
		a.nr_documento_gerador, 
		a.id_tipo_documento,
		a.nr_documento,
		a.nr_contrato,
		a.favorecidoDesc, 
		a.cd_despesa, 
		a.nr_banco, 
		a.valor,
		b.id_autorizacao_ob, 
		b.id_tipo_pagamento, 
		b.id_execucao_pd, 
		b.nr_agrupamento, 
		b.ug_pagadora, 
		b.gestao_pagadora, 
		b.ug_liquidante, 
		b.gestao_liquidante, 
		b.unidade_gestora, 
		b.gestao, 
		b.ano_ob, 
		b.valor_total_autorizacao, 
		b.qtde_autorizacao, 
		b.dt_cadastro, 
		a.cd_transmissao_item_status_siafem, 
		a.dt_transmissao_item_transmitido_siafem, 
		a.ds_transmissao_item_mensagem_siafem, 
		a.fl_transmissao_item_siafem,
		ISNULL(cpi.id_confirmacao_pagamento, PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 
		--ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 
		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
		ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,
		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
		ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,
		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,
		a.ds_consulta_op_prodesp,
		a.cd_aplicacao_obra
	FROM [contaunica].[tb_autorizacao_ob_itens] (nolock) a
	INNER JOIN	[contaunica].[tb_autorizacao_ob] b (nolock) ON b.id_autorizacao_ob = a.id_autorizacao_ob
	LEFT JOIN pagamento.tb_confirmacao_pagamento c (nolock) ON c.id_confirmacao_pagamento = b.id_confirmacao_pagamento
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item cpi (nolock) ON cpi.id_autorizacao_ob = a.id_autorizacao_ob and cpi.id_autorizacao_ob_item = a.id_autorizacao_ob_item
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item PI_2 (nolock) ON PI_2.id_execucao_pd = a.id_execucao_pd and PI_2.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
	WHERE 1 = 1
	  	AND (nullif( @id_autorizacao_ob, 0 ) is null or a.id_autorizacao_ob = @id_autorizacao_ob )
		AND (@nr_agrupamento IS NULL OR b.nr_agrupamento = @nr_agrupamento)
		AND (@ds_numob IS NULL OR a.ds_numob = @ds_numob)
END
GO
PRINT N'Altering [dbo].[PR_AUTORIZACAO_OB_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_SALVAR] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_SALVAR] 
    @id_autorizacao_ob int = NULL,
	@id_confirmacao_pagamento int = NULL,
	@id_execucao_pd int = NULL,
	@nr_agrupamento int = NULL,
	@unidade_gestora varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ano_ob varchar(4) = NULL,
	@valor_total_autorizacao decimal(18,2) = NULL,
	@qtde_autorizacao int = NULL,
	@dt_cadastro datetime = NULL,
	@cd_transmissao_status_siafem char(1) = NULL,
	@ds_transmissao_mensagem_siafem varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@ug_pagadora varchar(50) = null,
	@id_tipo_pagamento int = NULL,
	@fl_confirmacao bit = 0,
	@dt_confirmacao datetime = null
as
begin

	set nocount on;

	if exists (
		select	id_autorizacao_ob 
		from	[contaunica].[tb_autorizacao_ob](nolock)
		where	id_autorizacao_ob = @id_autorizacao_ob
	)
	begin
		print 'existe'
		update [contaunica].[tb_autorizacao_ob] set 
		nr_agrupamento = @nr_agrupamento, 
		unidade_gestora = @unidade_gestora, 
		gestao = @gestao, 
		ano_ob = @ano_ob, 
		valor_total_autorizacao = @valor_total_autorizacao, 
		qtde_autorizacao = @qtde_autorizacao, 
		cd_transmissao_status_siafem = @cd_transmissao_status_siafem, 
		dt_transmissao_transmitido_siafem = GETDATE(), 
		ds_transmissao_mensagem_siafem = @ds_transmissao_mensagem_siafem, 
		nr_contrato = @nr_contrato, 
		cd_aplicacao_obra = @cd_aplicacao_obra,
		ug_pagadora = @ug_pagadora,
		id_tipo_pagamento = @id_tipo_pagamento, 
		fl_confirmacao = @fl_confirmacao,
		dt_confirmacao = @dt_confirmacao
		where	id_autorizacao_ob = @id_autorizacao_ob

		select @id_autorizacao_ob;

	end
	else
	begin
		print 'no existe'
		print @gestao
		print '16055'
		insert into [contaunica].[tb_autorizacao_ob] (
			id_confirmacao_pagamento, 
			id_execucao_pd, 
			nr_agrupamento, 
			unidade_gestora, 
			gestao, 
			ano_ob, 
			valor_total_autorizacao, 
			qtde_autorizacao, 
			dt_cadastro, 
			cd_transmissao_status_siafem, 
			dt_transmissao_transmitido_siafem, 
			ds_transmissao_mensagem_siafem, 
			nr_contrato, 
			cd_aplicacao_obra,
			ug_pagadora,
			id_tipo_pagamento, 
			fl_confirmacao,
			dt_confirmacao
		)
		values
		(
			@id_confirmacao_pagamento
			, @id_execucao_pd
			, @nr_agrupamento
			, @unidade_gestora
			, @gestao
			, @ano_ob
			, @valor_total_autorizacao
			, @qtde_autorizacao
			, GETDATE()
			, @cd_transmissao_status_siafem
			, GETDATE()
			, @ds_transmissao_mensagem_siafem
			, @nr_contrato
			, @cd_aplicacao_obra
			, @ug_pagadora
			, @id_tipo_pagamento
			, @fl_confirmacao
			, @dt_confirmacao
		)

		select scope_identity()

	end

end

/*

SELECT '' + COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT 'b.' + COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'


SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'


*/
GO
PRINT N'Altering [dbo].[PR_AUTORIZACAO_OB_ITEM_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_SALVAR]   
	@id_autorizacao_ob_item int = NULL,
	@id_autorizacao_ob int = NULL,
	@nr_agrupamento_ob int = NULL,
	@id_execucao_pd int = NULL,
	@id_execucao_pd_item int = NULL,
	@ds_numob varchar(20) = NULL,
	@ds_numop varchar(20) = NULL,
	@id_tipo_documento int = NULL,
	@nr_documento varchar(50) = NULL,
	@nr_contrato varchar(50) = NULL,
	@nr_documento_gerador varchar(50) = NULL,
	@favorecidoDesc varchar(120) = NULL,
	@ug_pagadora varchar(20) = NULL,
	@ug_liquidante varchar(20) = NULL,
	@gestao_pagadora varchar(20) = NULL,
	@gestao_liguidante varchar(20) = NULL,
	@cd_despesa varchar(2) = NULL,
	@nr_banco varchar(30) = NULL,
	@valor varchar(20) = NULL,
	@fl_transmissao_item_siafem bit = NULL,
	@cd_transmissao_item_status_siafem char(1) = NULL,
	@ds_transmissao_item_mensagem_siafem varchar(140) = NULL,
	@dt_confirmacao datetime = NULL,
	@cd_aplicacao_obra varchar(140) = NULL
AS  
BEGIN  

set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_autorizacao_ob_itens
		where	id_autorizacao_ob_item = @id_autorizacao_ob_item
		and		id_autorizacao_ob = @id_autorizacao_ob
	)
	begin
	
		update contaunica.tb_autorizacao_ob_itens set  
			id_autorizacao_ob = @id_autorizacao_ob,
			id_execucao_pd = @id_execucao_pd,
			id_execucao_pd_item = @id_execucao_pd_item,
			nr_agrupamento_ob = @nr_agrupamento_ob,
			ds_numob = @ds_numob, 
			ds_numop = @ds_numop, 
			nr_documento_gerador = @nr_documento_gerador,
			id_tipo_documento = @id_tipo_documento,
			nr_documento = @nr_documento,
			nr_contrato = @nr_contrato,
			favorecidoDesc = @favorecidoDesc,
			ug_pagadora = @ug_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_pagadora = @gestao_pagadora, 
			gestao_liguidante = @gestao_liguidante,  
			cd_despesa = @cd_despesa, 
			nr_banco = @nr_banco, 
			valor = @valor,
			fl_transmissao_item_siafem = @fl_transmissao_item_siafem,
			cd_transmissao_item_status_siafem = @cd_transmissao_item_status_siafem, 
			dt_transmissao_item_transmitido_siafem = GETDATE(), 
			ds_transmissao_item_mensagem_siafem = @ds_transmissao_item_mensagem_siafem, 
			dt_confirmacao = @dt_confirmacao,
			cd_aplicacao_obra = @cd_aplicacao_obra
		WHERE 
			id_autorizacao_ob_item = @id_autorizacao_ob_item 
 
		select @id_autorizacao_ob_item
	end
	else
	begin

		insert into contaunica.tb_autorizacao_ob_itens (
			id_autorizacao_ob,
			id_execucao_pd,
			id_execucao_pd_item,
			nr_agrupamento_ob,
			ds_numob,
			ds_numop,
			nr_documento_gerador,
			id_tipo_documento,
			nr_documento,
			nr_contrato,
			favorecidoDesc,
			ug_pagadora, 
			ug_liquidante, 
			gestao_pagadora, 
			gestao_liguidante, 
			cd_despesa,
			nr_banco,
			valor,
			fl_transmissao_item_siafem,
			cd_transmissao_item_status_siafem, 
			dt_transmissao_item_transmitido_siafem, 
			ds_transmissao_item_mensagem_siafem,
			dt_confirmacao,
			dt_cadastro,
			cd_aplicacao_obra
		)
		values (
			  @id_autorizacao_ob
			, @id_execucao_pd
			, @id_execucao_pd_item
			, @nr_agrupamento_ob
			, @ds_numob
			, @ds_numop
			, @nr_documento_gerador
			, @id_tipo_documento
			, @nr_documento
			, @nr_contrato
			, @favorecidoDesc
			, @ug_pagadora
			, @ug_liquidante
			, @gestao_pagadora
			, @gestao_liguidante
			, @cd_despesa
			, @nr_banco
			, @valor
			, @fl_transmissao_item_siafem
			, @cd_transmissao_item_status_siafem
			, GETDATE() 
			, @ds_transmissao_item_mensagem_siafem
			, @dt_confirmacao
			, GETDATE()
			, @cd_aplicacao_obra
		)			
		select scope_identity();
	end

END

/* 

SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +'('+ CONVERT(VARCHAR(10), ISNULL(CHARACTER_OCTET_LENGTH,0))+') = NULL,' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'-- AND CHARACTER_OCTET_LENGTH IS NOT NULL

*/
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO]  
 @tipo int,  
 @nr_siafem_siafisico varchar(11) = null  
AS  
  
 --DECLARE @tipo int = 1 --PD ou OB  
  
 --IF LEN(@nr_siafem_siafisico) <= 5  
 -- SET @tipo = 2  
  
 --PD  
 IF @tipo = 1  
 BEGIN   
   
  IF EXISTS(  
   SELECT PD.id_programacao_desembolso  
   FROM [contaunica].[tb_programacao_desembolso] PD (nolock)  
   where nr_siafem_siafisico = @nr_siafem_siafisico  
  )  
  BEGIN   
  print 'existe'
   SELECT TOP 1  
     PD.nr_agrupamento  
    ,PD.id_programacao_desembolso  
    ,EI.id_programacao_desembolso_execucao_item  
    ,PD.nr_cnpj_cpf_pgto  
    ,nr_siafem_siafisico  
    ,[PD].[id_tipo_documento]  
    ,[PD].[nr_contrato]  
    ,[PD].[nr_documento]  
    ,[PD].[nr_documento_gerador]  
    ,[PI].cd_transmissao_status_prodesp  
    ,[PI].fl_transmissao_transmitido_prodesp  
    ,[PI].dt_transmissao_transmitido_prodesp  
    ,[PI].ds_transmissao_mensagem_prodesp  
	,EI.ob_cancelada as Cancelado
    ,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico  
    ,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico  
    ,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico  
    ,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico
	, PD.cd_aplicacao_obra
   FROM [contaunica].[tb_programacao_desembolso] PD (nolock)  
   LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PD.nr_siafem_siafisico  
   LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].nr_documento_gerador = EI.nr_documento_gerador  
   where  
    ( @nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico )  
    --AND nr_agrupamento_pd <> 0  
   ORDER BY [PI].cd_transmissao_status_prodesp DESC  
  END  
  
  ELSE  
  
  BEGIN  
  print 'no existe'
   Select TOP 1  
   PDA.nr_agrupamento  
   ,EI.id_programacao_desembolso_execucao_item  
   , PDA.nr_cnpj_cpf_pgto  
   ,PDA.[id_tipo_documento]  
   ,PD.[nr_contrato]  
   ,PDA.[nr_documento]  
   ,PDA.[nr_documento_gerador]  
  
   ,[PI].cd_transmissao_status_prodesp  
   ,[PI].fl_transmissao_transmitido_prodesp  
   ,[PI].dt_transmissao_transmitido_prodesp  
   ,[PI].ds_transmissao_mensagem_prodesp  
  
   ,EI.ob_cancelada as Cancelado
   ,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico  
   ,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico  
   ,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico  
   ,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico  
   , PD.cd_aplicacao_obra  
   from [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock)  
   LEFT JOIN contaunica.tb_programacao_desembolso AS PD ON PD.id_programacao_desembolso = PDA.id_programacao_desembolso
   LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PDA.nr_programacao_desembolso  
   LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].nr_documento_gerador = PDA.nr_documento_gerador  
   WHERE ( @nr_siafem_siafisico IS NULL OR PDA.nr_programacao_desembolso = @nr_siafem_siafisico )  
   --AND nr_agrupamento_pd = 0  
   --ORDER BY EI.id_execucao_pd DESC  
   ORDER BY [PI].cd_transmissao_status_prodesp DESC  
  END  
 END  
   
 ELSE   
 --Tipo = 2 --OB  
 BEGIN  
  IF LEN(@nr_siafem_siafisico) <= 5  
	SET @nr_siafem_siafisico = CONVERT(VARCHAR, YEAR(GETDATE()), 4) + 'OB' + @nr_siafem_siafisico

  SELECT TOP 1  
   PDE.ds_numob  
   ,AI.id_autorizacao_ob  
   ,AI.id_autorizacao_ob_item  
   ,[PI].id_confirmacao_pagamento_item
   ,[PDE].id_execucao_pd  
   ,[PDE].id_programacao_desembolso_execucao_item  
   ,PDE.nr_op as ds_numop  
   ,PDE.ds_consulta_op_prodesp  
   ,PDE.id_execucao_pd AS nr_agrupamento_ob  
   ,[PDE].id_programacao_desembolso_execucao_item  as id_execucao_pd_item  
   ,[PDE].[id_tipo_documento]  
   ,[PDE].[nr_documento]  
   ,[PDE].[nr_contrato]  
   ,[PDE].[nr_documento_gerador]  
   ,[PDE].ob_cancelada as Cancelado
   ,[PI].cd_transmissao_status_prodesp cd_transmissao_status_prodesp -- Transmisso Confirmao de Pagto  
   ,[PI].fl_transmissao_transmitido_prodesp fl_transmissao_transmitido_prodesp  
   ,[PI].dt_transmissao_transmitido_prodesp dt_transmissao_transmitido_prodesp  
   ,[PI].ds_transmissao_mensagem_prodesp ds_transmissao_mensagem_prodesp  
   ,AI.cd_transmissao_item_status_siafem -- Transmisso Autorizao de OB  
   ,AI.fl_transmissao_item_siafem  
   ,AI.dt_transmissao_item_transmitido_siafem  
   ,AI.ds_transmissao_item_mensagem_siafem  
   ,ISNULL(PD.cd_despesa, PDA.cd_despesa) AS cd_despesa  
   ,ISNULL(PD.cd_aplicacao_obra, PD2.cd_aplicacao_obra) AS cd_aplicacao_obra
  FROM contaunica.tb_programacao_desembolso_execucao_item AS PDE   
  LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON RIGHT(PDA.nr_programacao_desembolso,5) = RIGHT(PDE.ds_numpd ,5)
  LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON PD.id_programacao_desembolso = PDA.id_programacao_desembolso
  LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD2 (nolock) ON RIGHT(PD2.nr_siafem_siafisico,5) = RIGHT(PDE.ds_numpd ,5)
  LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON PDE.id_programacao_desembolso_execucao_item = PI.id_programacao_desembolso_execucao_item 
  LEFT JOIN contaunica.tb_autorizacao_ob_itens AS AI ON PDE.id_programacao_desembolso_execucao_item = AI.id_execucao_pd_item
  
  where RIGHT(PDE.ds_numob, 5) = RIGHT(@nr_siafem_siafisico, 5)
  ORDER BY AI.id_autorizacao_ob DESC  
  
 END
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID] --NULL, NULL, NULL, '2018PD00621'
	@tipo int,
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@filtro_nr_documento_gerador  varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@filtro_nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null
AS
BEGIN

	SET NOCOUNT ON;

	CREATE TABLE #tempListaGrid 
		(--pdexecucao
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		[cd_transmissao_status_siafem] [char](1) NULL,
		[fl_transmissao_transmitido_siafem] [bit] NULL,
		[dt_transmissao_transmitido_siafem] [date] NULL,
		[ds_transmissao_mensagem_siafem] varchar(140) NULL,
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		cd_transmissao_status_prodesp varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		dt_cadastro datetime,
		id_tipo_execucao_pd int,
		cd_aplicacao_obra varchar(140)
	)

	DECLARE @nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@dt_confirmacao datetime,
			@nr_contrato varchar(13),
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@dt_cadastro datetime,
			@id_tipo_execucao_pd int,
			@TransmissaoStatusProdesp char(1),
			@cursor_cd_aplicacao_obra varchar(140)

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_execucao CURSOR FOR

		SELECT	ITEM.[id_programacao_desembolso_execucao_item], ITEM.[nr_agrupamento_pd],ITEM.[id_execucao_pd],ITEM.[ds_numpd],ITEM.[ug],ITEM.[gestao],ITEM.[ug_pagadora],ITEM.[ug_liquidante],ITEM.[gestao_pagadora],ITEM.[gestao_liguidante],
				ITEM.[favorecido],ITEM.[favorecidoDesc],ITEM.[ordem],ITEM.[ano_pd],ITEM.[valor],ITEM.[ds_noup],ITEM.[id_execucao_pd],ITEM.[ds_numob],ITEM.[ob_cancelada],ITEM.[fl_sistema_prodesp],
				ITEM.nr_documento_gerador,ITEM.nr_op,ITEM.[cd_transmissao_status_siafem],ITEM.[fl_transmissao_transmitido_siafem],ITEM.[dt_transmissao_transmitido_siafem],ITEM.[ds_transmissao_mensagem_siafem]
				FROM contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) ITEM
		WHERE
		(@ob_cancelada is null or ITEM.ob_cancelada = @ob_cancelada) AND
		(@ds_numpd is null or ITEM.ds_numpd LIKE '%' + @ds_numpd + '%') AND
		(@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%') AND
		(@favorecido is null or ITEM.favorecido = @favorecido) AND
		(@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		(@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_status_siafem = @cd_transmissao_status_siafem)

	OPEN cursor_execucao

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_execucao 
	INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
			@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @dt_confirmacao = null
		set @id_confirmacao_pagamento = null
		set @id_confirmacao_pagamento_item = null
		set @TransmissaoStatusProdesp = null
		set @fl_transmissao_transmitido_prodesp = null
		set @dt_transmissao_transmitido_prodesp = null
		set @ds_transmissao_mensagem_prodesp= null
		SET @cursor_cd_aplicacao_obra = null

		--SELECT @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd
		----Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@TransmissaoStatusProdesp = [pi].cd_transmissao_status_prodesp, 
				@TransmissaoStatusProdesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].cd_transmissao_status_prodesp ELSE 'E' END, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp,
				@ds_transmissao_mensagem_prodesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].ds_transmissao_mensagem_prodesp ELSE 'FCFG404 - PAGAMENTO CONFIRMADO EM ' + CONVERT(VARCHAR, [pi].[dt_transmissao_transmitido_prodesp], 103) END
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	1 = 1
		AND		nr_documento_gerador = @nr_documento_gerador
		--AND		id_execucao_pd = @id_execucao_pd
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao) 
					,@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento) 
					,@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento) 
					,@nr_contrato = ISNULL(ITEM.nr_contrato, PD.nr_contrato) 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador 
					,@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) 
					,@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor)
					,@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
					,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
					,@cursor_cd_aplicacao_obra = PD.cd_aplicacao_obra
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.id_programacao_desembolso = PDA.id_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

		print 'meh'

		INSERT	#tempListaGrid
				(id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto, dt_cadastro, id_tipo_execucao_pd, cd_aplicacao_obra)
		VALUES	(@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto, @dt_cadastro, @id_tipo_execucao_pd, @cursor_cd_aplicacao_obra)

		FETCH NEXT FROM cursor_execucao 
		INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_execucao

	-- Desalocando o cursor
	DEALLOCATE cursor_execucao

	SELECT	* 
	FROM	#tempListaGrid
	WHERE	1 = 1
	AND		(@tipoExecucao IS NULL OR id_tipo_execucao_pd = @tipoExecucao)
	AND		(@filtro_nr_documento_gerador IS NULL OR nr_documento_gerador = @filtro_nr_documento_gerador)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
	AND		(@filtro_nr_contrato is null or nr_contrato = @filtro_nr_contrato )
	AND		(@cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra )

	DROP TABLE #tempListaGrid
	
END
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description:	Procedure para consulta de execuo de pd
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR]
	@id_execucao_pd int = NULL,
	@ds_numob varchar(12) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF ISNULL(@ds_numob, 0) = 0
		/* Listar Execuo de PD*/

		SELECT
			EI.[id_execucao_pd],
			EI.nr_agrupamento_pd,
			E.[id_tipo_execucao_pd], 
			E.[ug_pagadora],
			E.[gestao_pagadora], 
			E.[ug_liquidante], 
			E.[gestao_liquidante], 
			E.[unidade_gestora], 
			E.[gestao], 
			E.[ano_pd], 
			E.[valor_total], 
			E.[nr_agrupamento], 
			E.[dt_cadastro], 
			E.[fl_sistema_prodesp], 
			E.[fl_sistema_siafem_siafisico],
			PD.id_tipo_documento,
			PD.nr_contrato,
			PD.nr_documento,
			PD.nr_documento_gerador,
			CP.id_confirmacao_pagamento,
			CP.ds_transmissao_mensagem_prodesp,			
			E.[id_tipo_pagamento],
			E.[fl_confirmacao],
			ISNULL(E.[dt_confirmacao], CP.dt_confirmacao) as dt_confirmacao
		FROM contaunica.tb_programacao_desembolso_execucao AS E (nolock)
		LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON E.id_execucao_pd = EI.id_execucao_pd 
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON EI.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN pagamento.tb_confirmacao_pagamento AS CP (nolock) ON CP.id_execucao_pd = EI.id_execucao_pd
		--LEFT JOIN pagamento.tb_confirmacao_pagamento_item CPI (nolock) ON CPI.id_programacao_desembolso_execucao_item = EI.id_programacao_desembolso_execucao_item
			WHERE 
	  			( nullif( @id_execucao_pd, 0 ) is null or EI.id_execucao_pd = @id_execucao_pd )
				OR (@id_execucao_pd IS NULL OR EI.nr_agrupamento_pd = @id_execucao_pd)
				AND nr_agrupamento_pd <> 0
			ORDER BY 
				EI.[id_execucao_pd]
	ELSE
		
		/* Listar Autorizao de OB*/

		SELECT
			A.[id_execucao_pd],
			A.[id_tipo_execucao_pd], 
			A.[ug_pagadora], 
			A.[gestao_pagadora], 
			A.[ug_liquidante], 
			A.[gestao_liquidante], 
			A.[unidade_gestora], 
			A.[gestao], 
			A.[ano_pd], 
			A.[valor_total], 
			A.[nr_agrupamento], 
			A.[dt_cadastro], 
			A.[fl_sistema_prodesp], 
			A.[fl_sistema_siafem_siafisico],
			A.[id_tipo_pagamento], 
			A.[fl_confirmacao],
			A.[dt_confirmacao]
	  FROM [contaunica].[tb_programacao_desembolso_execucao] (nolock) A
	  INNER JOIN [contaunica].[tb_programacao_desembolso_execucao_item] (nolock) B ON B.id_execucao_pd = A.id_execucao_pd
			WHERE 
	  			( nullif( @ds_numob, 0 ) is null or B.ds_numob = @ds_numob )
				AND nr_agrupamento_pd <> 0
			ORDER BY 
				A.[id_execucao_pd]

END









/* 

SELECT  + '[' +  COLUMN_NAME + '], ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tempLista 
		(--pdexecucao
		[NumeroAgrupamentoProgramacaoDesembolso] int NULL, -- Nmero do agrupamento da programacao de desembolso
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL, -- Nmero do agrupamento da programacao de desembolso EXECUO
		[fl_sistema_prodesp] [bit] NULL,
		[cd_transmissao_status_siafem] [char](1) NULL,
		[fl_transmissao_transmitido_siafem] [bit] NULL,
		[dt_transmissao_transmitido_siafem] [date] NULL,
		[ds_transmissao_mensagem_siafem] varchar(140) NULL,
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		[cd_transmissao_status_prodesp] varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15)
	)

	DECLARE @ds_numob varchar(50),
			@ob_cancelada bit,
			@ug varchar(50),
			@gestao varchar(50),
			@ug_pagadora varchar(50),
			@ug_liquidante varchar(50),
			@gestao_pagadora varchar(50),
			@gestao_liguidante varchar(50),
			@favorecido varchar(20),
			@favorecidoDesc varchar(120),
			@ordem varchar(50),
			@ano_pd varchar(4),
			@valor varchar(50),
			@ds_noup varchar(1),
			@nr_agrupamento_pd int,
			@fl_sistema_prodesp bit,
			@cd_transmissao_status_siafem char(1),
			@fl_transmissao_transmitido_siafem bit,
			@dt_transmissao_transmitido_siafem date,
			@ds_transmissao_mensagem_siafem varchar(140),
			@nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@cd_transmissao_status_prodesp varchar(1),
			@fl_transmissao_transmitido_prodesp bit,
			@dt_transmissao_transmitido_prodesp datetime,
			@ds_transmissao_mensagem_prodesp varchar(140),
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@nr_contrato varchar(13), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@nr_agrupamento int

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_objects CURSOR FOR

		SELECT	ITEM.id_programacao_desembolso_execucao_item, ITEM.id_execucao_pd, ITEM.ds_numpd, ITEM.ds_numob, ITEM.ob_cancelada, ITEM.ug, ITEM.gestao,ITEM.ug_pagadora, ITEM.ug_liquidante, ITEM.gestao_pagadora, ITEM.gestao_liguidante, 
				ITEM.favorecido, ITEM.favorecidoDesc, ITEM.ordem, ITEM.ano_pd, ITEM.valor, ITEM.ds_noup, ITEM.nr_agrupamento_pd, ITEM.fl_sistema_prodesp, ITEM.cd_transmissao_status_siafem, ITEM.fl_transmissao_transmitido_siafem, 
				ITEM.dt_transmissao_transmitido_siafem, ITEM.ds_transmissao_mensagem_siafem, ITEM.nr_documento_gerador, ITEM.ds_consulta_op_prodesp, ITEM.nr_op 
					
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
	
		WHERE		(@id_programacao_desembolso_execucao_item is null or ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item) AND
	  				(@id_execucao_pd is null or ITEM.id_execucao_pd = @id_execucao_pd) AND
					(@ds_numpd is null or ITEM.ds_numpd = @ds_numpd)
					--AND ITEM.nr_agrupamento_pd <> 0
	
		ORDER BY	ITEM.id_programacao_desembolso_execucao_item

	-- Abrindo Cursor para leitura
	OPEN cursor_objects

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_objects 
	INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
			@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
			@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @dt_confirmacao = null
		set @id_confirmacao_pagamento = null
		set @id_confirmacao_pagamento_item = null
		set @cd_transmissao_status_prodesp = null
		set @fl_transmissao_transmitido_prodesp = null
		set @ds_transmissao_mensagem_prodesp = null

		--Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@cd_transmissao_status_prodesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].cd_transmissao_status_prodesp ELSE 'E' END, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = CASE WHEN id_execucao_pd = @id_execucao_pd THEN [pi].ds_transmissao_mensagem_prodesp ELSE 'FCFG404 - PAGAMENTO CONFIRMADO EM ' + CONVERT(VARCHAR, [pi].[dt_transmissao_transmitido_prodesp], 103) END
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	1 = 1
		--AND		id_execucao_pd = @id_execucao_pd
		AND		nr_documento_gerador = @nr_documento_gerador
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao), 
					@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
					@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
					@nr_contrato = ISNULL(ITEM.nr_contrato, PD.nr_contrato), 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador, 
					@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
					@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
					@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto),
					@nr_agrupamento = ISNULL(PD.nr_agrupamento, PDA.nr_agrupamento)
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		INSERT	#tempLista
				(NumeroAgrupamentoProgramacaoDesembolso, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto)
		VALUES	(@nr_agrupamento, @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto)

		-- Lendo a prxima linha
		FETCH NEXT FROM cursor_objects 
		INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
				@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
				@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_objects

	-- Desalocando o cursor
	DEALLOCATE cursor_objects

	SELECT	NumeroAgrupamentoProgramacaoDesembolso, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
			ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
			nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
			dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto 
	FROM	#tempLista
	
	DROP TABLE #tempLista

END
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description: Procedure para excluso de execusao de pd
-- =================================================================== 


ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR]   
@id_execucao_pd int = NULL,
@id_tipo_execucao_pd int = NULL,
@ug_pagadora varchar(20) = NULL,
@gestao_pagadora varchar(20) = NULL,
@ug_liquidante varchar(20) = NULL,
@gestao_liquidante varchar(20) = NULL,
@unidade_gestora varchar(20) = NULL,
@gestao varchar(20) = NULL,
@ano_pd varchar(20) = NULL,
@valor_total decimal = NULL,
@nr_agrupamento int = NULL,
@dt_cadastro datetime = NULL,
@fl_sistema_prodesp bit = 0,
@fl_sistema_siafem_siafisico bit = 0,
@id_tipo_pagamento int = NULL,
@fl_confirmacao bit = 0,
@dt_confirmacao datetime = null
AS  
BEGIN  

set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_execucao
		where	id_execucao_pd = @id_execucao_pd
	)
	begin
	
		update contaunica.tb_programacao_desembolso_execucao set 
			id_tipo_execucao_pd = @id_tipo_execucao_pd,
			ug_pagadora = @ug_pagadora, 
			gestao_pagadora = @gestao_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_liquidante = @gestao_liquidante, 
			unidade_gestora = @unidade_gestora, 
			gestao = @gestao, 
			ano_pd = @ano_pd, 
			valor_total = @valor_total, 
			nr_agrupamento = @nr_agrupamento, 
			fl_sistema_prodesp = @fl_sistema_prodesp, 
			fl_sistema_siafem_siafisico = @fl_sistema_siafem_siafisico,
			id_tipo_pagamento = @id_tipo_pagamento,
			fl_confirmacao = @fl_confirmacao,
			dt_confirmacao = @dt_confirmacao
		WHERE id_execucao_pd = @id_execucao_pd
 
		select @id_execucao_pd;
	end
	else
	begin

		insert into contaunica.tb_programacao_desembolso_execucao (
			id_tipo_execucao_pd,
			ug_pagadora,
			gestao_pagadora,
			ug_liquidante,
			gestao_liquidante,
			unidade_gestora,
			gestao,
			ano_pd,
			valor_total,
			nr_agrupamento,
			dt_cadastro,
			fl_sistema_prodesp,
			fl_sistema_siafem_siafisico,
			id_tipo_pagamento,
			fl_confirmacao,
			dt_confirmacao
		)
		values (
		  @id_tipo_execucao_pd
		, @ug_pagadora
		, @gestao_pagadora
		, @ug_liquidante
		, @gestao_liquidante
		, @unidade_gestora
		, @gestao
		, @ano_pd
		, @valor_total
		, @nr_agrupamento
		, ISNULL(@dt_cadastro, GETDATE())
		, @fl_sistema_prodesp
		, @fl_sistema_siafem_siafisico
		, @id_tipo_pagamento
		, @fl_confirmacao
		, @dt_confirmacao
		)			
           
		select scope_identity();

	end

END


/* 

SELECT  + '[' +  COLUMN_NAME + '] ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/
GO
PRINT N'Altering [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR] AS BEGIN SET NOCOUNT ON; END')


GO
 -- ==============================================================
-- Author:  Jose Braz
-- Create date: 30/01/2018
-- Description: Procedure para Alterar pagamento.tb_confirmacao_pagamento_item
-- ==============================================================
ALTER PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]
@id_confirmacao_pagamento_item int = NULL  ,
@id_confirmacao_pagamento int = NULL  ,
@id_execucao_pd int = NULL,
@id_programacao_desembolso_execucao_item int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(19) = NULL,
@dt_confirmacao datetime = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int  = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato varchar(20)  = NULL,
@cd_obra int  = NULL,
@nr_op varchar(50) = NULL,
@nr_banco_pagador varchar(10) = NULL,
@nr_agencia_pagador varchar(10) = NULL,
@nr_conta_pagador varchar(10) = NULL,
@nr_fonte_siafem varchar(50) = NULL,
@nr_emprenho varchar(50) = NULL,
@nr_processo varchar(20)  = NULL,
@nr_nota_fiscal int  = NULL,
@nr_nl_documento varchar(20)  = NULL,
@vr_documento decimal(8,2) = NULL,
@nr_natureza_despesa int  = NULL,
@cd_credor_organizacao int  = NULL,
@nr_cnpj_cpf_ug_credor varchar(14)  = NULL,
@vr_total_confirmado decimal(8,2) = NULL,
@ds_referencia nvarchar(100) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL

AS

 SET NOCOUNT ON;

 DECLARE @chave varchar(11),
		@cd_orgao_assinatura varchar(2),
		@nm_reduzido_credor varchar(14)

IF NOT ISNULL(@id_execucao_pd, 0) = 0
BEGIN
	SET @id_origem = 1 --EXEC. PD

	--SELECT	@chave = ds_numpd
	--FROM	contaunica.tb_programacao_desembolso_execucao_item
	--WHERE	id_execucao_pd = @id_execucao_pd
	--AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item	
END
ELSE
	SET @id_origem = 2 --OB

--SELECT		@id_regional = PD.id_regional,
--			@nr_agencia_pagador	= PP.nr_agencia_pgto,
--			@nr_banco_pagador = PP.nr_banco_pgto,
--			@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--			@nr_conta_pagador = PP.nr_conta_pgto,
--			@nr_contrato = PD.nr_contrato,
--			@cd_credor_organizacao = PP.cd_credor_organizacao,
--			@nr_op = PDI.nr_op,
--			@id_tipo_documento = PD.id_tipo_documento,
--			@nr_documento = PD.nr_documento,
--			@id_despesa_tipo = PD.cd_despesa,
--			@dt_vencimento = PD.dt_vencimento,
--			@nr_fonte_siafem = PDE.cd_fonte,
--			@nr_emprenho = SUBSTRING(PD.nr_documento, 1, 9),
--			@nr_processo = PD.nr_processo,
--			@nr_nl_documento = PD.nr_nl_referencia,
--			@vr_documento = PP.vr_documento,
--			@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--			@cd_obra = PD.cd_aplicacao_obra
--FROM		contaunica.tb_programacao_desembolso (NOLOCK) PD
--LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PD.nr_siafem_siafisico
--LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
--LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
--WHERE		nr_siafem_siafisico = @chave

--IF @@ROWCOUNT = 0
--	SELECT		@id_regional = PP.id_regional,
--				@nr_agencia_pagador	= PP.nr_agencia_pgto,
--				@nr_banco_pagador = PP.nr_banco_pgto,
--				@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--				@nr_conta_pagador = PP.nr_conta_pgto,
--				@cd_credor_organizacao = PP.cd_credor_organizacao,
--				@nr_op = PDI.nr_op,
--				@id_tipo_documento = PDA.id_tipo_documento,
--				@nr_documento = PDA.nr_documento,
--				@id_despesa_tipo = PDA.cd_despesa,
--				@dt_vencimento = PDA.dt_vencimento,
--				@nr_fonte_siafem = PDE.cd_fonte,
--				@nr_emprenho = SUBSTRING(PDA.nr_documento, 1, 9),
--				@nr_processo = PDA.nr_processo,
--				@nr_nl_documento = PDA.nr_nl_referencia,
--				@vr_documento = PP.vr_documento,
--				@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--				@nm_reduzido_credor = PDA.nm_reduzido_credor
--	FROM		contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA
--	LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PDA.nr_programacao_desembolso
--	LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
--	LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
--	WHERE		nr_programacao_desembolso = @chave

 IF ISNULL(@id_autorizacao_ob, 0) = 0
 BEGIN

	--UPDATE
	--pagamento.tb_confirmacao_pagamento_item
	--	SET
	----id_confirmacao_pagamento =  @id_confirmacao_pagamento,
	--id_tipo_documento = @id_tipo_documento,
	--nr_documento = @nr_documento,
	--dt_confirmacao = @dt_confirmacao,
	--id_regional =  @id_regional,
	--id_reclassificacao_retencao =  @id_reclassificacao_retencao,
	--id_origem =  @id_origem,
	--id_despesa_tipo =  @id_despesa_tipo,
	--dt_vencimento =  @dt_vencimento,
	--nr_contrato =  @nr_contrato,
	--cd_obra =  @cd_obra,
	--nr_op =  @nr_op,
	--nr_banco_pagador =  @nr_banco_pagador,
	--nr_agencia_pagador =  @nr_agencia_pagador,
	--nr_conta_pagador =  @nr_conta_pagador,
	--nr_fonte_siafem =  @nr_fonte_siafem,
	--nr_emprenho =  @nr_emprenho,
	--nr_processo =  @nr_processo,
	--nr_nota_fiscal =  @nr_nota_fiscal,
	--nr_nl_documento =  @nr_nl_documento,
	--vr_documento =  @vr_documento,
	--nr_natureza_despesa =  @nr_natureza_despesa,
	--cd_credor_organizacao =  @cd_credor_organizacao,
	--nr_cnpj_cpf_ug_credor =  @nr_cnpj_cpf_ug_credor,
	--ds_referencia = @ds_referencia,
	--cd_orgao_assinatura = @cd_orgao_assinatura,
	--nm_reduzido_credor = @nm_reduzido_credor,
	--cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
	--fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,
	--dt_transmissao_transmitido_prodesp = GETDATE(),
	--ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
	--WHERE
	----id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
	----AND id_confirmacao_pagamento = @id_confirmacao_pagamento
	--id_execucao_pd = @id_execucao_pd
	--AND id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

	IF @id_origem = 1
	BEGIN
		--SELECT	*
		--FROM	contaunica.tb_programacao_desembolso_execucao_item
		--WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
	
		DELETE pagamento.tb_confirmacao_pagamento_item WHERE LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		
		INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
					(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
					dt_confirmacao, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
					dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
					nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
					nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
					nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
					fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

		SELECT		@id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PDA.id_tipo_documento, PDA.nr_documento,
					@dt_confirmacao, PDA.id_regional, @id_reclassificacao_retencao, @id_origem, PDA.cd_despesa, 
					PDA.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
					PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PDA.nr_documento, 1, 9), 0), PDA.nr_processo,
					@nr_nota_fiscal, PDA.nr_nl_referencia, ISNULL(PDA.vl_valor/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
					nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, PDA.nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
		FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
		LEFT JOIN	contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA ON PDI.ds_numpd = PDA.nr_programacao_desembolso
		LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
		LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
		WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

		IF @@ROWCOUNT = 0
		BEGIN 
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
					(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
					dt_confirmacao, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
					dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
					nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
					nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
					nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
					fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT	@id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PD.id_tipo_documento, PD.nr_documento,
					@dt_confirmacao, PD.id_regional, @id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
					PD.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
					PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
					@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
					nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
			LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
			LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
			WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		END

	END

END
ELSE
BEGIN

	UPDATE
	pagamento.tb_confirmacao_pagamento_item
		SET
	--id_confirmacao_pagamento =  @id_confirmacao_pagamento,
	id_tipo_documento = @id_tipo_documento,
	nr_documento = @nr_documento,
	dt_confirmacao = @dt_confirmacao,
	id_regional =  @id_regional,
	id_reclassificacao_retencao =  @id_reclassificacao_retencao,
	id_origem =  @id_origem,
	id_despesa_tipo =  @id_despesa_tipo,
	dt_vencimento =  @dt_vencimento,
	nr_contrato =  @nr_contrato,
	cd_obra =  @cd_obra,
	nr_op =  @nr_op,
	nr_banco_pagador =  @nr_banco_pagador,
	nr_agencia_pagador =  @nr_agencia_pagador,
	nr_conta_pagador =  @nr_conta_pagador,
	nr_fonte_siafem =  @nr_fonte_siafem,
	nr_emprenho =  @nr_emprenho,
	nr_processo =  @nr_processo,
	nr_nota_fiscal =  @nr_nota_fiscal,
	nr_nl_documento =  @nr_nl_documento,
	vr_documento =  @vr_documento,
	nr_natureza_despesa =  @nr_natureza_despesa,
	cd_credor_organizacao =  @cd_credor_organizacao,
	nr_cnpj_cpf_ug_credor =  @nr_cnpj_cpf_ug_credor,
	ds_referencia = @ds_referencia,
	cd_orgao_assinatura = @cd_orgao_assinatura,
	nm_reduzido_credor = @nm_reduzido_credor,
	cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
	fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,
	dt_transmissao_transmitido_prodesp = GETDATE(),
	ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
	WHERE
	id_autorizacao_ob = @id_autorizacao_ob
	AND id_autorizacao_ob_item = @id_autorizacao_ob_item
END
GO
PRINT N'Altering [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
---- ==============================================================
---- Author:  Jose Braz
---- Create date: 30/43/2018
---- Description: Procedure para Incluir pagamento.tb_confirmacao_pagamento_item
---- ==============================================================
ALTER PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]
@id_confirmacao_pagamento int,
@id_programacao_desembolso_execucao_item int = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL,
@dt_confirmacao datetime = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(19) = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato varchar(20) = NULL,
@cd_obra varchar(20)  = NULL,
@nr_op varchar(50) = NULL,
@nr_banco_pagador varchar(10) = NULL,
@nr_agencia_pagador varchar(10) = NULL,
@nr_conta_pagador varchar(10) = NULL,
@nr_fonte_siafem varchar(50) = NULL,
@nr_emprenho varchar(50) = NULL,
@nr_processo varchar(20)  = NULL,
@nr_nota_fiscal int  = NULL,
@nr_nl_documento varchar(20)  = NULL,
@vr_documento decimal(8,2) = NULL,
@nr_natureza_despesa int  = NULL,
@cd_credor_organizacao int  = NULL,
@nr_cnpj_cpf_ug_credor varchar(14)  = NULL,
@fl_transmissao bit  = NULL,
@dt_trasmissao datetime  = NULL,
@ds_referencia nvarchar(100) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL
AS
BEGIN
BEGIN TRANSACTION
SET NOCOUNT ON;

DECLARE @chave varchar(11),
		@cd_orgao_assinatura varchar(2),
		@nm_reduzido_credor varchar(14),
		@vl_valor_desdobrado	decimal,
		@EhPDAAgrupamento		bit = 0

	IF @id_origem = 1
	BEGIN
		
		--Verificar se j existe desdobramento confirmado
		IF EXISTS(SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
					AND		id_execucao_pd = @id_execucao_pd)
		BEGIN
			PRINT 'j existe confirmao para o desdobramento ' + LEFT(@nr_documento_gerador, 17)

			--Caso o desdobrado no esteja em algum agrupamento
			IF EXISTS (	SELECT	id_programacao_desembolso_execucao_item 
							FROM	contaunica.tb_programacao_desembolso_execucao_item
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_pd = 0)
			BEGIN
				PRINT 'o desdobramento j possui grupopd'
						
				SELECT	@id_execucao_pd = id_execucao_pd FROM contaunica.tb_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_pd <> 0
				
				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_execucao_pd = @id_execucao_pd,
						id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
				WHERE	nr_documento_gerador = @nr_documento_gerador

				--Atualizar status das mensagens para todos desdobrados
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp,
						cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
						dt_confirmacao = @dt_confirmacao 
				WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_execucao_pd = @id_execucao_pd

			END
			ELSE
			BEGIN
				print 'o documento gerador ' + @nr_documento_gerador + ' ainda no foi agrupado'
				SELECT 0
			END
		END

		IF NOT EXISTS(SELECT	id_confirmacao_pagamento_item
			FROM	pagamento.tb_confirmacao_pagamento_item 
			WHERE	nr_documento_gerador = @nr_documento_gerador)
		BEGIN
			PRINT 'criando confirmacao'

			DECLARE	@nr_cnpj_cpf_credor varchar(15),
			@cd_despesa varchar(20),
			@nr_banco_pgto varchar(30), 
			@nr_agencia_pgto varchar(10), 
			@nr_conta_pgto varchar(15),
			@nr_nl_referencia varchar(11),
			@vl_total decimal(18,2),
			@id_programacao_desembolso int,
			@cd_fonte varchar(10)
				
			print 'meh1'	
			--Dados da Programao de Desembolso (PD)
			SELECT
				@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
				@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
				@nr_contrato = PD.nr_contrato, 
				@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
				@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
				@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento),
				@id_regional = ISNULL(PD.id_regional, PDA.id_regional),
				@cd_despesa = ISNULL(PD.cd_despesa, PDA.cd_despesa),
				@nr_processo = ISNULL(PD.nr_processo, PDA.nr_processo),
				@nr_nl_referencia = ISNULL(PD.nr_nl_referencia, PDA.nr_nl_referencia),
				@vl_total = ISNULL(PD.vl_total, PDA.vl_valor),
				@nm_reduzido_credor = PDA.nm_reduzido_credor,
				@id_programacao_desembolso = ISNULL(PD.id_programacao_desembolso, PDA.id_programacao_desembolso)
			FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
			LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
			LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
			WHERE		ITEM.id_execucao_pd = @id_execucao_pd
			AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

			SELECT		@nr_banco_pgto = PP.nr_banco_pgto, 
						@nr_agencia_pgto = PP.nr_agencia_pgto, 
						@nr_conta_pgto = PP.nr_conta_pgto, 
						@cd_credor_organizacao = PP.cd_credor_organizacao, 
						@cd_orgao_assinatura = PP.cd_orgao_assinatura 
			FROM		contaunica.tb_preparacao_pagamento (NOLOCK) PP
			WHERE		nr_documento = @nr_documento
						
			SELECT		@cd_fonte = PDE.cd_fonte
			FROM		contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE
			WHERE		id_programacao_desembolso = @id_programacao_desembolso
			
			print 'meh2'
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
						nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			VALUES	(@id_confirmacao_pagamento, @id_programacao_desembolso_execucao_item, @id_execucao_pd, @id_tipo_documento, @nr_documento,
					GETDATE(), @nr_documento_gerador, @id_regional, @id_reclassificacao_retencao, @id_origem, @cd_despesa, 
					@dt_vencimento, @nr_contrato, @cd_obra, @nr_op, @nr_banco_pgto, 
					@nr_agencia_pgto, @nr_conta_pgto, @cd_fonte, ISNULL(SUBSTRING(@nr_documento, 1, 9), 0), @nr_processo,
					@nr_nota_fiscal, @nr_nl_referencia, ISNULL(@vl_total/100, 0), @nr_natureza_despesa, @cd_credor_organizacao, 
					@nr_cnpj_cpf_credor, @ds_referencia, @cd_orgao_assinatura, @nm_reduzido_credor, @cd_transmissao_status_prodesp,
					@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp)

		
		END
	END
	ELSE IF @id_origem = 2
	BEGIN
		PRINT 'j existe confirmao para o desdobramento ' + LEFT(@nr_documento_gerador, 17)

		--Verificar se j existe desdobramento confirmado
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
					AND		id_autorizacao_ob = @id_autorizacao_ob)
		BEGIN
			
			--Caso o desdobrado no esteja em algum agrupamento
			IF EXISTS (	    SELECT	id_autorizacao_ob_item 
							FROM	contaunica.tb_autorizacao_ob_itens
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_ob = 0)
			BEGIN
				PRINT 'o desdobramento j possui grupoob'
				IF (@id_autorizacao_ob IS NULL)
				BEGIN				
					SELECT	@id_autorizacao_ob = id_autorizacao_ob FROM contaunica.tb_autorizacao_ob_itens 
					WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_ob <> 0
				END

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_autorizacao_ob = @id_autorizacao_ob,
						id_autorizacao_ob_item = @id_autorizacao_ob_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_autorizacao_ob = @id_autorizacao_ob

				--Atualizar status das mensagens para todos desdobrados
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp,
						cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,
						dt_confirmacao = @dt_confirmacao 
				WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_autorizacao_ob = @id_autorizacao_ob
			END
			ELSE
			BEGIN
				print 'o documento gerador ' + @nr_documento_gerador + ' ainda no foi agrupado'
				SELECT 0
			END		 
		END

		IF NOT EXISTS(SELECT	id_confirmacao_pagamento_item
			FROM	pagamento.tb_confirmacao_pagamento_item 
			WHERE	nr_documento_gerador = @nr_documento_gerador)
		BEGIN
			PRINT '...'
			DELETE pagamento.tb_confirmacao_pagamento_item WHERE LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_execucao_pd, id_programacao_desembolso_execucao_item, id_autorizacao_ob, id_autorizacao_ob_item,
						id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, 
						id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, 
						nr_banco_pagador, nr_agencia_pagador, nr_conta_pagador, 
						nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT		@id_confirmacao_pagamento, @id_execucao_pd, AUI.id_execucao_pd_item, AUI.id_autorizacao_ob, AUI.id_autorizacao_ob_item, 
						PD.id_tipo_documento, PD.nr_documento,
						ISNULL(@dt_confirmacao, GETDATE()), AUI.nr_documento_gerador, PD.id_regional, 
						@id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
						PD.dt_vencimento, @nr_contrato, @cd_obra, AUI.ds_numop, 
						PP.nr_banco_pgto, PP.nr_agencia_pgto, PP.nr_conta_pgto, 
						PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
						@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
						nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
						@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		[contaunica].[tb_autorizacao_ob_itens] (NOLOCK) AUI
			LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numob = AUI.ds_numob AND PDI.id_execucao_pd = AUI.id_execucao_pd
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = AUI.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
			LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
			WHERE 1 = 1
				AND LEFT(AUI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
				AND AUI.id_autorizacao_ob = @id_autorizacao_ob

		END

	END

COMMIT

	IF SCOPE_IDENTITY() IS NOT NULL
	BEGIN
		SELECT SCOPE_IDENTITY() as [id_confirmacao_pagamento_item];
	END
END

-----------------------------------------------------------------------------------------
GO
PRINT N'Altering [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 06/10/2017
-- Description: Procedure para salvar item de pd
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR]   
@id_programacao_desembolso_execucao_item int = NULL,
@id_execucao_pd int = NULL,
@ds_numpd varchar(40) = NULL,
@nr_documento_gerador varchar(50) = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(20) = NULL,
@nr_contrato varchar(13) = NULL,
@nr_op varchar(50) = NULL,
@id_tipo_pagamento int = NULL,
@dt_confirmacao datetime = NULL,
@ug varchar(20) = NULL,
@gestao varchar(20) = NULL,
@ug_pagadora varchar(20) = NULL,
@ug_liquidante varchar(20) = NULL,
@gestao_pagadora varchar(20) = NULL,
@gestao_liguidante varchar(20) = NULL,
@nr_cnpj_cpf_pgto varchar(15) = NULL,
@favorecido varchar(40) = NULL,
@favorecidoDesc varchar(100) = NULL,
@ordem varchar(20) = NULL,
@ano_pd varchar(20) = NULL,
@valor varchar(20) = NULL,
@ds_noup varchar(2) = NULL,
@nr_agrupamento_pd int = NULL,
@ds_numob varchar(50) = NULL,
@ob_cancelada bit = NULL,
@fl_sistema_prodesp bit = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL,
@cd_transmissao_status_siafem char(1) = NULL,
@fl_transmissao_transmitido_siafem bit = NULL,
@dt_transmissao_transmitido_siafem date = NULL,
@ds_transmissao_mensagem_siafem varchar(280) = NULL,
@ds_consulta_op_prodesp varchar(140) = NULL,
@dt_emissao datetime = NULL,
@dt_vencimento datetime = NULL,
@ds_causa_cancelamento varchar(200) = null
AS  
BEGIN  
set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_execucao_item
		where	id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		--and		nr_documento_gerador = @nr_documento_gerador
		--and		nr_agrupamento_pd = 0
	)
	begin
	
		update contaunica.tb_programacao_desembolso_execucao_item set  
			--id_execucao_pd = @id_execucao_pd, 
			ds_numpd = @ds_numpd,
			nr_documento_gerador = @nr_documento_gerador,
			id_tipo_documento = @id_tipo_documento,
			nr_documento = @nr_documento,
			nr_contrato = @nr_contrato,
			nr_op = @nr_op,
			ug = @ug,
			gestao = @gestao,
			ug_pagadora = @ug_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_pagadora = @gestao_pagadora, 
			gestao_liguidante = @gestao_liguidante, 
			favorecido = @favorecido, 
			favorecidoDesc = @favorecidoDesc,
			nr_cnpj_cpf_pgto = @nr_cnpj_cpf_pgto, 
			ordem = @ordem, 
			ano_pd = @ano_pd, 
			valor = @valor, 
			ds_noup = @ds_noup, 
			nr_agrupamento_pd = @nr_agrupamento_pd, 
			ds_numob = @ds_numob,
			ob_cancelada = @ob_cancelada,
			fl_sistema_prodesp = @fl_sistema_prodesp, 
			cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp, 
			fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp, 
			dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp, 
			ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp, 
			cd_transmissao_status_siafem = @cd_transmissao_status_siafem, 
			fl_transmissao_transmitido_siafem = @fl_transmissao_transmitido_siafem, 
			dt_transmissao_transmitido_siafem = GETDATE(),
			ds_transmissao_mensagem_siafem = @ds_transmissao_mensagem_siafem,
			dt_emissao = @dt_emissao,
			dt_vencimento = @dt_vencimento,
			ds_consulta_op_prodesp = @ds_consulta_op_prodesp,
			id_tipo_pagamento = @id_tipo_pagamento,
			dt_confirmacao = @dt_confirmacao,
			ds_causa_cancelamento = @ds_causa_cancelamento
		WHERE 
			id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
 
		select @id_programacao_desembolso_execucao_item;
	end
	else
	begin

		--Select @nr_agrupamento_pd = ISNULL(nr_agrupamento_pd, 0) + 1 from contaunica.tb_programacao_desembolso_execucao_item

		insert into contaunica.tb_programacao_desembolso_execucao_item (
			id_execucao_pd, 
			ds_numpd,
			nr_documento_gerador,
			id_tipo_documento,
			nr_documento,
			nr_contrato,
			nr_op, 
			ug,
			gestao,
			ug_pagadora, 
			ug_liquidante, 
			gestao_pagadora, 
			gestao_liguidante, 
			favorecido, 
			favorecidoDesc,
			nr_cnpj_cpf_pgto,
			ordem, 
			ano_pd, 
			valor, 
			ds_noup, 
			nr_agrupamento_pd, 
			ds_numob,
			ob_cancelada,
			fl_sistema_prodesp, 
			cd_transmissao_status_prodesp, 
			fl_transmissao_transmitido_prodesp, 
			dt_transmissao_transmitido_prodesp, 
			ds_transmissao_mensagem_prodesp, 
			cd_transmissao_status_siafem, 
			fl_transmissao_transmitido_siafem, 
			dt_transmissao_transmitido_siafem, 
			ds_transmissao_mensagem_siafem,
			ds_consulta_op_prodesp,
			dt_emissao,
			dt_vencimento,
			id_tipo_pagamento,
			dt_confirmacao
		)
		values (
			  @id_execucao_pd
			, @ds_numpd
			, @nr_documento_gerador
			, @id_tipo_documento
			, @nr_documento
			, @nr_contrato
			, @nr_op
			, @ug
			, @gestao
			, @ug_pagadora
			, @ug_liquidante
			, @gestao_pagadora
			, @gestao_liguidante
			, @favorecido
			, @favorecidoDesc
			, @nr_cnpj_cpf_pgto
			, @ordem
			, @ano_pd
			, @valor
			, @ds_noup
			, @nr_agrupamento_pd
			, @ds_numob
			, @ob_cancelada
			, @fl_sistema_prodesp
			, @cd_transmissao_status_prodesp
			, @fl_transmissao_transmitido_prodesp
			, @dt_transmissao_transmitido_prodesp
			, @ds_transmissao_mensagem_prodesp
			, @cd_transmissao_status_siafem
			, @fl_transmissao_transmitido_siafem
			, GETDATE()
			, @ds_transmissao_mensagem_siafem
			, @ds_consulta_op_prodesp
			, @dt_emissao
			, @dt_vencimento
			, @id_tipo_pagamento
			, @dt_confirmacao
		)			
		select scope_identity();
	end

END

/* 

SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +'('+ CONVERT(VARCHAR(10), ISNULL(CHARACTER_OCTET_LENGTH,0))+') = NULL,' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'-- AND CHARACTER_OCTET_LENGTH IS NOT NULL

*/
GO
PRINT N'Altering [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_INCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/33/2018
-- Description: Procedure para Incluir pagamento.tb_confirmacao_pagamento
-- ==============================================================
ALTER PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR]
@id_tipo_documento int  = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@ano_referencia varchar(4) = NULL,
@nr_documento varchar(19)  = NULL,
@id_tipo_pagamento int  = NULL,
@dt_confirmacao datetime  = NULL,
@dt_preparacao datetime  = NULL,
@nr_conta varchar(3) = NULL,
@dt_cadastro datetime = NULL,
@dt_modificacao datetime  = NULL,
@vr_total_confirmado decimal(18,2) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(200) = NULL
AS
BEGIN

	DECLARE @nr_agrupamento_novo INT;

	SET @nr_agrupamento_novo = COALESCE((SELECT MAX(nr_agrupamento) FROM pagamento.tb_confirmacao_pagamento), 0) + 1

	BEGIN TRANSACTION
	SET NOCOUNT ON;

	 IF @cd_transmissao_status_prodesp = 'S'
		SET @vr_total_confirmado = ISNULL(@vr_total_confirmado, 0)
	ELSE
		SET @vr_total_confirmado = 0

	INSERT INTO pagamento.tb_confirmacao_pagamento (
	id_tipo_documento,
	id_execucao_pd,
	id_autorizacao_ob,
	ano_referencia,
	--nr_documento,
	id_tipo_pagamento,
	dt_confirmacao,
	--dt_preparacao,
	--nr_conta,
	dt_cadastro,
	dt_modificacao,
	vr_total_confirmado,
	cd_transmissao_status_prodesp,
	fl_transmissao_transmitido_prodesp,
	dt_transmissao_transmitido_prodesp,
	ds_transmissao_mensagem_prodesp
	,nr_agrupamento
	)
	VALUES(
	@id_tipo_documento,
	@id_execucao_pd,
	@id_autorizacao_ob,
	@ano_referencia,
	--@nr_documento,
	@id_tipo_pagamento,
	@dt_confirmacao,
	--@dt_preparacao,
	--@nr_conta,
	GETDATE(),
	@dt_modificacao,
	@vr_total_confirmado,
	@cd_transmissao_status_prodesp,
	@fl_transmissao_transmitido_prodesp,
	@dt_transmissao_transmitido_prodesp,
	@ds_transmissao_mensagem_prodesp
	,@nr_agrupamento_novo)
	COMMIT
	SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------
GO
PRINT N'Altering [dbo].[PR_EMPENHO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_EMPENHO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_EMPENHO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description:	Procedure para consulta de valor de empenho
-- ==============================================================
ALTER PROCEDURE [dbo].[PR_EMPENHO_MES_CONSULTAR]
	@id_empenho_mes			int = null
,	@tb_empenho_id_empenho	int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_empenho_mes
		,	tb_empenho_id_empenho
		,	ds_mes
		,	vr_mes
		FROM empenho.tb_empenho_mes (nolock)
		WHERE 
	  		( nullif( @id_empenho_mes, 0 ) is null or id_empenho_mes = @id_empenho_mes ) and
			( nullif( @tb_empenho_id_empenho, 0 ) is null or tb_empenho_id_empenho = @tb_empenho_id_empenho )

			ORDER BY empenho.tb_empenho_mes.ds_mes
END
GO
PRINT N'Altering [dbo].[PR_EMPENHO_REFORCO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_EMPENHO_REFORCO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================
-- Author:		Carlos Henrique Magalhes
-- Create date: 17/01/2017
-- Description:	Procedure para consulta de valor de reforo de empenho
-- ==============================================================
ALTER PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_CONSULTAR]
	@id_empenho_reforco_mes			int
,	@tb_empenho_reforco_id_empenho_reforco	int
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_empenho_reforco_mes
		,	tb_empenho_reforco_id_empenho_reforco
		,	ds_mes
		,	vr_mes
		FROM empenho.tb_empenho_reforco_mes (nolock)
		WHERE 
	  		( nullif( @id_empenho_reforco_mes, 0 ) is null or id_empenho_reforco_mes = @id_empenho_reforco_mes ) and
			( nullif( @tb_empenho_reforco_id_empenho_reforco, 0 ) is null or tb_empenho_reforco_id_empenho_reforco = @tb_empenho_reforco_id_empenho_reforco )

			ORDER BY empenho.tb_empenho_reforco_mes.ds_mes
END
GO
PRINT N'Altering [dbo].[PR_RECLASSIFICACAO_RETENCAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_RECLASSIFICACAO_RETENCAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para salvar ou alterar reclassificacao retencao
-- ===================================================================  
ALTER procedure [dbo].[PR_RECLASSIFICACAO_RETENCAO_SALVAR] 
	@id_reclassificacao_retencao int= NULL
    ,@id_resto_pagar char(1)= NULL
    ,@id_tipo_reclassificacao_retencao int= NULL
    ,@id_tipo_documento int= NULL
    ,@dt_cadastro date= NULL
    ,@nr_siafem_siafisico varchar(11)= NULL
    ,@nr_contrato varchar(13)= NULL
	,@nr_processo varchar(15) = NULL
    ,@nr_empenho_siafem_siafisico varchar(11)= NULL
    ,@nr_documento varchar(19)= NULL
    ,@cd_unidade_gestora varchar(6)= NULL
    ,@cd_gestao varchar(5)= NULL
    ,@nr_medicao varchar(3)= NULL
    ,@vl_valor int= NULL
    ,@cd_evento varchar(6)= NULL
    ,@ds_inscricao varchar(22)= NULL
    ,@cd_classificacao varchar(9)= NULL
    ,@cd_fonte varchar(10)= NULL
    ,@dt_emissao date= NULL
    ,@nr_cnpj_cpf_credor varchar(15)= NULL
    ,@cd_gestao_credor varchar(140)= NULL
    ,@nr_ano_medicao char(4)= NULL
    ,@nr_mes_medicao char(2)= NULL
    ,@id_regional smallint= NULL
    ,@cd_aplicacao_obra varchar(140)= NULL
    ,@tb_obra_tipo_id_obra_tipo int= NULL
    ,@cd_credor_organizacao int= NULL
    ,@nr_cnpj_cpf_fornecedor varchar(15)= NULL
    ,@ds_normal_estorno char(1)= NULL
    ,@nr_nota_lancamento_medicao varchar(11)= NULL
    ,@nr_cnpj_prefeitura varchar(15)= NULL
    ,@ds_observacao_1 varchar(76)= NULL
    ,@ds_observacao_2 varchar(76)= NULL
    ,@ds_observacao_3 varchar(76)= NULL
    ,@fl_sistema_siafem_siafisico bit= NULL
    ,@cd_transmissao_status_siafem_siafisico char(1)= NULL
    ,@fl_transmissao_transmitido_siafem_siafisico bit= NULL
    ,@dt_transmissao_transmitido_siafem_siafisico date= NULL
    ,@ds_transmissao_mensagem_siafem_siafisico varchar(140)= NULL
    ,@bl_cadastro_completo bit= NULL
	,@nr_ano_exercicio int = null
	,@cd_origem int = null
	,@cd_agrupamento_confirmacao int = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_reclassificacao_retencao](nolock)
		where	id_reclassificacao_retencao = @id_reclassificacao_retencao
	)
	begin

		update [contaunica].[tb_reclassificacao_retencao] set 
			id_resto_pagar	=	@id_resto_pagar
			,id_tipo_reclassificacao_retencao	=	nullif(@id_tipo_reclassificacao_retencao,0)
			,id_tipo_documento	=	nullif(@id_tipo_documento,0)
			,id_regional	=	nullif(@id_regional,0)
			,nr_siafem_siafisico	=	@nr_siafem_siafisico
			,nr_contrato	=	@nr_contrato
			,nr_processo = @nr_processo
			,nr_empenho_siafem_siafisico	=	@nr_empenho_siafem_siafisico
			,nr_documento	=	@nr_documento
			,cd_unidade_gestora	=	@cd_unidade_gestora
			,cd_gestao	=	@cd_gestao
			,nr_medicao	=	@nr_medicao
			,vl_valor	=	@vl_valor
			,cd_evento	=	@cd_evento
			,ds_inscricao	=	@ds_inscricao
			,cd_classificacao	=	@cd_classificacao
			,cd_fonte	=	@cd_fonte
			,dt_emissao	=	@dt_emissao
			,nr_cnpj_cpf_credor	=	@nr_cnpj_cpf_credor
			,cd_gestao_credor	=	@cd_gestao_credor
			,nr_ano_medicao	=	@nr_ano_medicao
			,nr_mes_medicao	=	@nr_mes_medicao
			,cd_aplicacao_obra	=	@cd_aplicacao_obra
			,tb_obra_tipo_id_obra_tipo	=	@tb_obra_tipo_id_obra_tipo
			,cd_credor_organizacao	=	@cd_credor_organizacao
			,nr_cnpj_cpf_fornecedor	=	@nr_cnpj_cpf_fornecedor
			,ds_normal_estorno	=	@ds_normal_estorno
			,nr_nota_lancamento_medicao	=	@nr_nota_lancamento_medicao
			,nr_cnpj_prefeitura	=	@nr_cnpj_prefeitura
			,ds_observacao_1	=	@ds_observacao_1
			,ds_observacao_2	=	@ds_observacao_2
			,ds_observacao_3	=	@ds_observacao_3
			,fl_sistema_siafem_siafisico	=	@fl_sistema_siafem_siafisico
			,cd_transmissao_status_siafem_siafisico	=	@cd_transmissao_status_siafem_siafisico
			,fl_transmissao_transmitido_siafem_siafisico	=	@fl_transmissao_transmitido_siafem_siafisico
			,dt_transmissao_transmitido_siafem_siafisico	=	@dt_transmissao_transmitido_siafem_siafisico
			,ds_transmissao_mensagem_siafem_siafisico	=	@ds_transmissao_mensagem_siafem_siafisico
			,bl_cadastro_completo	=	@bl_cadastro_completo
			,nr_ano_exercicio = @nr_ano_exercicio
			,cd_origem = @cd_origem
			,cd_agrupamento_confirmacao = @cd_agrupamento_confirmacao

		where	id_reclassificacao_retencao = @id_reclassificacao_retencao;

		select @id_reclassificacao_retencao;

	end
	else
	begin

		insert into [contaunica].[tb_reclassificacao_retencao] (
			[id_resto_pagar]
           ,[id_tipo_reclassificacao_retencao]
           ,[id_tipo_documento]
           ,[id_regional]
		   ,[dt_cadastro]
           ,[nr_siafem_siafisico]
           ,[nr_contrato]
		   ,nr_processo
           ,[nr_empenho_siafem_siafisico]
           ,[nr_documento]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[nr_medicao]
           ,[vl_valor]
           ,[cd_evento]
           ,[ds_inscricao]
           ,[cd_classificacao]
           ,[cd_fonte]
           ,[dt_emissao]
           ,[nr_cnpj_cpf_credor]
           ,[cd_gestao_credor]
           ,[nr_ano_medicao]
           ,[nr_mes_medicao]
           ,[cd_aplicacao_obra]
           ,[tb_obra_tipo_id_obra_tipo]
           ,[cd_credor_organizacao]
           ,[nr_cnpj_cpf_fornecedor]
           ,[ds_normal_estorno]
           ,[nr_nota_lancamento_medicao]
           ,[nr_cnpj_prefeitura]
           ,[ds_observacao_1]
           ,[ds_observacao_2]
           ,[ds_observacao_3]
           ,[fl_sistema_siafem_siafisico]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[bl_cadastro_completo]	
		   ,[nr_ano_exercicio]					
		   ,[cd_origem]
		   ,[cd_agrupamento_confirmacao]
		)
		values
		(
			nullif(@id_resto_pagar,'')
			,nullif(@id_tipo_reclassificacao_retencao,0)
			,nullif(@id_tipo_documento,0)
			,nullif(@id_regional,0)
			,getdate()
			,@nr_siafem_siafisico
			,@nr_contrato
			,@nr_processo
			,@nr_empenho_siafem_siafisico
			,@nr_documento
			,@cd_unidade_gestora
			,@cd_gestao
			,@nr_medicao
			,@vl_valor
			,@cd_evento
			,@ds_inscricao
			,@cd_classificacao
			,@cd_fonte
			,@dt_emissao
			,@nr_cnpj_cpf_credor
			,@cd_gestao_credor
			,@nr_ano_medicao
			,@nr_mes_medicao
			,@cd_aplicacao_obra
			,@tb_obra_tipo_id_obra_tipo
			,@cd_credor_organizacao
			,@nr_cnpj_cpf_fornecedor
			,@ds_normal_estorno
			,@nr_nota_lancamento_medicao
			,@nr_cnpj_prefeitura
			,@ds_observacao_1
			,@ds_observacao_2
			,@ds_observacao_3
			,@fl_sistema_siafem_siafisico
			,'N'
			,@fl_transmissao_transmitido_siafem_siafisico
			,@dt_transmissao_transmitido_siafem_siafisico
			,@ds_transmissao_mensagem_siafem_siafisico
			,@bl_cadastro_completo
			,@nr_ano_exercicio
			,@cd_origem
			,@cd_agrupamento_confirmacao
		);

		select scope_identity();

	end

end
GO
PRINT N'Creating [dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ARQUIVO_REMESSA_CONSULTA_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID] AS BEGIN SET NOCOUNT ON; END')


GO
  
  
-- ===================================================================    
-- Author:  Alessandro de Santana  
-- Create date: 22/06/2018  
-- Description: Procedure para consultar arquivo de remessa grid  
-- ===================================================================   
ALTER PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID]  
  @id_arquivo_remessa int = null  
    ,@nr_codigo_conta int = null  
 ,@nr_geracao_arquivo INT = NULL  
 ,@id_regional int = null  
 ,@dt_cadastramento_de date = NULL  
 ,@dt_cadastramento_ate date = NULL  
 ,@fg_trasmitido_prodesp char(1) = null  
 ,@fg_arquivo_cancelado bit = null  
as    
begin    
    
 SET NOCOUNT ON;    
    
SELECT [id_arquivo_remessa]  
      ,[tb_arquivo_id_arquivo]  
      ,[nr_geracao_arquivo]  
      ,[dt_preparacao_pagamento]  
      ,[dt_pagamento]  
      ,[cd_assinatura]  
      ,[cd_grupo_assinatura]  
      ,[cd_orgao_assinatura]  
      ,[nm_assinatura]  
      ,[ds_cargo]  
      ,[cd_contra_assinatura]  
      ,[cd_grupo_contra_assinatura]  
      ,[cd_orgao_contra_assinatura]  
      ,[nm_contra_assinatura]  
      ,[ds_cargo_contra_assinatura]  
      ,[cd_conta]  
      ,[ds_banco]  
      ,[ds_agencia]  
      ,[ds_conta]  
      ,[qt_ordem_pagamento_arquivo]  
      ,[qt_deposito_arquivo]  
      ,[vr_total_pago]  
      ,[qt_doc_ted_arquivo]  
      ,[dt_cadastro]  
      ,[fg_trasmitido_prodesp]  
      ,[dt_trasmitido_prodesp]  
      ,[fg_arquivo_cancelado]  
	  ,[bl_cadastro_completo]
	  ,[ds_msg_retorno]
	  ,[bl_transmitir_prodesp]
	  ,[bl_transmitido_prodesp]
  FROM [contaunica].[tb_arquivo_remessa] (nolock)  
 where  
  
      ( nullif( @id_arquivo_remessa, 0 ) is null or id_arquivo_remessa = @id_arquivo_remessa )    
        and ( nullif( @nr_codigo_conta, 0 ) is null or cd_conta = @nr_codigo_conta )   
        and ( nullif( @nr_geracao_arquivo, 0 ) is null or nr_geracao_arquivo = @nr_geracao_arquivo )  
     and ( nullif( @id_regional, 0 ) is null or id_regional = @id_regional )  
  and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de )   
  and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate )   
  and (@fg_trasmitido_prodesp  is null or fg_trasmitido_prodesp  = @fg_trasmitido_prodesp  )  
  and (@fg_arquivo_cancelado  is null or fg_arquivo_cancelado  = @fg_arquivo_cancelado  )  
  
  
 Order by id_arquivo_remessa  
end
GO
PRINT N'Creating [dbo].[PR_ARQUIVO_REMESSA_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ARQUIVO_REMESSA_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_CONSULTAR]  
 @id_arquivo_remessa int = 0
as    
begin    
    
 SET NOCOUNT ON;    

SELECT [id_arquivo_remessa]
      ,[tb_arquivo_id_arquivo]
      ,[nr_geracao_arquivo]
      ,[dt_preparacao_pagamento]
      ,[dt_pagamento]
      ,[cd_assinatura]
      ,[cd_grupo_assinatura]
      ,[cd_orgao_assinatura]
      ,[nm_assinatura]
      ,[ds_cargo]
      ,[cd_contra_assinatura]
      ,[cd_grupo_contra_assinatura]
      ,[cd_orgao_contra_assinatura]
      ,[nm_contra_assinatura]
      ,[ds_cargo_contra_assinatura]
      ,[cd_conta]
      ,[ds_banco]
      ,[ds_agencia]
      ,[ds_conta]
      ,[qt_ordem_pagamento_arquivo]
      ,[qt_deposito_arquivo]
      ,[vr_total_pago]
      ,[qt_doc_ted_arquivo]
      ,[dt_cadastro]
      ,[fg_trasmitido_prodesp]
      ,[dt_trasmitido_prodesp]
      ,[fg_arquivo_cancelado]
	  ,[bl_cadastro_completo]
	  ,[ds_msg_retorno]
	  ,[bl_transmitir_prodesp]
	  ,[bl_transmitido_prodesp]
  FROM [contaunica].[tb_arquivo_remessa]  (nolock) 
   where  
  ( nullif( @id_arquivo_remessa, 0 ) is null or id_arquivo_remessa = @id_arquivo_remessa )  
 
 Order by id_arquivo_remessa  


  END
GO
PRINT N'Creating [dbo].[PR_ARQUIVO_REMESSA_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ARQUIVO_REMESSA_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_EXCLUIR]     
 @id_arquivo_remessa int = NULL  
AS    
BEGIN    
  
 SET NOCOUNT ON;    
   
 DELETE FROM [contaunica].[tb_arquivo_remessa]
 WHERE id_arquivo_remessa = @id_arquivo_remessa;  
  
 SELECT @@ROWCOUNT;  
  
END
GO
PRINT N'Creating [dbo].[PR_ARQUIVO_REMESSA_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ARQUIVO_REMESSA_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Alessandro de Santana
-- Create date: 29/06/2018
-- Description: Procedure para salvar ou alterar arquivo remessa
-- ===================================================================  
ALTER PROCEDURE [dbo].[PR_ARQUIVO_REMESSA_SALVAR] 

		@id_arquivo_remessa INTEGER  = NULL,
		@tb_arquivo_id_arquivo INTEGER  = NULL,
		@nr_geracao_arquivo INTEGER  = NULL,
		@dt_preparacao_pagamento DATE = NULL,
		@dt_pagamento DATE = NULL,
		@cd_assinatura  VARCHAR(5)  = NULL,
		@cd_grupo_assinatura VARCHAR(1)  = NULL,
		@cd_orgao_assinatura VARCHAR(2)  = NULL,
		@nm_assinatura VARCHAR(55) = NULL,
		@ds_cargo VARCHAR(55) = NULL,
		@cd_contra_assinatura VARCHAR(5)  = NULL,
		@cd_grupo_contra_assinatura VARCHAR(2)  = NULL,
		@cd_orgao_contra_assinatura VARCHAR(2)  = NULL,
		@nm_contra_assinatura VARCHAR(55) = NULL,
		@ds_cargo_contra_assinatura VARCHAR(55) = NULL,
		@cd_conta INTEGER  = NULL,
		@ds_banco VARCHAR(50) = NULL,
		@ds_agencia VARCHAR(50) = NULL,
		@ds_conta VARCHAR(50) = NULL,
		@qt_ordem_pagamento_arquivo INTEGER  = NULL,
		@qt_deposito_arquivo INTEGER  = NULL,
		@vr_total_pago INTEGER  = NULL,
		@qt_doc_ted_arquivo INTEGER  = NULL,
		@dt_cadastro DATE = NULL,
		@fg_trasmitido_prodesp CHAR(1) = NULL,
		@dt_trasmitido_prodesp DATE = NULL,
		@fg_arquivo_cancelado BIT = NULL,
		@id_regional INTEGER  = NULL,
		@bl_cadastro_completo BIT = NULL,
	    @ds_msg_retorno	 VARCHAR(256)  = NULL,
		@bl_transmitir_prodesp BIT = NULL,
		@bl_transmitido_prodesp BIT = NULL



as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_arquivo_remessa](nolock)
		where	id_arquivo_remessa = @id_arquivo_remessa
	)
	begin

		UPDATE [contaunica].[tb_arquivo_remessa]
   SET [tb_arquivo_id_arquivo] = @tb_arquivo_id_arquivo
      ,[nr_geracao_arquivo] = @nr_geracao_arquivo
      ,[dt_preparacao_pagamento] = @dt_preparacao_pagamento
      ,[dt_pagamento] = @dt_pagamento
      ,[cd_assinatura] = @cd_assinatura
      ,[cd_grupo_assinatura] = @cd_grupo_assinatura
      ,[cd_orgao_assinatura] = @cd_orgao_assinatura
      ,[nm_assinatura] = @nm_assinatura
      ,[ds_cargo] = @ds_cargo
      ,[cd_contra_assinatura] = @cd_contra_assinatura
      ,[cd_grupo_contra_assinatura] = @cd_grupo_contra_assinatura
      ,[cd_orgao_contra_assinatura] = @cd_orgao_contra_assinatura
      ,[nm_contra_assinatura] = @nm_contra_assinatura
      ,[ds_cargo_contra_assinatura] = @ds_cargo_contra_assinatura
      ,[cd_conta] = @cd_conta
      ,[ds_banco] = @ds_banco
      ,[ds_agencia] = @ds_agencia
      ,[ds_conta] = @ds_conta
      ,[qt_ordem_pagamento_arquivo] = @qt_ordem_pagamento_arquivo
      ,[qt_deposito_arquivo] = @qt_deposito_arquivo
      ,[vr_total_pago] = @vr_total_pago
      ,[qt_doc_ted_arquivo] = @qt_doc_ted_arquivo
      --,[dt_cadastro] = @dt_cadastro
      ,[fg_trasmitido_prodesp] = @fg_trasmitido_prodesp
      ,[dt_trasmitido_prodesp] = @dt_trasmitido_prodesp
      ,[fg_arquivo_cancelado] = @fg_arquivo_cancelado
      --,[id_regional] = @id_regional
	  ,[ds_msg_retorno] = @ds_msg_retorno
      ,[bl_cadastro_completo] = @bl_cadastro_completo
	  ,[bl_transmitido_prodesp] = @bl_transmitido_prodesp

		where	id_arquivo_remessa = @id_arquivo_remessa;

		select @id_arquivo_remessa;

	end
	else
	begin

      INSERT INTO	[contaunica].[tb_arquivo_remessa]
           (
		    [tb_arquivo_id_arquivo]
           ,[nr_geracao_arquivo]
           ,[dt_preparacao_pagamento]
           ,[dt_pagamento]
           ,[cd_assinatura]
           ,[cd_grupo_assinatura]
           ,[cd_orgao_assinatura]
           ,[nm_assinatura]
           ,[ds_cargo]
           ,[cd_contra_assinatura]
           ,[cd_grupo_contra_assinatura]
           ,[cd_orgao_contra_assinatura]
           ,[nm_contra_assinatura]
           ,[ds_cargo_contra_assinatura]
           ,[cd_conta]
           ,[ds_banco]
           ,[ds_agencia]
           ,[ds_conta]
           ,[qt_ordem_pagamento_arquivo]
           ,[qt_deposito_arquivo]
           ,[vr_total_pago]
           ,[qt_doc_ted_arquivo]
           ,[dt_cadastro]
           ,[fg_trasmitido_prodesp]
           ,[dt_trasmitido_prodesp]
           ,[fg_arquivo_cancelado]
           ,[id_regional]
           ,[bl_cadastro_completo]
		   ,[ds_msg_retorno]
		   ,[bl_transmitir_prodesp]
		   ,[bl_transmitido_prodesp]
		   )
		values
		(
		nullif(@tb_arquivo_id_arquivo,0)
		,nullif(@nr_geracao_arquivo,0)
		,@dt_preparacao_pagamento
		,@dt_pagamento
		,@cd_assinatura
		,@cd_grupo_assinatura
		,@cd_orgao_assinatura
		,@nm_assinatura
		,@ds_cargo
		,@cd_contra_assinatura
		,@cd_grupo_contra_assinatura
		,@cd_orgao_contra_assinatura
		,@nm_contra_assinatura
		,@ds_cargo_contra_assinatura
		,nullif(@cd_conta,0)
		,@ds_banco
		,@ds_agencia
		,@ds_conta
		,nullif(@qt_ordem_pagamento_arquivo,0)
		,nullif(@qt_deposito_arquivo,0)
		,nullif(@vr_total_pago,0)
		,nullif(@qt_doc_ted_arquivo,0)
		,GETDATE()
		,'N'
		,@dt_trasmitido_prodesp
		,0
		,nullif(@id_regional,0)
		,@bl_cadastro_completo
		,@ds_msg_retorno
		,@bl_transmitir_prodesp
		,@bl_transmitido_prodesp
		);

		select scope_identity();

	end

end
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_ITEM_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_ITEM_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_ITEM_GRID] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_ITEM_GRID]
	@tipo int,
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@filtro_cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null

AS

BEGIN
	
	SET NOCOUNT ON;

	CREATE TABLE #tempListaAutorizacaoGrid 
		(--ob_autorizacao
		id_autorizacao_ob int NULL,
		id_autorizacao_ob_item int NULL,
		nr_agrupamento_ob int NULL,
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		
		[cd_transmissao_item_status_siafem] [char](1) NULL,
		[fl_transmissao_item_siafem] [bit] NULL,
		[dt_transmissao_item_transmitido_siafem] [date] NULL,
		[ds_transmissao_item_mensagem_siafem] varchar(140) NULL,
		
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		cd_transmissao_status_prodesp varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		dt_cadastro datetime,
		id_tipo_execucao_pd int,
		cd_aplicacao_obra varchar(140) NULL
	)

	DECLARE @id_autorizacao_ob int,
			@id_autorizacao_ob_item int,
			@nr_agrupamento_ob int,
			@id_execucao_pd_item int,
			@cd_transmissao_item_status_siafem char(1),
			@nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@dt_cadastro datetime,
			@id_tipo_execucao_pd int,
			@TransmissaoStatusProdesp char(1),
			@GestaoPagadora varchar(50),
			@UGPagadora varchar(50),
			@cd_aplicacao_obra varchar(140)

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_autorizacao CURSOR FOR

		SELECT	ITEM.id_autorizacao_ob, ITEM.id_autorizacao_ob_item, ITEM.id_autorizacao_ob as nr_agrupamento_ob, ITEM.[favorecido] , ITEM.[favorecidoDesc], ITEM.[valor], ITEM.[ds_numob], ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem]
				, ITEM.id_execucao_pd_item, ITEM.id_execucao_pd, ITEM.[dt_cadastro]
				, ITEM.fl_transmissao_item_siafem, ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem], ITEM.[ds_transmissao_item_mensagem_siafem] 
				, ITEM.cd_aplicacao_obra
		FROM	contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		JOIN contaunica.tb_autorizacao_ob (NOLOCK) AOB ON AOB.id_autorizacao_ob = ITEM.id_autorizacao_ob
		
		WHERE	
		--ITEM.nr_agrupamento_ob <> 0 AND
			(ITEM.[ds_numob] IS NOT NULL)
		
			AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
			AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
			AND (@valor is null or ITEM.valor = @valor)
			AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
			AND (@de is null or ITEM.dt_cadastro >= @de )
			AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
			AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)
			AND (@filtro_cd_aplicacao_obra is null or ITEM.cd_aplicacao_obra = @filtro_cd_aplicacao_obra)

	OPEN cursor_autorizacao

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_autorizacao 
	INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem 
					,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
					,@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
		----Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@TransmissaoStatusProdesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_execucao_pd_item
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao) 
					,@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento) 
					,@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento) 
					,@nr_contrato = PD.nr_contrato 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador 
					,@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) 
					,@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor)
					,@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
					--,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
					,@ug_liquidante = ITEM.ug_liquidante
					,@UGPagadora = ITEM.ug_pagadora
					,@gestao_liguidante = ITEM.gestao_liguidante
					,@GestaoPagadora = ITEM.gestao_pagadora
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_execucao_pd_item

		INSERT	#tempListaAutorizacaoGrid
				(id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro, 
				fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem,
				dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
				,cd_aplicacao_obra, valor)
		VALUES	(@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd_item, @id_execucao_pd, @ds_numpd, @ds_numob, @ug_liquidante, @UGPagadora, @gestao_liguidante, @GestaoPagadora, @dt_cadastro,
				@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem,
				@dt_confirmacao, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp
				,@cd_aplicacao_obra, @valor)

		FETCH NEXT FROM cursor_autorizacao 
		INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem
						,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
						,@fl_transmissao_transmitido_siafem, @cd_transmissao_item_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_autorizacao

	-- Desalocando o cursor
	DEALLOCATE cursor_autorizacao

	SELECT	id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro
			,fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem 
			,dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
			, cd_aplicacao_obra, valor
	FROM	#tempListaAutorizacaoGrid
	WHERE	(@ug_pagadora is null or ug_pagadora = @ug_pagadora)
	AND		(@gestao_pagadora is null or gestao_pagadora = @gestao_pagadora)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	DROP TABLE #tempListaAutorizacaoGrid
	
END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS]   
	@id_autorizacao_ob int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_autorizacao_ob_itens]
	WHERE	nr_agrupamento_ob = 0
	AND		cd_transmissao_item_status_siafem IS NULL
	AND		NOT EXISTS (SELECT	id_autorizacao_ob 
						FROM	pagamento.tb_confirmacao_pagamento_item WHERE [contaunica].[tb_autorizacao_ob_itens].id_autorizacao_ob = pagamento.tb_confirmacao_pagamento_item.id_autorizacao_ob
																		AND	 LEFT([contaunica].[tb_autorizacao_ob_itens].nr_documento_gerador, 17) = LEFT(pagamento.tb_confirmacao_pagamento_item.nr_documento_gerador, 17))

	SELECT @@ROWCOUNT;

END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW]
--DECLARE
	@nr_agrupamento int = NULL,
	@id_autorizacao_ob int = 82,
	@ds_numob varchar(20) = NULL

AS
BEGIN
	/*

	SELECT		a.id_autorizacao_ob, 		a.id_autorizacao_ob_item,		a.nr_agrupamento_ob,		a.id_execucao_pd,		a.id_execucao_pd_item,		a.ds_numob, 		a.ds_numop, 		a.nr_documento_gerador, 		a.id_tipo_documento,
		a.nr_documento,		a.nr_contrato,		a.favorecidoDesc, 		a.cd_despesa, 		a.nr_banco, 		a.valor,		b.id_autorizacao_ob, 		b.id_tipo_pagamento, 		b.id_execucao_pd, 		b.nr_agrupamento, 		b.ug_pagadora, 
		b.gestao_pagadora, 		b.ug_liquidante, 		b.gestao_liquidante, 		b.unidade_gestora, 		b.gestao, 		b.ano_ob, 		b.valor_total_autorizacao, 		b.qtde_autorizacao, 		b.dt_cadastro, 		b.cd_aplicacao_obra,
		a.cd_transmissao_item_status_siafem, 		a.dt_transmissao_item_transmitido_siafem, 		a.ds_transmissao_item_mensagem_siafem, 	a.fl_transmissao_item_siafem,		ISNULL(cpi.id_confirmacao_pagamento, 
		PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 		ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
		ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
		ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,		a.ds_consulta_op_prodesp
	FROM [contaunica].[tb_autorizacao_ob_itens] (nolock) a
	INNER JOIN	[contaunica].[tb_autorizacao_ob] b (nolock) ON b.id_autorizacao_ob = a.id_autorizacao_ob
	LEFT JOIN pagamento.tb_confirmacao_pagamento c (nolock) ON c.id_confirmacao_pagamento = b.id_confirmacao_pagamento
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item cpi (nolock) ON cpi.id_autorizacao_ob = a.id_autorizacao_ob and cpi.id_autorizacao_ob_item = a.id_autorizacao_ob_item
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item PI_2 (nolock) ON PI_2.id_execucao_pd = a.id_execucao_pd and PI_2.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
		--WHERE
		--	ds_numob IS NOT NULL AND 
	  	--	(@nr_agrupamento_pd IS NULL OR nr_agrupamento_pd = @nr_agrupamento_pd)
		WHERE 
	  		--( nullif( @id_programacao_desembolso_execucao_item, 0 ) is null or id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item ) AND
	  		( nullif( @id_autorizacao_ob, 0 ) is null or a.id_autorizacao_ob = @id_autorizacao_ob ) AND
			(@nr_agrupamento IS NULL OR b.nr_agrupamento = @nr_agrupamento) AND
			(@ds_numob IS NULL OR a.ds_numob = @ds_numob)
		--ORDER BY 
			--id_programacao_desembolso_execucao_item
			
	*/


	SET NOCOUNT ON;

	CREATE TABLE #tempConultaItemOB 
		(--autorizacaoOB
		id_autorizacao_ob int NULL,
		id_autorizacao_ob_item int NULL,
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		
		[cd_transmissao_item_status_siafem] [char](1) NULL,
		fl_transmissao_item_siafem [bit] NULL,
		dt_transmissao_item_transmitido_siafem [date] NULL,
		ds_transmissao_item_mensagem_siafem varchar(140) NULL,
		
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[ds_numop] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		
		[cd_transmissao_status_prodesp] varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		cd_despesa varchar(2),
		nr_banco varchar(30)
	)

	DECLARE @id_autorizacao_ob_item int,
			@nr_agrupamento_ob int,
			@id_execucao_pd int,
			@id_execucao_pd_item int,
			@ob_cancelada bit,
			@ug varchar(50),
			@gestao varchar(50),
			@ug_pagadora varchar(50),
			@ug_liquidante varchar(50),
			@gestao_pagadora varchar(50),
			@gestao_liguidante varchar(50),
			@favorecido varchar(20),
			@favorecidoDesc varchar(120),
			@ordem varchar(50),
			@ano_pd varchar(4),
			@valor varchar(50),
			@ds_noup varchar(1),
			@nr_agrupamento_pd int,
			@fl_sistema_prodesp bit,
			@cd_transmissao_status_siafem char(1),
			@fl_transmissao_transmitido_siafem bit,
			@dt_transmissao_transmitido_siafem date,
			@ds_transmissao_mensagem_siafem varchar(140),
			@nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@ds_numop varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@cd_transmissao_status_prodesp varchar(1),
			@fl_transmissao_transmitido_prodesp bit,
			@dt_transmissao_transmitido_prodesp datetime,
			@ds_transmissao_mensagem_prodesp varchar(140),
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@nr_contrato varchar(13), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@cd_despesa varchar(2),
			@nr_banco varchar(30)
			

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_ItemOB CURSOR FOR

		SELECT		a.id_autorizacao_ob, a.id_autorizacao_ob_item, a.nr_agrupamento_ob, a.id_execucao_pd, a.id_execucao_pd_item, a.ds_numob, a.ds_numop, a.nr_documento_gerador, a.id_tipo_documento, a.nr_documento, a.nr_contrato, a.favorecidoDesc,
					a.cd_despesa, a.nr_banco, a.valor,		
					a.cd_transmissao_item_status_siafem, a.dt_transmissao_item_transmitido_siafem, a.ds_transmissao_item_mensagem_siafem

					--b.id_autorizacao_ob, b.id_tipo_pagamento, b.id_execucao_pd, b.nr_agrupamento, b.ug_pagadora, b.gestao_pagadora, b.ug_liquidante, b.gestao_liquidante, b.unidade_gestora, 		b.gestao, 		b.ano_ob, 		b.valor_total_autorizacao, 		b.qtde_autorizacao, 		b.dt_cadastro, 		b.cd_aplicacao_obra,
					--ISNULL(cpi.id_confirmacao_pagamento, 
					--PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 		ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
					--ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
					--ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,		a.ds_consulta_op_prodesp

		FROM		[contaunica].[tb_autorizacao_ob_itens] (nolock) a
		--LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (nolock) b on b.id_execucao_pd = a.id_execucao_pd and b.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
		WHERE		(@id_autorizacao_ob IS NULL OR a.id_autorizacao_ob = @id_autorizacao_ob) AND
					(@ds_numob IS NULL OR a.ds_numob = @ds_numob)

	-- Abrindo Cursor para leitura
	OPEN cursor_ItemOB

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_ItemOB 
	INTO	@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd, @id_execucao_pd_item, @ds_numob, @ds_numop, @nr_documento_gerador, @id_tipo_documento, @nr_documento, @nr_contrato, @favorecidoDesc,
			@cd_despesa, @nr_banco, @valor, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
			
	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		--SELECT @id_execucao_pd, @id_execucao_pd_item

		--Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@cd_transmissao_status_prodesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_execucao_pd_item
		--AND		id_autorizacao_ob_item = @id_autorizacao_ob_item
		
		IF @dt_confirmacao IS NULL
		BEGIN
			
			SELECT	@dt_confirmacao = [pi].dt_confirmacao,
					@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
					@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
					@cd_transmissao_status_prodesp = [pi].cd_transmissao_status_prodesp, 
					@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
					@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
					@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
			FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
			WHERE	id_autorizacao_ob = @id_autorizacao_ob
			AND		id_autorizacao_ob_item = @id_autorizacao_ob_item
		END

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao), 
					@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
					@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
					@nr_contrato = PD.nr_contrato, 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador, 
					@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
					@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
					@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_execucao_pd_item
				
		INSERT	#tempConultaItemOB
				(id_autorizacao_ob, id_autorizacao_ob_item, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_item_status_siafem, fl_transmissao_item_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				ds_numop, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto, cd_despesa, nr_banco)
		VALUES	(@id_autorizacao_ob, @id_autorizacao_ob_item, @id_execucao_pd_item, @id_execucao_pd, @ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@ds_numop, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto, @cd_despesa, @nr_banco)

		-- Lendo a prxima linha
		FETCH NEXT FROM cursor_ItemOB 
		INTO	@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd, @id_execucao_pd_item, @ds_numob, @ds_numop, @nr_documento_gerador, @id_tipo_documento, @nr_documento, @nr_contrato, @favorecidoDesc,
				@cd_despesa, @nr_banco, @valor, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem

	END

	-- Fechando Cursor para leitura
	CLOSE cursor_ItemOB

	-- Desalocando o cursor
	DEALLOCATE cursor_ItemOB

	SELECT * FROM #tempConultaItemOB
	--SELECT * FROM pagamento.tb_confirmacao_pagamento_item WHERE id_autorizacao_ob = @id_autorizacao_ob
	--SELECT * FROM pagamento.tb_confirmacao_pagamento_item WHERE id_execucao_pd IN (563)-- and id_programacao_desembolso_execucao_item IN (2186,2190,2203) and nr_documento_gerador IS NOT NULL
	DROP TABLE #tempConultaItemOB

END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD]
	@nr_agrupamento int = NULL,
	@id_autorizacao_ob int = NULL,
	@ds_numob varchar(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		a.id_autorizacao_ob, 
		a.id_autorizacao_ob_item,
		a.nr_agrupamento_ob,
		a.id_execucao_pd,
		a.id_execucao_pd_item,
		a.ds_numob, 
		a.ds_numop, 
		a.nr_documento_gerador, 
		a.id_tipo_documento,
		a.nr_documento,
		a.nr_contrato,
		a.favorecidoDesc, 
		a.cd_despesa, 
		a.nr_banco, 
		a.valor,
		b.id_autorizacao_ob, 
		b.id_tipo_pagamento, 
		b.id_execucao_pd, 
		b.nr_agrupamento, 
		b.ug_pagadora, 
		b.gestao_pagadora, 
		b.ug_liquidante, 
		b.gestao_liquidante, 
		b.unidade_gestora, 
		b.gestao, 
		b.ano_ob, 
		b.valor_total_autorizacao, 
		b.qtde_autorizacao, 
		b.dt_cadastro, 
		b.cd_aplicacao_obra,
		a.cd_transmissao_item_status_siafem, 
		a.dt_transmissao_item_transmitido_siafem, 
		a.ds_transmissao_item_mensagem_siafem, 
		a.fl_transmissao_item_siafem,
		ISNULL(cpi.id_confirmacao_pagamento, PI_2.id_confirmacao_pagamento) id_confirmacao_pagamento, 
		ISNULL(cpi.id_confirmacao_pagamento_item, PI_2.id_confirmacao_pagamento_item) id_confirmacao_pagamento_item, 
		ISNULL(cpi.cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp,
		ISNULL(cpi.fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp,
		ISNULL(cpi.dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp,
		ISNULL(cpi.ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp,
		ISNULL(cpi.dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao,
		a.ds_consulta_op_prodesp
	FROM [contaunica].[tb_autorizacao_ob_itens] (nolock) a
	INNER JOIN	[contaunica].[tb_autorizacao_ob] b (nolock) ON b.id_autorizacao_ob = a.id_autorizacao_ob
	LEFT JOIN pagamento.tb_confirmacao_pagamento c (nolock) ON c.id_confirmacao_pagamento = b.id_confirmacao_pagamento
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item cpi (nolock) ON cpi.id_autorizacao_ob = a.id_autorizacao_ob and cpi.id_autorizacao_ob_item = a.id_autorizacao_ob_item
	LEFT JOIN pagamento.tb_confirmacao_pagamento_item PI_2 (nolock) ON PI_2.id_execucao_pd = a.id_execucao_pd and PI_2.id_programacao_desembolso_execucao_item = a.id_execucao_pd_item
		--WHERE
		--	ds_numob IS NOT NULL AND 
	  	--	(@nr_agrupamento_pd IS NULL OR nr_agrupamento_pd = @nr_agrupamento_pd)
		WHERE 
	  		--( nullif( @id_programacao_desembolso_execucao_item, 0 ) is null or id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item ) AND
	  		( nullif( @id_autorizacao_ob, 0 ) is null or a.id_autorizacao_ob = @id_autorizacao_ob ) AND
			(@nr_agrupamento IS NULL OR b.nr_agrupamento = @nr_agrupamento) AND
			(@ds_numob IS NULL OR a.ds_numob = @ds_numob)
		--ORDER BY 
			--id_programacao_desembolso_execucao_item
END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_ITEM_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 14/08/2018
-- Description: Procedure para excluso de autorizao de pd
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_EXCLUIR]
	@id_autorizacao_ob_item int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_autorizacao_ob_itens]
	WHERE id_autorizacao_ob_item = @id_autorizacao_ob_item

	SELECT @@ROWCOUNT;

END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_ITEM_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_ITEM_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_GRID] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_ITEM_GRID]
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@filtro_cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null

AS

BEGIN
	
	SET NOCOUNT ON;

	CREATE TABLE #tempListaAutorizacaoGrid 
		(--ob_autorizacao
		id_autorizacao_ob int NULL,
		id_autorizacao_ob_item int NULL,
		nr_agrupamento_ob int NULL,
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		
		[cd_transmissao_item_status_siafem] [char](1) NULL,
		[fl_transmissao_item_siafem] [bit] NULL,
		[dt_transmissao_item_transmitido_siafem] [date] NULL,
		[ds_transmissao_item_mensagem_siafem] varchar(140) NULL,
		
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		cd_transmissao_status_prodesp varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		dt_cadastro datetime,
		id_tipo_execucao_pd int,
		cd_aplicacao_obra varchar(140) NULL
	)

	DECLARE @id_autorizacao_ob int,
			@id_autorizacao_ob_item int,
			@nr_agrupamento_ob int,
			@id_execucao_pd_item int,
			@cd_transmissao_item_status_siafem char(1),
			@nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@dt_cadastro datetime,
			@id_tipo_execucao_pd int,
			@TransmissaoStatusProdesp char(1),
			@GestaoPagadora varchar(50),
			@UGPagadora varchar(50),
			@cd_aplicacao_obra varchar(140)


	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_autorizacao CURSOR FOR

		SELECT	ITEM.id_autorizacao_ob, ITEM.id_autorizacao_ob_item, ITEM.nr_agrupamento_ob, ITEM.[favorecido] , ITEM.[favorecidoDesc], ITEM.[valor], ITEM.[ds_numob], ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem]
				, ITEM.id_execucao_pd_item, ITEM.id_execucao_pd, ITEM.[dt_cadastro]
				, ITEM.fl_transmissao_item_siafem, ITEM.cd_transmissao_item_status_siafem, ITEM.[dt_transmissao_item_transmitido_siafem], ITEM.[ds_transmissao_item_mensagem_siafem] 
				, '3377775'
		FROM	contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		JOIN contaunica.tb_autorizacao_ob (NOLOCK) AOB ON AOB.id_autorizacao_ob = ITEM.id_autorizacao_ob
		
		WHERE	
		--ITEM.nr_agrupamento_ob <> 0 AND
			(ITEM.[ds_numob] IS NOT NULL)
		
			AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
			AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
			AND (@valor is null or ITEM.valor = @valor)
			AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
			AND (@de is null or ITEM.dt_cadastro >= @de )
			AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
			AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)
			AND (@filtro_cd_aplicacao_obra is null or ITEM.cd_aplicacao_obra = @filtro_cd_aplicacao_obra)

	OPEN cursor_autorizacao

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_autorizacao 
	INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem 
					,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
					,@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN
	print @cd_aplicacao_obra
		--SELECT @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd

		----Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@TransmissaoStatusProdesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_execucao_pd_item
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao) 
					,@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento) 
					,@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento) 
					,@nr_contrato = PD.nr_contrato 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador 
					,@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) 
					,@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor)
					,@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
					--,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
					,@ug_liquidante = ITEM.ug_liquidante
					,@UGPagadora = ITEM.ug_pagadora
					,@gestao_liguidante = ITEM.gestao_liguidante
					,@GestaoPagadora = ITEM.gestao_pagadora
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_execucao_pd_item

		INSERT	#tempListaAutorizacaoGrid
				(id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro, 
				fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem,
				dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
				,cd_aplicacao_obra)
		VALUES	(@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @id_execucao_pd_item, @id_execucao_pd, @ds_numpd, @ds_numob, @ug_liquidante, @UGPagadora, @gestao_liguidante, @GestaoPagadora, @dt_cadastro,
				@fl_transmissao_transmitido_siafem, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem,
				@dt_confirmacao, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp
				,@cd_aplicacao_obra)

		FETCH NEXT FROM cursor_autorizacao 
		INTO			@id_autorizacao_ob, @id_autorizacao_ob_item, @nr_agrupamento_ob, @favorecido, @favorecidoDesc, @valor, @ds_numob, @cd_transmissao_status_siafem, @dt_transmissao_transmitido_siafem
						,@id_execucao_pd_item, @id_execucao_pd, @dt_cadastro
						,@fl_transmissao_transmitido_siafem, @cd_transmissao_item_status_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
					,@cd_aplicacao_obra
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_autorizacao

	-- Desalocando o cursor
	DEALLOCATE cursor_autorizacao

	SELECT	id_autorizacao_ob, id_autorizacao_ob_item, nr_agrupamento_ob, id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ug_liquidante, ug_pagadora, gestao_liguidante, gestao_pagadora, dt_cadastro
			,fl_transmissao_item_siafem, cd_transmissao_item_status_siafem, dt_transmissao_item_transmitido_siafem, ds_transmissao_item_mensagem_siafem 
			,dt_confirmacao, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp
			, cd_aplicacao_obra
	FROM	#tempListaAutorizacaoGrid
	WHERE	(@ug_pagadora is null or ug_pagadora = @ug_pagadora)
	AND		(@gestao_pagadora is null or gestao_pagadora = @gestao_pagadora)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	DROP TABLE #tempListaAutorizacaoGrid
	
END
GO
PRINT N'Creating [dbo].[PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_AUTORIZACAO_OB_NUMEROAGRUPAMENTO]
AS

	SELECT	ISNULL(MAX(nr_agrupamento_ob), 0) + 1
	FROM	contaunica.tb_autorizacao_ob_itens (NOLOCK)
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_DELETE]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_DELETE'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_DELETE] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_DELETE
(	
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null
)
As
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  

Begin
	DELETE FROM [pagamento].[tb_confirmacao_pagamento]
    WHERE id_confirmacao_pagamento =  @id_confirmacao_pagamento

End

Begin
	 DELETE FROM [pagamento].[tb_confirmacao_pagamento_item]
     WHERE id_confirmacao_pagamento =  @id_confirmacao_pagamento
	 And id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
End
COMMIT
End
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD] AS BEGIN SET NOCOUNT ON; END')


GO
--USE [dbSIDS]
--GO
--/****** Object:  StoredProcedure [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]    Script Date: 06/07/2018 09:14:59 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- ==============================================================
---- Author:  Jose Braz
---- Create date: 30/43/2018
---- Description: Procedure para Incluir pagamento.tb_confirmacao_pagamento_item
---- ==============================================================
ALTER PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD]
@id_confirmacao_pagamento int,
@id_programacao_desembolso_execucao_item int = NULL,
@id_execucao_pd int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL,
@dt_confirmacao datetime = NULL,
@id_tipo_documento int = NULL,
@nr_documento varchar(19) = NULL,
@id_regional smallint  = NULL,
@id_reclassificacao_retencao int  = NULL,
@id_origem int  = NULL,
@id_despesa_tipo int  = NULL,
@dt_vencimento datetime  = NULL,
@nr_contrato varchar(20) = NULL,
@cd_obra varchar(20)  = NULL,
@nr_op varchar(50) = NULL,
@nr_banco_pagador varchar(10) = NULL,
@nr_agencia_pagador varchar(10) = NULL,
@nr_conta_pagador varchar(10) = NULL,
@nr_fonte_siafem varchar(50) = NULL,
@nr_emprenho varchar(50) = NULL,
@nr_processo varchar(20)  = NULL,
@nr_nota_fiscal int  = NULL,
@nr_nl_documento varchar(20)  = NULL,
@vr_documento decimal(8,2) = NULL,
@nr_natureza_despesa int  = NULL,
@cd_credor_organizacao int  = NULL,
@nr_cnpj_cpf_ug_credor varchar(14)  = NULL,
@fl_transmissao bit  = NULL,
@dt_trasmissao datetime  = NULL,
@ds_referencia nvarchar(100) = NULL,
@cd_transmissao_status_prodesp varchar(1) = NULL,
@fl_transmissao_transmitido_prodesp bit = NULL,
@dt_transmissao_transmitido_prodesp datetime = NULL,
@ds_transmissao_mensagem_prodesp varchar(140) = NULL
AS
BEGIN
BEGIN TRANSACTION
SET NOCOUNT ON;

DECLARE @chave varchar(11),
		@cd_orgao_assinatura varchar(2),
		@nm_reduzido_credor varchar(14),
		--@nr_documento_gerador varchar(22),
		@vl_valor_desdobrado	decimal,
		@EhPDAAgrupamento		bit = 0

IF NOT ISNULL(@id_execucao_pd, 0) = 0
	--PRINT 'chave'
	SET @id_origem = 1 --EXEC. PD
	--SELECT	@chave = ds_numpd
	--FROM	contaunica.tb_programacao_desembolso_execucao_item
	--WHERE	id_execucao_pd = @id_execucao_pd
	--AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item	
ELSE
	SET @id_origem = 2 --OB

--SELECT		@id_regional = PD.id_regional,
--			@nr_agencia_pagador	= PP.nr_agencia_pgto,
--			@nr_banco_pagador = PP.nr_banco_pgto,
--			@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--			@nr_conta_pagador = PP.nr_conta_pgto,
--			@nr_contrato = PD.nr_contrato,
--			@cd_credor_organizacao = PP.cd_credor_organizacao,
--			@nr_op = PDI.nr_op,
--			@id_tipo_documento = PD.id_tipo_documento,
--			@nr_documento = PD.nr_documento,
--			@id_despesa_tipo = PD.cd_despesa,
--			@dt_vencimento = PD.dt_vencimento,
--			@nr_fonte_siafem = PDE.cd_fonte,
--			@nr_emprenho = SUBSTRING(PD.nr_documento, 1, 9),
--			@nr_processo = PD.nr_processo,
--			@nr_nl_documento = PD.nr_nl_referencia,
--			@vl_valor_desdobrado = PD.vl_total,
--			@vr_documento = PP.vr_documento,
--			@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--			@cd_obra = PD.cd_aplicacao_obra,
--			@nr_documento_gerador = PDI.nr_documento_gerador
--FROM		contaunica.tb_programacao_desembolso (NOLOCK) PD
--LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PD.nr_siafem_siafisico
--LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
--LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
--WHERE		nr_siafem_siafisico = @chave

--IF @@ROWCOUNT = 0
--	BEGIN 
--		SELECT		@id_regional = PP.id_regional,
--					@nr_agencia_pagador	= PP.nr_agencia_pgto,
--					@nr_banco_pagador = PP.nr_banco_pgto,
--					@nr_cnpj_cpf_ug_credor = nr_cnpj_cpf_credor,
--					@nr_conta_pagador = PP.nr_conta_pgto,
--					@cd_credor_organizacao = PP.cd_credor_organizacao,
--					@nr_op = PDI.nr_op,
--					@id_tipo_documento = PDA.id_tipo_documento,
--					@nr_documento = PDA.nr_documento,
--					@id_despesa_tipo = PDA.cd_despesa,
--					@dt_vencimento = PDA.dt_vencimento,
--					@nr_fonte_siafem = PDE.cd_fonte,
--					@nr_emprenho = SUBSTRING(PDA.nr_documento, 1, 9),
--					@nr_processo = PDA.nr_processo,
--					@nr_nl_documento = PDA.nr_nl_referencia,
--					@vl_valor_desdobrado = PDA.vl_valor,
--					@vr_documento = PP.vr_documento,
--					@cd_orgao_assinatura = PP.cd_orgao_assinatura,
--					@nm_reduzido_credor = PDA.nm_reduzido_credor,
--					@nr_documento_gerador = PDI.nr_documento_gerador
--		FROM		contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA
--		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numpd = PDA.nr_programacao_desembolso
--		LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
--		LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
--		WHERE		nr_programacao_desembolso = @chave

--		SET @EhPDAAgrupamento = 1
--	END

	--SELECT LEFT(@nr_documento_gerador, 17)

	IF @id_origem = 1
	BEGIN
		
		--Verificar se j existe desdobramento
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
		BEGIN
			
			--Caso o desdobrado no esteja em algum agrupamento
			IF EXISTS (	SELECT	id_programacao_desembolso_execucao_item 
							FROM	contaunica.tb_programacao_desembolso_execucao_item
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_pd = 0)
			BEGIN
								
				SELECT	@id_execucao_pd = id_execucao_pd FROM contaunica.tb_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_pd <> 0

				--PRINT @id_execucao_pd
				--PRINT @id_programacao_desembolso_execucao_item

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_execucao_pd = @id_execucao_pd,
						id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_execucao_pd = @id_execucao_pd

			END
			 
		END
		ELSE
		BEGIN

			DELETE	pagamento.tb_confirmacao_pagamento_item
			WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
			--AND NOT EXISTS (Select id_programacao_desembolso_execucao_item a FROM contaunica.tb_programacao_desembolso_execucao_item WHERE contaunica.tb_programacao_desembolso_execucao_item.id_execucao_pd = pagamento.tb_confirmacao_pagamento_item.id_execucao_pd)
						
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
						nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT		@id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PDA.id_tipo_documento, PDA.nr_documento,
						GETDATE(), PDA.nr_documento_gerador, PDA.id_regional, @id_reclassificacao_retencao, @id_origem, PDA.cd_despesa, 
						PDA.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
						PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PDA.nr_documento, 1, 9), 0), PDA.nr_processo,
						@nr_nota_fiscal, PDA.nr_nl_referencia, ISNULL(PDA.vl_valor/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
						nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, PDA.nm_reduzido_credor, @cd_transmissao_status_prodesp,
						@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
			LEFT JOIN	contaunica.tb_programacao_desembolso_agrupamento (NOLOCK) PDA ON PDI.ds_numpd = PDA.nr_programacao_desembolso
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PDA.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PDA.id_programacao_desembolso
			WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
			--AND			PDI.id_execucao_pd = @id_execucao_pd
			--AND			PDI.nr_agrupamento_pd <> 0

			IF @@ROWCOUNT = 0
			BEGIN 
				INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
						dt_confirmacao, PD.nr_documento_gerador, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
						nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

				SELECT @id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, @id_execucao_pd, PD.id_tipo_documento, PD.nr_documento,
						GETDATE(), @nr_documento_gerador, PD.id_regional, @id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
						PD.dt_vencimento, @nr_contrato, @cd_obra, PDI.nr_op, PP.nr_banco_pgto, 
						PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
						@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
						nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
						@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
				FROM		contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI
				LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
				LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = PD.nr_documento
				LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
				LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
				WHERE		LEFT(PDI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
				--AND			PDI.id_execucao_pd = @id_execucao_pd
				--AND			PDI.nr_agrupamento_pd <> 0
			END

		
		END

		--DELETE contaunica.tb_programacao_desembolso_execucao_item WHERE id_execucao_pd = @id_execucao_pd AND nr_agrupamento_pd = 0

	END
	

	IF @id_origem = 2
	BEGIN

		--Verificar se j existe desdobramento
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
		BEGIN
			
			--Caso o desdobrado no esteja em algum agrupamento
			IF EXISTS (	    SELECT	id_autorizacao_ob_item 
							FROM	contaunica.tb_autorizacao_ob_itens
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_ob = 0)
			BEGIN
								
				SELECT	@id_autorizacao_ob = id_autorizacao_ob FROM contaunica.tb_autorizacao_ob_itens 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_ob <> 0

				--PRINT @id_execucao_pd
				--PRINT @id_programacao_desembolso_execucao_item

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_autorizacao_ob = @id_autorizacao_ob,
						id_autorizacao_ob_item = @id_autorizacao_ob_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

				SELECT	id_confirmacao_pagamento_item
				FROM	pagamento.tb_confirmacao_pagamento_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador
				AND		id_autorizacao_ob = @id_autorizacao_ob

			END
			 
		END
		ELSE
		BEGIN

			DELETE pagamento.tb_confirmacao_pagamento_item WHERE LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
		
			INSERT INTO	pagamento.tb_confirmacao_pagamento_item 
						(id_confirmacao_pagamento, id_programacao_desembolso_execucao_item, id_execucao_pd, id_tipo_documento, nr_documento, 
						dt_confirmacao, nr_documento_gerador, id_regional, id_reclassificacao_retencao, id_origem, id_despesa_tipo, 
						dt_vencimento, nr_contrato, cd_obra, nr_op, nr_banco_pagador, 
						nr_agencia_pagador, nr_conta_pagador, nr_fonte_siafem, nr_emprenho, nr_processo, 
						nr_nota_fiscal, nr_nl_documento, vr_documento, nr_natureza_despesa, cd_credor_organizacao, 
						nr_cnpj_cpf_ug_credor, ds_referencia, cd_orgao_assinatura, nm_reduzido_credor, cd_transmissao_status_prodesp, 
						fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp)

			SELECT		@id_confirmacao_pagamento, id_autorizacao_ob_item, @id_execucao_pd, PD.id_tipo_documento, PD.nr_documento,
						GETDATE(), AUI.nr_documento_gerador, PD.id_regional, @id_reclassificacao_retencao, @id_origem, PD.cd_despesa, 
						PD.dt_vencimento, @nr_contrato, @cd_obra, AUI.ds_numop, PP.nr_banco_pgto, 
						PP.nr_agencia_pgto, PP.nr_conta_pgto, PDE.cd_fonte, ISNULL(SUBSTRING(PD.nr_documento, 1, 9), 0), PD.nr_processo,
						@nr_nota_fiscal, PD.nr_nl_referencia, ISNULL(PD.vl_total/100, 0), @nr_natureza_despesa, PP.cd_credor_organizacao, 
						nr_cnpj_cpf_credor, @ds_referencia, PP.cd_orgao_assinatura, DE.nm_reduzido_credor, @cd_transmissao_status_prodesp,
						@fl_transmissao_transmitido_prodesp, GETDATE(), @ds_transmissao_mensagem_prodesp
			FROM		[contaunica].[tb_autorizacao_ob_itens] (NOLOCK) AUI
			LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) PDI ON PDI.ds_numob = AUI.ds_numob AND PDI.id_execucao_pd = AUI.id_execucao_pd
			LEFT JOIN	contaunica.tb_preparacao_pagamento (NOLOCK) PP ON PP.nr_documento = AUI.nr_documento
			LEFT JOIN	contaunica.tb_programacao_desembolso (NOLOCK) PD ON PDI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	contaunica.tb_programacao_desembolso_evento (NOLOCK) PDE ON PDE.id_programacao_desembolso = PD.id_programacao_desembolso
			LEFT JOIN	contaunica.tb_desdobramento (NOLOCK) DE ON LEFT(DE.nr_contrato, 9) = LEFT(PD.nr_documento, 9)
			WHERE		LEFT(AUI.nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
			--AND			AUI.id_autorizacao_ob = @id_autorizacao_ob
			--AND			AUI.nr_agrupamento_ob <> 0

			--DELETE [contaunica].[tb_autorizacao_ob_itens] WHERE id_autorizacao_ob = @id_autorizacao_ob AND nr_agrupamento_ob = 0

		END

	END


--SELECT DISTINCT * FROM pagamento.tb_confirmacao_pagamento_item WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento
COMMIT

SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]
@id_execucao_pd int = NULL,
@id_programacao_desembolso_execucao_item int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL

AS
BEGIN
BEGIN TRANSACTION
SET NOCOUNT ON;

	IF ISNULL(@id_execucao_pd, 0) <> 0 
	BEGIN 
		--Verificar se j existe desdobramento
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
		BEGIN
			
			--Caso o desdobrado no esteja em algum agrupamento
			IF EXISTS (	SELECT	id_programacao_desembolso_execucao_item 
							FROM	contaunica.tb_programacao_desembolso_execucao_item
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_pd = 0)
			BEGIN
				
				SELECT	@id_execucao_pd = id_execucao_pd FROM contaunica.tb_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_pd <> 0

				--PRINT @id_execucao_pd
				--PRINT @id_programacao_desembolso_execucao_item

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_execucao_pd = @id_execucao_pd,
						id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

			END
			 
		END
	END
	ELSE
	BEGIN
		
		IF ISNULL(@id_autorizacao_ob, 0) <> 0
		BEGIN
			--Verificar se j existe desdobramento
			IF EXISTS(	SELECT	id_confirmacao_pagamento_item
						FROM	pagamento.tb_confirmacao_pagamento_item 
						WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
			BEGIN
			
				--Caso o desdobrado no esteja em algum agrupamento
				IF EXISTS (	SELECT	id_autorizacao_ob_item 
								FROM	contaunica.tb_autorizacao_ob_itens
								WHERE	nr_documento_gerador = @nr_documento_gerador
								AND		nr_agrupamento_ob = 0)
				BEGIN
				
					SELECT	@id_autorizacao_ob = id_autorizacao_ob FROM contaunica.tb_autorizacao_ob_itens
					WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_ob <> 0

					--PRINT @id_autorizacao_ob
					--PRINT @id_autorizacao_ob_item

					--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
					UPDATE	pagamento.tb_confirmacao_pagamento_item
					SET		id_autorizacao_ob = @id_autorizacao_ob,
							id_autorizacao_ob_item = @id_autorizacao_ob_item 
					WHERE	nr_documento_gerador = @nr_documento_gerador

				END
			 
			END
		END

	END

COMMIT
END
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_SELECT'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT]        
(    
  @id_tipo_documento int  = NULL,         
  @nr_documento varchar(30)  = NULL,        
  @nr_conta varchar(3) = NULL,         
  @dt_preparacao varchar(20)   = NULL,    
  @dt_cadastro datetime = NULL,        
  @id_tipo_pagamento int  = NULL,         
  @nr_orgao varchar(50) = NULL,        
  @id_despesa_tipo varchar(50) = null,        
  @nr_contrato int = null,        
  @cd_obra int = null,        
  @id_origem int = null,        
  @nm_reduzido_credor  varchar(14) = null,          
  @cd_cpf_cnpj  varchar(14) = null,          
  @dt_confirmacao varchar(20)   = NULL,          
  @cd_transmissao_status_prodesp varchar(50) = null,        
  --@dt_cadastro varchar(20) = NULL,    
  @de date = null,    
  @ate date = null        
 )        
AS          
BEGIN        
	SELECT cp.id_execucao_pd,      
     cpi.cd_credor_organizacao,      
     cpi.id_despesa_tipo,    
     cpi.nr_cnpj_cpf_ug_credor,    
     cpi.dt_confirmacao,      
     cpi.id_origem,       
     cpi.cd_transmissao_status_prodesp,      
     cpi.nr_nl_documento,      
     cpi.cd_obra
   FROM [pagamento].[tb_confirmacao_pagamento_item] cpi (nolock)        
   left join pagamento.tb_confirmacao_pagamento cp (NOLOCK)
   on cp.id_confirmacao_pagamento = cpi.id_confirmacao_pagamento 
   Where 1=1 AND
	(NULLIF(@id_tipo_documento, 0) IS NULL OR cpi.id_tipo_documento = @id_tipo_documento )  AND  
	(NULLIF(@nr_documento, '') IS NULL OR cpi.nr_documento = @nr_documento )  AND  
	(NULLIF(@nr_conta, '') IS NULL OR cpi.nr_conta_pagador = @nr_conta )  AND  
	(@dt_preparacao IS NULL OR cp.dt_preparacao = @dt_preparacao )  AND  
	(@de is null or cp.dt_cadastro >= @de )AND  
	(@ate is null or cp.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00')) AND  
	(NULLIF(@id_tipo_pagamento, 0) IS NULL OR cp.id_tipo_pagamento = @id_tipo_pagamento )  AND  
	(NULLIF(@id_despesa_tipo, '') IS NULL OR cpi.id_despesa_tipo = @id_despesa_tipo )  AND  
	(NULLIF(@nr_contrato, '') IS NULL OR cpi.nr_contrato = @nr_contrato )  AND  
	(@id_origem IS NULL OR cpi.id_origem = @id_origem )  AND  
	(NULLIF(@cd_cpf_cnpj, '') IS NULL OR cpi.nr_cnpj_cpf_ug_credor = @cd_cpf_cnpj )  AND  
	(NULLIF(@cd_transmissao_status_prodesp, '') IS NULL OR cpi.cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp) AND  
	(NULLIF(@nr_orgao, '') IS NULL OR cpi.cd_orgao_assinatura = @nr_orgao) AND  
	(NULLIF(@nm_reduzido_credor, '') IS NULL OR cpi.nm_reduzido_credor = @nm_reduzido_credor)     
     
End
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT_ID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_SELECT_ID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT_ID] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_SELECT_ID
(	
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null
)
AS
BEGIN

	SELECT pag.id_confirmacao_pagamento
      ,pag.id_confirmacao_pagamento_tipo
      ,pag.id_execucao_pd
      ,pag.id_autorizacao_ob
      ,pag.id_tipo_documento
      ,pag.nr_agrupamento
      ,pag.ano_referencia
      ,pag.id_tipo_pagamento
      ,pag.dt_confirmacao
      ,pag.dt_cadastro
      ,pag.dt_modificacao
      ,pag.vr_total_confirmado
      ,pag.cd_transmissao_status_prodesp
      ,pag.fl_transmissao_transmitido_prodesp
      ,pag.dt_transmissao_transmitido_prodesp
      ,pag.ds_transmissao_mensagem_prodesp
      ,pag.dt_preparacao
      ,pag.nr_conta
      ,pag.nr_documento
	  ,item.id_confirmacao_pagamento_item
      ,item.id_confirmacao_pagamento
      ,item.id_execucao_pd
      ,item.id_programacao_desembolso_execucao_item
      ,item.id_autorizacao_ob
      ,item.id_autorizacao_ob_item
      ,item.dt_confirmacao
      ,item.id_tipo_documento
      ,item.nr_documento
      ,item.id_regional
      ,item.id_reclassificacao_retencao
      ,item.id_origem
      ,item.id_despesa_tipo
      ,item.dt_vencimento
      ,item.nr_contrato
      ,item.cd_obra
      ,item.nr_op
      ,item.nr_banco_pagador
      ,item.nr_agencia_pagador
      ,item.nr_conta_pagador
      ,item.nr_fonte_siafem
      ,item.nr_emprenho
      ,item.nr_processo
      ,item.nr_nota_fiscal
      ,item.nr_nl_documento
      ,item.vr_documento
      ,item.nr_natureza_despesa
      ,item.cd_credor_organizacao
      ,item.nr_cnpj_cpf_ug_credor
      ,item.ds_referencia
      ,item.cd_orgao_assinatura
      ,item.nm_reduzido_credor
      ,item.fl_transmissao_transmitido_prodesp
      ,item.cd_transmissao_status_prodesp
      ,item.dt_transmissao_transmitido_prodesp
      ,item.ds_transmissao_mensagem_prodesp
  FROM pagamento.tb_confirmacao_pagamento pag WITH(NOLOCK)
  INNER JOIN pagamento.tb_confirmacao_pagamento_item item WITH(NOLOCK)
  ON pag.id_confirmacao_pagamento = item.id_confirmacao_pagamento
  Where pag.id_confirmacao_pagamento = @id_confirmacao_pagamento
  And item.id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
  End
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_UPDATE]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_UPDATE'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_UPDATE] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_UPDATE
(
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null,
	  @id_confirmacao_pagamento_tipo int = null,
      @id_execucao_pd int = null,
      @id_autorizacao_ob int = null,
      @id_tipo_documento int = null,
      @nr_agrupamento int  = null,
      @ano_referencia int = null,
      @id_tipo_pagamento int = null,
      @dt_confirmacao datetime = null,	  
      @dt_cadastro datetime = null,
      @dt_modificacao datetime = null,
      @vr_total_confirmado decimal(18,2) = null,
      @cd_transmissao_status_prodesp varchar(50) = null,
      @fl_transmissao_transmitido_prodesp bit = null,
      @dt_transmissao_transmitido_prodesp datetime = null,
      @ds_transmissao_mensagem_prodesp varchar(200) = null,
	  @dt_preparacao datetime = null,
      @nr_conta varchar(20) = null,
      @nr_documento varchar(30) = null,
      @id_programacao_desembolso_execucao_item int = null,
      @id_autorizacao_ob_item int = null,
      @id_regional smallint = null,
      @id_reclassificacao_retencao int = null,
      @id_origem int = null,
	  @id_despesa_tipo int = null,
      @dt_vencimento datetime = null,
      @nr_contrato varchar(13) = null,
      @cd_obra varchar(20) = null,
      @nr_op varchar(50) = null,
      @nr_banco_pagador varchar(10) = null,
      @nr_agencia_pagador varchar(10) = null,
      @nr_conta_pagador varchar(10) = null,
      @nr_fonte_siafem varchar(50) = null,
      @nr_emprenho varchar(50) = null,
      @nr_processo varchar(20)= null,
      @nr_nota_fiscal int = null,
      @nr_nl_documento varchar(20) = null,
      @vr_documento decimal(8,2) = null,
      @nr_natureza_despesa int = null,
      @cd_credor_organizacao int = null,
      @nr_cnpj_cpf_ug_credor varchar(14) = null,
      @ds_referencia nvarchar(100) = null,
      @cd_orgao_assinatura varchar(2) = null,
      @nm_reduzido_credor varchar(14) = null

)
AS      
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  
Begin
UPDATE pagamento.tb_confirmacao_pagamento
   SET id_confirmacao_pagamento_tipo = @id_confirmacao_pagamento_tipo
      ,id_execucao_pd = @id_execucao_pd
      ,id_autorizacao_ob = @id_autorizacao_ob
      ,id_tipo_documento = @id_tipo_documento
      ,nr_agrupamento = @nr_agrupamento
	  ,ano_referencia = @ano_referencia
      ,id_tipo_pagamento = @id_tipo_pagamento
      ,dt_confirmacao = @dt_confirmacao
      ,dt_cadastro = @dt_cadastro
      ,dt_modificacao = @dt_modificacao
      ,vr_total_confirmado = @vr_total_confirmado
      ,cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp
      ,fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp
      ,dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp
      ,ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
      ,dt_preparacao = @dt_preparacao
      ,nr_conta = @nr_conta
      ,nr_documento = @nr_documento
 WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento
End

select * from pagamento.tb_confirmacao_pagamento

Begin
UPDATE pagamento.tb_confirmacao_pagamento_item
   SET id_confirmacao_pagamento = @id_confirmacao_pagamento
      ,id_execucao_pd = @id_execucao_pd
      ,id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
      ,id_autorizacao_ob = @id_autorizacao_ob
      ,id_autorizacao_ob_item = @id_autorizacao_ob_item
      ,dt_confirmacao = @dt_confirmacao
      ,id_tipo_documento = @id_tipo_documento
      ,nr_documento = @nr_documento
      ,id_regional = @id_regional
      ,id_reclassificacao_retencao = @id_reclassificacao_retencao
      ,id_origem = @id_origem
      ,id_despesa_tipo = @id_despesa_tipo
      ,dt_vencimento = @dt_vencimento
      ,nr_contrato = @nr_contrato
      ,cd_obra = @cd_obra
      ,nr_op = @nr_op
      ,nr_banco_pagador = @nr_banco_pagador
      ,nr_agencia_pagador = @nr_agencia_pagador
      ,nr_conta_pagador = @nr_conta_pagador
      ,nr_fonte_siafem = @nr_fonte_siafem
      ,nr_emprenho = @nr_emprenho
      ,nr_processo = @nr_processo
      ,nr_nota_fiscal = @nr_nota_fiscal
      ,nr_nl_documento = @nr_nl_documento
      ,vr_documento = @vr_documento
      ,nr_natureza_despesa = @nr_natureza_despesa
      ,cd_credor_organizacao = @cd_credor_organizacao
      ,nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor
      ,ds_referencia = @ds_referencia
      ,cd_orgao_assinatura = @cd_orgao_assinatura
      ,nm_reduzido_credor = @nm_reduzido_credor
      ,fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp
      ,cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp
      ,dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp
      ,ds_transmissao_mensagem_prodesp = @ds_transmissao_mensagem_prodesp
 WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento And id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
 End
 COMMIT
 End
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCEL_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCEL_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCEL_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCEL_CONSULTAR]  
           --@id_cancelamento_movimentacao int =NULL,
           --@tb_fonte_id_fonte int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL
           --@nr_seq int =NULL
           --@nr_nota_cancelamento varchar(10) =NULL,
           --@cd_unidade_gestora varchar(15) =NULL,
           --@nr_categoria_gasto varchar(15) =NULL,
           --@ds_observacao varchar(231) =NULL,
           --@fg_transmitido_prodesp char(1) =NULL,
           --@fg_transmitido_siafem char(1)= NULL

		
AS    
BEGIN    
 SET NOCOUNT ON;  



SELECT c.[id_cancelamento_movimentacao] as IdCancelamento
      ,c.[tb_fonte_id_fonte]  as Fonte
      ,c.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao
      ,c.[nr_agrupamento] as NrAgrupamento
      ,c.[nr_seq] as NrSequencia
      ,c.[nr_nota_cancelamento] as NrNotaCancelamento
      ,c.[cd_unidade_gestora] as UnidadeGestoraFavorecida
      ,c.[nr_categoria_gasto] as CategoriaGasto
      ,c.[ds_observacao] as ObservacaoCancelamento
	  ,c.[ds_observacao2] as ObservacaoCancelamento2
	  ,c.[ds_observacao3] as ObservacaoCancelamento3
       ,c.[fg_transmitido_prodesp]   as StatusProdespItem
      ,c.[ds_msgRetornoProdesp]   as MensagemProdespItem
      ,c.[fg_transmitido_siafem]   as StatusSiafemItem
      ,c.[ds_msgRetornoSiafem]  as MensagemSiafemItem
	  ,c.valor as ValorTotal
	  ,c.cd_gestao_favorecido  
	  ,c.eventoNC as EventoNC
	  ,c.cd_gestao_favorecido as IdGestaoFavorecida

	  
  FROM [movimentacao].[tb_cancelamento_movimentacao]   c
 

  WHERE 
  ( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  ( nullif( @nr_agrupamento, 0 ) is null or c.nr_agrupamento = @nr_agrupamento )    
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_cancelamento_movimentacao,c.nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR]      
           @id_cancelamento_movimentacao int =NULL,    
           @tb_fonte_id_fonte varchar(10) =NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
           @nr_agrupamento int =NULL,    
           @nr_seq int =NULL,    
           @nr_nota_cancelamento varchar(10) =NULL,    
           @cd_unidade_gestora varchar(15) =NULL,   
		   @cd_gestao_favorecido varchar(10) =NULL,  
           @nr_categoria_gasto varchar(15) =NULL,    
           @ds_observacao varchar(231) =NULL,    
           @fg_transmitido_prodesp char(1) =NULL,    
           @fg_transmitido_siafem char(1)= NULL    
    
      
AS        
BEGIN        
 SET NOCOUNT ON;      
    
    
    
    
SELECT [id_cancelamento_movimentacao]    
      ,[tb_fonte_id_fonte]    
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
      ,[nr_agrupamento]    
      ,[nr_seq]    
      ,[nr_nota_cancelamento]    
      ,[cd_unidade_gestora]    
      ,[nr_categoria_gasto]    
      ,[ds_observacao]    
   ,[ds_observacao2]   
   ,[ds_observacao3]   
      ,[fg_transmitido_prodesp]    
      ,[ds_msgRetornoProdesp]    
      ,[fg_transmitido_siafem]    
      ,[ds_msgRetornoSiafem]    
	  ,[cd_gestao_favorecido]
	  ,[eventoNC]
  FROM [movimentacao].[tb_cancelamento_movimentacao]    
    
  WHERE     
  ( nullif( @id_cancelamento_movimentacao, 0 ) is null or id_cancelamento_movimentacao = @id_cancelamento_movimentacao )   and     
  (  @tb_fonte_id_fonte is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and     
  ( nullif(  @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
    --( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
  ( nullif( @nr_agrupamento, 0 ) is null or nr_agrupamento = @nr_agrupamento )   and     
  ( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and     
   (@nr_nota_cancelamento is null or nr_nota_cancelamento = @nr_nota_cancelamento  ) and    
    (@cd_unidade_gestora is null or cd_unidade_gestora= @cd_unidade_gestora  ) and    
	(@cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and  
  (@nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) and    
  (@ds_observacao is null or ds_observacao= @ds_observacao  ) and    
  (@fg_transmitido_prodesp is null or fg_transmitido_prodesp= @fg_transmitido_prodesp  ) and    
  (@fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem  )     
    
  ORDER BY id_cancelamento_movimentacao,nr_seq    
    
    
    
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR]   
	@id_cancelamento_movimentacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [movimentacao].[tb_cancelamento_movimentacao]
	WHERE 
		id_cancelamento_movimentacao = @id_cancelamento_movimentacao
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================                
-- Author:  Alessandro de Santana                
-- Create date: 25/07/2018               
-- Description: Procedure para consulta de valor de movimentacao                
-- ==============================================================                
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR]     
   @id_mes   int = null                  
,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao int = null           
, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null             
, @nr_agrupamento int = null          
,@nr_seq int = null          
,@ds_mes varchar(9) = null        
,@vr_mes decimal = null        
AS                
BEGIN                
 SET NOCOUNT ON;                
                
  SELECT                
   [id_mes]                
  , [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]     as     IdDistribuicao       
  ,[tb_reducao_suplementacao_id_reducao_suplementacao]             as IdReducaoSuplementacao 
  ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]      as IdCancelamento        
  ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]             as IdMovimentacao 
  ,[nr_agrupamento]      as NrAgrupamento        
  ,[nr_seq]       as NrSequencia       
  , [ds_mes]    as     Mes       
  , [vr_mes]       as ValorMes
  ,[cd_unidade_gestora]    as UnidadeGestoraFavorecida         
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)                
  WHERE                 
     ( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and                
      (  tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and          
      (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and         
      (  nr_agrupamento = @nr_agrupamento )   and           
      (  nr_seq = @nr_seq )                  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR]        
           --@id_cancelamento_movimentacao int =NULL,          
           --@tb_fonte_id_fonte int =NULL,          
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,          
           @nr_agrupamento int =NULL          
           --@nr_seq int =NULL          
           --@nr_nota_cancelamento varchar(10) =NULL,          
           --@cd_unidade_gestora varchar(15) =NULL,          
           --@nr_categoria_gasto varchar(15) =NULL,          
           --@ds_observacao varchar(231) =NULL,          
           --@fg_transmitido_prodesp char(1) =NULL,          
           --@fg_transmitido_siafem char(1)= NULL          
          
            
AS              
BEGIN              
 SET NOCOUNT ON;            
          
          
          
SELECT c.[id_cancelamento_movimentacao] as IdCancelamento          
      ,c.[tb_fonte_id_fonte]  as Fonte          
      ,c.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao          
      ,c.[nr_agrupamento] as NrAgrupamento          
      ,c.[nr_seq] as NrSequencia          
      ,c.[nr_nota_cancelamento] as NrNotaCancelamento          
      ,c.[cd_unidade_gestora] as UnidadeGestoraFavorecida       
   ,o.[cd_unidade_gestora_emitente] as UnidadeGestoraEmitente       
      ,c.[nr_categoria_gasto] as CategoriaGasto          
      ,c.[ds_observacao] as ObservacaoCancelamento          
      ,c.[ds_observacao2] as ObservacaoCancelamento2          
      ,c.[ds_observacao3] as ObservacaoCancelamento3          
      ,c.[fg_transmitido_prodesp]   as StatusProdespItem        
      ,c.[ds_msgRetornoProdesp]   as MensagemProdespItem        
      ,c.[fg_transmitido_siafem]   as StatusSiafemItem        
      ,c.[ds_msgRetornoSiafem]  as MensagemSiafemItem        
   ,rs.id_reducao_suplementacao as IdReducaoSuplementacao         
   ,rs.cd_destino_recurso as DestinoRecurso          
   ,rs.nr_orgao as NrOrgao          
   ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao          
   ,c.valor as ValorCancelamentoReducao          
   ,c.cd_gestao_favorecido  as IdGestaoFavorecida        
   ,c.eventoNC as EventoNC         
  , rs.flag_red_sup as FlagRedistribuicao       
      ,rs.[fl_proc]   as FlProc      
      ,rs.[nr_processo]   as NrProcesso      
      ,rs.[nr_orgao]   as  NrOrgao      
      ,rs.[nr_obra]   as  NrObra      
      ,rs.[cd_origem_recurso]   as  OrigemRecurso      
      ,rs.[cd_destino_recurso]  as DestinoRecurso      
      ,rs.[cd_especificacao_despesa]  as EspecDespesa      
      ,rs.[ds_especificacao_despesa]  as DescEspecDespesa      
      ,rs.[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura       
      ,rs.[cd_autorizado_grupo]  as CodigoAutorizadoGrupo      
      ,rs.[cd_autorizado_orgao]  as CodigoAutorizadoOrgao      
      ,rs.[ds_autorizado_cargo]   as DescricaoAutorizadoCargo      
      ,rs.[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura      
      ,rs.[cd_examinado_assinatura]  as CodigoExaminadoAssinatura      
      ,rs.[cd_examinado_grupo]  as CodigoExaminadoGrupo      
      ,rs.[cd_examinado_orgao]  as  CodigoExaminadoOrgao      
      ,rs.[ds_examinado_cargo]  as DescricaoExaminadoCargo      
      ,rs.[nm_examinado_assinatura]  as NomeExaminadoAssinatura      
      ,rs.[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura      
      ,rs.[cd_responsavel_grupo]   as CodigoResponsavelGrupo       
      ,rs.[cd_responsavel_orgao]  as CodigoResponsavelOrgao      
      ,rs.[ds_responsavel_cargo]  as DescricaoResponsavelCargo      
   ,rs.[nm_responsavel_assinatura] as NomeResponsavelAssinatura      
   ,rs.[TotalQ1]      
   ,rs.[TotalQ2]      
   ,rs.[TotalQ3]      
   ,rs.[TotalQ4]      
   , o.tb_programa_id_programa as  ProgramaId      
   ,o.tb_estrutura_id_estrutura as NaturezaId    
   ,o.tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao as IdTipoDocumento    
   ,o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria as IdTipoMovimentacao    
       
             
  FROM [movimentacao].[tb_cancelamento_movimentacao]   c          
      inner  join [movimentacao].[tb_reducao_suplementacao]  rs on c.id_cancelamento_movimentacao = rs.tb_cancelamento_movimentacao_id_cancelamento_movimentacao      
   left join    [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria     
          
  WHERE           
  (  c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
  (  c.nr_agrupamento = @nr_agrupamento )            
  -- ( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
  --( nullif( @nr_agrupamento, 0 ) is null or c.nr_agrupamento = @nr_agrupamento )              
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )              
          
          
  ORDER BY id_cancelamento_movimentacao,c.nr_seq          
          
          
          
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria    
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR]     
            @id_cancelamento_movimentacao int =NULL,    
           @tb_fonte_id_fonte varchar(10) =NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int= NULL,    
           @nr_agrupamento int =NULL,    
           @nr_seq int =NULL,    
           @nr_nota_cancelamento varchar(15) =NULL,    
           @cd_unidade_gestora varchar(10) =NULL, 
		   @cd_gestao_favorecido varchar(10) =NULL,     
           @nr_categoria_gasto  varchar(10) =NULL,    
           @ds_observacao varchar(77) =NULL,   
      @ds_observacao2 varchar(77) =NULL,    
    @ds_observacao3 varchar(77) =NULL,     
           @fg_transmitido_prodesp char(1)= NULL,    
           @ds_msgRetornoProdesp varchar(140)  =NULL,    
           @fg_transmitido_siafem char(1) =NULL,    
           @ds_msgRetornoSiafem varchar(140)  =NULL,    
     @valor int = null  ,  
  @eventoNC varchar(10)  =NULL  
    
    
as    
begin    
    
 set nocount on;    
    
 if exists (    
  select 1     
  from [movimentacao].[tb_cancelamento_movimentacao] (nolock)    
  where id_cancelamento_movimentacao = @id_cancelamento_movimentacao    
 )    
 begin    
    
 update [movimentacao].[tb_cancelamento_movimentacao] set    
    [tb_fonte_id_fonte] = @tb_fonte_id_fonte    
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
      ,[nr_agrupamento] = @nr_agrupamento    
      ,[nr_seq] = @nr_seq    
      ,[nr_nota_cancelamento] = @nr_nota_cancelamento    
      ,[cd_unidade_gestora] = @cd_unidade_gestora  
	  ,[cd_gestao_favorecido] = @cd_gestao_favorecido    
      ,[nr_categoria_gasto] = @nr_categoria_gasto    
      ,[ds_observacao] = @ds_observacao   
   ,[ds_observacao2] = @ds_observacao2   
   ,[ds_observacao3] = @ds_observacao3    
      ,[fg_transmitido_prodesp] = @fg_transmitido_prodesp    
      ,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp    
      ,[fg_transmitido_siafem] = @fg_transmitido_siafem    
      ,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem    
      ,[valor] = @valor    
     ,[eventoNC] = @eventoNC  
        
    
        where id_cancelamento_movimentacao = @id_cancelamento_movimentacao;    
    
  select @id_cancelamento_movimentacao;    
    
    
   end    
 else    
 begin    
    
 INSERT INTO [movimentacao].[tb_cancelamento_movimentacao]    
           ([tb_fonte_id_fonte]    
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
           ,[nr_agrupamento]    
           ,[nr_seq]    
           ,[nr_nota_cancelamento]    
           ,[cd_unidade_gestora]  
		   ,[cd_gestao_favorecido]   
           ,[nr_categoria_gasto]    
           ,[ds_observacao]   
     ,[ds_observacao2]  
     ,[ds_observacao3]   
           ,[fg_transmitido_prodesp]    
           ,[ds_msgRetornoProdesp]    
           ,[fg_transmitido_siafem]    
           ,[ds_msgRetornoSiafem]    
     ,[valor]  
  ,[eventoNC])    
     VALUES    
           (@tb_fonte_id_fonte    
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
           ,@nr_agrupamento    
           ,@nr_seq    
           ,@nr_nota_cancelamento    
           ,@cd_unidade_gestora   
		   ,@cd_gestao_favorecido 
           ,@nr_categoria_gasto    
           ,@ds_observacao    
     ,@ds_observacao2   
     ,@ds_observacao3   
           ,'N'    
           ,@ds_msgRetornoProdesp    
           ,'N'    
           ,@ds_msgRetornoSiafem    
     ,@valor  
  ,@eventoNC)    
    
    
    
  select scope_identity();    
    
 end    
    
end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CREDITO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR] 
             @id_nota_credito int= NULL
            ,@tb_programa_id_programa int =NULL
           ,@tb_fonte_id_fonte int= NULL
           ,@tb_estrutura_id_estrutura int =NULL
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int= NULL
           ,@nr_agrupamento int= NULL
           ,@nr_seq int= NULL
           ,@nr_nota_credito VARCHAR(15)= NULL
           ,@cd_unidade_gestora_favorecido varchar(10) =NULL
		   ,@cd_gestao_favorecido varchar(10) =NULL 
           ,@cd_uo VARCHAR(10)= NULL
           ,@plano_interno VARCHAR(10) =NULL

 


AS    
BEGIN    
 SET NOCOUNT ON;  



SELECT [id_nota_credito]
      ,[tb_programa_id_programa] as ProgramaId
      ,[tb_fonte_id_fonte]  as Fonte
      ,[tb_estrutura_id_estrutura] as IdEstrutura
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao
      ,[nr_agrupamento] as NrAgrupamento
      ,[nr_seq] as NrSequencia
      ,[nr_nota_credito] as NrNotaCredito
      ,[cd_unidade_gestora_favorecido] as UnidadeGestoraFavorecida
      ,[cd_uo] as Uo
	  ,[cd_ugo] as UGO
      ,[plano_interno] 
      ,[vr_credito] as ValorCredito
      ,[ds_observacao] as ObservacaoNC
	  ,[ds_observacao2] as ObservacaoNC2
	  ,[ds_observacao3] as ObservacaoNC3
	  ,[eventoNC] as EventoNC
  FROM [movimentacao].[tb_credito_movimentacao]

  WHERE 
  ( nullif( @id_nota_credito, 0 ) is null or id_nota_credito = @id_nota_credito )   and 
  ( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa )   and 
   (  @tb_fonte_id_fonte is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
	  ( nullif( @tb_estrutura_id_estrutura, 0 ) is null or tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura )   and 
  (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  ( nr_agrupamento = @nr_agrupamento )   and 
    ( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
	(@nr_nota_credito is null or nr_nota_credito = @nr_nota_credito  ) and
	(@cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido  ) and
	(@cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	(@cd_uo is null or cd_uo = @cd_uo  ) and
	(@plano_interno is null or plano_interno = @plano_interno  )


  ORDER BY id_nota_credito



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CREDITO_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR]   
	@id_nota_credito  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_credito_movimentacao]
	WHERE 
		id_nota_credito = @id_nota_credito
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CREDITO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria    
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR]     
            @id_nota_credito int =NULL    
            ,@tb_programa_id_programa int =NULL    
           ,@tb_fonte_id_fonte varchar(10)= NULL    
           ,@tb_estrutura_id_estrutura int= NULL    
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL    
           ,@nr_agrupamento int =NULL    
           ,@nr_seq int =NULL    
           ,@nr_nota_credito varchar(15)= NULL    
           ,@cd_unidade_gestora_favorecido varchar(15)= NULL   
		    ,@cd_gestao_favorecido varchar(10) =NULL  
           ,@cd_uo varchar(10) =NULL    
		   ,@cd_ugo varchar(10) =NULL  
           ,@plano_interno varchar(10) =NULL    
           ,@vr_credito int= NULL    
           ,@ds_observacao varchar(77)= NULL  
     ,@ds_observacao2 varchar(77)= NULL    
     ,@ds_observacao3 varchar(77)= NULL  
           ,@fg_transmitido_prodesp char(1)= NULL    
           ,@ds_msgRetornoProdesp varchar(140)  =NULL   
           ,@fg_transmitido_siafem char(1) =NULL    
           ,@ds_msgRetornoSiafem varchar(140)  =NULL    
      ,@eventoNC varchar(10)  =NULL  
    
    
as    
begin    
    
 set nocount on;    
    
 if exists (    
  select 1     
  from [movimentacao].[tb_credito_movimentacao] (nolock)    
  where id_nota_credito = @id_nota_credito    
 )    
 begin    
    
 UPDATE [movimentacao].[tb_credito_movimentacao]    
    SET [tb_programa_id_programa] = @tb_programa_id_programa    
      ,[tb_fonte_id_fonte] = @tb_fonte_id_fonte    
      ,[tb_estrutura_id_estrutura] = @tb_estrutura_id_estrutura    
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
      ,[nr_agrupamento] = @nr_agrupamento    
      ,[nr_seq] = @nr_seq    
      ,[nr_nota_credito] = @nr_nota_credito    
      ,[cd_unidade_gestora_favorecido] = @cd_unidade_gestora_favorecido 
	  ,[cd_gestao_favorecido] = @cd_gestao_favorecido    
      ,[cd_ugo] = @cd_uo  
	  ,[cd_uo] = @cd_ugo    
      ,[plano_interno] = @plano_interno    
      ,[vr_credito] = @vr_credito    
      ,[ds_observacao] = @ds_observacao    
   ,[ds_observacao2] = @ds_observacao2  
   ,[ds_observacao3] = @ds_observacao3   
   ,[fg_transmitido_prodesp] = @fg_transmitido_prodesp    
      ,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp    
      ,[fg_transmitido_siafem] = @fg_transmitido_siafem    
      ,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem    
   ,[eventoNC] = @eventoNC  
    
        
    
        where id_nota_credito = @id_nota_credito;    
    
  select @id_nota_credito;    
    
    
   end    
 else    
 begin    
    
 INSERT INTO [movimentacao].[tb_credito_movimentacao]    
           ([tb_programa_id_programa]    
           ,[tb_fonte_id_fonte]    
           ,[tb_estrutura_id_estrutura]    
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
           ,[nr_agrupamento]    
           ,[nr_seq]    
           ,[nr_nota_credito]    
           ,[cd_unidade_gestora_favorecido]  
		   ,[cd_gestao_favorecido]   
           ,[cd_uo] 
		   ,[cd_ugo]    
           ,[plano_interno]    
           ,[vr_credito]    
           ,[ds_observacao]   
     ,[ds_observacao2]    
     ,[ds_observacao3]     
     ,fg_transmitido_prodesp,    
     ds_msgRetornoProdesp,    
     fg_transmitido_siafem,    
     ds_msgRetornoSiafem,  
  eventoNC)    
     VALUES    
           (@tb_programa_id_programa    
           ,@tb_fonte_id_fonte    
           ,@tb_estrutura_id_estrutura    
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
           ,@nr_agrupamento    
           ,@nr_seq    
           ,@nr_nota_credito    
           ,@cd_unidade_gestora_favorecido 
		   ,@cd_gestao_favorecido   
           ,@cd_uo    
		   ,@cd_ugo 
           ,@plano_interno    
           ,@vr_credito    
           ,@ds_observacao   
     ,@ds_observacao2   
     ,@ds_observacao3    
     ,'N'    
           ,@ds_msgRetornoProdesp    
           ,'N'    
 ,@ds_msgRetornoSiafem  
     ,@eventoNC)    
    
    
  
    
    
  select scope_identity();    
    
 end    
    
end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DIST_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR]  
           --@id_cancelamento_movimentacao int =NULL,
           --@tb_fonte_id_fonte int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL
           --@nr_seq int =NULL
           --@nr_nota_cancelamento varchar(10) =NULL,
           --@cd_unidade_gestora varchar(15) =NULL,
           --@nr_categoria_gasto varchar(15) =NULL,
           --@ds_observacao varchar(231) =NULL,
           --@fg_transmitido_prodesp char(1) =NULL,
           --@fg_transmitido_siafem char(1)= NULL

		
AS    
BEGIN    
 SET NOCOUNT ON;  



SELECT d.id_distribuicao_movimentacao as IdDistribuicao
      ,d.[tb_fonte_id_fonte] as Fonte
      ,d.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao
      ,d.[nr_agrupamento] as NrAgrupamento
      ,d.[nr_seq] as NrSequencia
      ,d.[nr_nota_distribuicao] as NrNotaDistribuicao
      ,d.[cd_unidade_gestora_favorecido] as UnidadeGestoraFavorecida
      ,d.[nr_categoria_gasto] as CategoriaGasto
      ,d.[ds_observacao] as ObservacaoCancelamento
	  ,d.[ds_observacao2] as ObservacaoCancelamento2
	  ,d.[ds_observacao3] as ObservacaoCancelamento3 
	  ,d.[valor] as ValorTotal
	  ,d.cd_gestao_favorecido
	  ,d.eventoNC as EventoNC
	   ,d.[fg_transmitido_prodesp]   as StatusProdespItem
      ,d.[ds_msgRetornoProdesp]   as MensagemProdespItem
      ,d.[fg_transmitido_siafem]   as StatusSiafemItem
      ,d.[ds_msgRetornoSiafem]  as MensagemSiafemItem

	  
  FROM [movimentacao].[tb_distribuicao_movimentacao]   D


  WHERE 
  (  d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  d.nr_agrupamento = @nr_agrupamento )    
  --( nullif( @nr_seq, 0 ) is null or d.nr_seq = @nr_seq )    


  ORDER BY id_distribuicao_movimentacao,d.nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR]  
           @id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @tb_fonte_id_fonte int =NULL,
           @nr_nota_distribuicao VARCHAR(10) =NULL,
           @cd_unidade_gestora_favorecido varchar(10) =NULL,
		   @cd_gestao_favorecido varchar(10) =NULL,  
           @nr_categoria_gasto varchar(10)= NULL



AS    
BEGIN    
 SET NOCOUNT ON;  


 SELECT [id_distribuicao_movimentacao]
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
      ,[nr_agrupamento]
      ,[nr_seq]
      ,[tb_fonte_id_fonte]
      ,[nr_nota_distribuicao]
      ,[cd_unidade_gestora_favorecido]
      ,[nr_categoria_gasto]
      ,[ds_observacao]
	  ,[ds_observacao2]
	  ,[ds_observacao3]
	  ,[eventoNC] 
  FROM [movimentacao].[tb_distribuicao_movimentacao]



  WHERE 
  ( nullif( @id_distribuicao_movimentacao, 0 ) is null or id_distribuicao_movimentacao = @id_distribuicao_movimentacao )   and 
  (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  nr_agrupamento = @nr_agrupamento )   and 
  ( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
  ( nullif( @tb_fonte_id_fonte, 0 ) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
   (@nr_nota_distribuicao is null or nr_nota_distribuicao = @nr_nota_distribuicao  ) and
    (@cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido= @cd_unidade_gestora_favorecido  ) and
	(@cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	 (@nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) 
	 

  ORDER BY id_distribuicao_movimentacao,nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR]   
	@id_distribuicao_movimentacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_distribuicao_movimentacao]
	WHERE 
		id_distribuicao_movimentacao = @id_distribuicao_movimentacao
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================              
-- Author:  Alessandro de Santana              
-- Create date: 25/07/2018             
-- Description: Procedure para consulta de valor de movimentacao              
-- ==============================================================              
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR]            
   @id_mes   int = null              
, @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int = null       
, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null           
, @nr_agrupamento int = null        
,@nr_seq int = null        
,@ds_mes varchar(9) = null      
,@vr_mes decimal = null      
AS              
BEGIN              
 SET NOCOUNT ON;              
              
  SELECT              
   [id_mes]              
  , [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]              
  ,[tb_reducao_suplementacao_id_reducao_suplementacao]            
  ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]            
  ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]            
  ,[nr_agrupamento]            
  ,[nr_seq]            
  , [ds_mes]             
  , [vr_mes]   
  , [cd_unidade_gestora]            
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)              
  WHERE               
     (  nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and              
      (  tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and        
      ( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and       
      (  nr_agrupamento = @nr_agrupamento )   and         
      (  @nr_seq = @nr_seq )                
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria    
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR]     
           @id_distribuicao_movimentacao int= NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
           @nr_agrupamento int =NULL,    
           @nr_seq int =NULL,    
           @tb_fonte_id_fonte varchar(10) =NULL,    
           @nr_nota_distribuicao varchar(15) =NULL,    
           @cd_unidade_gestora_favorecido varchar(10) =NULL,   
		   @cd_gestao_favorecido varchar(10) =NULL,   
           @nr_categoria_gasto int =NULL,    
           @ds_observacao varchar(77) =NULL,   
     @ds_observacao2 varchar(77) =NULL,   
     @ds_observacao3 varchar(77) =NULL,    
     @fg_transmitido_prodesp char(1)= NULL,    
           @ds_msgRetornoProdesp varchar(140)  =NULL,    
           @fg_transmitido_siafem char(1) =NULL,    
           @ds_msgRetornoSiafem varchar(140)  =NULL,    
     @valor int = null  ,  
  @eventoNC varchar(10)  =NULL  
    
    
as    
begin    
    
 set nocount on;    
    
 if exists (    
  select 1     
  from [movimentacao].[tb_distribuicao_movimentacao] (nolock)    
  where id_distribuicao_movimentacao = @id_distribuicao_movimentacao    
 )    
 begin    
    
 UPDATE [movimentacao].[tb_distribuicao_movimentacao]    
   SET [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
      ,[nr_agrupamento] = @nr_agrupamento    
      ,[nr_seq] = @nr_seq    
      ,[tb_fonte_id_fonte] = @tb_fonte_id_fonte    
      ,[nr_nota_distribuicao] = @nr_nota_distribuicao    
      ,[cd_unidade_gestora_favorecido] = @cd_unidade_gestora_favorecido 
	  ,[cd_gestao_favorecido] = @cd_gestao_favorecido     
      ,[nr_categoria_gasto] = @nr_categoria_gasto    
      ,[ds_observacao] = @ds_observacao   
   ,[ds_observacao2] = @ds_observacao2  
   ,[ds_observacao3] = @ds_observacao3   
   ,[fg_transmitido_prodesp] = @fg_transmitido_prodesp    
      ,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp    
      ,[fg_transmitido_siafem] = @fg_transmitido_siafem    
      ,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem    
   ,[valor] = @valor   
   ,[eventoNC] = @eventoNC   
        
  
        where id_distribuicao_movimentacao = @id_distribuicao_movimentacao;    
    
  select @id_distribuicao_movimentacao;    
    
    
   end    
 else    
 begin    
    
 INSERT INTO [movimentacao].[tb_distribuicao_movimentacao]    
           ([tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
           ,[nr_agrupamento]    
           ,[nr_seq]    
           ,[tb_fonte_id_fonte]    
           ,[nr_nota_distribuicao]    
           ,[cd_unidade_gestora_favorecido]    
		   ,[cd_gestao_favorecido]    
           ,[nr_categoria_gasto]    
           ,[ds_observacao]   
     ,[ds_observacao2]  
     ,[ds_observacao3]   
     ,[fg_transmitido_prodesp]    
           ,[ds_msgRetornoProdesp]    
           ,[fg_transmitido_siafem]    
           ,[ds_msgRetornoSiafem]    
     ,[valor]  
  ,eventoNC)    
     VALUES    
           (@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria    
           ,@nr_agrupamento    
           ,@nr_seq    
           ,@tb_fonte_id_fonte    
           ,@nr_nota_distribuicao    
           ,@cd_unidade_gestora_favorecido 
		   ,@cd_gestao_favorecido   
           ,@nr_categoria_gasto    
           ,@ds_observacao    
     ,@ds_observacao2  
     ,@ds_observacao3  
     ,'N'    
           ,@ds_msgRetornoProdesp    
           ,'N'    
           ,@ds_msgRetornoSiafem    
     ,@valor  
  ,@eventoNC)    
    
    
    
  select scope_identity();    
    
 end    
    
end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR]       
           --@id_cancelamento_movimentacao int =NULL,          
           --@tb_fonte_id_fonte int =NULL,          
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,          
           @nr_agrupamento int =NULL          
           --@nr_seq int =NULL          
           --@nr_nota_cancelamento varchar(10) =NULL,          
           --@cd_unidade_gestora varchar(15) =NULL,          
           --@nr_categoria_gasto varchar(15) =NULL,          
           --@ds_observacao varchar(231) =NULL,          
           --@fg_transmitido_prodesp char(1) =NULL,          
           --@fg_transmitido_siafem char(1)= NULL          
          
            
AS              
BEGIN              
 SET NOCOUNT ON;            
          
          
          
SELECT d.id_distribuicao_movimentacao as IdDistribuicao          
      ,d.[tb_fonte_id_fonte] as Fonte          
      ,d.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao          
      ,d.[nr_agrupamento] as NrAgrupamento          
      ,d.[nr_seq] as NrSequencia          
      ,d.[nr_nota_distribuicao] as NrNotaDistribuicao           
      ,d.[cd_unidade_gestora_favorecido] as UnidadeGestoraFavorecida     
   ,o.[cd_unidade_gestora_emitente] as UnidadeGestoraEmitente         
      ,d.[nr_categoria_gasto] as CategoriaGasto          
      ,d.[ds_observacao] as ObservacaoCancelamento         
   ,d.[ds_observacao2] as ObservacaoCancelamento2        
   ,d.[ds_observacao3] as ObservacaoCancelamento3         
       ,d.[fg_transmitido_prodesp]   as StatusProdespItem        
      ,d.[ds_msgRetornoProdesp]   as MensagemProdespItem        
      ,d.[fg_transmitido_siafem]   as StatusSiafemItem        
      ,d.[ds_msgRetornoSiafem]  as MensagemSiafemItem         
   ,rs.id_reducao_suplementacao as IdReducaoSuplementacao          
   ,rs.cd_destino_recurso as DestinoRecurso          
   ,rs.nr_orgao as NrOrgao          
   ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao          
   ,d.valor as ValorDistribuicaoSuplementacao        
   ,d.cd_gestao_favorecido    as IdGestaoFavorecida    
   ,d.eventoNC as EventoNC        
   , rs.flag_red_sup as FlagRedistribuicao      
    ,rs.tb_programa_id_programa as  ProgramaId  
      ,rs.[fl_proc]   as FlProc  
      ,rs.[nr_processo]   as NrProcesso  
      ,rs.[nr_orgao]   as  NrOrgao  
      ,rs.[nr_obra]   as  NrObra  
      ,rs.[cd_origem_recurso]   as  OrigemRecurso  
      ,rs.[cd_destino_recurso]  as DestinoRecurso  
      ,rs.[cd_especificacao_despesa]  as EspecDespesa  
      ,rs.[ds_especificacao_despesa]  as DescEspecDespesa  
      ,rs.[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura   
      ,rs.[cd_autorizado_grupo]  as CodigoAutorizadoGrupo  
      ,rs.[cd_autorizado_orgao]  as CodigoAutorizadoOrgao  
      ,rs.[ds_autorizado_cargo]   as DescricaoAutorizadoCargo  
      ,rs.[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura  
      ,rs.[cd_examinado_assinatura]  as CodigoExaminadoAssinatura  
      ,rs.[cd_examinado_grupo]  as CodigoExaminadoGrupo  
      ,rs.[cd_examinado_orgao]  as  CodigoExaminadoOrgao  
      ,rs.[ds_examinado_cargo]  as DescricaoExaminadoCargo  
      ,rs.[nm_examinado_assinatura]  as NomeExaminadoAssinatura  
      ,rs.[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura  
      ,rs.[cd_responsavel_grupo]   as CodigoResponsavelGrupo   
      ,rs.[cd_responsavel_orgao]  as CodigoResponsavelOrgao  
      ,rs.[ds_responsavel_cargo]  as DescricaoResponsavelCargo  
   ,rs.[nm_responsavel_assinatura] as NomeResponsavelAssinatura  
   ,rs.[TotalQ1]  
   ,rs.[TotalQ2]  
   ,rs.[TotalQ3]  
   ,rs.[TotalQ4]  
  , o.tb_programa_id_programa as  ProgramaId      
   ,o.tb_estrutura_id_estrutura as NaturezaId    
   ,o.tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao as IdTipoDocumento    
   ,o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria as IdTipoMovimentacao    
             
  FROM [movimentacao].[tb_distribuicao_movimentacao]   D          
      left  join [movimentacao].[tb_reducao_suplementacao]  rs on d.id_distribuicao_movimentacao = rs.tb_distribuicao_movimentacao_id_distribuicao_movimentacao     
    left join    [movimentacao].[tb_movimentacao_orcamentaria] o on d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria        
          
  WHERE           
  (  d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
  ( d.nr_agrupamento = @nr_agrupamento )              
  --( nullif( @nr_seq, 0 ) is null or d.nr_seq = @nr_seq )              
          
          
  ORDER BY id_distribuicao_movimentacao,d.nr_seq          
          
          
          
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================          
-- Author:  Alessandro de Santana          
-- Create date: 25/07/2018         
-- Description: Procedure para consulta de valor de movimentacao          
-- ==============================================================          
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_CONSULTAR]          
   @id_mes   int = null          
, @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int = null   
,@tb_reducao_suplementacao_id_reducao_suplementacao int = null      
,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao int = null     
, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null       
, @nr_agrupamento int = null    
,@nr_seq int = null    
,@ds_mes varchar(9) = null  
,@vr_mes decimal = null  
AS          
BEGIN          
 SET NOCOUNT ON;          
          
  SELECT          
   [id_mes]          
  , [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]          
  ,[tb_reducao_suplementacao_id_reducao_suplementacao]        
  ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]        
  ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]        
  ,[nr_agrupamento]        
  ,[nr_seq]        
  , [ds_mes]         
  , [vr_mes]  
  ,[cd_unidade_gestora]        
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)          
  WHERE           
     ( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and          
      ( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and    
      ( nullif( [tb_reducao_suplementacao_id_reducao_suplementacao], 0 ) is null or tb_reducao_suplementacao_id_reducao_suplementacao = [tb_reducao_suplementacao_id_reducao_suplementacao] )    and    
	  ( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and    
      (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and   
      (  nr_agrupamento = @nr_agrupamento )   and     
      ( nr_seq = @nr_seq )            
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_MES_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_MES_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_EXCLUIR]   
	@id_mes  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes]
	WHERE 
		id_mes = @id_mes

END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_MES_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_MES_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria
-- ===================================================================  
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_SALVAR] 
           @id_mes int =NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_reducao_suplementacao_id_reducao_suplementacao int =NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int= NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @ds_mes varchar(9) =NULL,
           @vr_mes numeric = null,
		   @cd_unidade_gestora varchar(10) = null

as
begin

	set nocount on;

	if exists (
		select	1 
		from	[movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)
		where	id_mes = @id_mes
	)
	begin

	UPDATE [movimentacao].[tb_movimentacao_orcamentaria_mes]
   SET [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao
      ,[tb_reducao_suplementacao_id_reducao_suplementacao] = @tb_reducao_suplementacao_id_reducao_suplementacao
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
      ,[nr_agrupamento] = @nr_agrupamento
      ,[nr_seq] = @nr_seq
      ,[ds_mes] = @ds_mes
      ,[vr_mes] = @vr_mes
	  ,[cd_unidade_gestora] = @cd_unidade_gestora

	   

	     		where	id_mes = @id_mes;

		select @id_mes;


			end
	else
	begin

	INSERT INTO [movimentacao].[tb_movimentacao_orcamentaria_mes]
           ([tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
           ,[tb_reducao_suplementacao_id_reducao_suplementacao]
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
           ,[nr_agrupamento]
           ,[nr_seq]
           ,[ds_mes]
           ,[vr_mes]
		   ,[cd_unidade_gestora])
     VALUES
           (@tb_distribuicao_movimentacao_id_distribuicao_movimentacao
           ,@tb_reducao_suplementacao_id_reducao_suplementacao
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
           ,@nr_agrupamento
           ,@nr_seq
           ,@ds_mes
           ,@vr_mes
		   ,@cd_unidade_gestora)



		select scope_identity();

	end

end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_NC_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_NC_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_NC_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_NC_CONSULTAR]          
           --@id_cancelamento_movimentacao int =NULL,        
           --@tb_fonte_id_fonte int =NULL,        
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,        
           @nr_agrupamento int =NULL        
           --@nr_seq int =NULL        
           --@nr_nota_cancelamento varchar(10) =NULL,        
           --@cd_unidade_gestora varchar(15) =NULL,        
           --@nr_categoria_gasto varchar(15) =NULL,        
           --@ds_observacao varchar(231) =NULL,        
           --@fg_transmitido_prodesp char(1) =NULL,        
           --@fg_transmitido_siafem char(1)= NULL        
        
          
AS            
BEGIN            
 SET NOCOUNT ON;          
        
        
        
SELECT n.[id_nota_credito] as IdNotaCredito        
      ,n.[tb_programa_id_programa] as ProgramaId        
      ,n.[tb_fonte_id_fonte] as Fonte        
      ,n.[tb_estrutura_id_estrutura] as IdEstrutura        
      ,n.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]  as IdMovimentacao        
      ,n.[nr_agrupamento] as NrAgrupamento        
      ,n.[nr_seq] as NrSequencia         
      ,n.[nr_nota_credito] as NrNotaCredito        
      ,n.[cd_unidade_gestora_favorecido]  as UnidadeGestoraFavorecida    
      ,n.[cd_uo] as Uo   
	  ,n.[cd_ugo] as UGO      
      ,n.[plano_interno] as plano_interno        
      ,n.[vr_credito] as ValorCredito       
      ,n.[ds_observacao] as ObservacaoNC  
	  ,n.[ds_observacao2] as ObservacaoNC2 
	  ,n.[ds_observacao3] as ObservacaoNC3      
      , o.cd_unidade_gestora_emitente as UnidadeGestoraEmitente    
   ,n.eventoNC as EventoNC  
   ,n.cd_gestao_favorecido as IdGestaoFavorecida  
    ,n.[fg_transmitido_prodesp]   as StatusProdespItem  
      ,n.[ds_msgRetornoProdesp]   as MensagemProdespItem  
      ,n.[fg_transmitido_siafem]   as StatusSiafemItem  
      ,n.[ds_msgRetornoSiafem]  as MensagemSiafemItem  
  FROM [movimentacao].[tb_credito_movimentacao] n        
     inner join movimentacao.tb_movimentacao_orcamentaria o on n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria    
        
  WHERE         
  ( n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and         
  (  n.nr_agrupamento = @nr_agrupamento )           
        
        
  ORDER BY id_nota_credito,nr_seq         
      
      
      
        
        
        
        
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:  Alessandro de Santana 
-- Create date: 31/07/2018  
-- Description: Procedure para consultar ultimo agrupamento 
-- ===================================================================   
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO]  
as    
begin    
    
 SET NOCOUNT ON;    
    
 SELECT TOP 1   
  [nr_agrupamento_movimentacao]  
 FROM  [movimentacao].[tb_movimentacao_orcamentaria]  (nolock)  
 ORDER BY  
  [nr_agrupamento_movimentacao] desc  
     
end;
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR]        
   @id_movimentacao_orcamentaria int =NULL,      
 @nr_agrupamento_movimentacao int= NULL,      
 @nr_siafem varchar(15)= NULL,      
 @tb_regional_id_regional int= NULL,      
 @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao int =NULL,      
 @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int =NULL,      
 @cd_unidade_gestora_emitente varchar(10) =NULL,      
 @cd_gestao_emitente varchar(10) =NULL,      
 @nr_ano_exercicio int= NULL,      
 @fg_transmitido_siafem char(1) =NULL,      
 @dt_cadastro datetime =NULL,      
 @fg_transmitido_prodesp char(1) =NULL      
      
      
AS          
BEGIN          
 SET NOCOUNT ON;        
      
      
      
      
SELECT [id_movimentacao_orcamentaria]      
      ,[nr_agrupamento_movimentacao]   as NumAgrupamento    
      ,[nr_siafem]      
      ,[tb_regional_id_regional]      
      --,[tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao]      
      ,[tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria]      
      ,[cd_unidade_gestora_emitente]      
      ,[cd_gestao_emitente]      
      ,[nr_ano_exercicio]      
      ,[fg_transmitido_siafem]      
      ,[bl_transmitido_siafem]      
   ,[bl_transmitir_siafem]      
      ,[dt_trasmitido_siafem]      
      ,[fg_transmitido_prodesp]      
      ,[bl_transmitido_prodesp]      
   ,[bl_transmitir_prodesp]      
      ,[dt_trasmitido_prodesp]      
      ,[ds_msgRetornoProdesp]      
      ,[ds_msgRetornoSiafem]      
      ,[bl_cadastro_completo]      
      ,[dt_cadastro]     
   ,[tb_programa_id_programa] as Programa  
      ,[tb_fonte_id_fonte]  as IdFonte    
      ,[tb_estrutura_id_estrutura] as Estrutura     
  FROM [movimentacao].[tb_movimentacao_orcamentaria]      
      
  WHERE       
  ( nullif( @id_movimentacao_orcamentaria, 0 ) is null or id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria )   and       
  ( nullif( @nr_agrupamento_movimentacao, 0 ) is null or nr_agrupamento_movimentacao = @nr_agrupamento_movimentacao )   and       
  (@nr_siafem is null or @nr_siafem = @nr_siafem) and      
    ( nullif( @tb_regional_id_regional, 0 ) is null or tb_regional_id_regional = @tb_regional_id_regional )   and       
  ( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and       
  ( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and       
     (@cd_unidade_gestora_emitente is null or cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and      
   (@cd_gestao_emitente is null or cd_gestao_emitente = @cd_gestao_emitente) and      
 ( nullif( @nr_ano_exercicio, 0 ) is null or nr_ano_exercicio = @nr_ano_exercicio )   and       
   (@fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem) and      
  (@fg_transmitido_prodesp is null or fg_transmitido_prodesp = @fg_transmitido_prodesp) and      
  ([dt_cadastro] is null or dt_cadastro = [dt_cadastro] )         
       
      
      
      
      
  ORDER BY id_movimentacao_orcamentaria      
      
      
      
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID] 
 @nr_agrupamento_movimentacao int= NULL,  
 @nr_siafem varchar(15)= NULL,  
 @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao int =NULL,  
 @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int =NULL,  
 @cd_unidade_gestora_emitente varchar(10) =NULL,  
 @cd_gestao_emitente varchar(10) =NULL,  
 @cd_unidade_gestora_favorecido varchar(10) =NULL,  
 @cd_gestao_favorecido varchar(10) =NULL,  
 @tb_programa_id_programa int =NULL,
 @tb_estrutura_id_estrutura int =NULL,
 @dt_cadastramento_de date = NULL    ,  
 @dt_cadastramento_ate date = NULL   ,  
 @fg_transmitido_siafem char(1) =NULL,  
 @fg_transmitido_prodesp char(1) =NULL  
  
                  
AS      
BEGIN      
 SET NOCOUNT ON;    


 CREATE TABLE #auxiliar
    (
	        IDMov int,
			Id int,
			numAgrupamento int,
			DescDocumento varchar(30),  
			NumSiafem varchar(30),
			UnidadeGestoraEmitente varchar(30), 
			UnidadeGestoraFavorecida varchar(30), 
			idCFP varchar(30),
			idCED varchar(30),
			Valor int,
			DataCadastro datetime,
			TransmitidoProdesp char(1),
			TransmitidoSiafem char(1),
			MsgProdesp  varchar(200),
			MsgSiafem varchar(200)
     )
	            
      Declare @Teste int
	  if(@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = 0)
	  Begin
	  select  @Teste = null   
	  end
	  else
	  begin
	   select  @Teste = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao   
	  end
	   




	 if( @Teste is null or (      @Teste = 1 or @Teste = 6 or @Teste = 7))
	 begin
	

	       --CANCELAMENTOS
           Insert into #auxiliar (
		    IDMov,
            Id ,
			numAgrupamento ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem  )
			
			(
			select 
			o.id_movimentacao_orcamentaria,
			c.id_cancelamento_movimentacao,
			c.nr_agrupamento,
			'Cancelamento',
			c.nr_nota_cancelamento,
			o.cd_unidade_gestora_emitente,
		    c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem

			from  [movimentacao].[tb_cancelamento_movimentacao] c
			inner join [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			inner join [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			inner join [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura

			--where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = 138
			Where
				( nullif( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   and   
				(@nr_siafem is null or c.nr_nota_cancelamento = @nr_siafem) and  
				--( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and   
				( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and   
				
				(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and  
				(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) and  

				(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) and  
				(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) and  

				(nullif(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) and  
				(nullif(@tb_estrutura_id_estrutura,0) is null or o.tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura) and  

				( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     and  
				( @dt_cadastramento_ate is null or o.[dt_cadastro] <= @dt_cadastramento_ate )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 

				)

END




	 if(@Teste is null or ( @Teste = 2 or @Teste = 7))
	 begin

		--NOTAS CREDITOS
           Insert into #auxiliar (
		   IDMov,
                  Id ,
			numAgrupamento ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem )
			
			(
			select 
			o.id_movimentacao_orcamentaria,
			c.id_nota_credito,
			c.nr_agrupamento,
			'Nota de Crdito',
			c.nr_nota_credito,
			o.cd_unidade_gestora_emitente,
		    c.cd_unidade_gestora_favorecido,
			p.cd_cfp,
			e.cd_natureza,
			c.vr_credito,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem

			from  [movimentacao].[tb_credito_movimentacao] c
			inner join [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			inner join [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			inner join [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura

				
			Where	( nullif( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   and   
				(@nr_siafem is null or c.nr_nota_credito = @nr_siafem) and  
				--( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and   
				( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and   
				
				(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and  
				(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) and  

				(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido) and  
				(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) and  

				(nullif(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) and  
				(nullif(@tb_estrutura_id_estrutura,0) is null or o.tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura) and  

				( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     and  
				( @dt_cadastramento_ate is null or o.[dt_cadastro] <= @dt_cadastramento_ate )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
			)



			END



	 if(@Teste is null or (@Teste = 2 or @Teste = 3 or @Teste = 8))
	 begin
           --DISTRIBUICAO
           Insert into #auxiliar (
		   IDMov,
                Id ,
			numAgrupamento ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem  )
			
			(
			select 
			o.id_movimentacao_orcamentaria,
			c.id_distribuicao_movimentacao,
			c.nr_agrupamento,
			'Distribuio',
			c.nr_nota_distribuicao,
			o.cd_unidade_gestora_emitente,
		    c.cd_unidade_gestora_favorecido,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem

			from  [movimentacao].[tb_distribuicao_movimentacao] c
			inner join [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			inner join [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			inner join [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura

			where 
				( nullif( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   and   
				(@nr_siafem is null or c.nr_nota_distribuicao = @nr_siafem) and  
				--( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and   
				( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and   
				
				(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and  
				(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) and  

				(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido) and  
				(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) and  

				(nullif(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) and  
				(nullif(@tb_estrutura_id_estrutura,0) is null or o.tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura) and  

				( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     and  
				( @dt_cadastramento_ate is null or o.[dt_cadastro] <= @dt_cadastramento_ate )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
			)

			END




	 if(@Teste is null or (@Teste = 1 or @Teste = 4 or @Teste = 7))
	 begin
			 --REDUCAO
             Insert into #auxiliar (
			 IDMov,
                  Id ,
			numAgrupamento ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem  )
			
			(
			select 
			o.id_movimentacao_orcamentaria,
			c.id_reducao_suplementacao,
			c.nr_agrupamento,
			'Reduo',
			c.nr_suplementacao_reducao,
			o.cd_unidade_gestora_emitente,
		    c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem

			from  [movimentacao].[tb_reducao_suplementacao] c
			inner join [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			inner join [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			inner join [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura

			where 
							( nullif( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   and   
				(@nr_siafem is null or c.nr_suplementacao_reducao = @nr_siafem) and  
				--( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and   
				( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and   
				
				(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and  
				(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) and  

				(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) and  
				(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) and  

				(nullif(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) and  
				(nullif(@tb_estrutura_id_estrutura,0) is null or o.tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura) and  

				( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     and  
				( @dt_cadastramento_ate is null or o.[dt_cadastro] <= @dt_cadastramento_ate )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) and c.flag_red_sup = 'R'
			)

			END

			


  if(@Teste is null or ( @Teste = 2 or @Teste = 3 or @Teste = 5))
	 begin
			 --SUPLEMENTACAO
Insert into #auxiliar (
               IDMov,
                  Id ,
			numAgrupamento ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem  )
			
			(
			select 
			o.id_movimentacao_orcamentaria,
			c.id_reducao_suplementacao,
			c.nr_agrupamento,
			'Suplementao',
			c.nr_suplementacao_reducao,
			o.cd_unidade_gestora_emitente,
		    c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem

			from  [movimentacao].[tb_reducao_suplementacao] c
			inner join [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			inner join [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			inner join [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura

			where
										( nullif( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   and   
				(@nr_siafem is null or c.nr_suplementacao_reducao = @nr_siafem) and  
				--( nullif( @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao, 0 ) is null or tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao )   and   
				( nullif( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   and   
				
				(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) and  
				(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) and  

				(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) and  
				(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) and  

				(nullif(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) and  
				(nullif(@tb_estrutura_id_estrutura,0) is null or o.tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura) and  

				( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     and  
				( @dt_cadastramento_ate is null or o.[dt_cadastro] <= @dt_cadastramento_ate )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) and c.flag_red_sup = 'S'
			)
END







			select 
			IDMov as id_mov,
		    Id  as id_movimentacao_orcamentaria,
			numAgrupamento  as nr_agrupamento_movimentacao,
			DescDocumento  as desc_documento,  
			NumSiafem  as nr_siafem,
			UnidadeGestoraEmitente as cd_unidade_gestora_emitente, 
			UnidadeGestoraFavorecida as Ug_favorecida, 
			idCFP  as cd_estrutura,
			idCED  as cd_natureza,
			Valor  as valor_geral,
			DataCadastro  as dt_cadastro,
			TransmitidoProdesp  as fg_transmitido_prodesp,
			TransmitidoSiafem  as fg_transmitido_siafem,
			MsgProdesp   as ds_msgRetornoProdesp,
			MsgSiafem  as ds_msgRetornoSiafem
			
			
			
			
			
			
			
			
			 from #auxiliar order by  numAgrupamento,DescDocumento


  
  

  
  
  
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_CONSULTAR]  
           @id_evento int= NULL,
           @cd_evento int= NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int= NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @cd_inscricao_evento varchar(10)= NULL,
           @cd_classificacao int =NULL,
           @cd_fonte varchar(15)= NULL,
           @rec_despesa varchar(10)= NULL



AS    
BEGIN    
 SET NOCOUNT ON;  


SELECT [id_evento]
      ,[cd_evento]
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
      ,[nr_agrupamento]
      ,[nr_seq]
      ,[cd_inscricao_evento]
      ,[cd_classificacao]
      ,[cd_fonte]
      ,[rec_despesa]
      ,[vr_evento]
  FROM [movimentacao].[tb_movimentacao_orcamentaria_evento]



  WHERE 
  ( nullif( @id_evento, 0 ) is null or id_evento = @id_evento )   and 
  ( nullif( @cd_evento, 0 ) is null or cd_evento = @cd_evento )   and 
  ( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and 
  ( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and 
  (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
    (  nr_agrupamento = @nr_agrupamento )   and 
	(  nr_seq = @nr_seq )   and 

   (@cd_inscricao_evento is null or cd_inscricao_evento = @cd_inscricao_evento  ) and
    (@cd_classificacao is null or cd_classificacao= @cd_classificacao  ) and
	 (@cd_fonte is null or cd_fonte= @cd_fonte  ) and
	 (@rec_despesa is null or rec_despesa   = @rec_despesa  ) 

  ORDER BY id_evento,nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR]   
	@id_evento  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria_evento]
	WHERE 
		id_evento = @id_evento
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria
-- ===================================================================  
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR] 
            @id_evento int =NULL,
            @cd_evento int =NULL,
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL,
           @nr_seq int =NULL,
           @cd_inscricao_evento varchar(10) =NULL,
           @cd_classificacao int =NULL,
           @cd_fonte varchar(15) =NULL,
           @rec_despesa varchar(10) =NULL,
           @vr_evento int =NULL

as
begin

	set nocount on;

	if exists (
		select	1 
		from	movimentacao.tb_movimentacao_orcamentaria_evento (nolock)
		where	id_evento  = @id_evento
	)
	begin


	UPDATE [movimentacao].[tb_movimentacao_orcamentaria_evento]
   SET [cd_evento] = @cd_evento
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
      ,[nr_agrupamento] = @nr_agrupamento
      ,[nr_seq] = @nr_seq
      ,[cd_inscricao_evento] = @cd_inscricao_evento
      ,[cd_classificacao] = @cd_classificacao
      ,[cd_fonte] = @cd_fonte
      ,[rec_despesa] = @rec_despesa
      ,[vr_evento] = @vr_evento
	

	   

	     		where	id_evento = @id_evento;

		select @id_evento;


			end
	else
	begin

	INSERT INTO [movimentacao].[tb_movimentacao_orcamentaria_evento]
           ([cd_evento]
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
           ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
           ,[nr_agrupamento]
           ,[nr_seq]
           ,[cd_inscricao_evento]
           ,[cd_classificacao]
           ,[cd_fonte]
           ,[rec_despesa]
           ,[vr_evento])
     VALUES
           (@cd_evento
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao
           ,@tb_distribuicao_movimentacao_id_distribuicao_movimentacao
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
           ,@nr_agrupamento
           ,@nr_seq
           ,@cd_inscricao_evento
           ,@cd_classificacao
           ,@cd_fonte
           ,@rec_despesa
           ,@vr_evento)


		select scope_identity();

	end

end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR]   
	@id_movimentacao_orcamentaria  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria]
	WHERE 
		id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR]     
 @id  int  
AS    
BEGIN    
  
 SET NOCOUNT ON;    



 DELETE  [movimentacao].[tb_cancelamento_movimentacao] 
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id




  DELETE  [movimentacao].[tb_credito_movimentacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id


  DELETE  [movimentacao].[tb_distribuicao_movimentacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id


  DELETE  [movimentacao].[tb_movimentacao_orcamentaria_mes]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id

  DELETE  [movimentacao].[tb_reducao_suplementacao]
 where tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @id

  


 DELETE FROM  [movimentacao].[tb_movimentacao_orcamentaria]  
 WHERE   
  id_movimentacao_orcamentaria = @id







    
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria
-- ===================================================================  
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR] 
	@id_movimentacao_orcamentaria int =NULL,
	@nr_agrupamento_movimentacao int =NULL,
	@nr_siafem varchar(15) =NULL,
	@tb_regional_id_regional int= NULL,
	@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao int =NULL,
	@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int =NULL,
	@cd_unidade_gestora_emitente int =NULL,
	@cd_gestao_emitente int =NULL,
	@nr_ano_exercicio int =NULL,
	@fg_transmitido_siafem char(1) =NULL,
	@bl_transmitido_siafem bit =NULL,
	@bl_transmitir_siafem bit =NULL,
	@dt_trasmitido_siafem datetime =NULL,
	@fg_transmitido_prodesp char(1) =NULL,
	@bl_transmitido_prodesp bit =NULL,
	@bl_transmitir_prodesp bit =NULL,
	@dt_trasmitido_prodesp datetime= NULL,
	@ds_msgRetornoProdesp varchar(140)= NULL,
	@ds_msgRetornoSiafem varchar(140)= NULL,
	@bl_cadastro_completo bit =NULL,
	@dt_cadastro datetime =NULL,
	@tb_programa_id_programa int =NULL    ,
    @tb_fonte_id_fonte varchar(10)= NULL ,   
    @tb_estrutura_id_estrutura int= NULL    


as  
begin

	set nocount on;

	if exists (
		select	1 
		from	[movimentacao].[tb_movimentacao_orcamentaria]  (nolock)
		where	id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria
	)
	begin

	 update [movimentacao].[tb_movimentacao_orcamentaria] set
	nr_siafem  = @nr_siafem,
	tb_regional_id_regional =nullif(@tb_regional_id_regional,0) ,
	tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao ,
	tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria,
	cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente ,
	cd_gestao_emitente = @cd_gestao_emitente,
	nr_ano_exercicio  = @nr_ano_exercicio,
	fg_transmitido_siafem = @fg_transmitido_siafem,
	bl_transmitido_siafem =@bl_transmitido_siafem,
	bl_transmitir_siafem = @bl_transmitir_siafem,
	dt_trasmitido_siafem =    @dt_trasmitido_siafem,
	fg_transmitido_prodesp =@fg_transmitido_prodesp,
	bl_transmitido_prodesp =@bl_transmitido_prodesp,
	bl_transmitir_prodesp =@bl_transmitir_prodesp,
	dt_trasmitido_prodesp =@dt_trasmitido_prodesp,
	ds_msgRetornoProdesp =@ds_msgRetornoProdesp,
	ds_msgRetornoSiafem =@ds_msgRetornoSiafem,
	bl_cadastro_completo =@bl_cadastro_completo,
	tb_programa_id_programa = @tb_programa_id_programa,
	tb_fonte_id_fonte = @tb_fonte_id_fonte,
	tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura


		where	id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria;

		select @id_movimentacao_orcamentaria;

	end
	else
	begin

	insert into   [movimentacao].[tb_movimentacao_orcamentaria]    
	        ([nr_agrupamento_movimentacao]
           ,[nr_siafem]
           ,[tb_regional_id_regional]
           ,[tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao]
           ,[tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria]
           ,[cd_unidade_gestora_emitente]
           ,[cd_gestao_emitente]
           ,[nr_ano_exercicio]
           ,[fg_transmitido_siafem]
           ,[bl_transmitido_siafem]
		   ,[bl_transmitir_siafem]
           ,[dt_trasmitido_siafem]
           ,[fg_transmitido_prodesp]
           ,[bl_transmitido_prodesp]
		   ,[bl_transmitir_prodesp]
           ,[dt_trasmitido_prodesp]
           ,[ds_msgRetornoProdesp]
           ,[ds_msgRetornoSiafem]
           ,[bl_cadastro_completo]
		   ,[dt_cadastro]
		   ,[tb_programa_id_programa]
		   ,[tb_fonte_id_fonte]
		   ,[tb_estrutura_id_estrutura])
     VALUES
	 (
	nullif(@nr_agrupamento_movimentacao,0) ,
	@nr_siafem ,
	@tb_regional_id_regional ,
	@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao ,
	@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria ,
	@cd_unidade_gestora_emitente ,
	@cd_gestao_emitente ,
	@nr_ano_exercicio ,
	'N' ,
	@bl_transmitido_siafem ,
	@bl_transmitir_siafem ,
	@dt_trasmitido_siafem ,
	'N' ,
	@bl_transmitido_prodesp ,
	@bl_transmitir_prodesp ,
	@dt_trasmitido_prodesp ,
	@ds_msgRetornoProdesp ,
	@ds_msgRetornoSiafem ,
	@bl_cadastro_completo ,
	@dt_cadastro,
	@tb_programa_id_programa,
	@tb_fonte_id_fonte,
	@tb_estrutura_id_estrutura
	 )

		select scope_identity();

	end

end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_CONSULTAR]  
           --@id_cancelamento_movimentacao int =NULL,
           --@tb_fonte_id_fonte int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL
           --@nr_seq int =NULL
           --@nr_nota_cancelamento varchar(10) =NULL,
           --@cd_unidade_gestora varchar(15) =NULL,
           --@nr_categoria_gasto varchar(15) =NULL,
           --@ds_observacao varchar(231) =NULL,
           --@fg_transmitido_prodesp char(1) =NULL,
           --@fg_transmitido_siafem char(1)= NULL

		
AS    
BEGIN    
 SET NOCOUNT ON;  

  

SELECT 
	  rs.id_reducao_suplementacao as IdReducaoSuplementacao
	  ,rs.cd_destino_recurso as DestinoRecurso
	  ,rs.nr_orgao as NrOrgao
	  ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao
	  ,rs.valor as ValorTotal,
	  rs.cd_gestao_favorecido
	  
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs 

  WHERE 
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  rs.nr_agrupamento = @nr_agrupamento )    and flag_red_sup = 'R'
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_reducao_suplementacao,nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
    
    
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR]    
            @id_reducao_suplementacao int =NULL,    
            @tb_credito_movimentacao_id_nota_credito int =NULL,    
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao int =NULL,    
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int =NULL,    
           @tb_programa_id_programa int =NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
           @nr_agrupamento int =NULL,    
           @nr_seq int =NULL,    
           @nr_suplementacao_reducao varchar(10) =NULL,    
           @fl_proc varchar(10) =NULL,    
           @nr_processo varchar(60) =NULL,    
           @nr_orgao varchar(10) =NULL,    
           @nr_obra varchar(15) =NULL,    
           @flag_red_sup char(1) =NULL,    
           @nr_cnpj_cpf_ug_credor varchar(15) =NULL,    
           @ds_autorizado_supra_folha varchar(10) =NULL,    
           @cd_origem_recurso varchar(10) =NULL,    
           @cd_destino_recurso varchar(10) =NULL,    
           @cd_especificacao_despesa varchar(10) =NULL,    
           @fg_transmitido_prodesp char(1) =NULL,    
           @fg_transmitido_siafem char(1) =NULL    
    
    
    
    
AS        
BEGIN        
 SET NOCOUNT ON;      
    
    
SELECT [id_reducao_suplementacao]   as IdReducaoSuplementacao  
      ,[tb_credito_movimentacao_id_nota_credito]   as IdNotaCredito  
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]   as IdDistribuicao  
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]   as IdCancelamento  
      ,[tb_programa_id_programa]   as ProgramaId  
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]   as IdMovimentacao  
      ,[nr_agrupamento]   as NrAgrupamento  
      ,[nr_seq]   as NrSequencia  
      ,[nr_suplementacao_reducao]   as  NrSuplementacaoReducao  
      ,[fl_proc]   as FlProc  
      ,[nr_processo]   as NrProcesso  
      ,[nr_orgao]   as  NrOrgao  
      ,[nr_obra]   as  NrObra  
      ,[flag_red_sup]   as FlagRedistribuicao  
      ,[nr_cnpj_cpf_ug_credor]   as NrCnpjCpf  
      ,[ds_autorizado_supra_folha]   as  AutorizadoSupraFolha  
      ,[cd_origem_recurso]   as  OrigemRecurso  
      ,[cd_destino_recurso]  as DestinoRecurso  
      ,[cd_especificacao_despesa]  as EspecDespesa  
      ,[ds_especificacao_despesa]  as DescEspecDespesa  
      ,[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura   
      ,[cd_autorizado_grupo]  as CodigoAutorizadoGrupo  
      ,[cd_autorizado_orgao]  as CodigoAutorizadoOrgao  
      ,[ds_autorizado_cargo]   as DescricaoAutorizadoCargo  
      ,[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura  
      ,[cd_examinado_assinatura]  as CodigoExaminadoAssinatura  
      ,[cd_examinado_grupo]  as CodigoExaminadoGrupo  
      ,[cd_examinado_orgao]  as  CodigoExaminadoOrgao  
      ,[ds_examinado_cargo]  as DescricaoExaminadoCargo  
      ,[nm_examinado_assinatura]  as NomeExaminadoAssinatura  
      ,[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura  
      ,[cd_responsavel_grupo]   as CodigoResponsavelGrupo   
      ,[cd_responsavel_orgao]  as CodigoResponsavelOrgao  
      ,[ds_responsavel_cargo]  as DescricaoResponsavelCargo  
   ,[nm_responsavel_assinatura] as NomeResponsavelAssinatura  
      ,[fg_transmitido_prodesp]   as StatusProdespItem  
      ,[ds_msgRetornoProdesp]   as MensagemProdespItem  
      ,[fg_transmitido_siafem]   as StatusSiafemItem  
      ,[ds_msgRetornoSiafem]  as MensagemSiafemItem   
   ,[cd_unidade_gestora] as UnidadeGestoraFavorecida  
   ,[cd_gestao_favorecido] as IdGestaoFavorecida  
   ,[TotalQ1]  
   ,[TotalQ2]  
   ,[TotalQ3]  
   ,[TotalQ4]  
  FROM [movimentacao].[tb_reducao_suplementacao]    
    
    
  WHERE     
    
    ( nullif( @id_reducao_suplementacao, 0 ) is null or id_reducao_suplementacao = @id_reducao_suplementacao )   and     
  ( nullif( @tb_credito_movimentacao_id_nota_credito, 0 ) is null or tb_credito_movimentacao_id_nota_credito = @tb_credito_movimentacao_id_nota_credito )   and     
    ( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and     
  ( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and     
    ( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa )   and     
  ( nullif(@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria   ,0) is null or  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria ) 
    and     
    (  nr_agrupamento = @nr_agrupamento )   and     
  (  nr_seq = @nr_seq )   and     
  
  (@nr_suplementacao_reducao is null or nr_suplementacao_reducao = @nr_suplementacao_reducao  ) and    
  (@fl_proc is null or fl_proc = @fl_proc  ) and    
  (@nr_processo is null or nr_processo = @nr_processo  ) and    
  (@nr_orgao is null or nr_processo = @nr_orgao  ) and    
  (@nr_obra is null or nr_obra = @nr_obra  ) and    
  (@flag_red_sup is null or flag_red_sup = @flag_red_sup  ) and    
  (@nr_cnpj_cpf_ug_credor is null or nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor  ) and    
  (@ds_autorizado_supra_folha is null or ds_autorizado_supra_folha = @ds_autorizado_supra_folha  ) and    
    
  (@cd_origem_recurso is null or cd_origem_recurso = @cd_origem_recurso  ) and    
  (@cd_destino_recurso is null or cd_destino_recurso = @cd_destino_recurso  ) and    
  (@cd_especificacao_despesa is null or cd_especificacao_despesa = @cd_especificacao_despesa  ) and    
  (@fg_transmitido_prodesp is null or fg_transmitido_prodesp = @fg_transmitido_prodesp  ) and    
  (@fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem  )     
     
         
    
  ORDER BY id_reducao_suplementacao,nr_seq    
    
    
    
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016  
-- Description: Procedure para excluso de valores de empenho
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR]   
	@id_reducao_suplementacao  int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM  [movimentacao].[tb_reducao_suplementacao]
	WHERE 
		id_reducao_suplementacao = @id_reducao_suplementacao
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================      
-- Author:  Alessandro de Santanao  
-- Create date: 31/07/2018  
-- Description: Procedure para salvar ou alterar Movimentao Oramentaria  
-- ===================================================================    
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR]   
  
            @id_reducao_suplementacao INT =NULL,  
           @tb_credito_movimentacao_id_nota_credito INT =NULL,  
           @tb_distribuicao_movimentacao_id_distribuicao_movimentacao INT =NULL,  
           @tb_cancelamento_movimentacao_id_cancelamento_movimentacao INT =NULL,  
           @tb_programa_id_programa INT =NULL,  
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria INT =NULL,  
           @nr_agrupamento INT =NULL,  
           @nr_seq INT =NULL,  
           @nr_suplementacao_reducao varchar(15) =NULL,  
           @fl_proc varchar(10) =NULL,  
           @nr_processo varchar(60) =NULL,  
           @nr_orgao varchar(15) =NULL,  
           @nr_obra varchar(10) =NULL,  
           @flag_red_sup char(1)= NULL,  
           @nr_cnpj_cpf_ug_credor varchar(15)= NULL,  
           @ds_autorizado_supra_folha varchar(4) =NULL,  
           @cd_origem_recurso varchar(10) =NULL,  
           @cd_destino_recurso varchar(10) =NULL,  
           @cd_especificacao_despesa varchar(10) =NULL,  
           @ds_especificacao_despesa varchar(632) =NULL,  
           @cd_autorizado_assinatura varchar(5)= NULL,  
           @cd_autorizado_grupo int =NULL,  
           @cd_autorizado_orgao varchar(2)= NULL,  
           @ds_autorizado_cargo varchar(55) =NULL,  
           @nm_autorizado_assinatura varchar(55)= NULL,  
           @cd_examinado_assinatura varchar(5) =NULL,  
           @cd_examinado_grupo int =NULL,  
           @cd_examinado_orgao varchar(2) =NULL,  
           @ds_examinado_cargo varchar(55)= NULL,  
           @nm_examinado_assinatura varchar(55) =NULL,  
           @cd_responsavel_assinatura varchar(5)= NULL,  
           @cd_responsavel_grupo int =NULL,  
           @cd_responsavel_orgao varchar(2)= NULL,  
           @ds_responsavel_cargo varchar(140)= NULL,  
           @nm_responsavel_assinatura varchar(55)= NULL,  
           @fg_transmitido_prodesp char(1) =NULL,  
           @ds_msgRetornoProdesp varchar(140)= NULL,  
           @fg_transmitido_siafem char(1)= NULL,  
           @ds_msgRetornoSiafem varchar(140)= NULL,  
			 @valor int = NULL,  
			 @cd_unidade_gestora varchar(10)= NULL ,
			 @cd_gestao_favorecido varchar(10) =NULL ,
			 @TotalQ1 numeric = null,
			 @TotalQ2 numeric = null,
			 @TotalQ3 numeric = null,
			 @TotalQ4 numeric = null
	
  
as  
begin  
  
 set nocount on;  
  
 if exists (  
  select 1   
  from [movimentacao].[tb_reducao_suplementacao] (nolock)  
  where id_reducao_suplementacao = @id_reducao_suplementacao  
 )  
 begin  
  
 UPDATE [movimentacao].[tb_reducao_suplementacao]  
   SET [tb_credito_movimentacao_id_nota_credito] = @tb_credito_movimentacao_id_nota_credito  
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao] = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao  
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao] = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao  
      ,[tb_programa_id_programa] = @tb_programa_id_programa  
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria  
      ,[nr_agrupamento] = @nr_agrupamento  
      ,[nr_seq] = @nr_seq  
      ,[nr_suplementacao_reducao] = @nr_suplementacao_reducao  
      ,[fl_proc] = @fl_proc  
      ,[nr_processo] = @nr_processo  
      ,[nr_orgao] = @nr_orgao  
      ,[nr_obra] = @nr_obra  
      ,[flag_red_sup] = @flag_red_sup  
      ,[nr_cnpj_cpf_ug_credor] = @nr_orgao  
      ,[ds_autorizado_supra_folha] = @ds_autorizado_supra_folha  
      ,[cd_origem_recurso] = @cd_origem_recurso  
      ,[cd_destino_recurso] = @cd_destino_recurso  
      ,[cd_especificacao_despesa] = @cd_especificacao_despesa  
      ,[ds_especificacao_despesa] = @ds_especificacao_despesa  
      ,[cd_autorizado_assinatura] = @cd_autorizado_assinatura  
      ,[cd_autorizado_grupo] = @cd_autorizado_grupo  
      ,[cd_autorizado_orgao] = @cd_autorizado_orgao  
      ,[ds_autorizado_cargo] = @ds_autorizado_cargo  
      ,[nm_autorizado_assinatura] = @nm_autorizado_assinatura  
      ,[cd_examinado_assinatura] = @cd_examinado_assinatura  
      ,[cd_examinado_grupo] = @cd_examinado_grupo  
      ,[cd_examinado_orgao] = @cd_examinado_orgao  
      ,[ds_examinado_cargo] = @ds_examinado_cargo  
      ,[nm_examinado_assinatura] = @nm_examinado_assinatura  
      ,[cd_responsavel_assinatura] = @cd_responsavel_assinatura  
      ,[cd_responsavel_grupo] = @cd_responsavel_grupo  
      ,[cd_responsavel_orgao] = @cd_responsavel_orgao  
      ,[ds_responsavel_cargo] = @ds_responsavel_cargo  
      ,[nm_responsavel_assinatura] = @nm_responsavel_assinatura  
      ,[fg_transmitido_prodesp] = @fg_transmitido_prodesp  
      ,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp  
      ,[fg_transmitido_siafem] = @fg_transmitido_siafem  
      ,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem  
   ,[valor] = @valor  
   ,[cd_unidade_gestora] = @cd_unidade_gestora 
   ,[cd_gestao_favorecido] = @cd_gestao_favorecido 
   ,[TotalQ1] = @TotalQ1
   ,[TotalQ2] = @TotalQ2
   ,[TotalQ3] = @TotalQ3
   ,[TotalQ4] = @TotalQ4
  
      
  
        where id_reducao_suplementacao = @id_reducao_suplementacao;  
  
  select @id_reducao_suplementacao;  
  
  
   end  
 else  
 begin  
  
 INSERT INTO [movimentacao].[tb_reducao_suplementacao]  
           ([tb_credito_movimentacao_id_nota_credito]  
           ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]  
           ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]  
           ,[tb_programa_id_programa]  
           ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]  
           ,[nr_agrupamento]  
           ,[nr_seq]  
           ,[nr_suplementacao_reducao]  
           ,[fl_proc]  
           ,[nr_processo]  
           ,[nr_orgao]  
           ,[nr_obra]  
           ,[flag_red_sup]  
           ,[nr_cnpj_cpf_ug_credor]  
           ,[ds_autorizado_supra_folha]  
           ,[cd_origem_recurso]  
           ,[cd_destino_recurso]  
           ,[cd_especificacao_despesa]  
           ,[ds_especificacao_despesa]  
           ,[cd_autorizado_assinatura]  
           ,[cd_autorizado_grupo]  
           ,[cd_autorizado_orgao]  
           ,[ds_autorizado_cargo]  
           ,[nm_autorizado_assinatura]  
           ,[cd_examinado_assinatura]  
           ,[cd_examinado_grupo]  
           ,[cd_examinado_orgao]  
           ,[ds_examinado_cargo]  
           ,[nm_examinado_assinatura]  
           ,[cd_responsavel_assinatura]  
           ,[cd_responsavel_grupo]  
           ,[cd_responsavel_orgao]  
           ,[ds_responsavel_cargo]  
           ,[nm_responsavel_assinatura]  
           ,[fg_transmitido_prodesp]  
           ,[ds_msgRetornoProdesp]  
           ,[fg_transmitido_siafem]  
           ,[ds_msgRetornoSiafem]  
     ,[valor]  
     ,[cd_unidade_gestora]
	 ,[cd_gestao_favorecido]
	 ,[TotalQ1]
	 ,[TotalQ2]
	 ,[TotalQ3]
	 ,[TotalQ4])  
     VALUES  
           (@tb_credito_movimentacao_id_nota_credito  
           ,@tb_distribuicao_movimentacao_id_distribuicao_movimentacao  
           ,@tb_cancelamento_movimentacao_id_cancelamento_movimentacao  
           ,@tb_programa_id_programa  
           ,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria  
           ,@nr_agrupamento  
           ,@nr_seq  
           ,@nr_suplementacao_reducao  
           ,@fl_proc  
           ,@nr_processo  
           ,@nr_orgao  
           ,@nr_obra  
           ,@flag_red_sup  
           ,@nr_orgao  
           ,@ds_autorizado_supra_folha  
           ,@cd_origem_recurso  
           ,@cd_destino_recurso  
           ,@cd_especificacao_despesa  
           ,@ds_especificacao_despesa  
           ,@cd_autorizado_assinatura  
           ,@cd_autorizado_grupo  
           ,@cd_autorizado_orgao  
           ,@ds_autorizado_cargo  
           ,@nm_autorizado_assinatura  
           ,@cd_examinado_assinatura  
           ,@cd_examinado_grupo  
           ,@cd_examinado_orgao  
           ,@ds_examinado_cargo  
           ,@nm_examinado_assinatura  
           ,@cd_responsavel_assinatura  
           ,@cd_responsavel_grupo  
           ,@cd_responsavel_orgao  
           ,@ds_responsavel_cargo  
      ,@nm_responsavel_assinatura  
           ,'N'  
           ,@ds_msgRetornoProdesp  
           ,'N'  
           ,@ds_msgRetornoSiafem  
     ,@valor  
     ,@cd_unidade_gestora
	 ,@cd_gestao_favorecido
	 ,@TotalQ1
	 ,@TotalQ2
	 ,@TotalQ3
	 ,@TotalQ4)  
  
  
  
  select scope_identity();  
  
 end  
  
end
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR]   
           --@id_cancelamento_movimentacao int =NULL,    
           --@tb_fonte_id_fonte int =NULL,    
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,    
           @nr_agrupamento int =NULL    
           --@nr_seq int =NULL    
           --@nr_nota_cancelamento varchar(10) =NULL,    
           --@cd_unidade_gestora varchar(15) =NULL,    
           --@nr_categoria_gasto varchar(15) =NULL,    
           --@ds_observacao varchar(231) =NULL,    
           --@fg_transmitido_prodesp char(1) =NULL,    
           --@fg_transmitido_siafem char(1)= NULL    
    
      
AS        
BEGIN        
 SET NOCOUNT ON;      
    
    
    
SELECT 
      [id_reducao_suplementacao]   as IdReducaoSuplementacao 
      ,[tb_credito_movimentacao_id_nota_credito]   as IdNotaCredito
      ,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]   as IdDistribuicao
      ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]   as IdCancelamento
      ,[tb_programa_id_programa]   as ProgramaId
      ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]   as IdMovimentacao
      ,[nr_agrupamento]   as NrAgrupamento
      ,[nr_seq]   as NrSequencia
      ,[nr_suplementacao_reducao]   as  NrSuplementacaoReducao
      ,[fl_proc]   as FlProc
      ,[nr_processo]   as NrProcesso
      ,[nr_orgao]   as  NrOrgao
      ,[nr_obra]   as  NrObra
      ,[flag_red_sup]   as FlagRedistribuicao
      ,[nr_cnpj_cpf_ug_credor]   as NrCnpjCpf
      ,[ds_autorizado_supra_folha]   as  AutorizadoSupraFolha
      ,[cd_origem_recurso]   as  OrigemRecurso
      ,[cd_destino_recurso]  as DestinoRecurso
      ,[cd_especificacao_despesa]  as EspecDespesa
      ,[ds_especificacao_despesa]  as DescEspecDespesa
      ,[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura 
      ,[cd_autorizado_grupo]  as CodigoAutorizadoGrupo
      ,[cd_autorizado_orgao]  as CodigoAutorizadoOrgao
      ,[ds_autorizado_cargo]   as DescricaoAutorizadoCargo
      ,[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura
      ,[cd_examinado_assinatura]  as CodigoExaminadoAssinatura
      ,[cd_examinado_grupo]  as CodigoExaminadoGrupo
      ,[cd_examinado_orgao]  as  CodigoExaminadoOrgao
      ,[ds_examinado_cargo]  as DescricaoExaminadoCargo
      ,[nm_examinado_assinatura]  as NomeExaminadoAssinatura
      ,[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura
      ,[cd_responsavel_grupo]   as CodigoResponsavelGrupo 
      ,[cd_responsavel_orgao]  as CodigoResponsavelOrgao
      ,[ds_responsavel_cargo]  as DescricaoResponsavelCargo
	  ,[nm_responsavel_assinatura] as NomeResponsavelAssinatura
      ,[fg_transmitido_prodesp]   as StatusProdespItem
      ,[ds_msgRetornoProdesp]   as MensagemProdespItem
      ,[fg_transmitido_siafem]   as StatusSiafemItem
      ,[ds_msgRetornoSiafem]  as MensagemSiafemItem 
	  ,[cd_unidade_gestora] as UnidadeGestoraFavorecida
	  ,[cd_gestao_favorecido] as IdGestaoFavorecida
	  ,[TotalQ1]
	  ,[TotalQ2]
	  ,[TotalQ3]
	  ,[TotalQ4]
	  ,[valor] as ValorRedSup
       
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs   
    
  WHERE     
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
  (  rs.nr_agrupamento = @nr_agrupamento )      
  -- ( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and     
  --( nullif( @nr_agrupamento, 0 ) is null or c.nr_agrupamento = @nr_agrupamento )        
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )        
    
    
  ORDER BY id_reducao_suplementacao,nr_seq    
    
    
    
  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ==============================================================              
-- Author:  Alessandro de Santana              
-- Create date: 25/07/2018             
-- Description: Procedure para consulta de valor de movimentacao              
-- ==============================================================              
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR]         
   @id_mes   int = null              
,@tb_reducao_suplementacao_id_reducao_suplementacao int = null          
, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null           
, @nr_agrupamento int = null        
,@nr_seq int = null        
,@ds_mes varchar(9) = null      
,@vr_mes decimal = null      
AS              
BEGIN              
 SET NOCOUNT ON;              
              
  SELECT              
   [id_mes]              
  , [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]              
  ,[tb_reducao_suplementacao_id_reducao_suplementacao]            
  ,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]            
  ,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]            
  ,[nr_agrupamento]            
  ,[nr_seq]            
  , [ds_mes]             
  , [vr_mes]    
  ,[cd_unidade_gestora]            
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)              
  WHERE               
     (  nullif(@id_mes,0   ) is null or   id_mes = @id_mes ) and              
      (  tb_reducao_suplementacao_id_reducao_suplementacao = @tb_reducao_suplementacao_id_reducao_suplementacao )    and        
      (  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and       
      ( nr_agrupamento = @nr_agrupamento )   and         
      (  @nr_seq = @nr_seq )                
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR]  
           --@id_cancelamento_movimentacao int =NULL,
           --@tb_fonte_id_fonte int =NULL,
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,
           @nr_agrupamento int =NULL
           --@nr_seq int =NULL
           --@nr_nota_cancelamento varchar(10) =NULL,
           --@cd_unidade_gestora varchar(15) =NULL,
           --@nr_categoria_gasto varchar(15) =NULL,
           --@ds_observacao varchar(231) =NULL,
           --@fg_transmitido_prodesp char(1) =NULL,
           --@fg_transmitido_siafem char(1)= NULL

		
AS    
BEGIN    
 SET NOCOUNT ON;  

  

SELECT 
	  rs.id_reducao_suplementacao as IdReducaoSuplementacao
	  ,rs.cd_destino_recurso as DestinoRecurso
	  ,rs.nr_orgao as NrOrgao
	  ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao
	  ,rs.valor as ValorTotal
	  
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs 

  WHERE 
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  rs.nr_agrupamento = @nr_agrupamento )    and flag_red_sup = 'S'
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_reducao_suplementacao,nr_seq



  END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_TIPO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_TIPO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
  
-- ==============================================================  
-- Author:  Alessandro Santana  
-- Create date: 19/09/2016  
-- Description: Procedure para consulta de tipo de movimentao  
-- ==============================================================  
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_CONSULTAR]  
 @ds_tipo_movimentacao_orcamentaria varchar(100) = null,  
 @id_tipo_movimentacao_orcamentaria int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 SELECT mov.id_tipo_movimentacao_orcamentaria
      ,mov.ds_tipo_movimentacao_orcamentariao 
  FROM [movimentacao].[tb_tipo_movimentacao_orcamentaria] mov (nolock)  
  where (mov.ds_tipo_movimentacao_orcamentariao = @ds_tipo_movimentacao_orcamentaria or @ds_tipo_movimentacao_orcamentaria is null)  
  and (mov.id_tipo_movimentacao_orcamentaria = @id_tipo_movimentacao_orcamentaria or isnull(@id_tipo_movimentacao_orcamentaria,0) = 0)  

  order by mov.id_tipo_movimentacao_orcamentaria
  
END
GO
PRINT N'Creating [dbo].[PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
  
-- ==============================================================  
-- Author:  Alessandro Santana  
-- Create date: 19/09/2016  
-- Description: Procedure para consulta de tipo de documento de  movimentao  
-- ==============================================================  
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR]  
 @ds_tipo_documento_movimentacao varchar(100) = null,  
 @id_tipo_documento_movimentacao int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 SELECT doc.id_tipo_documento_movimentacao 
      ,doc.ds_tipo_documento_movimentacao  
  FROM [movimentacao].[tb_tipo_documento_movimentacao] doc (nolock)  
  where (doc.ds_tipo_documento_movimentacao = @ds_tipo_documento_movimentacao or @ds_tipo_documento_movimentacao is null)  
  and (doc.id_tipo_documento_movimentacao = @id_tipo_documento_movimentacao or isnull(@id_tipo_documento_movimentacao,0) = 0)  

  order by doc.ds_tipo_documento_movimentacao 
  
END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW] --NULL, 571
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = 571,
	@ds_numpd varchar(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tempLista 
		(--pdexecucao
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		[cd_transmissao_status_siafem] [char](1) NULL,
		[fl_transmissao_transmitido_siafem] [bit] NULL,
		[dt_transmissao_transmitido_siafem] [date] NULL,
		[ds_transmissao_mensagem_siafem] varchar(140) NULL,
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		[cd_transmissao_status_prodesp] varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15)
	)

	DECLARE @ds_numob varchar(50),
			@ob_cancelada bit,
			@ug varchar(50),
			@gestao varchar(50),
			@ug_pagadora varchar(50),
			@ug_liquidante varchar(50),
			@gestao_pagadora varchar(50),
			@gestao_liguidante varchar(50),
			@favorecido varchar(20),
			@favorecidoDesc varchar(120),
			@ordem varchar(50),
			@ano_pd varchar(4),
			@valor varchar(50),
			@ds_noup varchar(1),
			@nr_agrupamento_pd int,
			@fl_sistema_prodesp bit,
			@cd_transmissao_status_siafem char(1),
			@fl_transmissao_transmitido_siafem bit,
			@dt_transmissao_transmitido_siafem date,
			@ds_transmissao_mensagem_siafem varchar(140),
			@nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@cd_transmissao_status_prodesp varchar(1),
			@fl_transmissao_transmitido_prodesp bit,
			@dt_transmissao_transmitido_prodesp datetime,
			@ds_transmissao_mensagem_prodesp varchar(140),
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@nr_contrato varchar(13), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15)

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_objects CURSOR FOR

		SELECT	ITEM.id_programacao_desembolso_execucao_item, ITEM.id_execucao_pd, ITEM.ds_numpd, ITEM.ds_numob, ITEM.ob_cancelada, ITEM.ug, ITEM.gestao,ITEM.ug_pagadora, ITEM.ug_liquidante, ITEM.gestao_pagadora, ITEM.gestao_liguidante, 
				ITEM.favorecido, ITEM.favorecidoDesc, ITEM.ordem, ITEM.ano_pd, ITEM.valor, ITEM.ds_noup, ITEM.nr_agrupamento_pd, ITEM.fl_sistema_prodesp, ITEM.cd_transmissao_status_siafem, ITEM.fl_transmissao_transmitido_siafem, 
				ITEM.dt_transmissao_transmitido_siafem, ITEM.ds_transmissao_mensagem_siafem, ITEM.nr_documento_gerador, ITEM.ds_consulta_op_prodesp, ITEM.nr_op 
					
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
	
		WHERE		(@id_programacao_desembolso_execucao_item is null or ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item) AND
	  				(@id_execucao_pd is null or ITEM.id_execucao_pd = @id_execucao_pd) AND
					(@ds_numpd is null or ITEM.ds_numpd = @ds_numpd)
					--AND ITEM.nr_agrupamento_pd <> 0
	
		ORDER BY	ITEM.id_programacao_desembolso_execucao_item

	-- Abrindo Cursor para leitura
	OPEN cursor_objects

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_objects 
	INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
			@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
			@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		--Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@cd_transmissao_status_prodesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao), 
					@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento), 
					@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento), 
					@nr_contrato = PD.nr_contrato, 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador, 
					@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento), 
					@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor),
					@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		INSERT	#tempLista
				(id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto)
		VALUES	(@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto)

		-- Lendo a prxima linha
		FETCH NEXT FROM cursor_objects 
		INTO	@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,
				@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, @ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,
				@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, @nr_op
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_objects

	-- Desalocando o cursor
	DEALLOCATE cursor_objects

	SELECT * FROM #tempLista
	DROP TABLE #tempLista

END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description:	Procedure para consulta de execuo de pd
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD] --NULL, 556
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
		ITEM.id_programacao_desembolso_execucao_item, 
		ITEM.id_execucao_pd, 
		ITEM.ds_numpd,
		ITEM.ds_numob, 
		ITEM.ob_cancelada,
		ITEM.ug,
		ITEM.gestao,
		ITEM.ug_pagadora, 
		ITEM.ug_liquidante, 
		ITEM.gestao_pagadora, 
		ITEM.gestao_liguidante, 
		ITEM.favorecido, 
		ITEM.favorecidoDesc, 
		ITEM.ordem, 
		ITEM.ano_pd, 
		ITEM.valor, 
		ITEM.ds_noup, 
		ITEM.nr_agrupamento_pd, 
		ITEM.fl_sistema_prodesp,
		[pi].dt_confirmacao,
		[pi].id_confirmacao_pagamento,
		[pi].id_confirmacao_pagamento_item,
		[pi].cd_transmissao_status_prodesp, 
		[pi].fl_transmissao_transmitido_prodesp, 
		[pi].dt_transmissao_transmitido_prodesp, 
		[pi].ds_transmissao_mensagem_prodesp, 
		ITEM.cd_transmissao_status_siafem, 
		ITEM.fl_transmissao_transmitido_siafem, 
		ITEM.dt_transmissao_transmitido_siafem, 
		ITEM.ds_transmissao_mensagem_siafem,
		ITEM.nr_documento_gerador,
		ITEM.ds_consulta_op_prodesp,
		ITEM.nr_op,
		ISNULL(PD.dt_emissao, PDA.dt_emissao) AS dt_emissao,
		ISNULL(PD.dt_vencimento, PDA.dt_vencimento) as dt_vencimento,
		ISNULL(PD.nr_documento, PDA.nr_documento) as nr_documento,
		PD.nr_contrato,
		ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador,
		ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) as id_tipo_documento,
		ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) as nr_cnpj_cpf_credor,
		ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) as nr_cnpj_cpf_pgto
  FROM [contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
  		--LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE 
	  		( nullif( @id_programacao_desembolso_execucao_item, 0 ) is null or ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item ) AND
	  		( nullif( @id_execucao_pd, 0 ) is null or ITEM.id_execucao_pd = @id_execucao_pd ) AND
			( nullif( @ds_numpd, 0 ) is null or ITEM.ds_numpd = @ds_numpd )
			AND ITEM.nr_agrupamento_pd <> 0
		ORDER BY 
			ITEM.id_programacao_desembolso_execucao_item
END



/* 

SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_item'
*/
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS]   
	@id_execucao_pd int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  

	--IF ISNULL(@id_execucao_pd, 0) = 0
		DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
		WHERE	nr_agrupamento_pd = 0
		AND		cd_transmissao_status_siafem IS NULL
		AND		NOT EXISTS (SELECT	id_execucao_pd 
							FROM	pagamento.tb_confirmacao_pagamento_item WHERE [contaunica].[tb_programacao_desembolso_execucao_item].id_execucao_pd = pagamento.tb_confirmacao_pagamento_item.id_execucao_pd
																			AND	 LEFT([contaunica].[tb_programacao_desembolso_execucao_item].nr_documento_gerador, 17) = LEFT(pagamento.tb_confirmacao_pagamento_item.nr_documento_gerador, 17))


	--ELSE
	--	DELETE FROM [contaunica].[tb_programacao_desembolso_execucao_item]
	--	WHERE	id_execucao_pd = @id_execucao_pd
	--	AND		nr_agrupamento_pd = 0

	SELECT @@ROWCOUNT;

END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW] --NULL, NULL, NULL, '2018PD00621'
	@tipo int,
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null
AS
BEGIN
	
	SET NOCOUNT ON;

	IF ISNULL(@tipo, 1) = 1
	BEGIN

	CREATE TABLE #tempListaGrid 
		(--pdexecucao
		[id_programacao_desembolso_execucao_item] int NULL,
		[id_execucao_pd] int NULL,
		[ds_numpd] varchar(50) NULL,
		[ds_numob] varchar(50) NULL,
		[ob_cancelada] [bit] NULL,
		[ug] varchar(50) NULL,
		[gestao] varchar(50) NULL,
		[ug_pagadora] varchar(50) NULL,
		[ug_liquidante] varchar(50) NULL,
		[gestao_pagadora] varchar(50) NULL,
		[gestao_liguidante] varchar(50) NULL,
		[favorecido] varchar(20) NULL,
		[favorecidoDesc] varchar(120) NULL,
		[ordem] varchar(50) NULL,
		[ano_pd] varchar(4) NULL,
		[valor] varchar(50) NULL,
		[ds_noup] varchar(1) NULL,
		[nr_agrupamento_pd] int NULL,
		[fl_sistema_prodesp] [bit] NULL,
		[cd_transmissao_status_siafem] [char](1) NULL,
		[fl_transmissao_transmitido_siafem] [bit] NULL,
		[dt_transmissao_transmitido_siafem] [date] NULL,
		[ds_transmissao_mensagem_siafem] varchar(140) NULL,
		[nr_documento_gerador] varchar(50) NULL, --tambm na PD ou PDA
		[ds_consulta_op_prodesp] varchar(140) NULL,
		[nr_op] varchar(50) NULL,
		--fim pdexecucao
		--confirmacaoPagto
		id_confirmacao_pagamento int NULL,
		id_confirmacao_pagamento_item int NULL,
		cd_transmissao_status_prodesp varchar(1) NULL,
		[fl_transmissao_transmitido_prodesp] [bit] NULL,
		[dt_transmissao_transmitido_prodesp] [datetime] NULL,
		[ds_transmissao_mensagem_prodesp] varchar(140) NULL,
		[dt_confirmacao] [datetime] NULL,
		--fim confirmacaoPagto
		--PD ou PDAgrupamento
		dt_emissao datetime NULL, 
		dt_vencimento datetime NULL, 
		nr_documento varchar(20) NULL, 
		nr_contrato varchar(13) NULL, 
		id_tipo_documento int NULL, 
		nr_cnpj_cpf_credor varchar(15),
		nr_cnpj_cpf_pgto varchar(15),
		dt_cadastro datetime,
		id_tipo_execucao_pd int
	)

	DECLARE @nr_documento_gerador varchar(50), --tambm na PD ou PDA
			@ds_consulta_op_prodesp varchar(140),
			@nr_op varchar(50),
			--fim pdexecucao
			--confirmacaoPagto
			@id_confirmacao_pagamento int,
			@id_confirmacao_pagamento_item int,
			@dt_confirmacao datetime,
			--fim confirmacaoPagto
			--PD ou PDAgrupamento
			@dt_emissao datetime, 
			@dt_vencimento datetime, 
			@nr_documento varchar(20), 
			@id_tipo_documento int, 
			@nr_cnpj_cpf_credor varchar(15),
			@nr_cnpj_cpf_pgto varchar(15),
			@dt_cadastro datetime,
			@id_tipo_execucao_pd int,
			@TransmissaoStatusProdesp char(1)

	-- Cursor para percorrer os nomes dos objetos 
	DECLARE cursor_execucao CURSOR FOR

		SELECT	ITEM.[id_programacao_desembolso_execucao_item], ITEM.[nr_agrupamento_pd],ITEM.[id_execucao_pd],ITEM.[ds_numpd],ITEM.[ug],ITEM.[gestao],ITEM.[ug_pagadora],ITEM.[ug_liquidante],ITEM.[gestao_pagadora],ITEM.[gestao_liguidante],
				ITEM.[favorecido],ITEM.[favorecidoDesc],ITEM.[ordem],ITEM.[ano_pd],ITEM.[valor],ITEM.[ds_noup],ITEM.[nr_agrupamento_pd],ITEM.[ds_numob],ITEM.[ob_cancelada],ITEM.[fl_sistema_prodesp],
				ITEM.nr_documento_gerador,ITEM.nr_op,ITEM.[cd_transmissao_status_siafem],ITEM.[fl_transmissao_transmitido_siafem],ITEM.[dt_transmissao_transmitido_siafem],ITEM.[ds_transmissao_mensagem_siafem]
				--ex.[dt_cadastro], 
				--ISNULL(PD.dt_vencimento, PDA.dt_vencimento) AS dt_vencimento, ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) AS nr_cnpj_cpf_credor, ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) AS nr_cnpj_cpf_pgto
		FROM contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) ITEM
		--LEFT JOIN contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		--LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		--LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		--LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		--LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE
		--ITEM.nr_agrupamento_pd <> 0 AND
		(@ob_cancelada is null or ITEM.ob_cancelada = @ob_cancelada) AND
		(@ds_numpd is null or ITEM.ds_numpd LIKE '%' + @ds_numpd + '%') AND
		(@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%') AND
		(@favorecido is null or ITEM.favorecido = @favorecido) AND
		(@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		--(@tipoExecucao is null or Ex.id_tipo_execucao_pd = @tipoExecucao) AND
		(@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_status_siafem = @cd_transmissao_status_siafem)
		--(@cd_transmissao_status_prodesp IS NULL OR ISNULL([pi].cd_transmissao_status_prodesp, 'N') = @cd_transmissao_status_prodesp OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) AND
		--(@de is null or ex.dt_cadastro >= @de ) AND
		--(@ate is null or ex.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	OPEN cursor_execucao

	-- Lendo a prxima linha
	FETCH NEXT FROM cursor_execucao 
	INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
			@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
			

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		--SELECT @id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd

		----Dados da Confirmao
		SELECT	@dt_confirmacao = [pi].dt_confirmacao,
				@id_confirmacao_pagamento = [pi].id_confirmacao_pagamento,
				@id_confirmacao_pagamento_item = [pi].id_confirmacao_pagamento_item,
				@TransmissaoStatusProdesp = [pi].cd_transmissao_status_prodesp, 
				@fl_transmissao_transmitido_prodesp = [pi].fl_transmissao_transmitido_prodesp, 
				@dt_transmissao_transmitido_prodesp = [pi].dt_transmissao_transmitido_prodesp, 
				@ds_transmissao_mensagem_prodesp = [pi].ds_transmissao_mensagem_prodesp 
		FROM	pagamento.tb_confirmacao_pagamento_item (NOLOCK) [pi]
		WHERE	id_execucao_pd = @id_execucao_pd
		AND		id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item
		
		--SELECT	@nr_documento_gerador, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @cd_transmissao_status_prodesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao

		--Dados da Programao de Desembolso (PD)
		SELECT		@dt_emissao = ISNULL(PD.dt_emissao, PDA.dt_emissao) 
					,@dt_vencimento = ISNULL(PD.dt_vencimento, PDA.dt_vencimento) 
					,@nr_documento = ISNULL(PD.nr_documento, PDA.nr_documento) 
					,@nr_contrato = PD.nr_contrato 
					--ISNULL(PD.nr_documento_gerador, PDA.nr_documento_gerador) as nr_documento_gerador 
					,@id_tipo_documento = ISNULL(PD.id_tipo_documento, PDA.id_tipo_documento) 
					,@nr_cnpj_cpf_credor = ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor)
					,@nr_cnpj_cpf_pgto = ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto)
					,@dt_cadastro = EX.dt_cadastro
					,@id_tipo_execucao_pd = EX.id_tipo_execucao_pd
		FROM		[contaunica].[tb_programacao_desembolso_execucao_item] ITEM (nolock)
		LEFT JOIN	contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN	[contaunica].[tb_programacao_desembolso] (NOLOCK) PD ON PD.nr_siafem_siafisico = ITEM.ds_numpd
		LEFT JOIN	[contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE		ITEM.id_execucao_pd = @id_execucao_pd
		AND			ITEM.id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item

		INSERT	#tempListaGrid
				(id_programacao_desembolso_execucao_item, id_execucao_pd, ds_numpd,ds_numob, ob_cancelada, ug, gestao, ug_pagadora, ug_liquidante, gestao_pagadora, gestao_liguidante, favorecido, favorecidoDesc, ordem, ano_pd, valor, 
				ds_noup, nr_agrupamento_pd, fl_sistema_prodesp,cd_transmissao_status_siafem, fl_transmissao_transmitido_siafem, dt_transmissao_transmitido_siafem, ds_transmissao_mensagem_siafem, nr_documento_gerador, ds_consulta_op_prodesp, 
				nr_op, id_confirmacao_pagamento, id_confirmacao_pagamento_item, cd_transmissao_status_prodesp, fl_transmissao_transmitido_prodesp, dt_transmissao_transmitido_prodesp, ds_transmissao_mensagem_prodesp, dt_confirmacao, dt_emissao, 
				dt_vencimento, nr_documento, nr_contrato, id_tipo_documento, nr_cnpj_cpf_credor, nr_cnpj_cpf_pgto, dt_cadastro, id_tipo_execucao_pd)
		VALUES	(@id_programacao_desembolso_execucao_item, @id_execucao_pd, @ds_numpd,@ds_numob, @ob_cancelada, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @fl_sistema_prodesp,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem, @nr_documento_gerador, @ds_consulta_op_prodesp, 
				@nr_op, @id_confirmacao_pagamento, @id_confirmacao_pagamento_item, @TransmissaoStatusProdesp, @fl_transmissao_transmitido_prodesp, @dt_transmissao_transmitido_prodesp, @ds_transmissao_mensagem_prodesp, @dt_confirmacao, @dt_emissao, 
				@dt_vencimento, @nr_documento, @nr_contrato, @id_tipo_documento, @nr_cnpj_cpf_credor, @nr_cnpj_cpf_pgto, @dt_cadastro, @id_tipo_execucao_pd)

		FETCH NEXT FROM cursor_execucao 
		INTO	@id_programacao_desembolso_execucao_item,  @nr_agrupamento_pd, @id_execucao_pd, @ds_numpd, @ug, @gestao, @ug_pagadora, @ug_liquidante, @gestao_pagadora, @gestao_liguidante, @favorecido, @favorecidoDesc, @ordem, @ano_pd, @valor, 
				@ds_noup, @nr_agrupamento_pd, @ds_numob, @ob_cancelada, @fl_sistema_prodesp, @nr_documento_gerador,@nr_op,@cd_transmissao_status_siafem, @fl_transmissao_transmitido_siafem, @dt_transmissao_transmitido_siafem, @ds_transmissao_mensagem_siafem
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_execucao

	-- Desalocando o cursor
	DEALLOCATE cursor_execucao

	SELECT	* 
	FROM	#tempListaGrid
	WHERE	(@tipoExecucao IS NULL OR id_tipo_execucao_pd = @tipoExecucao)
	AND		(@cd_transmissao_status_prodesp IS NULL OR cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp)
	AND		(@de is null or dt_cadastro >= @de )
	AND		(@ate is null or dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))


	DROP TABLE #tempListaGrid

	END

	ELSE
	BEGIN

		SELECT
			ITEM.id_autorizacao_ob
			, ITEM.nr_agrupamento_ob
			, ITEM.[favorecido] 
			, ITEM.[favorecidoDesc] 
			, ITEM_EX.ug_liquidante
			, ITEM_EX.ug_pagadora
			, ITEM_EX.gestao_liguidante
			, ITEM_EX.gestao_pagadora
			--, ITEM.ug_liquidante
			--, ITEM.ug_pagadora
			--, ITEM.gestao_liguidante
			--, ITEM.gestao_pagadora
			--, ITEM.[ordem] 
			--, ITEM.[ano_pd] 
			, ITEM.[valor] 
			--, ITEM.[ds_noup] 
			--, ITEM.[nr_agrupamento_pd] 
			, ITEM.[ds_numob] 
			--, ITEM.[ob_cancelada]
			--, ITEM.[fl_sistema_prodesp]
			, ITEM.fl_transmissao_item_siafem 
			, ITEM.cd_transmissao_item_status_siafem 
			, ITEM.[dt_transmissao_item_transmitido_siafem] 
			, ITEM.[ds_transmissao_item_mensagem_siafem] 

			--, ITEM.[fl_transmissao_transmitido_prodesp] 
			--, ITEM.[dt_transmissao_transmitido_prodesp] 
			--, ITEM.[ds_transmissao_mensagem_prodesp]
			--, [pi].dt_confirmacao
			--, [pi].cd_transmissao_status_prodesp
			--, [pi].fl_transmissao_transmitido_prodesp
			--, [pi].dt_transmissao_transmitido_prodesp
			--, [pi].ds_transmissao_mensagem_prodesp 
			,ISNULL([pi].dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao -- Transmisso Confirmao de Pagto
			,ISNULL([PI].cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp 
			,ISNULL([PI].fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp
			,ISNULL([PI].dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp
			,ISNULL([PI].ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp

			, ITEM.[dt_cadastro]
			--, PD.dt_vencimento
		FROM contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_autorizacao_ob = ITEM.id_autorizacao_ob AND [pi].id_autorizacao_ob_item = ITEM.id_autorizacao_ob_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].tb_programacao_desembolso_execucao_item (NOLOCK) as ITEM_EX ON ITEM_EX.id_execucao_pd = ITEM.id_execucao_pd AND ITEM_EX.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS PI_2 ON PI_2.id_execucao_pd = ITEM.id_execucao_pd AND PI_2.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		
		WHERE
		--ITEM.nr_agrupamento_ob <> 0 AND
		(ITEM.[ds_numob] IS NOT NULL)
		AND (@ug_pagadora is null or ITEM_EX.ug_pagadora = @ug_pagadora)
		AND (@gestao_pagadora is null or ITEM_EX.gestao_pagadora = @gestao_pagadora)
		AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			--(@favorecido is null or ITEM.favorecido = @favorecido) AND
		AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
		AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
		AND (@valor is null or ITEM.valor = @valor)
		AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
		AND (@cd_transmissao_status_prodesp IS NULL OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp OR [pi_2].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp )
		AND (@de is null or ITEM.dt_cadastro >= @de )
		AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
		--AND (@cd_aplicacao_obra IS NULL OR ITEM.cd_aplicacao_obra = @cd_aplicacao_obra)
		AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)

	END

END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================      
-- Author:		JOSE G BRAZ
-- Create date: 11/10/2017
-- Description:	Procedure para consulta de execuo de pd no grid
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD]
	@tipo int,
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor varchar(50) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@fl_transmissao_transmitido_prodesp bit = NULL,
	@dt_transmissao_transmitido_prodesp datetime = NULL,
	@ds_transmissao_mensagem_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,
	@tipoExecucao int = null,
	@de date = null,
	@ate date = null
AS
BEGIN
	
	SET NOCOUNT ON;

	IF ISNULL(@tipo, 1) = 1
		SELECT DISTINCT
			  ITEM.[id_programacao_desembolso_execucao_item] 
			, ITEM.[nr_agrupamento_pd] 
			, ITEM.[id_execucao_pd] 
			, ITEM.[ds_numpd] 
			, ITEM.[ug] 
			, ITEM.[gestao] 
			, ITEM.[ug_pagadora] 
			, ITEM.[ug_liquidante] 
			, ITEM.[gestao_pagadora] 
			, ITEM.[gestao_liguidante] 
			, ITEM.[favorecido] 
			, ITEM.[favorecidoDesc] 
			, ITEM.[ordem] 
			, ITEM.[ano_pd] 
			, ITEM.[valor] 
			, ITEM.[ds_noup] 
			, ITEM.[nr_agrupamento_pd] 
			, ITEM.[ds_numob] 
			, ITEM.[ob_cancelada]
			, ITEM.[fl_sistema_prodesp] 
			--, ITEM.[cd_transmissao_status_prodesp] 
			--, ITEM.[fl_transmissao_transmitido_prodesp] 
			--, ITEM.[dt_transmissao_transmitido_prodesp] 
			--, ITEM.[ds_transmissao_mensagem_prodesp] 
			, [pi].dt_confirmacao
			, [pi].cd_transmissao_status_prodesp
			, [pi].fl_transmissao_transmitido_prodesp
			, [pi].dt_transmissao_transmitido_prodesp
			, [pi].ds_transmissao_mensagem_prodesp
			, ITEM.nr_documento_gerador
			, ITEM.nr_op
			, ITEM.[cd_transmissao_status_siafem] 
			, ITEM.[fl_transmissao_transmitido_siafem] 
			, ITEM.[dt_transmissao_transmitido_siafem] 
			, ITEM.[ds_transmissao_mensagem_siafem] 
			, ex.[dt_cadastro]
			, ISNULL(PD.dt_vencimento, PDA.dt_vencimento) AS dt_vencimento
			, ISNULL(PD.nr_cnpj_cpf_credor, PDA.nr_cnpj_cpf_credor) AS nr_cnpj_cpf_credor
			, ISNULL(PD.nr_cnpj_cpf_pgto, PDA.nr_cnpj_cpf_pgto) AS nr_cnpj_cpf_pgto
		FROM contaunica.tb_programacao_desembolso_execucao_item (NOLOCK) ITEM
		LEFT JOIN contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = ITEM.id_execucao_pd 
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_execucao_pd = ITEM.id_execucao_pd AND [pi].id_programacao_desembolso_execucao_item = ITEM.id_programacao_desembolso_execucao_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON ITEM.ds_numpd = PD.nr_siafem_siafisico
		LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON ITEM.ds_numpd = PDA.nr_programacao_desembolso
		WHERE
		--ITEM.nr_agrupamento_pd <> 0 AND
		(@ob_cancelada is null or ITEM.ob_cancelada = @ob_cancelada) AND
		(@ds_numpd is null or ITEM.ds_numpd LIKE '%' + @ds_numpd + '%') AND
		(@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%') AND
		(@favorecido is null or ITEM.favorecido = @favorecido) AND
		(@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		(@tipoExecucao is null or Ex.id_tipo_execucao_pd = @tipoExecucao) AND
		(@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_status_siafem = @cd_transmissao_status_siafem) AND
		(@cd_transmissao_status_prodesp IS NULL OR ISNULL([pi].cd_transmissao_status_prodesp, 'N') = @cd_transmissao_status_prodesp OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp ) AND
		(@de is null or ex.dt_cadastro >= @de ) AND
		(@ate is null or ex.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

	ELSE
		SELECT
			ITEM.id_autorizacao_ob
			, ITEM.nr_agrupamento_ob
			, ITEM.[favorecido] 
			, ITEM.[favorecidoDesc] 
			, ITEM_EX.ug_liquidante
			, ITEM_EX.ug_pagadora
			, ITEM_EX.gestao_liguidante
			, ITEM_EX.gestao_pagadora
			--, ITEM.ug_liquidante
			--, ITEM.ug_pagadora
			--, ITEM.gestao_liguidante
			--, ITEM.gestao_pagadora
			--, ITEM.[ordem] 
			--, ITEM.[ano_pd] 
			, ITEM.[valor] 
			--, ITEM.[ds_noup] 
			--, ITEM.[nr_agrupamento_pd] 
			, ITEM.[ds_numob] 
			--, ITEM.[ob_cancelada]
			--, ITEM.[fl_sistema_prodesp]
			, ITEM.fl_transmissao_item_siafem 
			, ITEM.cd_transmissao_item_status_siafem 
			, ITEM.[dt_transmissao_item_transmitido_siafem] 
			, ITEM.[ds_transmissao_item_mensagem_siafem] 

			--, ITEM.[fl_transmissao_transmitido_prodesp] 
			--, ITEM.[dt_transmissao_transmitido_prodesp] 
			--, ITEM.[ds_transmissao_mensagem_prodesp]
			--, [pi].dt_confirmacao
			--, [pi].cd_transmissao_status_prodesp
			--, [pi].fl_transmissao_transmitido_prodesp
			--, [pi].dt_transmissao_transmitido_prodesp
			--, [pi].ds_transmissao_mensagem_prodesp 
			,ISNULL([pi].dt_confirmacao, PI_2.dt_confirmacao) dt_confirmacao -- Transmisso Confirmao de Pagto
			,ISNULL([PI].cd_transmissao_status_prodesp, PI_2.cd_transmissao_status_prodesp) cd_transmissao_status_prodesp 
			,ISNULL([PI].fl_transmissao_transmitido_prodesp, PI_2.fl_transmissao_transmitido_prodesp) fl_transmissao_transmitido_prodesp
			,ISNULL([PI].dt_transmissao_transmitido_prodesp, PI_2.dt_transmissao_transmitido_prodesp) dt_transmissao_transmitido_prodesp
			,ISNULL([PI].ds_transmissao_mensagem_prodesp, PI_2.ds_transmissao_mensagem_prodesp) ds_transmissao_mensagem_prodesp

			, ITEM.[dt_cadastro]
			--, PD.dt_vencimento
		FROM contaunica.tb_autorizacao_ob_itens (NOLOCK) ITEM
		LEFT JOIN [pagamento].tb_confirmacao_pagamento_item (nolock) as [pi] ON [pi].id_autorizacao_ob = ITEM.id_autorizacao_ob AND [pi].id_autorizacao_ob_item = ITEM.id_autorizacao_ob_item
		LEFT JOIN [pagamento].tb_confirmacao_pagamento (nolock) as p ON p.id_confirmacao_pagamento = [pi].id_confirmacao_pagamento
		LEFT JOIN [contaunica].tb_programacao_desembolso_execucao_item (NOLOCK) as ITEM_EX ON ITEM_EX.id_execucao_pd = ITEM.id_execucao_pd AND ITEM_EX.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS PI_2 ON PI_2.id_execucao_pd = ITEM.id_execucao_pd AND PI_2.id_programacao_desembolso_execucao_item = ITEM.id_execucao_pd_item
		
		WHERE
		--ITEM.nr_agrupamento_ob <> 0 AND
		(ITEM.[ds_numob] IS NOT NULL)
		AND (@ug_pagadora is null or ITEM_EX.ug_pagadora = @ug_pagadora)
		AND (@gestao_pagadora is null or ITEM_EX.gestao_pagadora = @gestao_pagadora)
		AND (@ds_numob is null or ITEM.ds_numob LIKE '%' + @ds_numob + '%')
			--(@favorecido is null or ITEM.favorecido = @favorecido) AND
		AND (@favorecidoDesc is null or ITEM.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' )
		AND (@cd_despesa is null or ITEM.cd_despesa = @cd_despesa)
		AND (@valor is null or ITEM.valor = @valor)
		AND (@cd_transmissao_status_siafem IS NULL OR ITEM.cd_transmissao_item_status_siafem = @cd_transmissao_status_siafem)
		AND (@cd_transmissao_status_prodesp IS NULL OR [pi].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp OR [pi_2].cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp )
		AND (@de is null or ITEM.dt_cadastro >= @de )
		AND (@ate is null or ITEM.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))
		--AND (@cd_aplicacao_obra IS NULL OR ITEM.cd_aplicacao_obra = @cd_aplicacao_obra)
		AND (@nr_contrato IS NULL OR ITEM.nr_contrato = @nr_contrato)

END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS] --'2018PD00401'
	@nr_siafem_siafisico varchar(11) = null
AS

	DECLARE @NumeroDocumentoGerador varchar(22)

	--IF LEN(@nr_siafem_siafisico) <= 5
	--	SET @tipo = 2

	--PD
	IF EXISTS(
			SELECT PD.id_programacao_desembolso
			FROM [contaunica].[tb_programacao_desembolso] PD (nolock)
			where nr_siafem_siafisico = @nr_siafem_siafisico
		)
		BEGIN 

			SELECT	@NumeroDocumentoGerador = nr_documento_gerador
			FROM	[contaunica].[tb_programacao_desembolso] PD (nolock)
			WHERE	nr_siafem_siafisico = @nr_siafem_siafisico

			SELECT DISTINCT
				PD.nr_siafem_siafisico as NumeroSiafem
				,PD.id_programacao_desembolso
				,EI.id_programacao_desembolso_execucao_item
				,PD.nr_cnpj_cpf_pgto
				,nr_siafem_siafisico
				,[PD].[id_tipo_documento]
				,[PD].[nr_contrato]
				,[PD].[nr_documento]
				,[PD].[nr_documento_gerador]
				,[PI].cd_transmissao_status_prodesp
				,[PI].fl_transmissao_transmitido_prodesp
				,[PI].dt_transmissao_transmitido_prodesp
				,[PI].ds_transmissao_mensagem_prodesp
				,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico
				,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico
				,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico
				,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico
			FROM		[contaunica].[tb_programacao_desembolso] PD (nolock)
			LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].id_execucao_pd = EI.id_execucao_pd
			WHERE		SUBSTRING(PD.nr_documento_gerador, 10, 8) = SUBSTRING(@NumeroDocumentoGerador, 10, 8)
			AND			PD.nr_siafem_siafisico <> @nr_siafem_siafisico
			AND			EI.nr_agrupamento_pd IS NULL
			ORDER BY NumeroSiafem DESC
		END

		ELSE

		BEGIN
			
			SELECT	@NumeroDocumentoGerador = nr_documento_gerador
			FROM	[contaunica].[tb_programacao_desembolso_agrupamento] PDA (nolock)
			WHERE	nr_programacao_desembolso = @nr_siafem_siafisico

			SELECT DISTINCT
				PDA.nr_programacao_desembolso as NumeroSiafem
				,EI.id_programacao_desembolso_execucao_item
				, PDA.nr_cnpj_cpf_pgto
				,PDA.[id_tipo_documento]
				--,[nr_contrato]
				,PDA.[nr_documento]
				,PDA.[nr_documento_gerador]

				,[PI].cd_transmissao_status_prodesp
					,[PI].fl_transmissao_transmitido_prodesp
					,[PI].dt_transmissao_transmitido_prodesp
					,[PI].ds_transmissao_mensagem_prodesp

				,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico
				,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico
				,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico
				,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico

			from [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock)
			LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PDA.nr_programacao_desembolso
			LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].id_execucao_pd = EI.id_execucao_pd
			WHERE	SUBSTRING(PDA.nr_documento_gerador, 10, 8) = SUBSTRING(@NumeroDocumentoGerador, 10, 8)
			AND		PDA.nr_programacao_desembolso <> @nr_siafem_siafisico
			AND		EI.nr_agrupamento_pd IS NULL
			ORDER BY PDA.nr_programacao_desembolso
		END
GO
PRINT N'Creating [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_NUMEROAGRUPAMENTO]
AS

	SELECT	ISNULL(MAX(nr_agrupamento_pd), 0) + 1
	FROM	contaunica.tb_programacao_desembolso_execucao_item (NOLOCK)
GO
PRINT N'Creating [dbo].[sp_confirmacao_pagamento_incluir]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.sp_confirmacao_pagamento_incluir'))
   EXEC('CREATE PROCEDURE [dbo].[sp_confirmacao_pagamento_incluir] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE dbo.sp_confirmacao_pagamento_incluir 
(
		   @id_confirmacao_pagamento_tipo int = null,
           @id_execucao_pd int = null,
           @id_autorizacao_ob int  = null,
           @id_tipo_documento int  = null,
           @nr_agrupamento int  = null,
           @ano_referencia int  = null,
           @id_tipo_pagamento int  = null,
           @dt_confirmacao datetime  = null,
           @dt_cadastro datetime  = null,
           @dt_modificacao datetime  = null,
           @vr_total_confirmado decimal(18,2)  = null,
           @cd_transmissao_status_prodesp varchar(50)  = null,
           @fl_transmissao_transmitido_prodesp bit  = null,
           @dt_transmissao_transmitido_prodesp datetime  = null,
           @ds_transmissao_mensagem_prodesp varchar(200)  = null,
           @dt_preparacao datetime  = null,
           @nr_conta varchar(20)  = null,
           @nr_documento varchar(30)  = null,
		   @id_confirmacao_pagamento int  = null,
           @id_programacao_desembolso_execucao_item int  = null,
           @id_autorizacao_ob_item int = null,
           @id_regional smallint  = null,
           @id_reclassificacao_retencao int  = null,
           @id_origem int  = null,
           @id_despesa_tipo int  = null,
           @dt_vencimento datetime  = null,
           @nr_contrato varchar(13)  = null,
           @cd_obra varchar(20)  = null,
           @nr_op varchar(50) = null,
           @nr_banco_pagador varchar(10) = null,
           @nr_agencia_pagador varchar(10) = null,
           @nr_conta_pagador varchar(10) = null,
           @nr_fonte_siafem varchar(50) = null,
           @nr_emprenho varchar(50) = null,
           @nr_processo varchar(20) = null,
           @nr_nota_fiscal int = null,
           @nr_nl_documento varchar(20) = null,
           @vr_documento decimal(8,2) = null,
           @nr_natureza_despesa int = null,
           @cd_credor_organizacao int = null,
           @nr_cnpj_cpf_ug_credor varchar(14) = null,
           @ds_referencia nvarchar(100) = null,
           @cd_orgao_assinatura varchar(2) = null,
           @nm_reduzido_credor varchar(14) = null
)
As
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  
insert into pagamento.tb_confirmacao_pagamento
           (id_confirmacao_pagamento_tipo
           ,id_execucao_pd
           ,id_autorizacao_ob
           ,id_tipo_documento
           ,nr_agrupamento
           ,ano_referencia
           ,id_tipo_pagamento
           ,dt_confirmacao
           ,dt_cadastro
           ,dt_modificacao
           ,vr_total_confirmado
           ,cd_transmissao_status_prodesp
           ,fl_transmissao_transmitido_prodesp
           ,dt_transmissao_transmitido_prodesp
           ,ds_transmissao_mensagem_prodesp
           ,dt_preparacao
           ,nr_conta
           ,nr_documento)
     values
           (@id_confirmacao_pagamento_tipo
           ,@id_execucao_pd
           ,@id_autorizacao_ob
           ,@id_tipo_documento
           ,@nr_agrupamento
           ,@ano_referencia
           ,@id_tipo_pagamento
           ,@dt_confirmacao
           ,@dt_cadastro
           ,@dt_modificacao
           ,@vr_total_confirmado
           ,@cd_transmissao_status_prodesp
           ,@fl_transmissao_transmitido_prodesp
           ,@dt_transmissao_transmitido_prodesp
           ,@ds_transmissao_mensagem_prodesp
           ,@dt_preparacao
           ,@nr_conta
           ,@nr_documento)
End

begin
		insert into pagamento.tb_confirmacao_pagamento_item
           (id_confirmacao_pagamento
           ,id_execucao_pd
           ,id_programacao_desembolso_execucao_item
           ,id_autorizacao_ob
           ,id_autorizacao_ob_item
           ,dt_confirmacao
           ,id_tipo_documento
           ,nr_documento
           ,id_regional
           ,id_reclassificacao_retencao
           ,id_origem
           ,id_despesa_tipo
           ,dt_vencimento
           ,nr_contrato
           ,cd_obra
           ,nr_op
           ,nr_banco_pagador
           ,nr_agencia_pagador
           ,nr_conta_pagador
           ,nr_fonte_siafem
           ,nr_emprenho
           ,nr_processo
           ,nr_nota_fiscal
           ,nr_nl_documento
           ,vr_documento
           ,nr_natureza_despesa
           ,cd_credor_organizacao
           ,nr_cnpj_cpf_ug_credor
           ,ds_referencia
           ,cd_orgao_assinatura
           ,nm_reduzido_credor
           ,fl_transmissao_transmitido_prodesp
           ,cd_transmissao_status_prodesp
           ,dt_transmissao_transmitido_prodesp
           ,ds_transmissao_mensagem_prodesp)
     VALUES
           (@id_confirmacao_pagamento
           ,@id_execucao_pd
           ,@id_programacao_desembolso_execucao_item
           ,@id_autorizacao_ob
           ,@id_autorizacao_ob_item
           ,@dt_confirmacao
           ,@id_tipo_documento
           ,@nr_documento
           ,@id_regional
           ,@id_reclassificacao_retencao
           ,@id_origem
           ,@id_despesa_tipo
           ,@dt_vencimento
           ,@nr_contrato
           ,@cd_obra
           ,@nr_op
           ,@nr_banco_pagador
           ,@nr_agencia_pagador
           ,@nr_conta_pagador
           ,@nr_fonte_siafem
           ,@nr_emprenho
           ,@nr_processo
           ,@nr_nota_fiscal
           ,@nr_nl_documento
           ,@vr_documento
           ,@nr_natureza_despesa
           ,@cd_credor_organizacao
           ,@nr_cnpj_cpf_ug_credor
           ,@ds_referencia
           ,@cd_orgao_assinatura
           ,@nm_reduzido_credor
           ,@fl_transmissao_transmitido_prodesp
           ,@cd_transmissao_status_prodesp
           ,@dt_transmissao_transmitido_prodesp
           ,@ds_transmissao_mensagem_prodesp)
end
COMMIT  
SELECT SCOPE_IDENTITY();
GO
PRINT N'Creating [dbo].[sp_confirmacao_pagamento_insert]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.sp_confirmacao_pagamento_insert'))
   EXEC('CREATE PROCEDURE [dbo].[sp_confirmacao_pagamento_insert] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE dbo.sp_confirmacao_pagamento_insert 
(
		   @id_confirmacao_pagamento_tipo int = null,
           @id_execucao_pd int = null,
           @id_autorizacao_ob int  = null,
           @id_tipo_documento int  = null,
           @nr_agrupamento int  = null,
           @ano_referencia int  = null,
           @id_tipo_pagamento int  = null,
           @dt_confirmacao datetime  = null,
           @dt_cadastro datetime  = null,
           @dt_modificacao datetime  = null,
           @vr_total_confirmado decimal(18,2)  = null,
           @cd_transmissao_status_prodesp varchar(50)  = null,
           @fl_transmissao_transmitido_prodesp bit  = null,
           @dt_transmissao_transmitido_prodesp datetime  = null,
           @ds_transmissao_mensagem_prodesp varchar(200)  = null,
           @dt_preparacao datetime  = null,
           @nr_conta varchar(20)  = null,
           @nr_documento varchar(30)  = null,
		   @id_confirmacao_pagamento int  = null,
           @id_programacao_desembolso_execucao_item int  = null,
           @id_autorizacao_ob_item int = null,
           @id_regional smallint  = null,
           @id_reclassificacao_retencao int  = null,
           @id_origem int  = null,
           @id_despesa_tipo int  = null,
           @dt_vencimento datetime  = null,
           @nr_contrato varchar(13)  = null,
           @cd_obra varchar(20)  = null,
           @nr_op varchar(50) = null,
           @nr_banco_pagador varchar(10) = null,
           @nr_agencia_pagador varchar(10) = null,
           @nr_conta_pagador varchar(10) = null,
           @nr_fonte_siafem varchar(50) = null,
           @nr_emprenho varchar(50) = null,
           @nr_processo varchar(20) = null,
           @nr_nota_fiscal int = null,
           @nr_nl_documento varchar(20) = null,
           @vr_documento decimal(8,2) = null,
           @nr_natureza_despesa int = null,
           @cd_credor_organizacao int = null,
           @nr_cnpj_cpf_ug_credor varchar(14) = null,
           @ds_referencia nvarchar(100) = null,
           @cd_orgao_assinatura varchar(2) = null,
           @nm_reduzido_credor varchar(14) = null
)
As
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  
insert into pagamento.tb_confirmacao_pagamento
           (id_confirmacao_pagamento_tipo
           ,id_execucao_pd
           ,id_autorizacao_ob
           ,id_tipo_documento
           ,nr_agrupamento
           ,ano_referencia
           ,id_tipo_pagamento
           ,dt_confirmacao
           ,dt_cadastro
           ,dt_modificacao
           ,vr_total_confirmado
           ,cd_transmissao_status_prodesp
           ,fl_transmissao_transmitido_prodesp
           ,dt_transmissao_transmitido_prodesp
           ,ds_transmissao_mensagem_prodesp
           ,dt_preparacao
           ,nr_conta
           ,nr_documento)
     values
           (@id_confirmacao_pagamento_tipo
           ,@id_execucao_pd
           ,@id_autorizacao_ob
           ,@id_tipo_documento
           ,@nr_agrupamento
           ,@ano_referencia
           ,@id_tipo_pagamento
           ,@dt_confirmacao
           ,@dt_cadastro
           ,@dt_modificacao
           ,@vr_total_confirmado
           ,@cd_transmissao_status_prodesp
           ,@fl_transmissao_transmitido_prodesp
           ,@dt_transmissao_transmitido_prodesp
           ,@ds_transmissao_mensagem_prodesp
           ,@dt_preparacao
           ,@nr_conta
           ,@nr_documento)
End

begin
		insert into pagamento.tb_confirmacao_pagamento_item
           (id_confirmacao_pagamento
           ,id_execucao_pd
           ,id_programacao_desembolso_execucao_item
           ,id_autorizacao_ob
           ,id_autorizacao_ob_item
           ,dt_confirmacao
           ,id_tipo_documento
           ,nr_documento
           ,id_regional
           ,id_reclassificacao_retencao
           ,id_origem
           ,id_despesa_tipo
           ,dt_vencimento
           ,nr_contrato
           ,cd_obra
           ,nr_op
           ,nr_banco_pagador
           ,nr_agencia_pagador
           ,nr_conta_pagador
           ,nr_fonte_siafem
           ,nr_emprenho
           ,nr_processo
           ,nr_nota_fiscal
           ,nr_nl_documento
           ,vr_documento
           ,nr_natureza_despesa
           ,cd_credor_organizacao
           ,nr_cnpj_cpf_ug_credor
           ,ds_referencia
           ,cd_orgao_assinatura
           ,nm_reduzido_credor
           ,fl_transmissao_transmitido_prodesp
           ,cd_transmissao_status_prodesp
           ,dt_transmissao_transmitido_prodesp
           ,ds_transmissao_mensagem_prodesp)
     VALUES
           (@id_confirmacao_pagamento
           ,@id_execucao_pd
           ,@id_programacao_desembolso_execucao_item
           ,@id_autorizacao_ob
           ,@id_autorizacao_ob_item
           ,@dt_confirmacao
           ,@id_tipo_documento
           ,@nr_documento
           ,@id_regional
           ,@id_reclassificacao_retencao
           ,@id_origem
           ,@id_despesa_tipo
           ,@dt_vencimento
           ,@nr_contrato
           ,@cd_obra
           ,@nr_op
           ,@nr_banco_pagador
           ,@nr_agencia_pagador
           ,@nr_conta_pagador
           ,@nr_fonte_siafem
           ,@nr_emprenho
           ,@nr_processo
           ,@nr_nota_fiscal
           ,@nr_nl_documento
           ,@vr_documento
           ,@nr_natureza_despesa
           ,@cd_credor_organizacao
           ,@nr_cnpj_cpf_ug_credor
           ,@ds_referencia
           ,@cd_orgao_assinatura
           ,@nm_reduzido_credor
           ,@fl_transmissao_transmitido_prodesp
           ,@cd_transmissao_status_prodesp
           ,@dt_transmissao_transmitido_prodesp
           ,@ds_transmissao_mensagem_prodesp)
end
COMMIT  
SELECT SCOPE_IDENTITY();
GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_DE_OB_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_DE_OB_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_EXCLUIR]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [dbSIDS];


GO
ALTER TABLE [contaunica].[tb_autorizacao_ob] WITH CHECK CHECK CONSTRAINT [FK_tb_autorizacao_ob_tb_confirmacao_pagamento];

ALTER TABLE [contaunica].[tb_programacao_desembolso_execucao_item] WITH CHECK CHECK CONSTRAINT [FK_tb_programacao_desembolso_execucao_item_tb_programacao_desembolso_execucao];

ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH CHECK CHECK CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_confirmacao_pagamento];

ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH CHECK CHECK CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_origem];

ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH CHECK CHECK CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_reclassificacao_retencao];

ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH CHECK CHECK CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo];

ALTER TABLE [contaunica].[tb_arquivo_remessa] WITH CHECK CHECK CONSTRAINT [FK_tb_arquivo_remessa_tb_regional];


GO
PRINT N'Update complete.';


GO
