CREATE TABLE [empenho].[tb_empenho_cancelamento_mes] (
    [id_empenho_cancelamento_mes]                     INT          IDENTITY (1, 1) NOT NULL,
    [tb_empenho_cancelamento_id_empenho_cancelamento] INT          NOT NULL,
    [ds_mes]                                          VARCHAR (9)  NULL,
    [vr_mes]                                          DECIMAL (18) NULL,
    CONSTRAINT [PK_tb_empenho_cancelamento_mes] PRIMARY KEY CLUSTERED ([id_empenho_cancelamento_mes] ASC),
    CONSTRAINT [FK_tb_empenho_cancelamento_mes_tb_empenho_cancelamento] FOREIGN KEY ([tb_empenho_cancelamento_id_empenho_cancelamento]) REFERENCES [empenho].[tb_empenho_cancelamento] ([id_empenho_cancelamento])
);



