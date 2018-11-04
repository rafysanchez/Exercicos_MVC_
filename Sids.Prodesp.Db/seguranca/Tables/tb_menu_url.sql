CREATE TABLE [seguranca].[tb_menu_url] (
    [id_menu_url]   INT          IDENTITY (1, 1) NOT NULL,
    [ds_area]       VARCHAR (50) NULL,
    [ds_controller] VARCHAR (50) NULL,
    [ds_action]     VARCHAR (50) NULL,
    [ds_url]        VARCHAR (50) NULL,
    CONSTRAINT [PK_tb_menu_url] PRIMARY KEY CLUSTERED ([id_menu_url] ASC)
);



