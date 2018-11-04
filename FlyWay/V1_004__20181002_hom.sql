USE [dbSIDS];


GO

IF (OBJECT_ID('[pagamento].[FK_tb_despesa_tb_despesa_tipo]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_despesa_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_despesa] DROP CONSTRAINT [FK_tb_despesa_tb_despesa_tipo];
END

GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo];
END

GO

IF (OBJECT_ID('[contaunica].[FK_tb_arquivo_remessa_tb_regional]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [contaunica].[FK_tb_arquivo_remessa_tb_regional]...';
	ALTER TABLE [contaunica].[tb_arquivo_remessa] DROP CONSTRAINT [FK_tb_arquivo_remessa_tb_regional];
END

GO

IF (OBJECT_ID('[pagamento].[FK_tb_confirmacao_pagamento_tb_tipo_documento]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping [pagamento].[FK_tb_confirmacao_pagamento_tb_tipo_documento]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento] DROP CONSTRAINT [FK_tb_confirmacao_pagamento_tb_tipo_documento];
END

GO

PRINT N'Starting rebuilding table [contaunica].[tb_itens_obs_re]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_itens_obs_re] (
    [id_tb_itens_obs_re]    INT             IDENTITY (1, 1) NOT NULL,
    [cd_relob_re]           VARCHAR (11)    NULL,
    [nr_ob_re]              VARCHAR (11)    NULL,
    [fg_prioridade]         VARCHAR (1)     NULL,
    [cd_tipo_ob]            INT             NULL,
    [ds_nome_favorecido]    VARCHAR (50)    NULL,
    [ds_banco_favorecido]   VARCHAR (3)     NULL,
    [cd_agencia_favorecido] VARCHAR (5)     NULL,
    [ds_conta_favorecido]   VARCHAR (10)    NULL,
    [vl_ob]                 DECIMAL (15, 2) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_itens_obs_re1] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_re] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_itens_obs_re])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_itens_obs_re] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_itens_obs_re] ([id_tb_itens_obs_re], [fg_prioridade], [ds_nome_favorecido], [ds_banco_favorecido], [vl_ob])
        SELECT   [id_tb_itens_obs_re],
                 [fg_prioridade],
                 [ds_nome_favorecido],
                 [ds_banco_favorecido],
                 [vl_ob]
        FROM     [contaunica].[tb_itens_obs_re]
        ORDER BY [id_tb_itens_obs_re] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_itens_obs_re] OFF;
    END

DROP TABLE [contaunica].[tb_itens_obs_re];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_itens_obs_re]', N'tb_itens_obs_re';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_itens_obs_re1]', N'PK_tb_itens_obs_re', N'OBJECT';



GO

PRINT N'Starting rebuilding table [contaunica].[tb_itens_obs_rt]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_itens_obs_rt] (
    [id_tb_itens_obs_rt]            INT             IDENTITY (1, 1) NOT NULL,
    [cd_relob_rt]                   VARCHAR (11)    NULL,
    [nr_ob_rt]                      VARCHAR (11)    NULL,
    [cd_conta_bancaria_emitente]    VARCHAR (9)     NULL,
    [cd_unidade_gestora_favorecida] VARCHAR (6)     NULL,
    [cd_gestao_favorecida]          VARCHAR (5)     NULL,
    [ds_mnemonico_ug_favorecida]    VARCHAR (15)    NULL,
    [ds_banco_favorecido]           VARCHAR (3)     NULL,
    [cd_agencia_favorecido]         VARCHAR (5)     NULL,
    [ds_conta_favorecido]           VARCHAR (10)    NULL,
    [vl_ob]                         DECIMAL (15, 2) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_itens_obs_rt1] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_rt] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_itens_obs_rt])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_itens_obs_rt] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_itens_obs_rt] ([id_tb_itens_obs_rt], [cd_conta_bancaria_emitente], [ds_mnemonico_ug_favorecida], [ds_banco_favorecido], [vl_ob])
        SELECT   [id_tb_itens_obs_rt],
                 [cd_conta_bancaria_emitente],
                 [ds_mnemonico_ug_favorecida],
                 [ds_banco_favorecido],
                 [vl_ob]
        FROM     [contaunica].[tb_itens_obs_rt]
        ORDER BY [id_tb_itens_obs_rt] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_itens_obs_rt] OFF;
    END

DROP TABLE [contaunica].[tb_itens_obs_rt];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_itens_obs_rt]', N'tb_itens_obs_rt';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_itens_obs_rt1]', N'PK_tb_itens_obs_rt', N'OBJECT';




GO
PRINT N'Starting rebuilding table [movimentacao].[tb_cancelamento_movimentacao]...';


GO

CREATE TABLE [movimentacao].[tmp_ms_xx_tb_cancelamento_movimentacao] (
    [id_cancelamento_movimentacao]                              INT             IDENTITY (1, 1) NOT NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [tb_fonte_id_fonte]                                         INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [nr_nota_cancelamento]                                      VARCHAR (15)    NULL,
    [nr_siafem]                                                 NCHAR (15)      NULL,
    [valor]                                                     DECIMAL (18, 2) NULL,
    [cd_unidade_gestora]                                        VARCHAR (10)    NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)    NULL,
    [evento]                                                    VARCHAR (10)    NULL,
    [nr_categoria_gasto]                                        VARCHAR (15)    NULL,
    [eventoNC]                                                  VARCHAR (10)    NULL,
    [ds_observacao]                                             VARCHAR (77)    NULL,
    [ds_observacao2]                                            VARCHAR (77)    NULL,
    [ds_observacao3]                                            VARCHAR (77)    NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)        NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140)   NULL,
    [fg_transmitido_siafem]                                     CHAR (1)        NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140)   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_cancelamento_movimentacao1] PRIMARY KEY CLUSTERED ([id_cancelamento_movimentacao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [movimentacao].[tb_cancelamento_movimentacao])
    BEGIN
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_cancelamento_movimentacao] ON;
        INSERT INTO [movimentacao].[tmp_ms_xx_tb_cancelamento_movimentacao] ([id_cancelamento_movimentacao], [tb_fonte_id_fonte], [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria], [nr_agrupamento], [nr_seq], [nr_nota_cancelamento], [cd_unidade_gestora], [nr_categoria_gasto], [ds_observacao], [fg_transmitido_prodesp], [ds_msgRetornoProdesp], [fg_transmitido_siafem], [ds_msgRetornoSiafem], [valor], [ds_observacao2], [ds_observacao3], [eventoNC], [cd_gestao_favorecido])
        SELECT   [id_cancelamento_movimentacao],
                 [tb_fonte_id_fonte],
                 [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria],
                 [nr_agrupamento],
                 [nr_seq],
                 [nr_nota_cancelamento],
                 [cd_unidade_gestora],
                 [nr_categoria_gasto],
                 [ds_observacao],
                 [fg_transmitido_prodesp],
                 [ds_msgRetornoProdesp],
                 [fg_transmitido_siafem],
                 [ds_msgRetornoSiafem],
                 [valor],
                 [ds_observacao2],
                 [ds_observacao3],
                 [eventoNC],
                 [cd_gestao_favorecido]
        FROM     [movimentacao].[tb_cancelamento_movimentacao]
        ORDER BY [id_cancelamento_movimentacao] ASC;
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_cancelamento_movimentacao] OFF;
    END

DROP TABLE [movimentacao].[tb_cancelamento_movimentacao];

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_tb_cancelamento_movimentacao]', N'tb_cancelamento_movimentacao';

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_constraint_PK_tb_cancelamento_movimentacao1]', N'PK_tb_cancelamento_movimentacao', N'OBJECT';




GO
PRINT N'Starting rebuilding table [movimentacao].[tb_credito_movimentacao]...';


GO

CREATE TABLE [movimentacao].[tmp_ms_xx_tb_credito_movimentacao] (
    [id_nota_credito]                                           INT             IDENTITY (1, 1) NOT NULL,
    [tb_programa_id_programa]                                   INT             NULL,
    [tb_fonte_id_fonte]                                         INT             NULL,
    [tb_estrutura_id_estrutura]                                 INT             NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [cd_candis]                                                 CHAR (1)        NULL,
    [nr_nota_credito]                                           VARCHAR (15)    NULL,
    [nr_siafem]                                                 VARCHAR (15)    NULL,
    [vr_credito]                                                DECIMAL (18, 2) NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (15)    NULL,
    [cd_uo]                                                     VARCHAR (10)    NULL,
    [plano_interno]                                             VARCHAR (10)    NULL,
    [eventoNC]                                                  VARCHAR (10)    NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)    NULL,
    [cd_ugo]                                                    VARCHAR (10)    NULL,
    [fonte_recurso]                                             VARCHAR (10)    NULL,
    [ds_observacao]                                             VARCHAR (77)    NULL,
    [ds_observacao2]                                            VARCHAR (77)    NULL,
    [ds_observacao3]                                            VARCHAR (77)    NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)        NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140)   NULL,
    [fg_transmitido_siafem]                                     CHAR (1)        NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140)   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_credito_movimentacao1] PRIMARY KEY CLUSTERED ([id_nota_credito] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [movimentacao].[tb_credito_movimentacao])
    BEGIN
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_credito_movimentacao] ON;
        INSERT INTO [movimentacao].[tmp_ms_xx_tb_credito_movimentacao] ([id_nota_credito], [tb_programa_id_programa], [tb_fonte_id_fonte], [tb_estrutura_id_estrutura], [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria], [nr_agrupamento], [nr_seq], [nr_nota_credito], [cd_unidade_gestora_favorecido], [cd_uo], [plano_interno], [vr_credito], [ds_observacao], [fg_transmitido_prodesp], [ds_msgRetornoProdesp], [fg_transmitido_siafem], [ds_msgRetornoSiafem], [ds_observacao2], [ds_observacao3], [eventoNC], [cd_gestao_favorecido], [cd_ugo])
        SELECT   [id_nota_credito],
                 [tb_programa_id_programa],
                 [tb_fonte_id_fonte],
                 [tb_estrutura_id_estrutura],
                 [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria],
                 [nr_agrupamento],
                 [nr_seq],
                 [nr_nota_credito],
                 [cd_unidade_gestora_favorecido],
                 [cd_uo],
                 [plano_interno],
                 [vr_credito],
                 [ds_observacao],
                 [fg_transmitido_prodesp],
                 [ds_msgRetornoProdesp],
                 [fg_transmitido_siafem],
                 [ds_msgRetornoSiafem],
                 [ds_observacao2],
                 [ds_observacao3],
                 [eventoNC],
                 [cd_gestao_favorecido],
                 [cd_ugo]
        FROM     [movimentacao].[tb_credito_movimentacao]
        ORDER BY [id_nota_credito] ASC;
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_credito_movimentacao] OFF;
    END

DROP TABLE [movimentacao].[tb_credito_movimentacao];

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_tb_credito_movimentacao]', N'tb_credito_movimentacao';

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_constraint_PK_tb_credito_movimentacao1]', N'PK_tb_credito_movimentacao', N'OBJECT';




GO
PRINT N'Starting rebuilding table [movimentacao].[tb_distribuicao_movimentacao]...';


GO

