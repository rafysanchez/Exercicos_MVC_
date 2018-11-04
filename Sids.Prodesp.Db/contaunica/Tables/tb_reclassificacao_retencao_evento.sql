CREATE TABLE [contaunica].[tb_reclassificacao_retencao_evento] (
    [id_reclassificacao_retencao_evento] INT          IDENTITY (1, 1) NOT NULL,
    [id_reclassificacao_retencao]        INT          NULL,
    [cd_fonte]                           VARCHAR (10) NULL,
    [cd_evento]                          VARCHAR (6)  NULL,
    [cd_classificacao]                   VARCHAR (9)  NULL,
    [ds_inscricao]                       VARCHAR (22) NULL,
    [vl_evento]                          INT          NULL,
    [id_resto_pagar]                     CHAR (1)     NULL,
    CONSTRAINT [PK_tb_reclassificacao_retencao_evento] PRIMARY KEY CLUSTERED ([id_reclassificacao_retencao_evento] ASC),
    CONSTRAINT [FK_tb_reclassificacao_retencao_evento_tb_reclassificacao_retencao] FOREIGN KEY ([id_reclassificacao_retencao]) REFERENCES [contaunica].[tb_reclassificacao_retencao] ([id_reclassificacao_retencao]),
    CONSTRAINT [FK_tb_reclassificacao_retencao_evento_tb_tb_para_resto_pagar] FOREIGN KEY ([id_resto_pagar]) REFERENCES [contaunica].[tb_para_resto_pagar] ([id_resto_pagar])
);





