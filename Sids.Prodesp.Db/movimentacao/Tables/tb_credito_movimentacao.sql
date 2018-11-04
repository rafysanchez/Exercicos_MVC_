CREATE TABLE [movimentacao].[tb_credito_movimentacao] (
    [id_nota_credito]                                           INT             IDENTITY (1, 1) NOT NULL,
    [tb_programa_id_programa]                                   INT             NULL,
    [tb_fonte_id_fonte]                                         INT             NULL,
    [tb_estrutura_id_estrutura]                                 INT             NULL,
    [tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] INT             NULL,
    [nr_agrupamento]                                            INT             NULL,
    [nr_seq]                                                    INT             NULL,
    [cd_candis]                                                 CHAR (1)        NULL,
    [nr_siafem]                                                 VARCHAR (15)    NULL,
    [vr_credito]                                                DECIMAL (18, 2) NULL,
    [cd_unidade_gestora_emitente]                               VARCHAR (15)    NULL,
    [cd_unidade_gestora_favorecido]                             VARCHAR (15)    NULL,
    [cd_uo]                                                     VARCHAR (10)    NULL,
    [plano_interno]                                             VARCHAR (10)    NULL,
    [eventoNC]                                                  VARCHAR (10)    NULL,
    [cd_gestao_favorecido]                                      VARCHAR (10)    NULL,
    [cd_ugo]                                                    VARCHAR (10)    NULL,
    [fonte_recurso]                                             VARCHAR (10)    NULL,
    [ds_observacao]                                             VARCHAR (77)    NULL,
    [ds_observacao2]                                            VARCHAR (77)    NULL,
    [ds_observacao3]                                            VARCHAR (77)    NULL,
    [fg_transmitido_prodesp]                                    CHAR (1)        NULL,
    [ds_msgRetornoProdesp]                                      VARCHAR (140)   NULL,
    [fg_transmitido_siafem]                                     CHAR (1)        NULL,
    [ds_msgRetornoSiafem]                                       VARCHAR (140)   NULL,
    CONSTRAINT [PK_tb_credito_movimentacao] PRIMARY KEY CLUSTERED ([id_nota_credito] ASC)
);















