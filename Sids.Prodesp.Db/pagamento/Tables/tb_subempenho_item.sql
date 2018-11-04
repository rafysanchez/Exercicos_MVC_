CREATE TABLE [pagamento].[tb_subempenho_item] (
    [id_subempenho_item]          INT             IDENTITY (1, 1) NOT NULL,
    [tb_subempenho_id_subempenho] INT             NULL,
    [cd_servico]                  VARCHAR (10)    NULL,
    [cd_unidade_fornecimento]     VARCHAR (5)     NULL,
    [qt_material_servico]         DECIMAL (12, 3) NULL,
    [cd_status_siafisico]         CHAR (1)        CONSTRAINT [DF_tb_subempenho_item_cd_status_siafisico] DEFAULT ('N') NULL,
    [nr_sequencia]                INT             NULL,
    [transmitir]                  BIT             NULL,
    [vl_valor]                    DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tb_subempenho_item] PRIMARY KEY CLUSTERED ([id_subempenho_item] ASC),
    CONSTRAINT [CK_tb_subempenho_item_cd_status_siafisico] CHECK ([cd_status_siafisico]='N' OR [cd_status_siafisico]='E' OR [cd_status_siafisico]='S'),
    CONSTRAINT [FK_tb_subempenho_item_tb_subempenho] FOREIGN KEY ([tb_subempenho_id_subempenho]) REFERENCES [pagamento].[tb_subempenho] ([id_subempenho])
);



