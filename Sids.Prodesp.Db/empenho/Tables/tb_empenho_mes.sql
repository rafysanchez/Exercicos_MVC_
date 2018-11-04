CREATE TABLE [empenho].[tb_empenho_mes] (
    [id_empenho_mes]        INT          IDENTITY (1, 1) NOT NULL,
    [tb_empenho_id_empenho] INT          NOT NULL,
    [ds_mes]                VARCHAR (9)  NULL,
    [vr_mes]                DECIMAL (18) NULL,
    CONSTRAINT [PK_tb_empenho_mes] PRIMARY KEY CLUSTERED ([id_empenho_mes] ASC),
    CONSTRAINT [FK_tb_empenho_mes_tb_empenho] FOREIGN KEY ([tb_empenho_id_empenho]) REFERENCES [empenho].[tb_empenho] ([id_empenho])
);



