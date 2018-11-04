CREATE TABLE [pagamento].[tb_rap_inscricao_nota] (
    [id_rap_inscricao_nota]             INT          IDENTITY (1, 1) NOT NULL,
    [tb_rap_inscricao_id_rap_inscricao] INT          NULL,
    [cd_nota]                           VARCHAR (12) NULL,
    [nr_ordem]                          INT          NULL,
    CONSTRAINT [PK_tb_rap_inscricao_nota] PRIMARY KEY CLUSTERED ([id_rap_inscricao_nota] ASC),
    CONSTRAINT [FK_tb_rap_inscricao_nota_tb_rap_inscricao] FOREIGN KEY ([tb_rap_inscricao_id_rap_inscricao]) REFERENCES [pagamento].[tb_rap_inscricao] ([id_rap_inscricao])
);



