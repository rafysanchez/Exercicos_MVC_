CREATE TABLE [pagamento].[tb_subempenho_cancelamento_nota] (
    [id_subempenho_cancelamento_nota]                       INT          IDENTITY (1, 1) NOT NULL,
    [tb_subempenho_cancelamento_id_subempenho_cancelamento] INT          NULL,
    [cd_nota]                                               VARCHAR (12) NULL,
    [nr_ordem]                                              INT          NULL,
    CONSTRAINT [PK_tb_subempenho_cancelamento_nota] PRIMARY KEY CLUSTERED ([id_subempenho_cancelamento_nota] ASC),
    CONSTRAINT [FK_tb_subempenho_cancelamento_nota_tb_subempenho_cancelamento] FOREIGN KEY ([tb_subempenho_cancelamento_id_subempenho_cancelamento]) REFERENCES [pagamento].[tb_subempenho_cancelamento] ([id_subempenho_cancelamento])
);



