CREATE TABLE [contaunica].[tb_programacao_desembolso_parametros] (
    [id_programacao_desembolso_parametros] INT           IDENTITY (1, 1) NOT NULL,
    [nm_reduzido_credor]                   VARCHAR (100) NULL,
    [nr_cnpj_cpf_credor]                   VARCHAR (14)  NULL,
    [cd_unidade_gestora]                   VARCHAR (6)   NULL,
    [cd_gestao]                            VARCHAR (5)   NULL,
    [nr_banco_credor]                      VARCHAR (30)  NULL,
    [nr_agencia_credor]                    VARCHAR (100) NULL,
    [nr_conta_credor]                      VARCHAR (9)   NULL,
    [cd_classificacao]                     VARCHAR (9)   NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_parametros] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_parametros] ASC)
);



