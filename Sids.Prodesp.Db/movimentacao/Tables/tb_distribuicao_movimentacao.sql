CREATE TABLE [movimentacao].[tb_distribuicao_movimentacao] (
    [id_distribuicao_movimentacao]                              INT             IDENTITY (1, 1) NOT NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [nr_siafem]                                                 VARCHAR (15)    NULL,
    [valor]                                                     DECIMAL (18, 2) NULL,
    [tb_fonte_id_fonte]                                         VARCHAR (10)    NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (10)    NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)    NULL,
    [evento]                                                    VARCHAR (10)    NULL,
    [nr_categoria_gasto]                                        VARCHAR (10)    NULL,
    [eventoNC]                                                  VARCHAR (10)    NULL,
    [ds_observacao]                                             VARCHAR (77)    NULL,
    [ds_observacao2]                                            VARCHAR (77)    NULL,
    [ds_observacao3]                                            VARCHAR (77)    NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)        NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140)   NULL,
    [fg_transmitido_siafem]                                     CHAR (1)        NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140)   NULL,
    CONSTRAINT [PK_tb_distribuicao_movimentacao] PRIMARY KEY CLUSTERED ([id_distribuicao_movimentacao] ASC)
);











