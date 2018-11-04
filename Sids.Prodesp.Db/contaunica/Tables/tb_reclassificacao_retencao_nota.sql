CREATE TABLE [contaunica].[tb_reclassificacao_retencao_nota] (
    [id_reclassificacao_retencao_nota] INT          IDENTITY (1, 1) NOT NULL,
    [id_reclassificacao_retencao]      INT          NULL,
    [cd_nota]                          VARCHAR (12) NULL,
    [nr_ordem]                         INT          NULL,
    CONSTRAINT [PK_tb_reclassificacao_retencao_nota] PRIMARY KEY CLUSTERED ([id_reclassificacao_retencao_nota] ASC),
    CONSTRAINT [CK_tb_reclassificacao_retencao_nota_nr_ordem] CHECK ([nr_ordem]>=(1) AND [nr_ordem]<=(15)),
    CONSTRAINT [FK_tb_reclassificacao_retencao_nota_tb_reclassificacao_retencao] FOREIGN KEY ([id_reclassificacao_retencao]) REFERENCES [contaunica].[tb_reclassificacao_retencao] ([id_reclassificacao_retencao])
);



