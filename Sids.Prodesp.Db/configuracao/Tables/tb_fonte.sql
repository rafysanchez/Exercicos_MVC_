CREATE TABLE [configuracao].[tb_fonte] (
    [id_fonte] INT           IDENTITY (1, 1) NOT NULL,
    [cd_fonte] VARCHAR (10)  NULL,
    [ds_fonte] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_fonte] PRIMARY KEY CLUSTERED ([id_fonte] ASC)
);



