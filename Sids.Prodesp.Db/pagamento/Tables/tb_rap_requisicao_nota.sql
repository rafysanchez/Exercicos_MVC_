CREATE TABLE [pagamento].[tb_rap_requisicao_nota] (
    [id_rap_requisicao_nota]              INT          IDENTITY (1, 1) NOT NULL,
    [tb_rap_requisicao_id_rap_requisicao] INT          NULL,
    [cd_nota]                             VARCHAR (12) NULL,
    [nr_ordem]                            INT          NULL,
    CONSTRAINT [PK_tb_rap_requisicao_nota] PRIMARY KEY CLUSTERED ([id_rap_requisicao_nota] ASC),
    CONSTRAINT [FK_tb_rap_requisicao_nota_tb_rap_requisicao] FOREIGN KEY ([tb_rap_requisicao_id_rap_requisicao]) REFERENCES [pagamento].[tb_rap_requisicao] ([id_rap_requisicao])
);