CREATE TABLE [movimentacao].[tmp_ms_xx_tb_distribuicao_movimentacao] (
    [id_distribuicao_movimentacao]                              INT             IDENTITY (1, 1) NOT NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [nr_siafem]                                                 VARCHAR (15)    NULL,
    [valor]                                                     DECIMAL (18, 2) NULL,
    [tb_fonte_id_fonte]                                         VARCHAR (10)    NULL,
    [nr_nota_distribuicao]                                      VARCHAR (15)    NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (10)    NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)    NULL,
    [evento]                                                    VARCHAR (10)    NULL,
    [nr_categoria_gasto]                                        VARCHAR (10)    NULL,
    [eventoNC]                                                  VARCHAR (10)    NULL,
    [ds_observacao]                                             VARCHAR (77)    NULL,
    [ds_observacao2]                                            VARCHAR (77)    NULL,
    [ds_observacao3]                                            VARCHAR (77)    NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)        NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140)   NULL,
    [fg_transmitido_siafem]                                     CHAR (1)        NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140)   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_distribuicao_movimentacao1] PRIMARY KEY CLUSTERED ([id_distribuicao_movimentacao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [movimentacao].[tb_distribuicao_movimentacao])
    BEGIN
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_distribuicao_movimentacao] ON;
        INSERT INTO [movimentacao].[tmp_ms_xx_tb_distribuicao_movimentacao] ([id_distribuicao_movimentacao], [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria], [nr_agrupamento], [nr_seq], [tb_fonte_id_fonte], [nr_nota_distribuicao], [cd_unidade_gestora_favorecido], [nr_categoria_gasto], [ds_observacao], [fg_transmitido_prodesp], [ds_msgRetornoProdesp], [fg_transmitido_siafem], [ds_msgRetornoSiafem], [valor], [ds_observacao2], [ds_observacao3], [eventoNC], [cd_gestao_favorecido])
        SELECT   [id_distribuicao_movimentacao],
                 [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria],
                 [nr_agrupamento],
                 [nr_seq],
                 [tb_fonte_id_fonte],
                 [nr_nota_distribuicao],
                 [cd_unidade_gestora_favorecido],
                 [nr_categoria_gasto],
                 [ds_observacao],
                 [fg_transmitido_prodesp],
                 [ds_msgRetornoProdesp],
                 [fg_transmitido_siafem],
                 [ds_msgRetornoSiafem],
                 [valor],
                 [ds_observacao2],
                 [ds_observacao3],
                 [eventoNC],
                 [cd_gestao_favorecido]
        FROM     [movimentacao].[tb_distribuicao_movimentacao]
        ORDER BY [id_distribuicao_movimentacao] ASC;
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_distribuicao_movimentacao] OFF;
    END

DROP TABLE [movimentacao].[tb_distribuicao_movimentacao];

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_tb_distribuicao_movimentacao]', N'tb_distribuicao_movimentacao';

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_constraint_PK_tb_distribuicao_movimentacao1]', N'PK_tb_distribuicao_movimentacao', N'OBJECT';



GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'movimentacao' AND TABLE_NAME='tb_movimentacao_orcamentaria')
BEGIN
	PRINT N'Altering [movimentacao].[tb_movimentacao_orcamentaria]...';
	ALTER TABLE [movimentacao].[tb_movimentacao_orcamentaria] ALTER COLUMN [tb_fonte_id_fonte] INT NULL;
END

GO

PRINT N'Starting rebuilding table [movimentacao].[tb_movimentacao_orcamentaria_mes]...';


GO

CREATE TABLE [movimentacao].[tmp_ms_xx_tb_movimentacao_orcamentaria_mes] (
    [id_mes]                                                    INT             IDENTITY (1, 1) NOT NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT             NULL,
    [tb_reducao_suplementacao_id_reducao_suplementacao]         INT             NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT             NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [ds_mes]                                                    VARCHAR (9)     NULL,
    [vr_mes]                                                    DECIMAL (18, 2) NULL,
    [cd_unidade_gestora]                                        VARCHAR (10)    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_movimentacao_orcamentaria_mes1] PRIMARY KEY CLUSTERED ([id_mes] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [movimentacao].[tb_movimentacao_orcamentaria_mes])
    BEGIN
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_movimentacao_orcamentaria_mes] ON;
        INSERT INTO [movimentacao].[tmp_ms_xx_tb_movimentacao_orcamentaria_mes] ([id_mes], [tb_distribuicao_movimentacao_id_distribuicao_movimentacao], [tb_reducao_suplementacao_id_reducao_suplementacao], [tb_cancelamento_movimentacao_id_cancelamento_movimentacao], [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria], [nr_agrupamento], [nr_seq], [ds_mes], [vr_mes], [cd_unidade_gestora])
        SELECT   [id_mes],
                 [tb_distribuicao_movimentacao_id_distribuicao_movimentacao],
                 [tb_reducao_suplementacao_id_reducao_suplementacao],
                 [tb_cancelamento_movimentacao_id_cancelamento_movimentacao],
                 [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria],
                 [nr_agrupamento],
                 [nr_seq],
                 [ds_mes],
                 CAST ([vr_mes] AS DECIMAL (18, 2)),
                 [cd_unidade_gestora]
        FROM     [movimentacao].[tb_movimentacao_orcamentaria_mes]
        ORDER BY [id_mes] ASC;
        SET IDENTITY_INSERT [movimentacao].[tmp_ms_xx_tb_movimentacao_orcamentaria_mes] OFF;
    END

DROP TABLE [movimentacao].[tb_movimentacao_orcamentaria_mes];

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_tb_movimentacao_orcamentaria_mes]', N'tb_movimentacao_orcamentaria_mes';

EXECUTE sp_rename N'[movimentacao].[tmp_ms_xx_constraint_PK_tb_movimentacao_orcamentaria_mes1]', N'PK_tb_movimentacao_orcamentaria_mes', N'OBJECT';



GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'movimentacao' AND TABLE_NAME='tb_reducao_suplementacao')
BEGIN
	PRINT N'Altering [movimentacao].[tb_reducao_suplementacao]...';
	ALTER TABLE [movimentacao].[tb_reducao_suplementacao] ALTER COLUMN [valor] DECIMAL (18, 2) NULL;
END

GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_confirmacao_pagamento_item')
BEGIN
	PRINT N'Altering [pagamento].[tb_confirmacao_pagamento_item]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] ALTER COLUMN [nr_documento] VARCHAR (30) NULL;

	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item]
		ADD [nr_empenhoSiafem]            VARCHAR (11)   NULL,
			[nr_banco_favorecido]         VARCHAR (10)   NULL,
			[nr_agencia_favorecido]       VARCHAR (10)   NULL,
			[nr_conta_favorecido]         VARCHAR (10)   NULL,
			[vr_desdobramento]            DECIMAL (8, 2) NULL,
			[cd_credor_organizacao_docto] INT            NULL,
			[nr_cnpj_cpf_ug_credor_docto] VARCHAR (14)   NULL,
			[dt_realizacao]               DATETIME       NULL,
			[nm_reduzido_credor_docto]    VARCHAR (14)   NULL;
END

GO
PRINT N'Starting rebuilding table [pagamento].[tb_despesa_tipo]...';


GO

CREATE TABLE [pagamento].[tmp_ms_xx_tb_despesa_tipo] (
    [id_despesa_tipo] INT          NOT NULL,
    [cd_despesa_tipo] INT          NULL,
    [ds_despesa_tipo] VARCHAR (50) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_despesa_tipo1] PRIMARY KEY CLUSTERED ([id_despesa_tipo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [pagamento].[tb_despesa_tipo])
    BEGIN
        INSERT INTO [pagamento].[tmp_ms_xx_tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo])
        SELECT   [id_despesa_tipo],
                 [cd_despesa_tipo],
                 [ds_despesa_tipo]
        FROM     [pagamento].[tb_despesa_tipo]
        ORDER BY [id_despesa_tipo] ASC;
    END

DROP TABLE [pagamento].[tb_despesa_tipo];

EXECUTE sp_rename N'[pagamento].[tmp_ms_xx_tb_despesa_tipo]', N'tb_despesa_tipo';

EXECUTE sp_rename N'[pagamento].[tmp_ms_xx_constraint_PK_tb_despesa_tipo1]', N'PK_tb_despesa_tipo', N'OBJECT';



GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_evento')
BEGIN
	PRINT N'Altering [pagamento].[tb_evento]...';
	ALTER TABLE [pagamento].[tb_evento] ALTER COLUMN [ds_entrada_saida] CHAR (10) NULL;

	ALTER TABLE [pagamento].[tb_evento] ALTER COLUMN [nr_classificacao] VARCHAR (50) NULL;

	ALTER TABLE [pagamento].[tb_evento] ALTER COLUMN [nr_evento] VARCHAR (50) NULL;

	ALTER TABLE [pagamento].[tb_evento] ALTER COLUMN [nr_fonte] VARCHAR (50) NULL;
END

GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_nl_parametrizacao')
BEGIN
	PRINT N'Altering [pagamento].[tb_nl_parametrizacao]...';
	ALTER TABLE [pagamento].[tb_nl_parametrizacao] ALTER COLUMN [ds_observacao] VARCHAR (228) NULL;

	ALTER TABLE [pagamento].[tb_nl_parametrizacao]
		ADD [id_parametrizacao_forma_gerar_nl] INT DEFAULT ((0)) NOT NULL;
END


GO
PRINT N'Starting rebuilding table [contaunica].[tb_arquivo]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_arquivo] (
    [id_arquivo] INT IDENTITY (1, 1) NOT NULL,
    [ds_arquivo] INT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_arquivo1] PRIMARY KEY CLUSTERED ([id_arquivo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_arquivo])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_arquivo] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_arquivo] ([id_arquivo], [ds_arquivo])
        SELECT   [id_arquivo],
                 [ds_arquivo]
        FROM     [contaunica].[tb_arquivo]
        ORDER BY [id_arquivo] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_arquivo] OFF;
    END

DROP TABLE [contaunica].[tb_arquivo];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_arquivo]', N'tb_arquivo';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_arquivo1]', N'PK_tb_arquivo', N'OBJECT';



GO
PRINT N'Starting rebuilding table [contaunica].[tb_arquivo_remessa]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_arquivo_remessa] (
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
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_arquivo_remessa1] PRIMARY KEY CLUSTERED ([id_arquivo_remessa] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_arquivo_remessa])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_arquivo_remessa] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_arquivo_remessa] ([id_arquivo_remessa], [tb_arquivo_id_arquivo], [nr_geracao_arquivo], [dt_preparacao_pagamento], [dt_pagamento], [cd_assinatura], [cd_grupo_assinatura], [cd_orgao_assinatura], [nm_assinatura], [ds_cargo], [cd_contra_assinatura], [cd_grupo_contra_assinatura], [cd_orgao_contra_assinatura], [nm_contra_assinatura], [ds_cargo_contra_assinatura], [cd_conta], [ds_banco], [ds_agencia], [ds_conta], [qt_ordem_pagamento_arquivo], [qt_deposito_arquivo], [vr_total_pago], [qt_doc_ted_arquivo], [dt_cadastro], [fg_trasmitido_prodesp], [dt_trasmitido_prodesp], [fg_arquivo_cancelado], [id_regional], [bl_cadastro_completo], [ds_msg_retorno], [bl_transmitir_prodesp], [bl_transmitido_prodesp])
        SELECT   [id_arquivo_remessa],
                 [tb_arquivo_id_arquivo],
                 [nr_geracao_arquivo],
                 [dt_preparacao_pagamento],
                 [dt_pagamento],
                 [cd_assinatura],
                 [cd_grupo_assinatura],
                 [cd_orgao_assinatura],
                 [nm_assinatura],
                 [ds_cargo],
                 [cd_contra_assinatura],
                 [cd_grupo_contra_assinatura],
                 [cd_orgao_contra_assinatura],
                 [nm_contra_assinatura],
                 [ds_cargo_contra_assinatura],
                 [cd_conta],
                 [ds_banco],
                 [ds_agencia],
                 [ds_conta],
                 [qt_ordem_pagamento_arquivo],
                 [qt_deposito_arquivo],
                 [vr_total_pago],
                 [qt_doc_ted_arquivo],
                 [dt_cadastro],
                 [fg_trasmitido_prodesp],
                 [dt_trasmitido_prodesp],
                 [fg_arquivo_cancelado],
                 [id_regional],
                 [bl_cadastro_completo],
                 [ds_msg_retorno],
                 [bl_transmitir_prodesp],
                 [bl_transmitido_prodesp]
        FROM     [contaunica].[tb_arquivo_remessa]
        ORDER BY [id_arquivo_remessa] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_arquivo_remessa] OFF;
    END

