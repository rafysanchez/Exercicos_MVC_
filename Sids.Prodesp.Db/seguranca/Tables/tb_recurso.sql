CREATE TABLE [seguranca].[tb_recurso] (
    [id_recurso]  INT            IDENTITY (1, 1) NOT NULL,
    [no_recurso]  VARCHAR (100)  NOT NULL,
    [ds_recurso]  VARCHAR (1000) NULL,
    [ds_keywords] VARCHAR (100)  NULL,
    [ds_url]      VARCHAR (2048) NOT NULL,
    [bl_publico]  BIT            CONSTRAINT [DF_tb_recurso_bl_publico] DEFAULT ((0)) NULL,
    [bl_ativo]    BIT            CONSTRAINT [DF_tb_recurso_bl_ativo] DEFAULT ((0)) NOT NULL,
    [dt_criacao]  DATETIME       NOT NULL,
    [id_menu_url] INT            NULL,
    CONSTRAINT [pk_tb_recurso] PRIMARY KEY CLUSTERED ([id_recurso] ASC)
);

