CREATE TABLE [contaunica].[tb_tipo_de_boleto] (
    [id_tipo_de_boleto] INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_de_boleto] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_de_boleto] PRIMARY KEY CLUSTERED ([id_tipo_de_boleto] ASC)
);



