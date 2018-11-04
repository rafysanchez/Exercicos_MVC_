CREATE TABLE [movimentacao].[tb_tipo_documento_movimentacao] (
    [id_tipo_documento_movimentacao] INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_documento_movimentacao] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_documento_movimentacao] PRIMARY KEY CLUSTERED ([id_tipo_documento_movimentacao] ASC)
);

