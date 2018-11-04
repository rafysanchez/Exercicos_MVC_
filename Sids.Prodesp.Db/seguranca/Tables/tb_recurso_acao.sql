CREATE TABLE [seguranca].[tb_recurso_acao] (
    [id_recurso_acao] INT      IDENTITY (1, 1) NOT NULL,
    [id_recurso]      INT      NULL,
    [id_acao]         SMALLINT NULL,
    CONSTRAINT [PK_tb_recurso_acao] PRIMARY KEY CLUSTERED ([id_recurso_acao] ASC),
    CONSTRAINT [FK_tb_recurso_acao_tb_acao] FOREIGN KEY ([id_acao]) REFERENCES [seguranca].[tb_acao] ([id_acao]),
    CONSTRAINT [FK_tb_recurso_acao_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
);



