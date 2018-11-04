CREATE TABLE [pagamento].[tb_origem] (
    [id_origem] INT          IDENTITY (1, 1) NOT NULL,
    [ds_origem] VARCHAR (50) NULL,
    CONSTRAINT [PK_tb_origem] PRIMARY KEY CLUSTERED ([id_origem] ASC)
);

