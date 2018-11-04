CREATE TABLE [reserva].[tb_reforco_mes] (
    [id_reforco_mes] INT          IDENTITY (1, 1) NOT NULL,
    [id_reforco]     INT          NOT NULL,
    [ds_mes]         VARCHAR (9)  NULL,
    [vr_mes]         DECIMAL (20) NULL,
    CONSTRAINT [PK_tb_reforco_mes] PRIMARY KEY CLUSTERED ([id_reforco_mes] ASC),
    CONSTRAINT [FK_tb_reforco_mes_tb_reforco] FOREIGN KEY ([id_reforco]) REFERENCES [reserva].[tb_reforco] ([id_reforco])
);



