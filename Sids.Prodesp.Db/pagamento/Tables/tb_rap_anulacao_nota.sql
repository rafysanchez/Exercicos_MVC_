CREATE TABLE [pagamento].[tb_rap_anulacao_nota] (
    [id_rap_anulacao_nota]            INT          IDENTITY (1, 1) NOT NULL,
    [tb_rap_anulacao_id_rap_anulacao] INT          NULL,
    [cd_nota]                         VARCHAR (12) NULL,
    [nr_ordem]                        INT          NULL,
    CONSTRAINT [PK_tb_rap_anulacao_nota] PRIMARY KEY CLUSTERED ([id_rap_anulacao_nota] ASC),
    CONSTRAINT [FK_tb_rap_anulacao_nota_tb_rap_anulacao] FOREIGN KEY ([tb_rap_anulacao_id_rap_anulacao]) REFERENCES [pagamento].[tb_rap_anulacao] ([id_rap_anulacao])
);



