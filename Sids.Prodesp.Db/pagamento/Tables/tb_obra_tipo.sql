CREATE TABLE [pagamento].[tb_obra_tipo] (
    [id_obra_tipo] INT           IDENTITY (1, 1) NOT NULL,
    [ds_obra_tipo] VARCHAR (140) NULL,
    CONSTRAINT [PK_tb_obra_tipo] PRIMARY KEY CLUSTERED ([id_obra_tipo] ASC)
);



