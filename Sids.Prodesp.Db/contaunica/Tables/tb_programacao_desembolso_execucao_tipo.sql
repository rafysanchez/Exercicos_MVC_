CREATE TABLE [contaunica].[tb_programacao_desembolso_execucao_tipo] (
    [id_programacao_desembolso_execucao_tipo] INT          IDENTITY (1, 1) NOT NULL,
    [ds_programacao_desembolso_execucao_tipo] VARCHAR (50) NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_execucao_tipo] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_execucao_tipo] ASC)
);

