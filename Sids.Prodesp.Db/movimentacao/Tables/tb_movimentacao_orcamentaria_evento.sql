CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria_evento] (
    [id_evento]                                                 INT          IDENTITY (1, 1) NOT NULL,
    [cd_evento]                                                 VARCHAR (6)  NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT          NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT          NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT          NULL,
    [nr_agrupamento]                                            INT          NULL,
    [nr_seq]                                                    INT          NULL,
    [cd_inscricao_evento]                                       VARCHAR (10) NULL,
    [cd_classificacao]                                          VARCHAR (10) NULL,
    [cd_fonte]                                                  VARCHAR (10) NULL,
    [rec_despesa]                                               VARCHAR (8)  NULL,
    [vr_evento]                                                 INT          NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria_evento] PRIMARY KEY CLUSTERED ([id_evento] ASC)
);

