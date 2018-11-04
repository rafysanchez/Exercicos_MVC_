CREATE TABLE [seguranca].[tb_perfil_acao] (
    [id_perfil_acao]  INT      IDENTITY (1, 1) NOT NULL,
    [id_perfil]       INT      NULL,
    [id_recurso_acao] INT      NULL,
    [id_acao]         SMALLINT NULL,
    CONSTRAINT [PK_tb_perfil_acao] PRIMARY KEY CLUSTERED ([id_perfil_acao] ASC),
    CONSTRAINT [FK_tb_perfil_acao_tb_acao] FOREIGN KEY ([id_acao]) REFERENCES [seguranca].[tb_acao] ([id_acao]),
    CONSTRAINT [FK_tb_perfil_acao_tb_perfil] FOREIGN KEY ([id_perfil]) REFERENCES [seguranca].[tb_perfil] ([id_perfil]),
    CONSTRAINT [FK_tb_perfil_acao_tb_recurso_acao] FOREIGN KEY ([id_recurso_acao]) REFERENCES [seguranca].[tb_recurso_acao] ([id_recurso_acao])
);



