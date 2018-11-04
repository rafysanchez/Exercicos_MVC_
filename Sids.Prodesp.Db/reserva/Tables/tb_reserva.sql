CREATE TABLE [reserva].[tb_reserva] (
    [id_reserva]                        INT           IDENTITY (1, 1) NOT NULL,
    [id_fonte]                          INT           NULL,
    [id_estrutura]                      INT           NULL,
    [id_programa]                       INT           NULL,
    [id_tipo_reserva]                   INT           NULL,
    [id_regional]                       SMALLINT      NULL,
    [cd_contrato]                       VARCHAR (13)  NULL,
    [cd_processo]                       VARCHAR (50)  NULL,
    [nr_reserva_prodesp]                VARCHAR (9)   NULL,
    [nr_reserva_siafem_siafisico]       VARCHAR (11)  NULL,
    [cd_obra]                           INT           NULL,
    [nr_oc]                             VARCHAR (12)  NULL,
    [cd_ugo]                            VARCHAR (6)   NULL,
    [cd_uo]                             VARCHAR (5)   NULL,
    [cd_evento]                         INT           NULL,
    [nr_ano_exercicio]                  INT           NULL,
    [nr_ano_referencia_reserva]         INT           NULL,
    [cd_origem_recurso]                 VARCHAR (9)   NULL,
    [cd_destino_recurso]                VARCHAR (2)   NULL,
    [ds_observacao]                     VARCHAR (308) NULL,
    [fg_transmitido_prodesp]            BIT           NULL,
    [fg_transmitido_siafem]             BIT           NULL,
    [fg_transmitido_siafisico]          BIT           NULL,
    [bl_transmitir_prodesp]             BIT           NULL,
    [bl_transmitir_siafem]              BIT           NULL,
    [bl_transmitir_siafisico]           BIT           NULL,
    [ds_autorizado_supra_folha]         VARCHAR (4)   NULL,
    [cd_especificacao_despesa]          VARCHAR (3)   NULL,
    [ds_especificacao_despesa]          VARCHAR (719) NULL,
    [cd_autorizado_assinatura]          VARCHAR (5)   NULL,
    [cd_autorizado_grupo]               VARCHAR (1)   NULL,
    [cd_autorizado_orgao]               VARCHAR (2)   NULL,
    [nm_autorizado_assinatura]          VARCHAR (55)  NULL,
    [ds_autorizado_cargo]               VARCHAR (55)  NULL,
    [cd_examinado_assinatura]           VARCHAR (5)   NULL,
    [cd_examinado_grupo]                VARCHAR (1)   NULL,
    [cd_examinado_orgao]                VARCHAR (2)   NULL,
    [nm_examinado_assinatura]           VARCHAR (55)  NULL,
    [ds_examinado_cargo]                VARCHAR (55)  NULL,
    [cd_responsavel_assinatura]         VARCHAR (5)   NULL,
    [cd_responsavel_grupo]              VARCHAR (1)   NULL,
    [cd_responsavel_orgao]              VARCHAR (2)   NULL,
    [nm_responsavel_assinatura]         VARCHAR (55)  NULL,
    [ds_responsavel_cargo]              VARCHAR (55)  NULL,
    [dt_emissao_reserva]                DATE          NULL,
    [ds_status_siafem_siafisico]        VARCHAR (1)   NULL,
    [ds_status_prodesp]                 VARCHAR (1)   NULL,
    [ds_status_documento]               BIT           NULL,
    [dt_transmissao_prodesp]            DATE          NULL,
    [dt_transmissao_siafem_siafisico]   DATE          NULL,
    [dt_cadastro]                       DATE          NULL,
    [bl_cadastro_completo]              BIT           NULL,
    [ds_msgRetornoTransmissaoProdesp]   VARCHAR (150) NULL,
    [ds_msgRetornoTransSiafemSiafisico] VARCHAR (150) NULL,
    CONSTRAINT [PK_tb_reserva] PRIMARY KEY CLUSTERED ([id_reserva] ASC),
    CONSTRAINT [FK_tb_reserva_tb_estrutura] FOREIGN KEY ([id_estrutura]) REFERENCES [configuracao].[tb_estrutura] ([id_estrutura]),
    CONSTRAINT [FK_tb_reserva_tb_fonte] FOREIGN KEY ([id_fonte]) REFERENCES [configuracao].[tb_fonte] ([id_fonte]),
    CONSTRAINT [FK_tb_reserva_tb_programa] FOREIGN KEY ([id_programa]) REFERENCES [configuracao].[tb_programa] ([id_programa]),
    CONSTRAINT [FK_tb_reserva_tb_regional] FOREIGN KEY ([id_regional]) REFERENCES [seguranca].[tb_regional] ([id_regional]),
    CONSTRAINT [FK_tb_reserva_tb_tipo_reserva] FOREIGN KEY ([id_tipo_reserva]) REFERENCES [reserva].[tb_tipo_reserva] ([id_tipo_reserva])
);




GO
CREATE NONCLUSTERED INDEX [tb_reserva_FKIndex1]
    ON [reserva].[tb_reserva]([id_programa] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_reserva_FKIndex3]
    ON [reserva].[tb_reserva]([id_fonte] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_reserva_FKIndex4]
    ON [reserva].[tb_reserva]([id_tipo_reserva] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_reserva_FKIndex6]
    ON [reserva].[tb_reserva]([id_regional] ASC);


GO
CREATE NONCLUSTERED INDEX [tb_reserva_FKIndex7]
    ON [reserva].[tb_reserva]([id_estrutura] ASC);

