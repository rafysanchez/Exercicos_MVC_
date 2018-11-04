CREATE TABLE [seguranca].[tb_perfil_usuario] (
    [id_perfil_usuario] INT           IDENTITY (1, 1) NOT NULL,
    [id_perfil]         INT           NULL,
    [id_usuario]        INT           NULL,
    [bl_ativo]          BIT           NULL,
    [dt_criacao]        SMALLDATETIME NULL,
    CONSTRAINT [PK_tb_perfil_usuario] PRIMARY KEY CLUSTERED ([id_perfil_usuario] ASC),
    CONSTRAINT [FK_tb_perfil_usuario_tb_perfil] FOREIGN KEY ([id_perfil]) REFERENCES [seguranca].[tb_perfil] ([id_perfil]),
    CONSTRAINT [FK_tb_perfil_usuario_tb_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [seguranca].[tb_usuario] ([id_usuario])
);




GO
CREATE NONCLUSTERED INDEX [tb_perfil_usuario_id_perfil]
    ON [seguranca].[tb_perfil_usuario]([id_perfil] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_perfil_usuario_id_usuario]
    ON [seguranca].[tb_perfil_usuario]([id_usuario] ASC);

