CREATE TABLE [pagamento].[tb_subempenho_nota] (
    [id_subempenho_nota]          INT          IDENTITY (1, 1) NOT NULL,
    [tb_subempenho_id_subempenho] INT          NULL,
    [cd_nota]                     VARCHAR (12) NULL,
    [nr_ordem]                    INT          NULL,
    CONSTRAINT [PK_tb_subempenho_nota] PRIMARY KEY CLUSTERED ([id_subempenho_nota] ASC),
    CONSTRAINT [CK_tb_subempenho_nota_nr_ordem] CHECK ([nr_ordem]>=(1) AND [nr_ordem]<=(15)),
    CONSTRAINT [FK_tb_subempenho_nota_tb_subempenho] FOREIGN KEY ([tb_subempenho_id_subempenho]) REFERENCES [pagamento].[tb_subempenho] ([id_subempenho])
);



