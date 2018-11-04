CREATE TABLE [pagamento].[tb_nl_parametrizacao] (
    [id_nl_parametrizacao]             INT           IDENTITY (1, 1) NOT NULL,
    [id_nl_tipo]                       INT           NOT NULL,
    [ds_observacao]                    VARCHAR (228) NULL,
    [bl_transmitir]                    BIT           NOT NULL,
    [nr_favorecida_cnpjcpfug]          VARCHAR (15)  NULL,
    [nr_favorecida_gestao]             INT           NULL,
    [nr_unidade_gestora]               INT           NULL,
    [id_parametrizacao_forma_gerar_nl] INT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tb_nl_parametrizacao] PRIMARY KEY CLUSTERED ([id_nl_parametrizacao] ASC),
    CONSTRAINT [FK_tb_nl_parametrizacao_tb_nl_tipo] FOREIGN KEY ([id_nl_tipo]) REFERENCES [pagamento].[tb_nl_tipo] ([id_nl_tipo]),
    CONSTRAINT [FK_tb_nl_parametrizacao_tb_parametrizacao_forma_gerar_nl] FOREIGN KEY ([id_parametrizacao_forma_gerar_nl]) REFERENCES [pagamento].[tb_parametrizacao_forma_gerar_nl] ([id_parametrizacao_forma_gerar_nl])
);







