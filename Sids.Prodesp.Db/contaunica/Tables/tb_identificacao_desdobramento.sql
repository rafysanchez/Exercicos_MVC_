CREATE TABLE [contaunica].[tb_identificacao_desdobramento] (
    [id_identificacao_desdobramento]  INT             IDENTITY (1, 1) NOT NULL,
    [id_reter]                        INT             NULL,
    [id_desdobramento]                INT             NULL,
    [ds_nome_reduzido_credor]         VARCHAR (20)    NULL,
    [vr_percentual_base_calculo]      DECIMAL (18, 2) NULL,
    [vr_desdobrado]                   DECIMAL (18, 2) NULL,
    [vr_desdobrado_inicial]           DECIMAL (18, 2) NULL,
    [bl_tipo_bloqueio]                VARCHAR (10)    NULL,
    [bl_transmitido_prodesp]          BIT             NULL,
    [ds_status_prodesp]               CHAR (1)        NULL,
    [dt_transmitido_prodesp]          DATE            NULL,
    [ds_transmissao_mensagem_prodesp] VARCHAR (128)   NULL,
    [vr_distribuicao]                 DECIMAL (18, 2) NULL,
    [id_tipo_desdobramento]           INT             NULL,
    [nr_sequencia]                    INT             NULL,
    CONSTRAINT [PK_tb_identificacao_desdobramento] PRIMARY KEY CLUSTERED ([id_identificacao_desdobramento] ASC),
    CONSTRAINT [FK_tb_identificacao_desdobramento_tb_desdobramento] FOREIGN KEY ([id_desdobramento]) REFERENCES [contaunica].[tb_desdobramento] ([id_desdobramento]),
    CONSTRAINT [FK_tb_identificacao_desdobramento_tb_Reter] FOREIGN KEY ([id_reter]) REFERENCES [contaunica].[tb_Reter] ([id_Reter]),
    CONSTRAINT [FK_tb_identificacao_desdobramento_tb_tipo_desdobramento] FOREIGN KEY ([id_tipo_desdobramento]) REFERENCES [contaunica].[tb_tipo_desdobramento] ([id_tipo_desdobramento])
);



