CREATE TABLE [pagamento].[tb_cenario_tipo] (
    [id_cenario_tipo] INT           NOT NULL,
    [ds_cenario_tipo] VARCHAR (140) NOT NULL,
    [nm_servico]      VARCHAR (140) NOT NULL,
    [fl_siafem]       BIT           NOT NULL,
    CONSTRAINT [PK_tb_cenario_tipo] PRIMARY KEY CLUSTERED ([id_cenario_tipo] ASC)
);