DROP TABLE [contaunica].[tb_arquivo_remessa];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_arquivo_remessa]', N'tb_arquivo_remessa';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_arquivo_remessa1]', N'PK_tb_arquivo_remessa', N'OBJECT';



GO
PRINT N'Creating [contaunica].[tb_impressao_relacao_re_rt]...';


GO
CREATE TABLE [contaunica].[tb_impressao_relacao_re_rt] (
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


GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_parametrizacao_forma_gerar_nl')
BEGIN
	PRINT N'Creating [pagamento].[tb_parametrizacao_forma_gerar_nl]...';
	CREATE TABLE [pagamento].[tb_parametrizacao_forma_gerar_nl] (
		[id_parametrizacao_forma_gerar_nl] INT          NOT NULL,
		[ds_gerar_nl]                      VARCHAR (50) NULL,
		CONSTRAINT [PK_tb_parametrizacao_forma_gerar_nl] PRIMARY KEY CLUSTERED ([id_parametrizacao_forma_gerar_nl] ASC)
	);
END

GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_despesa')
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_despesa_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_despesa] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_despesa_tb_despesa_tipo] FOREIGN KEY ([id_despesa_tipo]) REFERENCES [pagamento].[tb_despesa_tipo] ([id_despesa_tipo]);
END


GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_confirmacao_pagamento_item')
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_confirmacao_pagamento_item_tb_despesa_tipo]...';
	ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo] FOREIGN KEY ([id_despesa_tipo]) REFERENCES [pagamento].[tb_despesa_tipo] ([id_despesa_tipo]);
END

GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'contaunica' AND TABLE_NAME='tb_arquivo_remessa')
BEGIN
	PRINT N'Creating [contaunica].[FK_tb_arquivo_remessa_tb_regional]...';
	ALTER TABLE [contaunica].[tb_arquivo_remessa] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_arquivo_remessa_tb_regional] FOREIGN KEY ([id_regional]) REFERENCES [seguranca].[tb_regional] ([id_regional]);
END

GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pagamento' AND TABLE_NAME='tb_nl_parametrizacao')
BEGIN
	PRINT N'Creating [pagamento].[FK_tb_nl_parametrizacao_tb_parametrizacao_forma_gerar_nl]...';
	ALTER TABLE [pagamento].[tb_nl_parametrizacao] WITH NOCHECK
		ADD CONSTRAINT [FK_tb_nl_parametrizacao_tb_parametrizacao_forma_gerar_nl] FOREIGN KEY ([id_parametrizacao_forma_gerar_nl]) REFERENCES [pagamento].[tb_parametrizacao_forma_gerar_nl] ([id_parametrizacao_forma_gerar_nl]);
END

GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CANCEL_CONSULTAR]...';
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
	  ,mo.cd_gestao_emitente as IdGestaoEmitente
	  
  FROM [movimentacao].[tb_cancelamento_movimentacao]   c
  JOIN [movimentacao].[tb_movimentacao_orcamentaria] mo ON mo.id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
 

  WHERE 
  ( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  ( @nr_agrupamento is null or c.nr_agrupamento = @nr_agrupamento )    
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_cancelamento_movimentacao,c.nr_seq



  END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR]      
	@id_cancelamento_movimentacao int = NULL,    
	@tb_fonte_id_fonte int = NULL,    
	@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL,    
	@nr_agrupamento int = NULL,    
	@nr_seq int = NULL,    
	@nr_nota_cancelamento varchar(10) = NULL,    
	@cd_unidade_gestora varchar(15) = NULL,   
	@cd_gestao_favorecido varchar(10) = NULL,  
	@nr_categoria_gasto varchar(15) = NULL,    
	@ds_observacao varchar(231) = NULL,    
	@fg_transmitido_prodesp char(1) = NULL,    
	@fg_transmitido_siafem char(1) = NULL    
      
AS        
BEGIN        
 SET NOCOUNT ON;      

SELECT [id_cancelamento_movimentacao]    
		,[tb_fonte_id_fonte]    
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]    
		,[nr_agrupamento]    
		,[nr_seq]    
		,[nr_nota_cancelamento]
		,[nr_siafem]
		,[valor]   
		,[cd_unidade_gestora]
		,[evento]    
		,[nr_categoria_gasto]
		,[eventoNC]   
		,[ds_observacao]    
		,[ds_observacao2]   
		,[ds_observacao3]   
		,[fg_transmitido_prodesp]    
		,[ds_msgRetornoProdesp]    
		,[fg_transmitido_siafem]    
		,[ds_msgRetornoSiafem]    
		,[cd_gestao_favorecido]
  FROM [movimentacao].[tb_cancelamento_movimentacao]    
    
  WHERE     
	( nullif( @id_cancelamento_movimentacao, 0 ) is null or id_cancelamento_movimentacao = @id_cancelamento_movimentacao )   and     
	( nullif(  @tb_fonte_id_fonte, 0) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and     
	( nullif(  @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and
	( nullif( @nr_agrupamento, 0 ) is null or nr_agrupamento = @nr_agrupamento )   and     
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and     
	( @nr_nota_cancelamento is null or nr_nota_cancelamento = @nr_nota_cancelamento  ) and    
	( @cd_unidade_gestora is null or cd_unidade_gestora= @cd_unidade_gestora  ) and    
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and  
	( @nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) and    
	( @ds_observacao is null or ds_observacao= @ds_observacao  ) and    
	( @fg_transmitido_prodesp is null or fg_transmitido_prodesp= @fg_transmitido_prodesp  ) and    
	( @fg_transmitido_siafem is null or fg_transmitido_siafem = @fg_transmitido_siafem  )     
    
  ORDER BY id_cancelamento_movimentacao,nr_seq

  END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR]...';
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
   ,o.cd_gestao_emitente as IdGestaoEmitente  
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CANCELAMENTO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria    
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_SALVAR]     
	@id_cancelamento_movimentacao int = NULL 
	, @tb_fonte_id_fonte varchar(10) = NULL
	, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL
	, @nr_agrupamento int = NULL
	, @nr_seq int = NULL
	, @nr_nota_cancelamento varchar(15) = NULL
	, @cd_unidade_gestora varchar(10) = NULL
	, @cd_gestao_favorecido varchar(10) = NULL
	, @evento varchar(10) = NULL
	, @nr_categoria_gasto varchar(10) = NULL
	, @eventoNC varchar(10) = NULL 
	, @ds_observacao varchar(77) = NULL
	, @ds_observacao2 varchar(77) = NULL
	, @ds_observacao3 varchar(77) = NULL
	, @fg_transmitido_prodesp char(1) = NULL
	, @ds_msgRetornoProdesp varchar(140) = NULL
	, @fg_transmitido_siafem char(1) = NULL
	, @ds_msgRetornoSiafem varchar(140) = NULL
	, @valor decimal(18,2) = null
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
		,[evento] = @evento
		,[nr_categoria_gasto] = @nr_categoria_gasto
		,[eventoNC] = @eventoNC
		,[ds_observacao] = @ds_observacao
		,[ds_observacao2] = @ds_observacao2
		,[ds_observacao3] = @ds_observacao3
		,[fg_transmitido_prodesp] = @fg_transmitido_prodesp
		,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp
		,[fg_transmitido_siafem] = @fg_transmitido_siafem
		,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem
		,[valor] = @valor

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
	,[evento]
	,[nr_categoria_gasto]
	,[eventoNC]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
	,[fg_transmitido_prodesp]
	,[ds_msgRetornoProdesp]
	,[fg_transmitido_siafem]
	,[ds_msgRetornoSiafem]
	,[valor])
VALUES    
	(@tb_fonte_id_fonte
	,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
	,@nr_agrupamento
	,@nr_seq
	,@nr_nota_cancelamento
	,@cd_unidade_gestora
	,@cd_gestao_favorecido
	,@evento
	,@nr_categoria_gasto
	,@eventoNC
	,@ds_observacao
	,@ds_observacao2
	,@ds_observacao3
	,'N'
	,@ds_msgRetornoProdesp
	,'N'
	,@ds_msgRetornoSiafem
	,@valor)
    
  select scope_identity();    
    
 end    
    
end
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID]...';
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
	        IdMovimentacao int,
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
		    IdMovimentacao,
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
			Where 1 = 1 and
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
				( @dt_cadastramento_ate is null or o.[dt_cadastro] < Dateadd(day, 1, @dt_cadastramento_ate) )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 

				)

END




	 if(@Teste is null or ( @Teste = 2 or @Teste = 7))
	 begin

		--NOTAS CREDITOS
           Insert into #auxiliar (
		   IdMovimentacao,
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
			'Nota de Crédito',
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
				( @dt_cadastramento_ate is null or o.[dt_cadastro] < Dateadd(day, 1, @dt_cadastramento_ate) )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
			)



			END



	 if(@Teste is null or (@Teste = 2 or @Teste = 3 or @Teste = 8))
	 begin
           --DISTRIBUICAO
           Insert into #auxiliar (
		   IdMovimentacao,
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
			'Distribuição',
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
				( @dt_cadastramento_ate is null or o.[dt_cadastro] < Dateadd(day, 1, @dt_cadastramento_ate) )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
			)

			END




	 if(@Teste is null or (@Teste = 1 or @Teste = 4 or @Teste = 7))
	 begin
			 --REDUCAO
             Insert into #auxiliar (
			 IdMovimentacao,
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
			'Redução',
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
				( @dt_cadastramento_ate is null or o.[dt_cadastro] < Dateadd(day, 1, @dt_cadastramento_ate) )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) and c.flag_red_sup = 'R'
			)

			END

			


  if(@Teste is null or ( @Teste = 2 or @Teste = 3 or @Teste = 5))
	 begin
			 --SUPLEMENTACAO
Insert into #auxiliar (
               IdMovimentacao,
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
			'Suplementação',
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
				( @dt_cadastramento_ate is null or o.[dt_cadastro] < Dateadd(day, 1, @dt_cadastramento_ate) )   and

				(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) and  
				(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) and c.flag_red_sup = 'S'
			)
END







			select 
			IdMovimentacao,
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CREDITO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_CONSULTAR] 
		@id_nota_credito int = NULL
		,@tb_programa_id_programa int = NULL
		,@tb_fonte_id_fonte int = NULL
		,@tb_estrutura_id_estrutura int = NULL
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL
		,@nr_agrupamento int = NULL
		,@nr_seq int = NULL
		,@cd_candis char(1) = null
		,@nr_nota_credito VARCHAR(15 )= NULL
		,@cd_unidade_gestora_favorecido varchar(10) = NULL
		,@cd_gestao_favorecido varchar(10) = NULL 
		,@cd_uo VARCHAR(10) = NULL
		,@plano_interno VARCHAR(10) = NULL
AS    
BEGIN    
 SET NOCOUNT ON;  

