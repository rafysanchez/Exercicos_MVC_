CREATE TABLE [contaunica].[tb_codigo_de_barras] (
    [id_codigo_de_barras]   INT             NOT NULL,
    [id_tipo_de_boleto]     INT             NULL,
    [id_lista_de_boletos]   INT             NULL,
    [vr_boleto]             DECIMAL (18, 2) NULL,
    [bl_transmitido_siafem] BIT             NULL,
    CONSTRAINT [PK_tb_codigo_de_barras] PRIMARY KEY CLUSTERED ([id_codigo_de_barras] ASC),
    CONSTRAINT [FK_tb_codigo_de_barras_tb_lista_de_boletos] FOREIGN KEY ([id_lista_de_boletos]) REFERENCES [contaunica].[tb_lista_de_boletos] ([id_lista_de_boletos]),
    CONSTRAINT [FK_tb_codigo_de_barras_tb_tipo_de_boleto] FOREIGN KEY ([id_tipo_de_boleto]) REFERENCES [contaunica].[tb_tipo_de_boleto] ([id_tipo_de_boleto])
);



