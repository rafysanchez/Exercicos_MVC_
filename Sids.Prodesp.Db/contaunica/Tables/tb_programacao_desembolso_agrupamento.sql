CREATE TABLE [contaunica].[tb_programacao_desembolso_agrupamento] (
    [id_programacao_desembolso_agrupamento]       INT           IDENTITY (1, 1) NOT NULL,
    [id_programacao_desembolso]                   INT           NULL,
    [id_tipo_documento]                           INT           NULL,
    [nr_agrupamento]                              INT           NULL,
    [vl_valor]                                    DECIMAL (18)  NULL,
    [nr_programacao_desembolso]                   VARCHAR (11)  NULL,
    [nr_sequencia]                                INT           NULL,
    [id_regional]                                 SMALLINT      NULL,
    [nr_documento]                                VARCHAR (19)  NULL,
    [cd_unidade_gestora]                          VARCHAR (6)   NULL,
    [cd_gestao]                                   VARCHAR (5)   NULL,
    [nr_nl_referencia]                            VARCHAR (11)  NULL,
    [nr_cnpj_cpf_credor]                          VARCHAR (15)  NULL,
    [cd_gestao_credor]                            VARCHAR (140) NULL,
    [nr_banco_credor]                             VARCHAR (30)  NULL,
    [nr_agencia_credor]                           VARCHAR (10)  NULL,
    [nr_conta_credor]                             VARCHAR (15)  NULL,
    [nm_reduzido_credor]                          VARCHAR (14)  NULL,
    [nr_cnpj_cpf_pgto]                            VARCHAR (15)  NULL,
    [cd_gestao_pgto]                              VARCHAR (140) NULL,
    [nr_banco_pgto]                               VARCHAR (30)  NULL,
    [nr_agencia_pgto]                             VARCHAR (10)  NULL,
    [nr_conta_pgto]                               VARCHAR (15)  NULL,
    [nr_processo]                                 VARCHAR (15)  NULL,
    [ds_finalidade]                               VARCHAR (40)  NULL,
    [cd_fonte]                                    VARCHAR (10)  NULL,
    [cd_evento]                                   VARCHAR (6)   NULL,
    [cd_classificacao]                            VARCHAR (9)   NULL,
    [ds_inscricao]                                VARCHAR (22)  NULL,
    [cd_despesa]                                  VARCHAR (2)   NULL,
    [dt_emissao]                                  DATE          NULL,
    [dt_vencimento]                               DATE          NULL,
    [nr_lista_anexo]                              VARCHAR (11)  NULL,
    [cd_transmissao_status_siafem_siafisico]      CHAR (1)      CONSTRAINT [DF_tb_programacao_desembolso_agrupamento_cd_transmissao_status_siafem_siafisico] DEFAULT ('N') NULL,
    [fl_transmissao_transmitido_siafem_siafisico] BIT           NULL,
    [dt_transmissao_transmitido_siafem_siafisico] DATE          NULL,
    [ds_msg_retorno]                              VARCHAR (256) NULL,
    [nr_documento_gerador]                        VARCHAR (22)  NULL,
    [dt_cadastro]                                 DATE          NULL,
    [ds_causa_cancelamento]                       VARCHAR (200) NULL,
    [bl_bloqueio]                                 BIT           NULL,
    [bl_cancelado]                                BIT           NULL,
    [rec_despesa]                                 VARCHAR (8)   NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_agrupamento] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_agrupamento] ASC),
    CONSTRAINT [FK_tb_programacao_desembolso_agrupamento_tb_programacao_desembolso] FOREIGN KEY ([id_programacao_desembolso]) REFERENCES [contaunica].[tb_programacao_desembolso] ([id_programacao_desembolso])
);