SELECT [id_nota_credito]
	,[tb_programa_id_programa]
	,[tb_fonte_id_fonte]
	,[tb_estrutura_id_estrutura]
	,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[cd_candis]
	,[nr_nota_credito]
	,[nr_siafem]
	,[vr_credito]   
	,[cd_unidade_gestora_favorecido]
	,[cd_uo]
	,[cd_ugo]
	,[cd_gestao_favorecido]
	,[plano_interno] 
	,[vr_credito]
	,[fonte_recurso]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
	,[eventoNC]
  FROM [movimentacao].[tb_credito_movimentacao]

  WHERE 
	( nullif( @id_nota_credito, 0 ) is null or id_nota_credito = @id_nota_credito )   and 
	( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa )   and 
	( nullif(@tb_fonte_id_fonte, 0) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
	( nullif( @tb_estrutura_id_estrutura, 0 ) is null or tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura )   and 
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
	( nr_agrupamento = @nr_agrupamento )   and 
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
	( @cd_candis is null or cd_candis = @cd_candis )   and 
	( @nr_nota_credito is null or nr_nota_credito = @nr_nota_credito  ) and
	( @cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido  ) and
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	( @cd_uo is null or cd_uo = @cd_uo  ) and
	( @plano_interno is null or plano_interno = @plano_interno  )


  ORDER BY id_nota_credito


  END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_CREDITO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================          
-- Author:  Alessandro de Santanao      
-- Create date: 31/07/2018      
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria      
-- ===================================================================        
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_CREDITO_SALVAR]       
		@id_nota_credito int = NULL      
		,@tb_programa_id_programa int = NULL      
		,@tb_fonte_id_fonte int = NULL      
		,@tb_estrutura_id_estrutura int = NULL      
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL      
		,@nr_agrupamento int = NULL      
		,@nr_seq int = NULL      
		,@cd_candis char(1) = NULL
		,@nr_nota_credito varchar(15) = NULL      
		,@cd_unidade_gestora_favorecido varchar(15) = NULL     
		,@cd_gestao_favorecido varchar(10) = NULL    
		,@cd_uo varchar(10) = NULL      
		,@cd_ugo varchar(10) = NULL    
		,@plano_interno varchar(10) = NULL      
		,@vr_credito decimal(18,2) = NULL      
		,@ds_observacao varchar(77) = NULL    
		,@ds_observacao2 varchar(77) = NULL      
		,@ds_observacao3 varchar(77) = NULL    
		,@fg_transmitido_prodesp char(1) = NULL      
		,@ds_msgRetornoProdesp varchar(140) = NULL     
		,@fg_transmitido_siafem char(1) = NULL      
		,@ds_msgRetornoSiafem varchar(140) = NULL      
		,@eventoNC varchar(10) = NULL 
		,@fonte_recurso varchar(10) = NULL   
      
      
AS      
BEGIN      
      
 SET NOCOUNT ON;      
      
 IF EXISTS (      
  SELECT 1       
  FROM [movimentacao].[tb_credito_movimentacao] (nolock)      
  WHERE id_nota_credito = @id_nota_credito      
 )      
 begin      
      
 UPDATE [movimentacao].[tb_credito_movimentacao]      
    SET 
		[tb_programa_id_programa] = @tb_programa_id_programa      
		,[tb_fonte_id_fonte] = @tb_fonte_id_fonte      
		,[tb_estrutura_id_estrutura] = @tb_estrutura_id_estrutura      
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria      
		,[nr_agrupamento] = @nr_agrupamento      
		,[nr_seq] = @nr_seq
		,[cd_candis] = @cd_candis
		,[nr_nota_credito] = @nr_nota_credito      
		,[cd_unidade_gestora_favorecido] = @cd_unidade_gestora_favorecido   
		,[cd_gestao_favorecido] = @cd_gestao_favorecido      
		,[cd_ugo] = @cd_uGo    
		,[cd_uo] = @cd_uo      
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
		,[fonte_recurso] = @fonte_recurso       
        where id_nota_credito = @id_nota_credito;      
      
  select @id_nota_credito;      
      
      
END      
ELSE      
BEGIN      
      
 INSERT INTO [movimentacao].[tb_credito_movimentacao]      
		([tb_programa_id_programa]      
		,[tb_fonte_id_fonte]      
		,[tb_estrutura_id_estrutura]      
		,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]      
		,[nr_agrupamento]      
		,[nr_seq]
		,[cd_candis]
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
		eventoNC,
		fonte_recurso)      
     VALUES      
		(@tb_programa_id_programa      
		,@tb_fonte_id_fonte      
		,@tb_estrutura_id_estrutura      
		,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria      
		,@nr_agrupamento      
		,@nr_seq
		,@cd_candis    
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
		,@eventoNC
		,@fonte_recurso)      
      
      
    
      
      
  select scope_identity();      
      
 end      
      
end
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_NC_CONSULTAR]...';
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
	,n.[tb_fonte_id_fonte] as IdFonte            
	,f.[cd_fonte] as Fonte        
	,n.[tb_estrutura_id_estrutura] as IdEstrutura        
	,n.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]  as IdMovimentacao        
	,n.[nr_agrupamento] as NrAgrupamento        
	,n.[nr_seq] as NrSequencia         
	,n.[nr_nota_credito] as NrNotaCredito   
	,n.[nr_siafem]
	,n.[vr_credito]     
	,n.[cd_unidade_gestora_favorecido]  as UnidadeGestoraFavorecida  
	,o.[cd_gestao_emitente] as IdGestaoEmitente  
	,n.[cd_uo] as Uo   
	,n.[cd_ugo] as Ugo      
	,n.[fonte_recurso] as FonteRecurso
	,n.[plano_interno] as plano_interno        
	,n.[vr_credito] as ValorCredito       
	,n.[ds_observacao] as ObservacaoNC  
	,n.[ds_observacao2] as ObservacaoNC2 
	,n.[ds_observacao3] as ObservacaoNC3      
	,o.cd_unidade_gestora_emitente as UnidadeGestoraEmitente    
	,n.eventoNC as EventoNC  
	,n.cd_gestao_favorecido as IdGestaoFavorecida  
	,n.[fg_transmitido_prodesp]   as StatusProdespItem  
	,n.[ds_msgRetornoProdesp]   as MensagemProdespItem  
	,n.[fg_transmitido_siafem]   as StatusSiafemItem  
	,n.[ds_msgRetornoSiafem]  as MensagemSiafemItem  
  FROM [movimentacao].[tb_credito_movimentacao] n        
	inner join movimentacao.tb_movimentacao_orcamentaria o on n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria    
	left join configuracao.tb_fonte f on f.id_fonte = n.tb_fonte_id_fonte
        
  WHERE         
  ( n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and         
  ( n.nr_agrupamento = @nr_agrupamento )           
        
  ORDER BY id_nota_credito,nr_seq         

        
  END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR]...';
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
	,[nr_siafem]
	,[valor]
	,[cd_unidade_gestora_favorecido]
	,[evento]
	,[nr_categoria_gasto]
	,[eventoNC]
	,[cd_gestao_favorecido]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
  FROM [movimentacao].[tb_distribuicao_movimentacao]
  WHERE 
	( nullif( @id_distribuicao_movimentacao, 0 ) is null or id_distribuicao_movimentacao = @id_distribuicao_movimentacao )   and 
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
	( nr_agrupamento = @nr_agrupamento )   and 
	( nullif( @nr_seq, 0 ) is null or nr_seq = @nr_seq )   and 
	( nullif( @tb_fonte_id_fonte, 0 ) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte )   and 
	( @nr_nota_distribuicao is null or nr_nota_distribuicao = @nr_nota_distribuicao  ) and
	( @cd_unidade_gestora_favorecido is null or cd_unidade_gestora_favorecido= @cd_unidade_gestora_favorecido  ) and
	( @cd_gestao_favorecido is null or cd_gestao_favorecido= @cd_gestao_favorecido  ) and 
	( @nr_categoria_gasto is null or nr_categoria_gasto= @nr_categoria_gasto  ) 

  ORDER BY id_distribuicao_movimentacao,nr_seq


END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================        
-- Author:  Alessandro de Santanao    
-- Create date: 31/07/2018    
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria    
-- ===================================================================      
ALTER PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR]     
	@id_distribuicao_movimentacao int = NULL,
	@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = NULL,
	@nr_agrupamento int = NULL,
	@nr_seq int = NULL,
	@tb_fonte_id_fonte varchar(10) = NULL,
	@nr_nota_distribuicao varchar(15) = NULL,
	@cd_unidade_gestora_favorecido varchar(10) = NULL,
	@cd_gestao_favorecido varchar(10) = NULL,
	@evento varchar(10) = NULL,
	@nr_categoria_gasto int = NULL,
	@eventoNC varchar(10) = NULL,
	@ds_observacao varchar(77) = NULL,
	@ds_observacao2 varchar(77) = NULL,
	@ds_observacao3 varchar(77) = NULL,
	@fg_transmitido_prodesp char(1) = NULL,
	@ds_msgRetornoProdesp varchar(140) = NULL,
	@fg_transmitido_siafem char(1) = NULL,
	@ds_msgRetornoSiafem varchar(140) = NULL,
	@valor decimal(18,2) = null    
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
		,[evento] = @evento
		,[nr_categoria_gasto] = @nr_categoria_gasto
		,[eventoNC] = @eventoNC
		,[ds_observacao] = @ds_observacao
		,[ds_observacao2] = @ds_observacao2
		,[ds_observacao3] = @ds_observacao3
		,[fg_transmitido_prodesp] = @fg_transmitido_prodesp
		,[ds_msgRetornoProdesp] = @ds_msgRetornoProdesp
		,[fg_transmitido_siafem] = @fg_transmitido_siafem
		,[ds_msgRetornoSiafem] = @ds_msgRetornoSiafem
		,[valor] = @valor
		          
    WHERE id_distribuicao_movimentacao = @id_distribuicao_movimentacao;    
    
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
	,[evento]
	,[nr_categoria_gasto]
	,[eventoNC]
	,[ds_observacao]
	,[ds_observacao2]
	,[ds_observacao3]
	,[fg_transmitido_prodesp]
	,[ds_msgRetornoProdesp]
	,[fg_transmitido_siafem]
	,[ds_msgRetornoSiafem]
	,[valor])
VALUES    
	(@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
	,@nr_agrupamento
	,@nr_seq
	,@tb_fonte_id_fonte
	,@nr_nota_distribuicao
	,@cd_unidade_gestora_favorecido
	,@cd_gestao_favorecido
	,@evento
	,@nr_categoria_gasto
	,@eventoNC
	,@ds_observacao
	,@ds_observacao2
	,@ds_observacao3
	,'N'
	,@ds_msgRetornoProdesp
	,'N'
	,@ds_msgRetornoSiafem
	,@valor)
    
  select scope_identity();    
    
 end    
    
end
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR]...';
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
      
      
      
      
SELECT 
	[id_movimentacao_orcamentaria]      
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
	,[tb_programa_id_programa]
	,[tb_fonte_id_fonte] 
	,[tb_estrutura_id_estrutura]
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR]...';
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
	, [tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
	, [tb_reducao_suplementacao_id_reducao_suplementacao]
	, [tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
	, [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	, [nr_agrupamento]
	, [nr_seq]
	, [ds_mes]
	, [vr_mes]
	, [cd_unidade_gestora]
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)                
  WHERE                 
	( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and                
	( tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao )   and          
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and         
	( nr_agrupamento = @nr_agrupamento )   and           
	( nr_seq = @nr_seq )                  
END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR]...';
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
	,@tb_distribuicao_movimentacao_id_distribuicao_movimentacao int = null
	,@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null
	,@nr_agrupamento int = null
	,@nr_seq int = null
	,@ds_mes varchar(9) = null
	,@vr_mes decimal = null
AS              
BEGIN              
 SET NOCOUNT ON;              
              
  SELECT              
	[id_mes]
	,[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]
	,[tb_reducao_suplementacao_id_reducao_suplementacao]
	,[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]
	,[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]
	,[nr_agrupamento]
	,[nr_seq]
	,[ds_mes]
	,[vr_mes]
	,[cd_unidade_gestora]
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock)              
  WHERE               
	( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and              
	( tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao )   and        
	( tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )    and       
	( nr_agrupamento = @nr_agrupamento )   and         
	( nr_seq = @nr_seq )                
END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_MES_CONSULTAR]...';
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
	, @tb_reducao_suplementacao_id_reducao_suplementacao int = null      
	, @tb_cancelamento_movimentacao_id_cancelamento_movimentacao int = null     
	, @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int = null       
	, @nr_agrupamento int = null    
	, @nr_seq int = null    
	, @ds_mes varchar(9) = null  
	, @vr_mes decimal = null
	, @red_sup char(1) = null
AS          
BEGIN          
 SET NOCOUNT ON;          
          
  SELECT          
   mom.[id_mes]          
  , mom.[tb_distribuicao_movimentacao_id_distribuicao_movimentacao]          
  , mom.[tb_reducao_suplementacao_id_reducao_suplementacao]        
  , mom.[tb_cancelamento_movimentacao_id_cancelamento_movimentacao]        
  , mom.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]        
  , mom.[nr_agrupamento]        
  , mom.[nr_seq]        
  , mom.[ds_mes]         
  , mom.[vr_mes]  
  , mom.[cd_unidade_gestora]        
  FROM  [movimentacao].[tb_movimentacao_orcamentaria_mes] (nolock) mom
  LEFT JOIN [movimentacao].[tb_reducao_suplementacao] (nolock) rs on rs.id_reducao_suplementacao = mom.tb_reducao_suplementacao_id_reducao_suplementacao
  WHERE           
	( nullif( @id_mes, 0 ) is null or id_mes = @id_mes ) and          
	( nullif( @tb_distribuicao_movimentacao_id_distribuicao_movimentacao, 0 ) is null or mom.tb_distribuicao_movimentacao_id_distribuicao_movimentacao = @tb_distribuicao_movimentacao_id_distribuicao_movimentacao ) and
	( nullif( [tb_reducao_suplementacao_id_reducao_suplementacao], 0 ) is null or mom.tb_reducao_suplementacao_id_reducao_suplementacao = [tb_reducao_suplementacao_id_reducao_suplementacao] ) and
	( nullif( @tb_cancelamento_movimentacao_id_cancelamento_movimentacao, 0 ) is null or mom.tb_cancelamento_movimentacao_id_cancelamento_movimentacao = @tb_cancelamento_movimentacao_id_cancelamento_movimentacao ) and
	( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0) is null or mom.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria ) and
	( nullif( @nr_agrupamento, 0) is null or mom.nr_agrupamento = @nr_agrupamento ) and
	( nullif( @nr_seq, 0) is null or mom.nr_seq = @nr_seq ) and
	( @red_sup is null or rs.flag_red_sup = @red_sup)
