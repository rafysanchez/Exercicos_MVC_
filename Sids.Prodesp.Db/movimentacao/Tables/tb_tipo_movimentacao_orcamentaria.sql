CREATE TABLE [movimentacao].[tb_tipo_movimentacao_orcamentaria] (
    [id_tipo_movimentacao_orcamentaria]  INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_movimentacao_orcamentariao] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_movimentacao_orcamentaria] PRIMARY KEY CLUSTERED ([id_tipo_movimentacao_orcamentaria] ASC)
);

