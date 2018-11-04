CREATE TABLE [seguranca].[tb_usuario] (
    [id_usuario]                   INT           IDENTITY (1, 1) NOT NULL,
    [id_regional]                  SMALLINT      NULL,
    [id_sistema]                   SMALLINT      NULL,
    [id_area]                      SMALLINT      NULL,
    [ds_email]                     VARCHAR (100) NULL,
    [ds_login]                     VARCHAR (100) NULL,
    [ds_senha]                     VARCHAR (200) NULL,
    [bl_senha_expirada]            BIT           CONSTRAINT [DF_tb_usuario_bl_senha_expirada] DEFAULT ((0)) NULL,
    [dt_expiracao_senha]           SMALLDATETIME NULL,
    [dt_ultimo_acesso]             SMALLDATETIME NULL,
    [nr_tentativa_login_invalidas] INT           CONSTRAINT [DF_tb_usuario_nr_tentativa_login_invalidas] DEFAULT ((0)) NULL,
    [bl_bloqueado]                 BIT           CONSTRAINT [DF_tb_usuario_bl_bloqueado] DEFAULT ((0)) NULL,
    [bl_alterar_senha]             BIT           CONSTRAINT [DF_tb_usuario_bl_alterar_senha] DEFAULT ((0)) NULL,
    [bl_ativo]                     BIT           CONSTRAINT [DF_tb_usuario_bl_ativo] DEFAULT ((0)) NULL,
    [dt_criacao]                   SMALLDATETIME NULL,
    [ds_senha_siafem]              VARCHAR (200) NULL,
    [nr_cpf]                       VARCHAR (11)  NULL,
    [ds_nome]                      VARCHAR (100) NULL,
    [ds_token]                     VARCHAR (MAX) NULL,
    [bl_acesso_siafem]             BIT           NULL,
    [bl_senha_siafem_expirada]     BIT           NULL,
    [bl_alterar_senha_siafem]      BIT           NULL,
    [cd_impressora132]             VARCHAR (4)   NULL,
    [cd_impressora80]              VARCHAR (4)   NULL,
    CONSTRAINT [PK_tb_usuario] PRIMARY KEY CLUSTERED ([id_usuario] ASC),
    CONSTRAINT [FK_tb_usuario_tb_area] FOREIGN KEY ([id_area]) REFERENCES [seguranca].[tb_area] ([id_area]),
    CONSTRAINT [FK_tb_usuario_tb_regional] FOREIGN KEY ([id_regional]) REFERENCES [seguranca].[tb_regional] ([id_regional]),
    CONSTRAINT [FK_tb_usuario_tb_sistema] FOREIGN KEY ([id_sistema]) REFERENCES [seguranca].[tb_sistema] ([id_sistema])
);




GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_ds_nome]
    ON [seguranca].[tb_usuario]([ds_nome] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_ds_login]
    ON [seguranca].[tb_usuario]([ds_login] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_nr_cpf]
    ON [seguranca].[tb_usuario]([nr_cpf] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_ds_email]
    ON [seguranca].[tb_usuario]([ds_email] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_id_regional]
    ON [seguranca].[tb_usuario]([id_regional] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_id_sistema]
    ON [seguranca].[tb_usuario]([id_sistema] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_usuario_id_area]
    ON [seguranca].[tb_usuario]([id_area] ASC);