END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_MES_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_MES_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_MES_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria
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
           @vr_mes decimal(18,2) = null,
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_REDUCAO_CONSULTAR]...';
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
	  rs.id_reducao_suplementacao
	  ,rs.cd_destino_recurso
	  ,rs.nr_orgao
	  ,rs.nr_suplementacao_reducao
	  ,rs.valor as ValorTotal
	  ,rs.cd_gestao_favorecido
	  ,rs.cd_autorizado_assinatura
	  ,rs.cd_autorizado_grupo
	  ,rs.cd_autorizado_orgao
	  ,rs.ds_autorizado_cargo
	  ,rs.ds_autorizado_supra_folha
	  ,rs.nm_autorizado_assinatura
	  ,rs.cd_examinado_assinatura
	  ,rs.cd_examinado_grupo
	  ,rs.cd_examinado_orgao
	  ,rs.ds_examinado_cargo
	  ,rs.nm_examinado_assinatura
	  ,rs.cd_responsavel_assinatura
	  ,rs.cd_responsavel_grupo
	  ,rs.cd_responsavel_orgao
	  ,rs.ds_responsavel_cargo
	  ,rs.nm_responsavel_assinatura
	  ,rs.nr_seq
	  ,rs.flag_red_sup
  FROM  [movimentacao].[tb_reducao_suplementacao]  rs 
  WHERE 
  (  rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and 
  (  rs.nr_agrupamento = @nr_agrupamento )    and flag_red_sup = 'R'
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )    


  ORDER BY id_reducao_suplementacao,nr_seq



  END
GO
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR]...';
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
    
    
SELECT 
	[id_reducao_suplementacao]
	,[tb_credito_movimentacao_id_nota_credito]
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
	,[cd_unidade_gestora]
	,[cd_gestao_favorecido]
	,[valor]
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
	( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0) is null or  tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria ) 	and     
	( nullif( @nr_agrupamento, 0) is null or nr_agrupamento  = @nr_agrupamento )   and     
	( nullif( @nr_seq, 0) is null or nr_seq = @nr_seq )   and     
  
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================      
-- Author:  Alessandro de Santanao  
-- Create date: 31/07/2018  
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria  
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
			 @valor decimal(18,2) = NULL,  
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
PRINT N'Altering [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR]...';
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
      [id_reducao_suplementacao]
      ,[tb_credito_movimentacao_id_nota_credito]
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
	  ,[cd_unidade_gestora]
	  ,[cd_gestao_favorecido]
	  ,[TotalQ1]
	  ,[TotalQ2]
	  ,[TotalQ3]
	  ,[TotalQ4]
	  ,[valor]  
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
PRINT N'Altering [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
 -- ==============================================================
-- Author:  Jose Braz
-- Create date: 30/01/2018
-- Description: Procedure para Consultar pagamento.tb_confirmacao_pagamento_item
-- ==============================================================
ALTER PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_CONSULTAR]
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
@nr_conta_favorecido varchar(10) = NULL
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
nr_conta_favorecido
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
( NULLIF( @nr_banco_pagador, '' ) IS NULL OR nr_conta_favorecido LIKE '%' +  @nr_conta_favorecido + '%' )
END

-----------------------------------------------------------------------------------------
GO
PRINT N'Altering [dbo].[sp_confirmacao_pagamento_incluir]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.sp_confirmacao_pagamento_incluir'))
   EXEC('CREATE PROCEDURE [dbo].[sp_confirmacao_pagamento_incluir] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[sp_confirmacao_pagamento_incluir] 
		   @id_confirmacao_pagamento int,
           @id_execucao_pd int = null,
           @id_programacao_desembolso_execucao_item int  = null,
           @id_autorizacao_ob int  = null,
           @id_autorizacao_ob_item int = null,
           @dt_confirmacao datetime  = null,           
           @id_tipo_documento int  = null,
		   @nr_documento varchar(30)  = null,
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
		   @nr_banco_favorecido varchar(10) = null,
           @nr_agencia_favorecido varchar(10) = null,
           @nr_conta_favorecido varchar(10) = null,
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
           @nm_reduzido_credor varchar(14) = null,
           @fl_transmissao_transmitido_prodesp bit  = null,
           @cd_transmissao_status_prodesp varchar(50)  = null,
           @dt_transmissao_transmitido_prodesp datetime  = null,
           @ds_transmissao_mensagem_prodesp varchar(200)  = null,
		   @nr_empenhoSiafem varchar(11) = null,
		   @cd_credor_organizacao_docto int = null,
		   @nr_cnpj_cpf_ug_credor_docto varchar(14) = null,
		   @vr_desdobramento decimal(8,2) = null,
		   @dt_realizacao datetime = null,
		   @nm_reduzido_credor_docto varchar(14) = null,
		   @nr_documento_gerador varchar(22) = null

As
begin 
Begin TRANSACTION 
 SET NOCOUNT ON;
		insert into pagamento.tb_confirmacao_pagamento_item
           (
		    id_confirmacao_pagamento
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
           ,nr_banco_favorecido
           ,nr_agencia_favorecido
           ,nr_conta_favorecido
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
           ,ds_transmissao_mensagem_prodesp
	       ,nr_empenhoSiafem
		   ,cd_credor_organizacao_docto
           ,nr_cnpj_cpf_ug_credor_docto
		   ,vr_desdobramento
		   ,dt_realizacao
		   ,nm_reduzido_credor_docto
		   ,nr_documento_gerador
		    )
     VALUES
           (
			@id_confirmacao_pagamento
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
			,@nr_banco_favorecido
			,@nr_agencia_favorecido
			,@nr_conta_favorecido
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
			,@ds_transmissao_mensagem_prodesp
			,@nr_empenhoSiafem
			,@cd_credor_organizacao_docto
			,@nr_cnpj_cpf_ug_credor_docto
			,@vr_desdobramento
			,@dt_realizacao
			,@nm_reduzido_credor_docto
			,@nr_documento_gerador)
end
commit
GO
PRINT N'Altering [dbo].[PR_EVENTO_ALTERAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_EVENTO_ALTERAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_EVENTO_ALTERAR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ==============================================================    
-- Author:  Rodrigo Borghi
-- Create date: 20/09/2018
-- Description: Procedure para Alterar Evento
-- ==============================================================   

ALTER PROCEDURE [dbo].[PR_EVENTO_ALTERAR]
	@id_evento int
	,@id_nl_parametrizacao int
	,@id_rap_tipo char(1) = null
	,@id_documento_tipo int
	,@nr_evento varchar(50) = null
	,@nr_classificacao varchar(50) = null
	,@ds_entrada_saida char(10) = null
	,@nr_fonte varchar(50) = null
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE
		[pagamento].[tb_evento]
	SET 
		id_nl_parametrizacao = @id_nl_parametrizacao
		,id_rap_tipo = @id_rap_tipo
		,id_documento_tipo = @id_documento_tipo
		,nr_evento = @nr_evento
		,nr_classificacao = @nr_classificacao
		,ds_entrada_saida = @ds_entrada_saida
		,nr_fonte = @nr_fonte
	WHERE
		id_evento = @id_evento
END

-- ==============================================================    
-- Author:  Rodrigo Borghi
-- Create date: 20/09/2018
-- Description: Procedure para Incluir um Evento
-- ==============================================================    
SET ANSI_NULLS ON
GO
PRINT N'Altering [dbo].[PR_EVENTO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_EVENTO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_EVENTO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar parametrização de NL
-- =================================================================== 

ALTER PROCEDURE [dbo].[PR_EVENTO_CONSULTAR]
	@id_evento int = null
	,@id_nl_parametrizacao int = null
	,@id_rap_tipo int = null
	,@id_documento_tipo int = null
	,@nr_evento varchar(50) = null
	,@nr_classificacao varchar(50) = null
	,@ds_entrada_saida char(10) = null
	,@nr_fonte varchar(50) = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
		  
	id_evento
	,id_nl_parametrizacao 
	,id_rap_tipo
	,id_documento_tipo
	,nr_evento
	,nr_classificacao
	,ds_entrada_saida
	,nr_fonte
	FROM [pagamento].[tb_evento] (NOLOCK)
	WHERE
		( NULLIF( @id_evento, 0 ) IS NULL OR id_evento = @id_evento )
        AND ( NULLIF( @id_nl_parametrizacao, 0 ) IS NULL OR id_nl_parametrizacao = @id_nl_parametrizacao )
		AND ( NULLIF( @id_rap_tipo, 0 ) IS NULL OR id_rap_tipo = @id_rap_tipo )
		AND ( NULLIF( @id_documento_tipo, 0 ) IS NULL OR id_documento_tipo = @id_documento_tipo )
		AND ( NULLIF( @nr_evento,'' ) IS NULL OR nr_evento = @nr_evento )
		AND ( NULLIF( @nr_classificacao, '' ) IS NULL OR nr_classificacao = @nr_classificacao )
		AND ( NULLIF( @ds_entrada_saida, '' ) IS NULL OR ds_entrada_saida LIKE '%' + @ds_entrada_saida + '%' )
		AND ( NULLIF( @nr_fonte, '' ) IS NULL OR nr_fonte = @nr_fonte )
	ORDER BY id_evento
END;
GO
PRINT N'Altering [dbo].[PR_EVENTO_INCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_EVENTO_INCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_EVENTO_INCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_EVENTO_INCLUIR]
   @id_nl_parametrizacao int
   ,@id_rap_tipo char(1) = null
   ,@id_documento_tipo int
   ,@nr_evento varchar(50) = null
   ,@nr_classificacao varchar(50) = null
   ,@ds_entrada_saida char(10) = null
   ,@nr_fonte varchar(50) = null
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
INSERT INTO [pagamento].[tb_evento]
           ([id_nl_parametrizacao]
		   ,[id_rap_tipo]
           ,[id_documento_tipo]
		   ,[nr_evento]
		   ,[nr_classificacao]
		   ,[ds_entrada_saida]
		   ,[nr_fonte])
     VALUES
           (@id_nl_parametrizacao
		   ,@id_rap_tipo
           ,@id_documento_tipo
		   ,@nr_evento
		   ,@nr_classificacao
		   ,@ds_entrada_saida
		   ,@nr_fonte)
  
COMMIT

SELECT SCOPE_IDENTITY();
GO
PRINT N'Altering [dbo].[PR_NL_PARAMETRIZACAO_ALTERAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_NL_PARAMETRIZACAO_ALTERAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_ALTERAR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Alterar parametrização de NL
-- ==============================================================   

ALTER PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_ALTERAR]
	@id_nl_parametrizacao int
	,@id_nl_tipo int
	,@ds_observacao varchar(250) = null
	,@bl_transmitir bit
	,@nr_favorecida_cnpjcpfug varchar(15) = null
	,@nr_favorecida_gestao int = null
	,@nr_unidade_gestora int = null
	,@id_parametrizacao_forma_gerar_nl int
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE
		[pagamento].[tb_nl_parametrizacao]
	SET 
		ds_observacao = @ds_observacao
		,id_nl_tipo = @id_nl_tipo
		,bl_transmitir = @bl_transmitir
		,nr_favorecida_cnpjcpfug = @nr_favorecida_cnpjcpfug
		,nr_favorecida_gestao = @nr_favorecida_gestao
		,nr_unidade_gestora = @nr_unidade_gestora
		,id_parametrizacao_forma_gerar_nl = @id_parametrizacao_forma_gerar_nl
	WHERE
		id_nl_parametrizacao = @id_nl_parametrizacao
