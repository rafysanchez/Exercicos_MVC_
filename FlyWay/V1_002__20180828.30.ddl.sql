USE [dbSIDS];


GO
IF (OBJECT_ID('[configuracao].[FK__tb_estrut__id_pr__7E42ABEE]', 'F') IS NOT NULL)
BEGIN
	PRINT N'Dropping unnamed constraint on [configuracao].[tb_estrutura]...';
	ALTER TABLE [configuracao].[tb_estrutura] DROP CONSTRAINT [FK__tb_estrut__id_pr__7E42ABEE];
END

GO

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'id_confirmacao_pagamento' AND Object_ID = Object_ID(N'contaunica.tb_programacao_desembolso_execucao'))
BEGIN
	PRINT N'Altering [contaunica].[tb_programacao_desembolso_execucao]...';
	ALTER TABLE [contaunica].[tb_programacao_desembolso_execucao] DROP COLUMN [id_confirmacao_pagamento];
END


GO
PRINT N'Starting rebuilding table [contaunica].[tb_programacao_desembolso_parametros]...';


GO

CREATE TABLE [contaunica].[tmp_ms_xx_tb_programacao_desembolso_parametros] (
    [id_programacao_desembolso_parametros] INT           IDENTITY (1, 1) NOT NULL,
    [nm_reduzido_credor]                   VARCHAR (100) NULL,
    [nr_cnpj_cpf_credor]                   VARCHAR (14)  NULL,
    [cd_unidade_gestora]                   VARCHAR (6)   NULL,
    [cd_gestao]                            VARCHAR (5)   NULL,
    [nr_banco_credor]                      VARCHAR (30)  NULL,
    [nr_agencia_credor]                    VARCHAR (100) NULL,
    [nr_conta_credor]                      VARCHAR (9)   NULL,
    [cd_classificacao]                     VARCHAR (9)   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_tb_programacao_desembolso_parametros1] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_parametros] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [contaunica].[tb_programacao_desembolso_parametros])
    BEGIN
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_programacao_desembolso_parametros] ON;
        INSERT INTO [contaunica].[tmp_ms_xx_tb_programacao_desembolso_parametros] ([id_programacao_desembolso_parametros], [nm_reduzido_credor], [nr_cnpj_cpf_credor], [cd_unidade_gestora], [cd_gestao], [nr_banco_credor], [nr_agencia_credor], [nr_conta_credor], [cd_classificacao])
        SELECT   [id_programacao_desembolso_parametros],
                 [nm_reduzido_credor],
                 [nr_cnpj_cpf_credor],
                 [cd_unidade_gestora],
                 [cd_gestao],
                 [nr_banco_credor],
                 [nr_agencia_credor],
                 [nr_conta_credor],
                 [cd_classificacao]
        FROM     [contaunica].[tb_programacao_desembolso_parametros]
        ORDER BY [id_programacao_desembolso_parametros] ASC;
        SET IDENTITY_INSERT [contaunica].[tmp_ms_xx_tb_programacao_desembolso_parametros] OFF;
    END

DROP TABLE [contaunica].[tb_programacao_desembolso_parametros];

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_tb_programacao_desembolso_parametros]', N'tb_programacao_desembolso_parametros';

EXECUTE sp_rename N'[contaunica].[tmp_ms_xx_constraint_PK_tb_programacao_desembolso_parametros1]', N'PK_tb_programacao_desembolso_parametros', N'OBJECT';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR]';


GO

PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_EXCLUIR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR]';


GO
PRINT N'Refreshing [dbo].[PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR]';


GO
PRINT N'Update complete.';


GO
