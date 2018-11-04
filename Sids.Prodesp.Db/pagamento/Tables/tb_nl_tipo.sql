CREATE TABLE [pagamento].[tb_nl_tipo] (
    [id_nl_tipo] INT           IDENTITY (1, 1) NOT NULL,
    [ds_nl_tipo] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tb_nl_tipo] PRIMARY KEY CLUSTERED ([id_nl_tipo] ASC)
);