END
GO
PRINT N'Altering [dbo].[PR_NL_PARAMETRIZACAO_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_NL_PARAMETRIZACAO_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar parametrização de NL
-- =================================================================== 

ALTER PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_CONSULTAR]
	@id_nl_parametrizacao int = null,
	@id_nl_tipo int = null,
	@ds_observacao varchar(250) = null,
	@bl_transmitir bit = null,
	@nr_favorecida_cnpjcpfug varchar(15) = null,
	@nr_favorecida_gestao int = null,
	@nr_unidade_gestora int = null,
	@id_parametrizacao_forma_gerar_nl int = null
AS  
BEGIN  

 SET NOCOUNT ON;  
  
	SELECT 
		  
	id_nl_parametrizacao
	,ds_observacao 
	,bl_transmitir
	,nr_favorecida_cnpjcpfug
	,nr_favorecida_gestao
	,nr_unidade_gestora
	,id_parametrizacao_forma_gerar_nl
	FROM [pagamento].[tb_nl_parametrizacao] (NOLOCK)
	WHERE
		( NULLIF( @id_nl_parametrizacao, 0 ) IS NULL OR id_nl_parametrizacao = @id_nl_parametrizacao )
		AND ( NULLIF( @id_nl_tipo, 0) IS NULL OR id_nl_tipo = @id_nl_tipo)
        AND ( NULLIF( @ds_observacao, '' ) IS NULL OR ds_observacao LIKE '%' +  @ds_observacao + '%' )
		AND ( @bl_transmitir IS NULL OR bl_transmitir = @bl_transmitir )
		AND ( NULLIF( @nr_favorecida_cnpjcpfug, '' ) IS NULL OR nr_favorecida_cnpjcpfug = @nr_favorecida_cnpjcpfug )
		AND ( NULLIF( @nr_favorecida_gestao, 0 ) IS NULL OR nr_favorecida_gestao = @nr_favorecida_gestao )
		AND ( NULLIF( @nr_unidade_gestora, 0 ) IS NULL OR nr_unidade_gestora = @nr_unidade_gestora )
		AND ( NULLIF( @id_parametrizacao_forma_gerar_nl, 0 ) IS NULL OR id_parametrizacao_forma_gerar_nl = @id_parametrizacao_forma_gerar_nl )
	ORDER BY id_nl_parametrizacao
END;
GO
PRINT N'Altering [dbo].[PR_NL_PARAMETRIZACAO_INCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_NL_PARAMETRIZACAO_INCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_INCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Incluir uma parametrização de NL
-- ==============================================================    
  
ALTER PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_INCLUIR]
   @ds_observacao varchar(250) = null
   ,@id_nl_tipo int
   ,@bl_transmitir bit
   ,@nr_favorecida_cnpjcpfug varchar(15) = null
   ,@nr_favorecida_gestao int = null
   ,@nr_unidade_gestora int = null
   ,@id_parametrizacao_forma_gerar_nl int
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
INSERT INTO [pagamento].[tb_nl_parametrizacao]
           ([id_nl_tipo]
		   ,[ds_observacao]
		   ,[bl_transmitir]
           ,[nr_favorecida_cnpjcpfug]
		   ,[nr_favorecida_gestao]
		   ,[nr_unidade_gestora]
		   ,[id_parametrizacao_forma_gerar_nl])
     VALUES
           (@id_nl_tipo
		   ,@ds_observacao
		   ,@bl_transmitir
		   ,@nr_favorecida_cnpjcpfug
		   ,@nr_favorecida_gestao
		   ,@nr_unidade_gestora
		   ,@id_parametrizacao_forma_gerar_nl)
  
COMMIT

SELECT SCOPE_IDENTITY();
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_FILTRO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_INCLUIR_FILTRO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_FILTRO] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_FILTRO](
@id_confirmacao_pagamento_tipo int,
@nr_conta varchar(20) = NULL,
@dt_preparacao datetime  = NULL,
@id_tipo_pagamento int  = NULL,
@dt_confirmacao datetime  = NULL,
@id_tipo_documento int  = NULL,
@nr_documento varchar(19)  = NULL
)
AS
BEGIN

	DECLARE @nr_agrupamento_novo INT;

	SET @nr_agrupamento_novo = COALESCE((SELECT MAX(nr_agrupamento) FROM pagamento.tb_confirmacao_pagamento), 0) + 1

	BEGIN TRANSACTION
	SET NOCOUNT ON;

	INSERT INTO pagamento.tb_confirmacao_pagamento (
	id_confirmacao_pagamento_tipo,
	nr_conta,
	dt_preparacao,
	id_tipo_pagamento,
	dt_confirmacao,
    id_tipo_documento,
	nr_documento,
	nr_agrupamento
	)
	VALUES(
	@id_confirmacao_pagamento_tipo,
	@nr_conta,
	@dt_preparacao,
	@id_tipo_pagamento,
	@dt_confirmacao,
	@id_tipo_documento,
	@nr_documento,
	@nr_agrupamento_novo
	)
	COMMIT
	SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------
GO
PRINT N'Creating [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_ITEM_PRODESP]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_CONFIRMACAO_PAGAMENTO_INCLUIR_ITEM_PRODESP'))
   EXEC('CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_ITEM_PRODESP] AS BEGIN SET NOCOUNT ON; END')


GO

ALTER PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_ITEM_PRODESP]        
     
  @nr_documento_gerador varchar(30)  = NULL,        
  @cd_transmissao_status_prodesp varchar(1) = NULL,
  @fl_transmissao_transmitido_prodesp bit = NULL,
  @dt_transmissao_transmitido_prodesp datetime = NULL,
  @ds_transmissao_mensagem_prodesp varchar(140) = NULL,
  @id_confirmacao_pagamento int = NULL    
       
AS          
BEGIN        
	UPDATE [pagamento].[tb_confirmacao_pagamento_item]
	SET cd_transmissao_status_prodesp = @cd_transmissao_status_prodesp,      
     fl_transmissao_transmitido_prodesp = @fl_transmissao_transmitido_prodesp,      
     dt_transmissao_transmitido_prodesp = @dt_transmissao_transmitido_prodesp,    
     ds_transmissao_mensagem_prodesp =  @ds_transmissao_mensagem_prodesp   
	WHERE id_confirmacao_pagamento = @id_confirmacao_pagamento AND
    LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17)
End
GO
PRINT N'Creating [dbo].[PR_FORMA_GERAR_NL_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_FORMA_GERAR_NL_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_FORMA_GERAR_NL_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO
ALTER PROCEDURE [dbo].[PR_FORMA_GERAR_NL_CONSULTAR]
	@id_parametrizacao_forma_gerar_nl int = null
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
	id_parametrizacao_forma_gerar_nl
	,ds_gerar_nl
	FROM [pagamento].[tb_parametrizacao_forma_gerar_nl] (NOLOCK)
	WHERE
		( NULLIF( @id_parametrizacao_forma_gerar_nl, 0 ) IS NULL OR id_parametrizacao_forma_gerar_nl = @id_parametrizacao_forma_gerar_nl )
	ORDER BY id_parametrizacao_forma_gerar_nl
END;
GO
PRINT N'Creating [dbo].[PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA'))
   EXEC('CREATE PROCEDURE [dbo].[PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA] AS BEGIN SET NOCOUNT ON; END')


GO


-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para consultar parametrização de NL
-- =================================================================== 

ALTER PROCEDURE [dbo].[PR_GET_FORMA_GERAR_NL_POR_TIPO_DESPESA]
	@id_despesa_tipo int
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 
	 P.id_parametrizacao_forma_gerar_nl
	,G.ds_gerar_nl
    ,D.id_despesa_tipo
	,T.cd_despesa_tipo 
		FROM [pagamento].[tb_nl_parametrizacao]						AS P
		INNER JOIN [pagamento].[tb_despesa]							AS D ON      P.id_nl_parametrizacao				= D.id_nl_parametrizacao
		INNER JOIN [pagamento].[tb_despesa_tipo]					AS T ON      D.id_despesa_tipo					= T.id_despesa_tipo
		INNER JOIN [pagamento].[tb_parametrizacao_forma_gerar_nl]   AS G ON      P.id_parametrizacao_forma_gerar_nl = G.id_parametrizacao_forma_gerar_nl
			WHERE
				( NULLIF( @id_despesa_tipo, 0 ) IS NULL OR D.id_despesa_tipo = @id_despesa_tipo )
			ORDER BY D.id_despesa_tipo
END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 18/09/2018
-- Description: Procedure que retorna o incremento +1 do campo agrupamento
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO]

AS

BEGIN

SET NOCOUNT ON;  

	BEGIN
	
		IF (SELECT MAX([nr_agrupamento] + 1) FROM [contaunica].[tb_impressao_relacao_re_rt]) IS NULL

			SELECT 1

		ELSE

			SELECT
				MAX([nr_agrupamento] + 1)
			FROM [contaunica].[tb_impressao_relacao_re_rt]

	END

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 29/08/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID]
	@cd_relre VARCHAR(11) = NULL,
	@cd_relrt VARCHAR(11) = NULL,
	@cd_relob VARCHAR(11) = NULL,
	@ds_status_siafem VARCHAR(1) = NULL,
	@dt_cadastramentoDe DATETIME = NULL,
	@dt_cadastramentoAte DATETIME = NULL,
	@cd_unidade_gestora VARCHAR(6) = NULL,
	@cd_gestao VARCHAR(5) = NULL,
	@cd_banco VARCHAR(5) = NULL,
	@dt_solicitacao datetime = NULL,
	@nr_agrupamento INT = NULL,
	@fg_cancelamento_relacao_re_rt BIT = NULL
AS

BEGIN

--DECLARE
--	@cd_relre VARCHAR(11) = NULL,
--	@cd_relrt VARCHAR(11) = NULL,
--	@cd_relob VARCHAR(11) = NULL,
--	@ds_status_siafem VARCHAR(1) = NULL,
--	@dt_cadastramentoDe DATETIME = NULL,
--	@dt_cadastramentoAte DATETIME = NULL,
--	@cd_unidade_gestora VARCHAR(6) = NULL,
--	@cd_gestao VARCHAR(5) = NULL,
--	@cd_banco VARCHAR(5) = NULL,
--	@dt_solicitacao datetime = NULL,
--	@nr_agrupamento INT = NULL,
--	@fg_cancelamento_relacao_re_rt BIT = NULL

