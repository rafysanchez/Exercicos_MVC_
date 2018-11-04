CREATE TABLE [seguranca].[tb_regional] (
    [id_regional] SMALLINT      IDENTITY (1, 1) NOT NULL,
    [ds_regional] VARCHAR (100) NOT NULL,
    [cd_uge]      VARCHAR (6)   NULL,
    CONSTRAINT [PK_tb_regional] PRIMARY KEY CLUSTERED ([id_regional] ASC)
);



