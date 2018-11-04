CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria_mes] (
    [id_mes]                                                    INT             IDENTITY (1, 1) NOT NULL,
    [tb_distribuicao_movimentacao_id_distribuicao_movimentacao] INT             NULL,
    [tb_reducao_suplementacao_id_reducao_suplementacao]         INT             NULL,
    [tb_cancelamento_movimentacao_id_cancelamento_movimentacao] INT             NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [ds_mes]                                                    VARCHAR (9)     NULL,
    [vr_mes]                                                    DECIMAL (18, 2) NULL,
    [cd_unidade_gestora]                                        VARCHAR (10)    NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria_mes] PRIMARY KEY CLUSTERED ([id_mes] ASC)
);





