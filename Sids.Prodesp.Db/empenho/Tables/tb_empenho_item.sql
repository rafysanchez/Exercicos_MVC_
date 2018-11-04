CREATE TABLE [empenho].[tb_empenho_item] (
    [id_item]                  INT             IDENTITY (1, 1) NOT NULL,
    [tb_empenho_id_empenho]    INT             NOT NULL,
    [cd_item_servico]          VARCHAR (9)     NULL,
    [cd_unidade_fornecimento]  VARCHAR (5)     NULL,
    [ds_unidade_medida]        VARCHAR (4)     NULL,
    [qt_material_servico]      DECIMAL (18, 2) NULL,
    [ds_justificativa_preco]   VARCHAR (142)   NULL,
    [ds_item_siafem]           VARCHAR (753)   NULL,
    [vr_unitario]              DECIMAL (18)    NULL,
    [vr_preco_total]           DECIMAL (18)    NULL,
    [ds_status_siafisico_item] CHAR (1)        NULL,
    [nr_sequeincia]            INT             NULL,
    CONSTRAINT [PK_tb_empenho_item] PRIMARY KEY CLUSTERED ([id_item] ASC),
    CONSTRAINT [FK_tb_empenho_item_tb_empenho] FOREIGN KEY ([tb_empenho_id_empenho]) REFERENCES [empenho].[tb_empenho] ([id_empenho])
);



