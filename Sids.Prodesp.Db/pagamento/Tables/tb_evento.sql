CREATE TABLE [pagamento].[tb_evento] (
    [id_evento]            INT          IDENTITY (1, 1) NOT NULL,
    [id_nl_parametrizacao] INT          NOT NULL,
    [id_rap_tipo]          CHAR (1)     NULL,
    [id_documento_tipo]    INT          NOT NULL,
    [nr_evento]            VARCHAR (50) NULL,
    [nr_classificacao]     VARCHAR (50) NULL,
    [ds_entrada_saida]     CHAR (10)    NULL,
    [nr_fonte]             VARCHAR (50) NULL,
    CONSTRAINT [PK_tb_evento] PRIMARY KEY CLUSTERED ([id_evento] ASC),
    CONSTRAINT [FK_tb_evento_tb_nl_parametrizacao] FOREIGN KEY ([id_nl_parametrizacao]) REFERENCES [pagamento].[tb_nl_parametrizacao] ([id_nl_parametrizacao]),
    CONSTRAINT [FK_tb_evento_tb_para_resto_pagar] FOREIGN KEY ([id_rap_tipo]) REFERENCES [contaunica].[tb_para_resto_pagar] ([id_resto_pagar]),
    CONSTRAINT [FK_tb_evento_tb_tipo_documento] FOREIGN KEY ([id_documento_tipo]) REFERENCES [contaunica].[tb_tipo_documento] ([id_tipo_documento])
);







