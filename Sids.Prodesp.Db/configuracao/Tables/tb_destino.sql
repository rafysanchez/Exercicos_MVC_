CREATE TABLE [configuracao].[tb_destino] (
    [id_destino] INT           IDENTITY (1, 1) NOT NULL,
    [cd_destino] CHAR (2)      NOT NULL,
    [ds_destino] VARCHAR (140) NOT NULL,
    CONSTRAINT [PK_tb_destino] PRIMARY KEY CLUSTERED ([id_destino] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UNQ_tb_destino_cd_destino]
    ON [configuracao].[tb_destino]([cd_destino] ASC);

