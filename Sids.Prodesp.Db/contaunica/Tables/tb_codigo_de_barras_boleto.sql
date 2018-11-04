CREATE TABLE [contaunica].[tb_codigo_de_barras_boleto] (
    [id_codigo_de_barras_boleto] INT          IDENTITY (1, 1) NOT NULL,
    [id_codigo_de_barras]        INT          NULL,
    [nr_conta_cob1]              VARCHAR (10) NULL,
    [nr_conta_cob2]              VARCHAR (10) NULL,
    [nr_conta_cob3]              VARCHAR (10) NULL,
    [nr_conta_cob4]              VARCHAR (10) NULL,
    [nr_conta_cob5]              VARCHAR (10) NULL,
    [nr_conta_cob6]              VARCHAR (10) NULL,
    [nr_digito]                  CHAR (1)     NULL,
    [nr_conta_cob7]              VARCHAR (20) NULL,
    CONSTRAINT [PK_tb_codigo_de_barras_boleto] PRIMARY KEY CLUSTERED ([id_codigo_de_barras_boleto] ASC),
    CONSTRAINT [FK_tb_codigo_de_barras_boleto_tb_codigo_de_barras] FOREIGN KEY ([id_codigo_de_barras]) REFERENCES [contaunica].[tb_codigo_de_barras] ([id_codigo_de_barras])
);