SET NOCOUNT ON;  

SELECT DISTINCT
	[id_impressao_relacao_re_rt]
	,[nr_agrupamento]
	,CASE 
		WHEN SUBSTRING([cd_relob_re], 5, 2) = 'RE' THEN [cd_relob_re]
		WHEN SUBSTRING([cd_relob_re], 5, 2) = 'RT' THEN [cd_relob_re]
		ELSE NULL
	END AS [cd_relob]
	,[cd_unidade_gestora]
	,[cd_gestao]
	,[cd_banco]
	,[dt_solicitacao]
	,[ds_status_siafem]
	,[fg_cancelamento_relacao_re_rt]
	,[fg_transmitido_siafem]
FROM (
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,re.[cd_relob_re]
		,re.[nr_ob_re]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_itens_obs_re] re
	INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_re
	UNION ALL
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,rt.[cd_relob_rt]
		,rt.[nr_ob_rt]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_itens_obs_rt] rt
	INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_rt
	UNION ALL
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,rt.[cd_relob_rt]
		,rt.[nr_ob_rt]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
	LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = rt.[cd_relob_rt]
	WHERE ir.[cd_relob] IS NULL
) AS ImpressaoRelacaoRERT
WHERE ( @ds_status_siafem IS NULL OR [ds_status_siafem] = @ds_status_siafem )
		AND ( FORMAT(@dt_cadastramentoDe, 'yyyy-MM-dd 00:00:00') IS NULL OR [dt_cadastramento] >= FORMAT(@dt_cadastramentoDe, 'yyyy-MM-dd 00:00:00') ) 
		AND ( FORMAT(@dt_cadastramentoAte, 'yyyy-MM-dd 23:59:59') IS NULL OR [dt_cadastramento] <= FORMAT(@dt_cadastramentoAte, 'yyyy-MM-dd 23:59:59') ) 
		AND ( @cd_unidade_gestora IS NULL OR [cd_unidade_gestora] = @cd_unidade_gestora ) 
		AND ( @cd_gestao IS NULL OR [cd_gestao] = @cd_gestao )
		AND ( @cd_banco IS NULL OR [cd_banco] = @cd_banco )
		AND ( @dt_solicitacao IS NULL OR [dt_solicitacao] = @dt_solicitacao ) 
		AND ( @nr_agrupamento IS NULL OR [nr_agrupamento] = @nr_agrupamento ) 
		AND ( @fg_cancelamento_relacao_re_rt IS NULL OR [fg_cancelamento_relacao_re_rt] = @fg_cancelamento_relacao_re_rt ) 
		AND ( ( @cd_relre IS NULL OR [cd_relob_re] = @cd_relre ) AND 
			( @cd_relrt IS NULL OR [cd_relob_re] = @cd_relrt ) AND 
			( @cd_relob IS NULL OR [nr_ob_re] = @cd_relob ) )
ORDER BY id_impressao_relacao_re_rt

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 04/09/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR]
	@id_impressao_relacao_re_rt INT = NULL,
	@relRERT VARCHAR(11) = NULL
AS

BEGIN

--DECLARE
--	@id_impressao_relacao_re_rt INT = 558,
--	@relRERT VARCHAR(11) = NULL

SET NOCOUNT ON;  

SET @relRERT = 
(
	SELECT DISTINCT
		[cd_relob_re]
	FROM (
		SELECT  
			ir.[id_impressao_relacao_re_rt]
			,re.[cd_relob_re]
		FROM [contaunica].[tb_itens_obs_re] re
		INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_re
		UNION ALL
		SELECT 
			ir.[id_impressao_relacao_re_rt]
			,rt.[cd_relob_rt]
		FROM [contaunica].[tb_itens_obs_rt] rt
		INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_rt
	) AS ImpressaoRelacaoRERT
	WHERE ( @id_impressao_relacao_re_rt IS NULL OR [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt )
)

IF SUBSTRING(@relRERT, 5, 2) = 'RE'
	
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,re.[cd_relob_re]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,re.[nr_ob_re] AS [nr_ob]
		,re.[fg_prioridade]
		,re.[cd_tipo_ob]
		,re.[ds_nome_favorecido]
		,re.[ds_banco_favorecido]
		,re.[cd_agencia_favorecido]
		,re.[ds_conta_favorecido]
		,re.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		INNER JOIN [contaunica].[tb_itens_obs_re] re ON ir.[cd_relob] = cd_relob_re
	WHERE ir.[cd_relob] = @relRERT
	ORDER BY id_impressao_relacao_re_rt

ELSE IF SUBSTRING(@relRERT, 5, 2) = 'RT'

	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,rt.[cd_relob_rt]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,rt.[nr_ob_rt] AS [nr_ob]
		,rt.[cd_conta_bancaria_emitente]
		,rt.[cd_unidade_gestora_favorecida]
		,rt.[cd_gestao_favorecida]
		,rt.[ds_mnemonico_ug_favorecida]
		,rt.[ds_banco_favorecido]
		,rt.[cd_agencia_favorecido]
		,rt.[ds_conta_favorecido]
		,rt.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		INNER JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
	WHERE ir.[cd_relob] = @relRERT
	ORDER BY id_impressao_relacao_re_rt

ELSE
	
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,rt.[cd_relob_rt]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,rt.[nr_ob_rt] AS [nr_ob]
		,rt.[cd_conta_bancaria_emitente]
		,rt.[cd_unidade_gestora_favorecida]
		,rt.[cd_gestao_favorecida]
		,rt.[ds_mnemonico_ug_favorecida]
		,rt.[ds_banco_favorecido]
		,rt.[cd_agencia_favorecido]
		,rt.[ds_conta_favorecido]
		,rt.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
	WHERE ir.[id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt
	ORDER BY id_impressao_relacao_re_rt

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 19/09/2018
-- Description: Procedure para consultar a lista de Re e/ou RT com as OBs transmitidas
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO]
	@nr_agrupamento INT = NULL
AS

BEGIN

--DECLARE
--	@nr_agrupamento INT = 5

	SET NOCOUNT ON;  

		SELECT DISTINCT
			--ROW_NUMBER() OVER(ORDER BY [cd_relob], [nr_ob] ASC) AS Row#
			ir.[nr_agrupamento]
			,ir.[cd_unidade_gestora]
			,ir.[cd_gestao]
			,ir.[cd_banco]
			,CASE 
				WHEN SUBSTRING(ir.[cd_relob], 5, 2) = 'RE' THEN re.[cd_relob_re]
				WHEN SUBSTRING(ir.[cd_relob], 5, 2) = 'RT' THEN rt.[cd_relob_rt]
				ELSE NULL
			END AS [cd_relob]
			,CASE 
				WHEN SUBSTRING(re.[cd_relob_re], 5, 2) = 'RE' THEN re.[nr_ob_re]
				WHEN SUBSTRING(rt.[cd_relob_rt], 5, 2) = 'RT' THEN rt.[nr_ob_rt]
				ELSE NULL
			END AS [nr_ob]
			,re.[fg_prioridade]
			,re.[cd_tipo_ob]
			,re.[ds_nome_favorecido]
			,rt.[cd_conta_bancaria_emitente]
			,rt.[cd_unidade_gestora_favorecida]
			,rt.[cd_gestao_favorecida]
			,rt.[ds_mnemonico_ug_favorecida]
			,CASE 
				WHEN re.[ds_banco_favorecido] != '' THEN re.[ds_banco_favorecido]
				WHEN rt.[ds_banco_favorecido] != '' THEN rt.[ds_banco_favorecido]
				ELSE NULL
			END AS [ds_banco_favorecido]
			,CASE 
				WHEN re.[cd_agencia_favorecido] != '' THEN re.[cd_agencia_favorecido]
				WHEN rt.[cd_agencia_favorecido] != '' THEN rt.[cd_agencia_favorecido]
				ELSE NULL
			END AS [cd_agencia_favorecido]
			,CASE 
				WHEN re.[ds_conta_favorecido] != '' THEN re.[ds_conta_favorecido]
				WHEN rt.[ds_conta_favorecido] != '' THEN rt.[ds_conta_favorecido]
				ELSE NULL
			END AS [ds_conta_favorecido]
			,CASE 
				WHEN re.[vl_ob] != 0 THEN re.[vl_ob]
				WHEN rt.[vl_ob] != 0 THEN rt.[vl_ob]
				ELSE NULL
			END AS [vl_ob]
		FROM [contaunica].[tb_impressao_relacao_re_rt] ir
			LEFT JOIN [contaunica].[tb_itens_obs_re] re ON ir.[cd_relob] = cd_relob_re
			LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
		WHERE ( ( @nr_agrupamento IS NULL OR [nr_agrupamento] = @nr_agrupamento ) )
		--ORDER BY Row# ASC

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID] AS BEGIN SET NOCOUNT ON; END')


GO

-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 04/09/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT por ID
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID]
	@id_impressao_relacao_re_rt INT = NULL
AS

BEGIN

--DECLARE
--	@id_impressao_relacao_re_rt INT = 36

SET NOCOUNT ON;  

	BEGIN

		SELECT
			[id_impressao_relacao_re_rt]
		   ,[cd_relob]
		   ,[nr_ob]
		   ,[cd_relatorio]
           ,[nr_agrupamento]
           ,[cd_unidade_gestora]
           ,[ds_nome_unidade_gestora]
           ,[cd_gestao]
           ,[ds_nome_gestao]
           ,[cd_banco]
           ,[ds_nome_banco]
           ,[ds_texto_autorizacao]
           ,[ds_cidade]
           ,[ds_nome_gestor_financeiro]
           ,[ds_nome_ordenador_assinatura]
           ,[dt_solicitacao]
           ,[dt_referencia]
           ,[dt_cadastramento]
           ,[dt_emissao]
           ,[vl_total_documento]
           ,[vl_extenso]
           ,[fg_transmitido_siafem]
           ,[fg_transmitir_siafem]
           ,[dt_transmitido_siafem]
           ,[ds_status_siafem]
           ,[ds_msgRetornoTransmissaoSiafem]
           ,[fg_cancelamento_relacao_re_rt]
           ,[nr_agencia]
           ,[ds_nome_agencia]
           ,[nr_conta_c]
		FROM [contaunica].[tb_impressao_relacao_re_rt]
		WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

	END

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para excluir Impressão de Relação RE e RT
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR]
		@id_impressao_relacao_re_rt INT = NULL,
		@relRERT VARCHAR(11) = NULL
AS

BEGIN

	--DECLARE
	--@id_impressao_relacao_re_rt INT = 1,
	--@relRERT VARCHAR(11) = NULL

	SET NOCOUNT ON;

		SET @relRERT = (SELECT DISTINCT [cd_relob] FROM [contaunica].[tb_impressao_relacao_re_rt] WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt)

		IF SUBSTRING(@relRERT, 5, 2) = 'RE'

			BEGIN

				DELETE FROM [contaunica].[tb_itens_obs_re] WHERE [cd_relob_re] = @relRERT

			END
		
		ELSE

			BEGIN

				DELETE FROM [contaunica].[tb_itens_obs_rt] WHERE [cd_relob_rt] = @relRERT

			END

		DELETE FROM [contaunica].[tb_impressao_relacao_re_rt] WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

