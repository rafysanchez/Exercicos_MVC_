CREATE TABLE [configuracao].[tb_programa] (
    [id_programa]       INT          IDENTITY (1, 1) NOT NULL,
    [cd_ptres]          VARCHAR (6)  NULL,
    [cd_cfp]            VARCHAR (16) NULL,
    [ds_programa]       VARCHAR (60) NULL,
    [nr_ano_referencia] INT          NULL,
    CONSTRAINT [PK_tb_programa] PRIMARY KEY CLUSTERED ([id_programa] ASC)
);



