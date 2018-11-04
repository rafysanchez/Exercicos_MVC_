CREATE TABLE [movimentacao].[tb_movimentacao_orcamentaria] (
    [id_movimentacao_orcamentaria]                                        INT           IDENTITY (1, 1) NOT NULL,
    [nr_agrupamento_movimentacao]                                         INT           NULL,
    [nr_siafem]                                                           VARCHAR (15)  NULL,
    [tb_regional_id_regional]                                             INT           NULL,
    [tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao]       INT           NULL,
    [tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria] INT           NULL,
    [cd_unidade_gestora_emitente]                                         VARCHAR (10)  NULL,
    [cd_gestao_emitente]                                                  VARCHAR (10)  NULL,
    [nr_ano_exercicio]                                                    INT           NULL,
    [fg_transmitido_siafem]                                               CHAR (1)      NULL,
    [bl_transmitido_siafem]                                               BIT           NULL,
    [dt_trasmitido_siafem]                                                DATETIME      NULL,
    [fg_transmitido_prodesp]                                              CHAR (1)      NULL,
    [bl_transmitido_prodesp]                                              BIT           NULL,
    [dt_trasmitido_prodesp]                                               DATETIME      NULL,
    [ds_msgRetornoProdesp]                                                VARCHAR (140) NULL,
    [ds_msgRetornoSiafem]                                                 VARCHAR (140) NULL,
    [bl_cadastro_completo]                                                BIT           NULL,
    [dt_cadastro]                                                         DATETIME      NULL,
    [bl_transmitir_siafem]                                                BIT           NULL,
    [bl_transmitir_prodesp]                                               BIT           NULL,
    [tb_programa_id_programa]                                             INT           NULL,
    [tb_fonte_id_fonte]                                                   INT           NULL,
    [tb_estrutura_id_estrutura]                                           INT           NULL,
    CONSTRAINT [PK_tb_movimentacao_orcamentaria] PRIMARY KEY CLUSTERED ([id_movimentacao_orcamentaria] ASC)
);







