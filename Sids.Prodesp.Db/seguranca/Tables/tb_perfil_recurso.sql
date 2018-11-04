CREATE TABLE [seguranca].[tb_perfil_recurso] (
    [id_perfil_recurso] INT           IDENTITY (1, 1) NOT NULL,
    [id_perfil]         INT           NULL,
    [id_recurso]        INT           NULL,
    [bl_ativo]          BIT           CONSTRAINT [DF_tb_perfil_recurso_bl_ativo] DEFAULT ((0)) NULL,
    [dt_criacao]        SMALLDATETIME NULL,
    CONSTRAINT [PK_tb_perfil_recurso] PRIMARY KEY CLUSTERED ([id_perfil_recurso] ASC),
    CONSTRAINT [FK_tb_perfil_recurso_tb_perfil] FOREIGN KEY ([id_perfil]) REFERENCES [seguranca].[tb_perfil] ([id_perfil]),
    CONSTRAINT [FK_tb_perfil_recurso_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
);




GO
CREATE NONCLUSTERED INDEX [tb_perfil_recurso_id_perfil]
    ON [seguranca].[tb_perfil_recurso]([id_perfil] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_perfil_recurso_id_recurso]
    ON [seguranca].[tb_perfil_recurso]([id_recurso] ASC);

