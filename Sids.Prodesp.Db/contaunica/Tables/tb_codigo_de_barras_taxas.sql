CREATE TABLE [contaunica].[tb_codigo_de_barras_taxas] (
    [id_codigo_de_barras_taxas] INT          IDENTITY (1, 1) NOT NULL,
    [id_codigo_de_barras]       INT          NULL,
    [nr_conta1]                 VARCHAR (15) NULL,
    [nr_conta2]                 VARCHAR (15) NULL,
    [nr_conta3]                 VARCHAR (15) NULL,
    [nr_conta4]                 VARCHAR (15) NULL,
    CONSTRAINT [PK_tb_codigo_de_barras_taxas] PRIMARY KEY CLUSTERED ([id_codigo_de_barras_taxas] ASC),
    CONSTRAINT [FK_tb_codigo_de_barras_taxas_tb_codigo_de_barras] FOREIGN KEY ([id_codigo_de_barras]) REFERENCES [contaunica].[tb_codigo_de_barras] ([id_codigo_de_barras])
);



