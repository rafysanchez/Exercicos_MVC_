CREATE TABLE [seguranca].[tb_menu_item] (
    [id_menu_item] INT           IDENTITY (1, 1) NOT NULL,
    [id_menu]      INT           NOT NULL,
    [id_recurso]   INT           NOT NULL,
    [ds_rotulo]    VARCHAR (100) NOT NULL,
    [ds_abrir_em]  VARCHAR (10)  CONSTRAINT [DF_tb_menu_item_ds_abrir_em] DEFAULT ('_self') NULL,
    [nr_ordem]     INT           NULL,
    [bl_ativo]     BIT           CONSTRAINT [DF_tb_menu_item_bl_ativo] DEFAULT ((0)) NOT NULL,
    [dt_criacao]   DATETIME      NOT NULL,
    CONSTRAINT [PK_tb_menu_item] PRIMARY KEY CLUSTERED ([id_menu_item] ASC),
    CONSTRAINT [FK_tb_menu_item_tb_menu] FOREIGN KEY ([id_menu]) REFERENCES [seguranca].[tb_menu] ([id_menu]),
    CONSTRAINT [FK_tb_menu_item_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
);



