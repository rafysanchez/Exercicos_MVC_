CREATE TABLE [seguranca].[tb_menu] (
    [id_menu]    INT           IDENTITY (1, 1) NOT NULL,
    [id_recurso] INT           NULL,
    [ds_menu]    VARCHAR (100) NOT NULL,
    [nr_ordem]   INT           NULL,
    [bl_ativo]   BIT           CONSTRAINT [DF_tb_menu_bl_ativo] DEFAULT ((0)) NOT NULL,
    [dt_criacao] DATETIME      NOT NULL,
    CONSTRAINT [pk_tb_menu] PRIMARY KEY CLUSTERED ([id_menu] ASC)
);

