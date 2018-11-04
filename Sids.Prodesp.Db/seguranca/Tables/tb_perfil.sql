CREATE TABLE [seguranca].[tb_perfil] (
    [id_perfil]        INT           IDENTITY (1, 1) NOT NULL,
    [ds_perfil]        VARCHAR (100) NULL,
    [ds_detalhe]       VARCHAR (200) NULL,
    [bl_ativo]         BIT           CONSTRAINT [DF_tb_perfil_bl_ativo] DEFAULT ((0)) NULL,
    [dt_criacao]       SMALLDATETIME NULL,
    [bl_administrador] BIT           NULL,
    CONSTRAINT [PK_tb_perfil] PRIMARY KEY CLUSTERED ([id_perfil] ASC)
);




GO
CREATE NONCLUSTERED INDEX [ix_tb_perfil_ds_perfil]
    ON [seguranca].[tb_perfil]([ds_perfil] ASC);

