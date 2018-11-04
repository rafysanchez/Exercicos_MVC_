CREATE TABLE [pagamento].[tb_subempenho_cancelamento_item] (
    [id_subempenho_cancelamento_item]                       INT             IDENTITY (1, 1) NOT NULL,
    [tb_subempenho_cancelamento_id_subempenho_cancelamento] INT             NULL,
    [cd_servico]                                            VARCHAR (10)    NULL,
    [cd_unidade_fornecimento]                               VARCHAR (5)     NULL,
    [qt_material_servico]                                   DECIMAL (12, 3) NULL,
    [cd_status_siafisico]                                   CHAR (1)        NULL,
    [nr_sequencia]                                          INT             NULL,
    [transmitir]                                            BIT             NULL,
    CONSTRAINT [PK_tb_subempenho_cancelamento_item] PRIMARY KEY CLUSTERED ([id_subempenho_cancelamento_item] ASC),
    CONSTRAINT [FK_tb_subempenho_cancelamento_item_tb_subempenho_cancelamento] FOREIGN KEY ([tb_subempenho_cancelamento_id_subempenho_cancelamento]) REFERENCES [pagamento].[tb_subempenho_cancelamento] ([id_subempenho_cancelamento])
);



