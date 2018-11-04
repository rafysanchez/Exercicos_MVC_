CREATE TABLE [empenho].[tb_empenho_cancelamento_tipo] (
    [id_empenho_cancelamento_tipo] INT           NOT NULL,
    [ds_empenho_cancelamento_tipo] VARCHAR (140) NOT NULL,
    [nm_web_service]               VARCHAR (140) NOT NULL,
    [fl_siafem]                    BIT           NOT NULL,
    CONSTRAINT [PK_tb_empenho_cancelamento_tipo] PRIMARY KEY CLUSTERED ([id_empenho_cancelamento_tipo] ASC)
);