END;
GO
PRINT N'Creating [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_IMPRESSAO_RELACAO_RE_RT_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Impressão de Relação RE e RT
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_SALVAR]
		@id_impressao_relacao_re_rt INT = NULL
		,@cd_relob VARCHAR(11) = NULL
		,@nr_ob VARCHAR(11) = NULL
		,@cd_relatorio VARCHAR(11) = NULL
		,@nr_agrupamento INT = NULL
		,@cd_unidade_gestora VARCHAR(6) = NULL
		,@ds_nome_unidade_gestora VARCHAR(30) = NULL
		,@cd_gestao VARCHAR(5) = NULL
		,@ds_nome_gestao VARCHAR(30) = NULL
		,@cd_banco VARCHAR(5) = NULL
		,@ds_nome_banco VARCHAR(30) = NULL
		,@ds_texto_autorizacao VARCHAR(70) = NULL
		,@ds_cidade VARCHAR(30) = NULL
		,@ds_nome_gestor_financeiro VARCHAR(30) = NULL
		,@ds_nome_ordenador_assinatura VARCHAR(30) = NULL
		,@dt_solicitacao DATETIME = NULL
		,@dt_referencia DATETIME = NULL
		,@dt_cadastramento DATETIME = NULL
		,@dt_emissao DATETIME = NULL
		,@vl_total_documento DECIMAL(15,2) = NULL
		,@vl_extenso VARCHAR(255) = NULL
		,@fg_transmitido_siafem BIT = NULL
		,@fg_transmitir_siafem BIT = NULL
		,@dt_transmitido_siafem DATETIME = NULL
		,@ds_status_siafem VARCHAR(1) = NULL
		,@ds_msgRetornoTransmissaoSiafem VARCHAR(140) = NULL
		,@fg_cancelamento_relacao_re_rt BIT = NULL
		,@nr_agencia VARCHAR(5) = NULL
		,@ds_nome_agencia VARCHAR(30) = NULL
		,@nr_conta_c VARCHAR(10) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	IF EXISTS (SELECT 1 FROM [contaunica].[tb_impressao_relacao_re_rt] (NOLOCK) WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt)
	
		BEGIN

			UPDATE [contaunica].[tb_impressao_relacao_re_rt] SET
				[cd_relob] = @cd_relob
				,[nr_ob] = @nr_ob
				,[cd_relatorio] = @cd_relatorio
				,[nr_agrupamento] = @nr_agrupamento
				,[cd_unidade_gestora] = @cd_unidade_gestora
				,[ds_nome_unidade_gestora] = @ds_nome_unidade_gestora
				,[cd_gestao] = @cd_gestao
				,[ds_nome_gestao] = @ds_nome_gestao
				,[cd_banco] = @cd_banco
				,[ds_nome_banco] = @ds_nome_banco
				,[ds_texto_autorizacao] = @ds_texto_autorizacao
				,[ds_cidade] = @ds_cidade
				,[ds_nome_gestor_financeiro] = @ds_nome_gestor_financeiro
				,[ds_nome_ordenador_assinatura] = @ds_nome_ordenador_assinatura
				,[dt_solicitacao] = @dt_solicitacao
				,[dt_referencia] = @dt_referencia
				,[dt_cadastramento] = @dt_cadastramento
				,[dt_emissao] = @dt_emissao
				,[vl_total_documento] = @vl_total_documento
				,[vl_extenso] = @vl_extenso
				,[fg_transmitido_siafem] = @fg_transmitido_siafem
				,[fg_transmitir_siafem] = @fg_transmitir_siafem
				,[dt_transmitido_siafem] = @dt_transmitido_siafem
				,[ds_status_siafem] = @ds_status_siafem
				,[ds_msgRetornoTransmissaoSiafem] = @ds_msgRetornoTransmissaoSiafem
				,[fg_cancelamento_relacao_re_rt] = @fg_cancelamento_relacao_re_rt
				,[nr_agencia] = @nr_agencia
				,[ds_nome_agencia] = @ds_nome_agencia
				,[nr_conta_c] = @nr_conta_c
			WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

			SELECT @id_impressao_relacao_re_rt;

		END

	ELSE

		BEGIN

			INSERT INTO [contaunica].[tb_impressao_relacao_re_rt] (
				[cd_relob]
				,[nr_ob]			
				,[cd_relatorio]
				,[nr_agrupamento]
				,[cd_unidade_gestora]
				,[ds_nome_unidade_gestora]
				,[cd_gestao]
				,[ds_nome_gestao]
				,[cd_banco]
				,[ds_nome_banco]
				,[ds_texto_autorizacao]
				,[ds_cidade]
				,[ds_nome_gestor_financeiro]
				,[ds_nome_ordenador_assinatura]
				,[dt_solicitacao]
				,[dt_referencia]
				,[dt_cadastramento]
				,[dt_emissao]
				,[vl_total_documento]
				,[vl_extenso]
				,[fg_transmitido_siafem]
				,[fg_transmitir_siafem]
				,[dt_transmitido_siafem]
				,[ds_status_siafem]
				,[ds_msgRetornoTransmissaoSiafem]
				,[fg_cancelamento_relacao_re_rt]
				,[nr_agencia]
				,[ds_nome_agencia]
				,[nr_conta_c]
			)
			VALUES 
			(
				@cd_relob
				,@nr_ob
				,@cd_relatorio
				,@nr_agrupamento
				,@cd_unidade_gestora
				,@ds_nome_unidade_gestora
				,@cd_gestao
				,@ds_nome_gestao
				,@cd_banco
				,@ds_nome_banco
				,@ds_texto_autorizacao
				,@ds_cidade
				,@ds_nome_gestor_financeiro
				,@ds_nome_ordenador_assinatura
				,@dt_solicitacao
				,@dt_referencia
				,@dt_cadastramento
				,@dt_emissao
				,@vl_total_documento
				,@vl_extenso
				,@fg_transmitido_siafem
				,@fg_transmitir_siafem
				,@dt_transmitido_siafem
				,@ds_status_siafem
				,@ds_msgRetornoTransmissaoSiafem
				,@fg_cancelamento_relacao_re_rt
				,@nr_agencia
				,@ds_nome_agencia
				,@nr_conta_c
			);
    
			SELECT scope_identity();

		END

END
GO
PRINT N'Creating [dbo].[PR_ITENS_OBS_RE_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ITENS_OBS_RE_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ITENS_OBS_RE_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Itens de Obs RE
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_ITENS_OBS_RE_SALVAR]
		@cd_relob_re VARCHAR(11) = NULL
		,@nr_ob_re VARCHAR(11) = NULL
		,@fg_prioridade VARCHAR(1) = NULL
		,@cd_tipo_ob INT = NULL
		,@ds_nome_favorecido VARCHAR(50) = NULL
		,@ds_banco_favorecido VARCHAR(5) = NULL
		,@cd_agencia_favorecido VARCHAR(5) = NULL
		,@ds_conta_favorecido VARCHAR(10) = NULL
		,@vl_ob DECIMAL(15,2) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO [contaunica].[tb_itens_obs_re] (
			[cd_relob_re]
			,[nr_ob_re]
			,[fg_prioridade]
			,[cd_tipo_ob]
			,[ds_nome_favorecido]
			,[ds_banco_favorecido]
			,[cd_agencia_favorecido]
			,[ds_conta_favorecido]
			,[vl_ob]
		)
		VALUES 
		(
			@cd_relob_re
			,@nr_ob_re
			,@fg_prioridade
			,@cd_tipo_ob
			,@ds_nome_favorecido
			,@ds_banco_favorecido
			,@cd_agencia_favorecido
			,@ds_conta_favorecido
			,@vl_ob
		);
	
	COMMIT	
    
    SELECT @@IDENTITY

END
GO
PRINT N'Creating [dbo].[PR_ITENS_OBS_RT_SALVAR]...';
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.PR_ITENS_OBS_RT_SALVAR'))
   EXEC('CREATE PROCEDURE [dbo].[PR_ITENS_OBS_RT_SALVAR] AS BEGIN SET NOCOUNT ON; END')


GO
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Itens de Obs RT
-- =================================================================== 
ALTER PROCEDURE [dbo].[PR_ITENS_OBS_RT_SALVAR]
		@cd_relob_rt VARCHAR(11) = NULL
		,@nr_ob_rt VARCHAR(11) = NULL
		,@cd_conta_bancaria_emitente VARCHAR(9) = NULL
		,@cd_unidade_gestora_favorecida VARCHAR(6) = NULL
		,@cd_gestao_favorecida VARCHAR(5) = NULL
		,@ds_mnemonico_ug_favorecida VARCHAR(15) = NULL
		,@ds_banco_favorecido VARCHAR(5) = NULL
		,@cd_agencia_favorecido VARCHAR(5) = NULL
		,@ds_conta_favorecido VARCHAR(10) = NULL
		,@vl_ob DECIMAL(15,2) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO [contaunica].[tb_itens_obs_rt] (
			[cd_relob_rt]
			,[nr_ob_rt]
			,[cd_conta_bancaria_emitente]
			,[cd_unidade_gestora_favorecida]
			,[cd_gestao_favorecida]
			,[ds_mnemonico_ug_favorecida]
			,[ds_banco_favorecido]
			,[cd_agencia_favorecido]
			,[ds_conta_favorecido]
			,[vl_ob]
		)
		VALUES 
		(
			@cd_relob_rt
			,@nr_ob_rt
			,@cd_conta_bancaria_emitente
			,@cd_unidade_gestora_favorecida
			,@cd_gestao_favorecida
			,@ds_mnemonico_ug_favorecida
			,@ds_banco_favorecido
			,@cd_agencia_favorecido
			,@ds_conta_favorecido
			,@vl_ob
		);
	
	COMMIT	
    
    SELECT @@IDENTITY

END
GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_CREDITO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_DIST_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_MES_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_MES_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_OB_EXCLUIR_NAOAGRUPADOS]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_NEW]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_OB_ITEM_CONSULTAR_OLD]';


GO
PRINT N'Refreshing [dbo].[PR_AUTORIZACAO_OB_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_AUTORIZACAO_OB_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_DELETE]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_DELETE]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_ALTERAR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_INCLUIR_OLD]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT_ID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_SELECT_ID]';


GO
PRINT N'Refreshing [dbo].[PR_CONFIRMACAO_PAGAMENTO_UPDATE]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_CONFIRMACAO_PAGAMENTO_UPDATE]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_NEW]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR_OLD]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_NEW]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID_OLD]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS]';


GO
PRINT N'Refreshing [dbo].[sp_confirmacao_pagamento_insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_confirmacao_pagamento_insert]';


GO
PRINT N'Refreshing [dbo].[PR_NL_PARAMETRIZACAO_OBTER_TIPONL_DA_DESPESA]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_NL_PARAMETRIZACAO_OBTER_TIPONL_DA_DESPESA]';


GO
PRINT N'Refreshing [dbo].[PR_TIPO_DESPESA_ALTERAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_TIPO_DESPESA_ALTERAR]';


GO
PRINT N'Refreshing [dbo].[PR_TIPO_DESPESA_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_TIPO_DESPESA_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_TIPO_DESPESA_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_TIPO_DESPESA_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_TIPO_DESPESA_INCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_TIPO_DESPESA_INCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_EVENTO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_EVENTO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_NL_PARAMETRIZACAO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_NL_PARAMETRIZACAO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_ARQUIVO_REMESSA_CONSULTA_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_ARQUIVO_REMESSA_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_ARQUIVO_REMESSA_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_ARQUIVO_REMESSA_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_ARQUIVO_REMESSA_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_ARQUIVO_REMESSA_SALVAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_ARQUIVO_REMESSA_SALVAR]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [dbSIDS];


GO
ALTER TABLE [pagamento].[tb_despesa] WITH CHECK CHECK CONSTRAINT [FK_tb_despesa_tb_despesa_tipo];

ALTER TABLE [pagamento].[tb_confirmacao_pagamento_item] WITH CHECK CHECK CONSTRAINT [FK_tb_confirmacao_pagamento_item_tb_despesa_tipo];

ALTER TABLE [contaunica].[tb_arquivo_remessa] WITH CHECK CHECK CONSTRAINT [FK_tb_arquivo_remessa_tb_regional];

ALTER TABLE [pagamento].[tb_nl_parametrizacao] WITH CHECK CHECK CONSTRAINT [FK_tb_nl_parametrizacao_tb_parametrizacao_forma_gerar_nl];


GO
PRINT N'Update complete.';


GO
