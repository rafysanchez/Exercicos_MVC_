CREATE TABLE [empenho].[tb_empenho_reforco_mes] (
    [id_empenho_reforco_mes]                INT          IDENTITY (1, 1) NOT NULL,
    [tb_empenho_reforco_id_empenho_reforco] INT          NOT NULL,
    [ds_mes]                                VARCHAR (9)  NULL,
    [vr_mes]                                DECIMAL (18) NULL,
    CONSTRAINT [PK_tb_empenho_reforco_mes] PRIMARY KEY CLUSTERED ([id_empenho_reforco_mes] ASC)
);



