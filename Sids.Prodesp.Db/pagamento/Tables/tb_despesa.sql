CREATE TABLE [pagamento].[tb_despesa] (
    [id_despesa]           INT IDENTITY (1, 1) NOT NULL,
    [id_despesa_tipo]      INT NOT NULL,
    [id_nl_parametrizacao] INT NOT NULL,
    CONSTRAINT [PK_tb_despesa] PRIMARY KEY CLUSTERED ([id_despesa] ASC),
    CONSTRAINT [FK_tb_despesa_tb_despesa_tipo] FOREIGN KEY ([id_despesa_tipo]) REFERENCES [pagamento].[tb_despesa_tipo] ([id_despesa_tipo]),
    CONSTRAINT [FK_tb_despesa_tb_nl_parametrizacao] FOREIGN KEY ([id_nl_parametrizacao]) REFERENCES [pagamento].[tb_nl_parametrizacao] ([id_nl_parametrizacao])
);

